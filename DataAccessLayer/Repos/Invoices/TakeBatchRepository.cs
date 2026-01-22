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
    public class TakeBatchRepository : Repository<TakeBatch> , ITakeBatchRepository
    {
        public TakeBatchRepository(InventoryDbContext context) : base(context)
        {
        }

        private IQueryable<TakeBatch> TakeBatchWithInvoiceDetails()
        {
            return _context.TakeBatches
                .AsNoTracking()
                .Include(b => b.Invoice)
                    .ThenInclude(i => i.Customer)
                        .ThenInclude(c => c.Person)
                .Include(b => b.Invoice)
                    .ThenInclude(i => i.Worker)
                        .ThenInclude(w => w.Person);
        }

        public async Task<List<TakeBatch>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage)
        {
            return await
                TakeBatchWithInvoiceDetails()
                        .OrderByDescending(b => b.TakeDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<TakeBatch>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int invoiceId)
        {
            return await
                TakeBatchWithInvoiceDetails()
                .Where(b =>b.InvoiceId == invoiceId)
                .OrderByDescending(b => b.TakeDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int invoiceId, int RowsPerPage)
        {
            int totalCount = 
                await
                _context
                .TakeBatches
                .AsNoTracking()
                .Where(b => b.InvoiceId == invoiceId)
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<TakeBatch?> GetWithDetailsByIdAsync(int batchId)
        {
            return await TakeBatchWithInvoiceDetails()
                 .Include(b => b.User)
                .Where(b => b.TakeBatchId == batchId)
                .FirstOrDefaultAsync();
        }
    }
}
