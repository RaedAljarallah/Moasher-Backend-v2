using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.KPIValues.Queries.ExportKPIsValues;

public record ExportKPIsValuesQuery : IRequest<ExportedKPIsValuesDto>;

public class ExportKPIsValuesQueryHandler : IRequestHandler<ExportKPIsValuesQuery, ExportedKPIsValuesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportKPIsValuesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedKPIsValuesDto> Handle(ExportKPIsValuesQuery request, CancellationToken cancellationToken)
    {
        var values = await _context.KPIValues
            .OrderBy(v => v.PlannedFinish)
            .ThenBy(v => v.MeasurementPeriod)
            .ThenBy(v => v.Year)
            .AsNoTracking()
            .ProjectTo<KPIValueDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedKPIsValuesDto("KPIs_Values.csv", _csvFileBuilder.BuildKPIsValues(values));
    }
}