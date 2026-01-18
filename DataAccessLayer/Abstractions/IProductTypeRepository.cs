using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        public Task<List<ProductType>> GetAllByProductTypeNameAsync(int PageNumber,int RowsPerPage,string ProductTypeName);

        public Task<int> GetTotalPagesByProductTypeNameAsync(string Name, int RowsPerPage);

    }
}
