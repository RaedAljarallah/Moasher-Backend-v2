using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiatives;

public class GetInitiativesQueryParameter : IQueryParameterBuilder<Initiative>
{
    private readonly GetInitiativesQuery _parameter;

    public GetInitiativesQueryParameter(GetInitiativesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<Initiative> Build(IQueryable<Initiative> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery,
                "Name", "UnifiedCode", "CodeByProgram", "EntityName", "PortfolioName", "ProgramName", "StatusName",
                "FundStatusName", "LevelOneStrategicObjectiveName", "LevelTwoStrategicObjectiveName",
                "LevelThreeStrategicObjectiveName", "LevelFourStrategicObjectiveName",
                "Scope", "TargetSegment", "ContributionOnStrategicObjective");
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(i => i.Id == _parameter.Id);
        }
        
        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(i => i.EntityId == _parameter.EntityId);
        }
        
        if (_parameter.PortfolioId.HasValue)
        {
            query = query.Where(i => i.PortfolioId == _parameter.PortfolioId);
        }

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(i => i.ProgramId == _parameter.ProgramId);
        }

        if (_parameter.L1Id.HasValue)
        {
            query = query.Where(i => i.LevelOneStrategicObjectiveId == _parameter.L1Id);
        }

        if (_parameter.L2Id.HasValue)
        {
            query = query.Where(i => i.LevelTwoStrategicObjectiveId == _parameter.L2Id);
        }

        if (_parameter.L3Id.HasValue)
        {
            query = query.Where(i => i.LevelThreeStrategicObjectiveId == _parameter.L3Id);
        }

        if (_parameter.L4Id.HasValue)
        {
            query = query.Where(i => i.LevelFourStrategicObjectiveId == _parameter.L4Id);
        }
        
        if (_parameter.StatusId.HasValue)
        {
            query = query.Where(i => i.StatusEnumId == _parameter.StatusId);
        }
        
        if (_parameter.FundStatusId.HasValue)
        {
            query = query.Where(i => i.FundStatusEnumId == _parameter.FundStatusId);
        }
        
        if (_parameter.IssueStatusId.HasValue)
        {
            query = query.Where(i => i.Issues.Any(isu => isu.StatusEnumId == _parameter.IssueStatusId));
        }
        
        if (_parameter.RiskImpactId.HasValue)
        {
            query = query.Where(i => i.Risks.Any(r => r.ImpactEnumId == _parameter.RiskImpactId));
        }
        
        return query;
    }
}