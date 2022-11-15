using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;

namespace Moasher.Application.Features.EditRequests.Commands.RejectEditRequest;

public record RejectEditRequestCommand : IRequest<EditRequestDto>
{
    private string? _justification;
    public Guid Id { get; set; }

    public string? Justification
    {
        get => _justification;
        set => _justification = value?.Trim();
    }
}

public class RejectEditRequestCommandHandler : IRequestHandler<RejectEditRequestCommand, EditRequestDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IUserNotification _userNotification;
    private readonly IIdentityService _identityService;

    public RejectEditRequestCommandHandler(IMoasherDbContext context, IMapper mapper, ICurrentUser currentUser,
        IUserNotification userNotification, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
        _userNotification = userNotification;
        _identityService = identityService;
    }

    public async Task<EditRequestDto> Handle(RejectEditRequestCommand request, CancellationToken cancellationToken)
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
                        _context.RemoveEntity(result);
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }

                if (entry.Type is EditRequestType.Delete or EditRequestType.Update)
                {
                    var result = await set.FirstOrDefaultAsync(s => s.Id == entry.ModelId, cancellationToken);
                    if (result != null)
                    {
                        result.Approved = true;
                        result.HasDeleteRequest = false;
                        result.HasUpdateRequest = false;
                        await _context.SaveChangesAsyncFromInternalProcess(cancellationToken);
                    }
                }
            }
        }

        editRequest.Status = EditRequestStatus.Approved;
        editRequest.Justification = request.Justification;
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
            var justification = string.Empty;
            if (editRequest.Justification is not null)
            {
                justification = $"بسبب {editRequest.Justification}";
            }

            var body =
                $"تم رفض الطلب رقم {editRequest.Code} لتعديل {string.Join(" - ", editRequest.Scopes)} بتاريخ {LocalDateTime.Now:yyyy-MM-dd} {justification}";
            var userNotification = await _userNotification.CreateAsync("رفض تعديل", body, recipient, cancellationToken);
            await _userNotification.NotifyAsync(userNotification, recipient, cancellationToken);
        }
    }
}