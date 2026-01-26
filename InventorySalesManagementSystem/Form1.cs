using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.PricesLog;
using InventorySalesManagementSystem.Products.ProductsTypes;
using InventorySalesManagementSystem.Products.StockMovementLog;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace InventorySalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new frmCustomerListScreen(_serviceProvider, selectButton: false);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new frmWorkerListScreen(_serviceProvider, selectButton: false);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var frm = new frmProductTypeListScreen(_serviceProvider))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(frm.SelectedProductType.Name, frm.SelectedProductType.ProductTypeId.ToString());
                }
            }
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            var frm = new frmProductListScreen(_serviceProvider);
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmStockMovementLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmPriceLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var Session = scope.ServiceProvider.GetRequiredService<UserSession>();

                MessageBox.Show(Session.CurrentUser.UserName, Session.CurrentUser.UserId.ToString());
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            var SoldProducts = new List<LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto>()
            {
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 23,
                    Quantity = 1,
                    TakeBatchId = 2
                },
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 24,
                    Quantity = 1,
                    TakeBatchId = 2
                },
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 23,
                    Quantity = 1,
                    TakeBatchId = 2
                },
            };

            var TakeBatch = new TakeBatchAddDto()
            {
                InvoiceId = 1,
                TakeName = "Test Batch 21235",
                Notes = "Test Notes 3sdds1",
                SoldProductAddDtos = SoldProducts
            };

            var Inovice = new InvoiceAddDto()
            {
                CustomerId = 11,
                InvoiceType = InvoiceType.Sale,
                WorkerId = 25
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var InvoiceService = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    await InvoiceService.AddInvoiceAsync(Inovice, TakeBatch, 1);
                    MessageBox.Show("Take Batch Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                         "Unexpected error while Saving Product");

                    MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {

            var SoldProducts = new List<LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto>()
            {
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 23,
                    Quantity = .2M,
                    TakeBatchId = 2
                },
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 24,
                    Quantity = .2M,
                    TakeBatchId = 2
                },
                new LogicLayer.DTOs.InvoiceDTO.SoldProducts.SoldProductAddDto()
                {
                    ProductId = 23,
                    Quantity = .23M,
                    TakeBatchId = 2
                },
            };

            var TakeBatch = new TakeBatchAddDto()
            {
                InvoiceId = 1,
                TakeName = "New Batch",
                Notes = "Any thing",
                SoldProductAddDtos = SoldProducts
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var InvoiceService = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                    await InvoiceService.AddBatchToInvoice(1, TakeBatch, 1);
                    MessageBox.Show("Take Batch Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                         "Unexpected error while Saving Product");

                    MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmInvoiceListScreen(_serviceProvider,selectButton:false);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
