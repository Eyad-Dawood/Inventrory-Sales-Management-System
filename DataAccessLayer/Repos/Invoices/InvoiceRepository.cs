using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repos.Invoices
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InventoryDbContext context) : base(context)
        {
        }

        public IQueryable<Invoice> InvoiceWithDetails()
        {
            return _context
                .Invoices
                .AsNoTracking()
                .Include(i => i.Customer)
                    .ThenInclude(c => c.Person)
                .Include(i => i.Worker)
                    .ThenInclude(w => w.Person)
                .Include(i => i.OpenUser)
                .Include(i => i.CloseUser);
        }

        private IQueryable<Invoice> ApplyFilter(
            IQueryable<Invoice> query,
            InvoiceType[] invoiceTypes,
            InvoiceState[] invoiceStates)
        {
            var inlineQuery = query;

            if (invoiceTypes != null && invoiceTypes.Any())
            {
                inlineQuery = inlineQuery
                    .Where(i => invoiceTypes.Contains(i.InvoiceType));
            }

            if (invoiceStates != null && invoiceStates.Any())
            {
                inlineQuery = inlineQuery
                    .Where(i => invoiceStates.Contains(i.InvoiceState));
            }

            return inlineQuery;
        }


        public async Task<List<Invoice>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState)
        {
            IQueryable<Invoice> query = InvoiceWithDetails();

            query = ApplyFilter(query,InvoiceType,InvoiceState);

            return
                await query
                .OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState)
        {
            IQueryable<Invoice> query = _context
                .Invoices
                .AsNoTracking()
                .Include(i => i.Worker)
                    .ThenInclude(w => w.Person)
                .Include(i => i.OpenUser)
                .Include(i => i.CloseUser);

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            return
                await query
                .Where(i => i.CustomerId == CustomerId)
                .OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();

        }

        public async Task<List<Invoice>> GetAllWithDetailsByWorkerIdAsync(int PageNumber, int RowsPerPage, int WorkerId, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState)
        {
            IQueryable<Invoice> query = _context
                .Invoices
                .AsNoTracking()
                .Include(i => i.Customer)
                    .ThenInclude(c => c.Person)
                .Include(i => i.OpenUser)
                .Include(i => i.CloseUser);

            query = ApplyFilter(query, InvoiceType, InvoiceState);


            return
                await query
                .Where(i => i.WorkerId == WorkerId)
                .OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState)
        {
            IQueryable<Invoice> query = _context.Invoices
                                       .AsNoTracking()
                .Where(i => i.CustomerId == CustomerId);

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<int> GetTotalPagesByWorkerIdAsync(int WorkerId, int RowsPerPage, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState)
        {
            IQueryable<Invoice> query = _context.Invoices
                                       .AsNoTracking()
                .Where(i => i.WorkerId == WorkerId);

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<Invoice?> GetWithDetailsByIdAsync(int invoiceId)
        {
            return
                await _context
                .Invoices
                .Include(i => i.Customer)
                    .ThenInclude(c => c.Person)
                .Include(i => i.Worker)
                    .ThenInclude(w => w.Person)
                .Include(i => i.OpenUser)
                .Include(i => i.CloseUser)
                .Where(i => i.InvoiceId == invoiceId)
                .FirstOrDefaultAsync();
        }
    }
}
