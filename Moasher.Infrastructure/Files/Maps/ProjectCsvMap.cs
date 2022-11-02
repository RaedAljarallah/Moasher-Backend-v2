using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Projects;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class ProjectCsvMap : ClassMap<ProjectDto>
{
    public ProjectCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(p => p.Approved).Ignore();
        Map(p => p.Audit!.CreatedAt).Ignore();
        Map(p => p.Audit!.CreatedBy).Ignore();
        Map(p => p.Audit!.LastModified).Ignore();
        Map(p => p.Audit!.LastModifiedBy).Ignore();
        Map(p => p.PlannedBiddingDate).Convert(p => $"{p.Value.PlannedBiddingDate:yyyy-MM-dd}");
        Map(p => p.ActualBiddingDate)
            .Convert(p => p.Value.ActualBiddingDate.HasValue ? $"{p.Value.ActualBiddingDate.Value:yyyy-MM-dd}" : "");
        Map(p => p.PlannedContractingDate).Convert(p => $"{p.Value.PlannedContractingDate:yyyy-MM-dd}");
        Map(p => p.PlannedContractEndDate).Convert(p => $"{p.Value.PlannedContractEndDate:yyyy-MM-dd}");
        Map(p => p.Phase.Name).Name("Phase_Name");
        Map(p => p.Phase.Style).Name("Phase_Style");
    }
}