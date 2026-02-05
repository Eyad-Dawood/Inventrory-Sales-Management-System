namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    partial class ucProductSelector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbTotal = new Label();
            pnBottom = new Panel();
            btnDelete = new Button();
            btnAddRecord = new Button();
            btnAddProduct = new Button();
            panel2 = new Panel();
            panel1 = new Panel();
            popupList = new ListBox();
            dgvData = new DataGridView();
            pnBottom.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // lbTotal
            // 
            lbTotal.Dock = DockStyle.Right;
            lbTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTotal.Location = new Point(506, 0);
            lbTotal.Name = "lbTotal";
            lbTotal.RightToLeft = RightToLeft.Yes;
            lbTotal.Size = new Size(110, 35);
            lbTotal.TabIndex = 52;
            lbTotal.Text = "0.00";
            lbTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnBottom
            // 
            pnBottom.Controls.Add(btnDelete);
            pnBottom.Controls.Add(btnAddRecord);
            pnBottom.Controls.Add(btnAddProduct);
            pnBottom.Controls.Add(lbTotal);
            pnBottom.Dock = DockStyle.Bottom;
            pnBottom.Location = new Point(0, 356);
            pnBottom.Name = "pnBottom";
            pnBottom.Size = new Size(616, 35);
            pnBottom.TabIndex = 53;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Left;
            btnDelete.BackgroundImage = Properties.Resources.DeleteIcon;
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.Location = new Point(434, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(30, 30);
            btnDelete.TabIndex = 56;
            btnDelete.TabStop = false;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAddRecord
            // 
            btnAddRecord.Anchor = AnchorStyles.Left;
            btnAddRecord.BackgroundImage = Properties.Resources.addIcon;
            btnAddRecord.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddRecord.Location = new Point(470, 2);
            btnAddRecord.Name = "btnAddRecord";
            btnAddRecord.Size = new Size(30, 30);
            btnAddRecord.TabIndex = 55;
            btnAddRecord.TabStop = false;
            btnAddRecord.UseVisualStyleBackColor = true;
            btnAddRecord.Click += btnAddRecord_Click;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Anchor = AnchorStyles.Left;
            btnAddProduct.BackgroundImage = Properties.Resources.addIcon;
            btnAddProduct.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddProduct.Location = new Point(2, 3);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(30, 30);
            btnAddProduct.TabIndex = 54;
            btnAddProduct.TabStop = false;
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(pnBottom);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(616, 391);
            panel2.TabIndex = 54;
            // 
            // panel1
            // 
            panel1.Controls.Add(popupList);
            panel1.Controls.Add(dgvData);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(616, 356);
            panel1.TabIndex = 58;
            // 
            // popupList
            // 
            popupList.DrawMode = DrawMode.OwnerDrawFixed;
            popupList.FormattingEnabled = true;
            popupList.IntegralHeight = false;
            popupList.ItemHeight = 24;
            popupList.Location = new Point(203, 177);
            popupList.Name = "popupList";
            popupList.Size = new Size(120, 94);
            popupList.TabIndex = 57;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 0);
            dgvData.Name = "dgvData";
            dgvData.RightToLeft = RightToLeft.Yes;
            dgvData.RowHeadersVisible = false;
            dgvData.Size = new Size(616, 356);
            dgvData.TabIndex = 54;
            dgvData.CellEndEdit += dgvData_CellEndEdit;
            dgvData.CellEnter += dgvData_CellEnter;
            dgvData.CellValueChanged += dgvData_CellValueChanged;
            dgvData.DataError += dgvData_DataError;
            dgvData.EditingControlShowing += dgvData_EditingControlShowing;
            // 
            // ucProductSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Enabled = false;
            Name = "ucProductSelector";
            Size = new Size(616, 391);
            pnBottom.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lbTotal;
        private Panel pnBottom;
        private Panel panel2;
        private Button btnAddProduct;
        private Button btnDelete;
        private Button btnAddRecord;
        private DataGridView dgvData;
        private Panel panel1;
        private ListBox popupList;
    }
}
