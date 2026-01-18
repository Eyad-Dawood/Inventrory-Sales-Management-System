using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Abstractions
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        public Task<Worker?> GetWithPersonByIdAsync(int WorkerId);
        public Task<List<Worker>> GetAllWithPersonAsync(int PageNumber, int RowsPerPage);
        public Task<Worker?> GetWithDetailsByIdAsync(int WorkerId);
        public Task<List<Worker>> GetAllByFullNameAsync(int PageNumber, int RowsPerPage, string FullName);
        public Task<List<Worker>> GetAllByTownNameAsync(int PageNumber, int RowsPerPage, string townName);
        public Task<int> GetTotalPagesByFullNameAsync(string Name, int RowsPerPage);
        public Task<int> GetTotalPagesByTownNameAsync(string TownName, int RowsPerPage);
    }
}
