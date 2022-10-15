using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Contracts.Queries.GetContracts;

public class GetContractsQueryParameter : IQueryParameterBuilder<InitiativeContract>
{
    private readonly GetContractsQuery _parameter;

    public GetContractsQueryParameter(GetContractsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeContract> Build(IQueryable<InitiativeContract> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name", "RefNumber", "Supplier");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.RefNumber))
        {
            query = query.Like(_parameter.RefNumber, "RefNumber");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Supplier))
        {
            query = query.Like(_parameter.Supplier, "Supplier");
        }

        if (_parameter.StartFrom.HasValue)
        {
            query = query.Where(c => c.StartDate >= _parameter.StartFrom.Value);
        }

        if (_parameter.StartTo.HasValue)
        {
            query = query.Where(c => c.StartDate <= _parameter.StartTo.Value);
        }

        if (_parameter.EndFrom.HasValue)
        {
            query = query.Where(c => c.EndDate >= _parameter.EndFrom.Value);
        }

        if (_parameter.EndTo.HasValue)
        {
            query = query.Where(c => c.EndDate <= _parameter.EndTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(_parameter.ExpenditurePlanStatus))
        {
            query = _parameter.ExpenditurePlanStatus.ToLower() switch
            {
                "matching" => query.Where(c => c.Amount == c.Expenditures.Sum(e => e.PlannedAmount)),
                "notmatching" => query.Where(c => c.Amount != c.Expenditures.Sum(e => e.PlannedAmount)),
                _ => query
            };
        }
        
        if (_parameter.StatusId.HasValue)
        {
            query = query.Where(c => c.StatusEnumId == _parameter.StatusId);
        }
        
        return query;
    }
}