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
        private CustomerListDto MapCustomer_ListDto(Customer customer)
        {
            return new CustomerListDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.Person.FullName,
                PhoneNumber = customer.Person.PhoneNumber,
                TownName = customer.Person.Town.TownName,
                Balance = customer.Balance,
                IsActive = customer.IsActive
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

                    throw new OperationFailedException(ex);
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

                throw new OperationFailedException(ex);
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

                    throw new OperationFailedException(ex);
                }
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public CustomerReadDto GetCustomerById(int customerId)
        {
            Customer customer = _customerRepo.GetWithDetailsById(customerId);

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
                .Select(c=> MapCustomer_ListDto(c))
                .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _customerRepo.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<CustomerListDto> GetAllByFullName(int PageNumber,int RowsPerPage,string Name)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _customerRepo.
                            GetAllByFullName(PageNumber, RowsPerPage,Name)
                            .Select(c => MapCustomer_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<CustomerListDto> GetAllByTownName(int PageNumber, int RowsPerPage, string TownName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _customerRepo.
                            GetAllByTownName(PageNumber, RowsPerPage, TownName)
                            .Select(c => MapCustomer_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByFullName(string Name,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _customerRepo.GetTotalPagesByFullName(Name,RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByTownName(string TownName,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _customerRepo.GetTotalPagesByTownName(TownName,RowsPerPage);
        }

    }
}
