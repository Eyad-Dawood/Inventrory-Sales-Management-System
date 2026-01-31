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
            List<PaymentReason> paymentReasons)
        {
            if (paymentReasons == null || paymentReasons.Count == 0)
                return query;

            return query.Where(p => paymentReasons.Contains(p.PaymentReason));
        }

        public async Task<List<Payment>> GetAllWithDetailsAsync(int PageNumber,int RowsPerPage,List<PaymentReason> PaymentReasons)
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

        public async Task<List<Payment>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, List<PaymentReason> PaymentReasons)
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

        public async Task<List<Payment>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, List<PaymentReason> PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Customer)
                .ThenInclude(c=>c.Person)
                .Where(p => p.InvoiceId == InvoiceId);

            query = ApplyFilter(query, PaymentReasons);

            return await query
                .OrderByDescending(p => p.Date)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage, List<PaymentReason> PaymentReasons)
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

        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, List<PaymentReason> PaymentReasons)
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

        public async Task<List<Payment>> GetAllByCustomerNameAndDateTimeAsync(int PageNumber, int RowsPerPage, string CustomerName, DateTime? date , List<PaymentReason> PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
           .AsNoTracking()
           .Include(p => p.Customer)
           .ThenInclude(c => c.Person);

            // ===== Filter by Date =====
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = fromDate.AddDays(1);

                query = query.Where(p =>
                    p.Date >= fromDate &&
                    p.Date < toDate);
            }

            // ===== Filter by Customer Name / Type =====
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                query = query.Where(p =>
                    p.Customer.Person.FullName.Contains(CustomerName));
            }


            query = ApplyFilter(query, PaymentReasons);


            return await query
                .OrderByDescending(p => p.Date)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();

        }

        public async Task<int> GetTotalPageByCustomerNameAndDateAsync(int RowsPerPage, string CustomerName, DateTime? date , List<PaymentReason> PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                                      .AsNoTracking();

            // ===== Filter by Date =====
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = fromDate.AddDays(1);

                query = query.Where(p =>
                    p.Date >= fromDate &&
                    p.Date < toDate);
            }

            // ===== Filter by Customer Name / Type =====
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                query = query.Where(p =>
                    p.Customer.Person.FullName.Contains(CustomerName));
            }

            query = ApplyFilter(query, PaymentReasons);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesAsync(int RowsPerPage, List<PaymentReason> PaymentReasons)
        {
            IQueryable<Payment> query = _context.Payments
                           .AsNoTracking();

            query = ApplyFilter(query, PaymentReasons);


            var totalCount = await _context
                                    .Payments
                                    .AsNoTracking()
                                    .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }
    }
}
