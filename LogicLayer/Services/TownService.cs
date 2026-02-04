using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.Logging;

namespace LogicLayer.Services
{
    public class TownService
    {
        private readonly IRepository<Town> _Townrepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TownService> _logger;
        public TownService(IRepository<Town>Townrepo, IUnitOfWork unitOfWork, ILogger<TownService> logger)
        {
            _Townrepo = Townrepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region Map
        private Town MapTown_AddDto(TownAddDto DTO)
        {
            return new Town()
            {
                TownName = DTO.TownName
            };
        }
        private void ApplyTownUpdate(Town town, TownUpdateDto DTO)
        {
            town.TownName = DTO.TownName;
        }
        private TownUpdateDto MapTown_UpdateDto(Town town)
        {
            return new TownUpdateDto()
            {
                TownId = town.TownID,
                TownName = town.TownName
            };
        }
        #endregion

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task AddTownAsync(TownAddDto DTO)
        {
            Town town = MapTown_AddDto(DTO);

            ValidationHelper.ValidateEntity(town);

           await _Townrepo.AddAsync(town);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to add Town {Town}",
                    DTO.TownName);

                throw new OperationFailedException(ex);
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
        public async Task UpdateTownAsync(TownUpdateDto DTO)
        {
            Town? town = await _Townrepo.GetByIdAsync(DTO.TownId);
            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            ApplyTownUpdate(town, DTO);

            ValidationHelper.ValidateEntity(town);

            try
            {
               await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                        "Failed to update Town {TownId}",
                        town.TownID);

                throw new OperationFailedException(ex);
            }
        }

        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public async Task<List<TownListDto>> GetAllTownsAsync(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return ( await _Townrepo.GetAllAsync(PageNumber,RowsPerPage))
                .Select(t=>new TownListDto()
                {
                    TownID = t.TownID,
                    TownName = t.TownName
                }).ToList();
        }

        public async Task<List<TownListDto>> GetAllTownsAsync()
        {
            return (await _Townrepo.GetAllAsync())
                .Select(t => new TownListDto()
                {
                    TownID = t.TownID,
                    TownName = t.TownName
                }).ToList();
        }


        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public async Task DeleteTownByIdAsync(int TownId)
        {
            Town? town = await _Townrepo.GetByIdAsync(TownId);

            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            _Townrepo.Delete(town);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to Delete Town {TownId}",
                    TownId);

                throw new OperationFailedException(ex);
            }
        }

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        public async Task<TownUpdateDto> GetTownForUpdateAsync(int TownId)
        {
            Town? Town = await _Townrepo.GetByIdAsync(TownId);

            if (Town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            return MapTown_UpdateDto(Town);
        }
    }
}
