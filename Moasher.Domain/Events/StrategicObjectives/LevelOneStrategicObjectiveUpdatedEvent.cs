using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Events.StrategicObjectives;

public class LevelOneStrategicObjectiveUpdatedEvent : DomainEvent
{
    public StrategicObjective StrategicObjective { get; }

    public LevelOneStrategicObjectiveUpdatedEvent(StrategicObjective strategicObjective)
    {
        StrategicObjective = strategicObjective;
    }
}