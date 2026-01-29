using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.DTOS;
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
            List<InvoiceType> invoiceTypes,
            List<InvoiceState> invoiceStates)
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


        public async Task<List<Invoice>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
        {
            IQueryable<Invoice> query = InvoiceWithDetails();

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            return
                await query
                .OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
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

        public async Task<List<Invoice>> GetAllWithDetailsByWorkerIdAsync(int PageNumber, int RowsPerPage, int WorkerId, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
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

        public async Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
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

        public async Task<int> GetTotalPagesByWorkerIdAsync(int WorkerId, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
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

        public async Task<List<Invoice>> GetAllWithDetailsByCustomerNameAsync(int PageNumber, int RowsPerPage, string CustomerName, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
        {
            IQueryable<Invoice> query = InvoiceWithDetails();

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            return
                await query
                .Where(i => i.Customer.Person.FullName.StartsWith(CustomerName))
                .OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllWithDetailsByWorkerNameAsync(int PageNumber, int RowsPerPage, string WorkerName, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
        {
            IQueryable<Invoice> query = InvoiceWithDetails();

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            return
                await query
                   .Where(i =>
                   i.Worker != null &&
                   i.Worker.Person.FullName.StartsWith(WorkerName)).OrderByDescending(i => i.OpenDate) //Latest
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPageByWorkerNameAsync(string WorkerName, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
        {
            IQueryable<Invoice> query = _context.Invoices
                           .AsNoTracking()
                .Where(i =>
                    i.Worker != null &&
                    i.Worker.Person.FullName.StartsWith(WorkerName));

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);

        }

        public async Task<int> GetTotalPageByCustomerNameAsync(string CustomerName, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState)
        {
            IQueryable<Invoice> query = _context.Invoices
                           .AsNoTracking()
                .Where(i => i.Customer.Person.FullName.StartsWith(CustomerName));

            query = ApplyFilter(query, InvoiceType, InvoiceState);

            int totalCount =
                await query
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<List<InvoiceProductSummary>> GetInvoiceProductSummaryAsync(int invoiceId)
        {
            return await _context.SoldProducts
                .AsNoTracking()
                .Where(sp => sp.TakeBatch.Invoice.InvoiceId == invoiceId)
                .GroupBy(sp => new
                {
                    sp.ProductId,
                    sp.Product.ProductName,
                    sp.Product.ProductType.ProductTypeName
                })
                .Select(g => new InvoiceProductSummary
                {
                    ProductId = g.Key.ProductId,
                    ProductFullName = $"{g.Key.ProductTypeName} [{g.Key.ProductName}]",


                    TotalSellingQuantity = g.Sum(x =>
                        x.TakeBatch.TakeBatchType == TakeBatchType.Invoice
                            ? x.Quantity
                                : 0),

                    RefundQuanttiy = g.Sum(x =>
                        x.TakeBatch.TakeBatchType == TakeBatchType.Refund
                            ? x.Quantity
                                : 0),


                    NetBuyingPrice =
                g.Sum(x =>
                      x.TakeBatch.TakeBatchType == TakeBatchType.Invoice
                      ? x.Quantity * x.BuyingPricePerUnit
                       : x.TakeBatch.TakeBatchType == TakeBatchType.Refund
                ? -(x.Quantity * x.BuyingPricePerUnit)
                : 0),


                    NetSellingPrice = g.Sum(x =>
                      x.TakeBatch.TakeBatchType == TakeBatchType.Invoice
                      ? x.Quantity * x.SellingPricePerUnit
                       : x.TakeBatch.TakeBatchType == TakeBatchType.Refund
                ? -(x.Quantity * x.SellingPricePerUnit)
                : 0),

                })
                .ToListAsync();
        }

        public async Task<List<SoldProductRefund>> GetInvoiceRefundProductSummaryAsync(int invoiceId)
        {
            return await _context.SoldProducts
                .AsNoTracking()
                .Where(sp =>
                    sp.TakeBatch.Invoice.InvoiceId == invoiceId &&
                    sp.TakeBatch.TakeBatchType == TakeBatchType.Refund)
                .GroupBy(sp => new
                {
                    sp.ProductId,
                    sp.Product.ProductName,
                    sp.Product.ProductType.ProductTypeName
                })
                .Select(g => new SoldProductRefund
                {
                    ProductId = g.Key.ProductId,
                    ProductFullName = $"{g.Key.ProductTypeName} [{g.Key.ProductName}]",

                    TotalRefundSellingQuantity =
                        g.Sum(x => x.Quantity),

                    NetRefundBuyingPrice =
                        g.Sum(x => x.Quantity * x.BuyingPricePerUnit),

                    NetRefundSellingPrice =
                        g.Sum(x => x.Quantity * x.SellingPricePerUnit)
                })
                .ToListAsync();
        }


    }
}
