using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Services.Invoices.Helper_Service;
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
    public class TakeBatchService
    {
        private readonly ITakeBatchRepository _takeBathcRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TakeBatchService> _logger;
        private readonly SoldProductService _SoldProductService;

        //Solve Circular Dependency
        private readonly InvoiceServiceHelper _InoviceServicehelper;

        public TakeBatchService(ITakeBatchRepository takeBathcRepo, IUnitOfWork unitOfWork, ILogger<TakeBatchService> logger , SoldProductService SoldProductService, InvoiceServiceHelper invoiceServicehelper)
        {
            _takeBathcRepo = takeBathcRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _SoldProductService = SoldProductService;
            _InoviceServicehelper = invoiceServicehelper;
        }
        #region Map

        public void MapAddDefaltAndNullValues(TakeBatch takeBatch)
        {

            //Take Date Is Defualt In Sql Now()

            takeBatch.TakeName = string.IsNullOrEmpty(takeBatch.TakeName) ? null : takeBatch.TakeName;
            takeBatch.Notes = string.IsNullOrEmpty(takeBatch.Notes) ? null : takeBatch.Notes;
        }

        public TakeBatch MapTakeBatch_AddDto(TakeBatchAddDto takeBatchAddDto, int UserId)
        {
            return new TakeBatch()
            {
                TakeName = takeBatchAddDto.TakeName,
                Notes = takeBatchAddDto.Notes,
                InvoiceId = takeBatchAddDto.InvoiceId,
                UserId = UserId,
                TakeDate = DateTime.Now,
                TakeBatchType = takeBatchAddDto.TakeBatchType
            };
        }
        #endregion

        private async Task<TakeBatch> CreateTakeBatchAggregateInternalAsync(
    TakeBatchAddDto dto,
    int userId,
    InvoiceType invoiceType,
            int CustomerId)
        {
            var takeBatch = MapTakeBatch_AddDto(dto, userId);

            MapAddDefaltAndNullValues(takeBatch);
            ValidationHelper.ValidateEntity(takeBatch);

            var soldProducts = await _SoldProductService
                .PrepareSoldProductsAsync(dto.SoldProductAddDtos, userId, invoiceType, dto.TakeBatchType, CustomerId);


            //Link Sold Products to Take Batch So EF can add them together
            soldProducts.ForEach(sp => sp.TakeBatch = takeBatch);
            takeBatch.SoldProducts = soldProducts;

            return takeBatch;
        }


        public async Task<TakeBatch> CreateTakeBatchAggregateAsync(
    TakeBatchAddDto dto,
    int userId,
    InvoiceType invoiceType,
             int CustomerId)
        {
            return await CreateTakeBatchAggregateInternalAsync(dto, userId, invoiceType,  CustomerId);
        }

        public async Task<TakeBatch> CreateTakeBatchAggregateAsync(
    TakeBatchAddDto dto,
    int userId,
    int CustomerId)
        {
            var invoiceType =
                await _InoviceServicehelper.GetInvoiceTypeByIdAsync(dto.InvoiceId);



            return await CreateTakeBatchAggregateInternalAsync(dto, userId, invoiceType, CustomerId);
        }


    }
}
