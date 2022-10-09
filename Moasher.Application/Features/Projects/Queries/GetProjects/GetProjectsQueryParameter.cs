using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Services;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsQueryParameter : IQueryParameterBuilder<InitiativeProject>
{
    private readonly GetProjectsQuery _parameter;

    public GetProjectsQueryParameter(GetProjectsQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<InitiativeProject> Build(IQueryable<InitiativeProject> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name");
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }

        if (_parameter.PlannedBiddingFrom.HasValue)
        {
            query = query.Where(p => p.PlannedBiddingDate >= _parameter.PlannedBiddingFrom.Value);
        }

        if (_parameter.PlannedBiddingTo.HasValue)
        {
            query = query.Where(p => p.PlannedBiddingDate <= _parameter.PlannedBiddingTo.Value);
        }

        if (_parameter.ActualBiddingFrom.HasValue)
        {
            query = query.Where(p => p.ActualBiddingDate >= _parameter.ActualBiddingFrom.Value);
        }

        if (_parameter.ActualBiddingTo.HasValue)
        {
            query = query.Where(p => p.ActualBiddingDate <= _parameter.ActualBiddingTo.Value);
        }

        if (_parameter.PlannedContractingFrom.HasValue)
        {
            query = query.Where(p => p.PlannedContractingDate >= _parameter.PlannedContractingFrom.Value);
        }

        if (_parameter.PlannedContractingTo.HasValue)
        {
            query = query.Where(p => p.PlannedContractingDate <= _parameter.PlannedContractingTo.Value);
        }

        if (_parameter.PhaseId.HasValue)
        {
            query = query.Where(p => p.PhaseEnumId == _parameter.PhaseId);
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Status))
        {
            if (string.Equals(_parameter.Status, "LateOnBidding", StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(p => (p.PlannedBiddingDate < DateTimeService.Now && !p.ActualBiddingDate.HasValue) 
                                         || (p.ActualBiddingDate.HasValue && p.ActualBiddingDate > p.PlannedBiddingDate &&
                                             p.PlannedContractingDate >= DateTimeService.Now));
            }
            
            if (string.Equals(_parameter.Status, "LateOnContracting", StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(p => p.PlannedBiddingDate < DateTimeService.Now);
            }
            

            if (string.Equals(_parameter.Status, "Ontrack", StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(p => p.PlannedBiddingDate >= DateTimeService.Now
                || (p.ActualBiddingDate.HasValue && p.ActualBiddingDate <= p.PlannedBiddingDate)
                || (p.ActualBiddingDate.HasValue && p.PlannedContractingDate >= DateTimeService.Now));
            }
        }

        return query;
    }
}