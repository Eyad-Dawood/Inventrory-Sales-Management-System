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
        private readonly IServiceProvider _serviceProvider;
        private readonly int _workerId;

        public frmShowWorker(IServiceProvider serviceProvider, int workerId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _workerId = workerId;
        }

        private async void frmShowWorker_Load(object sender, EventArgs e)
        {
                await ucWorkerShow1.ShowData(_serviceProvider, _workerId);
        }
    }

}
