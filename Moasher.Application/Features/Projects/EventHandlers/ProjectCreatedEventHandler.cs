using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Projects;

namespace Moasher.Application.Features.Projects.EventHandlers;

public class ProjectCreatedEventHandler : INotificationHandler<ProjectCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public ProjectCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
    {
        var projectId = notification.Project.Id;
        var project = await _context.InitiativeProjects
            .Include(p => p.PhaseEnum)
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);

        if (project is not null)
        {
            var progressItem = InitiativeProjectProgress.CreateProjectProgressItem(project);
            project.Progress.Add(progressItem);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}