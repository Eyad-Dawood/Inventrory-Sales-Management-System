using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.InvoiceDTO;
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

        public SoldProductService(ISoldProductRepository SoldProductRepo, IUnitOfWork unitOfWork, ILogger<SoldProductService> logger, ProductService productService)
        {
            _SoldProductRepo = SoldProductRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _productService = productService;
        }

        #region Map

        private SoldProductMiniReadDto MapSoldProduct_MiniReadDto(SoldProduct soldProduct)
        {
            return new SoldProductMiniReadDto
            {
                TakeDate = soldProduct.TakeBatch.TakeDate,
                BatchId = soldProduct.TakeBatchId,
                ProductFullName = $"{soldProduct.Product.ProductType.ProductTypeName}[{soldProduct.Product.ProductName}]",
                Quantity = soldProduct.Quantity,
                UnitName = soldProduct.Product.MasurementUnit.UnitName
            };
        }

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
                Product? product = products.FirstOrDefault(p => p.ProductId == item.ProductId);

                if (product == null)
                    throw new NotFoundException(typeof(Product), item.ProductId.ToString());

                if (!product.IsAvailable)
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

        public async Task<List<SoldProduct>> PrepareSoldProductsAsync(
            List<SoldProductAddDto> dtos,
            int userId,
            InvoiceType invoiceType)
        {
            if (dtos == null || !dtos.Any())
                throw new OperationFailedException("لا توجد منتجات لإضافتها");

            var merged = dtos
                .GroupBy(p => p.ProductId)
                .Select(g => new SoldProductAddDto
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            var soldProducts = await PrepareSoldProductsForCreate(merged);

            if (invoiceType == InvoiceType.Sale)
            {
                await _productService.UpdateProductsQuantityAsync(
                    merged.Select(p => (p.ProductId, p.Quantity)).ToList(),
                    userId,
                    StockMovementReason.Sale,
                    isAddition: false);
            }

            return soldProducts;
        }
        #endregion

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _SoldProductRepo.GetTotalPagesByInvoiceIdAsync(InvoiceId, RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<SoldProductMiniReadDto>> GetAllWithDetailsByInvoiceIdAsync(int InvoiceId, int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _SoldProductRepo
                .GetAllWithDetailsByInvoiceIdAsync(PageNumber,RowsPerPage,InvoiceId))
                .Select(i => MapSoldProduct_MiniReadDto(i)
                ).ToList();
        }

    }


}
