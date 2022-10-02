using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Programs.Queries.GetPrograms;

public record GetProgramsQuery :  QueryParameterBase, IRequest<PaginatedList<ProgramDto>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public bool WithInitiatives { get; set; }
    public bool WithKPIs { get; set; }
    public Guid? PortfolioId { get; set; }
}

public class GetProgramsQueryHandler : IRequestHandler<GetProgramsQuery, PaginatedList<ProgramDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetProgramsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<ProgramDto>> Handle(GetProgramsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Programs
            .AsNoTracking()
            .WithinParameters(new GetProgramsQueryParameter(request))
            .ProjectTo<ProgramDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}