using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
using LogicLayer.Services.Helpers;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
    public partial class frmAddUpdateCustomer : Form
    {
        private Enums.FormStateEnum State { set; get; }

        private CustomerAddDto _customerAdd { get; set; }
        private CustomerUpdateDto _customerUpdate { get; set; }

        private IServiceProvider _serviceProvider { get; set; }

        private frmAddUpdateCustomer(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private async Task SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            lbTitle.Text = "إضافة عميل";

            _customerAdd = new CustomerAddDto();

            lb_CustomerId.Text = "---";
           await uc_AddUpdatePerson1.Start(_serviceProvider);
        }
        private async Task SetupUpdate(CustomerUpdateDto dto)
        {
            State = Enums.FormStateEnum.Update;

            lbTitle.Text = "تعديل عميل";

            _customerUpdate = dto;

           await LoadUpdateData();
        }

        private async Task LoadUpdateData()
        {
            lb_CustomerId.Text = _customerUpdate.CustomerId.ToString();
           await uc_AddUpdatePerson1.Start(_serviceProvider, _customerUpdate.PersonUpdateDto);
        }

        public static async Task<frmAddUpdateCustomer> CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateCustomer(serviceProvider);
            await form.SetupAdd();
            return form;
        }
        public static async Task<frmAddUpdateCustomer> CreateForUpdate(IServiceProvider serviceProvider,int CustomerId)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                var dto = await service.GetCustomerForUpdateAsync(CustomerId);

                frmAddUpdateCustomer frm = new frmAddUpdateCustomer(serviceProvider);
               await frm.SetupUpdate(dto);
                return frm;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCustomerAdd()
        {
            _customerAdd.PersonAddDto = uc_AddUpdatePerson1.GetAddPerson();           
        }
        private void FillCustomerUpdate()
        {
            _customerUpdate.PersonUpdateDto = uc_AddUpdatePerson1.GetUpdatePerson();
        }


        private async Task UpdateCustomer(CustomerService CustomerService)
        {
            FillCustomerUpdate();

            //Validate Values Format
            LogicLayer.Validation.Custom_Validation.PersonFormatValidation.ValidateValues(_customerUpdate.PersonUpdateDto);

           await CustomerService.UpdateCustomerAsync(_customerUpdate);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تم التحديث بنجاح");
            this.Close();
        }
        private async Task AddCustomer(CustomerService CustomerService)
        {
            FillCustomerAdd();

            //Validate Values Format
            LogicLayer.Validation.Custom_Validation.PersonFormatValidation.ValidateValues(_customerAdd.PersonAddDto);

            await CustomerService.AddCustomerAsync(_customerAdd);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تمت الإضافة بنجاح");
            this.Close();
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {

                    var CustomerService = scope.ServiceProvider.GetRequiredService<CustomerService>();

                    if (State == Enums.FormStateEnum.AddNew)
                    {
                       await AddCustomer(CustomerService);
                    }
                    else if (State == Enums.FormStateEnum.Update)
                    {
                       await UpdateCustomer(CustomerService);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                      ex,
                     "Unexpected error while Saving Customer");

                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
