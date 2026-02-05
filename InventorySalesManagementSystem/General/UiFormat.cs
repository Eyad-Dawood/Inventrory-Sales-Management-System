using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesManagementSystem.General
{
    static public class UiFormat
    {
        public static string FormatNullableValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "----" : value;
        }

        public static void DgvDefualt(DataGridView dgv)
        {
            // Defualt Settings
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();


            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgv.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;


            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
    }
}
