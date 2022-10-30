using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.InitiativeTeams.Queries.EditInitiativeTeam;

public record EditInitiativeTeamQuery : IRequest<EditInitiativeTeamDto>
{
    public Guid Id { get; set; }
}

public class EditInitiativeTeamQueryHandler : IRequestHandler<EditInitiativeTeamQuery, EditInitiativeTeamDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditInitiativeTeamQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditInitiativeTeamDto> Handle(EditInitiativeTeamQuery request, CancellationToken cancellationToken)
    {
        var teamMember = await _context.InitiativeTeams
            .AsNoTracking()
            .Include(t => t.RoleEnum)
            .AsSplitQuery()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (teamMember is null)
        {
            throw new NotFoundException();
        }
        
        return _mapper.Map<EditInitiativeTeamDto>(teamMember);
    }
}