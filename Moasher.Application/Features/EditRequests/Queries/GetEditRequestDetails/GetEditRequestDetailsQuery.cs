using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;
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
        nameof(ApprovableDbEntity.Approved)
    };

    private IEnumerable<EnumType> _enumTypes = new List<EnumType>();

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

        _enumTypes = await _context.EnumTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var editScopes = editRequest.GetEditScopes();
        var originalValues = new List<EditRequestValue>();
        foreach (var scope in editScopes)
        {
            var editRequestValue = new EditRequestValue {ModelName = AttributeServices.GetDisplayName<EditRequest>(scope)};
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
        var result = new Dictionary<string, object>();
        foreach (var value in values.Where(value => !_ignoredFields.Contains(value.Key)))
        {
            if (value.Key.EndsWith("EnumId"))
            {
                try
                {
                    if (Guid.TryParse($"{value.Value}", out var enumId))
                    {
                        var enumTypeName = _enumTypes.FirstOrDefault(e => e.Id == enumId)?.Name;
                        result.Add(AttributeServices.GetDisplayName<EditRequest>(modelName, value.Key),
                            enumTypeName ?? string.Empty);
                    }
                    
                }
                catch (Exception)
                {
                    result.Add(AttributeServices.GetDisplayName<EditRequest>(modelName, value.Key), value.Value);
                }
                
            }

            if (value.Key.EndsWith("Id"))
            {
                continue;
            }

            result.Add(AttributeServices.GetDisplayName<EditRequest>(modelName, value.Key),
                GetFormattedValue(value.Value));
        }

        return result;
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