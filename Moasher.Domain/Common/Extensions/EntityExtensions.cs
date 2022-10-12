using Moasher.Domain.Entities;

namespace Moasher.Domain.Common.Extensions;

public static class EntityExtensions
{
    public static IEnumerable<string> GetStrategicObjectives(this Entity entity)
    {
        var kpiObjectives = entity.KPIs.Select(k => k.LevelThreeStrategicObjectiveName);

        var initiativeObjectives = entity.Initiatives.Select(i => i.LevelThreeStrategicObjectiveName);

        return kpiObjectives.Union(initiativeObjectives);
    }
}