using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.KPIs.Queries.ExportKPIs;

public record ExportKPIsQuery : IRequest<ExportedKPIsDto>;

public class ExportKPIsQueryHandler : IRequestHandler<ExportKPIsQuery, ExportedKPIsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportKPIsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedKPIsDto> Handle(ExportKPIsQuery request, CancellationToken cancellationToken)
    {
        var kpis = await _context.KPIs
            .AsNoTracking()
            .ProjectTo<KPIDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedKPIsDto("KPIs.csv", _csvFileBuilder.BuildKPIsFile(kpis));
    }
}