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
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllWithProductType_And_UnitAsync(int PageNumber, int RowsPerPage)
        {
            return
                await
                _context
                .Products
                .Include(p => p.ProductType)
                .OrderBy(p => p.ProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllByProductTypeNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName, bool? ActivationState)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
            {
                return new List<Product>();
            }

            ProductTypeName = ProductTypeName.Trim();

            return
                await _context.Products
                    .AsNoTracking()
                    .Include(p => p.ProductType)
                    .Where(p =>
                        EF.Functions.Like(
                            p.ProductType.ProductTypeName,
                            $"{ProductTypeName}%")
                        && (ActivationState == null || p.IsAvailable == ActivationState)
                    )
                    .OrderBy(p => p.ProductId)
                    .Skip((PageNumber - 1) * RowsPerPage)
                    .Take(RowsPerPage)
                    .ToListAsync();
        }

        public async Task<List<Product>> GetAllByFullNameAsync(
        int pageNumber,
        int rowsPerPage,
        string ProductTypeName,
        string ProductName,
        bool? ActivationState)
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
                .Where(p => p.ProductType.ProductTypeName.
                StartsWith(ProductTypeName)
                &&
                 p.ProductName.StartsWith(ProductName)
                 && (ActivationState == null || p.IsAvailable == ActivationState))
                .OrderBy(c => c.ProductId)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByFullNameAsync(string ProductTypeName,string ProductName, int rowsPerPage, bool? ActivationState)
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
                 p.ProductName.StartsWith(ProductName)
                 && (ActivationState == null || p.IsAvailable == ActivationState))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)rowsPerPage);
        }


        public async Task<int> GetTotalPagesByProductTypeNameAsync(string ProductTypeName, int RowsPerPage, bool? ActivationState)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
                return 0;

            ProductTypeName = ProductTypeName.Trim();

            int totalCount =
                await
                _context.Products
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(p.ProductType.ProductTypeName, $"{ProductTypeName}%")
                    && (ActivationState == null || p.IsAvailable == ActivationState))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<int> Ids)
        {
            return await _context.Products
                .Include(p=>p.ProductType)
                .Where(p => Ids.Contains(p.ProductId))
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByActivationState(bool? ActivationState, int RowsPerPage)
        {
            int totalCount =
                await
                _context.Products
                .AsNoTracking()
                .Where(p =>
                     (ActivationState == null || p.IsAvailable == ActivationState))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<List<Product>> GetAllByActivationStateAsync(int PageNumber, int RowsPerPage, bool? ActivationState)
        {
            return
               await
               _context.Products
               .AsNoTracking()
               .Include(p => p.ProductType)
               .Where(p=>
                (ActivationState == null || p.IsAvailable == ActivationState))
               .OrderBy(c => c.ProductId)
               .Skip((PageNumber - 1) * RowsPerPage)
               .Take(RowsPerPage)
               .ToListAsync();
        }
    }
}
