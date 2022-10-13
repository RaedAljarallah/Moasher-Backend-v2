using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Projects;

public class ProjectCreatedEvent : DomainEvent
{
    public InitiativeProject Project { get; }

    public ProjectCreatedEvent(InitiativeProject project)
    {
        Project = project;
    }
}