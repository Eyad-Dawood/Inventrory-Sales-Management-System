using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Helpers
{
    public static class ServiceHelper
    {
        public static CustomerService CreateCustomerService(InventoryDbContext dbContext)
        {

                var CustomerRepo = new CustomerRepository(dbContext);
                var UOW = new UnitOfWork(dbContext);
                var PersonRepo = new Repository<Person>(dbContext);

                var CustomerService = new CustomerService(CustomerRepo, PersonRepo, UOW);

                return CustomerService;
        }

        public static TownService CreateTownService(InventoryDbContext dbContext)
        {
            var Uow = new UnitOfWork(dbContext);
            var repo = new Repository<Town>(dbContext);
            var service = new TownService(repo, Uow);

            return service;
        }
    }
}
