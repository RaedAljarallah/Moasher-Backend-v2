using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;

public class GetInitiativesStatusProgressQueryParameter : IQueryParameterBuilder<Initiative>
{
    private readonly GetInitiativesStatusProgressQuery _parameter;

    public GetInitiativesStatusProgressQueryParameter(GetInitiativesStatusProgressQuery parameter)
    {
        _parameter = parameter;
    }
    public IQueryable<Initiative> Build(IQueryable<Initiative> query)
    {
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

        return query;
    }
}