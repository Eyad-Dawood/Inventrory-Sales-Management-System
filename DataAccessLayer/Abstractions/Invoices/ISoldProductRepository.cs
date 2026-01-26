using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Invoices
{
    public interface ISoldProductRepository : IRepository<SoldProduct>
    {
        public Task<List<SoldProduct>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId);
        public Task<List<SoldProduct>> GetAllWithDetailsByBatchIdAsync(int PageNumber, int RowsPerPage, int BatchId);
        public Task<List<SoldProduct>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId);


        public Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage);
        public Task<int> GetTotalPagesByBatchIdAsync(int BatchId, int RowsPerPage);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage);

    }
}
