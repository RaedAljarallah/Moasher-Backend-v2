using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Projects.Queries.ExportProjects;

public record ExportProjectsQuery : IRequest<ExportedProjectsDto>;

public class ExportProjectsQueryHandler : IRequestHandler<ExportProjectsQuery, ExportedProjectsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportProjectsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedProjectsDto> Handle(ExportProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.InitiativeProjects.OrderBy(p => p.PlannedBiddingDate).ThenBy(p => p.Name)
            .Where(p => !p.Contracted)
            .AsNoTracking()
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedProjectsDto("Projects.csv", _csvFileBuilder.BuildProjectsFile(projects));
    }
}