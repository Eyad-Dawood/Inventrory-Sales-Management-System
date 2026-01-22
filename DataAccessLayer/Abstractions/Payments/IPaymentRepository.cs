using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Payments
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public Task<Payment?> GetWithDetailsByIdAsync(int PaymentId);

        public Task<List<Payment>> GetAllWithDetailsAsync(int PageNumber,int RowsPerPage,params DataAccessLayer.Entities.Payments.PaymentReason[] PaymentReasons);


        public Task<List<Payment>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, params DataAccessLayer.Entities.Payments.PaymentReason[] PaymentReasons);
        public Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage,params DataAccessLayer.Entities.Payments.PaymentReason[] PaymentReasons);


        public Task<List<Payment>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, params DataAccessLayer.Entities.Payments.PaymentReason[] PaymentReasons);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, params DataAccessLayer.Entities.Payments.PaymentReason[] PaymentReasons);


    }
}
