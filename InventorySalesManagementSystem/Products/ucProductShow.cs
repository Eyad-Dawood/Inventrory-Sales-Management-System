using DataAccessLayer.Entities;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
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

            #region ProductNameFormat
            //Prodcut Name
            rtbName.BackColor = this.BackColor;
            rtbName.Clear();

            rtbName.SelectionColor = Color.Black;
            rtbName.AppendText($"{dto.ProductTypeName} ");

            rtbName.SelectionColor = Color.DarkRed;
            rtbName.AppendText($"[{dto.ProductName}]");
            #endregion

            #region QuantityFormat
            // Quantity
            rtbQuantity.BackColor = this.BackColor;
            rtbQuantity.ReadOnly = true;
            rtbQuantity.BorderStyle = BorderStyle.None;
            rtbQuantity.Clear();

            // Format quantity safely (no precision issues)
            string quantityStr = dto.QuantityInStorage.ToString("0.0000",
                System.Globalization.CultureInfo.InvariantCulture);

            // Split integer & fraction parts
            string[] parts = quantityStr.Split('.');

            // Integer part -> Black
            rtbQuantity.SelectionColor = Color.Black;
            rtbQuantity.AppendText(parts[0]);

            // Fraction part -> DarkRed
            rtbQuantity.SelectionColor = Color.DarkRed;
            rtbQuantity.AppendText("." + parts[1]);
            #endregion




            lbUnit.Text = dto.MesurementUnitName;



            lbSellingPrice.Text = $"{dto.SellingPrice.ToString("N2")}";
            lbBuyingPrice.Text = $"{dto.BuyingPrice.ToString("N2")}";
            lbProfit.Text = $"{dto.Profit.ToString("N2")}";


            lbTotalSales.Text = dto.TotalQuantitySold.ToString("N4");

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

        public async Task ShowData(IServiceProvider serviceProvider, int ProductId)
        {
            _serviceProvider = serviceProvider;

            using (var scope = _serviceProvider.CreateScope())
            {
                var productservice = scope.ServiceProvider.GetRequiredService<ProductService>();
                var soldProductservice = scope.ServiceProvider.GetRequiredService<SoldProductService>();
                try
                {
                    var Product = await productservice.GetProductByIdAsync(ProductId);
                    Product.TotalQuantitySold = await soldProductservice.GetTotalQuantitySoldByProductIdAsync(ProductId);
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
