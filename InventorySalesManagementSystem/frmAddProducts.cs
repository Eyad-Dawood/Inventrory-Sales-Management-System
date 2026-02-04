using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem
{
    public partial class frmAddProducts : Form
    {
        public frmAddProducts()
        {
            InitializeComponent();

            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();

            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvData.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvData.Font, FontStyle.Bold);

            dgvData.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvData.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;


            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
    }
}
