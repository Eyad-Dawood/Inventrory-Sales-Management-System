using DataAccessLayer.Entities.Invoices;
using LogicLayer.Services.Invoices;
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

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmInvoiceDiscount : Form
    {
        private IServiceProvider _serviceProvider { set; get; }
        private int _invoiceId;

        public frmInvoiceDiscount(IServiceProvider serviceProvider, int InvoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = InvoiceId;
        }

        private void ValidateAdditional()
        {
            List<string> errors = new List<string>();

            if (!FormatValidation.IsValidDecimal(txtamount.Text.Trim()))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Invoice), nameof(Invoice.Discount));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }


        private async Task LoadInvoice()
        {
            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    var invoice = await service.GetInvoiceByIdAsync(_invoiceId);

                    txtAdditionalNotes.Text = invoice.Notes;
                    txtamount.Text = invoice.Discount.ToString("N2");
                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
            ex,
            "Unexpected error while Looking For Invocie");
                MessageBox.Show("حدث خطأ غير متوقع أثناء البحث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private async void frmInvoiceAdditionalFees_Load(object sender, EventArgs e)
        {
            await LoadInvoice();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task SaveInvoiceAsync()
        {
            ValidateAdditional();

            Cursor.Current = Cursors.WaitCursor;
            thebutton.Enabled = false;

            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    await service.AddDiscount(_invoiceId, Convert.ToDecimal(txtamount.Text.Trim()), txtAdditionalNotes.Text.Trim());
                }

                this.Close();
                MessageBox.Show("تم حفظ البيانات الجديدة", "تمت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Serilog.Log.Error(
            ex,
            "Unexpected error while Updating Invocie");
                MessageBox.Show("حدث خطأ غير متوقع أثناء التحديث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                thebutton.Enabled = true;
            }
        }


        private async void thebutton_Click(object sender, EventArgs e)
        {
            await SaveInvoiceAsync();
        }
    }
}
