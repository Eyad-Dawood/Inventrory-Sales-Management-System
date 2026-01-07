using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;

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

        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddProductType(ProductTypeAddDto DTO)
        {
            ProductType ProductType = MapProductType_AddDto(DTO);

            ValidationHelper.ValidateEntity(ProductType);

            _productTypeRepo.Add(ProductType);

            try
            {
                _unitOfWork.Save();
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
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void DeleteProductTypeById(int ProductTypeId)
        {
            ProductType ProductType = _productTypeRepo.GetById(ProductTypeId);

            if (ProductType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            _productTypeRepo.Delete(ProductType);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to Delete productType {Id}",
                    ProductTypeId);

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
        public void UpdateProductType(ProductTypeUpdateDto DTO)
        {
            ProductType productType = _productTypeRepo.GetById(DTO.ProductTypeId);

            if (productType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            ApplyProductTypeUpdate(productType,DTO);

            ValidationHelper.ValidateEntity(productType);

            try
            {
                _unitOfWork.Save();
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
        public List<ProductTypeListDto> GetAllProductTypes(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return _productTypeRepo.GetAll(PageNumber,RowsPerPage)
                .Select(t => new ProductTypeListDto()
                {
                    Name = t.ProductTypeName,
                    ProductTypeId = t.ProductTypeId
                }).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public ProductTypeUpdateDto GetProductTypeForUpdate(int ProductTypeId)
        {
            ProductType ProductType = _productTypeRepo.GetById(ProductTypeId);

            if (ProductType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            return MapProductType_UpdateDto(ProductType);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<ProductTypeListDto> GetAllByProductTypeName(int PageNumber, int RowsPerPage, string TypeName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _productTypeRepo.
                            GetAllByProductTypeName(PageNumber, RowsPerPage, TypeName)
                            .Select(p => MapProductType_ListDto(p))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPagesByProductTypeName(string Name, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _productTypeRepo.GetTotalPagesByProductTypeName(Name, RowsPerPage);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _productTypeRepo.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void DeleteById(int ProductTypeId)
        {
            ProductType productType = _productTypeRepo.GetById(ProductTypeId);

            if (productType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            _productTypeRepo.Delete(ProductTypeId);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   "Failed to Delete ProductType {TypeId}",
                   ProductTypeId);

                throw new OperationFailedException(ex);
            }
        }
    }
}
