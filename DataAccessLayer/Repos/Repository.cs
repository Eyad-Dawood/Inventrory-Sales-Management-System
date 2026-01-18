using DataAccessLayer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InventoryDbContext _context;

        public Repository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<int> GetTotalPagesAsync(int RowsPerPage)
        {
            var totalCount = await _context
                                    .Set<T>()
                                    .AsNoTracking()
                                    .CountAsync();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(int PageNumber,int RowsPerPage)
        {
            return 
                await 
                _context
                .Set<T>()
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }

}
