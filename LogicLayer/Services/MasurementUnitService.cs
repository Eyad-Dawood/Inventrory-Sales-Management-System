using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;

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

        #region Map
        private void ApplyMasurementUnitUpdate(MasurementUnit Unit, MasurementUnitUpdateDto DTO)
        {
            Unit.UnitName = DTO.Name;
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
                Name = masurementUnit.UnitName
            };
        }

        #endregion

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddMasuremetUnitAsync(MasurementUnitAddDto DTO)
        {
            MasurementUnit masurementUnit = MapMasurementUnit_AddDto(DTO);

            ValidationHelper.ValidateEntity(masurementUnit);

            await _MasurementUnitRepo.AddAsync(masurementUnit);

            try
            {
                await _unitOfWork.SaveAsync();
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
        public async Task<MasurementUnitReadDto> GetMasurementUnitByIdAsync(int MasurementUnitid) 
        {
            MasurementUnit? masurementUnit = await _MasurementUnitRepo.GetByIdAsync(MasurementUnitid);

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
        public async Task DeleteMasuremetUnitByIdAsync(int MasurementUnitid)
        {
            MasurementUnit? masurementUnit = await _MasurementUnitRepo.GetByIdAsync(MasurementUnitid);

            if (masurementUnit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }

            _MasurementUnitRepo.Delete(masurementUnit);

            try
            {
               await _unitOfWork.SaveAsync();
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
        public async Task<List<MasurementUnitListDto>>GetAllMasurementUnitAsync(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return 
                (await _MasurementUnitRepo.GetAllAsync(PageNumber,RowsPerPage))
                .Select(m => new MasurementUnitListDto()
                {
                    MasurementUnitId = m.MasurementUnitId,
                    UnitName = m.UnitName
                }).ToList();
        }

        public async Task<List<MasurementUnitListDto>> GetAllMasurementUnitAsync()
        {

            return 
                (await _MasurementUnitRepo.GetAllAsync())
                .Select(m => new MasurementUnitListDto()
                {
                    MasurementUnitId = m.MasurementUnitId,
                    UnitName = m.UnitName
                }).ToList();
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<MasurementUnitUpdateDto> GetUnitForUpdateAsync(int UnitId)
        {
            MasurementUnit? Unit = await _MasurementUnitRepo.GetByIdAsync(UnitId);

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
        public async Task UpdateMasurementUnitAsync(MasurementUnitUpdateDto DTO)
        {
            MasurementUnit? masurement = await _MasurementUnitRepo.GetByIdAsync(DTO.MasurementUnitId);
            if (masurement == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }

            ApplyMasurementUnitUpdate(masurement, DTO);

            ValidationHelper.ValidateEntity(masurement);

            try
            {
                await _unitOfWork.SaveAsync();
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
