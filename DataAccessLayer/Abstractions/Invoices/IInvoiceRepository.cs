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

        public Task<List<Invoice>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage , DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType , DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState);


        public Task<List<Invoice>> GetAllWithDetailsByCustomerIdAsync(int PageNumber,int RowsPerPage,int CustomerId, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState);
        public Task<List<Invoice>> GetAllWithDetailsByWorkerIdAsync(int PageNumber, int RowsPerPage, int WorkerId, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState);

        public Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId,int RowsPerPage, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState);
        public Task<int> GetTotalPagesByWorkerIdAsync(int WorkerId, int RowsPerPage, DataAccessLayer.Entities.Invoices.InvoiceType[] InvoiceType, DataAccessLayer.Entities.Invoices.InvoiceState[] InvoiceState);

    }
}
