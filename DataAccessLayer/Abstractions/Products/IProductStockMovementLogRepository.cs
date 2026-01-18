using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions.Products
{
    public interface IProductStockMovementLogRepository : IRepository<ProductStockMovementLog>
    {
        public Task<List<ProductStockMovementLog>> GetAllWithDetailsAsync(int PageNumber, int RowsPerPage);

        public Task<List<ProductStockMovementLog>> GetAllByProductFullNameAndDateAsync(int pageNumber,
            int rowsPerPage,
            string? productFullName,
            DateTime? date);
        public Task<int> GetTotalPagesByFullNameAndDateAsync(
            int rowsPerPage,
            string? productFullName,
            DateTime? date);

    }
}
