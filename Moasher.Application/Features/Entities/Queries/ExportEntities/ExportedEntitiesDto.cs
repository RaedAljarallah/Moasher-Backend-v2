using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Entities.Queries.ExportEntities;

public record ExportedEntitiesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);