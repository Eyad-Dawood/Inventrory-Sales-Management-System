using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Invoices
{
    public interface ITakeBatchRepository : IRepository<TakeBatch>
    {
        public Task<TakeBatch?> GetWithDetailsByIdAsync(int batchId);

        public Task<List<TakeBatch>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int invoiceId, List<TakeBatchType> Types);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int IvoiceId, int RowsPerPage, List<TakeBatchType> Types);

        public Task<List<TakeBatch>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage, List<TakeBatchType> Types);
        public Task<int> GetTotalPagesAsync(List<TakeBatchType> Types, int RowsPerPage);
    }
}
