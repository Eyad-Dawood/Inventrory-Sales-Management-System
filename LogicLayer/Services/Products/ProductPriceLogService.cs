using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.Validation;
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
            _unitOfWork.Save();
        }

        public List<ProductPriceLogListDto> GetAllPriceLogs(int PageNumber , int RowsPerPage)
        {
            return _ProductPriceLogrepository
                .GetAllWithDetails(PageNumber, RowsPerPage)
                .Select(l => new ProductPriceLogListDto()
                {
                    OldBuyingPrice = l.OldBuyingPrice,
                    NewBuyingPrice = l.NewBuyingPrice,
                    LogDate = l.LogDate,
                    CreatedByUserName = l.User.Username,
                    OldSellingPrice= l.OldSellingPrice,
                    NewSellingPrice = l.NewSellingPrice,
                    ProductFullName = $@"{l.Product.ProductType.ProductTypeName} [{l.Product.ProductName}]"
                }).ToList();
        }
    }
}
