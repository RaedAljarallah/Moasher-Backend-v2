using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddInitialAmoutToBudgetTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialAmount",
                table: "InitiativeBudgets",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialAmount",
                table: "InitiativeBudgets");
        }
    }
}
