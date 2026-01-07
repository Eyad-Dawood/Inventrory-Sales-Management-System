using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.ProductsTypes;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
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
            using (var frm = new frmProductTypeListScreen(_serviceProvider, selectMode: true))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(frm.SelectedProductType.Name, frm.SelectedProductType.ProductTypeId.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = frmAddUpdateProduct.CreateForUpdate(_serviceProvider,4);
            frm.ShowDialog();
        }
    }
}
