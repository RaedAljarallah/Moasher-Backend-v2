namespace Moasher.Domain.Entities.StrategicObjectiveEntities;

public class StrategicObjectiveLevelFour : StrategicObjective
{
    public StrategicObjective LevelOne { get; set; } = default!;
    public StrategicObjective LevelTwo { get; set; } = default!;
    public StrategicObjective LevelThree { get; set; } = default!;
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
}