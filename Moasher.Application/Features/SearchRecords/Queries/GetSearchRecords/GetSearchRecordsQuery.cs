using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.SearchRecords.Queries.GetSearchRecords;

public record GetSearchRecordsQuery : IRequest<IEnumerable<SearchRecordDto>>
{
    private string? _searchQuery;

    public string? SearchQuery { get => _searchQuery; set => _searchQuery = value?.Trim(); }
}

public class GetSearchRecordsQueryHandler : IRequestHandler<GetSearchRecordsQuery, IEnumerable<SearchRecordDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetSearchRecordsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<SearchRecordDto>> Handle(GetSearchRecordsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            return new List<SearchRecordDto>();
        }

        return await _context.SearchRecords
            .OrderBy(s => s.Title)
            .Like(request.SearchQuery, "Title")
            .ProjectTo<SearchRecordDto>(_mapper.ConfigurationProvider)
            .Take(5)
            .ToListAsync(cancellationToken);
    }
}