using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.StrategicObjectives.Queries.ExportStrategicObjectives;

public record ExportedStrategicObjectivesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);