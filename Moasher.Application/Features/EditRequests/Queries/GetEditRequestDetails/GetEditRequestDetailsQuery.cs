using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.EditRequests;
using Newtonsoft.Json;

namespace Moasher.Application.Features.EditRequests.Queries.GetEditRequestDetails;

public record GetEditRequestDetailsQuery : IRequest<EditRequestDetailsDto>
{
    public Guid Id { get; set; }
}

public class GetEditRequestDetailsQueryHandler : IRequestHandler<GetEditRequestDetailsQuery, EditRequestDetailsDto>
{
    private readonly IMoasherDbContext _context;

    private readonly IEnumerable<string> _ignoredFields = new List<string>
    {
        nameof(AuditableDbEntity.Id),
        nameof(AuditableDbEntity.CreatedAt),
        nameof(AuditableDbEntity.CreatedBy),
        nameof(AuditableDbEntity.LastModified),
        nameof(AuditableDbEntity.LastModifiedBy),
        nameof(ApprovableDbEntity.Approved),
        nameof(ApprovableDbEntity.HasDeleteRequest),
        nameof(ApprovableDbEntity.HasUpdateRequest)
    };

    public GetEditRequestDetailsQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<EditRequestDetailsDto> Handle(GetEditRequestDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var editRequest = await _context.EditRequests
            .AsNoTracking()
            .Include(e => e.Snapshots)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (editRequest is null)
        {
            throw new NotFoundException();
        }
        
        var editScopes = editRequest.GetEditScopes();
        var originalValues = new List<EditRequestValue>();
        foreach (var scope in editScopes)
        {
            var editRequestValue = new EditRequestValue
                {ModelName = AttributeServices.GetDisplayName<EditRequest>(scope)};
            var editSnapshots = editRequest.Snapshots
                .Where(s => s.ModelName == scope)
                .Where(s => s.OriginalValues is not null)
                .Select(s => s.OriginalValues)
                .ToList();

            foreach (var snapshot in editSnapshots)
            {
                var value = JsonConvert.DeserializeObject<Dictionary<string, object>>(snapshot!);
                if (value is not null)
                {
                    editRequestValue.Values.Add(ParseKeysToDisplayName(value, scope));
                }
            }

            originalValues.Add(editRequestValue);
        }

        return new EditRequestDetailsDto
        {
            OriginalValues = originalValues,
            EditRequestId = editRequest.Id
        };
    }

    private Dictionary<string, object> ParseKeysToDisplayName(Dictionary<string, object> values,
        string modelName)
    {
        return values
            .Where(value => !_ignoredFields.Contains(value.Key))
            .Where(value => !value.Key.EndsWith("Id"))
            .Where(value => !value.Key.EndsWith("Style"))
            .ToDictionary(value => AttributeServices.GetDisplayName<EditRequest>(modelName, value.Key),
                value => GetFormattedValue(value.Value));
    }

    private static object GetFormattedValue(object value)
    {
        try
        {
            if (DateTimeOffset.TryParse($"{value}", out var date)) return $"{date:yyyy-MM-dd}";
            if (bool.TryParse($"{value}", out var result)) return result ? "نعم" : "لا";
            return value;
        }
        catch (Exception)
        {
            return value;
        }
    }
}