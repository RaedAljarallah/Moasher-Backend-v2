namespace Moasher.Domain.Entities.StrategicObjectiveEntities;

public class StrategicObjectiveLevelThree : StrategicObjective
{
    public StrategicObjective LevelOne { get; set; } = default!;
    public StrategicObjective LevelTwo { get; set; } = default!;
    public int LevelFourCount { get; set; }
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
}