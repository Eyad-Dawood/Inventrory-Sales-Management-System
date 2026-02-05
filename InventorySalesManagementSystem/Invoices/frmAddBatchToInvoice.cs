using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.DTOs.ProductDTO;
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
    public partial class frmAddBatchToInvoice : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _invoiceId;
        private readonly List<SoldProductSaleDetailsListDto> _products;
        public frmAddBatchToInvoice(IServiceProvider serviceProvider, int invoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = invoiceId;
            ucAddTakeBatch1.takeBatchType = TakeBatchType.Invoice;
        }
        public frmAddBatchToInvoice(IServiceProvider serviceProvider, int invoiceId, List<SoldProductSaleDetailsListDto> products)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = invoiceId;
            ucAddTakeBatch1.takeBatchType = TakeBatchType.Refund;
            _products = products;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task AddBatch(TakeBatchAddDto takeBatch)
        {
            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    var userSession = scope.ServiceProvider.GetRequiredService<UserSession>();

                    int UserId = userSession.CurrentUser!.UserId;

                    await service.AddBatchToInvoice(_invoiceId,
                        takeBatch
                         , UserId);

                    string op = takeBatch.TakeBatchType == TakeBatchType.Invoice ? "عملية الشراء" : "المرتجع";

                    MessageBox.Show($"تم إضافة {op} بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                      ex,
                     "Unexpected error while Saving Batch To Invoice");

                MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveBatch.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var takeBatch = ucAddTakeBatch1.GetTakeBatch();

                if (takeBatch.SoldProductAddDtos == null ||
                    !takeBatch.SoldProductAddDtos.Any())
                {
                    MessageBox.Show("لا توجد منتجات في الفاتورة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await AddBatch(takeBatch);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSaveBatch.Enabled = true;
            }
        }

        private void frmAddBatchToInvoice_Load(object sender, EventArgs e)
        {
             LoadFormAsync();
        }

        private void LoadFormAsync()
        {
            if (ucAddTakeBatch1.takeBatchType == TakeBatchType.Invoice)
            {
                ucAddTakeBatch1.Initialize(_serviceProvider);
            }
            else if (ucAddTakeBatch1.takeBatchType == TakeBatchType.Refund)
            {
                ucAddTakeBatch1.Initialize(_serviceProvider, _products);
            }
        }

        private void lkShowInvoice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                lkShowInvoice.Enabled = false;
                var frm = new frmShowInvoice(_serviceProvider, _invoiceId);
                frm.FormClosed += Frm_FormClosed;
                frm.Show();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ");
            }
        }

        private void Frm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            lkShowInvoice.Enabled = true;
        }
    }
}
