using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Deliverables.Queries.GetDeliverables;

public record GetDeliverablesQuery : SchedulableQueryParametersDto, IRequest<PaginatedList<DeliverableDto>>
{
    public string? Name { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public bool? ForDashboard { get; set; }
}

public class GetDeliverablesQueryHandler : IRequestHandler<GetDeliverablesQuery, PaginatedList<DeliverableDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetDeliverablesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<DeliverableDto>> Handle(GetDeliverablesQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeDeliverables.OrderBy(m => m.PlannedFinish).ThenBy(m => m.Name)
            .AsNoTracking()
            .WithinParameters(new GetDeliverablesQueryParameter(request))
            .ProjectTo<DeliverableDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.Pn, request.Ps, cancellationToken);
    }
}