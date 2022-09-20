using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.EnumTypes;

namespace Moasher.Application.Features.EnumTypes.EventHandlers;

public class EnumTypeUpdatedEventHandler : INotificationHandler<EnumTypeUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public EnumTypeUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(EnumTypeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var enumType = notification.EnumType;
        
        if (enumType.Category == EnumTypeCategory.InitiativeTeamRole.ToString())
        {
            var teamMembers = await _context.InitiativeTeams.Where(t => t.RoleEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            teamMembers.ForEach(t => t.RoleEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeStatus.ToString())
        {
            var initiatives = await _context.Initiatives.Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            initiatives.ForEach(i => i.StatusEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeFundStatus.ToString())
        {
            var initiatives = await _context.Initiatives.Where(i => i.FundStatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            initiatives.ForEach(i => i.FundStatusEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeIssueScope.ToString())
        {
            var issues = await _context.InitiativeIssues.Where(i => i.ScopeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.ScopeEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeIssueStatus.ToString())
        {
            var issues = await _context.InitiativeIssues.Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.StatusEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeIssueImpact.ToString())
        {
            var issues = await _context.InitiativeIssues.Where(i => i.ImpactEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.ImpactEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeRiskType.ToString())
        {
            var risks = await _context.InitiativeRisks.Where(i => i.TypeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.TypeEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeRiskPriority.ToString())
        {
            var risks = await _context.InitiativeRisks.Where(i => i.PriorityEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.PriorityEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeRiskProbability.ToString())
        {
            var risks = await _context.InitiativeRisks.Where(i => i.ProbabilityEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ProbabilityEnum = enumType);
        }
        
        if (enumType.Category == EnumTypeCategory.InitiativeRiskImpact.ToString())
        {
            var risks = await _context.InitiativeRisks.Where(i => i.ImpactEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ImpactEnum = enumType);
        }

        if (enumType.Category == EnumTypeCategory.InitiativeRiskScope.ToString())
        {
            var risks = await _context.InitiativeRisks.Where(i => i.ScopeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ScopeEnum = enumType);
        }

        if (enumType.Category == EnumTypeCategory.InitiativeContractType.ToString())
        {
            var contracts = await _context.InitiativeContracts.Where(i => i.TypeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            contracts.ForEach(i => i.TypeEnum = enumType);
        }

        if (enumType.Category == EnumTypeCategory.InitiativeContractStatus.ToString())
        {
            var contracts = await _context.InitiativeContracts.Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            contracts.ForEach(i => i.StatusEnum = enumType);
        }
        
        // TODO: Add Initiative ProjectsStatus
        if (enumType.Category == EnumTypeCategory.KPIStatus.ToString())
        {
            var kpis = await _context.KPIs.Where(k => k.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            kpis.ForEach(k => k.StatusEnum = enumType);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}