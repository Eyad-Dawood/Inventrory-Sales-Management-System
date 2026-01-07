using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
        public string ProductTypeName { get; set; }
        public string MesurementUnitName { get; set; }
    }
}
