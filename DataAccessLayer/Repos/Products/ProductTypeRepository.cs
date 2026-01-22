using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos.Products
{
    public class ProductTypeRepository : Repository<ProductType>,IProductTypeRepository
    {

        public ProductTypeRepository(InventoryDbContext context):base(context)
        {
            
        }

        public async Task<List<ProductType>> GetAllByProductTypeNameAsync(int PageNumber,int RowsPerPage,string ProductTypeName)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
            {
                return new List<ProductType>();
            }

            ProductTypeName = ProductTypeName.Trim();

            return 
                await _context.ProductTypes
                .Where(p => EF.Functions.Like(
                    p.ProductTypeName,
                    $@"{ProductTypeName}%"))
                .OrderBy(p => p.ProductTypeId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByProductTypeNameAsync(string Name, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return 0;

            Name = Name.Trim();

            int totalCount = 
                await 
                _context.ProductTypes
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(p.ProductTypeName, $"{Name}%"))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        
    }
}
