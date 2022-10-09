using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddContractedFieldToInitiativeProjectsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Contracted",
                table: "InitiativeProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CalculateAmount",
                table: "InitiativeContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpenditure",
                table: "InitiativeContracts",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contracted",
                table: "InitiativeProjects");

            migrationBuilder.DropColumn(
                name: "CalculateAmount",
                table: "InitiativeContracts");

            migrationBuilder.DropColumn(
                name: "TotalExpenditure",
                table: "InitiativeContracts");
        }
    }
}
