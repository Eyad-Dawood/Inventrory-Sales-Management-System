using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
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
        private TownUpdateDto MapTow_UpdateDto(Town town)
        {
            return new TownUpdateDto()
            {
                TownId = town.TownID,
                TownName = town.TownName
            };
        }

        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        /// <exception cref="OperationFailedException">
        /// Thrown when the Operation fails.
        /// </exception>
        public void AddTown(TownAddDto DTO)
        {
            Town town = MapTown_AddDto(DTO);

            ValidationHelper.ValidateEntity(town);

            _Townrepo.Add(town);

            try
            {
                _unitOfWork.Save();
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
        public void UpdateTown(TownUpdateDto DTO)
        {
            Town town = _Townrepo.GetById(DTO.TownId);
            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            ApplyTownUpdate(town, DTO);

            ValidationHelper.ValidateEntity(town);

            try
            {
                _unitOfWork.Save();
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
        public List<TownListDto> GetAllTowns(int PageNumber,int RowsPerPage)
        {
            Validation.ValidationHelper.ValidatePageginArguments(PageNumber, RowsPerPage);

            return _Townrepo.GetAll(PageNumber,RowsPerPage)
                .Select(t=>new TownListDto()
                {
                    TownID = t.TownID,
                    TownName = t.TownName
                }).ToList();
        }

        public List<TownListDto> GetAllTowns()
        {
            return _Townrepo.GetAll()
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
        public void DeleteTownById(int TownId)
        {
            Town town = _Townrepo.GetById(TownId);

            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            _Townrepo.Delete(town);

            try
            {
                _unitOfWork.Save();
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
        public TownUpdateDto GetTownForUpdate(int TownId)
        {
            Town Town = _Townrepo.GetById(TownId);

            if (Town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            return MapTow_UpdateDto(Town);
        }
    }
}
