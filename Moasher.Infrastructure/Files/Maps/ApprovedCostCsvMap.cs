using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.ApprovedCosts;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class ApprovedCostCsvMap : ClassMap<ApprovedCostDto>
{
    public ApprovedCostCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(a => a.Approved).Ignore();
        Map(a => a.Audit!.CreatedAt).Ignore();
        Map(a => a.Audit!.CreatedBy).Ignore();
        Map(a => a.Audit!.LastModified).Ignore();
        Map(a => a.Audit!.LastModifiedBy).Ignore();
        Map(a => a.ApprovalDate).Convert(a => $"{a.Value.ApprovalDate:yyyy-MM-dd}");
    }
}