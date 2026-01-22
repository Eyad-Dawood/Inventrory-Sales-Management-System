using DataAccessLayer.Abstractions.Payments;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos.Payments
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(InventoryDbContext context) : base(context)
        {
        }

        private IQueryable<Payment> ApplyFilter(
            IQueryable<Payment> query,
            params PaymentReason[] paymentReasons)
        {
            if (paymentReasons == null || paymentReasons.Length == 0)
                return query;

            return query.Where(p => paymentReasons.Contains(p.PaymentReason));
        }

        public async Task<List<Payment>> GetAllWithDetailsAsync(int PageNumber,int RowsPerPage,params PaymentReason[] PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Include(p => p.Customer);

            query = ApplyFilter(query, PaymentReasons);

            return await query
                .OrderByDescending(p => p.Date)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, params PaymentReason[] PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Where(p => p.CustomerId == CustomerId);
                
            query = ApplyFilter(query, PaymentReasons);

            return await query
                .OrderByDescending(p => p.Date)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, params PaymentReason[] PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Customer)
                .Where(p => p.InvoiceId == InvoiceId);

            query = ApplyFilter(query, PaymentReasons);

            return await query
                .OrderByDescending(p => p.Date)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage, params PaymentReason[] PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                            .AsNoTracking()
                            .Where(p => p.CustomerId == CustomerId);

            query = ApplyFilter(query, PaymentReasons);

            int totalCount = 
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, params PaymentReason[] PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                                       .AsNoTracking()
                                       .Where(p => p.InvoiceId == InvoiceId);

            query = ApplyFilter(query, PaymentReasons);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<Payment?> GetWithDetailsByIdAsync(int PaymentId)
        {
            IQueryable<Payment> query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Include(p => p.Customer);


            return await query
                .Where(p=>p.PaymentId == PaymentId)
                .FirstOrDefaultAsync();
        }
    }
}
