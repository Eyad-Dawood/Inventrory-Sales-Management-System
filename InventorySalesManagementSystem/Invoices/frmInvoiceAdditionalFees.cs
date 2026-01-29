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
    public partial class frmInvoiceAdditionalFees : Form
    {
        private IServiceProvider _serviceProvider { set; get; }
        private int _invoiceId ;

        public frmInvoiceAdditionalFees(IServiceProvider serviceProvider,int IvoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = IvoiceId;
        }

        private void ValidateAdditional()
        {
            List<string> errors = new List<string>();

            if (!FormatValidation.IsValidDecimal(txtamount.Text.Trim()))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Invoice), nameof(Invoice.Additional));
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

                    txtAdditionalNotes.Text = invoice.AdditionalNotes;
                    txtamount.Text = invoice.Additional.ToString("N4");
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

            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                   await service.UpdateInvoiceAdditional(_invoiceId, Convert.ToDecimal(txtamount.Text.Trim()), txtAdditionalNotes.Text.Trim());
                }
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

        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnSave.Enabled = false;

                await SaveInvoiceAsync();

                MessageBox.Show("تم حفظ البيانات الجديدة", "تمت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSave.Enabled = true;
            }
        }
    }
}
