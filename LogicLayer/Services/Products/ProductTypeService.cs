using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.DTOs.TownDTO;
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
    public class ProductTypeService
    {
        private readonly IRepository<ProductType> _productTypeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductTypeService> _logger;

        public ProductTypeService(IRepository<ProductType> productTypeRepository, IUnitOfWork unitOfWork,ILogger<ProductTypeService>logger)
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
            ProductType productType = _productTypeRepo.GetById(DTO.ProducTypetId);

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
                    DTO.ProducTypetId);

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

    }
}
