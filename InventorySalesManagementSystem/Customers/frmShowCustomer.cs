using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Customers
{
    public partial class frmShowCustomer : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _customerId;

        public frmShowCustomer(IServiceProvider serviceProvider, int customerId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _customerId = customerId;
        }

        private async void frmShowCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                await ucCustomerShow1.ShowData(_serviceProvider, _customerId);
            }
            catch
            {
                MessageBox.Show("حدث خطأ أثناء تحميل بيانات العميل");
            }
        }
    }

}
