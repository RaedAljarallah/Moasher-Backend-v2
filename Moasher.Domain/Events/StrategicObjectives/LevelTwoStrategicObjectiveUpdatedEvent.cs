using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Events.StrategicObjectives;

public class LevelTwoStrategicObjectiveUpdatedEvent : DomainEvent
{
    public StrategicObjective StrategicObjective { get; }

    public LevelTwoStrategicObjectiveUpdatedEvent(StrategicObjective strategicObjective)
    {
        StrategicObjective = strategicObjective;
    }
}