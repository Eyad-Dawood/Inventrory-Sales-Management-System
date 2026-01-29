using DataAccessLayer.Abstractions.Invoices;
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
    public class TakeBatchRepository : Repository<TakeBatch> , ITakeBatchRepository
    {
        public TakeBatchRepository(InventoryDbContext context) : base(context)
        {
        }

        private IQueryable<TakeBatch> ApplyFilter(
            IQueryable<TakeBatch> query,
            List<TakeBatchType> Types)
        {
            var inlineQuery = query;

            if(Types != null && Types.Any())
            {
                inlineQuery = inlineQuery.Where(t => Types.Contains(t.TakeBatchType));
            }

            return inlineQuery;
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

        public async Task<List<TakeBatch>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage, List<TakeBatchType> Types)
        {

                 IQueryable<TakeBatch> querey = TakeBatchWithInvoiceDetails();

            querey = ApplyFilter(querey, Types);

                 return await querey.OrderByDescending(b => b.TakeDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<TakeBatch>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int invoiceId, List<TakeBatchType> Types)
        {
            IQueryable<TakeBatch> querey = TakeBatchWithInvoiceDetails();

            querey = ApplyFilter(querey, Types);

            return await querey.Where(b =>b.InvoiceId == invoiceId)
                .OrderByDescending(b => b.TakeDate)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int invoiceId, int RowsPerPage, List<TakeBatchType> Types)
        {
            IQueryable<TakeBatch> querey = _context
                .TakeBatches
                .AsNoTracking();

            querey = ApplyFilter(querey, Types);

            int totalCount = 
                await
                querey.Where(b => b.InvoiceId == invoiceId)
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

        public async Task<int> GetTotalPagesAsync(List<TakeBatchType> Types,int RowsPerPage)
        {
            IQueryable<TakeBatch> querey = _context
                .TakeBatches
                .AsNoTracking();

            querey = ApplyFilter(querey, Types);

            int totalCount =
                await
                querey
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);

        }
    }
}
