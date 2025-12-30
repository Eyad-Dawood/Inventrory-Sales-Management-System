using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.PersonDTO
{
    public class PersonAddDto
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string? ThirdName { get; set; }

        public string? FourthName { get; set; }

        public string? NationalNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public int TownID { get; set; }
    }
}
