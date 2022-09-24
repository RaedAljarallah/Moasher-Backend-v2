using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Issues.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Issues.Commands.CreateIssue;

public record CreateIssueCommand : IssueCommandBase, IRequest<IssueDto>;

public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateIssueCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IssueDto> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Issues)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new IssueDomainValidator(initiative, request.Description));

        var enumTypesIds = new[]
        {
            request.ScopeEnumId,
            request.StatusEnumId,
            request.ImpactEnumId
        };

        var enumTypes = await _context.EnumTypes
            .AsNoTracking()
            .Where(e => enumTypesIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        var scopeEnum = enumTypes.FirstOrDefault(e => e.Id == request.ScopeEnumId);
        var statusEnum = enumTypes.FirstOrDefault(e => e.Id == request.StatusEnumId);
        var impactEnum = enumTypes.FirstOrDefault(e => e.Id == request.ImpactEnumId);
        if (scopeEnum is null)
        {
            throw new ValidationException(nameof(request.ScopeEnumId), IssueEnumsValidationMessages.WrongScopeEnumId);
        }

        if (statusEnum is null)
        {
            throw new ValidationException(nameof(request.StatusEnumId), IssueEnumsValidationMessages.WrongStatusEnumId);
        }

        if (impactEnum is null)
        {
            throw new ValidationException(nameof(request.ImpactEnumId), IssueEnumsValidationMessages.WrongImpactEnumId);
        }

        var issue = _mapper.Map<InitiativeIssue>(request);
        issue.ScopeEnum = scopeEnum;
        issue.StatusEnum = statusEnum;
        issue.ImpactEnum = impactEnum;
        issue.Initiative = initiative;
        initiative.Issues.Add(issue);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<IssueDto>(issue);
    }
}