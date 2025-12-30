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
using LogicLayer.DTOs;

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


        /// <exception cref="ValidationException">
        /// Thrown when the Worker or person data violates validation rules
        /// (e.g. invalid age, missing required fields).
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when an unexpected error occurs such as database failure
        /// or infrastructure-related issues.
        /// </exception>
        public void AddWorker(Worker worker)
        {
            ValidationHelper.ValidateEntity(worker);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                _personRepo.Add(worker.Person);

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
        public void UpdateWorker(Worker Worker)
        {
            ValidationHelper.ValidateEntity(Worker);

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {

                _personRepo.Update(Worker.Person);


                _workerRepo.Update(Worker);
                _unitOfWork.Save();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public void DeleteWorker(int workerId)
        {
            Worker worker = _workerRepo.GetById(workerId);

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


        public Worker GetWorkerById(int workerId)
        {
            Worker worker = _workerRepo.GetWithPerosnById(workerId);
            if (worker == null)
            {
                throw new NotFoundException(typeof(Worker));
            }
            return worker;
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
