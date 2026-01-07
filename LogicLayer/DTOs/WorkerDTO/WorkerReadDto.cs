using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.WorkerDTO
{
    public class WorkerReadDto
    {
        public int WorkerId { get; set; }
        public WorkersCraftsEnum Craft { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string TownName { get; set; }
    }
}
