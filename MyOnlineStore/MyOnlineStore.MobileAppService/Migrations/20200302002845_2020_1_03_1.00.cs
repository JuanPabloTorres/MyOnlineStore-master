using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineStore.MobileAppService.Migrations
{
    public partial class _2020_1_03_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("139f3f4e-600b-468d-b0b6-bde20b34bee8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4974d653-8b78-441b-99db-926a7d8031ae"));

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "ProductItems");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("08311449-2b94-44eb-9b3c-0a1b4e02002d"), "9f3eefcb-72cc-49be-aa23-64823d26c356", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("a766556d-5baf-4d36-9256-dd42d1373f4a"), "6f56917f-50d4-40d5-b61b-8af8a3bf5922", true, "Employee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("08311449-2b94-44eb-9b3c-0a1b4e02002d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a766556d-5baf-4d36-9256-dd42d1373f4a"));

            migrationBuilder.AddColumn<Guid>(
                name: "OfferId",
                table: "ProductItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("4974d653-8b78-441b-99db-926a7d8031ae"), "84b4cb05-d17f-4751-b36b-5ac759c5f685", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("139f3f4e-600b-468d-b0b6-bde20b34bee8"), "104752cf-86ea-4e0c-a66c-70d284d3fc62", true, "Employee" });
        }
    }
}
