using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
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

        public MasurementUnitService(IRepository<MasurementUnit> MasurementUnitRepo, IUnitOfWork UnitOfWork)
        {
            _MasurementUnitRepo = MasurementUnitRepo;
            _unitOfWork = UnitOfWork;
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


        public void AddMasuremetUnit(MasurementUnitAddDto DTO)
        {
            MasurementUnit masurementUnit = MapMasurementUnit_AddDto(DTO);

            ValidationHelper.ValidateEntity(masurementUnit);

            _MasurementUnitRepo.Add(masurementUnit);
            _unitOfWork.Save();
        }
        public MasurementUnitReadDto GetMasurementUnitById(int MasurementUnitid) 
        {
            MasurementUnit masurementUnit = _MasurementUnitRepo.GetById(MasurementUnitid);

            if (masurementUnit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }
            return MapMesuarmentUnit_ReadDto(masurementUnit);
        }
        public void DeleteMasuremetUnitById(int MasurementUnitid)
        {
            MasurementUnit masurementUnit = _MasurementUnitRepo.GetById(MasurementUnitid);

            if (masurementUnit == null)
            {
                throw new NotFoundException(typeof(MasurementUnit));
            }

            _MasurementUnitRepo.Delete(masurementUnit);
            _unitOfWork.Save();
        }
        public List<MasurementUnitListDto>GetAllMasurementUnit()
        {
            return _MasurementUnitRepo.GetAll()
                .Select(m => new MasurementUnitListDto()
                {
                    MasurementUnitId = m.MasurementUnitId,
                    Name = m.UnitName
                }).ToList();
        }
    }
}
