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

        public async Task<Customer?> GetWithPersonByIdAsync(int CustomerId)
        {
            return 
                await
                _context.Customers
                .Where(c => c.CustomerId == CustomerId)
                .Include(c => c.Person)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer?> GetWithDetailsByIdAsync(int CustomerId)
        {
            return
                await
                _context.Customers
                .Where(c => c.CustomerId == CustomerId)
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p=>p.Town)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Customer>> GetAllWithPersonAsync(int PageNumber, int RowsPerPage)
        {
            return 
                await 
                _context.Customers
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p=>p.Town)
                .OrderBy(c => c.CustomerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<List<Customer>> GetAllByFullNameAsync(
                int pageNumber,
                int rowsPerPage,
                string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Customer>();

            name = name.Trim();

            return await 
                _context.Customers
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
                .ToListAsync();
        }


        public async Task<List<Customer>> GetAllByTownNameAsync(int PageNumber,int RowsPerPage,string TownName)
        {
            if (string.IsNullOrWhiteSpace(TownName))
            {
                return new List<Customer>();
            }

            TownName = TownName.Trim();

            return await 
                _context.Customers
                .AsNoTracking()
                .Include(c => c.Person)
                .ThenInclude(p => p.Town)
                .Where(c => EF.Functions.Like(
                    c.Person.Town.TownName,
                    $@"{TownName}%"))
                .OrderBy(c => c.CustomerId)
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesByFullNameAsync(string fullName, int rowsPerPage)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return 0;

            fullName = fullName.Trim();

            int totalCount = await 
                _context.Customers
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

            int totalCount = await 
                _context.Customers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(c.Person.Town.TownName, $"{townName}%"))
                .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<bool> HasOpenInvoice(int customerId)
        {
           return await _context.Invoices.
                AnyAsync(i=> i.CustomerId == customerId && i.InvoiceState == Entities.Invoices.InvoiceState.Open);
        }
    }
}
