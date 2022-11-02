using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.ApprovedCosts.Queries.ExportApprovedCosts;

public record ExportedApprovedCostsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);