using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Budgets;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class BudgetCsvMap : ClassMap<BudgetDto>
{
    public BudgetCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(b => b.Approved).Ignore();
        Map(b => b.Audit!.CreatedAt).Ignore();
        Map(b => b.Audit!.CreatedBy).Ignore();
        Map(b => b.Audit!.LastModified).Ignore();
        Map(b => b.Audit!.LastModifiedBy).Ignore();
        Map(b => b.ApprovalDate).Convert(b => $"{b.Value.ApprovalDate:yyyy-MM-dd}");
    }
}