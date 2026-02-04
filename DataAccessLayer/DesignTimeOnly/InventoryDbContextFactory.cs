using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();

        var dbFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "InventorySales");
        var dbPath = Path.Combine(dbFolder, "inventory.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new InventoryDbContext(optionsBuilder.Options);
    }
}