using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.PaymentDTO;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Payments;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace InventorySalesManagementSystem.Payments
{
    public partial class frmAddPayment : Form
    {
        private readonly int _invoiceId = 0;
        private readonly int _customerId = 0;
        private readonly PaymentReason _paymentReason;
        private readonly IServiceProvider _serviceProvider;
        private InvoiceReadDto _invoiceDto;

        public frmAddPayment(IServiceProvider serviceProvider, PaymentReason paymentReason, int InvoiceId, int CustomerId)
        {
            InitializeComponent();
            _paymentReason = paymentReason;
            _invoiceId = InvoiceId;
            _customerId = CustomerId;
            _serviceProvider = serviceProvider;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var CustomerService = scope.ServiceProvider.GetRequiredService<CustomerService>();
                    var InvoiceService = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    var customer = await CustomerService.GetCustomerByIdAsync(_customerId);

                    lbCustomer.Text = customer.FullName;



                    if (_invoiceId != null)
                    {
                        var Invoice = await InvoiceService.GetInvoiceByIdAsync((int)_invoiceId);

                        if (Invoice.CustomerId != customer.CustomerId)
                            throw new OperationFailedException("عدم تطابق بين معرف العميل , وبيانات الفاتورة");

                        lbInvoiceID.Text = Invoice.InvoiceId.ToString();
                        _invoiceDto = Invoice;

                        lbDueAmount.Text = _invoiceDto.AmountDue.ToString("N2");
                        lbPaied.Text = _invoiceDto.TotalPaid.ToString("N2");
                        lbRemaining.Text = _invoiceDto.Remaining.ToString("N2");
                        lbDiscount.Text = _invoiceDto.Discount.ToString("N2");
                    }

                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
                this.Close();
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
                this.Close();
            }
        }

        private async void frmAddPayment_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ValidateAmounts()
        {
            List<string> errors = new List<string>();
            string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Payment), nameof(Payment.Amount));

            if (!FormatValidation.IsValidDecimal(txtamount.Text.Trim()))
            {
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
        private void ValidateMainLogic(decimal amount)
        {
            if (_invoiceDto != null)
            {
                if (amount > _invoiceDto.Remaining)
                    throw new OperationFailedException("لا يمكن أن يكون المبلغ المدفوع أكبر من الباقي");
            }
            if (Convert.ToDecimal(txtamount.Text) <= 0)
            {
                throw new ValidationException(new List<string>()
                {
                    "المبلغ خارج النطاق المسموح"
                });
            }
        }
        private async Task AddPayment(decimal amount)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();
                var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

                int userid = UserSession.CurrentUser != null ?
                    UserSession.CurrentUser.UserId
                    :
                    -1;

                await service.AddInvoicePaymentAsync(new LogicLayer.DTOs.PaymentDTO.PaymentAddDto()
                {
                    Amount = amount,
                    CustomerId = _customerId,
                    InvoiceId = _invoiceId,
                    Notes = txtAdditionalNotes.Text.Trim(),
                    PaidBy = txtFrom.Text.Trim(),
                    RecivedBy = txtTo.Text.Trim(),
                    PaymentReason = _paymentReason
                }, userid
                );
            }
        }
        private async Task SaveAsync()
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                btnSavePayment.Enabled = false;


                ValidateAmounts();

                decimal amount = Convert.ToDecimal(txtamount.Text.Trim());

                ValidateMainLogic(amount);

                await AddPayment(amount);

                decimal remain = _invoiceDto.Remaining - amount;

                string Message = $"تم دفع مبلغ {amount.ToString("N2")} بنجاح للفاتورة , الباقي {remain.ToString("N2")}";

                MessageBox.Show(Message, "تمت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
                return;
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(string.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSavePayment.Enabled = true;
            }


        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private void lkshowCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var frm = new frmShowCustomer(_serviceProvider, _customerId);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
        }

        private void lkShowInvoice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_invoiceId == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Invoice)));
                return;
            }

            try
            {
                var frm = new frmShowInvoice(_serviceProvider, (int)_invoiceId);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
        }
    }
}
