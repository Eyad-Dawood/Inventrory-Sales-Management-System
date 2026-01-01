using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class InventoryDbContext : DbContext
    {

        public DbSet<Person>People { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Worker>Workers { get; set; }
        public DbSet<User>Users { get; set; }
        public DbSet<MasurementUnit> MasurementUnits { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPriceLog> ProductPricesLog { get; set; }
        public DbSet<ProductStockMovementLog> productStockMovmentsLog { get; set; }

        public InventoryDbContext(
         DbContextOptions<InventoryDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Worker
            modelBuilder.Entity<Worker>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();


            //Customer
            modelBuilder.Entity<Customer>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(u => u.Balance)
                .HasDefaultValue(0)
                .ValueGeneratedOnAdd();


            //Product Price Log 
            modelBuilder.Entity<ProductPriceLog>()
            .Property(p => p.LogDate)
            .HasDefaultValueSql("SYSDATETIME()")
            .ValueGeneratedOnAdd();


            //Product Stock Movement Log
            modelBuilder.Entity<ProductStockMovementLog>()
            .Property(p => p.LogDate)
            .HasDefaultValueSql("SYSDATETIME()")
            .ValueGeneratedOnAdd();


            foreach (var foreignKey in modelBuilder.Model
                       .GetEntityTypes()
                       .SelectMany(e => e.GetForeignKeys()))
            {
                if(foreignKey.DeleteBehavior!=DeleteBehavior.Restrict)
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}
