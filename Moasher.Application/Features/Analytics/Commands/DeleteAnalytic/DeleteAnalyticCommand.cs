using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;

namespace Moasher.Application.Features.Analytics.Commands.DeleteAnalytic;

public record DeleteAnalyticCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteAnalyticCommandHandler : IRequestHandler<DeleteAnalyticCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteAnalyticCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteAnalyticCommand request, CancellationToken cancellationToken)
    {
        var analytic = await _context.Analytics
            .Include(a => a.Initiative!)
            .ThenInclude(i => i.Analytics)
            .Include(a => a.KPI!)
            .ThenInclude(k => k.Analytics)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        
        if (analytic is null)
        {
            throw new NotFoundException();
        }
        
        var initiative = analytic.Initiative;
        var kpi = analytic.KPI;
        
        if (initiative is not null)
        {
            initiative.Analytics.Remove(analytic);
            initiative.SetLatestAnalytics();
        }
        
        if (kpi is not null)
        {
            kpi.Analytics.Remove(analytic);
            kpi.SetLatestAnalytics();
        }
        
        _context.Analytics.Remove(analytic);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}