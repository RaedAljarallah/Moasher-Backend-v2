namespace Moasher.Application.Features.DataExporting;

public record ExportedDataDto
{
    public string? Entities { get; set; }
    public string? Objectives { get; set; }
    
}