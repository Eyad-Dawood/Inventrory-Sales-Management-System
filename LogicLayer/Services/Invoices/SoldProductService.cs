using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.Services.Products;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using DataAccessLayer.Entities.DTOS;

namespace LogicLayer.Services.Invoices
{
    public class SoldProductService
    {

        private readonly ISoldProductRepository _SoldProductRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SoldProductService> _logger;
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;
        
        public SoldProductService(ISoldProductRepository SoldProductRepo, IUnitOfWork unitOfWork, ILogger<SoldProductService> logger, ProductService productService, CustomerService customerService)
        {
            _SoldProductRepo = SoldProductRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _productService = productService;
            _customerService = customerService;
        }

        #region Map

        private SoldProductListDto MapSoldProduct_ListDto(SoldProduct soldProduct)
        {
            return new SoldProductListDto
            {
                TakeDate = soldProduct.TakeBatch.TakeDate,
                BatchId = soldProduct.TakeBatchId,
                ProductFullName = $"{soldProduct.Product.ProductType.ProductTypeName}[{soldProduct.Product.ProductName}]",
                Quantity = soldProduct.Quantity,
                SellingPricePerUnit = soldProduct.SellingPricePerUnit,
                TotalSellingPrice = soldProduct.SellingPricePerUnit * soldProduct.Quantity,
                Reciver = soldProduct.TakeBatch.TakeName
            };
        }

        private SoldProductSaleDetailsListDto MapSoldProduct_WithProductListDto(SoldProduct soldProduct)
        {
            return new SoldProductSaleDetailsListDto
            {
                SoldProductId = soldProduct.SoldProductId,
                ProductId = soldProduct.ProductId,
                ProductName = soldProduct.Product.ProductName,
                ProductTypeName = soldProduct.Product.ProductType.ProductTypeName,
                Quantity = soldProduct.Quantity,
                IsAvilable = soldProduct.Product.IsAvailable,
                QuantityInStorage = soldProduct.Product.QuantityInStorage,
                SellingPricePerUnit = soldProduct.SellingPricePerUnit,
            };
        }
        private SoldProductSaleDetailsListDto MapSoldProduct_WithProductListDto(SoldProductForRefund soldProduct)
        {
            return new SoldProductSaleDetailsListDto
            {
                ProductId = soldProduct.ProductId,
                ProductName = soldProduct.ProductName,
                ProductTypeName = soldProduct.ProductTypeName,
                Quantity = soldProduct.Quantity,
                IsAvilable = soldProduct.IsAvilable,
                QuantityInStorage = soldProduct.QuantityInStorage,
                SellingPricePerUnit = soldProduct.SellingPricePerUnit,
            };
        }


        #endregion

        #region Helpers


        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        private async Task<List<SoldProduct>> ProcessSoldProductsAsyncForCreate(List<SoldProductAddDto> SoldProductsDTO,TakeBatchType takeBatchType)
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

                if (takeBatchType == TakeBatchType.Refund && item.QuantityInStorage < item.Quantity)
                    throw new OperationFailedException("لا يمكن إرجاع كمية أكبر من المأخوذة");


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

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<List<SoldProduct>> ProcessSoldProductsAsync(
            List<SoldProductAddDto> dtos,
            int userId,
            InvoiceType invoiceType,
            TakeBatchType takeBatchType,
            int CustomerId)
        {
            if (dtos == null || !dtos.Any())
                throw new OperationFailedException("لا توجد منتجات لإضافتها");

            var merged = dtos
                .GroupBy(p => p.ProductId)
                .Select(g => new SoldProductAddDto
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                    QuantityInStorage = g.First().QuantityInStorage,
                })
                .ToList();

            var soldProducts = await ProcessSoldProductsAsyncForCreate(merged,takeBatchType);

            //Withdraw
            

            if(invoiceType == InvoiceType.Sale)
            {
                if(takeBatchType == TakeBatchType.Invoice)
                {
                    //take from storage
                    await _productService.UpdateProductsQuantityAsync(
                        merged.Select(p => (p.ProductId, p.Quantity)).ToList(),
                        userId,
                        StockMovementReason.Sale,
                        isAddition: false);

                    //Take From Customer Balance -> it becomes (-) unitl he pays ,
                    await _customerService.WithdrawBalance
                        (CustomerId,
                        soldProducts.Sum(p => p.Quantity * p.SellingPricePerUnit)
                        );
                }
                else if (takeBatchType == TakeBatchType.Refund)
                {
                    //GiveBack from storage
                    await _productService.UpdateProductsQuantityAsync(
                        merged.Select(p => (p.ProductId, p.Quantity)).ToList(),
                        userId,
                        StockMovementReason.Refund,
                        isAddition: true);

                    //Add To Customer Balance , So It Gets Positive
                    await _customerService.DepositBalance
                        (CustomerId,
                        soldProducts.Sum(p => p.Quantity * p.SellingPricePerUnit)
                        );
                }
            }            

            return soldProducts;
        }

        #endregion

        #region ListData

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<SoldProductListDto>> GetAllWithDetailsByInvoiceIdAsync(int InvoiceId, int PageNumber, int RowsPerPage, List<TakeBatchType> takeBatchTypes)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _SoldProductRepo
                .GetAllWithDetailsByInvoiceIdAsync(PageNumber, RowsPerPage, InvoiceId, takeBatchTypes))
                .Select(i => MapSoldProduct_ListDto(i)
                ).ToList();
        }

        public async Task<List<SoldProductSaleDetailsListDto>> GetAllWithProductDetailsByInvoiceIdAsync(int InvoiceId, List<TakeBatchType> takeBatchTypes)
        {
            return
                (await _SoldProductRepo
                .GetAllWithDetailsByInvoiceIdAsync(InvoiceId, takeBatchTypes))
                .Select(i => MapSoldProduct_WithProductListDto(i)
                ).ToList();
        }

        public async Task<List<SoldProductSaleDetailsListDto>> GetAllSoldProductsToRefund(int InvoiceId)
        {
            return
                (await _SoldProductRepo
                .GetAllForRefundWithDetailsByInvoiceIdAsync(InvoiceId))
                .Select(i => MapSoldProduct_WithProductListDto(i)
                ).ToList();
        }
           
        #endregion

        #region PageNumber
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPagesByInvoiceIdAsync(int InvoiceId, int RowsPerPage, List<TakeBatchType> takeBatchTypes)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _SoldProductRepo.GetTotalPagesByInvoiceIdAsync(InvoiceId, RowsPerPage, takeBatchTypes);
        }
        #endregion

        #region Etc
        public async Task<decimal> GetTotalQuantitySoldByProductIdAsync(int ProductId)
        {
            return await _SoldProductRepo.GetTotalQuantitySoldByProductIdAsync(ProductId);
        }
        #endregion
    }

}
