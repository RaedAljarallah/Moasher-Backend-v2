namespace Moasher.Application.Common.Abstracts;

public record ExportedCsvFileBase(string FileName, byte[] Content)
{
    public string ContentType { get; } = "text/csv;charset=utf-8";
}