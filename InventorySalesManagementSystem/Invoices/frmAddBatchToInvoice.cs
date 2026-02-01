using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
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
    public partial class frmAddBatchToInvoice : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _invoiceId;

        public frmAddBatchToInvoice(IServiceProvider serviceProvider, int invoiceId , TakeBatchType takeBatchType)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = invoiceId;
            ucAddTakeBatch1.takeBatchType = takeBatchType;
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
                btnSave.Enabled = false;
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
                btnSave.Enabled = true;
            }
        }

        private async void frmAddBatchToInvoice_Load(object sender, EventArgs e)
        {
           await LoadFormAsync();
        }
        private async Task LoadFormAsync()
        {
              await ucInvoiceDetails1.ShowInvoice(_serviceProvider, _invoiceId);

            if (ucAddTakeBatch1.takeBatchType == TakeBatchType.Invoice)
            {
                ucAddTakeBatch1.Initialize(_serviceProvider);
            }
            else if (ucAddTakeBatch1.takeBatchType == TakeBatchType.Refund)

            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var invoiceService = scope.ServiceProvider.GetRequiredService<InvoiceService>();
                    var productService = scope.ServiceProvider.GetRequiredService<ProductService>();

                    var productsSummary =
                        await invoiceService.GetInvoiceProductSummaryAsync(_invoiceId);

                    var summaryDict = productsSummary
                        .ToDictionary(p => p.ProductId);

                    var products =
                        await productService.GetProductsByIdsAsync(summaryDict.Keys.ToList());

                    var productListDtos = new List<SoldProductWithProductListDto>();

                    foreach (var item in products)
                    {
                        if (!summaryDict.TryGetValue(item.ProductId, out var summary))
                            continue;

                        if (summary.NetSellingQuantity <= 0)
                            continue;

                        productListDtos.Add(new SoldProductWithProductListDto
                        {
                            ProductId = item.ProductId,
                            IsAvilable = item.IsAvailable,
                            ProductName = item.ProductName,
                            ProductTypeName = item.ProductType.ProductTypeName,
                            QuantityInStorage = summary.NetSellingQuantity, // Maximum refundable quantity
                            Quantity = 0,
                            SellingPricePerUnit = item.SellingPrice,
                            UnitName = item.MasurementUnit.UnitName,
                            SoldProductId = -1
                        });
                    }
                    ucAddTakeBatch1.Initialize(_serviceProvider, productListDtos);
                }
            }
        }
        
    }
}
