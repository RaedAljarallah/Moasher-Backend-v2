using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Milestones;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.Milestones.EventHandlers;

public class MilestoneCreatedEventHandler : INotificationHandler<MilestoneCreatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public MilestoneCreatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(MilestoneCreatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

        var initiative = notification.Milestone.Initiative;
        initiative.SetProgress();
        if (initiative.CalculateStatus)
        {
            var status = await context.EnumTypes
                .Where(e => e.Category == EnumTypeCategory.InitiativeStatus.ToString())
                .ToListAsync(cancellationToken);

            initiative.SetStatus(status);
        }

        context.Initiatives.Update(initiative);
        await context.SaveChangesAsync(cancellationToken);
    }
}