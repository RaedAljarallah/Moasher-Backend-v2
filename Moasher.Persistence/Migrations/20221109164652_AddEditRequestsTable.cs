using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddEditRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "EnumTypes");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Analytics");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "InitiativeProjectProgress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "InitiativeProjectProgress",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModified",
                table: "InitiativeProjectProgress",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "InitiativeProjectProgress",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "InitiativeProjectProgress");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "InitiativeProjectProgress");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "StrategicObjectives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Programs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Portfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "KPIs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Initiatives",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Analytics",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
