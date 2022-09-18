using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_EnumTypes_FundStatusEnumId",
                table: "Initiatives");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "StrategicObjectives",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "StrategicObjectives",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "StrategicObjectives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Programs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Programs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Programs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Portfolios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Portfolios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Portfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "FundStatusEnumId",
                table: "Initiatives",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Initiatives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedCost",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentYearBudget",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LatestAnalytics",
                table: "Initiatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LatestAnalyticsDate",
                table: "Initiatives",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBudget",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "EnumTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Entities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InitiativeApprovedCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    SupportingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeApprovedCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeApprovedCosts_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeBudgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    SupportingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeBudgets_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeContractExpenditures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InitiativeContractExpenditures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ContractedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    TypeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DurationUnit = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeContracts_EnumTypes_StatusEnumId",
                        column: x => x.StatusEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeContracts_EnumTypes_TypeEnumId",
                        column: x => x.TypeEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeContracts_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeDeliverables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SupportingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeDeliverables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeDeliverables_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeImpacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_InitiativeImpacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeImpacts_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeIssues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedResolutionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RaisedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaisedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeIssues_EnumTypes_ImpactEnumId",
                        column: x => x.ImpactEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeIssues_EnumTypes_ScopeEnumId",
                        column: x => x.ScopeEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeIssues_EnumTypes_StatusEnumId",
                        column: x => x.StatusEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeIssues_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeMilestones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    SupportingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeMilestones_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriorityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProbabilityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsePlane = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaisedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaisedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_InitiativeRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_EnumTypes_ImpactEnumId",
                        column: x => x.ImpactEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_EnumTypes_PriorityEnumId",
                        column: x => x.PriorityEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_EnumTypes_ProbabilityEnumId",
                        column: x => x.ProbabilityEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_EnumTypes_ScopeEnumId",
                        column: x => x.ScopeEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_EnumTypes_TypeEnumId",
                        column: x => x.TypeEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeRisks_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_InitiativeTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeTeams_EnumTypes_RoleEnumId",
                        column: x => x.RoleEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeTeams_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<byte>(type: "tinyint", nullable: false),
                    Polarity = table.Column<byte>(type: "tinyint", nullable: false),
                    ValidationStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaselineValue = table.Column<float>(type: "real", nullable: true),
                    BaselineYear = table.Column<short>(type: "smallint", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedProgress = table.Column<float>(type: "real", nullable: true),
                    ActualProgress = table.Column<float>(type: "real", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    VisibleOnDashboard = table.Column<bool>(type: "bit", nullable: false),
                    CalculateStatus = table.Column<bool>(type: "bit", nullable: false),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelOneStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelOneStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelTwoStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelTwoStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelThreeStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelThreeStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelFourStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelFourStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LatestAnalytics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatestAnalyticsDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIs_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPIs_EnumTypes_StatusEnumId",
                        column: x => x.StatusEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KPIs_StrategicObjectives_LevelThreeStrategicObjectiveId",
                        column: x => x.LevelThreeStrategicObjectiveId,
                        principalTable: "StrategicObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analytics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyzedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AnalyzedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitiativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KPIName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analytics_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Analytics_KPIs_KPIId",
                        column: x => x.KPIId,
                        principalTable: "KPIs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KPIValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementPeriod = table.Column<byte>(type: "tinyint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    TargetValue = table.Column<float>(type: "real", nullable: false),
                    ActualValue = table.Column<float>(type: "real", nullable: true),
                    PlannedFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Polarity = table.Column<byte>(type: "tinyint", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KPIName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KPIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIValues_KPIs_KPIId",
                        column: x => x.KPIId,
                        principalTable: "KPIs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_InitiativeId",
                table: "Analytics",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_KPIId",
                table: "Analytics",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeApprovedCosts_InitiativeId",
                table: "InitiativeApprovedCosts",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeBudgets_InitiativeId",
                table: "InitiativeBudgets",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeContracts_InitiativeId",
                table: "InitiativeContracts",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeContracts_StatusEnumId",
                table: "InitiativeContracts",
                column: "StatusEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeContracts_TypeEnumId",
                table: "InitiativeContracts",
                column: "TypeEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeDeliverables_InitiativeId",
                table: "InitiativeDeliverables",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeImpacts_InitiativeId",
                table: "InitiativeImpacts",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeIssues_ImpactEnumId",
                table: "InitiativeIssues",
                column: "ImpactEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeIssues_InitiativeId",
                table: "InitiativeIssues",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeIssues_ScopeEnumId",
                table: "InitiativeIssues",
                column: "ScopeEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeIssues_StatusEnumId",
                table: "InitiativeIssues",
                column: "StatusEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeMilestones_InitiativeId",
                table: "InitiativeMilestones",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_ImpactEnumId",
                table: "InitiativeRisks",
                column: "ImpactEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_InitiativeId",
                table: "InitiativeRisks",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_PriorityEnumId",
                table: "InitiativeRisks",
                column: "PriorityEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_ProbabilityEnumId",
                table: "InitiativeRisks",
                column: "ProbabilityEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_ScopeEnumId",
                table: "InitiativeRisks",
                column: "ScopeEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeRisks_TypeEnumId",
                table: "InitiativeRisks",
                column: "TypeEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeTeams_InitiativeId",
                table: "InitiativeTeams",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeTeams_RoleEnumId",
                table: "InitiativeTeams",
                column: "RoleEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_EntityId",
                table: "KPIs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_LevelThreeStrategicObjectiveId",
                table: "KPIs",
                column: "LevelThreeStrategicObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_StatusEnumId",
                table: "KPIs",
                column: "StatusEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIValues_KPIId",
                table: "KPIValues",
                column: "KPIId");

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_EnumTypes_FundStatusEnumId",
                table: "Initiatives",
                column: "FundStatusEnumId",
                principalTable: "EnumTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_EnumTypes_FundStatusEnumId",
                table: "Initiatives");

            migrationBuilder.DropTable(
                name: "Analytics");

            migrationBuilder.DropTable(
                name: "InitiativeApprovedCosts");

            migrationBuilder.DropTable(
                name: "InitiativeBudgets");

            migrationBuilder.DropTable(
                name: "InitiativeContractExpenditures");

            migrationBuilder.DropTable(
                name: "InitiativeContracts");

            migrationBuilder.DropTable(
                name: "InitiativeDeliverables");

            migrationBuilder.DropTable(
                name: "InitiativeImpacts");

            migrationBuilder.DropTable(
                name: "InitiativeIssues");

            migrationBuilder.DropTable(
                name: "InitiativeMilestones");

            migrationBuilder.DropTable(
                name: "InitiativeRisks");

            migrationBuilder.DropTable(
                name: "InitiativeTeams");

            migrationBuilder.DropTable(
                name: "KPIValues");

            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "StrategicObjectives");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "ApprovedCost",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "CurrentYearBudget",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "LatestAnalytics",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "LatestAnalyticsDate",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "TotalBudget",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "EnumTypes");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Entities");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "StrategicObjectives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "StrategicObjectives",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<Guid>(
                name: "FundStatusEnumId",
                table: "Initiatives",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_EnumTypes_FundStatusEnumId",
                table: "Initiatives",
                column: "FundStatusEnumId",
                principalTable: "EnumTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
