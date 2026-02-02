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

        #region Map
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
        #endregion

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddCustomerAsync(CustomerAddDto DTO)
        {            
            Customer Customer = MapCustomer_AddDto(DTO);

            //Map Nulls
            PersonService.MappNullStrings(Customer.Person);


            ValidationHelper.ValidateEntity(Customer);

            //Alwase Active
            Customer.IsActive = true;


            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {

                try
                {
                    await _personRepo.AddAsync(Customer.Person);
                    await _unitOfWork.SaveAsync();

                    Customer.PersonId = Customer.Person.PersonId;


                    await _customerRepo.AddAsync(Customer);
                    await _unitOfWork.SaveAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

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
        public async Task UpdateCustomerAsync(CustomerUpdateDto DTO)
        {
            var customer = await _customerRepo.GetWithPersonByIdAsync(DTO.CustomerId);

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
                await _unitOfWork.SaveAsync();
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
        public async Task DeleteCustomerAsync(int customerId)
        {
            Customer? customer = await _customerRepo.GetWithPersonByIdAsync(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _customerRepo.Delete(customer);

                    _personRepo.Delete(customer.Person);



                    await _unitOfWork.SaveAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

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
        public async Task<CustomerReadDto> GetCustomerByIdAsync(int customerId)
        {
            Customer? customer = await _customerRepo.GetWithDetailsByIdAsync(customerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_ReadDto(customer);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<CustomerUpdateDto> GetCustomerForUpdateAsync(int customerId)
        {
            Customer? customer = await _customerRepo.GetWithPersonByIdAsync(customerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            return MapCustomer_UpdateDto(customer);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<CustomerListDto>> GetAllCustomersAsync(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return
                (await _customerRepo.
                GetAllWithPersonAsync(PageNumber,RowsPerPage))
                .Select(c=> MapCustomer_ListDto(c))
                .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _customerRepo.GetTotalPagesAsync(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<CustomerListDto>> GetAllByFullNameAsync(int PageNumber,int RowsPerPage,string Name)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _customerRepo.
                            GetAllByFullNameAsync(PageNumber, RowsPerPage,Name))
                            .Select(c => MapCustomer_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<CustomerListDto>> GetAllByTownNameAsync(int PageNumber, int RowsPerPage, string TownName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return 
                (await _customerRepo.
                            GetAllByTownNameAsync(PageNumber, RowsPerPage, TownName))
                            .Select(c => MapCustomer_ListDto(c))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByFullNameAsync(string Name,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _customerRepo.GetTotalPagesByFullNameAsync(Name,RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByTownNameAsync(string TownName,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _customerRepo.GetTotalPagesByTownNameAsync(TownName,RowsPerPage);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task ChangeActivationStateAsync(int CustomerId, bool State)
        {
            Customer? customer = await _customerRepo.GetByIdAsync(CustomerId);

            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }

            customer.IsActive = State;

            if(!customer.IsActive &&
               await _customerRepo.HasOpenInvoice(customer.CustomerId))
            {
                throw new OperationFailedException("لا يمكن إلغاء تنشيط حالة العميل , لوجود فواتير مفتوحة باسمه");
            }

            try
            {
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("تم تغيير حالة العميل {CutomerId} إلى {State}", CustomerId, State);
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل تغيير حالة تنشيط العميل {CutomerId}", CustomerId);
                throw new OperationFailedException(ex);
            }
        }

        private async Task ChangeCusotmerBalance(int CustomerId, decimal Amount , bool IsAddition)
        {
            //Tracking
            Customer? customer = await _customerRepo.GetByIdAsync(CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(typeof(Customer));
            }
            if(IsAddition)
            {
                customer.Balance += Amount;
            }
            else
            {
                customer.Balance -= Amount;
            }
        }
        public async Task DepositBalance(int CustomerId,decimal Amount)
        {
            await ChangeCusotmerBalance(CustomerId,Amount,IsAddition:true);
        }
        public async Task WithdrawBalance(int CustomerId, decimal Amount)
        {
            await ChangeCusotmerBalance(CustomerId, Amount, IsAddition: false);
        }
    }
}
