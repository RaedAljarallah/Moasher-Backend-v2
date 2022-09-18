using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.Commands.DeleteStrategicObjective;

public record DeleteStrategicObjectiveCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public class DeleteStrategicObjectiveCommandHandler : IRequestHandler<DeleteStrategicObjectiveCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteStrategicObjectiveCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteStrategicObjectiveCommand request, CancellationToken cancellationToken)
    {
        var strategicObjective = await _context.StrategicObjectives.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
        if (strategicObjective is null)
        {
            throw new NotFoundException();
        }
        
        if (strategicObjective.HierarchyId.GetLevel() < 4)
        {
            var strategicObjectives = await _context.StrategicObjectives.Where(o => o.HierarchyId.IsDescendantOf(strategicObjective.HierarchyId))
                .ToListAsync(cancellationToken);
            _context.StrategicObjectives.RemoveRange(strategicObjectives);
        }
        else
        {
            _context.StrategicObjectives.Remove(strategicObjective);
        }

        if (strategicObjective.HierarchyId.GetLevel() == 4)
        {
            strategicObjective.AddDomainEvent(new LevelFourStrategicObjectiveDeletedEvent(strategicObjective));
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}