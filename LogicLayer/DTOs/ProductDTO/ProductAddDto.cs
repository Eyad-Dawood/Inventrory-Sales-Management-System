using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductAddDto
    {

        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public string ProductName { get; set; }
        public Status Status { get; set; }
        public int ProductTypeId { get; set; }
        public int MasurementUnitId { get; set; }
    }
}
