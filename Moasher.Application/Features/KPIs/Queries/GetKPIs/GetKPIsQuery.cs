using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIs;

public record GetKPIsQuery : QueryParameterBase, IRequest<PaginatedList<KPIDto>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? EntityName { get; set; }
    public string? L1Name { get; set; }
    public string? L2Name { get; set; }
    public string? L3Name { get; set; }
    public string? L4Name { get; set; }
    public string? Status { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? StatusId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
}

public class GetKPIsQueryHandler : IRequestHandler<GetKPIsQuery, PaginatedList<KPIDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetKPIsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<KPIDto>> Handle(GetKPIsQuery request, CancellationToken cancellationToken)
    {
        return await _context.KPIs
            .AsNoTracking()
            .WithinParameters(new GetKPIsQueryParameter(request))
            .ProjectTo<KPIDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}