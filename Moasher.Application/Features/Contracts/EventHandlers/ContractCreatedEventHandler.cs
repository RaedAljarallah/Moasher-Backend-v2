using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Events.Contracts;

namespace Moasher.Application.Features.Contracts.EventHandlers;

public class ContractCreatedEventHandler : INotificationHandler<ContractCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public ContractCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ContractCreatedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Contract.InitiativeId;
        
        var initiative = await _context.Initiatives
            .IgnoreQueryFilters()
            .Include(i => i.Contracts)
            .ThenInclude(c => c.Expenditures)
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);
        
        if (initiative is not null)
        {
            initiative.SetContractsAmount();
            initiative.SetTotalExpenditure();
            initiative.SetCurrentYearExpenditure();
        
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}