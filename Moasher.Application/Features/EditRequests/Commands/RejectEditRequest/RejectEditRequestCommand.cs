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

    public string? Justification { get => _justification; set => _justification = value?.Trim(); }
}

public class RejectEditRequestCommandHandler : IRequestHandler<RejectEditRequestCommand, EditRequestDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public RejectEditRequestCommandHandler(IMoasherDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
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
        return _mapper.Map<EditRequestDto>(editRequest);
    }
}