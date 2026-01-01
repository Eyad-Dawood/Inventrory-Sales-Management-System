using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.TownDTO
{
    [Display(Name = "بيانات البلد/المدينة")]
    public class TownUpdateDto
    {
        public int TownId { get; set; }
        public string TownName { get; set; }
    }
}
