using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.CustomerDTO
{
    public class CustomerListDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string TownName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

    }
}
