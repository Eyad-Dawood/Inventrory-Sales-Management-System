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
        public List<ProductType> GetAllByProductTypeName(int PageNumber,int RowsPerPage,string ProductTypeName);

        public int GetTotalPagesByProductTypeName(string Name, int RowsPerPage);

    }
}
