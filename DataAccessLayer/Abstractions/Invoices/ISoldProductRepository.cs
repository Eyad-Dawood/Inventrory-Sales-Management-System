using DataAccessLayer.Entities.DTOS;
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
        public Task<List<SoldProduct>> GetAllWithDetailsByProductIdAsync(int PageNumber, int RowsPerPage, int ProductId, List<TakeBatchType> takeBatchTypes);
        public Task<List<SoldProduct>> GetAllWithDetailsByBatchIdAsync(int PageNumber, int RowsPerPage, int BatchId, List<TakeBatchType> takeBatchTypes);
        public Task<List<SoldProduct>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, List<TakeBatchType> takeBatchTypes);
        public Task<List<SoldProduct>> GetAllWithDetailsByInvoiceIdAsync(int InvoiceId, List<TakeBatchType> takeBatchTypes);
        public Task<List<SoldProductForRefund>> GetAllForRefundWithDetailsByInvoiceIdAsync(int InvoiceId);


        public Task<int> GetTotalPagesByProductIdAsync(int ProductId, int RowsPerPage, List<TakeBatchType> takeBatchTypes);
        public Task<int> GetTotalPagesByBatchIdAsync(int BatchId, int RowsPerPage, List<TakeBatchType> takeBatchTypes);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, List<TakeBatchType> takeBatchTypes);

        public Task<decimal> GetTotalQuantitySoldByProductIdAsync(int ProductId);

    }
}
