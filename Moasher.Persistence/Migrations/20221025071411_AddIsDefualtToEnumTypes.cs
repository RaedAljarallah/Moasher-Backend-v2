using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddIsDefualtToEnumTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "EnumTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "EnumTypes",
                columns: new[] { "Id", "Approved", "CanBeDeleted", "Category", "CreatedAt", "CreatedBy", "IsDefault", "LastModified", "LastModifiedBy", "Metadata", "Name", "Style" },
                values: new object[] { new Guid("d0fd13dd-a1e1-4e56-9e22-9c160b1f67e3"), true, false, "InitiativeStatus", new DateTimeOffset(new DateTime(2022, 10, 25, 13, 14, 10, 623, DateTimeKind.Unspecified).AddTicks(9661), new TimeSpan(0, 3, 0, 0, 0)), "System", true, null, null, null, "لا توجد جالة", "gray-1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EnumTypes",
                keyColumn: "Id",
                keyValue: new Guid("d0fd13dd-a1e1-4e56-9e22-9c160b1f67e3"));

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "EnumTypes");
        }
    }
}
