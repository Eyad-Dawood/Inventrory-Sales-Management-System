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
        public Worker GetWithPersonById(int WorkerId);
        public List<Worker> GetAllWithPerson(int PageNumber, int RowsPerPage);
        public Worker GetWithDetailsById(int WorkerId);

        public List<Worker> GetAllByFullName(int PageNumber, int RowsPerPage, string FullName);
        public List<Worker> GetAllByTownName(int PageNumber, int RowsPerPage, string townName);
        public int GetTotalPagesByFullName(string Name, int RowsPerPage);
        public int GetTotalPagesByTownName(string TownName, int RowsPerPage);
    }
}
