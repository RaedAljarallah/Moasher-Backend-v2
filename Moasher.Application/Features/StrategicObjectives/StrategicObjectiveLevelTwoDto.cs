namespace Moasher.Application.Features.StrategicObjectives;

public record StrategicObjectiveLevelTwoDto : StrategicObjectiveDtoBase
{
    public StrategicObjectiveDtoBase LevelOne { get; set; } = default!;
    public int LevelThreeCount { get; set; }
    public int LevelFourCount { get; set; }
}