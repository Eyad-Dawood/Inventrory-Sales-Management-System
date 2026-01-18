using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;

namespace LogicLayer.Services
{
    public class WorkerService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IRepository<Person> _personRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkerService> _logger;
        public WorkerService(IWorkerRepository workerRepo, IRepository<Person> personRepo, IUnitOfWork unitOfWork, ILogger<WorkerService> logger)
        {
            _workerRepo = workerRepo;
            _personRepo = personRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region
        private Worker MapWorker_AddDto(WorkerAddDto DTO)
        {
            return new Worker()
            {
                Craft = DTO.Craft,
                Person = PersonService.MapPerosn_AddDto(DTO.PersonAddDto)
            };
        }
        private void ApplyWorkerUpdates(Worker worker, WorkerUpdateDto DTO)
        {
            //Update Worker
            worker.Craft = DTO.Craft;

            //Update Person
            PersonService.UpdatePersonData(worker.Person, DTO.PersonUpdateDto);
        }
        private WorkerReadDto MapWorker_ReadDto(Worker worker)
        {
            return new WorkerReadDto()
            {
                Craft = worker.Craft,
                FullName = worker.Person.FullName,
                IsActive = worker.IsActive,
                WorkerId = worker.WorkerId,
                NationalNumber = worker.Person.NationalNumber,
                PhoneNumber = worker.Person.PhoneNumber,
                TownName = worker.Person.Town.TownName
            };
        }

        private WorkerListDto MapWorker_ListDto(Worker worker)
        {
            return new WorkerListDto()
            {
                WorkerId = worker.WorkerId,
                FullName = worker.Person.FullName,
                Craft = (worker.Craft.ToDisplayText()),
                IsActive = worker.IsActive,
                PhoneNumber = worker.Person.PhoneNumber,
                TownName = worker.Person.Town.TownName
            };
        }
        private WorkerUpdateDto MapWorker_UpdateDto(Worker worker)
        {
            return new WorkerUpdateDto()
            {
                WorkerId = worker.WorkerId,
                Craft = worker.Craft,
                PersonUpdateDto = PersonService.MapPerosn_UpdateDto(worker.Person)
            };
        }
        #endregion

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddWorkerAsync(WorkerAddDto DTO)
        {
            Worker worker = MapWorker_AddDto(DTO);

            ValidationHelper.ValidateEntity(worker);

            //Alwase Active
            worker.IsActive = true;

            //Mapp Null
            PersonService.MappNullStrings(worker.Person);

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {

                try
                {
                    await _personRepo.AddAsync(worker.Person);
                    await _unitOfWork.SaveAsync();

                    worker.PersonId = worker.Person.PersonId;


                    await _workerRepo.AddAsync(worker);
                    await _unitOfWork.SaveAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    _logger.LogError(ex,
                        "Failed to add worker {FirstName} {LastName}",
                        worker.Person.FirstName,
                        worker.Person.SecondName);

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
        public async Task UpdateWorkerAsync(WorkerUpdateDto DTO)
        {
            var worker = await _workerRepo.GetWithPersonByIdAsync(DTO.WorkerId);

            if (worker == null || worker.Person == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            //Apply Changes
            ApplyWorkerUpdates(worker, DTO);

            //Mapping Nulls
            PersonService.MappNullStrings(worker.Person);


            ValidationHelper.ValidateEntity(worker);



            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                        "Failed to Update Worker {WorkerId} Wit Person {PersonId}",
                        worker.WorkerId,
                        worker.PersonId);

                throw new OperationFailedException(ex);
            }
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task DeleteWorkerAsync(int workerId)
        {
            Worker? worker = await _workerRepo.GetWithPersonByIdAsync(workerId);

            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _workerRepo.Delete(worker);

                    _personRepo.Delete(worker.Person);

                    await _unitOfWork.SaveAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    _logger.LogError(ex,
                    "Failed to Delete Worker {WorkerId} With Person {PersonId}",
                    worker.WorkerId,
                    worker.PersonId);

                    throw new OperationFailedException(ex);
                }
            }
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<WorkerReadDto> GetWorkerByIdAsync(int workerId)
        {
            Worker? worker = await _workerRepo.GetWithDetailsByIdAsync(workerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            return MapWorker_ReadDto(worker);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<WorkerListDto>> GetAllWorkersAsync(int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return
                (await _workerRepo
                .GetAllWithPersonAsync(PageNumber, RowsPerPage))
                .Select(w => MapWorker_ListDto(w)
                ).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<WorkerUpdateDto> GetWorkerForUpdateAsync(int WorkerId)
        {
            Worker? worker = await _workerRepo.GetWithPersonByIdAsync(WorkerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }
            return MapWorker_UpdateDto(worker);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageNumberAsync(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _workerRepo.GetTotalPagesAsync(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<WorkerListDto>> GetAllByFullNameAsync(int PageNumber, int RowsPerPage, string Name)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _workerRepo.
                            GetAllByFullNameAsync(PageNumber, RowsPerPage, Name))
                            .Select(w => MapWorker_ListDto(w))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<WorkerListDto>> GetAllByTownNameAsync(int PageNumber, int RowsPerPage, string TownName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return (await _workerRepo.
                            GetAllByTownNameAsync(PageNumber, RowsPerPage, TownName))
                            .Select(w => MapWorker_ListDto(w))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByFullNameAsync(string Name, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _workerRepo.GetTotalPagesByFullNameAsync(Name, RowsPerPage);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<int> GetTotalPageByTownNameAsync(string TownName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return await _workerRepo.GetTotalPagesByTownNameAsync(TownName, RowsPerPage);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task ChangeActivationStateAsync(int WorkerId,bool State)
        {
            Worker? worker = await _workerRepo.GetByIdAsync(WorkerId);

            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            worker.IsActive = State;

            try
            {
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("تم تغيير حالة العامل {WorkerId} إلى {State}", WorkerId, State);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "فشل تغيير حالة تنشيط العامل {WorkerId}", WorkerId);
                throw new OperationFailedException(ex);
            }
        }
    }
}
