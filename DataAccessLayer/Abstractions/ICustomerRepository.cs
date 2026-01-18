using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<Customer?> GetWithPersonByIdAsync(int CustomerId);
        public Task<Customer?> GetWithDetailsByIdAsync(int CustomerId);
        public Task<List<Customer>> GetAllWithPersonAsync(int PageNumber,int RowsPerPage);
        public Task<List<Customer>> GetAllByFullNameAsync(int PageNumber, int RowsPerPage,string FullName);
        public Task<List<Customer>> GetAllByTownNameAsync(int PageNumber, int RowsPerPage,string townName);
        public Task<int> GetTotalPagesByFullNameAsync(string Name,int RowsPerPage);
        public Task<int> GetTotalPagesByTownNameAsync(string TownName,int RowsPerPage);
    }
}
