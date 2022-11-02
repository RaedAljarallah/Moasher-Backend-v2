using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Analytics.Queries.ExportAnalytics;

public record ExportAnalyticsQuery : IRequest<ExportedAnalyticsDto>
{
    public string? Model { get; set; }
}

public class ExportAnalyticsQueryHandler : IRequestHandler<ExportAnalyticsQuery, ExportedAnalyticsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportAnalyticsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedAnalyticsDto> Handle(ExportAnalyticsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Model))
        {
            throw new NotFoundException();
        }

        if (request.Model.ToUpper() is not "INITIATIVES" or "KPIS")
        {
            throw new NotFoundException();
        }

        var analyticsQuery = _context.Analytics.OrderByDescending(a => a.AnalyzedAt)
            .AsNoTracking()
            .ProjectTo<AnalyticDto>(_mapper.ConfigurationProvider);

        analyticsQuery = request.Model.ToUpper() switch
        {
            "INITIATIVES" => analyticsQuery.Where(a => a.InitiativeId.HasValue),
            "KPIS" => analyticsQuery.Where(a => a.KPIId.HasValue),
            _ => analyticsQuery
        };

        var analytics = await analyticsQuery.ToListAsync(cancellationToken);

        return request.Model.ToUpper() == "INITIATIVES"
            ? new ExportedAnalyticsDto("Initiatives_Analytics.csv",
                _csvFileBuilder.BuildInitiativesAnalytics(analytics))
            : new ExportedAnalyticsDto("KPIs_Analytics.csv", _csvFileBuilder.BuildKPIsAnalytics(analytics));
    }
}