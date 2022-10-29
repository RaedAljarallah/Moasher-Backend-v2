using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddStartAndEndDateToKPIValuesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EnumTypes",
                keyColumn: "Id",
                keyValue: new Guid("358be129-37c5-402d-bbdf-dc82633415c3"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDate",
                table: "KPIs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                table: "KPIs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "KPIs");

            migrationBuilder.InsertData(
                table: "EnumTypes",
                columns: new[] { "Id", "Approved", "CanBeDeleted", "Category", "CreatedAt", "CreatedBy", "IsDefault", "LastModified", "LastModifiedBy", "LimitFrom", "LimitTo", "Name", "Style" },
                values: new object[] { new Guid("358be129-37c5-402d-bbdf-dc82633415c3"), true, false, "InitiativeStatus", new DateTimeOffset(new DateTime(2022, 10, 25, 16, 0, 44, 207, DateTimeKind.Unspecified).AddTicks(1567), new TimeSpan(0, 3, 0, 0, 0)), "System", true, null, null, null, null, "لا توجد جالة", "gray-1" });
        }
    }
}
