using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Validation;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Products;
using LogicLayer.Validation;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmAddInvoice : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private int _SelectedCustomerId = 0;
        private int? _SelectedWorkerId = null;

        private bool _IsEvaluationToSaleMode = false;
        private List<SoldProductSaleDetailsListDto> _products = null;

        public frmAddInvoice(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IsEvaluationToSaleMode = false;
        }
        public frmAddInvoice(IServiceProvider serviceProvider, int CustomerId, int? WorkerId, List<SoldProductSaleDetailsListDto> products)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IsEvaluationToSaleMode = true;
            _products = products;
            _SelectedCustomerId = CustomerId;
            _SelectedWorkerId = WorkerId;
        }


        private async Task AddInvoice(TakeBatchAddDto takeBatchAdd)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
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
                        WorkerId = _SelectedWorkerId,
                        Discount = Convert.ToDecimal(string.IsNullOrEmpty(txtAdditional.Text.Trim()) ? 0 : txtAdditional.Text.Trim()),
                        Notes = txtAdditionalNotes.Text.Trim()
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
                    MessageBox.Show(string.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _products = _products
                    .GroupBy(p => p.ProductId)
                    .Select(g =>
                    {
                        var p = g.First();
                        return new SoldProductSaleDetailsListDto
                        {
                            IsAvilable = p.IsAvilable,
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            ProductTypeName = p.ProductTypeName,
                            Quantity = g.Sum(x => x.Quantity),
                            QuantityInStorage = p.QuantityInStorage,
                            SellingPricePerUnit = p.SellingPricePerUnit,
                            SoldProductId = p.ProductId,
                        };
                    })
                    .ToList();


                ucAddTakeBatch1.Initialize(_serviceProvider, _products);

                using (var scope = _serviceProvider.CreateAsyncScope())
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

                    if (_SelectedWorkerId == null)
                    {
                        rtbWorker.Clear();
                        rtbWorker.Text = "----";
                        return;
                    }

                    var workerService = scope.ServiceProvider.GetRequiredService<WorkerService>();
                    var worker = await workerService.GetWorkerByIdAsync((int)_SelectedWorkerId);
                    if (worker == null)
                    {
                        MessageBox.Show("العامل المحدد غير موجود", "خطأ");
                        this.Close();
                        return;
                    }
                    SetEntityDisplay(rtbWorker, worker.FullName, worker.WorkerId);
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

        private void ValidateAdditional()
        {
            List<string> errors = new List<string>();

            if (!string.IsNullOrEmpty(txtAdditional.Text.Trim()) && !FormatValidation.IsValidDecimal(txtAdditional.Text.Trim()))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Invoice), nameof(Invoice.Discount));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
        private async Task SaveInvoiceAsync()
        {
            if (_SelectedCustomerId <= 0)
            {
                MessageBox.Show("يرجى اختيار عميل للفاتورة", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ValidateAdditional();

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

            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(string.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

        private void txtAdditional_Leave(object sender, EventArgs e)
        {
            decimal.TryParse(txtAdditional.Text, out decimal discount);

            try
            {
                ucAddTakeBatch1.GetProductSelector.UpdateTotal(discount);
            }
            catch(OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);

                txtAdditional.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ");

                txtAdditional.Focus();
            }

        }
    }
}
