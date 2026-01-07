using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class MasurementUnitService
    {
        private readonly IRepository<MasurementUnit> _MasurementUnitRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MasurementUnitService> _logger;

        public MasurementUnitService(IRepository<MasurementUnit> MasurementUnitRepo, IUnitOfWork UnitOfWork , ILogger<MasurementUnitService>logger)
        {
            _MasurementUnitRepo = MasurementUnitRepo;
            _unitOfWork = UnitOfWork;
            _logger = logger;
        }

        private void ApplyMasurementUnitUpdate(MasurementUnit Unit, MasurementUnitUpdateDto DTO)
        {
            Unit.UnitName = DTO.MasurementUnitName;
        }

        private MasurementUnit MapMasurementUnit_AddDto(MasurementUnitAddDto DTO)
        {
            return new MasurementUnit()
            {
                UnitName = DTO.Name
            };
        }
        private MasurementUnitReadDto MapMesuarmentUnit_ReadDto(MasurementUnit masurementUnit)
        {
            return new MasurementUnitReadDto()
            {
                Name = masurementUnit.UnitName
            };
        }
        private MasurementUnitUpdateDto MapMasurementUnit_UpdateDto(MasurementUnit masurementUnit)
        {
            return new MasurementUnitUpdateDto()
            {
                MasurementUnitId = masurementUnit.MasurementUnitId,
                MasurementUnitName = masurementUnit.UnitName
            };
        }

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddMasuremetUnit(MasurementUnitAddDto DTO)
        {
            MasurementUnit masurementUnit = MapMasurementUnit_AddDto(DTO);

            ValidationHelper.ValidateEntity(masurementUnit);

            _MasurementUnitRepo.Add(masurementUnit);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,
                    "Failed to add Unit {UnitName}",
                    DTO.Name);

                throw new OperationFailedException(ex);
            }
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public MasurementUnitReadDto GetMasurementUnitById(int MasurementUnitid) 
        {
            MasurementUnit masurementUnit = _MasurementUnitRepo.GetById(MasurementUnitid);

            if (masurementUnit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }
            return MapMesuarmentUnit_ReadDto(masurementUnit);
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void DeleteMasuremetUnitById(int MasurementUnitid)
        {
            MasurementUnit masurementUnit = _MasurementUnitRepo.GetById(MasurementUnitid);

            if (masurementUnit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }

            _MasurementUnitRepo.Delete(masurementUnit);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   "Failed to Delete Unit {UnitId}",
                   MasurementUnitid);

                throw new OperationFailedException(ex);
            }
        }


        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public List<MasurementUnitListDto>GetAllMasurementUnit(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return _MasurementUnitRepo.GetAll(PageNumber,RowsPerPage)
                .Select(m => new MasurementUnitListDto()
                {
                    MasurementUnitId = m.MasurementUnitId,
                    Name = m.UnitName
                }).ToList();
        }

        public List<MasurementUnitListDto> GetAllMasurementUnit()
        {

            return _MasurementUnitRepo.GetAll()
                .Select(m => new MasurementUnitListDto()
                {
                    MasurementUnitId = m.MasurementUnitId,
                    Name = m.UnitName
                }).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public MasurementUnitUpdateDto GetUnitForUpdate(int UnitId)
        {
            MasurementUnit Unit = _MasurementUnitRepo.GetById(UnitId);

            if (Unit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }
            return MapMasurementUnit_UpdateDto(Unit);
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
        public void UpdateMasurementUnit(MasurementUnitUpdateDto DTO)
        {
            MasurementUnit masurement = _MasurementUnitRepo.GetById(DTO.MasurementUnitId);
            if (masurement == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }

            ApplyMasurementUnitUpdate(masurement, DTO);

            ValidationHelper.ValidateEntity(masurement);

            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                        "Failed to update Masurement Unit {UnitId}",
                        masurement.MasurementUnitId);

                throw new OperationFailedException(ex);
            }
        }

    }
}
