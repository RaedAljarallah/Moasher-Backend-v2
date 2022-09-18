using Moasher.Domain.Entities;

namespace Moasher.Domain.Extensions;

public static class ProgramExtensions
{
    public static IEnumerable<string> GetStrategicObjectives(this Program program)
    {
        var initiativeObjectives = program.Initiatives.Select(i => i.LevelThreeStrategicObjectiveName);

        var kpiObjectives = program.Initiatives.Select(i => i.LevelThreeStrategicObjective)
            .SelectMany(o => o.KPIs).Select(k => k.LevelThreeStrategicObjectiveName);

        return initiativeObjectives.Union(kpiObjectives);
    }

    public static IEnumerable<string> GetKPIs(this Program program)
    {
        return program.Initiatives
            .Select(i => i.LevelThreeStrategicObjective)
            .SelectMany(o => o.KPIs).Select(k => k.Name).Distinct();
    }

    public static IEnumerable<string> GetEntities(this Program program)
    {
        var initiativeEntities = program.Initiatives.Select(i => i.EntityName);
        return initiativeEntities.Distinct();
    }
}