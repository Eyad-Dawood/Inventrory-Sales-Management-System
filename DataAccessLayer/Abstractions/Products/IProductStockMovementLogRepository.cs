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
        public List<ProductStockMovementLog> GetAllWithDetails(int PageNumber, int RowsPerPage);

        public List<ProductStockMovementLog> GetAllByProductFullNameAndDate(int pageNumber,
            int rowsPerPage,
            string? productFullName,
            DateTime? date);
        public int GetTotalPagesByFullNameAndDate(
            int rowsPerPage,
            string? productFullName,
            DateTime? date);

    }
}
