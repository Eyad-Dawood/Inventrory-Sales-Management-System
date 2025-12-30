using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using LogicLayer.DTOs.PersonDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.CustomerDTO
{
    public class CustomerAddDto
    {
        public PersonAddDto PersonAddDto { get; set; }
    }
}
