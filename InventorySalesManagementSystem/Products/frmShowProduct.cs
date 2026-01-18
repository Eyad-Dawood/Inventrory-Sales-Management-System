using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Products
{
    public partial class frmShowProduct : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _productId;

        public frmShowProduct(IServiceProvider serviceProvider, int productId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _productId = productId;
        }

        private async void frmShowProduct_Load(object sender, EventArgs e)
        {
             await ucProductShow1.ShowData(_serviceProvider, _productId);
        }
    }
}
