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
                Notes = dto.Notes
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

        public void AddProductStockMovementLog(ProductStockMovementLogAddDto DTO)
        {
            ProductStockMovementLog productStockMovementLog = MapProductStockMovementLog_AddDto(DTO);

            MapNullValues(DTO);

            _ProductStockMovementLogrepository.Add(productStockMovementLog);
            //Do Not Save and leave it to the main caller
            //_unitOfWork.Save();
            //Cannot Handle Any Exceptions Here By Using Try Catch , Because Want THe Caller (Product Service) To Know If There Is An Exception and Handle It There Not Here
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductStockMovementLogListDto> GetAllProductMovments(int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return _ProductStockMovementLogrepository
                .GetAllWithDetails(PageNumber, RowsPerPage)
                .Select(p => MapProductStockMovementLog_ListDto(p)
                ).ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductStockMovementLogListDto> GetAllByProductNameAndDateTime(int PageNumber, int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _ProductStockMovementLogrepository.
                            GetAllByProductFullNameAndDate(PageNumber, RowsPerPage, prouctFullName,date)
                            .Select(p => MapProductStockMovementLog_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _ProductStockMovementLogrepository.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByProductNameAndDate(int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _ProductStockMovementLogrepository.GetTotalPagesByFullNameAndDate(RowsPerPage, prouctFullName,date);
        }

    }



}
