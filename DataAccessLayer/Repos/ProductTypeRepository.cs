using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class ProductTypeRepository : Repository<ProductType>,IProductTypeRepository
    {

        public ProductTypeRepository(InventoryDbContext context):base(context)
        {
            
        }

        public List<ProductType> GetAllByProductTypeName(int PageNumber,int RowsPerPage,string ProductTypeName)
        {
            if (string.IsNullOrWhiteSpace(ProductTypeName))
            {
                return new List<ProductType>();
            }

            ProductTypeName = ProductTypeName.Trim();

            return _context.ProductTypes
                .Where(p => EF.Functions.Like(
                    p.ProductTypeName,
                    $@"{ProductTypeName}%"))
                .OrderBy(p => p.ProductTypeId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }

        public int GetTotalPagesByProductTypeName(string Name, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return 0;

            Name = Name.Trim();

            int totalCount = _context.ProductTypes
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(p.ProductTypeName, $"{Name}%"))
                .Count();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        
    }
}
