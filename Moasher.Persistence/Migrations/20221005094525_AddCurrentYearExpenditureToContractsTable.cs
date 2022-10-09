using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddCurrentYearExpenditureToContractsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentYearExpenditure",
                table: "InitiativeContracts",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentYearExpenditure",
                table: "InitiativeContracts");
        }
    }
}
