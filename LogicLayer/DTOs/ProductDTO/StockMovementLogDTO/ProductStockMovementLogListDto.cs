using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.ProductDTO.StockMovementLogDTO
{
    public class ProductStockMovementLogListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal QuantityChange { get { return NewQuantity - OldQuantity; } } 
        public decimal OldQuantity { get; set; }
        public decimal NewQuantity { get; set; }
        public StockMovementReason Reason { get; set; }
        public string CreatedbyUserName { get; set; }
        public DateTime LogDate { get; set; }
    }
}
