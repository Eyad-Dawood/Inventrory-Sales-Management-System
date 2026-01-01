using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Services.Helpers;

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


        private Customer MapCustomer_AddDto(CustomerAddDto DTO)
        {
            return new Customer()
            {
                Person = PersonService.MapPerosn_AddDto(DTO.PersonAddDto)
            };
        }
        private CustomerReadDto MapCustomer_ReadDto(Customer customer)
        {
            return new CustomerReadDto()
            {
                CustomerId = customer.CustomerId,
                PersonId = customer.PersonId,
                Balance = customer.Balance,
                IsActive = customer.IsActive,
                FullName = customer.Person.FullName,
                NationalNumber = customer.Person.NationalNumber,
                PhoneNumber = customer.Person.PhoneNumber,
                TownName = customer.Person.Town.TownName
            };
        }
        private void ApplyCustomerUpdates(Customer customer,CustomerUpdateDto DTO)
        {
            // Update person
            PersonService.UpdatePersonData(customer.Person, DTO.PersonUpdateDto);
        }
        private CustomerUpdateDto MapCustomer_UpdateDto(Customer customer)
        {
            return new CustomerUpdateDto()
            {
                CustomerId = customer.CustomerId,
                PersonUpdateDto = PersonService.MapPerosn_UpdateDto(customer.Person)
            };
        }
       

        /// <exception cref="ValidationException">
        /// Thrown when the Customer or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void AddCustomer(CustomerAddDto DTO)
        {
            Customer Customer = MapCustomer_AddDto(DTO);

            ValidationHelper.ValidateEntity(Customer);

            //Alwase Active
            Customer.IsActive = true;

            //Map Nulls
            PersonService.MappNullStrings(Customer.Person);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                _personRepo.Add(Customer.Person);
                _unitOfWork.Save();

                Customer.PersonId = Customer.Person.PersonId;


                _customerRepo.Add(Customer);
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
        public void UpdateCustomer(CustomerUpdateDto DTO)
        {
            var customer = _customerRepo.GetWithPersonById(DTO.CustomerId);
            if (customer == null||customer.Person==null)
                throw new NotFoundException(typeof(Customer));

            ApplyCustomerUpdates(customer, DTO);

            PersonService.MappNullStrings(customer.Person);

            ValidationHelper.ValidateEntity(customer);

            _unitOfWork.Save();
        }
        public void DeleteCustomer(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);

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

        public CustomerReadDto GetCustomerById(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_ReadDto(customer);
        }

        public CustomerUpdateDto GetCustomerForUpdate(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_UpdateDto(customer);
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
