using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.InitiativeTeams.Commands.Common;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.InitiativeTeams.Commands.UpdateInitiativeTeam;

public record UpdateInitiativeTeamCommand : InitiativeTeamCommandBase, IRequest<InitiativeTeamDto>
{
    public Guid Id { get; set; }
}

public class UpdateInitiativeTeamCommandHandler : IRequestHandler<UpdateInitiativeTeamCommand, InitiativeTeamDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateInitiativeTeamCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<InitiativeTeamDto> Handle(UpdateInitiativeTeamCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Teams)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var teamMember = initiative.Teams.FirstOrDefault(m => m.Id == request.Id);
        if (teamMember is null)
        {
            throw new NotFoundException();
        }
        
        initiative.Teams = initiative.Teams.Where(t => t.Id != request.Id).ToList();
        request.ValidateAndThrow(new InitiativeTeamDomainValidator(initiative, request.Name, request.Email, request.Phone, request.RoleEnumId));

        if (teamMember.RoleEnumId != request.RoleEnumId)
        {
            var role = await _context.EnumTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == request.RoleEnumId, cancellationToken);
            if (role is null)
            {
                throw new ValidationException(nameof(request.RoleEnumId), InitiativeTeamEnumsValidationMessages.WrongRoleEnumId);
            }

            teamMember.RoleEnum = role;
        }
        
        _mapper.Map(request, teamMember);
        _context.InitiativeTeams.Update(teamMember);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<InitiativeTeamDto>(teamMember);
    }
}