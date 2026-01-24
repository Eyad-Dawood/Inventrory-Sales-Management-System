using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.General;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
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
        public void MapAddDefaltAndNullValues(Invoice Invoice)
        {
            //OpenDate Is In The Sql Defulat Now()


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
            };
        }

        public InvoiceReadDto MapInvoice_ReadDto(Invoice Invoice)
        {
            return new InvoiceReadDto()
            {
                InvoiceId = Invoice.InvoiceId,
                OpenDate = Invoice.OpenDate,
                CloseDate = Invoice.CloseDate,
                InvoiceType = Invoice.InvoiceType,
                InvoiceState = Invoice.InvoiceState,
                ClosedByUserName = Invoice.CloseUser?.Username,
                CustomerName = Invoice.Customer.Person.FullName,
                OpenedByUserName = Invoice.OpenUser.Username,
                InvoiceFinance = new InvoiceFinance()
                {
                    TotalBuyingPrice = Invoice.TotalBuyingPrice,
                    TotalPaid = Invoice.TotalPaid,
                    TotalRefundBuyingPrice = Invoice.TotalRefundBuyingPrice,
                    TotalRefundSellingPrice = Invoice.TotalRefundSellingPrice,
                    TotalSellingPrice = Invoice.TotalSellingPrice,
                    Additional = Invoice.Additional,
                    AdditionalNotes = Invoice.AdditionNotes
                }
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
        }

        #endregion


        private void CalculateInvoiceFinance(Invoice Invoice,TakeBatch Batch)
        {
            Invoice.TotalSellingPrice += Batch.SoldProducts.Sum(sp => sp.SellingPricePerUnit * sp.Quantity);
            Invoice.TotalBuyingPrice += Batch.SoldProducts.Sum(sp => sp.BuyingPricePerUnit * sp.Quantity);
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
            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId);

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

            var TakeBatch = await _takeBatchService.CreateTakeBatchAggregateAsync(BatchDTO, UserId);

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

    }
}
