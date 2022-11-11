using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class UpdateEditRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandName",
                table: "EditRequests");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "EditRequests");

            migrationBuilder.RenameColumn(
                name: "OriginalValues",
                table: "EditRequests",
                newName: "Events");

            migrationBuilder.RenameColumn(
                name: "CurrentValues",
                table: "EditRequests",
                newName: "EventArguments");

            migrationBuilder.AddColumn<string>(
                name: "EventArgumentTypes",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasEvents",
                table: "EditRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EditRequestSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
            
            migrationBuilder.CreateIndex(
                name: "IX_EditRequestSnapshot_EditRequestId",
                table: "EditRequestSnapshot",
                column: "EditRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditRequestSnapshot");
            
            migrationBuilder.DropColumn(
                name: "EventArgumentTypes",
                table: "EditRequests");

            migrationBuilder.DropColumn(
                name: "HasEvents",
                table: "EditRequests");

            migrationBuilder.RenameColumn(
                name: "Events",
                table: "EditRequests",
                newName: "OriginalValues");

            migrationBuilder.RenameColumn(
                name: "EventArguments",
                table: "EditRequests",
                newName: "CurrentValues");

            migrationBuilder.AddColumn<string>(
                name: "CommandName",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
