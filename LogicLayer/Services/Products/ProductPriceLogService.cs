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

        #region Map
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
        #endregion

        public async Task AddProductPriceLogAsync(ProductPriceLogAddDto DTO)
        {
            ProductPriceLog productPriceLog = MapProductPriceLog_AddDto(DTO);

            await _ProductPriceLogrepository.AddAsync(productPriceLog);

            //Do Not Save and leave it to the main caller
            //_unitOfWork.Save();
            //Cannot Handle Any Exceptions Here By Using Try Catch , Because Want THe Caller (Product Service) To Know If There Is An Exception and Handle It There Not Here
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductPriceLogListDto>> GetAllPriceLogsAsync(int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return (await _ProductPriceLogrepository
                .GetAllWithDetailsAsync(PageNumber, RowsPerPage))
                .Select(l => MapProductPriceLog_ListDto(l))
                .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return (await _ProductPriceLogrepository.GetTotalPagesAsync(RowsPerPage));
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductPriceLogListDto>> GetAllByProductNameAndDateTimeAsync(int PageNumber, int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _ProductPriceLogrepository.
                            GetAllByProductFullNameAndDateAsync(PageNumber, RowsPerPage, prouctFullName, date))
                            .Select(p => MapProductPriceLog_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByProductNameAndDateAsync(int RowsPerPage, string prouctFullName, DateTime? date)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return (await _ProductPriceLogrepository.GetTotalPagesByFullNameAndDateAsync(RowsPerPage, prouctFullName, date));
        }

    }
}
