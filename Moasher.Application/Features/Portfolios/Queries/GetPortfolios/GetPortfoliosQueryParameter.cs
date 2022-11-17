using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities;
using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.Portfolios.Queries.GetPortfolios;

public class GetPortfoliosQueryParameter : IQueryParameterBuilder<Portfolio>
{
    private readonly GetPortfoliosQuery _parameter;

    public GetPortfoliosQueryParameter(GetPortfoliosQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<Portfolio> Build(IQueryable<Portfolio> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name", "Code");
        }

        return query;
    }
}