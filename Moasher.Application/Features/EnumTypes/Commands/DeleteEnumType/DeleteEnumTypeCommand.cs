using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.EnumTypes.Commands.DeleteEnumType;

public record DeleteEnumTypeCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public class DeleteEnumTypeCommandHandler : IRequestHandler<DeleteEnumTypeCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteEnumTypeCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteEnumTypeCommand request, CancellationToken cancellationToken)
    {
        var enumType = await _context.EnumTypes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
         if (enumType is null)
        {
            throw new NotFoundException();
        }

        if (enumType.CanBeDeleted is false)
        {
            throw new ValidationException("لا يمكن حذف هذا المدخل");
        }
        
        _context.EnumTypes.Remove(enumType);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new ValidationException("لا يمكن حذف العنصر لوجود عناصر آخرى مرتبطة به");
        }
        
        return Unit.Value;
    }
}