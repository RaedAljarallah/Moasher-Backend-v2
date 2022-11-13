using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class ConvertEnumValueToStringForAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_Name",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "Role_Style",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "FundStatus_Name",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "FundStatus_Style",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "Impact_Name",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Impact_Style",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Priority_Name",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Priority_Style",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Probability_Name",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Probability_Style",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Scope_Name",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Scope_Style",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Type_Name",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Type_Style",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "Phase_Name",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "Phase_Style",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "Phase_Name",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "Phase_Style",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "Status_Name",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Status_Style",
                table: "InitiativeContracts");

            migrationBuilder.RenameColumn(
                name: "Status_Style",
                table: "KPIs",
                newName: "StatusStyle");

            migrationBuilder.RenameColumn(
                name: "Status_Name",
                table: "KPIs",
                newName: "StatusName");

            migrationBuilder.RenameColumn(
                name: "Status_Style",
                table: "Initiatives",
                newName: "StatusStyle");

            migrationBuilder.RenameColumn(
                name: "Status_Name",
                table: "Initiatives",
                newName: "StatusName");

            migrationBuilder.RenameColumn(
                name: "Status_Style",
                table: "InitiativeIssues",
                newName: "StatusStyle");

            migrationBuilder.RenameColumn(
                name: "Status_Name",
                table: "InitiativeIssues",
                newName: "StatusName");

            migrationBuilder.RenameColumn(
                name: "Scope_Style",
                table: "InitiativeIssues",
                newName: "ScopeStyle");

            migrationBuilder.RenameColumn(
                name: "Scope_Name",
                table: "InitiativeIssues",
                newName: "ScopeName");

            migrationBuilder.RenameColumn(
                name: "Impact_Style",
                table: "InitiativeIssues",
                newName: "ImpactStyle");

            migrationBuilder.RenameColumn(
                name: "Impact_Name",
                table: "InitiativeIssues",
                newName: "ImpactName");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleStyle",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundStatusName",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundStatusStyle",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImpactName",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImpactStyle",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PriorityName",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PriorityStyle",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProbabilityName",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProbabilityStyle",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScopeName",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScopeStyle",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeStyle",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhaseName",
                table: "InitiativeProjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhaseStyle",
                table: "InitiativeProjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhaseName",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhaseStyle",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusStyle",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "RoleStyle",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "FundStatusName",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "FundStatusStyle",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "ImpactName",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "ImpactStyle",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "PriorityName",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "PriorityStyle",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "ProbabilityName",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "ProbabilityStyle",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "ScopeName",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "ScopeStyle",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "TypeStyle",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "PhaseName",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "PhaseStyle",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "PhaseName",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "PhaseStyle",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "StatusStyle",
                table: "InitiativeContracts");

            migrationBuilder.RenameColumn(
                name: "StatusStyle",
                table: "KPIs",
                newName: "Status_Style");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "KPIs",
                newName: "Status_Name");

            migrationBuilder.RenameColumn(
                name: "StatusStyle",
                table: "Initiatives",
                newName: "Status_Style");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "Initiatives",
                newName: "Status_Name");

            migrationBuilder.RenameColumn(
                name: "StatusStyle",
                table: "InitiativeIssues",
                newName: "Status_Style");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "InitiativeIssues",
                newName: "Status_Name");

            migrationBuilder.RenameColumn(
                name: "ScopeStyle",
                table: "InitiativeIssues",
                newName: "Scope_Style");

            migrationBuilder.RenameColumn(
                name: "ScopeName",
                table: "InitiativeIssues",
                newName: "Scope_Name");

            migrationBuilder.RenameColumn(
                name: "ImpactStyle",
                table: "InitiativeIssues",
                newName: "Impact_Style");

            migrationBuilder.RenameColumn(
                name: "ImpactName",
                table: "InitiativeIssues",
                newName: "Impact_Name");

            migrationBuilder.AddColumn<string>(
                name: "Role_Name",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role_Style",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundStatus_Name",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundStatus_Style",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impact_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impact_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Probability_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Probability_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phase_Name",
                table: "InitiativeProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phase_Style",
                table: "InitiativeProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phase_Name",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phase_Style",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status_Style",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
