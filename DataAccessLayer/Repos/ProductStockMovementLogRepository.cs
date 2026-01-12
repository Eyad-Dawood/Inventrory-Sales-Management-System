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
    public class ProductStockMovementLogRepository : Repository<ProductStockMovementLog>, IProductStockMovementLogRepository
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

        public int GetTotalPagesByFullNameAndDate(
            int rowsPerPage,
            string? productFullName,
            DateTime? date)
        {
            var query = _context.productStockMovmentsLog
                .AsNoTracking()
                .AsQueryable();

            // ===== Filter by Date =====
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = fromDate.AddDays(1);

                query = query.Where(p =>
                    p.LogDate >= fromDate &&
                    p.LogDate < toDate);
            }

            // ===== Filter by Product Name / Type =====
            if (!string.IsNullOrWhiteSpace(productFullName))
            {
                query = query.Where(p =>
                    p.Product.ProductName.Contains(productFullName) ||
                    p.Product.ProductType.ProductTypeName.Contains(productFullName));
            }

            int totalRecords = query.Count();

            return (int)Math.Ceiling(totalRecords / (double)rowsPerPage);
        }



        public List<ProductStockMovementLog> GetAllByProductFullNameAndDate(
            int pageNumber,
            int rowsPerPage,
            string? productFullName,
            DateTime? date)
        {
            var query = _context.productStockMovmentsLog
                .Include(p => p.User)
                .Include(p => p.Product)
                    .ThenInclude(p2 => p2.ProductType)
                .AsNoTracking()
                .AsQueryable();

            // ===== Filter by Date (Day only) =====
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = fromDate.AddDays(1);

                query = query.Where(p =>
                    p.LogDate >= fromDate &&
                    p.LogDate < toDate);
            }

            // ===== Filter by Product Name or Type =====
            if (!string.IsNullOrWhiteSpace(productFullName))
            {
                query = query.Where(p =>
                    p.Product.ProductName.Contains(productFullName) ||
                    p.Product.ProductType.ProductTypeName.Contains(productFullName));
            }

            return query
                .OrderByDescending(p => p.LogDate)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();
        }

    }
}
