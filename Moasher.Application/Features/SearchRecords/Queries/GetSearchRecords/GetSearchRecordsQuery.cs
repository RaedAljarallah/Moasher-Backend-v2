using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.SearchRecords.Queries.GetSearchRecords;

public record GetSearchRecordsQuery : IRequest<IEnumerable<SearchRecordDto>>
{
    private string? _searchQuery;

    public string? SearchQuery
    {
        get => _searchQuery;
        set => _searchQuery = value?.Trim();
    }
}

public class GetSearchRecordsQueryHandler : IRequestHandler<GetSearchRecordsQuery, IEnumerable<SearchRecordDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public GetSearchRecordsQueryHandler(IMoasherDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<SearchRecordDto>> Handle(GetSearchRecordsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.SearchQuery is null)
        {
            return new List<SearchRecordDto>();
        }

        if (_currentUser.IsEntityUser())
        {
            var userEntities = await _context.Entities
                .Select(e => new Search(e.Id, e.Name, SearchCategory.Entity))
                .ToListAsync(cancellationToken);

            var userPrograms = await _context.Programs
                .Select(p => new Search(p.Id, p.Name, SearchCategory.Program))
                .ToListAsync(cancellationToken);

            var userKPIs = await _context.KPIs
                .Select(k => new Search(k.Id, k.Name, SearchCategory.KPI))
                .ToListAsync(cancellationToken);

            var userInitiatives = await _context.Initiatives
                .Select(i => new Search(i.Id, i.Name, SearchCategory.Initiative))
                .ToListAsync(cancellationToken);

            var userSearchRecords =
                new List<Search>(userEntities.Count + userPrograms.Count + userKPIs.Count + userInitiatives.Count);
            
            userSearchRecords.AddRange(userEntities);
            userSearchRecords.AddRange(userPrograms);
            userSearchRecords.AddRange(userKPIs);
            userSearchRecords.AddRange(userInitiatives);
            
            return userSearchRecords.AsQueryable()
                .OrderBy(s => s.Title)
                .Like(request.SearchQuery, "Title")
                .ProjectTo<SearchRecordDto>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToList();
        }

        return await _context.SearchRecords
            .OrderBy(s => s.Title)
            .Like(request.SearchQuery, "Title")
            .ProjectTo<SearchRecordDto>(_mapper.ConfigurationProvider)
            .Take(5)
            .ToListAsync(cancellationToken);
    }
}