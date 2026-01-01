using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
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



        public void AddTown(TownAddDto DTO)
        {
            Town town = MapTown_AddDto(DTO);

            ValidationHelper.ValidateEntity(town);

            _Townrepo.Add(town);
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

    }
}
