using DataAccessLayer.Abstractions;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repos;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities.Products;

namespace LogicLayer.Services.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductPriceLogService _pricelogService;
        private readonly ProductStockMovementLogService _stockMovementService;

        public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork, ProductPriceLogService logService, ProductStockMovementLogService stockMovementService)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _pricelogService = logService;
            _stockMovementService = stockMovementService;
        }

        private Product MapProduct_AddDto(ProductAddDto DTO)
        {
            return new Product()
            {
                BuyingPrice = DTO.BuyingPrice,
                SellingPrice = DTO.SellingPrice,
                MasurementUnitId = DTO.MasurementUnitId,
                ProductName = DTO.ProductName,
                ProductTypeId = DTO.ProductTypeId,
                QuantityInStorage = DTO.QuantityInStorage,
                Status = DTO.Status
            };
        }
        private void ApplyProductUpdate(Product product,ProductUpdateDto DTO)
        {
            product.SellingPrice = DTO.SellingPrice;
            product.BuyingPrice = DTO.BuyingPrice;
            product.MasurementUnitId = DTO.MasurementUnitId;
            product.ProductTypeId = DTO.ProductTypeId;
            product.ProductName = DTO.ProductName;
            product.Status = DTO.Status;
        }
        private ProductReadDto MapProduct_ReadDto(Product product)
        {
            return new ProductReadDto()
            {
                BuyingPrice = product.BuyingPrice,
                MesurementUnitName = product.MasurementUnit.UnitName,
                SellingPrice = product.SellingPrice,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Status = product.Status,
                ProductTypeName = product.ProductType.ProductTypeName,
                QuantityInStorage = product.QuantityInStorage
            };
        }
        private ProductPriceLogAddDto MapProduct_PriceLogDto(
            Product product,
            decimal oldBuyingPrice,
            decimal oldSellingPrice,
            int userId)
        {
            return new ProductPriceLogAddDto
            {
                ProductId = product.ProductId,
                OldBuyingPrice = oldBuyingPrice,
                NewBuyingPrice = product.BuyingPrice,
                OldSellingPrice = oldSellingPrice,
                NewSellingPrice = product.SellingPrice,
                CreatedByUserId = userId
            };
        }

        private ProductStockMovementLogAddDto MapProduct_StockMovementLogDto
            (
            Product product,
            int UserId,
            StockMovementReason Reason,
            decimal oldQuantity

            )
        {
            return new ProductStockMovementLogAddDto()
            {
                CreatedByUserId = UserId,
                ProductId = product.ProductId,
                Reason = Reason,
                NewQuantity = product.QuantityInStorage,
                OldQuantity = oldQuantity
            };
        }

        public void AddProduct(ProductAddDto DTO)
        {
            Product Product = MapProduct_AddDto(DTO);

            ValidationHelper.ValidateEntity(Product);

            _productRepo.Add(Product);
            _unitOfWork.Save();
        }
        public void UpdateProduct(ProductUpdateDto dto, int userId)
        {
            var product = _productRepo.GetById(dto.ProductId);

            if (product == null)
                throw new NotFoundException(typeof(Product));

            var oldBuyingPrice = product.BuyingPrice;
            var oldSellingPrice = product.SellingPrice;

            ApplyProductUpdate(product, dto);

            ValidationHelper.ValidateEntity(product);

            _unitOfWork.Save();

            if (oldBuyingPrice != product.BuyingPrice ||
                oldSellingPrice != product.SellingPrice)
            {
                //Try Catch Because Dont Want To Make An Exception If Log Fails
                try
                {
                    var logDto = MapProduct_PriceLogDto(
                    product,
                    oldBuyingPrice,
                    oldSellingPrice,
                    userId);

                    _pricelogService.AddProductPriceLog(logDto);
                }
                catch
                {
                    //Log Here Later 
                }

                
            }
        }
        public void DeleteProductById(int ProductId)
        {
            Product Product = _productRepo.GetById(ProductId);

            if (Product == null)
            {
                throw new NotFoundException(typeof(Product));
            }

            _productRepo.Delete(Product);
            _unitOfWork.Save();
        }
        public ProductReadDto GetProductById(int ProductId) 
        {
            Product product = _productRepo.GetWithProductType_And_UnitById(ProductId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }
            return MapProduct_ReadDto(product);
        }
        public List<ProductListDto> GetProductList(int PageNumber, int RowsPerPage) 
        {
            return _productRepo
                .GetAllWithProductType_And_Unit(PageNumber, RowsPerPage)
                .Select(p => new ProductListDto()
                {
                    SellingPrice = p.SellingPrice,
                    Status = p.Status,
                    BuyingPrice = p.BuyingPrice,
                    MesurementUnitName = p.MasurementUnit.UnitName,
                    ProductId = p.ProductId,
                    QuantityInStorage = p.QuantityInStorage,
                    ProductName = p.ProductName,
                    ProductTypeName = p.ProductType.ProductTypeName
                }).ToList();
        }

        private void EditQuantity(int productId,decimal quantity,int userId,StockMovementReason reason,bool isAddition)
        {
            if (quantity <= 0)
            {

                List<string> errors = new List<string>()
                {
                    ErrorMessagesManager.WriteValidationErrorMessageInArabic(new ValidationError()
                    {
                        Code = ValidationErrorCode.ValueOutOfRange,
                        ObjectType = typeof(Product),
                        PropertyName = nameof(Product.QuantityInStorage),
                    })
                };

                throw new ValidationException(errors);
            }

            var product = _productRepo.GetById(productId)
                ?? throw new NotFoundException(typeof(Product));

            var oldQuantity = product.QuantityInStorage;

            if (isAddition)
                product.QuantityInStorage = oldQuantity + quantity;
            else
            {
                if(oldQuantity < quantity)
                {
                    throw new MessageException("المخزن لا يحتوي علي كمية كافية");
                }

                product.QuantityInStorage = oldQuantity - quantity;

            }

            ValidationHelper.ValidateEntity(product);

            _unitOfWork.Save();

            try
            {
                var logDto = MapProduct_StockMovementLogDto(
                    product,
                    userId,
                    reason,
                    oldQuantity);

                _stockMovementService.AddProductStockMovementLog(logDto);
            }
            catch (Exception)
            {
                // log later
            }
        }
        public void AddQuantity(int productId, decimal quantity, int userId, StockMovementReason reason)
        {
            EditQuantity(productId, quantity,userId,reason,true);
        }
        public void RemoveQuantity(int productId, decimal quantity, int userId, StockMovementReason reason)
        {
            EditQuantity(productId, quantity, userId, reason, false);
        }
    }
}
