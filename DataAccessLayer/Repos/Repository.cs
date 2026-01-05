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

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public int GetTotalPages(int RowsPerPage)
        {
            var totalCount = _context.Set<T>().Count();

            return (int)Math.Ceiling(totalCount / (double)RowsPerPage);
        }

        public List<T> GetAll()
        {
            return _context
                .Set<T>()
                .AsNoTracking()
                .ToList();
        }

        public List<T>GetAll(int PageNumber,int RowsPerPage)
        {
            return _context
                .Set<T>()
                .Skip((PageNumber - 1) * RowsPerPage)
                .Take(RowsPerPage)
                .AsNoTracking()
                .ToList();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }

}
