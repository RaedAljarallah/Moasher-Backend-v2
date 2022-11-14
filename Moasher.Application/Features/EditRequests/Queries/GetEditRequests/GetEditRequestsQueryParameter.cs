using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EditRequests.Queries.GetEditRequests;

public class GetEditRequestsQueryParameter : IQueryParameterBuilder<EditRequest>
{
    private readonly GetEditRequestsQuery _parameter;

    public GetEditRequestsQueryParameter(GetEditRequestsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<EditRequest> Build(IQueryable<EditRequest> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Where(e => e.Code == _parameter.SearchQuery);
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Status))
        {
            if (string.Equals(_parameter.Status, EditRequestStatus.Approved.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Status == EditRequestStatus.Approved);
            }
            
            if (string.Equals(_parameter.Status, EditRequestStatus.Pending.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Status == EditRequestStatus.Pending);
            }
            
            if (string.Equals(_parameter.Status, EditRequestStatus.Rejected.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Status == EditRequestStatus.Rejected);
            }
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Type))
        {
            if (string.Equals(_parameter.Type, EditRequestType.Create.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Snapshots.Select(s => s.Type).Contains(EditRequestType.Create));
            }
            
            if (string.Equals(_parameter.Type, EditRequestType.Update.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Snapshots.Select(s => s.Type).Contains(EditRequestType.Update));
            }
            
            if (string.Equals(_parameter.Type, EditRequestType.Delete.ToString(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Where(e => e.Snapshots.Select(s => s.Type).Contains(EditRequestType.Delete));
            }
        }

        return query;
    }
}