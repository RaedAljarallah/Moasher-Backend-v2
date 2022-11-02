using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Programs;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class ProgramCsvMap : ClassMap<ProgramDto>
{
    public ProgramCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(p => p.Approved).Ignore();
        Map(p => p.Audit!.CreatedAt).Ignore();
        Map(p => p.Audit!.CreatedBy).Ignore();
        Map(p => p.Audit!.LastModified).Ignore();
        Map(p => p.Audit!.LastModifiedBy).Ignore();
    }
}