using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Utilities;
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
                IsAvailable = DTO.IsAvailable
            };
        }
        private void ApplyProductUpdate(Product product,ProductUpdateDto DTO)
        {
            product.SellingPrice = DTO.SellingPrice;
            product.BuyingPrice = DTO.BuyingPrice;
            product.ProductName = DTO.ProductName;
            product.IsAvailable = DTO.IsAvilable;
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
                IsAvailable = product.IsAvailable,
                ProductTypeName = product.ProductType.ProductTypeName,
                QuantityInStorage = product.QuantityInStorage,
                TotalSlaes = 0
            };
        }

        private ProductListDto MapProduct_ListDto(Product product)
        {
            return new ProductListDto()
            {
                SellingPrice = product.SellingPrice,
                IsAvilable = product.IsAvailable,
                BuyingPrice = product.BuyingPrice,
                MesurementUnitName = product.MasurementUnit.UnitName,
                ProductId = product.ProductId,
                QuantityInStorage = product.QuantityInStorage,
                ProductName = product.ProductName,
                ProductTypeName = product.ProductType.ProductTypeName
            };
        }

        private ProductUpdateDto MapProduct_UpdateDto(Product product)
        {
            return new ProductUpdateDto()
            {
                BuyingPrice = product.BuyingPrice,
                IsAvilable = product.IsAvailable,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SellingPrice = product.SellingPrice,
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
            decimal oldQuantity,
            string? Notes

            )
        {
            return new ProductStockMovementLogAddDto()
            {
                CreatedByUserId = UserId,
                ProductId = product.ProductId,
                Reason = Reason,
                NewQuantity = product.QuantityInStorage,
                OldQuantity = oldQuantity,
                Notes = Notes
            };
        }

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddProduct(ProductAddDto DTO,int UserId)
        {
            Product Product = MapProduct_AddDto(DTO);

            ValidationHelper.ValidateEntity(Product);

            using (var Transaction = _unitOfWork.BeginTransaction())
            {

                try
                {
                    _productRepo.Add(Product);

                    //To Get Id 
                    _unitOfWork.Save();


                    //Log
                    var logDto = MapProduct_StockMovementLogDto(
                                Product,
                                UserId,
                                StockMovementReason.InitialStock,
                                0,
                                null);

                    _stockMovementService.AddProductStockMovementLog(logDto);


                    _unitOfWork.Save();
                    Transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Failed to add product {ProductName}",
                        DTO.ProductName);

                    _logger.LogError(ex,
                       "Failed to change product {ProductId} quantity from {OldQuantity} by {QuantityChange} by user {UserId} for reason {Reason}",
                       Product.ProductId,
                       0,
                       Product.QuantityInStorage,
                       UserId,
                       StockMovementReason.InitialStock);

                    Transaction.Rollback();
                    throw new OperationFailedException(ex);
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

                    throw new OperationFailedException(ex);
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

                throw new OperationFailedException(ex);
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
            //Get Total Salse
            return MapProduct_ReadDto(product);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductListDto> GetAllProducts(int PageNumber, int RowsPerPage) 
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _productRepo
                .GetAllWithProductType_And_Unit(PageNumber, RowsPerPage)
                .Select(p => MapProduct_ListDto(p)).ToList();
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
        private void EditQuantity(int productId,decimal quantity,int userId,StockMovementReason reason,bool isAddition,string Notes)
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
                        oldQuantity,
                        Notes);

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

                    throw new OperationFailedException(ex);
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
        public void AddQuantity(int productId, decimal quantity, int userId, StockMovementReason reason, string Notes)
        {
            //Check For Increasing Reasons Only
            if (reason != StockMovementReason.Purchase && reason != StockMovementReason.Adjustment)
            {
                throw new OperationFailedException($"هذا السبب [{reason.GetDisplayName()}] لا يمكن أن يزيد من الكمية");
            }

            EditQuantity(productId, quantity,userId,reason,true, Notes);
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
        public void RemoveQuantity(int productId, decimal quantity, int userId, StockMovementReason reason, string Notes)
        {
            //Check For Decreasing Reasons Only
            if(reason!=StockMovementReason.Adjustment
                &&reason!= StockMovementReason.Sale
                &&reason!= StockMovementReason.Damage)
            {
                throw new OperationFailedException($"هذا السبب {reason.GetDisplayName()} لا يمكن أن يقلل من الكمية");
            }

            EditQuantity(productId, quantity, userId, reason, false, Notes);
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public ProductUpdateDto GetProductForUpdate(int ProductId)
        {
            Product product = _productRepo.GetById(ProductId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }
            return MapProduct_UpdateDto(product);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductListDto> GetAllByProductTypeName(int PageNumber, int RowsPerPage, string ProductTypeName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _productRepo.
                            GetAllByProductTypeName(PageNumber, RowsPerPage, ProductTypeName)
                            .Select(c => MapProduct_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductListDto> GetAllByFullName(int PageNumber, int RowsPerPage, string ProductTypeName, string ProductName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _productRepo.
                            GetAllByFullName(PageNumber, RowsPerPage, ProductTypeName, ProductName)
                            .Select(c => MapProduct_ListDto(c))
                            .ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByFullName(string ProductTypeName,string ProductName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _productRepo.GetTotalPagesByFullName(ProductTypeName, ProductName, RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByProductTypeName(string TownName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _productRepo.GetTotalPagesByProductTypeName(TownName, RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _productRepo.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void ChangeAvaliableState(int ProductId, bool State)
        {
            Product product = _productRepo.GetById(ProductId);

            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }

            product.IsAvailable = State;

            try
            {
                _unitOfWork.Save();
                _logger.LogInformation("تم تغيير حالة المنتج {ProductId} إلى {State}", ProductId, State);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل تغيير حالة إتاحة المنتج {ProductId}", ProductId);
                throw new OperationFailedException(ex);
            }
        }
    }
}
