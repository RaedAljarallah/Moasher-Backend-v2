using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moasher.Persistence.Migrations
{
    public partial class AddLocalizedNameToRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("069730c0-2dfa-4041-bb53-b0b1e253c798"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11d1c3b2-b8d2-474b-bb31-c5c5c8ed14cb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7c2a62e5-1aa7-4b94-b15f-ea0acc48be32"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a8aa5387-28ef-4ed1-88f4-60806e0fd9d6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cdf48cb9-7c3e-470a-9aff-c3738fa5148a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e520a586-07df-4d31-8861-52aaecbde1f8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4c3ec3a-01bf-410a-ac90-9789a4260a47"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f7d2c15b-c82b-4fe3-9c53-114833e3010b"));

            migrationBuilder.AddColumn<string>(
                name: "LocalizedName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "LocalizedName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("22d4796d-2153-4337-8eea-945501e0052a"), "358f0d50-60c8-451b-b8f1-43b5e8c3b87b", "مدقق بيانات", "DataAssurance", "DATAASSURANCE" },
                    { new Guid("28e41dd5-a5ec-4771-80df-40fc7a47f1dc"), "e41367bc-2dec-400f-a596-a3f335ef3762", "مشرف", "Admin", "ADMIN" },
                    { new Guid("45019006-2a1c-4317-9ad1-975e0d3f649f"), "882d959c-feb4-48fb-981e-f45ac2c5496b", "مسؤول تنفيذ", "ExecutionOperator", "EXECUTIONOPERATOR" },
                    { new Guid("67b1e096-8a63-4558-b745-c6667286f2ea"), "6c33e3f0-2baa-4ecf-a021-1d9812395db3", "مستعرض جميع البيانات", "FullAccessViewer", "FULLACCESSVIEWER" },
                    { new Guid("6889bb7f-0978-4973-97b0-26d07388ab0b"), "4d83c096-b0ae-4d47-bccd-11f3f458878b", "مدير النظام", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("7376a0fa-462b-467d-8acf-05a798e3611a"), "95681612-27ce-4f9e-8f00-393545d112a3", "مسؤول مالي", "FinancialOperator", "FINANCIALOPERATOR" },
                    { new Guid("caabd6ac-68b4-4cd2-b8b7-9730890af912"), "e46cf33e-a8d8-40a4-98cd-6a1b9c8777c6", "مستخدم جهة", "EntityUser", "ENTITYUSER" },
                    { new Guid("ff555378-83d8-42f0-9936-b20e89e6fbeb"), "29012076-53ed-4c85-bfbe-9c41c3101e96", "مسؤول مؤشرات أداء", "KPIsOperator", "KPISOPERATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22d4796d-2153-4337-8eea-945501e0052a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28e41dd5-a5ec-4771-80df-40fc7a47f1dc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45019006-2a1c-4317-9ad1-975e0d3f649f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("67b1e096-8a63-4558-b745-c6667286f2ea"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6889bb7f-0978-4973-97b0-26d07388ab0b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7376a0fa-462b-467d-8acf-05a798e3611a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("caabd6ac-68b4-4cd2-b8b7-9730890af912"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ff555378-83d8-42f0-9936-b20e89e6fbeb"));

            migrationBuilder.DropColumn(
                name: "LocalizedName",
                table: "Roles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("069730c0-2dfa-4041-bb53-b0b1e253c798"), "649dd7c1-a0cc-4e47-92eb-d32acaf53ec7", "ExecutionOperator", "EXECUTIONOPERATOR" },
                    { new Guid("11d1c3b2-b8d2-474b-bb31-c5c5c8ed14cb"), "b564ab93-8152-4bd5-9885-884716e94ab0", "Admin", "ADMIN" },
                    { new Guid("7c2a62e5-1aa7-4b94-b15f-ea0acc48be32"), "a9fbdeac-df51-4d0f-b104-fc6ea00217f3", "KPIsOperator", "KPISOPERATOR" },
                    { new Guid("a8aa5387-28ef-4ed1-88f4-60806e0fd9d6"), "179d48c8-a128-4010-92d5-0c4314d72993", "FullAccessViewer", "FULLACCESSVIEWER" },
                    { new Guid("cdf48cb9-7c3e-470a-9aff-c3738fa5148a"), "b2e1487e-dd30-4cee-b1f3-341a3dd54a83", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("e520a586-07df-4d31-8861-52aaecbde1f8"), "9d436616-0aba-4a02-9a44-5fa3960678c4", "DataAssurance", "DATAASSURANCE" },
                    { new Guid("f4c3ec3a-01bf-410a-ac90-9789a4260a47"), "adcfdf3c-5816-452e-8952-79f12e44e107", "FinancialOperator", "FINANCIALOPERATOR" },
                    { new Guid("f7d2c15b-c82b-4fe3-9c53-114833e3010b"), "9fe3b6b7-c841-43f5-a84e-0fd609b30fea", "EntityUser", "ENTITYUSER" }
                });
        }
    }
}
