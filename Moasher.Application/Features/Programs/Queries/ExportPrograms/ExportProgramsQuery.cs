using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Programs.Queries.ExportPrograms;

public record ExportProgramsQuery : IRequest<ExportedProgramsDto>;

public class ExportProgramsQueryHandler : IRequestHandler<ExportProgramsQuery, ExportedProgramsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportProgramsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedProgramsDto> Handle(ExportProgramsQuery request, CancellationToken cancellationToken)
    {
        var programs = await _context.Programs.OrderBy(p => p.Name).ThenBy(p => p.Code)
            .AsNoTracking()
            .Include(e => e.Initiatives)
            .ThenInclude(i => i.LevelThreeStrategicObjective)
            .ThenInclude(o => o.KPIs)
            .ProjectTo<ProgramDto>(_mapper.ConfigurationProvider)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        return new ExportedProgramsDto("Programs.csv", _csvFileBuilder.BuildProgramsFile(programs));
    }
}