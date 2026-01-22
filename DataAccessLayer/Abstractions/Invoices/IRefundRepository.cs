using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Invoices
{
    public interface IRefundRepository : IRepository<Refund>
    {

        public Task<Refund?> GetWithDetailsByIdAsync(int refundId);
        
        public Task<List<Refund>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage);


        public Task<List<Refund>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId);
        public Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage);


        public Task<List<Refund>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage);


        public Task<List<Refund>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId);
        public Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage);
    }
}
