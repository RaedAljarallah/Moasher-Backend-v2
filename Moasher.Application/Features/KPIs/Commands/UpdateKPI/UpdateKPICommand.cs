using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.KPIs.Commands.Common;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Events.KPIs;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.KPIs.Commands.UpdateKPI;

public record UpdateKPICommand : KPICommandBase, IRequest<KPIDto>
{
    public Guid Id { get; set; }
}

public class UpdateKPICommandHandler : IRequestHandler<UpdateKPICommand, KPIDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateKPICommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<KPIDto> Handle(UpdateKPICommand request, CancellationToken cancellationToken)
    {
        var kpis = await _context.KPIs.ToListAsync(cancellationToken);
        var kpi = kpis.FirstOrDefault(k => k.Id == request.Id);
        if (kpi is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new KPIDomainValidator(kpis.Where(k => k.Id != request.Id).ToList(),
            request.Name, request.Code));
        
        #region EnumTypes
        if (IsDifferentEnums(request, kpi))
        {
            var statusEnum = request.StatusEnumId.HasValue
                ? await _context.EnumTypes.FirstOrDefaultAsync(e => e.Id == request.StatusEnumId, cancellationToken)
                : null;
            if (request.StatusEnumId.HasValue && statusEnum is null)
            {
                throw new ValidationException(nameof(request.StatusEnumId),
                    KPIDependenciesValidationMessages.WrongStatusEnumId);
            }

            // calculate the status the statusEnum is null
            if (statusEnum is null)
            {
                kpi.AddDomainEvent(new KPIStatusUpdateEvent(kpi));
            }
            else
            {
                kpi.StatusEnum = statusEnum;
            }
        }
        #endregion
        
        #region Entity
        if (request.EntityId != kpi.EntityId)
        {
            var entity = await _context.Entities
                .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);
            if (entity is null)
            {
                throw new ValidationException(nameof(request.EntityId),
                    KPIDependenciesValidationMessages.WrongEntityId);
            }

            kpi.Entity = entity;
        }
        #endregion
        
        #region StrategicObjectives
        if (kpi.LevelThreeStrategicObjectiveId != request.LevelThreeStrategicObjectiveId ||
            kpi.LevelFourStrategicObjectiveId != request.LevelFourStrategicObjectiveId)
        {
            var strategicObjectivesIds = new List<Guid> {request.LevelThreeStrategicObjectiveId};
            if (request.LevelFourStrategicObjectiveId.HasValue)
            {
                strategicObjectivesIds.Add(request.LevelFourStrategicObjectiveId.Value);
            }

            var strategicObjectives = await _context.StrategicObjectives
                .Where(o => strategicObjectivesIds.Contains(o.Id))
                .ToListAsync(cancellationToken);
            var l3StrategicObjective =
                strategicObjectives.FirstOrDefault(o => o.Id == request.LevelThreeStrategicObjectiveId);
            var l4StrategicObjective = request.LevelFourStrategicObjectiveId.HasValue
                ? strategicObjectives.FirstOrDefault(o => o.Id == request.LevelFourStrategicObjectiveId)
                : null;
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

            if (kpi.LevelThreeStrategicObjectiveId != request.LevelThreeStrategicObjectiveId)
            {
                var l3StrategicObjectiveAncestors = await _context.StrategicObjectives
                    .Where(l => l3StrategicObjective.HierarchyId.IsDescendantOf(l.HierarchyId))
                    .ToListAsync(cancellationToken);
                var l1StrategicObjective =
                    l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 1);
                var l2StrategicObjective =
                    l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 2);

                kpi.LevelOneStrategicObjective = l1StrategicObjective!;
                kpi.LevelTwoStrategicObjective = l2StrategicObjective!;
            }

            kpi.LevelThreeStrategicObjective = l3StrategicObjective;
            kpi.LevelFourStrategicObjective = l4StrategicObjective;
        }
        #endregion

        var hasEvent = request.Name != kpi.Name || request.Polarity != kpi.Polarity;
        
        _mapper.Map(request, kpi);
        if (hasEvent)
        {
            kpi.AddDomainEvent(new KPIUpdatedEvent(kpi));
        }
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<KPIDto>(kpi);
    }

    private static bool IsDifferentEnums(UpdateKPICommand request, KPI kpi)
    {
        return kpi.StatusEnumId != request.StatusEnumId;
    }
}