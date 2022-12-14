using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Events.Contracts;

namespace Moasher.Application.Features.Contracts.EventHandlers;

public class ContractUpdatedEventHandler : INotificationHandler<ContractUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public ContractUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ContractUpdatedEvent notification, CancellationToken cancellationToken)
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
            var contract = initiative.Contracts.FirstOrDefault(c => c.Id == notification.Contract.Id);
            if (contract is not null)
            {
                contract.SetTotalExpenditure();
                contract.SetCurrentYearExpenditure();
            }
            
            initiative.SetContractsAmount();
            initiative.SetTotalExpenditure();
            initiative.SetCurrentYearExpenditure();
        
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}
