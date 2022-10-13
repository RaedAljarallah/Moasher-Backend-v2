﻿using MediatR;
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
        var contract = await _context.InitiativeContracts
            .Include(c => c.Project)
            .ThenInclude(p => p.Expenditures)
            .Include(c => c.Project)
            .ThenInclude(p => p.ExpendituresBaseline)
            .FirstOrDefaultAsync(c => c.Id == notification.Contract.Id, cancellationToken);

        if (contract is not null)
        {
            contract.Project.Expenditures.ToList().ForEach(e => e.MoveToContract(contract));
            contract.Project.ExpendituresBaseline.ToList().ForEach(b => b.MoveToContract(contract));
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        var initiativeId = notification.Contract.InitiativeId;
        
        var initiative = await _context.Initiatives
            .Include(i => i.Contracts)
            .ThenInclude(c => c.Project)
            .ThenInclude(p => p.Expenditures)
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);
        
        if (initiative is not null)
        {
            initiative.SetContractsAmount();
            initiative.SetTotalExpenditure();
            initiative.SetCurrentYearExpenditure();
        
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}