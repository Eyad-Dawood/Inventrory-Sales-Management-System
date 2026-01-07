using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddWorker(WorkerAddDto DTO)
        {
            Worker worker = MapWorker_AddDto(DTO);

            ValidationHelper.ValidateEntity(worker);

            //Alwase Active
            worker.IsActive = true;

            //Mapp Null
            PersonService.MappNullStrings(worker.Person);

            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {
                    _personRepo.Add(worker.Person);
                    _unitOfWork.Save();

                    worker.PersonId = worker.Person.PersonId;


                    _workerRepo.Add(worker);
                    _unitOfWork.Save();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

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
        public void UpdateWorker(WorkerUpdateDto DTO)
        {
            var worker = _workerRepo.GetWithPersonById(DTO.WorkerId);

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
                _unitOfWork.Save();
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
        public void DeleteWorker(int workerId)
        {
            Worker worker = _workerRepo.GetWithPersonById(workerId);

            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _workerRepo.Delete(worker);

                    _personRepo.Delete(worker.Person);

                    _unitOfWork.Save();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

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
        public WorkerReadDto GetWorkerById(int workerId)
        {
            Worker worker = _workerRepo.GetWithDetailsById(workerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            return MapWorker_ReadDto(worker);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<WorkerListDto> GetAllWorkers(int PageNumber, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _workerRepo
                .GetAllWithPerson(PageNumber, RowsPerPage)
                .Select(w => MapWorker_ListDto(w)
                ).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public WorkerUpdateDto GetWorkerForUpdate(int WorkerId)
        {
            Worker worker = _workerRepo.GetWithPersonById(WorkerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }
            return MapWorker_UpdateDto(worker);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageNumber(int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _workerRepo.GetTotalPages(RowsPerPage);
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<WorkerListDto> GetAllByFullName(int PageNumber, int RowsPerPage, string Name)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _workerRepo.
                            GetAllByFullName(PageNumber, RowsPerPage, Name)
                            .Select(w => MapWorker_ListDto(w))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<WorkerListDto> GetAllByTownName(int PageNumber, int RowsPerPage, string TownName)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);


            return _workerRepo.
                            GetAllByTownName(PageNumber, RowsPerPage, TownName)
                            .Select(w => MapWorker_ListDto(w))
                            .ToList();
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByFullName(string Name, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _workerRepo.GetTotalPagesByFullName(Name, RowsPerPage);
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public int GetTotalPageByTownName(string TownName, int RowsPerPage)
        {
            Validation.ValidationHelper.ValidateRowsPerPage(RowsPerPage);

            return _workerRepo.GetTotalPagesByTownName(TownName, RowsPerPage);
        }

    }
}
