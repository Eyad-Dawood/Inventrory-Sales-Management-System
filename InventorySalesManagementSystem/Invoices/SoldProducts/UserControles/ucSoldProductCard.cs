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
        private Color NotAvilabe = Color.DarkRed;
        private Color ZeroColor = Color.DarkGray;

        private string _ProductTypeName { get; set; }
        private string _ProductName { get; set; }
        private decimal _QuantityInStorage { get; set; }
        public int ProductId { get; private set; }
        private decimal _PricePerUnit { get; set; }
        public decimal TotalPrice { get
            {
                return Math.Round(_PricePerUnit * (numericUpDown1.Value), 2);
            }
        }
        private bool _IsAvailable { get; set; } = true;
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
        #region Ui
        private void ApplyStateStyle()
        {
            if (!_IsAvailable)
            {
                ApplyUnavailableStyle();
            }
            else if (numericUpDown1.Value == 0)
            {
                ApplyZeroStyle();
            }
            else
            {
                ApplyNormalStyle();
            }
        }

        private void ApplyUnavailableStyle()
        {

            SetTextColor(Color.Gray);
            SetBackColor(NotAvilabe);

            numericUpDown1.Enabled = false;
            numericUpDown1.Value = 0;
        }

        private void ApplyZeroStyle()
        {
            SetTextColor(Color.Gray);
            SetBackColor(ZeroColor);
            numericUpDown1.Enabled = true;
        }

        private void ApplyNormalStyle()
        {
            SetTextColor(Color.Black);
            SetBackColor(DefaultBackColor);
            numericUpDown1.Enabled = true;
        }

        private void SetTextColor(Color color)
        {
            rtbName.ForeColor = color;
            rtbQuantity.ForeColor = color;
            rtbPricePerUnit.ForeColor = color;
            rtbTotal.ForeColor = color;
        }
        private void SetBackColor(Color color)
        {
            rtbName.BackColor = color;
            rtbQuantity.BackColor = color;
            rtbPricePerUnit.BackColor = color;
            rtbTotal.BackColor = color;
        }

        #endregion
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

            if(!_IsAvailable)
                ApplyStateStyle();
            

            rtbTotal.Text = Math.Round(_PricePerUnit*(numericUpDown1.Value), 2)
                .ToString("N2");
        }

        private void FillData(string productTypeName, string productName, int productId, decimal quantityInStorage, decimal pricePerUnit, string unitName)
        {
            _ProductTypeName = productTypeName;
            _ProductName = productName;
            _QuantityInStorage = quantityInStorage;
            ProductId = productId;
            _PricePerUnit = pricePerUnit;
            _UnitName = unitName;

            //Validation Conditions
            numericUpDown1.Maximum = quantityInStorage;

            this.Enabled = true;

            //New Card Added
            OnQuantityChanged?.Invoke();
        }
        public void LoadData(string productTypeName, string productName, int productId, decimal quantityInStorage , decimal pricePerUnit , string unitName)
        {
            FillData(productTypeName, productName, productId, quantityInStorage, pricePerUnit, unitName);

            //Update UI
            UpdateUI();
        }
        public void LoadData(string productTypeName, string productName, int productId, decimal quantityInStorage, decimal pricePerUnit, string unitName , decimal Quantity , bool IsAvilable)
        {
            FillData(productTypeName, productName, productId, quantityInStorage, pricePerUnit, unitName);

            numericUpDown1.Value = Quantity;
            _IsAvailable = IsAvilable;

            //Update UI
            UpdateUI();

        }

        public SoldProductAddDto GetSoldProductData()
        {
            return new SoldProductAddDto()
            {
                ProductId = this.ProductId,
                Quantity = Math.Round(numericUpDown1.Value, 4),
                TakeBatchId = -1 // to be set later
            };
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemoveButtonClicked?.Invoke(this);
            OnQuantityChanged?.Invoke();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal total = numericUpDown1.Value * _PricePerUnit;

            rtbTotal.Text = Math.Round(total, 2)
                .ToString("N2");

            if (numericUpDown1.Value == 0 && _IsAvailable) //WHen its not avilable , it resets to zero , so add this condition so i can see the deffrance
                ApplyStateStyle();
            else if (_IsAvailable)
                ApplyStateStyle();

            OnQuantityChanged?.Invoke();
        }
    }
}
