using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.Services.Products;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Invoices
{
    public class SoldProductService
    {

        private readonly ISoldProductRepository _SoldProductRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SoldProductService> _logger;
        private readonly ProductService _productService;

        public SoldProductService(ISoldProductRepository SoldProductRepo, IUnitOfWork unitOfWork, ILogger<SoldProductService> logger , ProductService productService)
        {
            _SoldProductRepo = SoldProductRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _productService = productService;
        }

        #region Map

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<List<SoldProduct>> PrepareSoldProductsForCreate(List<SoldProductAddDto> SoldProductsDTO)
        {
            List<SoldProduct> result = new List<SoldProduct>();

            var productIds = SoldProductsDTO.Select(p => p.ProductId).Distinct().ToList();

            var products = await _productService.GetProductsByIdsAsync(productIds);

            foreach (var item in SoldProductsDTO)
            {
                //Get Product Data
                var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);

                if (product == null)
                    throw new NotFoundException(typeof(Product),item.ProductId.ToString());

                if (!product.IsAvilable)
                    throw new OperationFailedException("لا يمكن الشراء من متج موقوف/غير متاح");

                var SoldProduct = new SoldProduct()
                {
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    TakeBatchId = item.TakeBatchId,
                    BuyingPricePerUnit = product.BuyingPrice,
                    SellingPricePerUnit = product.SellingPrice,
                };

                //If Failed Throw An Exception
                ValidationHelper.ValidateEntity(SoldProduct);


                result.Add(SoldProduct);
            }

            return result;
        }


        #endregion

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task<List<int>> AddProductsAsync(List<SoldProductAddDto> SoldProductsDTO,int UserId,DataAccessLayer.Entities.Invoices.InvoiceType invoiceType)
        {
            if (SoldProductsDTO == null || !SoldProductsDTO.Any())
                throw new OperationFailedException("لا توجد منتجات لإضافتها");

            using (var Transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    List<SoldProduct> SoldProducts = await PrepareSoldProductsForCreate(SoldProductsDTO);

                    await _SoldProductRepo.AddRangeAsync(SoldProducts);

                    if (invoiceType == InvoiceType.Sale)
                    {
                      await  _productService.UpdateProductsQuantityAsync(
                                SoldProductsDTO
                                    .Select(p => (p.ProductId, p.Quantity))
                                    .ToList(),
                                UserId,
                                StockMovementReason.Sale,
                                isAddition: false
                            );
                    }

                    await _unitOfWork.SaveAsync();


                    await Transaction.CommitAsync();
                    return SoldProducts.Select(b => b.SoldProductId).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                    "Exception When Trying To Add Sold Products With ProductIds {Ids}",
                    string.Join(" , ",SoldProductsDTO.Select(b=>b.ProductId).ToList()));

                    
                    await Transaction.RollbackAsync();

                    throw;
                }
            }
        }
    }
}
