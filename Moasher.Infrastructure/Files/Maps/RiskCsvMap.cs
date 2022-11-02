using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Risks;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class RiskCsvMap : ClassMap<RiskDto>
{
    public RiskCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(r => r.Approved).Ignore();
        Map(r => r.Audit!.CreatedAt).Ignore();
        Map(r => r.Audit!.CreatedBy).Ignore();
        Map(r => r.Audit!.LastModified).Ignore();
        Map(r => r.Audit!.LastModifiedBy).Ignore();
        Map(r => r.RaisedAt).Convert(r => $"{r.Value.RaisedAt:yyyy-MM-dd}");
        Map(r => r.Type.Name).Name("Type_Name");
        Map(r => r.Type.Style).Name("Type_Style");
        Map(r => r.Priority.Name).Name("Priority_Name");
        Map(r => r.Priority.Style).Name("Priority_Style");
        Map(r => r.Probability.Name).Name("Probability_Name");
        Map(r => r.Probability.Style).Name("Probability_Style");
        Map(r => r.Impact.Name).Name("Impact_Name");
        Map(r => r.Impact.Style).Name("Impact_Style");
        Map(r => r.Scope.Name).Name("Scope_Name");
        Map(r => r.Scope.Style).Name("Scope_Style");
    }
}