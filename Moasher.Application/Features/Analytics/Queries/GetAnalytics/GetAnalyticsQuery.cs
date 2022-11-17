using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Application.Features.Analytics.Queries.GetAnalytics;

public record GetAnalyticsQuery : QueryParameterBase, IRequest<PaginatedList<AnalyticDto>>
{
    public DateTimeOffset? AnalyzedFrom { get; set; }
    public DateTimeOffset? AnalyzedTo { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? KPIId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public bool Latest { get; set; }
    public string? Model { get; set; }
}

public class GetAnalyticsQueryHandler : IRequestHandler<GetAnalyticsQuery, PaginatedList<AnalyticDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetAnalyticsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AnalyticDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var analytics = await _context.Analytics.OrderByDescending(a => a.AnalyzedAt)
            .AsNoTracking()
            .WithinParameters(new GetAnalyticsQueryParameter(request))
            .IncludeWhen(a => a.Initiative, request.Latest)
            .IncludeWhen(a => a.KPI, request.Latest)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var filteredAnalytics = new List<Analytic>(analytics);
        if (request.Latest)
        {
            filteredAnalytics.Clear();
            IEnumerable<LatestAnalyticsObject> latestAnalytics;
            switch (request.Model?.ToLower())
            {
                case "initiatives":
                    latestAnalytics = GetLatestAnalyticsObject(analytics.Select(a => a.Initiative));
                    break;
                case "kpis":
                    latestAnalytics = GetLatestAnalyticsObject(analytics.Select(a => a.KPI));
                    break;
                default:
                {
                    var initiativesLatest = GetLatestAnalyticsObject(analytics.Select(a => a.Initiative));
                    var kpisLatest = GetLatestAnalyticsObject(analytics.Select(a => a.KPI));
                    latestAnalytics = initiativesLatest.Union(kpisLatest);
                    break;
                }
            }

            latestAnalytics = latestAnalytics.ToList();
            filteredAnalytics.AddRange(from analytic in analytics
                let match = latestAnalytics.Any(a =>
                    a.LatestAnalytics == analytic.Description && a.LatestAnalyticsDate.HasValue &&
                    a.LatestAnalyticsDate.Value.Date == analytic.AnalyzedAt.Date)
                where match
                select analytic);
        }
        
        var analyticsDto = filteredAnalytics.Select(a => _mapper.Map<AnalyticDto>(a)).ToList();
        var result = new PaginatedList<AnalyticDto>(analyticsDto, filteredAnalytics.Count, request.PageNumber, request.PageSize);
        return result;
    }

    private static IEnumerable<LatestAnalyticsObject> GetLatestAnalyticsObject(IEnumerable<Initiative?> initiatives)
    {
        return initiatives.Select(i => new LatestAnalyticsObject
        {
            LatestAnalyticsDate = i?.LatestAnalyticsDate,
            LatestAnalytics = i?.LatestAnalytics,
        });
    }

    private static IEnumerable<LatestAnalyticsObject> GetLatestAnalyticsObject(IEnumerable<KPI?> kpis)
    {
        return kpis.Select(k => new LatestAnalyticsObject
        {
            LatestAnalyticsDate = k?.LatestAnalyticsDate,
            LatestAnalytics = k?.LatestAnalytics,
        });
    }
}

internal class LatestAnalyticsObject
{
    public string? LatestAnalytics { get; set; }
    public DateTimeOffset? LatestAnalyticsDate { get; set; }
}