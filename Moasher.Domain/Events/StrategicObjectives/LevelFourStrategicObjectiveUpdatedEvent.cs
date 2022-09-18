using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Events.StrategicObjectives;

public class LevelFourStrategicObjectiveUpdatedEvent : DomainEvent
{
    public StrategicObjective StrategicObjective { get; }

    public LevelFourStrategicObjectiveUpdatedEvent(StrategicObjective strategicObjective)
    {
        StrategicObjective = strategicObjective;
    }
}