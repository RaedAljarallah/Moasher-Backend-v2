using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddHasUpdateAndHasDeleteRequestToApprovableTabes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "KPIValues",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeTeams",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeRisks",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeProjectsBaseline",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeProjects",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeProjectProgress",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeMilestones",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeIssues",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeImpacts",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeExpendituresBaseline",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeExpenditures",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeDeliverables",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeContracts",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeBudgets",
                newName: "HasUpdateRequest");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InitiativeApprovedCosts",
                newName: "HasUpdateRequest");

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "KPIValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeRisks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeProjectsBaseline",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeProjectProgress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeMilestones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeIssues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeImpacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeExpendituresBaseline",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeExpenditures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeDeliverables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeBudgets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleteRequest",
                table: "InitiativeApprovedCosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "KPIValues");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeProjectsBaseline");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeMilestones");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeImpacts");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeExpendituresBaseline");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeExpenditures");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeDeliverables");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeBudgets");

            migrationBuilder.DropColumn(
                name: "HasDeleteRequest",
                table: "InitiativeApprovedCosts");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "KPIValues",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeTeams",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeRisks",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeProjectsBaseline",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeProjects",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeProjectProgress",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeMilestones",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeIssues",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeImpacts",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeExpendituresBaseline",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeExpenditures",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeDeliverables",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeContracts",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeBudgets",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "HasUpdateRequest",
                table: "InitiativeApprovedCosts",
                newName: "IsDeleted");
        }
    }
}
