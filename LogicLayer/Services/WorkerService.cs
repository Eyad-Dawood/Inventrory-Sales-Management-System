using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using LogicLayer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Validation.Exceptions;
using LogicLayer.DTOs.WorkerDTO;

namespace LogicLayer.Services
{
    public class WorkerService 
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IRepository<Person> _personRepo;
        private readonly IUnitOfWork _unitOfWork;

        public WorkerService(IWorkerRepository workerRepo, IRepository<Person> personRepo, IUnitOfWork unitOfWork)
        {
            _workerRepo = workerRepo;
            _personRepo = personRepo;
            _unitOfWork = unitOfWork;
        }

        public Worker MapWorker_AddDto(WorkerAddDto DTO)
        {
            return new Worker()
            {
                Craft = DTO.Craft,
                Person = PersonService.MapPerosn_AddDto(DTO.PersonAddDto)
            };
        }
        public void ApplyWorkerUpdates(Worker worker , WorkerUpdateDto DTO)
        {
            //Update Worker
            worker.Craft = DTO.Craft;
            worker.IsActive = DTO.IsActive;

            //Update Person
            PersonService.UpdatePersonData(worker.Person, DTO.PersonUpdateDto);
        }
        public WorkerReadDto MapWorker_ReadDto(Worker worker)
        {
            return new WorkerReadDto()
            {
                Craft = worker.Craft,
                FullName = worker.Person.FullName,
                IsActive = worker.IsActive,
                WorkerId = worker.WorkerId,
                NationalNumber = worker.Person.NationalNumber,
                PersonId = worker.Person.PersonId,
                PhoneNumber = worker.Person.PhoneNumber,
                TownName = worker.Person.Town.TownName
            };
        }

        /// <exception cref="ValidationException">
        /// Thrown when the Worker or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void AddWorker(WorkerAddDto DTO)
        {
            Worker worker = MapWorker_AddDto(DTO);

            ValidationHelper.ValidateEntity(worker);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                _personRepo.Add(worker.Person);
                _unitOfWork.Save();

                worker.PersonId = worker.Person.PersonId;


                _workerRepo.Add(worker);
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
        /// Thrown when the Worker or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void UpdateWorker(WorkerUpdateDto DTO)
        {
            var worker = _workerRepo.GetWithPersonById(DTO.WorkerId);
            if (worker == null || worker.Person == null)
                throw new NotFoundException(typeof(Worker));

            ApplyWorkerUpdates(worker,DTO);

            ValidationHelper.ValidateEntity(worker);

            _unitOfWork.Save();
        }


        public void DeleteWorker(int workerId)
        {
            Worker worker = _workerRepo.GetWithPersonById(workerId);

            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                _workerRepo.Delete(worker);

                _personRepo.Delete(worker.Person);
                _unitOfWork.Save();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public WorkerReadDto GetWorkerById(int workerId)
        {
            Worker worker = _workerRepo.GetWithPersonById(workerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }

            return MapWorker_ReadDto(worker);
        }

        public List<WorkerListDto> GetAllWorkers(int PageNumber, int RowsPerPage)
        {
            return _workerRepo
                .GetAllWithPerson(PageNumber, RowsPerPage)
                .Select(w => new WorkerListDto
                {
                    WorkerId = w.WorkerId,
                    FullName = w.Person.FullName,
                    PhoneNumber = w.Person.PhoneNumber,
                    TownName = w.Person.Town.TownName,
                    IsActive = w.IsActive,
                    Craft = w.Craft
                }
                ).ToList();
        }

    }
}
