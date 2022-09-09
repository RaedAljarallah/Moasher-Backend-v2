
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
        if (!string.IsNullOrWhiteSpace(_parameter.Q))
        {
            query = query.Like(_parameter.Q,
                "Name", "UnifiedCode", "CodeByProgram", "EntityName", "PortfolioName", "ProgramName", "Status_Name",
                "FundStatus_Name", "LevelOneStrategicObjectiveName", "LevelTwoStrategicObjectiveName",
                "LevelThreeStrategicObjectiveName", "LevelFourStrategicObjectiveName",
                "Scope", "TargetSegment", "ContributionOnStrategicObjective");
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Code))
        {
            query = query.Like(_parameter.Code, "UnifiedCode");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.PCode))
        {
            query = query.Like(_parameter.PCode, "CodeByProgram");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Entity))
        {
            query = query.Like(_parameter.Entity, "EntityName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Portfolio))
        {
            query = query.Like(_parameter.Portfolio, "PortfolioName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Program))
        {
            query = query.Like(_parameter.Program, "ProgramName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.St))
        {
            query = query.Like(_parameter.St, "Status_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Fst))
        {
            query = query.Like(_parameter.Fst, "FundStatus_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L1))
        {
            query = query.Like(_parameter.L1, "LevelOneStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L2))
        {
            query = query.Like(_parameter.L2, "LevelTwoStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L3))
        {
            query = query.Like(_parameter.L3, "LevelThreeStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L4))
        {
            query = query.Like(_parameter.L4, "LevelFourStrategicObjectiveName");
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
        
        if (_parameter.StId.HasValue)
        {
            query = query.Where(i => i.StatusEnumId == _parameter.StId);
        }
        
        if (_parameter.FstId.HasValue)
        {
            query = query.Where(i => i.FundStatusEnumId == _parameter.FstId);
        }
        
        // if (_parameter.IssueId.HasValue)
        // {
        //     query = query.Where(i => i.Issues.Any(i => i.StatusEnumId == _parameter.IssueId));
        // }
        //
        // if (_parameter.RiskId.HasValue)
        // {
        //     query = query.Where(i => i.Risks.Any(r => r.ImpactEnumId == _parameter.RiskId));
        // }
        
        return query;
    }
}