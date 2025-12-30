using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.DTOs.PersonDTO;

namespace LogicLayer.DTOs.WorkerDTO
{
    public class WorkerAddDto
    {
        public WorkersCraftsEnum Craft { get; set; }
        public PersonAddDto PersonAddDto { get; set; }
    }
}
