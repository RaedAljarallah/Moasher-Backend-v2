namespace Moasher.Application.Features.StrategicObjectives;

public record StrategicObjectiveLevelFourDto : StrategicObjectiveDtoBase
{
    public StrategicObjectiveDtoBase LevelOne { get; set; } = default!;
    public StrategicObjectiveDtoBase LevelTwo { get; set; } = default!;
    public StrategicObjectiveDtoBase LevelThree { get; set; } = default!;
}