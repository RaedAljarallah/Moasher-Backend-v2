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
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ERCodeSequence",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "EditRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false, computedColumnSql: "('ER-'+right(replicate('0',(5))+CONVERT([varchar],[CodeInc]),(5)))"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RequestedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    HasEvents = table.Column<bool>(type: "bit", nullable: false),
                    Events = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeInc = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ERCodeSequence")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditRequests", x => x.Id);
                });

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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    LimitFrom = table.Column<float>(type: "real", nullable: true),
                    LimitTo = table.Column<float>(type: "real", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CanBeDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvalidTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Jti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvalidTokens", x => x.Id);
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    LocalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRecords", x => x.Id);
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategicObjectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditRequestSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    EditRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditRequestSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditRequestSnapshot_EditRequests_EditRequestId",
                        column: x => x.EditRequestId,
                        principalTable: "EditRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Suspended = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveEmailNotification = table.Column<bool>(type: "bit", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Users_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FundStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundStatusStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateAmount = table.Column<bool>(type: "bit", nullable: false),
                    TotalExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    CurrentYearExpenditure = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    BalancedExpenditurePlan = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    ScopeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpactStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriorityStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriorityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProbabilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProbabilityStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProbabilityEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpactStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpactEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImpactDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
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
                    PlannedContractEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhaseStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhaseEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Contracted = table.Column<bool>(type: "bit", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false),
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
                name: "ContractMilestones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MilestoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractMilestones_InitiativeContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "InitiativeContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractMilestones_InitiativeMilestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "InitiativeMilestones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractMilestones_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id");
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
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
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
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "InitiativeProjectProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhaseStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhaseEnumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhaseStartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PhaseEndedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PhaseStartedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhaseEndedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "InitiativeProjectsBaseline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialPlannedContractingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    InitialEstimatedAmount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    HasDeleteRequest = table.Column<bool>(type: "bit", nullable: false),
                    HasUpdateRequest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeProjectsBaseline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeProjectsBaseline_InitiativeProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "InitiativeProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "IsOrganizer", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { new Guid("9faac513-cf0d-4c30-8310-be92508416ab"), "VRO", new DateTimeOffset(new DateTime(2022, 11, 29, 11, 50, 35, 464, DateTimeKind.Unspecified).AddTicks(6454), new TimeSpan(0, 0, 0, 0, 0)), "System", true, null, null, "مكتب تحقيق الرؤية" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "LocalizedName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a756d7e-586b-4793-9211-11f9203e0727"), "a1713fc2-a2d2-44d1-a629-ce50a8222eee", "مدير النظام", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("15aa2eeb-d5af-4f47-a584-1e6d2f62d48f"), "78434753-19ef-4252-8411-f227bfe6917e", "مسؤول تنفيذ", "ExecutionOperator", "EXECUTIONOPERATOR" },
                    { new Guid("520a4376-a452-4c32-a908-ba429f3a7311"), "c07b42e8-1770-45f0-af64-c3683cc6a87d", "مشرف", "Admin", "ADMIN" },
                    { new Guid("6937839e-c1ce-43b7-afc1-a9899fc8f561"), "254f0afa-2389-4687-8aa7-a7544054092a", "مدقق بيانات", "DataAssurance", "DATAASSURANCE" },
                    { new Guid("b5cc4db1-fef0-4a53-b977-4c0d080b5493"), "63d385ed-7ffa-4dad-84e7-01be2d79982e", "مستعرض جميع البيانات", "FullAccessViewer", "FULLACCESSVIEWER" },
                    { new Guid("dbbaa46a-d86a-4592-991d-dd0a1eee71f7"), "5811ca9d-e8ab-415a-8ce7-91102e52748a", "مستخدم جهة", "EntityUser", "ENTITYUSER" },
                    { new Guid("dc8085f0-1a02-48a7-a99f-f11fde69f6a1"), "505a5ad3-773e-40c8-8e2e-fa52db132eba", "مسؤول مالي", "FinancialOperator", "FINANCIALOPERATOR" },
                    { new Guid("f35ed777-29cf-41c6-8e99-76ba01178380"), "2f95dd2c-6928-409c-8ace-4269285eade0", "مسؤول مؤشرات أداء", "KPIsOperator", "KPISOPERATOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EntityId", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MustChangePassword", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ReceiveEmailNotification", "Role", "SecurityStamp", "Suspended", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("696eaaa5-530a-4b41-90b5-276df94ad086"), 0, "e2912eea-0b2b-4dd8-84f4-91c258fcb70a", "SuperAdmin@Moasher.com", true, new Guid("9faac513-cf0d-4c30-8310-be92508416ab"), "Super", "Admin", true, null, true, "SUPERADMIN@MOASHER.COM", "SUPERADMIN@MOASHER.COM", "AQAAAAEAACcQAAAAECyJ4TjFh9229Xsi1x5eagDHPRzVduoEwh2ZgzsOfB3unBz/i5WnDxiSDiLJqJ2VaQ==", "0555555555", false, true, "SUPERADMIN", "69f7341a-3857-46d7-bbdf-05bbd74fd28c", false, false, "SuperAdmin@Moasher.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("0a756d7e-586b-4793-9211-11f9203e0727"), new Guid("696eaaa5-530a-4b41-90b5-276df94ad086") });

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_InitiativeId",
                table: "Analytics",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_KPIId",
                table: "Analytics",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestones_ContractId",
                table: "ContractMilestones",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestones_MilestoneId",
                table: "ContractMilestones",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestones_ProjectId",
                table: "ContractMilestones",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EditRequests_Code",
                table: "EditRequests",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EditRequestSnapshot_EditRequestId",
                table: "EditRequestSnapshot",
                column: "EditRequestId");

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
                name: "IX_InitiativeProjectProgress_PhaseEnumId",
                table: "InitiativeProjectProgress",
                column: "PhaseEnumId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjectProgress_ProjectId",
                table: "InitiativeProjectProgress",
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

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeProjectsBaseline_ProjectId",
                table: "InitiativeProjectsBaseline",
                column: "ProjectId",
                unique: true);

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
                name: "IX_Users_EntityId",
                table: "Users",
                column: "EntityId");

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
                name: "ContractMilestones");

            migrationBuilder.DropTable(
                name: "EditRequestSnapshot");

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
                name: "InitiativeProjectProgress");

            migrationBuilder.DropTable(
                name: "InitiativeProjectsBaseline");

            migrationBuilder.DropTable(
                name: "InitiativeRisks");

            migrationBuilder.DropTable(
                name: "InitiativeTeams");

            migrationBuilder.DropTable(
                name: "InvalidTokens");

            migrationBuilder.DropTable(
                name: "KPIValues");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SearchRecords");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "InitiativeMilestones");

            migrationBuilder.DropTable(
                name: "EditRequests");

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

            migrationBuilder.DropSequence(
                name: "ERCodeSequence",
                schema: "dbo");
        }
    }
}
