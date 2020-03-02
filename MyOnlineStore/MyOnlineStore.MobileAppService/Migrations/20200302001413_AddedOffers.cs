using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineStore.MobileAppService.Migrations
{
    public partial class AddedOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Offers_OfferId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_OfferId",
                table: "ProductItems");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("56f87f8b-cc8d-4199-8dfd-1d5ac2aff712"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("91f3c156-8313-4d37-aadc-7da37ed229b3"));

            migrationBuilder.DropColumn(
                name: "BuyOne",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Offers");

            migrationBuilder.AddColumn<Guid>(
                name: "MyOfferId",
                table: "ProductItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "OfferPrice",
                table: "Offers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("4974d653-8b78-441b-99db-926a7d8031ae"), "84b4cb05-d17f-4751-b36b-5ac759c5f685", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("139f3f4e-600b-468d-b0b6-bde20b34bee8"), "104752cf-86ea-4e0c-a66c-70d284d3fc62", true, "Employee" });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MyProductId",
                table: "Offers",
                column: "MyProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_ProductItems_MyProductId",
                table: "Offers",
                column: "MyProductId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_ProductItems_MyProductId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_MyProductId",
                table: "Offers");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("139f3f4e-600b-468d-b0b6-bde20b34bee8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4974d653-8b78-441b-99db-926a7d8031ae"));

            migrationBuilder.DropColumn(
                name: "MyOfferId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "Offers");

            migrationBuilder.AddColumn<double>(
                name: "BuyOne",
                table: "Offers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Offers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("91f3c156-8313-4d37-aadc-7da37ed229b3"), "45f4ce28-2cd5-4b4c-985a-a9adb1f0cac3", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[] { new Guid("56f87f8b-cc8d-4199-8dfd-1d5ac2aff712"), "1092cc0c-d94d-4d87-9a29-3311b688d9b3", true, "Employee" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_OfferId",
                table: "ProductItems",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Offers_OfferId",
                table: "ProductItems",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
