using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddProjectProgressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitiativeProjectProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phase_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhaseEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhaseStartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PhaseEndedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PhaseStartedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhaseEndedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeProjectProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeProjectProgress_EnumTypes_PhaseEnumId",
                        column: x => x.PhaseEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeProjectProgress_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjectProgress_PhaseEnumId",
                table: "InitiativeProjectProgress",
                column: "PhaseEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjectProgress_ProjectId",
                table: "InitiativeProjectProgress",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitiativeProjectProgress");
        }
    }
}
