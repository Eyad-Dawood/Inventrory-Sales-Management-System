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
        public frmShowProduct(IServiceProvider serviceProvider, int ProductId)
        {
            InitializeComponent();
            ucProductShow1.ShowData(serviceProvider, ProductId);
        }
    }
}
