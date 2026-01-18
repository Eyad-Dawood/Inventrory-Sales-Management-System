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
        public Task<List<ProductPriceLog>> GetAllWithDetailsAsync(int PageNumber,int RowsPerPage);

        public Task<List<ProductPriceLog>> GetAllByProductFullNameAndDateAsync(int pageNumber,
            int rowsPerPage,
            string? productFullName,
            DateTime? date);
        public Task<int> GetTotalPagesByFullNameAndDateAsync(
            int rowsPerPage,
            string? productFullName,
            DateTime? date);
    }
}
