using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class ProductPriceLogRepository : Repository<ProductPriceLog>,IProductPriceLogRepository
    {
        public ProductPriceLogRepository(InventoryDbContext context) : base(context)
        {
        }

        public List<ProductPriceLog> GetAllWithDetails(int PageNumber, int RowsPerPage)
        {
            return _context
                .ProductPricesLog
                .Include(p=>p.Product)
                .ThenInclude(p2 => p2.ProductType)
                .Include(p=>p.User)
                .OrderByDescending(p=>p.LogDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
    }
}
