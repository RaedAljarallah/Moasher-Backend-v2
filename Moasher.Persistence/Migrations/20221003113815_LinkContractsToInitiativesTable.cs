using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class LinkContractsToInitiativesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ContractsAmount",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentYearExpenditure",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpenditure",
                table: "Initiatives",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractsAmount",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "CurrentYearExpenditure",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "TotalExpenditure",
                table: "Initiatives");
        }
    }
}
