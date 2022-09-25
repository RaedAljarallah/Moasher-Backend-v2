using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.KPIValues.Queries.GetKPIValues;

public record GetKPIValuesQuery : SchedulableQueryParametersDto, IRequest<PaginatedList<KPIValueDto>>
{
    public Guid? KPIId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
}

public class GetKPIValuesQueryHandler : IRequestHandler<GetKPIValuesQuery, PaginatedList<KPIValueDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetKPIValuesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<KPIValueDto>> Handle(GetKPIValuesQuery request, CancellationToken cancellationToken)
    {
        return await _context.KPIValues.OrderBy(v => v.PlannedFinish).ThenBy(v => v.MeasurementPeriod).ThenBy(v => v.Year)
            .AsNoTracking()
            .WithinParameters(new GetKPIValuesQueryParameter(request))
            .ProjectTo<KPIValueDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}