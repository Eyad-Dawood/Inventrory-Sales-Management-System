using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Validation.Exceptions;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;

namespace LogicLayer.Services.Invoices
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly CustomerService _customerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IInvoiceRepository invoiceRepo, IUnitOfWork unitOfWork, ILogger<InvoiceService> logger,CustomerService customerService)
        {
            _invoiceRepo = invoiceRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _customerService = customerService;
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
                OpenUserId = UserId
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


            try
            {
                await _invoiceRepo.AddAsync(Invoice);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                        "Failed to add Invoice To Customer {CustomerId}",
                        InvoiceDTO.CustomerId);

                _logger.LogError(ex,
                    "Failed To Add Take Bathch To Customer {CustomertId}",
                    InvoiceDTO.CustomerId);
                throw new OperationFailedException(ex);
            }
        }
    }
}
