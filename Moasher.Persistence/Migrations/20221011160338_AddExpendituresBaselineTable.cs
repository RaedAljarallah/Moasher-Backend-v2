using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddExpendituresBaselineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialPlannedAmount",
                table: "InitiativeExpenditures");

            migrationBuilder.CreateTable(
                name: "InitiativeExpenditureBaseline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    InitialPlannedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeExpenditureBaseline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeExpenditureBaseline_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpenditureBaseline_ProjectId",
                table: "InitiativeExpenditureBaseline",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitiativeExpenditureBaseline");

            migrationBuilder.AddColumn<decimal>(
                name: "InitialPlannedAmount",
                table: "InitiativeExpenditures",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
