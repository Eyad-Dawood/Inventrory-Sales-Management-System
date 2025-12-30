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
        public Worker GetWithPerosnById(int WorkerId);
        public List<Worker> GetAllWithPerson(int PageNumber, int RowsPerPage);
    }
}
