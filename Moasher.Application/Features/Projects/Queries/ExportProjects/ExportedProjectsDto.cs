using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Projects.Queries.ExportProjects;

public record ExportedProjectsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);