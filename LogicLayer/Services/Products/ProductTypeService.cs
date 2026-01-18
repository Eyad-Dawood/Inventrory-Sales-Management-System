using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LogicLayer.Services.Products
{
    public class ProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductTypeService> _logger;

        public ProductTypeService(IProductTypeRepository productTypeRepository, IUnitOfWork unitOfWork,ILogger<ProductTypeService>logger)
        {
            _productTypeRepo = productTypeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region 
        private ProductType MapProductType_AddDto(ProductTypeAddDto DTO)
        {
            return new ProductType()
            {
                ProductTypeName = DTO.Name,
            };
        }
        private void ApplyProductTypeUpdate(ProductType productType,ProductTypeUpdateDto DTO)
        {
            productType.ProductTypeName = DTO.Name;
        }

        private ProductTypeUpdateDto MapProductType_UpdateDto(ProductType product)
        {
            return new ProductTypeUpdateDto()
            {
                Name = product.ProductTypeName,
                ProductTypeId = product.ProductTypeId
            };
        }


        private ProductTypeListDto MapProductType_ListDto(ProductType product)
        {
            return new ProductTypeListDto()
            {
                ProductTypeId = product.ProductTypeId,
                Name = product.ProductTypeName
            };
        }

        private ProductTypeReadDto MapProductType_ReadDto(ProductType productType)
        {
            return new ProductTypeReadDto()
            {
                Name = productType.ProductTypeName
            };
        }
        #endregion


        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddProductTypeAsync(ProductTypeAddDto DTO)
        {
            ProductType ProductType = MapProductType_AddDto(DTO);

            ValidationHelper.ValidateEntity(ProductType);

            await _productTypeRepo.AddAsync(ProductType);

            try
            {
               await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to add productType {ProductName}",
                    DTO.Name);

                throw new OperationFailedException(ex);
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
        public async Task UpdateProductTypeAsync(ProductTypeUpdateDto DTO)
        {
            ProductType? productType = await _productTypeRepo.GetByIdAsync(DTO.ProductTypeId);

            if (productType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            ApplyProductTypeUpdate(productType,DTO);

            ValidationHelper.ValidateEntity(productType);

            try
            {
               await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to Update productType {Id}",
                    DTO.ProductTypeId);

                throw new OperationFailedException(ex);
            }
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductTypeListDto>> GetAllProductTypesAsync(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return (await _productTypeRepo.GetAllAsync(PageNumber, RowsPerPage))
                .Select(t => new ProductTypeListDto()
                {
                    Name = t.ProductTypeName,
                    ProductTypeId = t.ProductTypeId
                }).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<ProductTypeUpdateDto> GetProductTypeForUpdateAsync(int ProductTypeId)
        {
            ProductType? ProductType = await _productTypeRepo.GetByIdAsync(ProductTypeId);

            if (ProductType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            return MapProductType_UpdateDto(ProductType);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<ProductTypeListDto>> GetAllByProductTypeNameAsync(int PageNumber, int RowsPerPage, string TypeName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _productTypeRepo.
                            GetAllByProductTypeNameAsync(PageNumber, RowsPerPage, TypeName))
                            .Select(p => MapProductType_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPagesByProductTypeNameAsync(string Name, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return ( await _productTypeRepo.GetTotalPagesByProductTypeNameAsync(Name, RowsPerPage));
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return ( await _productTypeRepo.GetTotalPagesAsync(RowsPerPage));
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task DeleteByIdAsync(int ProductTypeId)
        {
            ProductType? productType = await _productTypeRepo.GetByIdAsync(ProductTypeId);

            if (productType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            _productTypeRepo.Delete(productType);

            try
            {
               await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   "Failed to Delete ProductType {TypeId}",
                   ProductTypeId);

                throw new OperationFailedException(ex);
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<ProductTypeReadDto> GetProductTypeByIdAsync(int ProductTypeId)
        {
            ProductType? ProductType = await _productTypeRepo.GetByIdAsync(ProductTypeId);

            if (ProductType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            return MapProductType_ReadDto(ProductType);
        }
    }
}
