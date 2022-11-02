using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.InitiativeTeams;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class InitiativeTeamCsvMap : ClassMap<InitiativeTeamDto>
{
    public InitiativeTeamCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(t => t.Approved).Ignore();
        Map(t => t.Audit!.CreatedAt).Ignore();
        Map(t => t.Audit!.CreatedBy).Ignore();
        Map(t => t.Audit!.LastModified).Ignore();
        Map(t => t.Audit!.LastModifiedBy).Ignore();
        Map(t => t.Role.Name).Name("Role_Name");
        Map(t => t.Role.Style).Name("Role_Style");
    }
}