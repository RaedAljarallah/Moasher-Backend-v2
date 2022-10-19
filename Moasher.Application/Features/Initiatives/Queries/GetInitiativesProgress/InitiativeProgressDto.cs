using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesProgress;

public record InitiativeProgressDto
{
    private EnumType? _statusEnum;
    public int Year { get; set; }
    public Month Month { get; set; }
    public float PlannedProgressCumulative { get; set; }
    public float ActualProgressCumulative { get; set; }
    public EnumValue? Status => GetStatus();

    public void SetStatusEnum(EnumType? statusEnum)
    {
        _statusEnum = statusEnum;
    }

    private EnumValue? GetStatus()
    {
        return _statusEnum is not null 
            ? new EnumValue(_statusEnum.Name, _statusEnum.Style) 
            : default;
    }
}