using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class FixEnumValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "KPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Style",
                table: "KPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_Name",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_Style",
                table: "InitiativeTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundStatus_Name",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundStatus_Style",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Style",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Probability_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Probability_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Scope_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Scope_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Name",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Style",
                table: "InitiativeRisks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact_Name",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact_Style",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Scope_Name",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Scope_Style",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Style",
                table: "InitiativeIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Style",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Name",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Style",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status_Name",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "Status_Style",
                table: "KPIs");

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
                name: "Status_Name",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "Status_Style",
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
                name: "Impact_Name",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Impact_Style",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Scope_Name",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Scope_Style",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Status_Name",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Status_Style",
                table: "InitiativeIssues");

            migrationBuilder.DropColumn(
                name: "Status_Name",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Status_Style",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Type_Name",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Type_Style",
                table: "InitiativeContracts");
        }
    }
}
