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
        public List<Customer> GetAllWithPerson(int PageNumber,int RowsPerPage);
    }
}
