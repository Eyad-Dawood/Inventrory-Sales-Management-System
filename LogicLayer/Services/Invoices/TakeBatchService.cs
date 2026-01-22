using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Validation;
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

        public TakeBatchService(ITakeBatchRepository takeBathcRepo, IUnitOfWork unitOfWork, ILogger<TakeBatchService> logger , SoldProductService SoldProductService)
        {
            _takeBathcRepo = takeBathcRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _SoldProductService = SoldProductService;
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

            };
        }
        #endregion


        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddTakeBatchAsync(TakeBatchAddDto DTO, int UserId)
        {
            TakeBatch takeBatch = MapTakeBatch_AddDto(DTO, UserId);

            MapAddDefaltAndNullValues(takeBatch);

            ValidationHelper.ValidateEntity(takeBatch);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                }
                catch 
                {

                }
            }
        }
    

    }
}
