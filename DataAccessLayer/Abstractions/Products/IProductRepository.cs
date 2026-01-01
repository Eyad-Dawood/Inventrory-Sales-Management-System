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

    }
}
