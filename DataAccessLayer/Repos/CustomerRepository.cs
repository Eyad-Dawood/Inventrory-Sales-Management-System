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

        public List<Customer> GetAllWithPerson(int PageNumber, int RowsPerPage)
        {
            return _context.Customers
                .Include(c => c.Person)
                .ThenInclude(p=>p.Town)
                .OrderBy(c => c.CustomerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
    }
}
