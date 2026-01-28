using DataAccessLayer.Entities.Invoices;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
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

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmAddInvoice : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private int _SelectedCustomerId = 0;
        private int? _SelectedWorkerId = null;

        private bool _IsEvaluationToSaleMode = false;
        private List<SoldProductWithProductListDto> _products = null;

        public frmAddInvoice(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IsEvaluationToSaleMode = false;
        }
        public frmAddInvoice(IServiceProvider serviceProvider, int CustomerId,List<SoldProductWithProductListDto> products)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IsEvaluationToSaleMode = true;
            _products = products;
            _SelectedCustomerId = CustomerId;
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
                        InvoiceType = rdSale.Checked ? InvoiceType.Sale : InvoiceType.Evaluation,
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



        private async void frmAddInvoice_Load(object sender, EventArgs e)
        {
            if (_IsEvaluationToSaleMode && (_products == null || _products.Count == 0))
            {
                MessageBox.Show("بيانات فاتورة التسعير غير صحيحة", "خطأ");
            }


            if (_IsEvaluationToSaleMode)
            {
                ucAddTakeBatch1.Initialize(_serviceProvider, _products);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var customerService = scope.ServiceProvider.GetRequiredService<CustomerService>();
                    var customer = await customerService.GetCustomerByIdAsync(_SelectedCustomerId);
                    if (customer == null)
                    {
                        MessageBox.Show("العميل المحدد غير موجود", "خطأ");
                        this.Close();
                        return;
                    }
                    SetEntityDisplay(rtbCustomer, customer.FullName, customer.CustomerId);
                }
            }
            else
            {
                ucAddTakeBatch1.Initialize(_serviceProvider);
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task SaveInvoiceAsync()
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
        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                await SaveInvoiceAsync();
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSave.Enabled = true;
            }
        }


        private void SetEntityDisplay(RichTextBox rtb, string name, int id)
        {
            rtb.BackColor = this.BackColor;
            rtb.Clear();

            rtb.SelectionColor = Color.Black;
            rtb.AppendText($"{name} ");

            rtb.SelectionColor = Color.DarkRed;
            rtb.AppendText($"({id})");
        }

        private void lkSelectCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmCustomerListScreen(_serviceProvider, selectButton: true);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _SelectedCustomerId = frm.SelectedCustomer.CustomerId;

                SetEntityDisplay(rtbCustomer, frm.SelectedCustomer.FullName, frm.SelectedCustomer.CustomerId);
            }
        }

        private void lkSelectWorker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmWorkerListScreen(_serviceProvider, selectButton: true);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _SelectedWorkerId = frm.SelectedWorker.WorkerId;

                SetEntityDisplay(rtbWorker, frm.SelectedWorker.FullName, frm.SelectedWorker.WorkerId);
            }
        }

        private void lkDeleteWorker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SelectedWorkerId = null;
            rtbWorker.Clear();
            rtbWorker.Text = "----";
        }

        private void rdEvaluation_CheckedChanged(object sender, EventArgs e)
        {
                ucAddTakeBatch1.AllowBathcData = !rdEvaluation.Checked;
        }
    }
}
