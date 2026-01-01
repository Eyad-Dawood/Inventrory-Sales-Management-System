using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Products
{
    public interface IProductPriceLogRepository : IRepository<ProductPriceLog>
    {
        public List<ProductPriceLog> GetAllWithDetails(int PageNumber,int RowsPerPage);
    }
}
