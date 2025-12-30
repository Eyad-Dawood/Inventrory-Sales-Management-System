using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
