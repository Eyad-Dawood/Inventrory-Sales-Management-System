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
    public class CustomerRepository:Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InventoryDbContext context) : base(context)
        {
        }

        public Customer GetWithPersonById(int CustomerId)
        {
            return _context.Customers
                .Where(c => c.CustomerId == CustomerId)
                .Include(c => c.Person)
                .FirstOrDefault();
        }

        public Customer GetWithDetailsById(int CustomerId)
        {
            return _context.Customers
                .Where(c => c.CustomerId == CustomerId)
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p=>p.Town)
                .FirstOrDefault();
        }
        public List<Customer> GetAllWithPerson(int PageNumber, int RowsPerPage)
        {
            return _context.Customers
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p=>p.Town)
                .OrderBy(c => c.CustomerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToList();
        }

        public List<Customer> GetAllByFullName(
                int pageNumber,
                int rowsPerPage,
                string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Customer>();

            name = name.Trim();

            return _context.Customers
                .AsNoTracking()
                .Include(c => c.Person)
                    .ThenInclude(p => p.Town)
                .Where(c =>
                    EF.Functions.Like(
                        c.Person.FullName,
                        $"{name}%"))
                .OrderBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToList();
        }


        public List<Customer> GetAllByTownName(int PageNumber,int RowsPerPage,string TownName)
        {
            if (string.IsNullOrWhiteSpace(TownName))
            {
                return new List<Customer>();
            }

            TownName = TownName.Trim();

            return _context.Customers
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p => p.Town)
                .Where(c => EF.Functions.Like(
                    c.Person.Town.TownName,
                    $@"{TownName}%"))
                .OrderBy(c => c.CustomerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToList();
        }

        public int GetTotalPagesByFullName(string fullName, int rowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return 0;

            fullName = fullName.Trim();

            int totalCount = _context.Customers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(
                        c.Person.FullName,
                        $"{fullName}%"))
                .Count();

            return (int)Math.Ceiling(totalCount / (double)rowsPerPage);
        }



        public int GetTotalPagesByTownName(string townName, int RowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(townName))
                return 0;

            townName = townName.Trim();

            int totalCount = _context.Customers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(c.Person.Town.TownName, $"{townName}%"))
                .Count();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }
    }
}
