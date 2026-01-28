using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
using LogicLayer.Services.Invoices;
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

        public frmAddBatchToInvoice(IServiceProvider serviceProvider, int invoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = invoiceId;
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

                    MessageBox.Show("تم إضافة العملية بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            var takeBatch = ucAddTakeBatch1.GetTakeBatch();

            if (takeBatch.SoldProductAddDtos == null ||
                !takeBatch.SoldProductAddDtos.Any())
            {
                MessageBox.Show("لا توجد منتجات في الفاتورة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _ = AddBatch(takeBatch);
        }

        private void frmAddBatchToInvoice_Load(object sender, EventArgs e)
        {
            //Load
            ucAddTakeBatch1.Initialize(_serviceProvider);
            _ = ucInvoiceDetails1.ShowInvoice(_serviceProvider, _invoiceId);
        }
    }
}
