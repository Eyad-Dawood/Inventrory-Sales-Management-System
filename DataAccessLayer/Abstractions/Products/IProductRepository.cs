using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities.Products;

namespace DataAccessLayer.Abstractions.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<Product?> GetWithProductType_And_UnitByIdAsync(int id);

        public Task<List<Product>> GetAllWithProductType_And_UnitAsync(int PageNumber, int RowsPerPage);

        public Task<List<Product>> GetAllByFullNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName, string ProductName);
        public Task<List<Product>> GetAllByProductTypeNameAsync(int PageNumber, int RowsPerPage, string ProductTypeName);
        public Task<int> GetTotalPagesByFullNameAsync(string ProductTypeName,string ProductName, int RowsPerPage);
        public Task<int> GetTotalPagesByProductTypeNameAsync(string ProductTypeName, int RowsPerPage);

    }
}
