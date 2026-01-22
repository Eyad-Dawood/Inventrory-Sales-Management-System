using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
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
        public DbSet<ProductStockMovementLog> ProductStockMovmentsLog { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<TakeBatch> TakeBatches { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }


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


            //Town
            modelBuilder.Entity<Town>()
                .HasIndex(u => u.TownName)
                .IsUnique();

            //MasurementUnit
            modelBuilder.Entity<MasurementUnit>()
                .HasIndex(u=>u.UnitName)
                .IsUnique();

            //Product Type
            modelBuilder.Entity<ProductType>()
                .HasIndex(u => u.ProductTypeName)
                .IsUnique();


            //Person
            modelBuilder.Entity<Person>()
                .Property(p => p.FullName)
                .HasComputedColumnSql($"CONCAT_WS(' ', [{nameof(Person.FirstName)}], [{nameof(Person.SecondName)}], [{nameof(Person.ThirdName)}], [{nameof(Person.FourthName)}])"
                ,stored:true);

            //For Faster Search
            modelBuilder.Entity<Person>()
               .HasIndex(p => p.FullName);


            //ProductType
            modelBuilder.Entity<ProductType>()
               .HasIndex(u => u.ProductTypeName)
               .IsUnique();

            //Product

            //For Faster Search
            modelBuilder.Entity<Product>()
               .HasIndex(p => p.ProductName);


            //Invoices

            //For Faster Search
            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.CustomerId);

                //Defualt
                modelBuilder.Entity<Invoice>()
                .Property(i => i.OpenDate)
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();

                
            //Batches

            //For Faster Search
            modelBuilder.Entity<TakeBatch>()
                .HasIndex(t => t.InvoiceId);

            modelBuilder.Entity<TakeBatch>()
            .Property(p => p.TakeDate)
            .HasDefaultValueSql("SYSDATETIME()")
            .ValueGeneratedOnAdd();

            //Sold Product

            //For Faster Search
            modelBuilder.Entity<SoldProduct>()
                .HasIndex(b => b.TakeBatchId);

            modelBuilder.Entity<SoldProduct>()
                .HasIndex(b => b.ProductId);

            //Payments

            //For Faster Search
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.InvoiceId);
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.CustomerId);
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.PaymentReason);

            //Refunds

            //For Faster Search
            modelBuilder.Entity<Refund>()
                .HasIndex(r => r.InvoiceId);
            modelBuilder.Entity<Refund>()
                .HasIndex(r => r.ProductId);





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
