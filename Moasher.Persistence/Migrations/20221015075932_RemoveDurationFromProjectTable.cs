using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class RemoveDurationFromProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "InitiativeProjects");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PlannedContractEndDate",
                table: "InitiativeProjects",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedContractEndDate",
                table: "InitiativeProjects");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "InitiativeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
