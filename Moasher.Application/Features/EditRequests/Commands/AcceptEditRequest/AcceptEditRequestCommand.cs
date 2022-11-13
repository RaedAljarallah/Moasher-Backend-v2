using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;
using Newtonsoft.Json;

namespace Moasher.Application.Features.EditRequests.Commands.AcceptEditRequest;

public record AcceptEditRequestCommand : IRequest<EditRequestDto>
{
    public Guid Id { get; set; }
}

public class AcceptEditRequestCommandHandler : IRequestHandler<AcceptEditRequestCommand, EditRequestDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IPublisher _publisher;

    public AcceptEditRequestCommandHandler(IMoasherDbContext context, IMapper mapper, ICurrentUser currentUser, IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
        _publisher = publisher;
    }
    
    public async Task<EditRequestDto> Handle(AcceptEditRequestCommand request, CancellationToken cancellationToken)
    {
        var editRequest = await _context.EditRequests
            .Where(e => e.Status == EditRequestStatus.Pending)
            .Include(e => e.Snapshots)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (editRequest is null)
        {
            throw new NotFoundException();
        }

        var entries = editRequest.Snapshots.Select(s => new {Id = s.ModelId, Table = s.TableName}).ToList();
        
        if (editRequest.Type == EditRequestType.Create)
        {
            foreach (var entry in entries)
            {
                var set = _context.GetSet<ApprovableDbEntity>(entry.Table);
                if (set is not null)
                {
                    var result = await set.FirstOrDefaultAsync(s => s.Id == entry.Id, cancellationToken);
                    if (result != null)
                    {
                        result.Approved = true;
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }
            }
        }

        if (editRequest.Type == EditRequestType.Delete)
        {
            foreach (var entry in entries)
            {
                var set = _context.GetSet<ApprovableDbEntity>(entry.Table);
                if (set is not null)
                {
                    var result = await set.FirstOrDefaultAsync(s => s.Id == entry.Id, cancellationToken);
                    if (result != null)
                    {
                        _context.RemoveEntity(result);
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }
            }
        }
        
        
        if (editRequest.HasEvents && editRequest.Events is { Length: > 0 })
        {
            var editRequestEvents =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(editRequest.Events);

            if (editRequestEvents is not null)
            {
                foreach (var editRequestEvent in editRequestEvents)
                {
                    var eventKeys = editRequestEvent.Key.Split(".");
                    var eventName = eventKeys.First();
                    var eventType = AttributeServices.GetType<EditRequest>(eventName);
                    if (eventType is null) continue;
                    var eventArgumentName = eventKeys.Last();
                    var eventArgumentType = AttributeServices.GetType<EditRequest>(eventArgumentName);
                    if (eventArgumentType is null) continue;
                    var eventArgumentValues = JsonConvert.DeserializeObject(editRequestEvent.Value.ToString() ?? string.Empty, eventArgumentType);
                    if (eventArgumentValues is null) continue;
                    var domainEvent = AttributeServices.CreateInstance<DomainEvent>(eventType, eventArgumentValues);
                    if (domainEvent is null) continue;
                    await _publisher.Publish(domainEvent, cancellationToken);
                }
            }
        }

        editRequest.Status = EditRequestStatus.Approved;
        editRequest.ActionAt = LocalDateTime.Now;
        editRequest.ActionBy = _currentUser.GetEmail();
        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
        return _mapper.Map<EditRequestDto>(editRequest);
    }
}