using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Analytics.Queries.ExportAnalytics;

public record ExportedAnalyticsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);