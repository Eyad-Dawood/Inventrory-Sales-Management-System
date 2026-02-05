using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.DTOS;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Services.Helpers;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;

namespace LogicLayer.Services.Invoices
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly CustomerService _customerService;
        private readonly TakeBatchService _takeBatchService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IInvoiceRepository invoiceRepo, IUnitOfWork unitOfWork, ILogger<InvoiceService> logger,CustomerService customerService,TakeBatchService takeBatchService)
        {
            _invoiceRepo = invoiceRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _customerService = customerService;
            _takeBatchService = takeBatchService;
        }

        #region Map
        private InvoiceProductSummaryDto MapInvoiceProductSummary_Dto(InvoiceProductSummary Summary)
        {
            return new InvoiceProductSummaryDto()
            {
                ProductId = Summary.ProductId,
                ProductFullName = Summary.ProductFullName,
                NetBuyingPrice = Summary.NetBuyingPrice,
                TotalSellingQuantity = Summary.TotalSellingQuantity,
                NetSellingPrice = Summary.NetSellingPrice,
                RefundQuanttiy = Summary.RefundQuanttiy,
                AvrageBuyingPrice = Summary.TotalSellingQuantity != 0 ? Summary.NetBuyingPrice / Summary.TotalSellingQuantity : 0,
                AvrageSellingPrice = Summary.TotalSellingQuantity != 0 ? Summary.NetSellingPrice / Summary.TotalSellingQuantity : 0,
                
            };
        }

        public void MapNullDefValues(Invoice Invoice)
        {

            Invoice.CloseDate = null;


            Invoice.TotalSellingPrice = 0;

            Invoice.TotalBuyingPrice = 0;
            Invoice.TotalRefundSellingPrice = 0;
            Invoice.TotalRefundBuyingPrice = 0;
            Invoice.TotalPaid = 0;

            Invoice.Notes = string.IsNullOrEmpty(Invoice.Notes) ? null : Invoice.Notes;
            
            Invoice.InvoiceState = InvoiceState.Open;
            
            Invoice.WorkerId = Invoice.WorkerId <= 0 ? null : Invoice.WorkerId;

            Invoice.CloseUserId = Invoice.CloseUserId <= 0 ? null : Invoice.CloseUserId;
        }

        public Invoice MapInvoice_AddDto(InvoiceAddDto DTO,int UserId)
        {
            return new Invoice()
            {
                InvoiceType = DTO.InvoiceType,
                CustomerId = DTO.CustomerId,
                WorkerId = DTO.WorkerId,
                OpenUserId = UserId,
                OpenDate = DateTime.Now,
                InvoiceState = InvoiceState.Open,
                Discount = DTO.Discount,
                Notes = DTO.Notes,
            };
        }

        public InvoiceReadDto MapInvoice_ReadDto(Invoice Invoice)
        {
            var finance = InvoiceFinanceHelper.InvoiceFinance(Invoice);

            return new InvoiceReadDto()
            {
                InvoiceId = (int)Invoice.InvoiceId,
                OpenDate = Invoice.OpenDate,
                CloseDate = Invoice.CloseDate,
                InvoiceType = Invoice.InvoiceType.GetDisplayName(),
                InvoiceState = Invoice.InvoiceState.GetDisplayName(),
                InvoiceStateEn = Invoice.InvoiceState,
                InvoiceTypeEn = Invoice.InvoiceType,
                ClosedByUserName = Invoice.CloseUser?.Username,
                CustomerName = Invoice.Customer.Person.FullName,
                OpenedByUserName = Invoice.OpenUser.Username,
                Discount = Invoice.Discount,
                Notes = Invoice.Notes,
                TotalBuyingPrice = Invoice.TotalBuyingPrice,
                TotalPaid = Invoice.TotalPaid,
                TotalRefundBuyingPrice = Invoice.TotalRefundBuyingPrice,
                TotalRefundSellingPrice = Invoice.TotalRefundSellingPrice,
                TotalSellingPrice = Invoice.TotalSellingPrice,
                WorkerName = Invoice.Worker != null ? Invoice.Worker.Person.FullName : string.Empty,
                CustomerId = Invoice.CustomerId,
                WorkerId = Invoice.WorkerId.HasValue ? Invoice.WorkerId.Value : null,
                AmountDue = finance.AmountDue,
                NetBuying = finance.NetBuying,
                NetProfit = finance.NetProfit,
                NetSale = finance.NetSale,
                Remaining = finance.Remaining,
            };
        }

        public InvoiceListDto MapInvoice_ListDto(Invoice invoice)
        {
            var finance = InvoiceFinanceHelper.InvoiceFinance(invoice);

            return new InvoiceListDto()
            {
                InvoiceId = (int)invoice.InvoiceId,
                OpenDate = invoice.OpenDate,
                CloseDate = invoice.CloseDate,
                InvoiceType = invoice.InvoiceType.GetDisplayName(),
                InvoiceState = invoice.InvoiceState.GetDisplayName(),
                InvoiceStateEn = invoice.InvoiceState,
                InvoiceTypeEn = invoice.InvoiceType,
                CustomerName = invoice.Customer.Person.FullName,
                CustomerPhoneNumber = invoice.Customer.Person.PhoneNumber,
                WorkerName = invoice.Worker != null ? invoice.Worker.Person.FullName : string.Empty,
                Town = invoice.Customer.Person.Town.TownName,
                TotalSellingPrice = invoice.TotalSellingPrice,
                TotalPaid = invoice.TotalPaid,
                Discount = invoice.Discount,
                TotalBuyingPrice = invoice.TotalBuyingPrice,
                TotalRefundBuyingPrice = invoice.TotalRefundBuyingPrice,
                TotalRefundSellingPrice = invoice.TotalRefundSellingPrice,
                CustomerId = invoice.CustomerId,
                WorkerId = invoice.WorkerId,
                AmountDue = finance.AmountDue,
                NetBuying = finance.NetBuying,
                NetProfit = finance.NetProfit,
                NetSale = finance.NetSale,
                Remaining = finance.Remaining,
            };
        }

        public InvoiceProductRefundSummaryListDto MapInvoiceProductRefundSummary_Dto(SoldProductRefundSummary invoice)
        {
            return new InvoiceProductRefundSummaryListDto()
            {
                NetRefundBuyingPrice = invoice.NetRefundBuyingPrice,
                NetRefundSellingPrice = invoice.NetRefundSellingPrice,
                ProductFullName = invoice.ProductFullName,
                ProductId = invoice.ProductId,
                TotalRefundSellingQuantity = invoice.TotalRefundSellingQuantity
            };
        }
        #endregion

        #region Helpers
       

        

        private void CalculateInvoiceFinance(Invoice Invoice, TakeBatch Batch)
        {
            if (Batch.TakeBatchType == TakeBatchType.Invoice)
            {
                foreach (var sp in Batch.SoldProducts)
                {
                    Invoice.TotalSellingPrice += sp.SellingPricePerUnit * sp.Quantity;
                    Invoice.TotalBuyingPrice += sp.BuyingPricePerUnit * sp.Quantity;
                }
            }
            else if (Batch.TakeBatchType == TakeBatchType.Refund)
            {
                foreach (var sp in Batch.SoldProducts)
                {
                    Invoice.TotalRefundSellingPrice += sp.SellingPricePerUnit * sp.Quantity;
                    Invoice.TotalRefundBuyingPrice += sp.BuyingPricePerUnit * sp.Quantity;
                }
            }
        }


        #endregion

        #region Validate Logic
        private async Task ValidateMainAddingLogic(Invoice Invoice,TakeBatchType batchType)
        {
            var customer = await _customerService.GetCustomerByIdAsync(Invoice.CustomerId);
            
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            if(!customer.IsActive)
            {
                throw new OperationFailedException($"لا يمكن تنفيذ العميلة للعميل {customer.FullName} لأنه غير نشط");
            }

            if((Invoice.InvoiceState == InvoiceState.Closed) && batchType==TakeBatchType.Invoice)
            {
                //Allow Refund
                throw new OperationFailedException("لا يمكن التعديل على فاتورة مغلقة");
            }
                        
        }

        private void ValidateMainDiscountLogic(Invoice invoice,decimal discount)
        {
            if (discount < 0)
            {
                throw new ValidationException(
                    ErrorMessagesManager.WriteValidationErrorMessageInArabic(new ValidationError()
                    {
                        Code = ValidationErrorCode.ValueOutOfRange,
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(Invoice.Discount),
                    })
              );
            }

            if (invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }

            if (invoice.InvoiceState == InvoiceState.Closed)
            {
                throw new OperationFailedException("لا يمكن إضافة خصم على فاتورة مغلقة");
            }

            if (invoice.InvoiceType == InvoiceType.Evaluation)
            {
                throw new OperationFailedException("لا يمكن إضافة على فاتورة تسعير");
            }

            decimal remaining = InvoiceFinanceHelper.GetRemainingAmount(invoice);
            decimal discountIncrease = discount - invoice.Discount;

            if (discountIncrease > remaining)
            {
                throw new OperationFailedException(
                    "لا يمكن إضافة خصم يؤدي إلى رصيد سالب"
                );
            }
        }

        private async Task ValidateMainPayLogic(Invoice invoice,decimal Amount)
        {
            if (Amount <= 0)
            {
                throw new ValidationException(

                    ErrorMessagesManager.WriteValidationErrorMessageInArabic(new ValidationError()
                    {
                        Code = ValidationErrorCode.ValueOutOfRange,
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(Payment.Amount),
                    })
              );
            }


            var customer = await _customerService.GetCustomerByIdAsync(invoice.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            else if(!customer.IsActive)
            {
                    throw new OperationFailedException($"لا يمكن الدفع للعميل {customer.FullName} لأنه غير نشط");
            }


            if (invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }

            if (invoice.InvoiceState == InvoiceState.Closed)
            {
                throw new OperationFailedException("لا يمكن الدفع على فاتورة مغلقة");
            }

            if (invoice.InvoiceType == InvoiceType.Evaluation)
            {
                throw new OperationFailedException("لا يمكن الدفع على فاتورة تسعير");
            }


            decimal remaining = InvoiceFinanceHelper.GetRemainingAmount(invoice);

            if (Amount > remaining)
            {
                throw new OperationFailedException("المبلغ المدفوع لا يمكن أن يكون أكبر من المبلغ الباقي");
            }

        }

        private async Task<decimal> ValidateMainRefundLogic(Invoice invoice)
        {
            if (invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }

            var customer = await _customerService.GetCustomerByIdAsync(invoice.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            else if (!customer.IsActive)
            {
                throw new OperationFailedException($"لا يمكن عمل مرتجع للعميل {customer.FullName} لأنه غير نشط");
            }


            if (invoice.InvoiceType == InvoiceType.Evaluation)
            {
                throw new OperationFailedException("لا يمكن عمل مرتجع على فاتورة تسعير");
            }

            decimal RefundAmount = InvoiceFinanceHelper.GetRefundAmount(invoice);

            if (RefundAmount <= 0)
            {
                throw new OperationFailedException("لا يوجد مبلغ مرتجع على الفاتورة");
            }

            return RefundAmount;
        }

        private void ValidateMainCloseLogic(Invoice invoice,int UserId)
        {
            

            if (invoice == null)
                throw new NotFoundException(typeof(Invoice));

            if (invoice.InvoiceState == InvoiceState.Closed)
                throw new OperationFailedException("الفاتورة مغلقة بالفعل");


            decimal remaining = InvoiceFinanceHelper.GetRemainingAmount(invoice);

            if (remaining > 0)
            {
                throw new OperationFailedException("لا يمكن غلق الفاتورة دون سداد كامل مستحقات المحل");
            }
            else if (remaining < 0)
            {
                throw new OperationFailedException("لا يمكن غلق الفاتورة دون سداد كامل مستحقات العميل");
            }

        }
        #endregion

        #region ListData

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllInvoicesAsync(int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsAsync(PageNumber, RowsPerPage, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByWorkerNameAsync(string Name, int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByWorkerNameAsync(PageNumber, RowsPerPage, Name, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByCustomerNameAsync(string Name, int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByCustomerNameAsync(PageNumber, RowsPerPage, Name, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByTownNameAsync(string Name, int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByTownNameAsync(PageNumber, RowsPerPage, Name, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByPhoneNumberAsync(string Number, int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByPhoneNumberAsync(PageNumber, RowsPerPage, Number, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }


        public async Task<List<InvoiceProductSummaryDto>> GetInvoiceProductSummaryAsync(int invoiceId)
        {
            return (await _invoiceRepo.GetInvoiceProductSummaryAsync(invoiceId))
                .Select(c => MapInvoiceProductSummary_Dto(c))
                .ToList();
        }



        public async Task<List<InvoiceProductRefundSummaryListDto>> GetInvoiceRefundProductSummaryAsync(int invoiceId)
        {
            return (await _invoiceRepo.GetInvoiceRefundProductSummaryAsync(invoiceId))
                .Select(c => MapInvoiceProductRefundSummary_Dto(c))
                .ToList();
        }



        #endregion

        #region PagesNumbers

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _invoiceRepo.GetTotalPagesAsync(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByWorkerNameAsync(string Name, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _invoiceRepo.GetTotalPageByWorkerNameAsync(Name, RowsPerPage, invoiceTypes, invoiceStates);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByCustomerNameAsync(string Name, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _invoiceRepo.GetTotalPageByCustomerNameAsync(Name, RowsPerPage, invoiceTypes, invoiceStates);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByTownNameAsync(string Name, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _invoiceRepo.GetTotalPageByTownNameAsync(Name, RowsPerPage, invoiceTypes, invoiceStates);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByPhoneuNmberAsync(string Number, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _invoiceRepo.GetTotalPageByPhoneNumberAsync(Number, RowsPerPage, invoiceTypes, invoiceStates);
        }

        #endregion

        #region Operations

        public async Task<InvoiceReadDto> GetInvoiceByIdAsync(int InvoiceId)
        {
            Invoice? Invoice = await _invoiceRepo.GetWithDetailsByIdAsync(InvoiceId);

            if (Invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }
            return MapInvoice_ReadDto(Invoice);
        }



        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddInvoiceAsync(InvoiceAddDto InvoiceDTO, TakeBatchAddDto BatchDTO, int UserId)
        {
            Invoice Invoice = MapInvoice_AddDto(InvoiceDTO, UserId);

            MapNullDefValues(Invoice);

            ValidationHelper.ValidateEntity(Invoice);

            await ValidateMainAddingLogic(Invoice, BatchDTO.TakeBatchType);

            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId, InvoiceDTO.InvoiceType, Invoice.CustomerId);

            //Link The TakeBatch With The Invoice
            Invoice.takeBatches.Add(TakeBatch);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _invoiceRepo.AddAsync(Invoice);

                    CalculateInvoiceFinance(Invoice, TakeBatch);

                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Invoice To Customer {CustomerId}",
                            InvoiceDTO.CustomerId);

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
        public async Task AddBatchToInvoice(int InvoiceId, TakeBatchAddDto BatchDTO, int UserId)
        {
            var Invoice = await _invoiceRepo.GetWithDetailsByIdAsync(InvoiceId);

            if (Invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }

            await ValidateMainAddingLogic(Invoice, BatchDTO.TakeBatchType);

            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId, Invoice.InvoiceType, Invoice.CustomerId);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Invoice.takeBatches.Add(TakeBatch);

                    CalculateInvoiceFinance(Invoice, TakeBatch);

                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Take Batches To Invoice {InvoiceId}",
                            InvoiceId);
                    await Transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task CloseInvoiceAsync(int InvoiceId, int UserId)
        {
            Invoice? invoice;
            try
            {
                invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل جلب الفاتورة {invoiceId} عن طريق المستخدم {userId}", InvoiceId, UserId);
                throw new OperationFailedException(ex);
            }

            ValidateMainCloseLogic(invoice, UserId);

            //Close NOW
            invoice.InvoiceState = InvoiceState.Closed;
            invoice.CloseDate = DateTime.Now; // If We Hosted on A server , we should change it to GMT with no offsets
            invoice.CloseUserId = UserId;

            try
            {
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("تم غلق الفاتورة : {invoiceId} عن طريق المستخدم : {userId}", InvoiceId, UserId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل قفل الفاتورة {invoiceId} عن طريق المستخدم {userId}", InvoiceId, UserId);
                throw new OperationFailedException(ex);
            }
        }

        public async Task AddDiscount(int InvoiceId, decimal discount, string AdditionalNotes)
        {

            var invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);

            ValidateMainDiscountLogic(invoice, discount);


            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {

                try
                {
                    //When Customer Bought Products we Decreased its Balance
                    decimal disocuntdiff = discount - invoice.Discount;

                    invoice.Discount = discount;
                    invoice.Notes = string.IsNullOrWhiteSpace(AdditionalNotes)
                                    ? null
                                    : AdditionalNotes;

                    if(disocuntdiff > 0) // Increased The Discoun , Add To Customer Balance
                        await _customerService.DepositBalance(invoice.CustomerId, disocuntdiff);
                    else if (disocuntdiff < 0) //Decreased Discount , Take From Customer Balance
                        await _customerService.WithdrawBalance(invoice.CustomerId,Math.Abs(disocuntdiff));


                    await _unitOfWork.SaveAsync();
                    await Transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "فشل عمل خصم على الفاتورة {invoiceId} بقيمة {disoucnt}", InvoiceId, discount);
                    await Transaction.RollbackAsync();
                    throw;
                }

            }
        }

        public async Task Pay(int InvoiceId, decimal Amount)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);
            
            await ValidateMainPayLogic(invoice,Amount);

            invoice.TotalPaid += Amount;

            await _customerService.DepositBalance(invoice.CustomerId,Amount);
        }

        public async Task<decimal> Refund(int InvoiceId)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);
            
            decimal RefundAmount = await ValidateMainRefundLogic(invoice);

            invoice.TotalPaid -= RefundAmount;

            await _customerService.WithdrawBalance(invoice.CustomerId, RefundAmount);

            return RefundAmount;
        }

        #endregion
    }
}
