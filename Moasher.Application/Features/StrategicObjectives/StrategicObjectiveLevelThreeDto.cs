namespace Moasher.Application.Features.StrategicObjectives;

public record StrategicObjectiveLevelThreeDto : StrategicObjectiveDtoBase
{
    public StrategicObjectiveDtoBase LevelOne { get; set; } = default!;
    public StrategicObjectiveDtoBase LevelTwo { get; set; } = default!;
    public int LevelFourCount { get; set; }
}