namespace Moasher.Application.Features.Programs.Commands;

public record ProgramCommandBase
{
    private string _code = default!;
    private string _name = default!;

    public string Code { get => _code; set => _code = value.Trim(); }
    public string Name { get => _name; set => _name = value.Trim(); }
}