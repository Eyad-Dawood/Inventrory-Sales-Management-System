using InventorySalesManagementSystem.Backup;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.Payments;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.PricesLog;
using InventorySalesManagementSystem.Products.StockMovementLog;
using InventorySalesManagementSystem.Workers;

namespace InventorySalesManagementSystem
{
    public partial class FrmMain : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public FrmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void OpenForm<T>(
            T current,
            Func<T> factory,
            Action<T> setReference
        ) where T : Form
        {
            if (current != null && !current.IsDisposed)
            {
                current.BringToFront();
                return;
            }

            var frm = factory();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnMain.Controls.Add(frm);

            setReference(frm);

            frm.FormClosed += (s, e) =>
            {
                setReference(null);
            };

            frm.Show();
            frm.BringToFront();
        }

        private void Workers_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
                MainFormsContainer.frmWorkerListScreen,
                () => new frmWorkerListScreen(_serviceProvider, false),
                f => MainFormsContainer.frmWorkerListScreen = f
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Customers_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
                MainFormsContainer.frmCustomerListScreen,
                () => new frmCustomerListScreen(_serviceProvider, false),
                f => MainFormsContainer.frmCustomerListScreen = f
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Products_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
                MainFormsContainer.frmProductListScreen,
                () => new frmProductListScreen(_serviceProvider),
                f => MainFormsContainer.frmProductListScreen = f
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PriceLogs_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
                MainFormsContainer.frmPriceLogListScreen,
                () => new frmPriceLogListScreen(_serviceProvider),
                f => MainFormsContainer.frmPriceLogListScreen = f
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StorageLog_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
                MainFormsContainer.frmStockMovementLogListScreen,
                () => new FrmStockMovementLogListScreen(_serviceProvider),
                f => MainFormsContainer.frmStockMovementLogListScreen = f
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Invoices_Click(object sender, EventArgs e)
        {

        }

        private void Payments_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
               MainFormsContainer.frmPaymentsListScreen,
               () => new frmPaymentsListScreen(_serviceProvider),
               f => MainFormsContainer.frmPaymentsListScreen = f
               );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QuickInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
               MainFormsContainer.frmInvoiceSummaryListScreen,
               () => new frmInvoiceListScreen(_serviceProvider, selectButton: false, summaryMode: true),
               f => MainFormsContainer.frmInvoiceSummaryListScreen = f
               );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InvoiceManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(
               MainFormsContainer.frmInvoiceManagementListScreen,
               () => new frmInvoiceListScreen(_serviceProvider, selectButton: false, summaryMode: false),
               f => MainFormsContainer.frmInvoiceManagementListScreen = f
               );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmBackup(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
