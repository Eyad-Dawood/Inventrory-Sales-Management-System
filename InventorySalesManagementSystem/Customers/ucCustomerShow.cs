using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
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

namespace InventorySalesManagementSystem.Customers
{
    public partial class ucCustomerShow : UserControl
    {
        private IServiceProvider _serviceProvider;
        public ucCustomerShow()
        {
            InitializeComponent();
        }


        private void FillCustomerData(CustomerReadDto dto)
        {
            lbID.Text = dto.CustomerId.ToString();
            lbName.Text = dto.FullName;
            lbNational.Text = UiFormat.FormatNullableValue(dto.NationalNumber);
            lbPhone.Text = UiFormat.FormatNullableValue(dto.PhoneNumber);
            lbTown.Text = dto.TownName;

            lbBalance.Text = $"{dto.Balance.ToString("N2")}";
            lbBalance.ForeColor = dto.Balance >= 0 ? Color.DarkGreen : Color.DarkRed;

            if (dto.IsActive)
            {
                lbActive.Text = "نشط";
                lbActive.ForeColor = Color.Green;
            }
            else
            {
                lbActive.Text = "غير نشط";
                lbActive.ForeColor = Color.Red;
            }

        }
        public async Task ShowData(IServiceProvider serviceProvider, int customerId)
        {
            _serviceProvider = serviceProvider;

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                try
                {
                    var customer = await service.GetCustomerByIdAsync(customerId);
                    this.Enabled = true;
                    FillCustomerData(customer);

                }
                catch (NotFoundException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "During Finding Customer {CustomerId} In Show Data Function", customerId);
                    throw;
                }
            }
        }

    }
}
