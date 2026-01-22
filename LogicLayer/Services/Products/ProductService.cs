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
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

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

        #region Map
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
        #endregion


        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddProductAsync(ProductAddDto DTO,int UserId)
        {
            Product Product = MapProduct_AddDto(DTO);

            ValidationHelper.ValidateEntity(Product);

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {

                try
                {
                    await _productRepo.AddAsync(Product);

                    //To Get Id 
                    await _unitOfWork.SaveAsync();


                    //Log
                    var logDto = MapProduct_StockMovementLogDto(
                                Product,
                                UserId,
                                StockMovementReason.InitialStock,
                                0,
                                "كمية افتتاحية عند إضافة المنتج لأول مرة");

                   await _stockMovementService.AddProductStockMovementLogAsync(logDto);


                    await _unitOfWork.SaveAsync();
                   await Transaction.CommitAsync();
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

                    await Transaction.RollbackAsync();
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
        public async Task UpdateProductAsync(ProductUpdateDto dto, int userId)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new NotFoundException(typeof(Product));

            var oldBuyingPrice = product.BuyingPrice;
            var oldSellingPrice = product.SellingPrice;

            ApplyProductUpdate(product, dto);
            ValidationHelper.ValidateEntity(product);


            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
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

                       await _pricelogService.AddProductPriceLogAsync(logDto);
                    }

                    await _unitOfWork.SaveAsync();

                    await Transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Failed to update product {ProductId} by user {UserId}",
                        dto.ProductId,
                        userId);

                    await Transaction.RollbackAsync();

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
        public async Task DeleteProductByIdAsync(int ProductId)
        {
            var Product = await _productRepo.GetByIdAsync(ProductId);

            if (Product == null)
            {
                throw new NotFoundException(typeof(Product));
            }


            try
            {
                _productRepo.Delete(Product);
                await _unitOfWork.SaveAsync();
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
        public async Task<ProductReadDto> GetProductByIdAsync(int ProductId) 
        {
            Product? product = await _productRepo.GetWithProductType_And_UnitByIdAsync(ProductId);
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
        public async Task<List<ProductListDto>> GetAllProductsAsync(int PageNumber, int RowsPerPage) 
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await 
                _productRepo
                .GetAllWithProductType_And_UnitAsync(PageNumber, RowsPerPage))
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
        public async Task UpdateProductsQuantityAsync(List<(int ProductId,decimal Quantity)> items, int userId, StockMovementReason reason, bool isAddition)
        {
            var ids = items.Select(x => x.ProductId).ToList();
            var products = await _productRepo.GetProductsByIdsAsync(ids);

                foreach (var item in items)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);

                    if (product == null)
                        throw new NotFoundException(typeof(Product));

                    await EditQuantityAsync(product, item.Quantity, userId, reason, isAddition, string.Empty);
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
        private async Task EditQuantityAsync(Product product,decimal quantity,int userId,StockMovementReason reason,bool isAddition,string Notes)
        {
            //Only Checks For quantity , decreas/increase it , validate , but no Save In DB
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

            //Log But Dont Save
            var logDto = MapProduct_StockMovementLogDto(
                        product,
                        userId,
                        reason,
                        oldQuantity,
                        Notes);

            await _stockMovementService.AddProductStockMovementLogAsync(logDto);


            ValidationHelper.ValidateEntity(product);
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
        public async Task AddQuantityAsync(int productId, decimal quantity, int userId, StockMovementReason reason, string Notes)
        {
            //Check For Increasing Reasons Only
            if (reason != StockMovementReason.Purchase && reason != StockMovementReason.Adjustment)
            {
                throw new OperationFailedException($"هذا السبب [{reason.GetDisplayName()}] لا يمكن أن يزيد من الكمية");
            }

            //Tracking
            var product = await _productRepo.GetByIdAsync(productId);

            await EditQuantityAsync(product, quantity,userId,reason, isAddition : true, Notes);

            //Save 
            await _unitOfWork.SaveAsync();
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
        public async Task RemoveQuantityAsync(int productId, decimal quantity, int userId, StockMovementReason reason, string Notes)
        {
            //Check For Decreasing Reasons Only
            if(reason!=StockMovementReason.Adjustment
                &&reason!= StockMovementReason.Sale
                &&reason!= StockMovementReason.Damage)
            {
                throw new OperationFailedException($"هذا السبب {reason.GetDisplayName()} لا يمكن أن يقلل من الكمية");
            }

            //Tracking
            var product = await _productRepo.GetByIdAsync(productId);

            await EditQuantityAsync(product, quantity, userId, reason, isAddition : false, Notes);

            //Save 
            await _unitOfWork.SaveAsync();
        }


         


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<ProductUpdateDto> GetProductForUpdateAsync(int ProductId)
        {
            Product? product = await _productRepo.GetByIdAsync(ProductId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }
            return MapProduct_UpdateDto(product);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductListDto>> GetAllByProductTypeNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return 
                (await _productRepo.
                            GetAllByProductTypeNameAsync(PageNumber, RowsPerPage, ProductTypeName))
                            .Select(c => MapProduct_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public  async Task<List<ProductListDto>> GetAllByFullNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName, string ProductName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return 
                (await _productRepo.
                            GetAllByFullNameAsync(PageNumber, RowsPerPage, ProductTypeName, ProductName))
                            .Select(c => MapProduct_ListDto(c))
                            .ToList();
        }

        public async Task<List<ProductListDto>> GetProductsByIdsAsync(List<int>Ids)
        {
            return
                 (await _productRepo.
                             GetProductsByIdsAsync(Ids))
                             .Select(c => MapProduct_ListDto(c))
                             .ToList();
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByFullNameAsync(string ProductTypeName,string ProductName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _productRepo.GetTotalPagesByFullNameAsync(ProductTypeName, ProductName, RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByProductTypeNameAsync(string TownName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _productRepo.GetTotalPagesByProductTypeNameAsync(TownName, RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _productRepo.GetTotalPagesAsync(RowsPerPage);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task ChangeAvaliableStateAsync(int ProductId, bool State)
        {
            Product? product = await _productRepo.GetByIdAsync(ProductId);

            if (product == null)
            {
                throw new NotFoundException(typeof(Product));
            }

            product.IsAvailable = State;

            try
            {
                await _unitOfWork.SaveAsync();
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
