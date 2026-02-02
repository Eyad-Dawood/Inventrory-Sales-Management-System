using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Abstractions.Payments;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.DTOS;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using DataAccessLayer.Repos.Payments;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.DTOs.PaymentDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.Services.Invoices;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Payments
{
    public class PaymentService
    {

        private readonly ILogger<PaymentService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;

        public PaymentService(IPaymentRepository paymentRepository,IUnitOfWork unitOfWork , ILogger<PaymentService> logger,InvoiceService invoiceService,CustomerService customerService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _invoiceService = invoiceService;
            _customerService = customerService;
        }


        #region Map

        public Payment MapPayment_AddDto(PaymentAddDto DTO,int UserId)
        {
            return new Payment()
            {
                Date = DateTime.Now,
                Amount = DTO.Amount,
                CustomerId = DTO.CustomerId,
                InvoiceId = DTO.InvoiceId,
                Notes = DTO.Notes,
                UserId = UserId,
                PaymentReason = DTO.PaymentReason,
                PaidBy = DTO.PaidBy,
                RecivedBy = DTO.RecivedBy,
            };
        }
        public Payment MapPayment_AddDto(int InvoiceId,decimal amount , int CustomerId,int UserId)
        {
            return new Payment()
            {
                Date = DateTime.Now,
                Amount = amount,
                CustomerId = CustomerId,
                InvoiceId =InvoiceId,
                UserId = UserId,
                PaymentReason = PaymentReason.Refund,
                PaidBy = "المستخدم",
                RecivedBy = "المستلم"
            };
        }

        public void MapAddDefaltAndNullValues(Payment payment)
        {
            payment.Notes = string.IsNullOrEmpty(payment.Notes) ? null : payment.Notes;
            payment.InvoiceId = payment.InvoiceId <= 0 ? null : payment.InvoiceId;
        }

        public PaymentListDto MapPayment_ListDto(Payment payment)
        {
            return new PaymentListDto()
            {
                PaymentReasonEn = payment.PaymentReason,
                Date = payment.Date,
                Amount = payment.Amount,
                CustomerId = payment.CustomerId,
                CustomerName = payment.Customer.Person.FullName,
                InvoiceId = payment.InvoiceId,
                PaymentId = payment.PaymentId,
                PaymentReason = payment.PaymentReason.GetDisplayName(),
                PaidBy = payment.PaidBy,
                RecivedBy = payment.RecivedBy,
            };
        }

        public InvoicePaymentSummaryDto MapPayment_SummaryDto(InvoicePaymentSummary invoicePayment)
        {
            return new InvoicePaymentSummaryDto()
            {
                Date = invoicePayment.Date,
                Amount = invoicePayment.Amount,
                PaidBy = invoicePayment.PaidBy,
                PaymentId = invoicePayment.PaymentId,
                PaymentReason = invoicePayment.PaymentReason.GetDisplayName(),
                RecivedBy = invoicePayment.RecivedBy,
            };
        }

        #endregion


        #region Validate Logic

        private async Task ValidateMainLogic(Payment payment)
        {
            if (payment.Amount <= 0)
            {
                throw new ValidationException(
                    new List<string>()
                    {
                        LogicLayer.Validation.ErrorMessagesManager
                        .WriteValidationErrorMessageInArabic(
                            new DataAccessLayer.Validation.ValidationError()
                            {
                                Code = DataAccessLayer.Validation.ValidationErrorCode.ValueOutOfRange,
                                ObjectType = typeof(Payment),
                                PropertyName = nameof(Payment.Amount)
                            })
                    });
            }


           


            if (payment.PaymentReason == PaymentReason.Invoice && payment.InvoiceId == null)
            {
                throw new ValidationException(
                     new List<string>()
                     {
                        LogicLayer.Validation.ErrorMessagesManager
                        .WriteValidationErrorMessageInArabic(
                            new DataAccessLayer.Validation.ValidationError()
                            {
                                Code = DataAccessLayer.Validation.ValidationErrorCode.RequiredFieldMissing,
                                ObjectType = typeof(Payment),
                                PropertyName = nameof(Payment.Invoice)
                            })
                     });
            }



            if (payment.InvoiceId != null)
            {
                var invoice = await _invoiceService.GetInvoiceByIdAsync((int)payment.InvoiceId);

                if (invoice == null)
                {
                    throw new NotFoundException(typeof(Invoice));
                }

                

                if (invoice.TotalPaid <= _invoiceService.GetRemainingAmount(invoice.TotalSellingPrice,invoice.TotalRefundSellingPrice,invoice.Discount,invoice.TotalPaid))
                {
                    //No money to refund
                    throw new OperationFailedException("لا يوجد مبلغ زائد ليتم إرجاعه");
                }

                if (invoice.InvoiceTypeEn == InvoiceType.Evaluation)
                {
                    throw new OperationFailedException("لا يمكن الدفع لفاتورة تسعير");
                }

                if(invoice.InvoiceStateEn == InvoiceState.Closed)
                {
                    throw new OperationFailedException("لا يمكن الدفع لفاتورة مغلقة");
                }

            }

            var customer = await _customerService.GetCustomerByIdAsync(payment.CustomerId);


            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            else
            {
                if (!customer.IsActive)
                {
                    throw new OperationFailedException($"لا يمكن الدفع للعميل {customer.FullName} لأنه غير نشط");
                }
            }
        }

        #endregion



        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddInvoicePaymentAsync(PaymentAddDto PaymentDTO, int UserId)
        {
            Payment Payment = MapPayment_AddDto(PaymentDTO, UserId);

            MapAddDefaltAndNullValues(Payment);

            ValidationHelper.ValidateEntity(Payment);

            await ValidateMainLogic(Payment);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _paymentRepository.AddAsync(Payment);

                    if(Payment.InvoiceId!=null)
                    {
                        if (Payment.PaymentReason == PaymentReason.Invoice)
                              await _invoiceService.Pay((int)Payment.InvoiceId, Payment.Amount);
                    }

                    switch (Payment.PaymentReason)
                    {
                        case PaymentReason.Invoice:
                        case PaymentReason.Investment:
                            await _customerService.DepositBalance(Payment.CustomerId, Payment.Amount);
                            break;
                    }


                    await _unitOfWork.SaveAsync();
                    
                    await Transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Payment To Customer {CustomerId} Amount : {amount}",
                            Payment.CustomerId,
                            Payment.Amount);

                    await Transaction.RollbackAsync();

                    throw;
                }

            }
        }

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task<decimal> AddRefundPayment(int invoiceId,int userId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);

            if(invoice == null)
                throw new NotFoundException(typeof(Invoice));


            Payment Payment = MapPayment_AddDto(invoiceId,(invoice.TotalPaid-invoice.AmountDue),invoice.CustomerId,userId);

            MapAddDefaltAndNullValues(Payment);

            ValidationHelper.ValidateEntity(Payment);

            await ValidateMainLogic(Payment);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    decimal amount = 0;

                    await _paymentRepository.AddAsync(Payment);

                    if (Payment.InvoiceId != null)
                    {
                        if (Payment.PaymentReason == PaymentReason.Refund)
                            amount = await _invoiceService.Refund((int)Payment.InvoiceId);
                    }

                    switch (Payment.PaymentReason)
                    {
                        case PaymentReason.Refund:
                            await _customerService.DepositBalance(Payment.CustomerId,amount);
                            break;
                    }


                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                    return amount;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Refund Payment To Customer {CustomerId} Amount : {amount}",
                            Payment.CustomerId,
                            Payment.Amount);

                    await Transaction.RollbackAsync();

                    throw;
                }

            }
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage, List<PaymentReason> PaymentReasons)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _paymentRepository.GetTotalPagesAsync(RowsPerPage, PaymentReasons);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByCustomerNameAndDateAsync(int RowsPerPage, string CustomerName, DateTime? date, List<PaymentReason> PaymentReasons)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _paymentRepository.GetTotalPageByCustomerNameAndDateAsync(RowsPerPage,CustomerName, date, PaymentReasons);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<PaymentListDto>> GetAllPaymentsAsync(int PageNumber, int RowsPerPage, List<PaymentReason> PaymentReasons)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return (await _paymentRepository
                .GetAllWithDetailsAsync(PageNumber, RowsPerPage,PaymentReasons))
                .Select(p => MapPayment_ListDto(p)
                ).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<PaymentListDto>> GetAllByCustomerNameAndDateTimeAsync(int PageNumber, int RowsPerPage, string CustomerName, DateTime? date, List<PaymentReason> PaymentReasons)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _paymentRepository.
                            GetAllByCustomerNameAndDateTimeAsync(PageNumber, RowsPerPage, CustomerName, date, PaymentReasons))
                            .Select(p => MapPayment_ListDto(p))
                            .ToList();
        }

        public async Task<List<InvoicePaymentSummaryDto>> GetInvoiceProductSummaryAsync(int invoiceId)
        {
            return (await _paymentRepository.GetAllWithDetailsByInvoiceIdAsync(invoiceId))
                .Select(c => MapPayment_SummaryDto(c))
                .ToList();
        }
    }
}
