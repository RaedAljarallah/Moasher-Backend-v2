using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Projects.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand : ProjectCommandBase, IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Projects.Where(p => !p.Contracted))
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var phaseEnum = await _context.EnumTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.PhaseEnumId, cancellationToken);

        if (phaseEnum is null)
        {
            throw new ValidationException(nameof(request.PhaseEnumId), ProjectEnumsValidationMessages.WrongPhaseEnumId);
        }
        // Domain Validator Goes Here

        var project = _mapper.Map<InitiativeProject>(request);
        project.PhaseEnum = phaseEnum;
        project.Initiative = initiative;
        // Domain Events Goes Here
        initiative.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }
}