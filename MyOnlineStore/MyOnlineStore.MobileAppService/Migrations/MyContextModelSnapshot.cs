﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyOnlineStore.MobileAppService.Context;

namespace MyOnlineStore.MobileAppService.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Models.Audits.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("KeyValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhoMadeIt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Purchases.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<Guid>("MyClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MyStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PayedWithCash")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MyClientId");

                    b.HasIndex("MyStoreId");

                    b.HasIndex("Status");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Purchases.ProductItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("MyOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MyStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<double>("RealPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MyStoreId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("08311449-2b94-44eb-9b3c-0a1b4e02002d"),
                            ConcurrencyStamp = "9f3eefcb-72cc-49be-aa23-64823d26c356",
                            IsAlive = true,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("a766556d-5baf-4d36-9256-dd42d1373f4a"),
                            ConcurrencyStamp = "6f56917f-50d4-40d5-b61b-8af8a3bf5922",
                            IsAlive = true,
                            Name = "Employee"
                        });
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.ClientsFavoriteStores", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.HasKey("Id", "ClientId", "StoreId");

                    b.HasIndex("ClientId");

                    b.HasIndex("StoreId");

                    b.ToTable("ClientsFavoriteStores");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreOwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.StoreFeaturedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayPosition")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("MyStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyStoreId");

                    b.ToTable("FeaturedItems");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.StoresEmployees", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EmployeePosition")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserEmployeeId");

                    b.ToTable("StoresEmployees");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Users.CardAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Exp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MyStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MyUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyStoreId");

                    b.HasIndex("MyUserId");

                    b.ToTable("CardAccounts");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Entries.JobRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<int>("RequestAnwser")
                        .HasColumnType("int");

                    b.Property<string>("SenderFullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderStoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserReciverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserSenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("JobRequest");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Purchases.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BuyQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MyProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("OfferPrice")
                        .HasColumnType("float");

                    b.Property<double>("Percent")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyProductId")
                        .IsUnique();

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Purchases.OrderStatus", b =>
                {
                    b.Property<string>("NameOfStatus")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NameOfStatus");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            NameOfStatus = "Pending"
                        },
                        new
                        {
                            NameOfStatus = "Completed"
                        });
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Purchases.OrdersProductItems", b =>
                {
                    b.Property<Guid>("ProductItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("QuantityOfItem")
                        .HasColumnType("bigint");

                    b.HasKey("ProductItemId", "OrderId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersProductItems");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Roles.UsersRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Stores.DayOfTheWeek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DayOfTheWeek");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Stores.EmployeeWorkHour", b =>
                {
                    b.Property<Guid>("WorkHourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("End")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("Start")
                        .HasColumnType("time");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StoresEmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkHourId");

                    b.HasIndex("StoresEmployeesId");

                    b.ToTable("EmployeeWorkHour");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Stores.WorkingHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllDay")
                        .HasColumnType("bit");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("From")
                        .HasColumnType("time");

                    b.Property<bool>("NoWork")
                        .HasColumnType("bit");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("time");

                    b.Property<Guid>("WorkingHourOwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("StoreId");

                    b.ToTable("WorkingHour");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Users.UserConnection", b =>
                {
                    b.Property<string>("ConnectionID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Connected")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConnectionID");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Purchases.Order", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("MyOrders")
                        .HasForeignKey("MyClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", null)
                        .WithMany("StoreOrders")
                        .HasForeignKey("MyStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Shared.Models.Purchases.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Purchases.ProductItem", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", null)
                        .WithMany("ProductItems")
                        .HasForeignKey("MyStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.ClientsFavoriteStores", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("MyFavorites")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.Store", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("MyStores")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.StoreFeaturedItem", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", null)
                        .WithMany("FeaturedItems")
                        .HasForeignKey("MyStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Stores.StoresEmployees", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("MyWorkStores")
                        .HasForeignKey("UserEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Entities.Models.Users.CardAccount", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", null)
                        .WithMany("MyAccounts")
                        .HasForeignKey("MyStoreId");

                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("MyCardAccounts")
                        .HasForeignKey("MyUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Entries.JobRequest", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", null)
                        .WithMany("JobRequests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Purchases.Offer", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Purchases.ProductItem", null)
                        .WithOne("ProductOffer")
                        .HasForeignKey("MyOnlineStore.Shared.Models.Purchases.Offer", "MyProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Purchases.OrdersProductItems", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Purchases.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Purchases.ProductItem", "ProductItem")
                        .WithMany()
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Roles.UsersRoles", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", "Store")
                        .WithMany("Employees")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Users.User", "User")
                        .WithMany("MyRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Stores.EmployeeWorkHour", b =>
                {
                    b.HasOne("MyOnlineStore.Entities.Models.Stores.StoresEmployees", null)
                        .WithMany("EmployeeWork")
                        .HasForeignKey("StoresEmployeesId");
                });

            modelBuilder.Entity("MyOnlineStore.Shared.Models.Stores.WorkingHour", b =>
                {
                    b.HasOne("MyOnlineStore.Shared.Models.Stores.DayOfTheWeek", "Day")
                        .WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOnlineStore.Entities.Models.Stores.Store", null)
                        .WithMany("WorkingHours")
                        .HasForeignKey("StoreId");
                });
#pragma warning restore 612, 618
        }
    }
}
