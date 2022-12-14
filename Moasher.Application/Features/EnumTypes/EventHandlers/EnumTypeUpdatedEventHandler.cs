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

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeTeamRole.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var teamMembers = await _context.InitiativeTeams
                .IgnoreQueryFilters()
                .Where(t => t.RoleEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            teamMembers.ForEach(t => t.RoleEnum = enumType);
            _context.InitiativeTeams.UpdateRange(teamMembers);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeStatus.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var initiatives = await _context.Initiatives
                .IgnoreQueryFilters()
                .Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            initiatives.ForEach(i => i.StatusEnum = enumType);
            _context.Initiatives.UpdateRange(initiatives);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeFundStatus.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var initiatives = await _context.Initiatives
                .IgnoreQueryFilters()
                .Where(i => i.FundStatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            initiatives.ForEach(i => i.FundStatusEnum = enumType);
            _context.Initiatives.UpdateRange(initiatives);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeIssueScope.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var issues = await _context.InitiativeIssues
                .IgnoreQueryFilters()
                .Where(i => i.ScopeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.ScopeEnum = enumType);
            _context.InitiativeIssues.UpdateRange(issues);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeIssueStatus.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var issues = await _context.InitiativeIssues
                .IgnoreQueryFilters()
                .Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.StatusEnum = enumType);
            _context.InitiativeIssues.UpdateRange(issues);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeIssueImpact.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var issues = await _context.InitiativeIssues
                .IgnoreQueryFilters()
                .Where(i => i.ImpactEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            issues.ForEach(i => i.ImpactEnum = enumType);
            _context.InitiativeIssues.UpdateRange(issues);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeRiskType.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var risks = await _context.InitiativeRisks
                .IgnoreQueryFilters()
                .Where(i => i.TypeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.TypeEnum = enumType);
            _context.InitiativeRisks.UpdateRange(risks);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeRiskPriority.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var risks = await _context.InitiativeRisks
                .IgnoreQueryFilters()
                .Where(i => i.PriorityEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.PriorityEnum = enumType);
            _context.InitiativeRisks.UpdateRange(risks);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeRiskProbability.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var risks = await _context.InitiativeRisks
                .IgnoreQueryFilters()
                .Where(i => i.ProbabilityEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ProbabilityEnum = enumType);
            _context.InitiativeRisks.UpdateRange(risks);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeRiskImpact.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var risks = await _context.InitiativeRisks
                .IgnoreQueryFilters()
                .Where(i => i.ImpactEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ImpactEnum = enumType);
            _context.InitiativeRisks.UpdateRange(risks);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeRiskScope.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var risks = await _context.InitiativeRisks
                .IgnoreQueryFilters()
                .Where(i => i.ScopeEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            risks.ForEach(i => i.ScopeEnum = enumType);
            _context.InitiativeRisks.UpdateRange(risks);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeContractStatus.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var contracts = await _context.InitiativeContracts
                .IgnoreQueryFilters()
                .Where(i => i.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            contracts.ForEach(i => i.StatusEnum = enumType);
            _context.InitiativeContracts.UpdateRange(contracts);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.InitiativeProjectPhase.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var projects = await _context.InitiativeProjects
                .IgnoreQueryFilters()
                .Where(p => p.PhaseEnumId == enumType.Id)
                .Include(p => p.Progress)
                .ToListAsync(cancellationToken);
            
            projects.ForEach(p =>
            {
                p.PhaseEnum = enumType;
                p.Progress.ToList().ForEach(pr => pr.PhaseEnum = enumType);
            });
            _context.InitiativeProjects.UpdateRange(projects);
        }

        if (string.Equals(enumType.Category, EnumTypeCategory.KPIStatus.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
        {
            var kpis = await _context.KPIs
                .IgnoreQueryFilters()
                .Where(k => k.StatusEnumId == enumType.Id)
                .ToListAsync(cancellationToken);

            kpis.ForEach(k => k.StatusEnum = enumType);
            _context.KPIs.UpdateRange(kpis);
        }

        await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
    }
}