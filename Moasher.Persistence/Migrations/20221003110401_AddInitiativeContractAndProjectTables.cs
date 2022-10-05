using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddInitiativeContractAndProjectTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeContracts_EnumTypes_TypeEnumId",
                table: "InitiativeContracts");

            migrationBuilder.DropTable(
                name: "InitiativeContractExpenditures");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeContracts_TypeEnumId",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "ContractedDate",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "DurationUnit",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "TypeEnumId",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Type_Name",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "Type_Style",
                table: "InitiativeContracts");

            migrationBuilder.RenameColumn(
                name: "OfferingDate",
                table: "InitiativeContracts",
                newName: "StartDate");

            migrationBuilder.CreateTable(
                name: "InitiativeProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedBiddingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualBiddingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PlannedContractingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualContractingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Phase_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhaseEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    InitiativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeProjects_EnumTypes_PhaseEnumId",
                        column: x => x.PhaseEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeProjects_InitiativeContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "InitiativeContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeProjects_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeExpenditures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    InitialPlannedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    PlannedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ActualAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeExpenditures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeExpenditures_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpenditures_ProjectId",
                table: "InitiativeExpenditures",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjects_ContractId",
                table: "InitiativeProjects",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjects_InitiativeId",
                table: "InitiativeProjects",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjects_PhaseEnumId",
                table: "InitiativeProjects",
                column: "PhaseEnumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitiativeExpenditures");

            migrationBuilder.DropTable(
                name: "InitiativeProjects");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "InitiativeContracts",
                newName: "OfferingDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ContractedDate",
                table: "InitiativeContracts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "InitiativeContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "DurationUnit",
                table: "InitiativeContracts",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeEnumId",
                table: "InitiativeContracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Name",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Style",
                table: "InitiativeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InitiativeContractExpenditures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitiativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeContractExpenditures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeContracts_TypeEnumId",
                table: "InitiativeContracts",
                column: "TypeEnumId");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeContracts_EnumTypes_TypeEnumId",
                table: "InitiativeContracts",
                column: "TypeEnumId",
                principalTable: "EnumTypes",
                principalColumn: "Id");
        }
    }
}
