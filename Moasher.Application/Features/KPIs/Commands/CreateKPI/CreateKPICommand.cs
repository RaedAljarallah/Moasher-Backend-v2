using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.KPIs.Commands.Common;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.KPIs.Commands.CreateKPI;

public record CreateKPICommand : KPICommandBase, IRequest<KPIDto>;

public class CreateKPICommandHandler : IRequestHandler<CreateKPICommand, KPIDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateKPICommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<KPIDto> Handle(CreateKPICommand request, CancellationToken cancellationToken)
    {
        var kpis = await _context.KPIs.AsNoTracking().ToListAsync(cancellationToken);
        request.ValidateAndThrow(new KPIDomainValidator(kpis, request.Name, request.Code));
        
        #region EnumTypes
        var statusEnum = await _context.EnumTypes.FirstOrDefaultAsync(e => e.Id == request.StatusEnumId, cancellationToken);
        if (request.StatusEnumId.HasValue && statusEnum is null)
        {
            throw new ValidationException(nameof(request.StatusEnumId),
                KPIDependenciesValidationMessages.WrongStatusEnumId);
        }
        #endregion
        
        #region Entity
        var entity = await _context.Entities
            .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);
        if (entity is null)
        {
            throw new ValidationException(nameof(request.EntityId),
                KPIDependenciesValidationMessages.WrongEntityId);
        }
        #endregion
        
        #region StrategicObjectives
        var strategicObjectivesIds = new List<Guid> { request.LevelThreeStrategicObjectiveId };
        if (request.LevelFourStrategicObjectiveId.HasValue)
        {
            strategicObjectivesIds.Add(request.LevelFourStrategicObjectiveId.Value);
        }
        var strategicObjectives = await _context.StrategicObjectives
            .Where(o => strategicObjectivesIds.Contains(o.Id))
            .ToListAsync(cancellationToken);
        var l3StrategicObjective = strategicObjectives.FirstOrDefault(o => o.Id == request.LevelThreeStrategicObjectiveId);
        var l4StrategicObjective = request.LevelFourStrategicObjectiveId.HasValue
            ? strategicObjectives.FirstOrDefault(o => o.Id == request.LevelFourStrategicObjectiveId) : null;
        if (l3StrategicObjective is null)
        {
            throw new ValidationException(nameof(request.LevelThreeStrategicObjectiveId),
                KPIDependenciesValidationMessages.WrongLevelThreeStrategicObjectiveId);
        }
        if (request.LevelFourStrategicObjectiveId.HasValue && l4StrategicObjective is null)
        {
            throw new ValidationException(nameof(request.LevelFourStrategicObjectiveId),
                KPIDependenciesValidationMessages.WrongLevelFourStrategicObjectiveId);
        }

        var l3StrategicObjectiveAncestors = await _context.StrategicObjectives
            .Where(l => l3StrategicObjective.HierarchyId.IsDescendantOf(l.HierarchyId))
            .ToListAsync(cancellationToken);
        var l1StrategicObjective = l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 1);
        var l2StrategicObjective = l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 2);
        #endregion
        
        var kpi = _mapper.Map<KPI>(request);
        kpi.StatusEnum = statusEnum;
        kpi.Entity = entity;
        kpi.LevelOneStrategicObjective = l1StrategicObjective!;
        kpi.LevelTwoStrategicObjective = l2StrategicObjective!;
        kpi.LevelThreeStrategicObjective = l3StrategicObjective;
        kpi.LevelFourStrategicObjective = l4StrategicObjective;
        
        _context.KPIs.Add(kpi);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<KPIDto>(kpi);
    }
}