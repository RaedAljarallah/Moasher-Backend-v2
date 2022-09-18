using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Events.StrategicObjectives;

public class LevelFourStrategicObjectiveDeletedEvent : DomainEvent
{
    public StrategicObjective StrategicObjective { get; }

    public LevelFourStrategicObjectiveDeletedEvent(StrategicObjective strategicObjective)
    {
        StrategicObjective = strategicObjective;
    }
}