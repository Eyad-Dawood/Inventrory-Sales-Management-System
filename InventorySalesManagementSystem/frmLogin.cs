using DataAccessLayer.Entities;
using LogicLayer.DTOs.UserDTO;
using LogicLayer.Global.Users;
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

namespace InventorySalesManagementSystem
{
    public partial class frmLogin : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public frmLogin(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPassword.Text)
                ||string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("لا يمكن ترك إسم المستخدم أو كلمة المرور فارغين"); 
                return;
            }

            try
            {
                UserReadDto loginUser;

                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<UserService>();

                    loginUser = await service.ValidateUserCredentialsAsync(txtUserName.Text.Trim(),txtPassword.Text.Trim());

                    //Save the current user 
                    var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

                    UserSession.Login(loginUser);
                }


                this.DialogResult = DialogResult.OK;

            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody,ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (WrongPasswordException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                    "Error During Logging In , [{username}] [{password}]"
                    ,txtUserName.Text,
                    txtPassword.Text);

                MessageBox.Show("فشل الدخول","خطأ غير متوقع", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
