using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Issues;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class IssueCsvMap : ClassMap<IssueDto>
{
    public IssueCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(i => i.Approved).Ignore();
        Map(i => i.Audit!.CreatedAt).Ignore();
        Map(i => i.Audit!.CreatedBy).Ignore();
        Map(i => i.Audit!.LastModified).Ignore();
        Map(i => i.Audit!.LastModifiedBy).Ignore();
        Map(i => i.RaisedAt).Convert(i => $"{i.Value.RaisedAt:yyyy-MM-dd}");
        Map(i => i.EstimatedResolutionDate)
            .Convert(i => i.Value.EstimatedResolutionDate.HasValue 
                ? $"{i.Value.EstimatedResolutionDate.Value:yyyy-MM-dd}" 
                : "");
        Map(i => i.ClosedAt).Convert(i => i.Value.ClosedAt.HasValue ? $"{i.Value.ClosedAt.Value:yyyy-MM-dd}" : "");
        Map(i => i.Scope.Name).Name("Scope_Name");
        Map(i => i.Scope.Style).Name("Scope_Style");
        Map(i => i.Status.Name).Name("Status_Name");
        Map(i => i.Status.Style).Name("Status_Style");
        Map(i => i.Impact.Name).Name("Impact_Name");
        Map(i => i.Impact.Style).Name("Impact_Style");
    }
}