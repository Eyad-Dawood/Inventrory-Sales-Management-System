using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Products
{
    public class ProductPriceLogService
    {
        private readonly IProductPriceLogRepository _ProductPriceLogrepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductPriceLogService(IProductPriceLogRepository productPriceLogrepository, IUnitOfWork unitOfWork)
        {
            _ProductPriceLogrepository = productPriceLogrepository;
            _unitOfWork = unitOfWork;
        }

        public ProductPriceLogListDto MapProductPriceLog_ListDto(ProductPriceLog productPriceLog)
        {
            return new ProductPriceLogListDto()
            {
                ProductId = productPriceLog.ProductId,
                OldBuyingPrice = productPriceLog.OldBuyingPrice,
                NewBuyingPrice = productPriceLog.NewBuyingPrice,
                LogDate = productPriceLog.LogDate,
                CreatedByUserName = productPriceLog.User.Username,
                OldSellingPrice = productPriceLog.OldSellingPrice,
                NewSellingPrice = productPriceLog.NewSellingPrice,
                ProductFullName = $@"{productPriceLog.Product.ProductType.ProductTypeName} [{productPriceLog.Product.ProductName}]"
            };
        }

        public ProductPriceLog MapProductPriceLog_AddDto(ProductPriceLogAddDto DTO)
        {
            return new ProductPriceLog()
            {
                OldBuyingPrice = DTO.OldBuyingPrice,
                NewBuyingPrice = DTO.NewBuyingPrice,
                OldSellingPrice = DTO.OldSellingPrice,
                NewSellingPrice = DTO.NewSellingPrice,
                CreatedByUserId = DTO.CreatedByUserId,
                ProductId = DTO.ProductId
            };
        }

        public void AddProductPriceLog(ProductPriceLogAddDto DTO)
        {
            ProductPriceLog productPriceLog = MapProductPriceLog_AddDto(DTO);

            _ProductPriceLogrepository.Add(productPriceLog);

            //Do Not Save and leave it to the main caller
            //_unitOfWork.Save();
            //Cannot Handle Any Exceptions Here By Using Try Catch , Because Want THe Caller (Product Service) To Know If There Is An Exception and Handle It There Not Here
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductPriceLogListDto> GetAllPriceLogs(int PageNumber , int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber,RowsPerPage);

            return _ProductPriceLogrepository
                .GetAllWithDetails(PageNumber, RowsPerPage)
                .Select(l => MapProductPriceLog_ListDto(l)).ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _ProductPriceLogrepository.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductPriceLogListDto> GetAllByProductNameAndDateTime(int PageNumber, int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _ProductPriceLogrepository.
                            GetAllByProductFullNameAndDate(PageNumber, RowsPerPage, prouctFullName, date)
                            .Select(p => MapProductPriceLog_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByProductNameAndDate(int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _ProductPriceLogrepository.GetTotalPagesByFullNameAndDate(RowsPerPage, prouctFullName, date);
        }

    }
}
