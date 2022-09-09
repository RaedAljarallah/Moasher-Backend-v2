namespace Moasher.Domain.Entities.StrategicObjectiveEntities;

public class StrategicObjectiveLevelTwo : StrategicObjective
{
    public StrategicObjective LevelOne { get; set; } = default!;
    public int LevelThreeCount { get; set; }
    public int LevelFourCount { get; set; }
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
}