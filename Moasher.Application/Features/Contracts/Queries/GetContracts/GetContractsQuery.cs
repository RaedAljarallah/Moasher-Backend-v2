using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Contracts.Queries.GetContracts;

public record GetContractsQuery : QueryParameterBase, IRequest<PaginatedList<ContractDto>>
{
    public string? Name { get; set; }
    public string? RefNumber { get; set; }
    public string? Supplier { get; set; }
    public DateTimeOffset? StartFrom { get; set; }
    public DateTimeOffset? StartTo { get; set; }
    public DateTimeOffset? EndFrom { get; set; }
    public DateTimeOffset? EndTo { get; set; }
    public Guid? StatusId { get; set; }
}

public class GetContractsQueryHandler : IRequestHandler<GetContractsQuery, PaginatedList<ContractDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetContractsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<ContractDto>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeContracts.OrderBy(c => c.StartDate).ThenBy(c => c.Name)
            .AsNoTracking()
            .Include(c => c.Expenditures)
            .WithinParameters(new GetContractsQueryParameter(request))
            .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}