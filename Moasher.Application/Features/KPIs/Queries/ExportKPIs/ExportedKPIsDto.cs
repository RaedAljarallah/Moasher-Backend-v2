using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.KPIs.Queries.ExportKPIs;

public record ExportedKPIsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);