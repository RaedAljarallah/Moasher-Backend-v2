namespace Moasher.Application.Features.StrategicObjectives;

public record StrategicObjectiveLevelOneDto : StrategicObjectiveDtoBase
{
    public int LevelTwoCount { get; set; }
    public int LevelThreeCount { get; set; }
    public int LevelFourCount { get; set; }
}