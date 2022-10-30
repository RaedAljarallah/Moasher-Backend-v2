using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddSuspendedToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Suspended",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suspended",
                table: "Users");
        }
    }
}
