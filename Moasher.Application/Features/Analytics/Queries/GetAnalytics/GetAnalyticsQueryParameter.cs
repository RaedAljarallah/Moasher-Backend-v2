using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Analytics.Queries.GetAnalytics;

public class GetAnalyticsQueryParameter : IQueryParameterBuilder<Analytic>
{
    private readonly GetAnalyticsQuery _parameter;

    public GetAnalyticsQueryParameter(GetAnalyticsQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<Analytic> Build(IQueryable<Analytic> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Description", "AnalyzedBy", "InitiativeName", "KPIName");
        }

        if (_parameter.Id.HasValue)
        {
            query = query.Where(a => a.Id == _parameter.Id);
        }

        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(a => a.InitiativeId == _parameter.InitiativeId);
        }

        if (_parameter.KPIId.HasValue)
        {
            query = query.Where(a => a.KPIId == _parameter.KPIId);
        }

        if (_parameter.AnalyzedFrom.HasValue)
        {
            query = query.Where(a => a.AnalyzedAt >= _parameter.AnalyzedFrom);
        }

        if (_parameter.AnalyzedTo.HasValue)
        {
            query = query.Where(a => a.AnalyzedAt <= _parameter.AnalyzedTo);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = _parameter.Model?.ToLower() switch
            {
                "initiatives" => query.Where(a => a.Initiative != null && a.Initiative.EntityId == _parameter.EntityId),
                "kpis" => query.Where(a => a.KPI != null && a.KPI.EntityId == _parameter.EntityId),
                _ => query.Where(a =>
                    a.KPI != null && a.Initiative != null && (a.Initiative.EntityId == _parameter.EntityId ||
                                                              a.KPI.EntityId == _parameter.EntityId))
            };
        }

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(a => a.Initiative != null && a.Initiative.ProgramId == _parameter.ProgramId);
        }

        if (_parameter.PortfolioId.HasValue)
        {
            query = query.Where(a => a.Initiative != null && a.Initiative.PortfolioId == _parameter.PortfolioId);
        }

        if (_parameter.L1Id.HasValue)
        {
            query = _parameter.Model?.ToLower() switch
            {
                "initiatives" => query.Where(a =>
                    a.Initiative != null && a.Initiative.LevelOneStrategicObjectiveId == _parameter.L1Id),
                "kpis" => query.Where(a => a.KPI != null && a.KPI.LevelOneStrategicObjectiveId == _parameter.L1Id),
                _ => query.Where(a =>
                    a.KPI != null && a.Initiative != null &&
                    (a.Initiative.LevelOneStrategicObjectiveId == _parameter.L1Id ||
                     a.KPI.LevelOneStrategicObjectiveId == _parameter.L1Id))
            };
        }

        if (_parameter.L2Id.HasValue)
        {
            query = _parameter.Model?.ToLower() switch
            {
                "initiatives" => query.Where(a =>
                    a.Initiative != null && a.Initiative.LevelTwoStrategicObjectiveId == _parameter.L2Id),
                "kpis" => query.Where(a => a.KPI != null && a.KPI.LevelTwoStrategicObjectiveId == _parameter.L2Id),
                _ => query.Where(a =>
                    a.KPI != null && a.Initiative != null &&
                    (a.Initiative.LevelTwoStrategicObjectiveId == _parameter.L2Id ||
                     a.KPI.LevelTwoStrategicObjectiveId == _parameter.L2Id))
            };
        }

        if (_parameter.L3Id.HasValue)
        {
            query = _parameter.Model?.ToLower() switch
            {
                "initiatives" => query.Where(a =>
                    a.Initiative != null && a.Initiative.LevelThreeStrategicObjectiveId == _parameter.L3Id),
                "kpis" => query.Where(a => a.KPI != null && a.KPI.LevelThreeStrategicObjectiveId == _parameter.L3Id),
                _ => query.Where(a =>
                    a.KPI != null && a.Initiative != null &&
                    (a.Initiative.LevelThreeStrategicObjectiveId == _parameter.L3Id ||
                     a.KPI.LevelThreeStrategicObjectiveId == _parameter.L3Id))
            };
        }

        if (_parameter.L4Id.HasValue)
        {
            query = _parameter.Model?.ToLower() switch
            {
                "initiatives" => query.Where(a =>
                    a.Initiative != null && a.Initiative.LevelFourStrategicObjectiveId == _parameter.L4Id),
                "kpis" => query.Where(a => a.KPI != null && a.KPI.LevelFourStrategicObjectiveId == _parameter.L4Id),
                _ => query.Where(a =>
                    a.KPI != null && a.Initiative != null &&
                    (a.Initiative.LevelFourStrategicObjectiveId == _parameter.L4Id ||
                     a.KPI.LevelFourStrategicObjectiveId == _parameter.L4Id))
            };
        }

        return query;
    }
}