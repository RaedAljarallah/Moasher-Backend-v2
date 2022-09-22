using Moasher.Domain.Enums;

namespace Moasher.Application.Features.KPIs.Commands;

public abstract record KPICommandBase
{
    private string _code = default!;
    private string _name = default!;
    private string _ownerName = default!;
    private string _ownerEmail = default!;
    private string _ownerPhoneNumber = default!;
    private string? _ownerPosition;
    private string? _formula;
    private string _measurementUnit = default!;
    private string? _dataSource;
    private string _description = default!;
    
    public string Code { get => _code; set => _code = value.Trim(); }
    public string Name { get => _name; set => _name = value.Trim(); }
    public string OwnerName { get => _ownerName; set => _ownerName = value.Trim(); }
    public string OwnerEmail { get => _ownerEmail; set => _ownerEmail = value.Trim(); }
    public string? OwnerPosition { get => _ownerPosition; set => _ownerPosition = value?.Trim(); }
    public string OwnerPhoneNumber { get => _ownerPhoneNumber; set => _ownerPhoneNumber = value.Trim(); }
    public string? Formula { get => _formula; set => _formula = value?.Trim(); }
    public string MeasurementUnit { get => _measurementUnit; set => _measurementUnit = value.Trim(); }
    public string? DataSource { get => _dataSource; set => _dataSource = value?.Trim(); }
    public string Description { get => _description; set => _description = value.Trim(); }
    public Frequency Frequency { get; set; }
    public Polarity Polarity { get; set; }
    public ValidationStatus ValidationStatus { get; set; }
    public float? BaselineValue { get; set; }
    public short? BaselineYear { get; set; }
    public bool Visible { get; set; }
    public bool VisibleOnDashboard { get; set; }
    public bool CalculateStatus { get; set; }
    public Guid? StatusEnumId { get; set; }
    public Guid EntityId { get; set; }
    public Guid LevelThreeStrategicObjectiveId { get; set; }
    public Guid? LevelFourStrategicObjectiveId { get; set; }
}