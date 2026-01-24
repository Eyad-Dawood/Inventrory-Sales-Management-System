using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Products
{
    public class ProductStockMovementLogService
    {
        private readonly IProductStockMovementLogRepository _ProductStockMovementLogrepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductStockMovementLogService(IProductStockMovementLogRepository productPriceLogrepository, IUnitOfWork unitOfWork)
        {
            _ProductStockMovementLogrepository = productPriceLogrepository;
            _unitOfWork = unitOfWork;
        }

        #region Map
        private void MapNullValues(ProductStockMovementLogAddDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Notes))
                dto.Notes = null;
        }

        private ProductStockMovementLog MapProductStockMovementLog_AddDto(ProductStockMovementLogAddDto dto)
        {
            return new ProductStockMovementLog()
            {
                CreatedByUserId = dto.CreatedByUserId,
                ProductId = dto.ProductId,
                OldQuantity = dto.OldQuantity,
                NewQuantity = dto.NewQuantity,
                Reason = dto.Reason,
                Notes = dto.Notes,
                LogDate = DateTime.Now,
            };
        }
        private ProductStockMovementLogListDto MapProductStockMovementLog_ListDto(ProductStockMovementLog productStockMovment)
        {
            return new ProductStockMovementLogListDto()
            {
                ProductId = productStockMovment.ProductId,
                LogDate = productStockMovment.LogDate,
                CreatedbyUserName = productStockMovment.User.Username,
                NewQuantity = productStockMovment.NewQuantity,
                OldQuantity = productStockMovment.OldQuantity,
                ProductFullName = $@"{productStockMovment.Product.ProductType.ProductTypeName} [{productStockMovment.Product.ProductName}]",
                Reason = productStockMovment.Reason.GetDisplayName(),
                Notes = productStockMovment.Notes,
            };
        }
        #endregion

        public async Task AddProductStockMovementLogAsync(ProductStockMovementLogAddDto DTO)
        {
            MapNullValues(DTO);

            ProductStockMovementLog productStockMovementLog = MapProductStockMovementLog_AddDto(DTO);

            await _ProductStockMovementLogrepository.AddAsync(productStockMovementLog);
            //Do Not Save and leave it to the main caller
            //_unitOfWork.Save();
            //Cannot Handle Any Exceptions Here By Using Try Catch , Because Want THe Caller (Product Service) To Know If There Is An Exception and Handle It There Not Here
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task <List<ProductStockMovementLogListDto>> GetAllProductMovmentsAsync(int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return (await _ProductStockMovementLogrepository
                .GetAllWithDetailsAsync(PageNumber, RowsPerPage))
                .Select(p => MapProductStockMovementLog_ListDto(p)
                ).ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductStockMovementLogListDto>> GetAllByProductNameAndDateTimeAsync(int PageNumber, int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _ProductStockMovementLogrepository.
                            GetAllByProductFullNameAndDateAsync(PageNumber, RowsPerPage, prouctFullName,date))
                            .Select(p => MapProductStockMovementLog_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _ProductStockMovementLogrepository.GetTotalPagesAsync(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByProductNameAndDateAsync(int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _ProductStockMovementLogrepository.GetTotalPagesByFullNameAndDateAsync(RowsPerPage, prouctFullName,date);
        }

    }



}
