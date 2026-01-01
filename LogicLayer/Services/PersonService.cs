using DataAccessLayer.Entities;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.Services.Helpers;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class PersonService
    {
        public static Person MapPerosn_AddDto(PersonAddDto DTO)
        {
            return new Person()
            {
                FirstName = DTO.FirstName,
                SecondName = DTO.SecondName,
                ThirdName = DTO.ThirdName,
                FourthName = DTO.FourthName,
                NationalNumber = DTO.NationalNumber,
                PhoneNumber = DTO.PhoneNumber,
                TownID = DTO.TownID
            };
        }

        public static void UpdatePersonData(Person Person,PersonUpdateDto DTO)
        {
            Person.FirstName = DTO.FirstName;
            Person.SecondName = DTO.SecondName;
            Person.ThirdName = DTO.ThirdName;
            Person.FourthName = DTO.FourthName;
            Person.NationalNumber = DTO.NationalNumber;
            Person.PhoneNumber = DTO.PhoneNumber;
            Person.TownID = DTO.TownID;
        }

        public static void MappNullStrings(Person Person)
        {
            Person.ThirdName = NullMapperHelper.NormalizeString(Person.ThirdName);
            Person.FourthName = NullMapperHelper.NormalizeString(Person.FourthName);
            Person.NationalNumber = NullMapperHelper.NormalizeString(Person.NationalNumber);
            Person.PhoneNumber = NullMapperHelper.NormalizeString(Person.PhoneNumber);

        }

        public static PersonUpdateDto MapPerosn_UpdateDto(Person person)
        {
            return new PersonUpdateDto()
            {
                FirstName = person.FirstName,
                TownID = person.TownID,
                FourthName = person.FourthName,
                NationalNumber = person.NationalNumber,
                PersonId = person.PersonId,
                PhoneNumber = person.PhoneNumber,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
            };
        }
    }
}
