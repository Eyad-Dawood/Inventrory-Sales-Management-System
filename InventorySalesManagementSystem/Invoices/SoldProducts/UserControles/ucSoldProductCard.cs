using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucSoldProductCard : UserControl
    {
        private string _ProductTypeName { get; set; }
        private string _ProductName { get; set; }
        private decimal _QuantityInStorage { get; set; }
        private int _ProductId { get; set; }
        private decimal _PricePerUnit { get; set; }
        public decimal TotalPrice { get
            {
                return Math.Round(_PricePerUnit * (numericUpDown1.Value), 2);
            }
        }
        private string _UnitName { get; set; }


        [Browsable(false)]
        [DefaultValue(null)]
        public Action<ucSoldProductCard> OnRemoveButtonClicked { get; set;}

        [Browsable(false)]
        [DefaultValue(null)]
        public Action OnQuantityChanged { get; set; }


        public ucSoldProductCard()
        {
            InitializeComponent();
        }

        public void UpdateUI()
        {
            #region productNameFormat
            //Product Name
            rtbName.BackColor = this.BackColor;
            rtbName.Clear();

            rtbName.SelectionColor = Color.Black;
            rtbName.AppendText($"{_ProductTypeName} ");

            rtbName.SelectionColor = Color.DarkRed;
            rtbName.AppendText($"[{_ProductName}]");
            #endregion

            #region QuantityFormat
            // Quantity
            rtbQuantity.BackColor = this.BackColor;
            rtbQuantity.ReadOnly = true;
            rtbQuantity.BorderStyle = BorderStyle.None;
            rtbQuantity.Clear();

            // Format quantity safely (no precision issues)
            string quantityStr = _QuantityInStorage.ToString("0.0000",
                System.Globalization.CultureInfo.InvariantCulture);

            // Split integer & fraction parts
            string[] parts = quantityStr.Split('.');

            // Integer part -> Black
            rtbQuantity.SelectionColor = Color.Black;
            rtbQuantity.AppendText(parts[0]);

            // Fraction part -> DarkRed
            rtbQuantity.SelectionColor = Color.DarkRed;
            rtbQuantity.AppendText("." + parts[1]);
            #endregion

            #region PricePerUnit 
            rtbPricePerUnit.BackColor = this.BackColor;
            rtbPricePerUnit.Clear();

            // السعر – الحجم الطبيعي
            rtbPricePerUnit.SelectionColor = Color.Black;
            rtbPricePerUnit.SelectionFont = new Font(rtbPricePerUnit.Font, FontStyle.Regular);
            rtbPricePerUnit.AppendText($"{Math.Round(_PricePerUnit, 2):N2} ");

            // وحدة القياس – حجم أصغر
            rtbPricePerUnit.SelectionColor = Color.DarkRed;
            rtbPricePerUnit.SelectionFont = new Font(
                rtbPricePerUnit.Font.FontFamily,
                rtbPricePerUnit.Font.Size - 4,
                FontStyle.Regular
            );

            rtbPricePerUnit.AppendText($"[{_UnitName}]");
            #endregion

            rtbTotal.Text = Math.Round(_PricePerUnit*(numericUpDown1.Value), 2)
                .ToString("N2");

            OnQuantityChanged?.Invoke();
        }

        public void LoadData(string productTypeName, string productName, int productId, decimal quantityInStorage , decimal pricePerUnit , string unitName)
        {
            _ProductTypeName = productTypeName;
            _ProductName = productName;
            _QuantityInStorage = quantityInStorage;
            _ProductId = productId;
            _PricePerUnit = pricePerUnit;
            _UnitName = unitName;

            //Update UI
            UpdateUI();


            //Validation Conditions
            numericUpDown1.Maximum = quantityInStorage;

            this.Enabled = true;
        }

        public SoldProductAddDto GetSoldProductData()
        {
            return new SoldProductAddDto()
            {
                ProductId = this._ProductId,
                Quantity = Math.Round(numericUpDown1.Value, 4),
                TakeBatchId = -1 // to be set later
            };
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemoveButtonClicked?.Invoke(this);
            OnQuantityChanged();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal total = numericUpDown1.Value * _PricePerUnit;

            rtbTotal.Text = Math.Round(total, 2)
                .ToString("N2");

            OnQuantityChanged?.Invoke();
        }
    }
}
