using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Initiatives.Queries.EditInitiative;

public record EditInitiativeQuery : IRequest<EditInitiativeDto>
{
    public Guid Id { get; set; }
}

public class EditInitiativeQueryHandler : IRequestHandler<EditInitiativeQuery, EditInitiativeDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditInitiativeQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditInitiativeDto> Handle(EditInitiativeQuery request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.FundStatusEnum)
            .Include(i => i.StatusEnum)
            .Include(i => i.Entity)
            .Include(i => i.Program)
            .Include(i => i.Portfolio)
            .Include(i => i.LevelThreeStrategicObjective)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        if (initiative.LevelFourStrategicObjectiveId.HasValue)
        {
            var l4StrategicObjective = await _context.StrategicObjectives
                .FirstOrDefaultAsync(o => o.Id == initiative.LevelFourStrategicObjectiveId, cancellationToken);

            initiative.LevelFourStrategicObjective = l4StrategicObjective;
        }

        return _mapper.Map<EditInitiativeDto>(initiative);
    }
}