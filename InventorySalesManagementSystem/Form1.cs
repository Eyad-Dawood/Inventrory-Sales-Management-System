using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
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

            
        }
    }
}
