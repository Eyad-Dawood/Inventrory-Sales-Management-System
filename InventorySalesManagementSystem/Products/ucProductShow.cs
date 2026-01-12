using DataAccessLayer.Entities;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.Services;
using LogicLayer.Services.Products;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Products
{
    public partial class ucProductShow : UserControl
    {
        private IServiceProvider _serviceProvider;

        public ucProductShow()
        {
            InitializeComponent();
        }

        private void FillProductData(ProductReadDto dto)
        {
            lbID.Text = dto.ProductId.ToString();


            //Prodcut Name
            rtbName.BackColor = this.BackColor;
            rtbName.Clear();

            rtbName.SelectionColor = Color.Black;
            rtbName.AppendText($"{dto.ProductTypeName} ");

            rtbName.SelectionColor = Color.DarkRed;
            rtbName.AppendText($"[{dto.ProductName}]");


            // Quantity
            rtbQuantity.BackColor = this.BackColor;
            rtbQuantity.ReadOnly = true;
            rtbQuantity.BorderStyle = BorderStyle.None;
            rtbQuantity.Clear();

            
            //Separate 


            // decimal quantity = dto.QuantityInStorage;
            int integerPart = (int)Math.Truncate(dto.QuantityInStorage); // الجزء الصحيح
            decimal fractionPart = dto.QuantityInStorage - integerPart;   // الجزء العشري

           
            //Quantity

            // int -> Black
            rtbQuantity.SelectionColor = Color.Black;
            rtbQuantity.AppendText(integerPart.ToString());

            // Fraction -> DarkRed
            rtbQuantity.SelectionColor = Color.DarkRed;

            //Format Fraction
            string fractionStr = fractionPart.ToString(".0000");
            rtbQuantity.AppendText($"{fractionStr}");




            lbUnit.Text = dto.MesurementUnitName;



            lbSellingPrice.Text = $"{dto.SellingPrice.ToString("N2")}";
            lbBuyingPrice.Text = $"{dto.BuyingPrice.ToString("N2")}";
            lbProfit.Text = $"{dto.Profit.ToString("N2")}";


            if (dto.IsAvailable)
            {
                lbAvilable.Text = "متاح";
                lbAvilable.ForeColor = Color.Green;
            }
            else
            {
                lbAvilable.Text = "غير متاح";
                lbAvilable.ForeColor = Color.Red;
            }

        }

        public void ShowData(IServiceProvider serviceProvider, int ProductId)
        {
            _serviceProvider = serviceProvider;

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                try
                {
                    var Product = service.GetProductById(ProductId);
                    this.Enabled = true;
                    FillProductData(Product);

                }
                catch (NotFoundException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "During Finding Product {ProductId} In Show Data Function", ProductId);
                    throw;
                }
            }
        }

    }
}
