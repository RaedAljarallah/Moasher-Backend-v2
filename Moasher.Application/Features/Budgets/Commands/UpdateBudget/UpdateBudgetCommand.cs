using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Budgets;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Budgets.Commands.UpdateBudget;

public record UpdateBudgetCommand : BudgetCommandBase, IRequest<BudgetDto>
{
    public Guid Id { get; set; }
}

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBudgetCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<BudgetDto> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Budgets)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        var budget = initiative.Budgets.FirstOrDefault(b => b.Id == request.Id);
        if (budget is null)
        {
            throw new NotFoundException();
        }
        
        initiative.Budgets = initiative.Budgets.Where(b => b.Id != request.Id).ToList();
        request.ValidateAndThrow(new BudgetDomainValidator(initiative, request.ApprovalDate, request.Amount));
        
        _mapper.Map(request, budget);
        budget.AddDomainEvent(new BudgetUpdatedEvent(budget));
        _context.InitiativeBudgets.Update(budget);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BudgetDto>(budget);
    }
}