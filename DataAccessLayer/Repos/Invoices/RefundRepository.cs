using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos.Invoices
{
    public class RefundRepository : Repository<Refund> , IRefundRepository
    {
        public RefundRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<List<Refund>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage)
        {
            return
                await
                _context.Refunds
                .AsNoTracking()
                .Include(r=>r.Invoice)
                    .ThenInclude(i=>i.Customer)
                        .ThenInclude(c=>c.Person)
                .Include(r=>r.Product)
                    .ThenInclude(p=>p.ProductType)
                .Include(r=>r.Product)
                    .ThenInclude(p=>p.MasurementUnit)
                .OrderByDescending(r => r.DateTime) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Refund>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId)
        {
            return
                 await
                _context.Refunds
                .AsNoTracking()
                .Include(r => r.Invoice)
                .Include(r => r.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(r => r.Product)
                    .ThenInclude(p => p.MasurementUnit)
                .Where(r=>r.Invoice.CustomerId ==  CustomerId)
                .OrderByDescending(r => r.DateTime) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Refund>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId)
        {
            return
                await
                _context.Refunds
                .AsNoTracking()
                .Include(r => r.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(r => r.Product)
                    .ThenInclude(p => p.MasurementUnit)
                .Where(r=>r.InvoiceId == InvoiceId)
                .OrderByDescending(r => r.DateTime) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Refund>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId)
        {
            return
                await
                _context.Refunds
                .AsNoTracking()
                .Include(r => r.Invoice)
                    .ThenInclude(i => i.Customer)
                        .ThenInclude(c => c.Person)
                .Where(r=>r.ProductId == ProductId)
                .OrderByDescending(r => r.DateTime) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage)
        {
            int totalCount =
                await
                _context.Refunds
                .AsNoTracking()
                .Where(r => r.Invoice.CustomerId == CustomerId)
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage)
        {
            int totalCount =
                            await
                            _context.Refunds
                            .AsNoTracking()
                            .Where(r => r.InvoiceId == InvoiceId)
                            .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage)
        {
            int totalCount =
                            await
                            _context.Refunds
                            .AsNoTracking()
                            .Where(r => r.ProductId == ProductId)
                            .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<Refund?> GetWithDetailsByIdAsync(int refundId)
        {
            return
                await
                _context.Refunds
                .AsNoTracking()
                .Include(r => r.Invoice)
                    .ThenInclude(i => i.Customer)
                        .ThenInclude(c => c.Person)
                .Include(r => r.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(r => r.Product)
                    .ThenInclude(p => p.MasurementUnit)
                .Where(r=>r.RefundId == refundId)
                .FirstOrDefaultAsync();
        }
    }
}
