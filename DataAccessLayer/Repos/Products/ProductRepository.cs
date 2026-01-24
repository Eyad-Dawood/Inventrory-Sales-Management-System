using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities.Products;

namespace DataAccessLayer.Repos.Products
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(InventoryDbContext context) : base(context)
        {

        }

        public async Task<Product?> GetWithProductType_And_UnitByIdAsync(int ProductId)
        {
            return
                await 
                _context
                .Products
                .Where(p => p.ProductId == ProductId)
                .Include(p=>p.ProductType)
                .Include(p=>p.MasurementUnit)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllWithProductType_And_UnitAsync(int PageNumber, int RowsPerPage)
        {
            return
                await
                _context
                .Products
                .Include(p => p.ProductType)
                .Include(p => p.MasurementUnit)
                .OrderBy(p => p.ProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllByProductTypeNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
            {
                return new List<Product>();
            }

            ProductTypeName = ProductTypeName.Trim();

            return await
                _context.Products
                .AsNoTracking()
                .Include(p=>p.ProductType)
                .Include(p=>p.MasurementUnit)
                .Where(p => EF.Functions.Like(
                    p.ProductType.ProductTypeName,
                    $@"{ProductTypeName}%"))
                .OrderBy(c => c.ProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllByFullNameAsync(
        int pageNumber,
        int rowsPerPage,
        string ProductTypeName,
        string ProductName)
        {
            //Both Should Be Null TO Go Back
            if (string.IsNullOrWhiteSpace(ProductTypeName)&& string.IsNullOrWhiteSpace(ProductName))
                return new List<Product>();

            ProductName = ProductName.Trim();
            ProductTypeName = ProductTypeName.Trim();

            return
                await
                _context.Products
                .AsNoTracking()
                .Include(p => p.ProductType)
                 .Include(p=>p.MasurementUnit)
                .Where(p => p.ProductType.ProductTypeName.
                StartsWith(ProductTypeName)
                &&
                 p.ProductName.StartsWith(ProductName))
                .OrderBy(c => c.ProductId)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByFullNameAsync(string ProductTypeName,string ProductName, int rowsPerPage)
        {
            //Both Should Be Null TO Go Back
            if (string.IsNullOrWhiteSpace(ProductTypeName) && string.IsNullOrWhiteSpace(ProductName))
                return 0;


            ProductName = ProductName.Trim();
            ProductTypeName = ProductTypeName.Trim();

            int totalCount = await
                _context.Products
                .AsNoTracking()
                .Where(p => p.ProductType.ProductTypeName.
                StartsWith(ProductTypeName)
                &&
                 p.ProductName.StartsWith(ProductName))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)rowsPerPage);
        }


        public async Task<int> GetTotalPagesByProductTypeNameAsync(string ProductTypeName, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
                return 0;

            ProductTypeName = ProductTypeName.Trim();

            int totalCount =
                await
                _context.Products
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(p.ProductType.ProductTypeName, $"{ProductTypeName}%"))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<int> Ids)
        {
            return await _context.Products
                .Where(p => Ids.Contains(p.ProductId))
                .ToListAsync();
        }
    }
}
