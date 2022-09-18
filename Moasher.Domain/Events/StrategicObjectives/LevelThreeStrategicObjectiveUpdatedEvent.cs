using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Events.StrategicObjectives;

public class LevelThreeStrategicObjectiveUpdatedEvent : DomainEvent
{
    public StrategicObjective StrategicObjective { get; }

    public LevelThreeStrategicObjectiveUpdatedEvent(StrategicObjective strategicObjective)
    {
        StrategicObjective = strategicObjective;
    }
}