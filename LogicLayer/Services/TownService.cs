using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
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
        
        public TownService(IRepository<Town>Townrepo, IUnitOfWork unitOfWork)
        {
            _Townrepo = Townrepo;
            _unitOfWork = unitOfWork;
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

        public void AddTown(TownAddDto DTO)
        {
            Town town = MapTown_AddDto(DTO);

            ValidationHelper.ValidateEntity(town);

            _Townrepo.Add(town);
            _unitOfWork.Save();
        }
        public void UpdateTown(TownUpdateDto DTO)
        {
            Town town = _Townrepo.GetById(DTO.TownId);
            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            ApplyTownUpdate(town, DTO);

            ValidationHelper.ValidateEntity(town);

            _unitOfWork.Save();
        }
        public List<TownListDto> GetAllTowns()
        {
            return _Townrepo.GetAll()
                .Select(t=>new TownListDto()
                {
                    TownID = t.TownID,
                    TownName = t.TownName
                }).ToList();
        }
        public void DeleteTownById(int TownId)
        {
            Town town = _Townrepo.GetById(TownId);

            if (town == null)
            {
                throw new NotFoundException(typeof(Town));
            }

            _Townrepo.Delete(town);
            _unitOfWork.Save();
        }
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
