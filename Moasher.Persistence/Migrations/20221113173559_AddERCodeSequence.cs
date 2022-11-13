using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddERCodeSequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ERCodeSequence",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "CodeInc",
                table: "EditRequests",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR dbo.ERCodeSequence");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EditRequests",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "('ER-'+right(replicate('0',(5))+CONVERT([varchar],[CodeInc]),(5)))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            
            migrationBuilder.CreateIndex(
                name: "IX_EditRequests_Code",
                table: "EditRequests",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EditRequests_Code",
                table: "EditRequests");

            migrationBuilder.DropSequence(
                name: "ERCodeSequence",
                schema: "dbo");
            
            migrationBuilder.DropColumn(
                name: "CodeInc",
                table: "EditRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComputedColumnSql: "('ER-'+right(replicate('0',(5))+CONVERT([varchar],[CodeInc]),(5)))");
        }
    }
}
