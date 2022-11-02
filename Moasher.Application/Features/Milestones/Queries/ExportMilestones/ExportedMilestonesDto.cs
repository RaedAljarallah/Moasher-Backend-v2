using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Milestones.Queries.ExportMilestones;

public record ExportedMilestonesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);