using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Invoices
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public Task<Invoice?> GetWithDetailsByIdAsync(int invoiceId);

        public Task<List<Invoice>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage , List<InvoiceType> InvoiceType , List<InvoiceState> InvoiceState);


        public Task<List<Invoice>> GetAllWithDetailsByCustomerIdAsync(int PageNumber,int RowsPerPage,int CustomerId, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<List<Invoice>> GetAllWithDetailsByWorkerIdAsync(int PageNumber, int RowsPerPage, int WorkerId, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<List<Invoice>> GetAllWithDetailsByCustomerNameAsync(int PageNumber, int RowsPerPage, string CustomerName, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<List<Invoice>> GetAllWithDetailsByWorkerNameAsync(int PageNumber, int RowsPerPage, string WorkerName, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);

        public Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId,int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<int> GetTotalPagesByWorkerIdAsync(int WorkerId, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<int> GetTotalPageByWorkerNameAsync(string WorkerName, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);
        public Task<int> GetTotalPageByCustomerNameAsync(string CustomerName, int RowsPerPage, List<InvoiceType> InvoiceType, List<InvoiceState> InvoiceState);

    }
}
