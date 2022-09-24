using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.InitiativeTeams.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.InitiativeTeams.Commands.CreateInitiativeTeam;

public record CreateInitiativeTeamCommand : InitiativeTeamCommandBase, IRequest<InitiativeTeamDto>;

public class CreateInitiativeTeamCommandHandler : IRequestHandler<CreateInitiativeTeamCommand, InitiativeTeamDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateInitiativeTeamCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<InitiativeTeamDto> Handle(CreateInitiativeTeamCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Teams)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new InitiativeTeamDomainValidator(initiative, request.Name, request.Email, request.Phone, request.RoleEnumId));
        
        var role = await _context.EnumTypes.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == request.RoleEnumId, cancellationToken);
        if (role is null)
        {
            throw new ValidationException(nameof(request.RoleEnumId), InitiativeTeamEnumsValidationMessages.WrongRoleEnumId);
        }
        
        var teamMember = _mapper.Map<InitiativeTeam>(request);
        teamMember.RoleEnum = role;
        teamMember.Initiative = initiative;
        initiative.Teams.Add(teamMember);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InitiativeTeamDto>(teamMember);
    }
}