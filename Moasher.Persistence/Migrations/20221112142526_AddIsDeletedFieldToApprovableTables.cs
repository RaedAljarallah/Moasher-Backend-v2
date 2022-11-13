using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddIsDeletedFieldToApprovableTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventArgumentTypes",
                table: "EditRequests");

            migrationBuilder.DropColumn(
                name: "EventArguments",
                table: "EditRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KPIValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeRisks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeProjectsBaseline",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeProjectProgress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeMilestones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeIssues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeImpacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeExpendituresBaseline",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeExpenditures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeDeliverables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeBudgets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InitiativeApprovedCosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KPIValues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeRisks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeProjectsBaseline");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeMilestones");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeImpacts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeExpendituresBaseline");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeExpenditures");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeDeliverables");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeBudgets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InitiativeApprovedCosts");

            migrationBuilder.AddColumn<string>(
                name: "EventArgumentTypes",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventArguments",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
