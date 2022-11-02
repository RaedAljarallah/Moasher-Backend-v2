using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Risks.Queries.ExportRisks;

public record ExportedRisksDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);