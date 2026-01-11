using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public bool IsAvilable { get; set; }
        public decimal Profit
        {
            get
            {
                return SellingPrice - BuyingPrice;
            }
        }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public string MesurementUnitName { get; set; }
    }
}
