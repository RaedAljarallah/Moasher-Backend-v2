using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Contracts;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class ContractCsvMap : ClassMap<ContractDto>
{
    public ContractCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(c => c.Approved).Ignore();
        Map(c => c.Audit!.CreatedAt).Ignore();
        Map(c => c.Audit!.CreatedBy).Ignore();
        Map(c => c.Audit!.LastModified).Ignore();
        Map(c => c.Audit!.LastModifiedBy).Ignore();
        Map(c => c.StartDate).Convert(c => $"{c.Value.StartDate:yyyy-MM-dd}");
        Map(c => c.EndDate).Convert(c => $"{c.Value.EndDate:yyyy-MM-dd}");
        Map(k => k.Status.Name).Name("Status_Name");
        Map(k => k.Status.Style).Name("Status_Style");
    }
}