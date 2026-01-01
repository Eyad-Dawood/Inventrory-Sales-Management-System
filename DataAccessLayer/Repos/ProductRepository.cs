using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities.Products;

namespace DataAccessLayer.Repos
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(InventoryDbContext context) : base(context)
        {

        }

        public Product GetWithProductType_And_UnitById(int ProductId)
        {
            return _context
                .Products
                .Where(p => p.ProductId == ProductId)
                .Include(p=>p.ProductType)
                .Include(p=>p.MasurementUnit)
                .FirstOrDefault();
        }

        public List<Product> GetAllWithProductType_And_Unit(int PageNumber, int RowsPerPage)
        {
            return _context
                .Products
                .Include(p => p.ProductType)
                .Include(p => p.MasurementUnit)
                .OrderBy(p => p.ProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
    }
}
