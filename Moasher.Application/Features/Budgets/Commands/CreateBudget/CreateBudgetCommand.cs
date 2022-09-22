using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Budgets;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Budgets.Commands.CreateBudget;

public record CreateBudgetCommand : BudgetCommandBase, IRequest<BudgetDto>;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateBudgetCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Budgets)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new BudgetDomainValidator(initiative, request.ApprovalDate, request.Amount));
        
        var budget = _mapper.Map<InitiativeBudget>(request);
        budget.Initiative = initiative;
        budget.InitialAmount = request.Amount;
        budget.AddDomainEvent(new BudgetCreatedEvent(budget));
        initiative.Budgets.Add(budget);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<BudgetDto>(budget);
    }
}