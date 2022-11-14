using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Contracts.Commands.Common;
using Moasher.Application.Features.Expenditures.Commands.CreateContractExpenditure;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Contracts;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Contracts.Commands.UpdateContract;

public record UpdateContractCommand : ContractCommandBase, IRequest<ContractDto>
{
    public Guid Id { get; set; }

    public IEnumerable<CreateContractExpenditureCommand> Expenditures { get; set; } =
        Enumerable.Empty<CreateContractExpenditureCommand>();
}

public class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, ContractDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateContractCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContractDto> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Budgets)
            .Include(i => i.Contracts)
            .ThenInclude(c => c.Expenditures)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var contract = initiative.Contracts.FirstOrDefault(c => c.Id == request.Id);
        if (contract is null)
        {
            throw new NotFoundException();
        }

        // TODO: Clone initiative here and everywhere else to prevent updating initiative when saving
        initiative.Contracts = initiative.Contracts.Where(c => c.Id != request.Id).ToList();
        request.ValidateAndThrow(new ContractDomainValidator(initiative, request.Name, request.RefNumber,
            request.Amount, request.StartDate, request.EndDate));

        if (request.StatusEnumId != contract.StatusEnumId)
        {
            var statusEnum = await _context.EnumTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.StatusEnumId, cancellationToken);

            if (statusEnum is null)
            {
                throw new ValidationException(nameof(request.StatusEnumId),
                    ContractEnumsValidationMessages.WrongStatusEnumId);
            }

            contract.StatusEnum = statusEnum;
        }

        if (IsDifferentExpenditures(request, contract))
        {
            _context.InitiativeExpenditures.RemoveRange(contract.Expenditures);
            foreach (var expenditure in request.Expenditures)
            {
                if (expenditure.PlannedAmount == 0 && expenditure.ActualAmount is null or 0)
                {
                    // a validation to skip the empty expenditures
                    continue;
                }

                var expenditureValidator = new CreateContractExpenditureCommandValidator();
                expenditureValidator.SetValidationArguments(request.StartDate, request.EndDate);

                var expenditureValidationResult = expenditureValidator.Validate(expenditure);
                if (!expenditureValidationResult.IsValid)
                {
                    throw new ValidationException(expenditureValidationResult.Errors);
                }

                var mappedExpenditure = _mapper.Map<InitiativeExpenditure>(expenditure);
                if (mappedExpenditure.ActualAmount is 0)
                {
                    mappedExpenditure.ActualAmount = default;
                }

                contract.Expenditures.Add(mappedExpenditure);
            }

            // Validate actual expenditures against year budget
            var budgets = initiative.Budgets
                .GroupBy(b => b.ApprovalDate.Year)
                .OrderBy(b => b.Key)
                .Select(budget => new {Year = budget.Key, Amount = budget.Sum(b => b.Amount)})
                .ToList();

            var actualExpenditures = contract.Expenditures
                .GroupBy(e => e.Year)
                .OrderBy(e => e.Key)
                .Select(expenditure => new
                {
                    Year = expenditure.Key, 
                    CurrentActualAmount = expenditure.Where(e => e.Id == Guid.Empty).Sum(e => e.ActualAmount ?? 0),
                    OriginalActualAmount = expenditure.Where(e => e.Id != Guid.Empty).Sum(e => e.ActualAmount ?? 0)
                })
                .ToList();
            
            actualExpenditures.ForEach(expenditure =>
            {
                if (budgets.All(b => b.Year != expenditure.Year))
                {
                    throw new ValidationException(nameof(InitiativeContract.Expenditures),
                        $"لا يمكن إضافة مصاريف فعلية لسنة {expenditure.Year} لعدم وجود ميزانية للسنة");
                }
            });

            var currentExpenditures = actualExpenditures.Sum(e => e.CurrentActualAmount);
            var originalExpenditures = actualExpenditures.Sum(e => e.OriginalActualAmount);
            var totalActualExpenditures = initiative.TotalExpenditure - originalExpenditures + currentExpenditures;
            if (totalActualExpenditures > (initiative.TotalBudget ?? 0))
            {
                throw new ValidationException(nameof(InitiativeContract.Expenditures),
                    $"إجمالي الصرف الفعلي المدخل [{totalActualExpenditures:N0}] أعلى من إجمالي ميزانية المبادرة [{initiative.TotalBudget ?? 0:N0}]");
            }
        }

        _mapper.Map(request, contract);

        // It's safe to make BalancedExpenditurePlan 'true'
        // since we've validated the total expenditures amount against the contract amount
        contract.BalancedExpenditurePlan = true;
        contract.AddDomainEvent(new ContractUpdatedEvent(contract));
        _context.InitiativeContracts.Update(contract);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ContractDto>(contract);
    }

    private static bool IsDifferentExpenditures(UpdateContractCommand request, InitiativeContract contract)
    {
        var currentExpenditures = request.Expenditures
            .OrderByDescending(e => e.Year)
            .ThenBy(e => e.Month)
            .Select(e => new
            {
                e.Year,
                e.Month,
                e.PlannedAmount,
                e.ActualAmount
            }).ToList();

        var originalExpenditures = contract.Expenditures
            .OrderByDescending(e => e.Year)
            .ThenBy(e => e.Month)
            .Select(e => new
            {
                e.Year,
                e.Month,
                e.PlannedAmount,
                e.ActualAmount
            }).ToList();

        if (currentExpenditures.Count != originalExpenditures.Count)
        {
            return true;
        }

        if (!currentExpenditures.SequenceEqual(originalExpenditures))
        {
            return true;
        }

        return false;
    }
}