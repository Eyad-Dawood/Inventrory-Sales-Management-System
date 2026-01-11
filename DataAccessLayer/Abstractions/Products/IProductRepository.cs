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
        public Product GetWithProductType_And_UnitById(int id);

        public List<Product> GetAllWithProductType_And_Unit(int PageNumber, int RowsPerPage);

        public List<Product> GetAllByFullName(int PageNumber, int RowsPerPage, string ProductTypeName, string ProductName);
        public List<Product> GetAllByProductTypeName(int PageNumber, int RowsPerPage, string ProductTypeName);
        public int GetTotalPagesByFullName(string ProductTypeName,string ProductName, int RowsPerPage);
        public int GetTotalPagesByProductTypeName(string ProductTypeName, int RowsPerPage);

    }
}
