using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Issues.Queries.ExportIssues;

public record ExportedIssuesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);