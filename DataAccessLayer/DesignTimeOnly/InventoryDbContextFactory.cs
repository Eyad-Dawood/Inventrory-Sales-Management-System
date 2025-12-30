using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class InventoryDbContextFactory
    : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<InventoryDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;User Id=sa;Password=sa123456;Database=InventorySalesManagementDB;Encrypt=True;TrustServerCertificate=True;"
        );

        return new InventoryDbContext(optionsBuilder.Options);
    }
}