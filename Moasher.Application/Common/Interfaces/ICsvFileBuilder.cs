using Moasher.Application.Features.Analytics;
using Moasher.Application.Features.ApprovedCosts;
using Moasher.Application.Features.Budgets;
using Moasher.Application.Features.Contracts;
using Moasher.Application.Features.Deliverables;
using Moasher.Application.Features.Entities;
using Moasher.Application.Features.Expenditures;
using Moasher.Application.Features.Initiatives;
using Moasher.Application.Features.InitiativeTeams;
using Moasher.Application.Features.Issues;
using Moasher.Application.Features.KPIs;
using Moasher.Application.Features.KPIValues;
using Moasher.Application.Features.Milestones;
using Moasher.Application.Features.Programs;
using Moasher.Application.Features.Projects;
using Moasher.Application.Features.Risks;
using Moasher.Application.Features.StrategicObjectives;

namespace Moasher.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    public byte[] BuildEntitiesFile(IEnumerable<EntityDto> entities);
    public byte[] BuildObjectivesFile(IEnumerable<StrategicObjectiveDtoBase> objectives);
    public byte[] BuildProgramsFile(IEnumerable<ProgramDto> programs);
    public byte[] BuildKPIsFile(IEnumerable<KPIDto> kpis);
    public byte[] BuildKPIsValues(IEnumerable<KPIValueDto> values);
    public byte[] BuildKPIsAnalytics(IEnumerable<AnalyticDto> analytics);
    public byte[] BuildInitiativesFile(IEnumerable<InitiativeDto> initiatives);
    public byte[] BuildMilestonesFile(IEnumerable<MilestoneDto> milestones);
    public byte[] BuildDeliverablesFile(IEnumerable<DeliverableDto> deliverables);
    public byte[] BuildApprovedCostsFile(IEnumerable<ApprovedCostDto> approvedCosts);
    public byte[] BuildBudgetsFile(IEnumerable<BudgetDto> budgets);
    public byte[] BuildContractsFile(IEnumerable<ContractDto> contracts);
    public byte[] BuildProjectsFile(IEnumerable<ProjectDto> projects);
    public byte[] BuildExpenditures(IEnumerable<ExpenditureDto> expenditures);
    public byte[] BuildIssuesFile(IEnumerable<IssueDto> issues);
    public byte[] BuildRisksFile(IEnumerable<RiskDto> risks);
    public byte[] BuildTeamsFile(IEnumerable<InitiativeTeamDto> teams);
    public byte[] BuildInitiativesAnalytics(IEnumerable<AnalyticDto> analytics);

}