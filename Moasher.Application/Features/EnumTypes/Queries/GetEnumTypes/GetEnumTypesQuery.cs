using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.EnumTypes.Queries.GetEnumTypes;

public record GetEnumTypesQuery : QueryParameterBase, IRequest<PaginatedList<EnumTypeDto>>
{
    public string? Category { get; set; }
    public string? Name { get; set; }
}

public class GetEnumTypesQueryHandler : IRequestHandler<GetEnumTypesQuery, PaginatedList<EnumTypeDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetEnumTypesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<EnumTypeDto>> Handle(GetEnumTypesQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Category))
        {
            throw new NotFoundException();
        }
        
        return await _context.EnumTypes
            .AsNoTracking()
            .WithinParameters(new GetEnumTypesQueryParameter(request))
            .ProjectTo<EnumTypeDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}