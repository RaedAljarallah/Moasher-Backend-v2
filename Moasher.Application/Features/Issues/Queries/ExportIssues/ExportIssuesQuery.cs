using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Issues.Queries.ExportIssues;

public record ExportIssuesQuery : IRequest<ExportedIssuesDto>;

public class ExportIssuesQueryHandler : IRequestHandler<ExportIssuesQuery, ExportedIssuesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportIssuesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedIssuesDto> Handle(ExportIssuesQuery request, CancellationToken cancellationToken)
    {
        var issues = await _context.InitiativeIssues.OrderBy(i => i.RaisedAt).ThenBy(i => i.Description)
            .AsNoTracking()
            .ProjectTo<IssueDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedIssuesDto("Issues.csv", _csvFileBuilder.BuildIssuesFile(issues));
    }
}