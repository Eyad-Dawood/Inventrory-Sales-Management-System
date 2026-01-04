using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductPriceLogService _pricelogService;
        private readonly ProductStockMovementLogService _stockMovementService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork, ProductPriceLogService PricelogService, ProductStockMovementLogService stockMovementService,ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _pricelogService = PricelogService;
            _stockMovementService = stockMovementService;
            _logger = logger;
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

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddProduct(ProductAddDto DTO)
        {
            Product Product = MapProduct_AddDto(DTO);

            ValidationHelper.ValidateEntity(Product);

            _productRepo.Add(Product);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to add product {ProductName}",
                    DTO.ProductName);

                throw new OperationFailedException();
            }
        }



        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails
        /// </exception>
        public void UpdateProduct(ProductUpdateDto dto, int userId)
        {
            var product = _productRepo.GetById(dto.ProductId);

            if (product == null)
                throw new NotFoundException(typeof(Product));

            var oldBuyingPrice = product.BuyingPrice;
            var oldSellingPrice = product.SellingPrice;

            ApplyProductUpdate(product, dto);
            ValidationHelper.ValidateEntity(product);


            using (var Transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    if (oldBuyingPrice != product.BuyingPrice ||
                        oldSellingPrice != product.SellingPrice)
                    {

                        var logDto = MapProduct_PriceLogDto(
                        product,
                        oldBuyingPrice,
                        oldSellingPrice,
                        userId);

                        _pricelogService.AddProductPriceLog(logDto);
                    }

                    _unitOfWork.Save();

                    Transaction.Commit();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Failed to update product {ProductId} by user {UserId}",
                        dto.ProductId,
                        userId);

                    Transaction.Rollback();

                    throw new OperationFailedException();
                }
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void DeleteProductById(int ProductId)
        {
            Product Product = _productRepo.GetById(ProductId);

            if (Product == null)
            {
                throw new NotFoundException(typeof(Product));
            }


            try
            {
                _productRepo.Delete(Product);
                _unitOfWork.Save();
            }
            catch(Exception ex)
            {

                _logger.LogError(ex,
                    "Failed to Delete product {ProductId}",
                    ProductId);

                throw new OperationFailedException();
            }
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public ProductReadDto GetProductById(int ProductId) 
        {
            Product product = _productRepo.GetWithProductType_And_UnitById(ProductId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }
            return MapProduct_ReadDto(product);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductListDto> GetProductList(int PageNumber, int RowsPerPage) 
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


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

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
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

            var product = _productRepo.GetById(productId);

            if (product == null)
                 throw new NotFoundException(typeof(Product));

            var oldQuantity = product.QuantityInStorage;

            if (isAddition)
            {
                product.QuantityInStorage = oldQuantity + quantity;
            }
            else
            {
                if (oldQuantity < quantity)
                {
                    throw new OperationFailedException("المخزن لا يحتوي علي كمية كافية");
                }
                product.QuantityInStorage = oldQuantity - quantity;
            }

            ValidationHelper.ValidateEntity(product);

            using (var Transaction = _unitOfWork.BeginTransaction())
            {

                try
                {
                    var logDto = MapProduct_StockMovementLogDto(
                        product,
                        userId,
                        reason,
                        oldQuantity);

                    _stockMovementService.AddProductStockMovementLog(logDto);

                    _unitOfWork.Save();
                    Transaction.Commit();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Failed to change product {ProductId} quantity from {OldQuantity} by {QuantityChange} by user {UserId} for reason {Reason}",
                        productId,
                        oldQuantity,
                        quantity,
                        userId,
                        reason);


                    Transaction.Rollback();

                    throw new OperationFailedException();
                }
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddQuantity(int productId, decimal quantity, int userId, StockMovementReason reason)
        {
            EditQuantity(productId, quantity,userId,reason,true);
        }

        
        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void RemoveQuantity(int productId, decimal quantity, int userId, StockMovementReason reason)
        {
            EditQuantity(productId, quantity, userId, reason, false);
        }
    }
}
