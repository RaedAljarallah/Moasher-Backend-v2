namespace Moasher.Domain.Entities.StrategicObjectiveEntities;

public class StrategicObjectiveLevelOne : StrategicObjective
{
    public int LevelTwoCount { get; set; }
    public int LevelThreeCount { get; set; }
    public int LevelFourCount { get; set; }
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
}