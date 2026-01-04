using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.DTOs.PersonDTO;

namespace LogicLayer.DTOs.CustomerDTO
{
    public class CustomerUpdateDto
    {
        public int CustomerId { get; set; }

        public PersonUpdateDto PersonUpdateDto { get; set; }
    }
}
