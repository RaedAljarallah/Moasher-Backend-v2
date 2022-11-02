using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Deliverables.Queries.ExportDeliverables;

public record ExportDeliverablesQuery : IRequest<ExportedDeliverablesDto>;

public class ExportDeliverablesQueryHandler : IRequestHandler<ExportDeliverablesQuery, ExportedDeliverablesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportDeliverablesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedDeliverablesDto> Handle(ExportDeliverablesQuery request, CancellationToken cancellationToken)
    {
        var deliverables = await _context.InitiativeDeliverables.OrderBy(m => m.PlannedFinish).ThenBy(m => m.Name)
            .AsNoTracking()
            .ProjectTo<DeliverableDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedDeliverablesDto("Deliverables.csv", _csvFileBuilder.BuildDeliverablesFile(deliverables));
    }
}