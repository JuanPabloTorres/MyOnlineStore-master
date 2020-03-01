using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineStore.MobileAppService.Migrations
{
    public partial class _2020_01_03_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TableName = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    KeyValues = table.Column<string>(nullable: false),
                    OldValues = table.Column<string>(nullable: false),
                    NewValues = table.Column<string>(nullable: false),
                    WhoMadeIt = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayOfTheWeek",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfTheWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    Longitude = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    NameOfStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.NameOfStatus);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MyProductId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Percent = table.Column<double>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    BuyQuantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    BuyOne = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    ConnectionID = table.Column<string>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.ConnectionID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserReciverId = table.Column<Guid>(nullable: false),
                    UserSenderId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    SenderFullName = table.Column<string>(nullable: false),
                    SenderStoreName = table.Column<string>(nullable: false),
                    RequestAnwser = table.Column<int>(nullable: false),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StoreName = table.Column<string>(nullable: false),
                    StoreOwnerName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Logo = table.Column<byte[]>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true),
                    OwnerUserId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Exp = table.Column<DateTime>(nullable: false),
                    Pin = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    CardHolderName = table.Column<string>(nullable: false),
                    SecurityCode = table.Column<string>(nullable: false),
                    MyUserId = table.Column<Guid>(nullable: false),
                    MyStoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardAccounts_Stores_MyStoreId",
                        column: x => x.MyStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardAccounts_Users_MyUserId",
                        column: x => x.MyUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientsFavoriteStores",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsFavoriteStores", x => new { x.Id, x.ClientId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_ClientsFavoriteStores_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsFavoriteStores_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FeaturedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Image = table.Column<byte[]>(nullable: false),
                    MyStoreId = table.Column<Guid>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    DisplayPosition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedItems_Stores_MyStoreId",
                        column: x => x.MyStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MyStoreId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CardId = table.Column<Guid>(nullable: true),
                    PayedWithCash = table.Column<bool>(nullable: false),
                    MyClientId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_MyClientId",
                        column: x => x.MyClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_MyStoreId",
                        column: x => x.MyStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_Status",
                        column: x => x.Status,
                        principalTable: "OrderStatuses",
                        principalColumn: "NameOfStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    RealPrice = table.Column<double>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    Quantity = table.Column<long>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    MyStoreId = table.Column<Guid>(nullable: false),
                    OfferId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItems_Stores_MyStoreId",
                        column: x => x.MyStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductItems_StoreOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "StoreOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoresEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserEmployeeId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    EmployeePosition = table.Column<int>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoresEmployees_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoresEmployees_Users_UserEmployeeId",
                        column: x => x.UserEmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkingHourOwnerId = table.Column<Guid>(nullable: false),
                    DayId = table.Column<Guid>(nullable: false),
                    From = table.Column<TimeSpan>(nullable: false),
                    To = table.Column<TimeSpan>(nullable: false),
                    AllDay = table.Column<bool>(nullable: false),
                    NoWork = table.Column<bool>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHour_DayOfTheWeek_DayId",
                        column: x => x.DayId,
                        principalTable: "DayOfTheWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingHour_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProductItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductItemId = table.Column<Guid>(nullable: false),
                    QuantityOfItem = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProductItems", x => new { x.ProductItemId, x.OrderId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_OrdersProductItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProductItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWorkHour",
                columns: table => new
                {
                    WorkHourId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    Day = table.Column<string>(nullable: false),
                    StoresEmployeesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkHour", x => x.WorkHourId);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkHour_StoresEmployees_StoresEmployeesId",
                        column: x => x.StoresEmployeesId,
                        principalTable: "StoresEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                column: "NameOfStatus",
                values: new object[]
                {
                    "Pending",
                    "Completed"
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsAlive", "Name" },
                values: new object[,]
                {
                    { new Guid("7f76d5b5-22cc-4630-9443-0b0b0f16ba8b"), "6ab3a912-309f-472c-b499-87694b2eabb0", true, "Admin" },
                    { new Guid("d0adbaf9-05d3-4bfb-911a-98d11c977c0a"), "31f9390f-b18b-43ef-8843-59b806305ea2", true, "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardAccounts_MyStoreId",
                table: "CardAccounts",
                column: "MyStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CardAccounts_MyUserId",
                table: "CardAccounts",
                column: "MyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsFavoriteStores_ClientId",
                table: "ClientsFavoriteStores",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsFavoriteStores_StoreId",
                table: "ClientsFavoriteStores",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkHour_StoresEmployeesId",
                table: "EmployeeWorkHour",
                column: "StoresEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedItems_MyStoreId",
                table: "FeaturedItems",
                column: "MyStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequest_UserId",
                table: "JobRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MyClientId",
                table: "Orders",
                column: "MyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MyStoreId",
                table: "Orders",
                column: "MyStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProductItems_OrderId",
                table: "OrdersProductItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_MyStoreId",
                table: "ProductItems",
                column: "MyStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_OfferId",
                table: "ProductItems",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_LocationId",
                table: "Stores",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_OwnerUserId",
                table: "Stores",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresEmployees_StoreId",
                table: "StoresEmployees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresEmployees_UserEmployeeId",
                table: "StoresEmployees",
                column: "UserEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_StoreId",
                table: "UsersRoles",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_UserId",
                table: "UsersRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHour_DayId",
                table: "WorkingHour",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHour_StoreId",
                table: "WorkingHour",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "CardAccounts");

            migrationBuilder.DropTable(
                name: "ClientsFavoriteStores");

            migrationBuilder.DropTable(
                name: "EmployeeWorkHour");

            migrationBuilder.DropTable(
                name: "FeaturedItems");

            migrationBuilder.DropTable(
                name: "JobRequest");

            migrationBuilder.DropTable(
                name: "OrdersProductItems");

            migrationBuilder.DropTable(
                name: "UserConnections");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "WorkingHour");

            migrationBuilder.DropTable(
                name: "StoresEmployees");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DayOfTheWeek");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "StoreOffers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
