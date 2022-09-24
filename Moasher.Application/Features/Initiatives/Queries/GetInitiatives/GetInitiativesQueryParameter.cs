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
                "Name", "UnifiedCode", "CodeByProgram", "EntityName", "PortfolioName", "ProgramName", "Status_Name",
                "FundStatus_Name", "LevelOneStrategicObjectiveName", "LevelTwoStrategicObjectiveName",
                "LevelThreeStrategicObjectiveName", "LevelFourStrategicObjectiveName",
                "Scope", "TargetSegment", "ContributionOnStrategicObjective");
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.UnifiedCode))
        {
            query = query.Like(_parameter.UnifiedCode, "UnifiedCode");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.CodeByProgram))
        {
            query = query.Like(_parameter.CodeByProgram, "CodeByProgram");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.EntityName))
        {
            query = query.Like(_parameter.EntityName, "EntityName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.PortfolioName))
        {
            query = query.Like(_parameter.PortfolioName, "PortfolioName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.ProgramName))
        {
            query = query.Like(_parameter.ProgramName, "ProgramName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Status))
        {
            query = query.Like(_parameter.Status, "Status_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.FundStatus))
        {
            query = query.Like(_parameter.FundStatus, "FundStatus_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L1Name))
        {
            query = query.Like(_parameter.L1Name, "LevelOneStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L2Name))
        {
            query = query.Like(_parameter.L2Name, "LevelTwoStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L3Name))
        {
            query = query.Like(_parameter.L3Name, "LevelThreeStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L4Name))
        {
            query = query.Like(_parameter.L4Name, "LevelFourStrategicObjectiveName");
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