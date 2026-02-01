using DataAccessLayer.Entities.DTOS;
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

        public Task<List<Payment>> GetAllWithDetailsAsync(int PageNumber,int RowsPerPage,List<PaymentReason> PaymentReasons);


        public Task<int> GetTotalPagesAsync(int RowsPerPage, List<PaymentReason> PaymentReasons);


        public Task<List<Payment>> GetAllWithDetailsByCustomerIdAsync(int PageNumber, int RowsPerPage, int CustomerId, List<PaymentReason> PaymentReasons);
        public Task<int> GetTotalPagesByCustomerIdAsync(int CustomerId, int RowsPerPage,List<PaymentReason> PaymentReasons);


        public Task<List<InvoicePaymentSummary>> GetAllWithDetailsByInvoiceIdAsync(int InvoiceId);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId);


        public Task<List<Payment>> GetAllWithDetailsByInvoiceIdAsync(int PageNumber, int RowsPerPage, int InvoiceId, List<PaymentReason> PaymentReasons);
        public Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, List<PaymentReason> PaymentReasons);


        public  Task<List<Payment>> GetAllByCustomerNameAndDateTimeAsync(int PageNumber, int RowsPerPage, string CustomerName, DateTime? date, List<PaymentReason> PaymentReasons);

        public  Task<int> GetTotalPageByCustomerNameAndDateAsync(int RowsPerPage, string CustomerName, DateTime? date, List<PaymentReason> PaymentReasons);
    }
}
