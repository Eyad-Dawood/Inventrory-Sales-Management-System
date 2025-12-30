using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using LogicLayer.DTOs;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IRepository<Person> _personRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepo, IRepository<Person> perosnRepo, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _personRepo = perosnRepo;
            _unitOfWork = unitOfWork;
        }


        /// <exception cref="ValidationException">
        /// Thrown when the Customer or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void AddCustomer(Customer customer)
        {
            ValidationHelper.ValidateEntity(customer);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                _personRepo.Add(customer.Person);

                customer.PersonId = customer.Person.PersonId;


                _customerRepo.Add(customer);
                _unitOfWork.Save();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


        }

        /// <exception cref="ValidationException">
        /// Thrown when the patient or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void UpdateCustomer(Customer customer)
        {
            ValidationHelper.ValidateEntity(customer);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {

                _personRepo.Update(customer.Person);


                _customerRepo.Update(customer);
                _unitOfWork.Save();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = _customerRepo.GetById(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                _customerRepo.Delete(customer);

                _personRepo.Delete(customer.Person);
                _unitOfWork.Save();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return customer;
        }

        public List<CustomerListDto> GetAllCustomers(int PageNumber,int RowsPerPage)
        {
            return _customerRepo.
                GetAllWithPerson(PageNumber,RowsPerPage)
                .Select(c=>new CustomerListDto 
                {
                CustomerId =c.CustomerId,
                FullName = c.Person.FullName,
                PhoneNumber = c.Person.PhoneNumber,
                TownName = c.Person.Town.TownName,
                Balance = c.Balance,
                IsActive = c.IsActive
                })
                .ToList();
        }

    }
}
