using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.KPIs.Queries.EditKPI;

public record EditKPIQuery : IRequest<EditKPIDto>
{
    public Guid Id { get; set; }
}

public class EditKPIQueryHandler : IRequestHandler<EditKPIQuery, EditKPIDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditKPIQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditKPIDto> Handle(EditKPIQuery request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs
            .AsNoTracking()
            .Include(k => k.StatusEnum)
            .Include(k => k.Entity)
            .Include(k => k.LevelThreeStrategicObjective)
            .FirstOrDefaultAsync(k => k.Id == request.Id, cancellationToken);
        
        if (kpi is null)
        {
            throw new NotFoundException();
        }
        
        if (kpi.LevelFourStrategicObjectiveId.HasValue)
        {
            var l4StrategicObjective = await _context.StrategicObjectives
                .FirstOrDefaultAsync(o => o.Id == kpi.LevelFourStrategicObjectiveId, cancellationToken);

            kpi.LevelFourStrategicObjective = l4StrategicObjective;
        }

        return _mapper.Map<EditKPIDto>(kpi);
    }
}