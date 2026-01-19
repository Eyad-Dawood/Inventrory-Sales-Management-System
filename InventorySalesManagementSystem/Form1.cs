using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.PricesLog;
using InventorySalesManagementSystem.Products.ProductsTypes;
using InventorySalesManagementSystem.Products.StockMovementLog;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventorySalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new frmCustomerListScreen(_serviceProvider);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new frmWorkerListScreen(_serviceProvider);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var frm = new frmProductTypeListScreen(_serviceProvider))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(frm.SelectedProductType.Name, frm.SelectedProductType.ProductTypeId.ToString());
                }
            }
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            var frm = new frmProductListScreen(_serviceProvider);
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmStockMovementLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmPriceLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var Session = scope.ServiceProvider.GetRequiredService<UserSession>();

                MessageBox.Show(Session.CurrentUser.UserName, Session.CurrentUser.UserId.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var frm = new Test(_serviceProvider)
                ;
            frm.ShowDialog();
        }
    }
}
