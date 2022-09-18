using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Initiatives;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.Initiatives.Commands.EventHandlers;

public class InitiativeStatusUpdateEventHandler : INotificationHandler<InitiativeStatusUpdateEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InitiativeStatusUpdateEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Handle(InitiativeStatusUpdateEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var initiativeId = notification.Initiative.Id;

        var initiative = await context.Initiatives
            .Include(i => i.Milestones)
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);

        if (initiative is not null)
        {
            if (initiative.Milestones.Any())
            {
                var statusEnums = await context.EnumTypes
                    .Where(e => string.Equals(e.Category, EnumTypeCategory.InitiativeStatus.ToString(),
                        StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync(cancellationToken);

                initiative.SetStatus(statusEnums);
            }
            else
            {
                initiative.StatusEnum = null;
            }
            
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}