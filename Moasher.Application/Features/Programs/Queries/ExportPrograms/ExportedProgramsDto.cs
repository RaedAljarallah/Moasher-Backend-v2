using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Programs.Queries.ExportPrograms;

public record ExportedProgramsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);