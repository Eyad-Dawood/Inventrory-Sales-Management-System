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
    public class WorkerUpdateDto
    {
        public int WorkerId { get; set; }

        public WorkersCraftsEnum Craft { get; set; }

        public bool IsActive { get; set; }

        public PersonUpdateDto PersonUpdateDto { get; set; }
    }
}
