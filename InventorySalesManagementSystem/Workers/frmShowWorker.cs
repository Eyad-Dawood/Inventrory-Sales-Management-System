using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Workers
{
    public partial class frmShowWorker : Form
    {
       
        public frmShowWorker(IServiceProvider serviceProvider,int WorkerId)
        {
            InitializeComponent();

            ucWorkerShow1.ShowData(serviceProvider, WorkerId);
        }
    }
}
