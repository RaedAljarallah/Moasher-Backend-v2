using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.JsonContracts;
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
    private readonly IUserNotification _userNotification;
    private readonly IIdentityService _identityService;

    public AcceptEditRequestCommandHandler(IMoasherDbContext context, IMapper mapper, ICurrentUser currentUser,
        IPublisher publisher, IUserNotification userNotification, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
        _publisher = publisher;
        _userNotification = userNotification;
        _identityService = identityService;
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

        foreach (var entry in editRequest.Snapshots)
        {
            var set = _context.GetSet<ApprovableDbEntity>(entry.TableName);
            if (set is not null)
            {
                if (entry.Type == EditRequestType.Create)
                {
                    var result = await set.FirstOrDefaultAsync(s => s.Id == entry.ModelId, cancellationToken);
                    if (result != null)
                    {
                        result.Approved = true;
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }

                if (entry.Type == EditRequestType.Delete)
                {
                    var result = await set.FirstOrDefaultAsync(s => s.Id == entry.ModelId, cancellationToken);
                    if (result != null)
                    {
                        _context.RemoveEntity(result);
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }

                if (entry.Type == EditRequestType.Update)
                {
                    var result = await set
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == entry.ModelId, cancellationToken);
                    if (result != null)
                    {
                        var currentState =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(
                                JsonConvert.SerializeObject(result));
                        if (currentState is null) continue;

                        var newState =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(entry.OriginalValues ??
                                string.Empty);
                        if (newState is null) continue;

                        foreach (var key in newState.Keys.Where(key => key != nameof(ApprovableDbEntity.Id)))
                        {
                            currentState[key] = newState[key];
                        }

                        currentState[nameof(ApprovableDbEntity.Approved)] = true;
                        currentState[nameof(ApprovableDbEntity.HasUpdateRequest)] = false;

                        var entryType = AttributeServices.GetType<EditRequest>(entry.ModelName);
                        if (entryType is null) continue;

                        var updatedEntry =
                            JsonConvert.DeserializeObject(JsonConvert.SerializeObject(currentState), entryType,
                                new JsonSerializerSettings
                                {
                                    ContractResolver = new PrivateSetterContractResolver()
                                });
                        if (updatedEntry is null) continue;

                        _context.UpdateEntity(updatedEntry);
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }
            }
        }

        if (editRequest.HasEvents && editRequest.Events is {Length: > 0})
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
                    var eventArgumentValues =
                        JsonConvert.DeserializeObject(editRequestEvent.Value.ToString() ?? string.Empty,
                            eventArgumentType);
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

        var dto = _mapper.Map<EditRequestDto>(editRequest);
        
        await SendNotification(dto, cancellationToken);

        return dto;
    }

    private async Task SendNotification(EditRequestDto editRequest, CancellationToken cancellationToken)
    {
        var recipient = await _identityService.GetUserByEmail(editRequest.RequestedBy, cancellationToken);
        if (recipient is not null)
        {
            var body =
                $"تم قبول الطلب رقم {editRequest.Code} لتعديل {string.Join(" - ", editRequest.Scopes)} بتاريخ {LocalDateTime.Now:yyyy-MM-dd}";
            var userNotification = await _userNotification.CreateAsync("قبول تعديل", body, recipient, cancellationToken);
            await _userNotification.NotifyAsync(userNotification, recipient, cancellationToken);
        }
    }
}