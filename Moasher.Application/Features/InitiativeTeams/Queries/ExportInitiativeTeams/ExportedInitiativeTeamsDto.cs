using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.InitiativeTeams.Queries.ExportInitiativeTeams;

public record ExportedInitiativeTeamsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);