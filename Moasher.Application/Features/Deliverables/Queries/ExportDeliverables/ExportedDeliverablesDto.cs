using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Deliverables.Queries.ExportDeliverables;

public record ExportedDeliverablesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);