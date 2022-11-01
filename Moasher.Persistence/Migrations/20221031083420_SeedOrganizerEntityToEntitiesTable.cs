using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class SeedOrganizerEntityToEntitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "Approved", "Code", "CreatedAt", "CreatedBy", "IsOrganizer", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { new Guid("28499b81-7009-49ad-93d2-b87ba1bf08da"), false, "VRO", new DateTimeOffset(new DateTime(2022, 10, 31, 11, 34, 20, 290, DateTimeKind.Unspecified).AddTicks(2202), new TimeSpan(0, 0, 0, 0, 0)), "System", true, null, null, "مكتب تحقيق الرؤية" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("28499b81-7009-49ad-93d2-b87ba1bf08da"));
        }
    }
}
