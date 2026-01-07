using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Utilities;
namespace LogicLayer.DTOs.WorkerDTO
{
    public class WorkerListDto
    {
        public int WorkerId { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string TownName { get; set; }
        public bool IsActive { get; set; }
        public string Craft { get; set; }

        
    }
}
