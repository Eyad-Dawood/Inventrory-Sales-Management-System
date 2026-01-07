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

        public Worker GetWithDetailsById(int WorkerId)
        {
            return _context.Workers
                 .AsNoTracking()
                 .Where(w => w.WorkerId == WorkerId)
                 .Include(w => w.Person)
                 .ThenInclude(p=>p.Town)
                 .FirstOrDefault();
        }
        public Worker GetWithPersonById(int WorkerId)
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
        public List<Worker> GetAllByFullName(
                int pageNumber,
                int rowsPerPage,
                string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Worker>();

            name = name.Trim();

            return _context.Workers
                .AsNoTracking()
                .Include(c => c.Person)
                    .ThenInclude(p => p.Town)
                .Where(c =>
                    EF.Functions.Like(
                        (c.Person.FirstName ?? "") + " " +
                        (c.Person.SecondName ?? "") + " " +
                        (c.Person.ThirdName ?? "") + " " +
                        (c.Person.FourthName ?? ""),
                        $"{name}%"))
                .OrderBy(c => c.WorkerId)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();
        }

        public List<Worker> GetAllByTownName(int PageNumber, int RowsPerPage, string TownName)
        {
            if (string.IsNullOrWhiteSpace(TownName))
            {
                return new List<Worker>();
            }

            TownName = TownName.Trim();

            return _context.Workers
                .Include(c => c.Person)
                .ThenInclude(p => p.Town)
                .Where(c => EF.Functions.Like(
                    c.Person.Town.TownName,
                    $@"{TownName}%"))
                .OrderBy(c => c.WorkerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }

        public int GetTotalPagesByFullName(string fullName, int rowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return 0;

            fullName = fullName.Trim();

            int totalCount = _context.Workers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(
                        (c.Person.FirstName ?? "") + " " +
                        (c.Person.SecondName ?? "") + " " +
                        (c.Person.ThirdName ?? "") + " " +
                        (c.Person.FourthName ?? ""),
                        $"{fullName}%"))
                .Count();

            return (int)Math.Ceiling(totalCount / (double)rowsPerPage);
        }

        public int GetTotalPagesByTownName(string townName, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(townName))
                return 0;

            townName = townName.Trim();

            int totalCount = _context.Workers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(c.Person.Town.TownName, $"{townName}%"))
                .Count();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }


       
    }
}
