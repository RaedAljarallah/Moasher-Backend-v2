namespace Moasher.Application.Features.StrategicObjectives.Commands;

public abstract record StrategicObjectiveCommandBase
{
    private string _code = default!;
    private string _name = default!;
    private string _hierarchyId = default!;

    public string HierarchyId { get => _hierarchyId; set => _hierarchyId = value.Trim(); }
    public string Code { get => _code; set => _code = value.Trim(); }
    public string Name { get => _name; set => _name = value.Trim(); }
    public short Level { get; set; }
}