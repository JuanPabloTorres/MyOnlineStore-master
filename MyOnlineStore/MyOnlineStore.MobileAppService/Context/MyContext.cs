using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Models.Models;
using MyOnlineStore.Entities.Models.Models.Audits;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Entries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Shared.Models.Stores;

namespace MyOnlineStore.MobileAppService.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Audit> Audits { get;private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<UsersRoles> UsersRoles { get; private set; }
        public DbSet<ClientsFavoriteStores> ClientsFavoriteStores { get; private set; }
        public DbSet<Location> Locations { get; private set; }
        public DbSet<Store> Stores { get; private set; }
        public DbSet<ProductItem> ProductItems { get; private set; }
        public DbSet<OrdersProductItems> OrdersProductItems { get; private set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<StoreFeaturedItem> FeaturedItems { get; private set; }
        public DbSet<OrderStatus> OrderStatuses { get; private set; }
        public DbSet<CardAccount> CardAccounts { get; private set; }

        public DbSet<Offer> Offers { get; private set; }

        public DbSet<StoresEmployees> Employees { get; private set; }

        public DbSet<JobRequest> JobRequest { get; private set; }

        public DbSet<UserConnection> UserConnections { get; private set; }

        public DbSet<EmployeeWorkHour>  EmployeeWorkHours { get; private set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public MyContext() { }
        
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        
        {
            modelBuilder.Entity<Audit>();

          

            //------------------------ Store Employee ------------------------
            //modelBuilder
            //    .Entity<StoresEmployees>()
            //    .HasKey(se => new { se.StoreId, se.UserId });
            //modelBuilder
            //    .Entity<StoresEmployees>()
            //    .HasOne(se => se.EmployeeUser)
            //    .WithMany(se => se.MyWorkStore)
            //    .HasForeignKey(se => se.UserId);
            //modelBuilder
            //    .Entity<StoresEmployees>()
            //    .HasOne(se => se.Store);

            //------------------------ Favorite Stores Clients ------------------------
            modelBuilder
                .Entity<ClientsFavoriteStores>()
                .HasKey(fs => new {fs.Id ,fs.ClientId, fs.StoreId });
           

            //------------------------ Orders Products Items ------------------------
            modelBuilder.Entity<OrdersProductItems>()
                .HasKey(o => new { o.ProductItemId, o.OrderId })
                .IsClustered(true);
            modelBuilder
                .Entity<OrdersProductItems>();

            //------------------------ Orders ------------------------
            modelBuilder.Entity<Order>()
                 .HasMany(o => o.OrderItems)
                 .WithOne()
                 .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
              .HasOne(o => o.OrderStatus)
              .WithMany()
              .HasForeignKey(o=>o.Status);

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
               .Property(o => o.MyClientId)
               .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
              .Property(o => o.MyStoreId)
              .ValueGeneratedNever();

            //------------------------ Stores ------------------------
            modelBuilder
                .Entity<Store>()
                .HasMany(s => s.ProductItems)
                .WithOne()
                .HasForeignKey(s=>s.MyStoreId);

            modelBuilder
               .Entity<Store>()
               .Property(s => s.Id)
               .ValueGeneratedNever();
            modelBuilder.Entity<Store>()
                .HasMany(s => s.FeaturedItems)
                .WithOne()
                .HasForeignKey(s=>s.MyStoreId);

            modelBuilder.Entity<Store>()
                 .HasMany(s => s.StoreOrders)
                 .WithOne()
                 .HasForeignKey(s=>s.MyStoreId);

            modelBuilder.Entity<Store>()
                .HasMany(s => s.WorkingHours)
                .WithOne();

            modelBuilder.Entity<Store>()
                .HasMany(_ => _.Employees)
                .WithOne(_ => _.Store)
                .HasForeignKey(_ => _.StoreId);

            modelBuilder.Entity<Store>()
                .HasMany(_ => _.MyAccounts)
                .WithOne()
                .HasForeignKey(_ => _.MyStoreId);

           

            //------------------------ Users ------------------------

            modelBuilder
              .Entity<User>()
              .Property(k => k.Id)
              .ValueGeneratedNever();

            modelBuilder
                .Entity<User>()
                .HasMany(eu => eu.MyCardAccounts)
                .WithOne()
                .HasForeignKey(u => u.MyUserId);

            modelBuilder
                .Entity<User>()
                .HasMany(eu => eu.MyStores)
                .WithOne()
                .HasForeignKey(u => u.OwnerUserId);

            modelBuilder
                .Entity<User>()
                .HasMany(eu => eu.MyRoles)
                .WithOne(u=>u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder
               .Entity<User>()
               .HasMany(eu => eu.MyFavorites)
               .WithOne()
               .HasForeignKey(u => u.ClientId);

            modelBuilder
               .Entity<User>()
               .HasMany(eu => eu.MyWorkStores)
               .WithOne()
               .HasForeignKey(u => u.UserEmployeeId);

            modelBuilder
              .Entity<User>()
              .HasMany(eu => eu.MyOrders)
              .WithOne()
              .HasForeignKey(u => u.MyClientId);

            //modelBuilder
            // .Entity<User>()
            // .HasMany(eu => eu.Connections)
            // .WithOne()
            // .HasForeignKey(u => u.ConnectionID);


            //------------------------ Roles ------------------------
            modelBuilder.Entity<Role>()
                .Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .Property(_ => _.ConcurrencyStamp)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        IsAlive = true,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Employee",
                        IsAlive = true,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                );

            //------------------------ Store Featured Items ------------------------
            modelBuilder.Entity<StoreFeaturedItem>()
                .Property(sfi => sfi.Id);

            //------------------------ Order Status ------------------------
            modelBuilder.Entity<OrderStatus>()
                .HasData(
                    OrderStatus.Pending,
                    OrderStatus.Completed
                );

            //------------------------ Job Request ------------------------

            //modelBuilder.Entity<JobRequest>()
            //    .HasOne(_ => _.UserSender)
            //    .WithMany(_ => _.JobRequests)
            //    .HasForeignKey(_ => _.UserSenderId);


            //--------------------Offers----------------------
            modelBuilder.Entity<ProductItem>()
               .HasOne(_ => _.ProductOffer)
               .WithOne()
               .HasForeignKey<Offer>(_ => _.MyProductId);

            //modelBuilder.Entity<Offer>()
            //    .HasOne(_ => _.Product);
            //   .WithOne(_ => _.Product);

            //modelBuilder.Entity<ProductItem>().HasOne(_ => _.ProductOffer);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result  = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                 
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName()// Relational().TableName
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }
    }
}
