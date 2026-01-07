using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Customer GetWithPersonById(int CustomerId);
        public Customer GetWithDetailsById(int CustomerId);
        public List<Customer> GetAllWithPerson(int PageNumber,int RowsPerPage);
        public List<Customer> GetAllByFullName(int PageNumber, int RowsPerPage,string FullName);
        public List<Customer> GetAllByTownName(int PageNumber, int RowsPerPage,string townName);
        public int GetTotalPagesByFullName(string Name,int RowsPerPage);
        public int GetTotalPagesByTownName(string TownName,int RowsPerPage);
    }
}
