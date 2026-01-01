using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities.Products;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
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

        public ProductTypeService(IRepository<ProductType> productTypeRepository, IUnitOfWork unitOfWork)
        {
            _productTypeRepo = productTypeRepository;
            _unitOfWork = unitOfWork;
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


        public void AddProductType(ProductTypeAddDto DTO)
        {
            ProductType ProductType = MapProductType_AddDto(DTO);

            ValidationHelper.ValidateEntity(ProductType);

            _productTypeRepo.Add(ProductType);
            _unitOfWork.Save();
        }
        public void DeleteProductTypeById(int ProductTypeId)
        {
            ProductType ProductType = _productTypeRepo.GetById(ProductTypeId);

            if (ProductType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            _productTypeRepo.Delete(ProductType);
            _unitOfWork.Save();
        }
        public void UpdateProductType(ProductTypeUpdateDto DTO)
        {
            ProductType productType = _productTypeRepo.GetById(DTO.ProductId);
            if (productType == null)
            {
                throw new NotFoundException(typeof(ProductType));
            }

            ApplyProductTypeUpdate(productType,DTO);

            ValidationHelper.ValidateEntity(productType);

            _unitOfWork.Save();
        }
        public List<ProductTypeListDto> GetAllProductTypes()
        {
            return _productTypeRepo.GetAll()
                .Select(t => new ProductTypeListDto()
                {
                    Name = t.ProductTypeName,
                    ProductTypeId = t.ProductTypeId
                }).ToList();
        }

    }
}
