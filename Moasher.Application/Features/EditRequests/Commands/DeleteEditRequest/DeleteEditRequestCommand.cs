using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EditRequests.Commands.DeleteEditRequest;

public record DeleteEditRequestCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public class DeleteEditRequestCommandHandler : IRequestHandler<DeleteEditRequestCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteEditRequestCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteEditRequestCommand request, CancellationToken cancellationToken)
    {
        var editRequest = await _context.EditRequests
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        
        if (editRequest is null)
        {
            throw new NotFoundException();
        }

        if (editRequest.Status == EditRequestStatus.Pending)
        {
            throw new ValidationException("لا يمكن حذف طلب تعديل تحت الإجراء");
        }

        _context.EditRequests.Remove(editRequest);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}