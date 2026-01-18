using DataAccessLayer;
using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork , IDisposable
{
    private readonly InventoryDbContext _dbContext;

    public UnitOfWork(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await _dbContext.Database.BeginTransactionAsync();

    public void Dispose () => _dbContext.Dispose();
}
