using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repos
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(InventoryDbContext context) : base(context)
        {
        }

        public Worker GetWithPerosnById(int WorkerId)
        {
            return _context.Workers
                .Where(w => w.WorkerId == WorkerId)
                .Include(w => w.Person)
                .FirstOrDefault();
        }

        public List<Worker> GetAllWithPerson(int PageNumber, int RowsPerPage)
        {
            return _context.Workers
                .Include(w => w.Person)
                .ThenInclude(p => p.Town)
                .OrderBy(w => w.WorkerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
    }
}
