using DataAccessLayer.Entities.Invoices;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
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

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmAddInvoice : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private int _SelectedCustomerId = 0;
        private int? _SelectedWorkerId = null;

        public frmAddInvoice(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

       


        private async Task AddInvoice(TakeBatchAddDto takeBatchAdd)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

                    int userid = UserSession.CurrentUser != null ?
                        UserSession.CurrentUser.UserId
                        :
                        -1;

                    var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();


                    await service.AddInvoiceAsync(new InvoiceAddDto()
                    {
                        CustomerId = _SelectedCustomerId,
                        InvoiceType = rdSale.Checked? InvoiceType.Sale : InvoiceType.Evaluation,
                        WorkerId = _SelectedWorkerId
                    }
                    ,
                    takeBatchAdd
                    , userid);

                    MessageBox.Show($"تمت إضافة فاتورة بنجاح للعميل {rtbCustomer.Text}", "تمت الإضافة بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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
                catch (OperationFailedException ex)
                {
                    MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(
                          ex,
                         "Unexpected error while Saving Invoice");

                    MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }



        private void frmAddInvoice_Load(object sender, EventArgs e)
        {
            ucAddTakeBatch1.Initialize(_serviceProvider);
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_SelectedCustomerId <= 0)
            {
                MessageBox.Show("يرجى اختيار عميل للفاتورة", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var takeBatch = ucAddTakeBatch1.GetTakeBatch();

            if (takeBatch.SoldProductAddDtos == null ||
                !takeBatch.SoldProductAddDtos.Any())
            {
                MessageBox.Show("لا توجد منتجات في الفاتورة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await AddInvoice(takeBatch);   
                            
        }

        private void lkSelectCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmCustomerListScreen(_serviceProvider, selectButton: true);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _SelectedCustomerId = frm.SelectedCustomer.CustomerId;

                #region Customer
                //Prodcut Name
                rtbCustomer.BackColor = this.BackColor;
                rtbCustomer.Clear();

                rtbCustomer.SelectionColor = Color.Black;
                rtbCustomer.AppendText($"{frm.SelectedCustomer.FullName} ");

                rtbCustomer.SelectionColor = Color.DarkRed;
                rtbCustomer.AppendText($"({frm.SelectedCustomer.CustomerId})");
                #endregion
            }
        }

        private void lkSelectWorker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmWorkerListScreen(_serviceProvider, selectButton: true);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _SelectedWorkerId = frm.SelectedWorker.WorkerId;

                #region WorkerName
                //Prodcut Name
                rtbWorker.BackColor = this.BackColor;
                rtbWorker.Clear();

                rtbWorker.SelectionColor = Color.Black;
                rtbWorker.AppendText($"{frm.SelectedWorker.FullName} ");

                rtbWorker.SelectionColor = Color.DarkRed;
                rtbWorker.AppendText($"({frm.SelectedWorker.WorkerId})");
                #endregion
            }
        }

        private void lkDeleteWorker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SelectedWorkerId = null;
            rtbWorker.Clear();
            rtbWorker.Text = "----";
        }
    }
}
