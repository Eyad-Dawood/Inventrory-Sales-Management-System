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
            btnClearZeros = new Button();
            panel2 = new Panel();
            dgvData = new DataGridView();
            pnBottom.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // lbTotal
            // 
            lbTotal.Anchor = AnchorStyles.Right;
            lbTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTotal.Location = new Point(503, 3);
            lbTotal.Name = "lbTotal";
            lbTotal.RightToLeft = RightToLeft.Yes;
            lbTotal.Size = new Size(110, 30);
            lbTotal.TabIndex = 52;
            lbTotal.Text = "0.00";
            // 
            // pnBottom
            // 
            pnBottom.Controls.Add(btnDelete);
            pnBottom.Controls.Add(btnAddRecord);
            pnBottom.Controls.Add(btnAddProduct);
            pnBottom.Controls.Add(btnClearZeros);
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
            btnDelete.Location = new Point(378, 3);
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
            btnAddRecord.Location = new Point(414, 2);
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
            // btnClearZeros
            // 
            btnClearZeros.Anchor = AnchorStyles.Left;
            btnClearZeros.BackgroundImage = Properties.Resources.clean;
            btnClearZeros.BackgroundImageLayout = ImageLayout.Stretch;
            btnClearZeros.Location = new Point(34, 3);
            btnClearZeros.Name = "btnClearZeros";
            btnClearZeros.Size = new Size(30, 30);
            btnClearZeros.TabIndex = 53;
            btnClearZeros.TabStop = false;
            btnClearZeros.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvData);
            panel2.Controls.Add(pnBottom);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(616, 391);
            panel2.TabIndex = 54;
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
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lbTotal;
        private Panel pnBottom;
        private Panel panel2;
        private Button btnClearZeros;
        private Button btnAddProduct;
        private Button btnDelete;
        private Button btnAddRecord;
        private DataGridView dgvData;
    }
}
