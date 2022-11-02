using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Expenditures;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class ExpenditureCsvMap : ClassMap<ExpenditureDto>
{
    public ExpenditureCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(c => c.Approved).Ignore();
        Map(c => c.Audit!.CreatedAt).Ignore();
        Map(c => c.Audit!.CreatedBy).Ignore();
        Map(c => c.Audit!.LastModified).Ignore();
        Map(c => c.Audit!.LastModifiedBy).Ignore();
    }
}