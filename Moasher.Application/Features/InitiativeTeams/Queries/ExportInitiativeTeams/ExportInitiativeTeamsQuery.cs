using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.InitiativeTeams.Queries.ExportInitiativeTeams;

public record ExportInitiativeTeamsQuery : IRequest<ExportedInitiativeTeamsDto>;

public class ExportInitiativeTeamsQueryHandler : IRequestHandler<ExportInitiativeTeamsQuery, ExportedInitiativeTeamsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportInitiativeTeamsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedInitiativeTeamsDto> Handle(ExportInitiativeTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _context.InitiativeTeams
            .AsNoTracking()
            .ProjectTo<InitiativeTeamDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedInitiativeTeamsDto("Initiatives_Team.csv", _csvFileBuilder.BuildTeamsFile(teams));
    }
}