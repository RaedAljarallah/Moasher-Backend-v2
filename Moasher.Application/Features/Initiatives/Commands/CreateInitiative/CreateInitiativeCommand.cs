using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Initiatives.Commands.Common;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Initiatives.Commands.CreateInitiative;

public record CreateInitiativeCommand : InitiativeCommandBase, IRequest<InitiativeDto>
{
}

public class CreateInitiativeCommandHandler : IRequestHandler<CreateInitiativeCommand, InitiativeDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateInitiativeCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<InitiativeDto> Handle(CreateInitiativeCommand request, CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives.AsNoTracking().ToListAsync(cancellationToken);
        request.ValidateAndThrow(new InitiativeDomainValidator(initiatives, request.Name, request.UnifiedCode, request.CodeByProgram));

        #region EnumTypes
        var enumTypesIds = new List<Guid> { request.FundStatusEnumId };
        if (request.StatusEnumId.HasValue)
        {
            enumTypesIds.Add(request.StatusEnumId.Value);
        }
        var enumTypes = await _context.EnumTypes
            .Where(e => enumTypesIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        var fundStatusEnum = enumTypes.FirstOrDefault(e => e.Id == request.FundStatusEnumId);
        var statusEnum = request.StatusEnumId.HasValue
            ? enumTypes.FirstOrDefault(e => e.Id == request.StatusEnumId) : null;
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
        #endregion
        
        #region Entity
        var entity = await _context.Entities
            .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);
        if (entity is null)
        {
            throw new ValidationException(nameof(request.EntityId),
                InitiativeDependenciesValidationMessages.WrongEntityId);
        }
        #endregion
        
        #region Program
        var program = await _context.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken);
        if (program is null)
        {
            throw new ValidationException(nameof(request.ProgramId),
                InitiativeDependenciesValidationMessages.WrongProgramId);
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
                InitiativeDependenciesValidationMessages.WrongLevelThreeStrategicObjectiveId);
        }
        if (request.LevelFourStrategicObjectiveId.HasValue && l4StrategicObjective is null)
        {
            throw new ValidationException(nameof(request.LevelFourStrategicObjectiveId),
                InitiativeDependenciesValidationMessages.WrongLevelFourStrategicObjectiveId);
        }

        var l3StrategicObjectiveAncestors = await _context.StrategicObjectives
            .Where(l => l3StrategicObjective.HierarchyId.IsDescendantOf(l.HierarchyId))
            .ToListAsync(cancellationToken);
        var l1StrategicObjective = l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 1);
        var l2StrategicObjective = l3StrategicObjectiveAncestors.FirstOrDefault(o => o.HierarchyId.GetLevel() == 2);
        #endregion
        
        #region Portfolio
        Portfolio? portfolio = null;
        if (request.PortfolioId.HasValue)
        {
            portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == request.PortfolioId, cancellationToken);

            if (portfolio is null)
            {
                throw new ValidationException(nameof(request.PortfolioId),
                    InitiativeDependenciesValidationMessages.WrongPortfolioId);
            }
        }
        #endregion
        
        var initiative = _mapper.Map<Initiative>(request);
        initiative.StatusEnum = statusEnum;
        initiative.FundStatusEnum = fundStatusEnum;
        initiative.Entity = entity;
        initiative.Program = program;
        initiative.Portfolio = portfolio;
        initiative.LevelOneStrategicObjective = l1StrategicObjective!;
        initiative.LevelTwoStrategicObjective = l2StrategicObjective!;
        initiative.LevelThreeStrategicObjective = l3StrategicObjective;
        initiative.LevelFourStrategicObjective = l4StrategicObjective;
        
        _context.Initiatives.Add(initiative);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InitiativeDto>(initiative);
    }
}