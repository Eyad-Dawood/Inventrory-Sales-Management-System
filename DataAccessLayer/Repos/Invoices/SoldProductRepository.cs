using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos.Invoices
{
    public class SoldProductRepository : Repository<SoldProduct> , ISoldProductRepository
    {
        public SoldProductRepository(InventoryDbContext context) : base(context)
        {
        }    

        public async Task<List<SoldProduct>> GetAllWithDetailsByBatchIdAsync(int PageNumber, int RowsPerPage,int BatchId)
        {
            return await
                _context.
                SoldProducts
                .AsNoTracking()
                .Include(b => b.Product)
                .ThenInclude(p => p.ProductType)
                .Include(b => b.Product)
                .ThenInclude(p => p.MasurementUnit)
                .Where(b => b.TakeBatchId == BatchId)
                .OrderByDescending(b => b.SoldProductId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<SoldProduct>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId)
        {
            return await
                 _context.
                 SoldProducts
                 .AsNoTracking()
                 .Include(b=>b.TakeBatch)
                 .ThenInclude(b=>b.Invoice)
                 .ThenInclude(i=>i.Customer)
                 .ThenInclude(i=>i.Person)

                 .Include(b => b.TakeBatch)
                 .ThenInclude(b => b.Invoice)
                 .ThenInclude(i=>i.Worker)
                 .ThenInclude(w=>w.Person)

                 .Where(b => b.ProductId == ProductId)
                 .OrderByDescending(b => b.SoldProductId)
                 .Skip((PageNumber - 1) * RowsPerPage)
                 .Take(RowsPerPage)
                 .ToListAsync();
        }
        
        public async Task<int> GetTotalPagesByBatchIdAsync(int BatchId, int RowsPerPage)
        {
            int totalCount =  await
                _context.
                SoldProducts
                .AsNoTracking()
                .Where(b => b.TakeBatchId == BatchId)
                .CountAsync();


            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage)
        {
            int totalCount = await
                _context.
                SoldProducts
                .AsNoTracking()
                .Where(b => b.ProductId == ProductId)
                .CountAsync();


            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }
    }
}
