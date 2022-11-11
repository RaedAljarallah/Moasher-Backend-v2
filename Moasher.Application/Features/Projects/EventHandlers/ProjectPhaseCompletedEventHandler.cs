using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Projects;

namespace Moasher.Application.Features.Projects.EventHandlers;

public class ProjectPhaseCompletedEventHandler : INotificationHandler<ProjectPhaseCompletedEvent>
{
    private readonly IMoasherDbContext _context;

    public ProjectPhaseCompletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ProjectPhaseCompletedEvent notification, CancellationToken cancellationToken)
    {
        var projectId = notification.Project.Id;
        var project = await _context.InitiativeProjects
            .Include(p => p.PhaseEnum)
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        
        if (project is not null)
        {
            var activeProgressItem = project.Progress.FirstOrDefault(p => !p.Completed);
            activeProgressItem?.Complete();
            
            var newProgressItem = InitiativeProjectProgress.CreateProjectProgressItem(project);
            project.Progress.Add(newProgressItem);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}