using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class ProductStockMovementLogRepository : Repository<ProductStockMovementLog> , IProductStockMovementLogRepository
    {
        public ProductStockMovementLogRepository(InventoryDbContext context) : base(context)
        {
        }

        public List<ProductStockMovementLog> GetAllWithDetails(int PageNumber, int RowsPerPage)
        {
            return _context
                .productStockMovmentsLog
                .Include(p => p.User)
                .Include(p => p.Product)
                .ThenInclude(p2 => p2.ProductType)
                .OrderByDescending(p => p.LogDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
    }
}
