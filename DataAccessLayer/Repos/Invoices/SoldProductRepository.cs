using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.DTOS;
using DataAccessLayer.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccessLayer.Repos.Invoices
{
    public class SoldProductRepository : Repository<SoldProduct> , ISoldProductRepository
    {
        public SoldProductRepository(InventoryDbContext context) : base(context)
        {
        }

        private IQueryable<SoldProduct> ApplyFilter(
           IQueryable<SoldProduct> query,
           List<TakeBatchType> Types)
        {
            var inlineQuery = query;

            if (Types != null && Types.Any())
            {
                inlineQuery = inlineQuery.Where(s => Types.Contains(s.TakeBatch.TakeBatchType));
            }

            return inlineQuery;
        }

        public async Task<List<SoldProduct>> GetAllWithDetailsByBatchIdAsync(int PageNumber, int RowsPerPage,int BatchId,List<TakeBatchType>takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
                SoldProducts
                .AsNoTracking()
                .Include(b => b.Product)
                .ThenInclude(p => p.ProductType)
                .Include(b => b.Product);

            query = ApplyFilter(query, takeBatchTypes);

            return await
                query
                .Where(b => b.TakeBatchId == BatchId)
                .OrderByDescending(b => b.SoldProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<SoldProduct>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, List<TakeBatchType> takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
                SoldProducts
                .AsNoTracking()
                .Include(b => b.Product)
                .ThenInclude(p => p.ProductType)
                .Include(b => b.Product)
                .Include(b => b.TakeBatch);

            query = ApplyFilter(query, takeBatchTypes);

            return await
                query
                .Where(b => b.TakeBatch.InvoiceId == InvoiceId)
                .OrderByDescending(b => b.TakeBatchId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<SoldProduct>> GetAllWithDetailsByInvoiceIdAsync(int InvoiceId,List<TakeBatchType> takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
               SoldProducts
               .AsNoTracking()
               .Include(b => b.Product)
               .ThenInclude(p => p.ProductType)
               .Include(b => b.Product)
               .Include(b => b.TakeBatch);

            query = ApplyFilter(query, takeBatchTypes);

            return await
               query
               .Where(b => b.TakeBatch.InvoiceId == InvoiceId)
               .OrderByDescending(b => b.TakeBatchId)
               .ToListAsync();
        }


        public async Task<List<SoldProduct>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId, List<TakeBatchType> takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
                 SoldProducts
                 .AsNoTracking()
                 .Include(b => b.TakeBatch)
                 .ThenInclude(b => b.Invoice)
                 .ThenInclude(i => i.Customer)
                 .ThenInclude(i => i.Person)

                 .Include(b => b.TakeBatch)
                 .ThenInclude(b => b.Invoice)
                 .ThenInclude(i => i.Worker)
                 .ThenInclude(w => w.Person);

            query = ApplyFilter(query, takeBatchTypes);

            return await

                query
                 .Where(b => b.ProductId == ProductId)
                 .OrderByDescending(b => b.SoldProductId)
                 .Skip((PageNumber - 1) * RowsPerPage)
                 .Take(RowsPerPage)
                 .ToListAsync();
        }
        
        public async Task<int> GetTotalPagesByBatchIdAsync(int BatchId, int RowsPerPage, List<TakeBatchType> takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
                SoldProducts
                .AsNoTracking();

            query = ApplyFilter(query, takeBatchTypes);

            int totalCount =  await
                query
                .Where(b => b.TakeBatchId == BatchId)
                .CountAsync();


            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, List<TakeBatchType> takeBatchTypes)
        {

            IQueryable<SoldProduct> query = _context.
                           SoldProducts
                           .AsNoTracking();

            query = ApplyFilter(query, takeBatchTypes);


            int totalCount = await
                           query
                           .Where(b => b.TakeBatch.InvoiceId == InvoiceId)
                           .CountAsync();


            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage, List<TakeBatchType> takeBatchTypes)
        {
            IQueryable<SoldProduct> query = _context.
                SoldProducts
                .AsNoTracking();

            query = ApplyFilter(query, takeBatchTypes);

            int totalCount = await
                query
                .Where(b => b.ProductId == ProductId)
                .CountAsync();


            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<decimal> GetTotalQuantitySoldByProductIdAsync(int productId)
        {
            decimal totalQuantitySold = await _context.SoldProducts
                .AsNoTracking()
                .Where(sp =>
                    sp.ProductId == productId &&
                    sp.TakeBatch.Invoice.InvoiceType == InvoiceType.Sale &&
                    sp.TakeBatch.TakeBatchType == TakeBatchType.Invoice)
                .SumAsync(sp => (decimal)sp.Quantity);

            decimal totalQuantityRefunded = await _context.SoldProducts
                .AsNoTracking()
                .Where(sp =>
                    sp.ProductId == productId &&
                    sp.TakeBatch.Invoice.InvoiceType == InvoiceType.Sale &&
                    sp.TakeBatch.TakeBatchType == TakeBatchType.Refund)
                .SumAsync(sp => (decimal)sp.Quantity);
            return totalQuantitySold - totalQuantityRefunded;
        }

        public async Task<List<SoldProductForRefund>> GetAllForRefundWithDetailsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.SoldProducts
                .AsNoTracking()
                .Where(sp => sp.TakeBatch.Invoice.InvoiceId == invoiceId)
                .GroupBy(sp => new
                {
                    sp.ProductId,
                    sp.Product.ProductName,
                    sp.Product.ProductType.ProductTypeName,
                    sp.Product.IsAvailable,
                })
                .Select(g => new SoldProductForRefund
                {
                    ProductId = g.Key.ProductId,
                    ProductName = $"{g.Key.ProductName}",
                    ProductTypeName = $"{g.Key.ProductTypeName}",
                    IsAvilable = g.Key.IsAvailable,
                    Quantity = 0, // The Quantity User Has Take , Def = 0 let Ui detirmin
                    QuantityInStorage = g.Sum(x =>
                              x.TakeBatch.TakeBatchType == TakeBatchType.Invoice
                              ? x.Quantity
                               : x.TakeBatch.TakeBatchType == TakeBatchType.Refund
                                    ? -x.Quantity
                                        : 0),
                    SellingPricePerUnit = g.Where(x => x.TakeBatch.TakeBatchType == TakeBatchType.Invoice) // setting the price based on the buying price
                                   .Select(x => x.SellingPricePerUnit) 
                                   .FirstOrDefault(),
                })
               .Where(x => x.QuantityInStorage > 0)
                .ToListAsync();
        }
    }
}
