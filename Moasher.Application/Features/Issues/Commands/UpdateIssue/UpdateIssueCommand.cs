using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Issues.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Issues.Commands.UpdateIssue;

public record UpdateIssueCommand : IssueCommandBase, IRequest<IssueDto>
{
    public Guid Id { get; set; }
}

public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, IssueDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateIssueCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IssueDto> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Issues)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        var issue = initiative.Issues.FirstOrDefault(i => i.Id == request.Id);
        if (issue is null)
        {
            throw new NotFoundException();
        }
        
        initiative.Issues = initiative.Issues.Where(i => i.Id != request.Id).ToList();
        request.ValidateAndThrow(new IssueDomainValidator(initiative, request.Description));

        if (IsDifferentEnums(request, issue))
        {
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
            
            var scopeEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[0]);
            var statusEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[1]);
            var impactEnum = enumTypes.FirstOrDefault(e => e.Id == enumTypesIds[2]);
            if (scopeEnum == null)
            {
                throw new ValidationException(nameof(request.ScopeEnumId), IssueEnumsValidationMessages.WrongScopeEnumId);
            }
            if (statusEnum == null)
            {
                throw new ValidationException(nameof(request.StatusEnumId), IssueEnumsValidationMessages.WrongStatusEnumId);
            }
            if (impactEnum == null)
            {
                throw new ValidationException(nameof(request.ImpactEnumId), IssueEnumsValidationMessages.WrongImpactEnumId);
            }

            issue.ScopeEnum = scopeEnum;
            issue.StatusEnum = statusEnum;
            issue.ImpactEnum = impactEnum;
            
        }
        
        _mapper.Map(request, issue);
        _context.InitiativeIssues.Update(issue);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<IssueDto>(issue);
    }
    
    private static bool IsDifferentEnums(UpdateIssueCommand request, InitiativeIssue issue)
    {
        return issue.ScopeEnumId != request.ScopeEnumId
               || issue.StatusEnumId != request.StatusEnumId
               || issue.ImpactEnumId != request.ImpactEnumId;
    }
}