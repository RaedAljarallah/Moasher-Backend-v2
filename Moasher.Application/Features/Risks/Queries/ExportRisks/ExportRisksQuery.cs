using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Risks.Queries.ExportRisks;

public record ExportRisksQuery : IRequest<ExportedRisksDto>;

public class ExportRisksQueryHandler : IRequestHandler<ExportRisksQuery, ExportedRisksDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportRisksQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedRisksDto> Handle(ExportRisksQuery request, CancellationToken cancellationToken)
    {
        var risks = await _context.InitiativeRisks.OrderBy(r => r.RaisedAt).ThenBy(r => r.Description)
            .AsNoTracking()
            .ProjectTo<RiskDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedRisksDto("Risks.csv", _csvFileBuilder.BuildRisksFile(risks));
    }
}