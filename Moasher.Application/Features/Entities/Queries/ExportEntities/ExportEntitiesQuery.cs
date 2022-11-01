using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Entities.Queries.ExportEntities;

public record ExportEntitiesQuery : IRequest<ExportedEntitiesDto>;

public class ExportEntitiesQueryHandler : IRequestHandler<ExportEntitiesQuery, ExportedEntitiesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportEntitiesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedEntitiesDto> Handle(ExportEntitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Entities.OrderBy(e => e.Name).ThenBy(e => e.Code)
            .AsNoTracking()
            .Include(e => e.Initiatives)
            .Include(e => e.KPIs)
            .ProjectTo<EntityDto>(_mapper.ConfigurationProvider)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        return new ExportedEntitiesDto("Entities.csv", _csvFileBuilder.BuildEntitiesFile(entities));
    }
}