﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Contracts.Commands.Common;
using Moasher.Application.Features.Expenditures.Commands.CreateContractExpenditure;
using Moasher.Domain.Entities.InitiativeEntities;
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
                throw new ValidationException(nameof(request.StatusEnumId), ContractEnumsValidationMessages.WrongStatusEnumId);
            }

            contract.StatusEnum = statusEnum;
        }

        var expenditurePlanChanged = false;
        var originalExpenditures = new List<InitiativeExpenditure>();
        if (IsDifferentExpenditures(request, contract))
        {
            expenditurePlanChanged = true;
            originalExpenditures = new List<InitiativeExpenditure>(contract.Expenditures);
            _context.InitiativeExpenditures.RemoveRange(contract.Expenditures);
            foreach (var expenditure in request.Expenditures)
            {
                if (expenditure.PlannedAmount == 0 && !expenditure.ActualAmount.HasValue)
                {
                    // a validation to skip the empty expenditures
                    continue;
                }
                
                var expenditureValidator = new CreateContractExpenditureCommandValidator();
                expenditureValidator.SetValidationArguments(request.StartDate,
                    request.EndDate);
        
                var expenditureValidationResult = expenditureValidator.Validate(expenditure);
                if (!expenditureValidationResult.IsValid)
                {
                    throw new ValidationException(expenditureValidationResult.Errors);
                }
        
                var mappedExpenditure = _mapper.Map<InitiativeExpenditure>(expenditure);
                contract.Expenditures.Add(mappedExpenditure);
            }
        }

        _mapper.Map(request, contract);
        if (!expenditurePlanChanged)
        {
            contract.Expenditures = originalExpenditures;
        }
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