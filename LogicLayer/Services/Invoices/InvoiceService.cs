using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.DTOS;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.General;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void MapAddDefaltAndNullValues(Invoice Invoice)
        {

            Invoice.CloseDate = null;


            Invoice.TotalSellingPrice = 0;

            Invoice.TotalBuyingPrice = 0;
            Invoice.TotalRefundSellingPrice = 0;
            Invoice.TotalRefundBuyingPrice = 0;
            Invoice.TotalPaid = 0;
            Invoice.Additional = 0;

            Invoice.AdditionNotes = string.IsNullOrEmpty(Invoice.AdditionNotes) ? null : Invoice.AdditionNotes;
            
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
            };
        }

        public InvoiceReadDto MapInvoice_ReadDto(Invoice Invoice)
        {
            return new InvoiceReadDto()
            {
                InvoiceId = Invoice.InvoiceId,
                OpenDate = Invoice.OpenDate,
                CloseDate = Invoice.CloseDate,
                InvoiceType = Invoice.InvoiceType.GetDisplayName(),
                InvoiceState = Invoice.InvoiceState.GetDisplayName(),
                ClosedByUserName = Invoice.CloseUser?.Username,
                CustomerName = Invoice.Customer.Person.FullName,
                OpenedByUserName = Invoice.OpenUser.Username,
                Additional = Invoice.Additional,
                AdditionalNotes = Invoice.AdditionNotes,
                TotalBuyingPrice = Invoice.TotalBuyingPrice,
                TotalPaid = Invoice.TotalPaid,
                TotalRefundBuyingPrice = Invoice.TotalRefundBuyingPrice,
                TotalRefundSellingPrice = Invoice.TotalRefundSellingPrice,
                TotalSellingPrice = Invoice.TotalSellingPrice,
                WorkerName = Invoice.Worker != null ? Invoice.Worker.Person.FullName : string.Empty,
                CustomerId = Invoice.CustomerId,
                WorkerId = Invoice.WorkerId.HasValue ? Invoice.WorkerId.Value : null,
            };
        }

        public InvoiceListDto MapInvoice_ListDto(Invoice invoice)
        {
            return new InvoiceListDto()
            {
                InvoiceId = invoice.InvoiceId,
                OpenDate = invoice.OpenDate,
                CloseDate = invoice.CloseDate,
                InvoiceType = invoice.InvoiceType.GetDisplayName(),
                InvoiceState = invoice.InvoiceState.GetDisplayName(),
                CustomerName = invoice.Customer.Person.FullName,
                WorkerName = invoice.Worker != null ? invoice.Worker.Person.FullName : string.Empty,
                TotalSellingPrice = invoice.TotalSellingPrice,
                TotalPaid = invoice.TotalPaid,
                Additional = invoice.Additional,
                TotalBuyingPrice = invoice.TotalBuyingPrice,
                TotalRefundBuyingPrice = invoice.TotalRefundBuyingPrice,
                TotalRefundSellingPrice = invoice.TotalRefundSellingPrice,
                CustomerId = invoice.CustomerId,
                WorkerId = invoice.WorkerId,
            };
        }

        public SoldProductRefundListDto MapInvoiceProductRefundSummary_Dto(SoldProductRefund invoice)
        {
            return new SoldProductRefundListDto()
            {
                NetRefundBuyingPrice = invoice.NetRefundBuyingPrice,
                NetRefundSellingPrice = invoice.NetRefundSellingPrice,
                ProductFullName = invoice.ProductFullName,
                ProductId = invoice.ProductId,
                TotalRefundSellingQuantity = invoice.TotalRefundSellingQuantity
            };
        }
        #endregion

        #region Validate Logic

        private async Task ValidateMainLogic(Invoice Invoice)
        {
            var customer = await _customerService.GetCustomerByIdAsync(Invoice.CustomerId);
            
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            if(!customer.IsActive)
            {
                throw new OperationFailedException($"لا يمكن فتح فاتورة للعميل {customer.FullName} لأنه غير نشط");
            }

            if(Invoice.InvoiceState == InvoiceState.Closed)
            {
                throw new OperationFailedException("لا يمكن التعديل على فاتورة مغلقة");
            }
        }

        #endregion


        private void CalculateInvoiceFinance(Invoice Invoice,TakeBatch Batch)
        {
            if(Batch.TakeBatchType == TakeBatchType.Invoice)
            {
                Invoice.TotalSellingPrice += Batch.SoldProducts.Sum(sp => sp.SellingPricePerUnit * sp.Quantity);
                Invoice.TotalBuyingPrice += Batch.SoldProducts.Sum(sp => sp.BuyingPricePerUnit * sp.Quantity);
            }
            else if(Batch.TakeBatchType == TakeBatchType.Refund)
            {
                Invoice.TotalRefundSellingPrice += Batch.SoldProducts.Sum(sp => sp.SellingPricePerUnit * sp.Quantity);
                Invoice.TotalRefundBuyingPrice += Batch.SoldProducts.Sum(sp => sp.BuyingPricePerUnit * sp.Quantity);
            }
        }


        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddInvoiceAsync(InvoiceAddDto InvoiceDTO,TakeBatchAddDto BatchDTO,int UserId)
        {
            Invoice Invoice = MapInvoice_AddDto(InvoiceDTO,UserId);

            MapAddDefaltAndNullValues(Invoice);

            ValidationHelper.ValidateEntity(Invoice);

            await ValidateMainLogic(Invoice);

            //return the created TakeBatch Aggregate
            //Linked with the SoldProducts
            //Should Send The Invoice Type cuz its not yet in DB so it cant find it
            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId , InvoiceDTO.InvoiceType, Invoice.CustomerId);

            //Link The TakeBatch With The Invoice
            Invoice.takeBatches.Add(TakeBatch);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _invoiceRepo.AddAsync(Invoice);

                    CalculateInvoiceFinance(Invoice,TakeBatch);

                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Invoice To Customer {CustomerId}",
                            InvoiceDTO.CustomerId);

                    _logger.LogError(ex,
                        "Failed To Add Take Batches To Customer {CustomertId}",
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
        public async Task AddBatchToInvoice(int InvoiceId,TakeBatchAddDto BatchDTO,int UserId)
        {
            var Invoice = await _invoiceRepo.GetWithDetailsByIdAsync(InvoiceId);
            
            if(Invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }

            await ValidateMainLogic(Invoice);

            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId,Invoice.InvoiceType,Invoice.CustomerId);

            using(var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Invoice.takeBatches.Add(TakeBatch);

                    CalculateInvoiceFinance(Invoice, TakeBatch);

                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex,
                            "Failed to add Take Batches To Invoice {InvoiceId}",
                            InvoiceId);
                    await Transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<InvoiceReadDto> GetInvoiceByIdAsync(int InvoiceId)
        {
            Invoice? Invoice = await _invoiceRepo.GetWithDetailsByIdAsync(InvoiceId);

            if (Invoice == null)
            {
                throw new NotFoundException(typeof(Invoice));
            }
            return MapInvoice_ReadDto(Invoice);
        }

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

            return await _invoiceRepo.GetTotalPageByWorkerNameAsync(Name,RowsPerPage, invoiceTypes,invoiceStates);
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
        public async Task<List<InvoiceListDto>> GetAllInvoicesAsync(int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsAsync(PageNumber, RowsPerPage,invoiceTypes,invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByWorkerNameAsync(string Name,int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByWorkerNameAsync(PageNumber, RowsPerPage,Name, invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<InvoiceListDto>> GetAllByCustomerNameAsync(string Name,int PageNumber, int RowsPerPage, List<InvoiceType> invoiceTypes, List<InvoiceState> invoiceStates)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _invoiceRepo
                .GetAllWithDetailsByCustomerNameAsync(PageNumber, RowsPerPage,Name,invoiceTypes, invoiceStates))
                .Select(i => MapInvoice_ListDto(i)
                ).ToList();
        }

        public async Task CloseInvoiceAsync(int InvoiceId,int UserId)
        {
            Invoice? invoice;

            try
            {
                 invoice = await _invoiceRepo.GetByIdAsync(InvoiceId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "فشل جلب الفاتورة {invoiceId} عن طريق المستخدم {userId}", InvoiceId, UserId);
                throw new OperationFailedException(ex);
            }

            if (invoice == null)
                throw new NotFoundException(typeof(Invoice));

            if(invoice.InvoiceState == InvoiceState.Closed)
                throw new OperationFailedException("الفاتورة مغلقة بالفعل");

            invoice.InvoiceState = InvoiceState.Closed;
            invoice.CloseDate = DateTime.Now;
            invoice.CloseUserId = UserId;

            try
            {
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("تم غلق الفاتورة : {invoiceId} عن طريق المستخدم : {userId}",InvoiceId,UserId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل قفل الفاتورة {invoiceId} عن طريق المستخدم {userId}", InvoiceId,UserId);
                throw new OperationFailedException(ex);
            }
        }

        public async Task<List<InvoiceProductSummaryDto>> GetInvoiceProductSummaryAsync(int invoiceId)
        {
            return (await _invoiceRepo.GetInvoiceProductSummaryAsync(invoiceId))
                .Select(c=>MapInvoiceProductSummary_Dto(c))
                .ToList();
        }

        public async Task<List<SoldProductRefundListDto>> GetInvoiceRefundProductSummaryAsync(int invoiceId)
        {
            return (await _invoiceRepo.GetInvoiceRefundProductSummaryAsync(invoiceId))
                .Select(c => MapInvoiceProductRefundSummary_Dto(c))
                .ToList();
        }

    }
}
