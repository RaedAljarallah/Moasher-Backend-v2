using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Initiatives.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Initiatives.Commands.UpdateInitiative;

public record UpdateInitiativeCommand : InitiativeCommandBase, IRequest<InitiativeDto>
{
    public Guid Id { get; set; }
}

public class UpdateInitiativeCommandHandler : IRequestHandler<UpdateInitiativeCommand, InitiativeDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateInitiativeCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InitiativeDto> Handle(UpdateInitiativeCommand request, CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives.ToListAsync(cancellationToken);
        var initiative = initiatives.FirstOrDefault(i => i.Id == request.Id);
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new InitiativeDomainValidator(initiatives.Where(e => e.Id != request.Id).ToList(),
            request.Name, request.UnifiedCode, request.CodeByProgram));

        if (IsDifferentEnums(request, initiative))
        {
            var enumTypesIds = new List<Guid> {request.FundStatusEnumId};
            if (request.StatusEnumId.HasValue)
            {
                enumTypesIds.Add(request.StatusEnumId.Value);
            }

            var enumTypes = await _context.EnumTypes
                .Where(e => enumTypesIds.Contains(e.Id))
                .ToListAsync(cancellationToken);

            var fundStatusEnum = enumTypes.FirstOrDefault(e => e.Id == request.FundStatusEnumId);
            var statusEnum = request.StatusEnumId.HasValue
                ? enumTypes.FirstOrDefault(e => e.Id == request.StatusEnumId)
                : null;
            if (fundStatusEnum is null)
            {
                throw new ValidationException(nameof(request.FundStatusEnumId),
                    InitiativeDependenciesValidationMessages.WrongFundStatusEnumId);
            }

            if (request.StatusEnumId.HasValue && statusEnum is null)
            {
                throw new ValidationException(nameof(request.StatusEnumId),
                    InitiativeDependenciesValidationMessages.WrongStatusEnumId);
            }

            // calculate the status the statusEnum is null
            // if (statusEnum is null)
            // {
            //     backgroundJobService.Once(new InitiativeStatusSetterJob(context), initiative.Id, cancellationToken);
            // } 
            // else
            // {
            //     initiative.StatusEnum = statusEnum;
            // }

            initiative.FundStatusEnum = fundStatusEnum;
        }

        if (initiative.EntityId != request.EntityId)
        {
            var entity = await _context.Entities
                .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);
            if (entity is null)
            {
                throw new ValidationException(nameof(request.EntityId),
                    InitiativeDependenciesValidationMessages.WrongEntityId);
            }

            initiative.Entity = entity;
        }

        if (initiative.ProgramId != request.ProgramId)
        {
            var program = await _context.Programs
                .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken);
            if (program is null)
            {
                throw new ValidationException(nameof(request.ProgramId),
                    InitiativeDependenciesValidationMessages.WrongProgramId);
            }

            initiative.Program = program;
        }

        if (initiative.LevelThreeStrategicObjectiveId != request.LevelThreeStrategicObjectiveId ||
            initiative.LevelFourStrategicObjectiveId != request.LevelFourStrategicObjectiveId)
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
                    InitiativeDependenciesValidationMessages.WrongLevelThreeStrategicObjectiveId);
            }

            if (request.LevelFourStrategicObjectiveId.HasValue && l4StrategicObjective is null)
            {
                throw new ValidationException(nameof(request.LevelFourStrategicObjectiveId),
                    InitiativeDependenciesValidationMessages.WrongLevelFourStrategicObjectiveId);
            }

            if (initiative.LevelThreeStrategicObjectiveId != request.LevelThreeStrategicObjectiveId)
            {
                var l3StrategicObjectiveAncestors = await _context.StrategicObjectives
                    .Where(l => l3StrategicObjective.HierarchyId.IsDescendantOf(l.HierarchyId))
                    .ToListAsync(cancellationToken);
                var l1StrategicObjective =
                    l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 1);
                var l2StrategicObjective =
                    l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 2);

                initiative.LevelOneStrategicObjective = l1StrategicObjective!;
                initiative.LevelTwoStrategicObjective = l2StrategicObjective!;
            }

            initiative.LevelThreeStrategicObjective = l3StrategicObjective;
            initiative.LevelFourStrategicObjective = l4StrategicObjective;
        }
        
        if (initiative.PortfolioId != request.PortfolioId)
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == request.PortfolioId, cancellationToken);

            if (portfolio is null)
            {
                throw new ValidationException(nameof(request.PortfolioId),
                    InitiativeDependenciesValidationMessages.WrongPortfolioId);
            }
            initiative.Portfolio = portfolio;
        }
        
        _mapper.Map(request, initiative);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InitiativeDto>(initiative);
    }

    private static bool IsDifferentEnums(UpdateInitiativeCommand request, Initiative initiative)
    {
        return initiative.FundStatusEnumId != request.FundStatusEnumId
               || initiative.StatusEnumId != request.StatusEnumId;
    }
}