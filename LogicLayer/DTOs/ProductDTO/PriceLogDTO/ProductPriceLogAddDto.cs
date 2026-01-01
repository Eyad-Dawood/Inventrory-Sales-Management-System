using DataAccessLayer.Abstractions;
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
    public class ProductPriceLogAddDto 
    {
        public decimal OldBuyingPrice { get; set; }
        public decimal NewBuyingPrice { get; set; }


        public decimal OldSellingPrice { get; set; }
        public decimal NewSellingPrice { get; set; }


        public int ProductId { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
