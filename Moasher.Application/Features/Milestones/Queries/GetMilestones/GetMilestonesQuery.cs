using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Milestones.Queries.GetMilestones;

public record GetMilestonesQuery : SchedulableQueryParametersDto, IRequest<PaginatedList<MilestoneDto>>
{
    public string? Name { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public bool? ForDashboard { get; set; }
}

public class GetMilestonesQueryHandler : IRequestHandler<GetMilestonesQuery, PaginatedList<MilestoneDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetMilestonesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<MilestoneDto>> Handle(GetMilestonesQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeMilestones.OrderBy(m => m.PlannedFinish).ThenBy(m => m.Name)
            .AsNoTracking()
            .WithinParameters(new GetMilestonesQueryParameter(request))
            .ProjectTo<MilestoneDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}