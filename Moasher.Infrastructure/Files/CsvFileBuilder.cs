using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Moasher.Application.Common.Interfaces;
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
using Moasher.Infrastructure.Files.Maps;

namespace Moasher.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildEntitiesFile(IEnumerable<EntityDto> entities) => GetCsvFile<EntityDto, EntityCsvMap>(entities);

    public byte[] BuildObjectivesFile(IEnumerable<StrategicObjectiveDtoBase> objectives) =>
        GetCsvFile<StrategicObjectiveDtoBase, StrategicObjectiveCsvMap>(objectives);

    public byte[] BuildProgramsFile(IEnumerable<ProgramDto> programs) =>
        GetCsvFile<ProgramDto, ProgramCsvMap>(programs);

    public byte[] BuildKPIsFile(IEnumerable<KPIDto> kpis) => 
        GetCsvFile<KPIDto, KPICsvMap>(kpis);

    public byte[] BuildKPIsValues(IEnumerable<KPIValueDto> values) =>
        GetCsvFile<KPIValueDto, KPIValueCsvMap>(values);

    public byte[] BuildKPIsAnalytics(IEnumerable<AnalyticDto> analytics) =>
        GetCsvFile<AnalyticDto, KPIAnalyticCsvMap>(analytics);

    public byte[] BuildInitiativesFile(IEnumerable<InitiativeDto> initiatives) =>
        GetCsvFile<InitiativeDto, InitiativeCsvMap>(initiatives);

    public byte[] BuildMilestonesFile(IEnumerable<MilestoneDto> milestones) =>
        GetCsvFile<MilestoneDto, MilestoneCsvMap>(milestones);

    public byte[] BuildDeliverablesFile(IEnumerable<DeliverableDto> deliverables) =>
        GetCsvFile<DeliverableDto, DeliverableCsvMap>(deliverables);

    public byte[] BuildApprovedCostsFile(IEnumerable<ApprovedCostDto> approvedCosts) =>
        GetCsvFile<ApprovedCostDto, ApprovedCostCsvMap>(approvedCosts);

    public byte[] BuildBudgetsFile(IEnumerable<BudgetDto> budgets) =>
        GetCsvFile<BudgetDto, BudgetCsvMap>(budgets);

    public byte[] BuildContractsFile(IEnumerable<ContractDto> contracts) =>
        GetCsvFile<ContractDto, ContractCsvMap>(contracts);

    public byte[] BuildProjectsFile(IEnumerable<ProjectDto> projects) =>
        GetCsvFile<ProjectDto, ProjectCsvMap>(projects);

    public byte[] BuildExpenditures(IEnumerable<ExpenditureDto> expenditures) =>
        GetCsvFile<ExpenditureDto, ExpenditureCsvMap>(expenditures);

    public byte[] BuildIssuesFile(IEnumerable<IssueDto> issues) =>
        GetCsvFile<IssueDto, IssueCsvMap>(issues);

    public byte[] BuildRisksFile(IEnumerable<RiskDto> risks) =>
        GetCsvFile<RiskDto, RiskCsvMap>(risks);

    public byte[] BuildTeamsFile(IEnumerable<InitiativeTeamDto> teams) =>
        GetCsvFile<InitiativeTeamDto, InitiativeTeamCsvMap>(teams);

    public byte[] BuildInitiativesAnalytics(IEnumerable<AnalyticDto> analytics) =>
        GetCsvFile<AnalyticDto, InitiativeAnalyticCsvMap>(analytics);

    private byte[] GetCsvFile<TSource, TMap>(IEnumerable<TSource> records) where TMap : ClassMap<TSource>
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}