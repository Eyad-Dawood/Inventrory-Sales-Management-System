using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services.Helpers;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
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
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ICustomerRepository customerRepo, IRepository<Person> perosnRepo, IUnitOfWork unitOfWork,ILogger<CustomerService>logger)
        {
            _customerRepo = customerRepo;
            _personRepo = perosnRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddCustomer(CustomerAddDto DTO)
        {            
            Customer Customer = MapCustomer_AddDto(DTO);

            ValidationHelper.ValidateEntity(Customer);

            //Alwase Active
            Customer.IsActive = true;

            //Map Nulls
            PersonService.MappNullStrings(Customer.Person);

            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {
                    _personRepo.Add(Customer.Person);
                    _unitOfWork.Save();

                    Customer.PersonId = Customer.Person.PersonId;


                    _customerRepo.Add(Customer);
                    _unitOfWork.Save();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    _logger.LogError(ex,
                        "Failed to add customer {FirstName} {LastName}",
                        Customer.Person.FirstName,
                        Customer.Person.SecondName);

                    throw new OperationFailedException();
                }
            }
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails
        /// </exception>
        public void UpdateCustomer(CustomerUpdateDto DTO)
        {
            var customer = _customerRepo.GetWithPersonById(DTO.CustomerId);

            if (customer == null || customer.Person == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            ApplyCustomerUpdates(customer, DTO);


            //Map Nulls
            PersonService.MappNullStrings(customer.Person);


            //Validate
            ValidationHelper.ValidateEntity(customer);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                        "Failed to Update Customer {CustomerId} Wit Person {PersonId}",
                        customer.CustomerId,
                        customer.PersonId);

                throw new OperationFailedException();
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void DeleteCustomer(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _customerRepo.Delete(customer);

                    _personRepo.Delete(customer.Person);
                    


                    _unitOfWork.Save();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    _logger.LogError(ex,
                    "Failed to Delete Customer {CustomerId} With Person {PersonId}",
                    customer.CustomerId,
                    customer.PersonId);

                    throw new OperationFailedException();
                }
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public CustomerReadDto GetCustomerById(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_ReadDto(customer);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public CustomerUpdateDto GetCustomerForUpdate(int customerId)
        {
            Customer customer = _customerRepo.GetWithPersonById(customerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_UpdateDto(customer);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<CustomerListDto> GetAllCustomers(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

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
