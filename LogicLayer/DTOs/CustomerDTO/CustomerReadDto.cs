using DataAccessLayer.Entities;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.DTOs.TownDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.CustomerDTO
{
    public class CustomerReadDto
    {
        public int CustomerId { get; set; }

        public int PersonId { get; set; }

        public decimal Balance { get; set; }

        public string FullName { get; set; }

        public string? NationalNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public string TownName { get; set; }

        public bool IsActive { get; set; }
    }
}
