using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Contracts.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Contracts;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Contracts.Commands.CreateContract;

public record CreateContractCommand : ContractCommandBase, IRequest<ContractDto>
{
    public Guid ProjectId { get; set; }
}

public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, ContractDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateContractCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContractDto> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Contracts)
            .ThenInclude(c => c.Project)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new ContractDomainValidator(initiative, request.Name, request.RefNumber,
            request.Amount, request.StartDate, request.EndDate));

        var project = await _context.InitiativeProjects
            .Include(p => p.Expenditures)
            .Include(p => p.ExpendituresBaseline)
            .Include(p => p.Progress)
            .Include(p => p.ContractMilestones)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);
        
        if (project is null)
        {
            throw new NotFoundException();
        }

        var statusEnum = await _context.EnumTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.StatusEnumId, cancellationToken);

        if (statusEnum is null)
        {
            throw new ValidationException(nameof(request.StatusEnumId), ContractEnumsValidationMessages.WrongStatusEnumId);
        }

        var contract = _mapper.Map<InitiativeContract>(request);
        contract.StatusEnum = statusEnum;
        contract.Initiative = initiative;
        
        project.Contracting(contract.StartDate);
        var totalExpenditureAmount = project.Expenditures.Sum(e => e.PlannedAmount);
        project.Expenditures.ToList().ForEach(e => e.MoveToContract(contract));
        project.ExpendituresBaseline.ToList().ForEach(b => b.MoveToContract(contract));
        project.ContractMilestones.ToList().ForEach(cm => cm.MoveToContract(contract));
        var projectActiveProgressItem = contract.Project?.Progress.FirstOrDefault(p => !p.Completed);
        projectActiveProgressItem?.Complete();
        
        contract.Project = project;
        contract.BalancedExpenditurePlan = totalExpenditureAmount == contract.Amount;
        
        contract.AddDomainEvent(new ContractCreatedEvent(contract));
        initiative.Contracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ContractDto>(contract);

    }
}