using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Milestones.Queries.ExportMilestones;

public record ExportMilestonesQuery : IRequest<ExportedMilestonesDto>;

public class ExportMilestonesQueryHandler : IRequestHandler<ExportMilestonesQuery, ExportedMilestonesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportMilestonesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedMilestonesDto> Handle(ExportMilestonesQuery request, CancellationToken cancellationToken)
    {
        var milestones = await _context.InitiativeMilestones
            .OrderBy(m => m.PlannedFinish)
            .ThenBy(m => m.Name)
            .AsNoTracking()
            .ProjectTo<MilestoneDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedMilestonesDto("Milestones.csv", _csvFileBuilder.BuildMilestonesFile(milestones));
    }
}