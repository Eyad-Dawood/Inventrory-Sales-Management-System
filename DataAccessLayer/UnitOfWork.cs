using DataAccessLayer;
using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly InventoryDbContext _dbContext;

    public UnitOfWork(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Save() => _dbContext.SaveChanges();

    public IDbContextTransaction BeginTransaction()
        => _dbContext.Database.BeginTransaction();
}
