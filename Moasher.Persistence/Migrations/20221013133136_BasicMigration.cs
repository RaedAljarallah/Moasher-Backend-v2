using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class BasicMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOrganizer = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnumTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBeDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrategicObjectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HierarchyId = table.Column<HierarchyId>(type: "hierarchyid", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategicObjectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Initiatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnifiedCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeByProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetSegment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributionOnStrategicObjective = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FundStatus_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundStatus_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundStatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlannedStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PlannedFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActualFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RequiredCost = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    CapexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    VisibleOnDashboard = table.Column<bool>(type: "bit", nullable: false),
                    CalculateStatus = table.Column<bool>(type: "bit", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelOneStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelOneStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelTwoStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelTwoStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelThreeStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelThreeStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelFourStrategicObjectiveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelFourStrategicObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlannedProgress = table.Column<float>(type: "real", nullable: true),
                    ActualProgress = table.Column<float>(type: "real", nullable: true),
                    ApprovedCost = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    CurrentYearBudget = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    TotalBudget = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    ContractsAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    TotalExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    CurrentYearExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
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
                    table.PrimaryKey("PK_Initiatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Initiatives_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Initiatives_EnumTypes_FundStatusEnumId",
                        column: x => x.FundStatusEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Initiatives_EnumTypes_StatusEnumId",
                        column: x => x.StatusEnumId,
                        principalTable: "EnumTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Initiatives_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Initiatives_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Initiatives_StrategicObjectives_LevelThreeStrategicObjectiveId",
                        column: x => x.LevelThreeStrategicObjectiveId,
                        principalTable: "StrategicObjectives",
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
                    Status_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    InitialAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
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
                name: "InitiativeContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    RefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateAmount = table.Column<bool>(type: "bit", nullable: false),
                    TotalExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    CurrentYearExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
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
                    Scope_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scope_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Impact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Impact_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Type_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Priority_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Probability_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Probability_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProbabilityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Impact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Impact_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpactEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scope_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Analytics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyzedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AnalyzedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Phase_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase_Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhaseEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Contracted = table.Column<bool>(type: "bit", nullable: false),
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
                    PlannedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ActualAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_InitiativeExpenditures_InitiativeContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "InitiativeContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeExpenditures_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InitiativeExpendituresBaseline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    InitialPlannedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeExpendituresBaseline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeExpendituresBaseline_InitiativeContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "InitiativeContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InitiativeExpendituresBaseline_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id");
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
                name: "IX_InitiativeDeliverables_InitiativeId",
                table: "InitiativeDeliverables",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpenditures_ContractId",
                table: "InitiativeExpenditures",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpenditures_ProjectId",
                table: "InitiativeExpenditures",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpendituresBaseline_ContractId",
                table: "InitiativeExpendituresBaseline",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeExpendituresBaseline_ProjectId",
                table: "InitiativeExpendituresBaseline",
                column: "ProjectId");

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
                name: "IX_Initiatives_EntityId",
                table: "Initiatives",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_FundStatusEnumId",
                table: "Initiatives",
                column: "FundStatusEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_LevelThreeStrategicObjectiveId",
                table: "Initiatives",
                column: "LevelThreeStrategicObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_PortfolioId",
                table: "Initiatives",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_ProgramId",
                table: "Initiatives",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_StatusEnumId",
                table: "Initiatives",
                column: "StatusEnumId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analytics");

            migrationBuilder.DropTable(
                name: "InitiativeApprovedCosts");

            migrationBuilder.DropTable(
                name: "InitiativeBudgets");

            migrationBuilder.DropTable(
                name: "InitiativeDeliverables");

            migrationBuilder.DropTable(
                name: "InitiativeExpenditures");

            migrationBuilder.DropTable(
                name: "InitiativeExpendituresBaseline");

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
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "InitiativeProjects");

            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InitiativeContracts");

            migrationBuilder.DropTable(
                name: "Initiatives");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "EnumTypes");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "StrategicObjectives");
        }
    }
}
