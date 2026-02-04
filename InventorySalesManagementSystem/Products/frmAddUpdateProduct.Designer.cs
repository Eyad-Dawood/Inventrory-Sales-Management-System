namespace InventorySalesManagementSystem.Products
{
    partial class frmAddUpdateProduct
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCancel = new Button();
            btnSave = new Button();
            lbTitle = new Label();
            label2 = new Label();
            txtProductTypeName = new TextBox();
            lkSelectProductType = new LinkLabel();
            dgvData = new DataGridView();
            panel1 = new Panel();
            panel2 = new Panel();
            btnAddProduct = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(567, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(669, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(771, 62);
            lbTitle.TabIndex = 36;
            lbTitle.Text = "إضافة أصناف";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label2.Location = new Point(680, 7);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(85, 25);
            label2.TabIndex = 39;
            label2.Text = "الموديل : ";
            // 
            // txtProductTypeName
            // 
            txtProductTypeName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtProductTypeName.BorderStyle = BorderStyle.FixedSingle;
            txtProductTypeName.Enabled = false;
            txtProductTypeName.Location = new Point(381, 10);
            txtProductTypeName.Name = "txtProductTypeName";
            txtProductTypeName.ReadOnly = true;
            txtProductTypeName.RightToLeft = RightToLeft.Yes;
            txtProductTypeName.Size = new Size(299, 23);
            txtProductTypeName.TabIndex = 42;
            txtProductTypeName.TabStop = false;
            // 
            // lkSelectProductType
            // 
            lkSelectProductType.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkSelectProductType.Location = new Point(323, 11);
            lkSelectProductType.Name = "lkSelectProductType";
            lkSelectProductType.RightToLeft = RightToLeft.Yes;
            lkSelectProductType.Size = new Size(52, 20);
            lkSelectProductType.TabIndex = 0;
            lkSelectProductType.TabStop = true;
            lkSelectProductType.Text = "أختر";
            lkSelectProductType.TextAlign = ContentAlignment.MiddleCenter;
            lkSelectProductType.LinkClicked += lkSelectProductType_LinkClicked;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 98);
            dgvData.Name = "dgvData";
            dgvData.RightToLeft = RightToLeft.Yes;
            dgvData.RowHeadersVisible = false;
            dgvData.Size = new Size(771, 273);
            dgvData.TabIndex = 43;
            dgvData.DataError += dgvData_DataError;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnCancel);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 371);
            panel1.Name = "panel1";
            panel1.Size = new Size(771, 42);
            panel1.TabIndex = 44;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAddProduct);
            panel2.Controls.Add(btnDelete);
            panel2.Controls.Add(txtProductTypeName);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lkSelectProductType);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 62);
            panel2.Name = "panel2";
            panel2.Size = new Size(771, 36);
            panel2.TabIndex = 45;
            // 
            // btnAddProduct
            // 
            btnAddProduct.BackgroundImage = Properties.Resources.addIcon;
            btnAddProduct.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddProduct.Location = new Point(3, 3);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(30, 30);
            btnAddProduct.TabIndex = 56;
            btnAddProduct.TabStop = false;
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = Properties.Resources.DeleteIcon;
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.Location = new Point(35, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(30, 30);
            btnDelete.TabIndex = 55;
            btnDelete.TabStop = false;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // frmAddUpdateProduct
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(771, 413);
            Controls.Add(dgvData);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmAddUpdateProduct";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private Label lbTitle;
        private Label label2;
        private TextBox txtProductTypeName;
        private LinkLabel lkSelectProductType;
        private DataGridView dgvData;
        private Panel panel1;
        private Panel panel2;
        private Button btnAddProduct;
        private Button btnDelete;
    }
}