using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Initiatives.Queries.ExportInitiatives;

public record ExportedInitiativesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);