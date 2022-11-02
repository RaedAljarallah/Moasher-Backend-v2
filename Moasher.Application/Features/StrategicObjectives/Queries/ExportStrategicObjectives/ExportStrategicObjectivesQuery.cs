using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Application.Features.StrategicObjectives.Queries.ExportStrategicObjectives;

public record ExportStrategicObjectivesQuery : IRequest<ExportedStrategicObjectivesDto>;

public class ExportStrategicObjectivesQueryHandler : IRequestHandler<ExportStrategicObjectivesQuery,
        ExportedStrategicObjectivesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportStrategicObjectivesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedStrategicObjectivesDto> Handle(ExportStrategicObjectivesQuery request, CancellationToken cancellationToken)
    {
        var objectives = await _context.StrategicObjectives
            .AsNoTracking()
            .Select(o => new StrategicObjective
            {
                Id = o.Id,
                Name = o.Name,
                Code = o.Code,
                HierarchyId = o.HierarchyId,
                CreatedBy = o.CreatedBy,
                CreatedAt = o.CreatedAt,
                LastModifiedBy = o.LastModifiedBy,
                LastModified = o.LastModified
            })
            .ProjectTo<StrategicObjectiveDtoBase>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedStrategicObjectivesDto("Strategic_Objectives.csv",
            _csvFileBuilder.BuildObjectivesFile(objectives));
    }
}