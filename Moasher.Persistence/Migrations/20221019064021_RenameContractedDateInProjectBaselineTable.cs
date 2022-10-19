using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class RenameContractedDateInProjectBaselineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeProjectBaseline_InitiativeProjects_ProjectId",
                table: "InitiativeProjectBaseline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitiativeProjectBaseline",
                table: "InitiativeProjectBaseline");

            migrationBuilder.RenameTable(
                name: "InitiativeProjectBaseline",
                newName: "InitiativeProjectsBaseline");

            migrationBuilder.RenameColumn(
                name: "InitialPlannedContractEndDate",
                table: "InitiativeProjectsBaseline",
                newName: "InitialPlannedContractingDate");

            migrationBuilder.RenameIndex(
                name: "IX_InitiativeProjectBaseline_ProjectId",
                table: "InitiativeProjectsBaseline",
                newName: "IX_InitiativeProjectsBaseline_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitiativeProjectsBaseline",
                table: "InitiativeProjectsBaseline",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeProjectsBaseline_InitiativeProjects_ProjectId",
                table: "InitiativeProjectsBaseline",
                column: "ProjectId",
                principalTable: "InitiativeProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeProjectsBaseline_InitiativeProjects_ProjectId",
                table: "InitiativeProjectsBaseline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitiativeProjectsBaseline",
                table: "InitiativeProjectsBaseline");

            migrationBuilder.RenameTable(
                name: "InitiativeProjectsBaseline",
                newName: "InitiativeProjectBaseline");

            migrationBuilder.RenameColumn(
                name: "InitialPlannedContractingDate",
                table: "InitiativeProjectBaseline",
                newName: "InitialPlannedContractEndDate");

            migrationBuilder.RenameIndex(
                name: "IX_InitiativeProjectsBaseline_ProjectId",
                table: "InitiativeProjectBaseline",
                newName: "IX_InitiativeProjectBaseline_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitiativeProjectBaseline",
                table: "InitiativeProjectBaseline",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeProjectBaseline_InitiativeProjects_ProjectId",
                table: "InitiativeProjectBaseline",
                column: "ProjectId",
                principalTable: "InitiativeProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
