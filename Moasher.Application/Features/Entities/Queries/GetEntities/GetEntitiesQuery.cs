using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Entities.Queries.GetEntities;

public record GetEntitiesQuery : QueryParameterBase, IRequest<PaginatedList<EntityDto>>
{
    public Guid? ProgramId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public bool WithInitiatives { get; set; }
    public bool WithKPIs { get; set; }
}

public class GetEntitiesQueryHandler : IRequestHandler<GetEntitiesQuery, PaginatedList<EntityDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetEntitiesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<EntityDto>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Entities.OrderBy(e => e.Name).ThenBy(e => e.Code)
            .AsNoTracking()
            .WithinParameters(new GetEntitiesQueryParameter(request))
            .ProjectTo<EntityDto>(_mapper.ConfigurationProvider)
            .AsSplitQuery()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}