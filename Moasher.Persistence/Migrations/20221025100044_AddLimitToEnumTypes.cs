using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddLimitToEnumTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EnumTypes",
                keyColumn: "Id",
                keyValue: new Guid("d0fd13dd-a1e1-4e56-9e22-9c160b1f67e3"));

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "EnumTypes");

            migrationBuilder.AddColumn<float>(
                name: "LimitFrom",
                table: "EnumTypes",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LimitTo",
                table: "EnumTypes",
                type: "real",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EnumTypes",
                columns: new[] { "Id", "Approved", "CanBeDeleted", "Category", "CreatedAt", "CreatedBy", "IsDefault", "LastModified", "LastModifiedBy", "LimitFrom", "LimitTo", "Name", "Style" },
                values: new object[] { new Guid("358be129-37c5-402d-bbdf-dc82633415c3"), true, false, "InitiativeStatus", new DateTimeOffset(new DateTime(2022, 10, 25, 16, 0, 44, 207, DateTimeKind.Unspecified).AddTicks(1567), new TimeSpan(0, 3, 0, 0, 0)), "System", true, null, null, null, null, "لا توجد جالة", "gray-1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EnumTypes",
                keyColumn: "Id",
                keyValue: new Guid("358be129-37c5-402d-bbdf-dc82633415c3"));

            migrationBuilder.DropColumn(
                name: "LimitFrom",
                table: "EnumTypes");

            migrationBuilder.DropColumn(
                name: "LimitTo",
                table: "EnumTypes");

            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "EnumTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EnumTypes",
                columns: new[] { "Id", "Approved", "CanBeDeleted", "Category", "CreatedAt", "CreatedBy", "IsDefault", "LastModified", "LastModifiedBy", "Metadata", "Name", "Style" },
                values: new object[] { new Guid("d0fd13dd-a1e1-4e56-9e22-9c160b1f67e3"), true, false, "InitiativeStatus", new DateTimeOffset(new DateTime(2022, 10, 25, 13, 14, 10, 623, DateTimeKind.Unspecified).AddTicks(9661), new TimeSpan(0, 3, 0, 0, 0)), "System", true, null, null, null, "لا توجد جالة", "gray-1" });
        }
    }
}
