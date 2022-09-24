using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Issues.Queries.EditIssue;

public record EditIssueQuery : IRequest<EditIssueDto>
{
    public Guid Id { get; set; }
}

public class EditIssueQueryHandler : IRequestHandler<EditIssueQuery, EditIssueDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditIssueQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditIssueDto> Handle(EditIssueQuery request, CancellationToken cancellationToken)
    {
        var issue = await _context.InitiativeIssues
            .AsNoTracking()
            .Include(i => i.ScopeEnum)
            .Include(i => i.StatusEnum)
            .Include(i => i.ImpactEnum)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (issue is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<EditIssueDto>(issue);
    }
}