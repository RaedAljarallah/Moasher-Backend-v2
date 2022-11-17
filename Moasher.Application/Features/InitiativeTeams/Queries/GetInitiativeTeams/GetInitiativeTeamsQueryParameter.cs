using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.InitiativeTeams.Queries.GetInitiativeTeams;

public class GetInitiativeTeamsQueryParameter : IQueryParameterBuilder<InitiativeTeam>
{
    private readonly GetInitiativeTeamsQuery _parameter;

    public GetInitiativeTeamsQueryParameter(GetInitiativeTeamsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeTeam> Build(IQueryable<InitiativeTeam> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name", "Email", "Phone", "RoleName");
        }

        if (_parameter.Id.HasValue)
        {
            query = query.Where(t => t.Id == _parameter.Id);
        }

        if (_parameter.RoleId.HasValue)
        {
            query = query.Where(t => t.RoleEnumId == _parameter.RoleId);
        }
        
        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(i => i.InitiativeId == _parameter.InitiativeId);
        }

        return query;
    }
}