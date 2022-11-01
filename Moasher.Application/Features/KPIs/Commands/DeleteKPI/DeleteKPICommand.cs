using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.Commands.DeleteKPI;

public record DeleteKPICommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteKPICommandHandler : IRequestHandler<DeleteKPICommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteKPICommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteKPICommand request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs
            .Include(k => k.Analytics)
            .FirstOrDefaultAsync(k => k.Id == request.Id, cancellationToken);
        
        if (kpi is null)
        {
            throw new NotFoundException();
        }
        
        kpi.AddDomainEvent(new KPIDeletedEvent(kpi));
        _context.Analytics.RemoveRange(kpi.Analytics);
        _context.KPIs.Remove(kpi);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}