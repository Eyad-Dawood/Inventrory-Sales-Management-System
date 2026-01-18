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

        public async Task<Worker?> GetWithDetailsByIdAsync(int WorkerId)
        {
            return
                await 
                _context.Workers
                 .AsNoTracking()
                 .Where(w => w.WorkerId == WorkerId)
                 .Include(w => w.Person)
                 .ThenInclude(p=>p.Town)
                 .FirstOrDefaultAsync();
        }
        public async Task<Worker?> GetWithPersonByIdAsync(int WorkerId)
        {
            return
                await
                _context.Workers
                .Where(w => w.WorkerId == WorkerId)
                .Include(w => w.Person)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Worker>> GetAllWithPersonAsync(int PageNumber, int RowsPerPage)
        {
            return
                await
                _context.Workers
                .AsNoTracking()
                .Include(w => w.Person)
                .ThenInclude(p => p.Town)
                .OrderBy(w => w.WorkerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }
        public async Task<List<Worker>> GetAllByFullNameAsync(
                int pageNumber,
                int rowsPerPage,
                string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Worker>();

            name = name.Trim();

            return
                await
                _context.Workers
                .AsNoTracking()
                .Include(c => c.Person)
                    .ThenInclude(p => p.Town)
                .Where(c =>
                    EF.Functions.Like(
                        c.Person.FullName,
                        $"{name}%"))
                .OrderBy(c => c.WorkerId)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Worker>> GetAllByTownNameAsync(int PageNumber, int RowsPerPage, string TownName)
        {
            if (string.IsNullOrWhiteSpace(TownName))
            {
                return new List<Worker>();
            }

            TownName = TownName.Trim();

            return
                await
                _context.Workers
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p => p.Town)
                .Where(c => EF.Functions.Like(
                    c.Person.Town.TownName,
                    $@"{TownName}%"))
                .OrderBy(c => c.WorkerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByFullNameAsync(string fullName, int rowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return 0;

            fullName = fullName.Trim();

            int totalCount =
                await
                _context.Workers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(
                        c.Person.FullName,
                        $"{fullName}%"))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)rowsPerPage);
        }

        public async Task<int> GetTotalPagesByTownNameAsync(string townName, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(townName))
                return 0;

            townName = townName.Trim();

            int totalCount =
                await
                _context.Workers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(c.Person.Town.TownName, $"{townName}%"))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        
       
    }
}
