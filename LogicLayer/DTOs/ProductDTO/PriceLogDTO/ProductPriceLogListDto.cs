using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.ProductDTO.PriceLogDTO
{
    public class ProductPriceLogListDto
    {
        public decimal OldBuyingPrice { get; set; }

        public decimal NewBuyingPrice { get; set; }


        public decimal OldSellingPrice { get; set; }

        public decimal NewSellingPrice { get; set; }


        public DateTime LogDate { get; set; }

        [Display(Name = "إسم المنتج الكامل")]
        public string ProductFullName { get; set; }
        public int ProductId { get; set; }

        public string CreatedByUserName { get; set; }
    }
}
