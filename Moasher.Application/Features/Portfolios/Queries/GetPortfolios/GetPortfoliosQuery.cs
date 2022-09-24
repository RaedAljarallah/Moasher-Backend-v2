using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;


namespace Moasher.Application.Features.Portfolios.Queries.GetPortfolios;

public record GetPortfoliosQuery : QueryParameterBase, IRequest<PaginatedList<PortfolioDto>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class GetPortfoliosQueryHandler : IRequestHandler<GetPortfoliosQuery, PaginatedList<PortfolioDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetPortfoliosQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<PortfolioDto>> Handle(GetPortfoliosQuery request, CancellationToken cancellationToken)
    {
        return await _context.Portfolios
            .AsNoTracking()
            .WithinParameters(new GetPortfoliosQueryParameter(request))
            .ProjectTo<PortfolioDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}