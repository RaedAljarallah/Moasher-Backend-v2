using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Risks.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Risks.Commands.CreateRisk;

public record CreateRiskCommand : RiskCommandBase, IRequest<RiskDto>;

public class CreateRiskCommandHandler : IRequestHandler<CreateRiskCommand, RiskDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateRiskCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<RiskDto> Handle(CreateRiskCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Risks)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }
        request.ValidateAndThrow(new RiskDomainValidator(initiative, request.Description));
        
        var enumTypesIds = new[]
        {
            request.TypeEnumId,
            request.PriorityEnumId,
            request.ProbabilityEnumId,
            request.ImpactEnumId,
            request.ScopeEnumId
        };
        
        var enumTypes = await _context.EnumTypes
            .AsNoTracking()
            .Where(e => enumTypesIds.Contains(e.Id))
            .ToListAsync(cancellationToken);
        
        var typeEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[0]);
        var priorityEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[1]);
        var probabilityEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[2]);
        var impactEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[3]);
        var scopeEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[4]);
        if (typeEnum == null)
        {
            throw new ValidationException(nameof(request.TypeEnumId), RiskEnumsValidationMessages.WrongTypeEnumId);
        }
        if (priorityEnum == null)
        {
            throw new ValidationException(nameof(request.PriorityEnumId), RiskEnumsValidationMessages.WrongPriorityEnumId);
        }
        if (probabilityEnum == null)
        {
            throw new ValidationException(nameof(request.ProbabilityEnumId), RiskEnumsValidationMessages.WrongProbabilityEnumId);
        }
        if (impactEnum == null)
        {
            throw new ValidationException(nameof(request.ImpactEnumId), RiskEnumsValidationMessages.WrongImpactEnumId);
        }
        if (scopeEnum == null)
        {
            throw new ValidationException(nameof(request.ScopeEnumId), RiskEnumsValidationMessages.WrongScopeEnumId);
        }
        
        var risk = _mapper.Map<InitiativeRisk>(request);
        risk.TypeEnum = typeEnum;
        risk.PriorityEnum = priorityEnum;
        risk.ProbabilityEnum = probabilityEnum;
        risk.ImpactEnum = impactEnum;
        risk.ScopeEnum = scopeEnum;
        risk.Initiative = initiative;
        initiative.Risks.Add(risk);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<RiskDto>(risk);
    }
}