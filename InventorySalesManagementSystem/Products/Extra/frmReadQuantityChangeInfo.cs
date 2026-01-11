using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.Utilities;
using LogicLayer.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.Products.Extra
{
    public partial class frmReadQuantityChangeInfo : Form
    {
        public enum State { Add = 0 , Remove = 1}

        [Browsable(false)]
        [DefaultValue(0)]
        public decimal Quantity {  get; set; }

        [Browsable(false)]
        [DefaultValue(0)]
        public DataAccessLayer.Entities.Products.StockMovementReason Reason { get; set; }

        [Browsable(false)]
        [DefaultValue("")]
        public string Notes { get; set; }


        private void FillcmpReasons(State state)
        {
            if (state == State.Add)
                FillAddReasons();
            else if (state == State.Remove)
                FillRemoveReasons();
        }

        private void FillAddReasons()
        {
            var data = Enum.GetValues(typeof(StockMovementReason))
                .Cast<StockMovementReason>()
                .Where(r=> r==StockMovementReason.Purchase || r== StockMovementReason.Adjustment)
                .Select(r => new
                {
                    Value = r,
                    Display = r.GetDisplayName()
                })
                .ToList();

            cmpReason.DataSource = data;
            cmpReason.DisplayMember = "Display";
            cmpReason.ValueMember = "Value";
        }

        private void FillRemoveReasons()
        {
            var data = Enum.GetValues(typeof(StockMovementReason))
                .Cast<StockMovementReason>()
                .Where(r => r == StockMovementReason.Sale || r == StockMovementReason.Adjustment || r == StockMovementReason.Damage)
                .Select(r => new
                {
                    Value = r,
                    Display = r.GetDisplayName()
                })
                .ToList();

            cmpReason.DataSource = data;
            cmpReason.DisplayMember = "Display";
            cmpReason.ValueMember = "Value";
        }

        public frmReadQuantityChangeInfo(State state)
        {
            InitializeComponent();
            FillcmpReasons(state);
            cmpReason.SelectedIndex = 0;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //Validate Data 

            if (!decimal.TryParse(txtQuantity.Text, out var qty))
            {
                MessageBox.Show("الكمية تنسيقها غير صحيح");
                return;
            }
            if(qty <= 0)
            {
                MessageBox.Show("الكمية خارج النطاق المسموح");
                return;
            }

            if (cmpReason.SelectedValue == null)
            {
                MessageBox.Show($"يجب إختيار سبب");

                return;
            }

            Quantity = qty;

            if (cmpReason.SelectedValue is StockMovementReason selectedReason)
            {
                Reason = selectedReason;
            }

            Notes = txtNote.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}
