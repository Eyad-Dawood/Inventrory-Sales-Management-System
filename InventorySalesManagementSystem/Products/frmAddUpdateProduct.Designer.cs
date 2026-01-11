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
            lbId = new Label();
            label9 = new Label();
            lbTitle = new Label();
            label1 = new Label();
            txtProductName = new TextBox();
            label2 = new Label();
            cmpUnit = new ComboBox();
            txtProductTypeName = new TextBox();
            lkSelectProductType = new LinkLabel();
            label3 = new Label();
            txtBuyingPrice = new TextBox();
            label4 = new Label();
            txtSellingPrice = new TextBox();
            label5 = new Label();
            lkAddUnit = new LinkLabel();
            chkAvilable = new CheckBox();
            txtQuantity = new TextBox();
            lbStorageQuantity = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(513, 229);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(615, 229);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lbId
            // 
            lbId.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lbId.ForeColor = Color.Red;
            lbId.Location = new Point(412, 76);
            lbId.Name = "lbId";
            lbId.RightToLeft = RightToLeft.Yes;
            lbId.Size = new Size(162, 25);
            lbId.TabIndex = 35;
            lbId.Text = "----";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(580, 76);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(131, 25);
            label9.TabIndex = 34;
            label9.Text = "معرف الصنف : ";
            // 
            // lbTitle
            // 
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(243, 9);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(241, 49);
            lbTitle.TabIndex = 36;
            lbTitle.Text = "إضافة صنف";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label1.Location = new Point(656, 142);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(69, 25);
            label1.TabIndex = 37;
            label1.Text = "الإسم : ";
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(356, 145);
            txtProductName.Name = "txtProductName";
            txtProductName.RightToLeft = RightToLeft.Yes;
            txtProductName.Size = new Size(259, 23);
            txtProductName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label2.Location = new Point(641, 106);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(85, 25);
            label2.TabIndex = 39;
            label2.Text = "الموديل : ";
            // 
            // cmpUnit
            // 
            cmpUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmpUnit.FormattingEnabled = true;
            cmpUnit.Location = new Point(394, 184);
            cmpUnit.Name = "cmpUnit";
            cmpUnit.RightToLeft = RightToLeft.Yes;
            cmpUnit.Size = new Size(203, 23);
            cmpUnit.TabIndex = 2;
            // 
            // txtProductTypeName
            // 
            txtProductTypeName.BorderStyle = BorderStyle.FixedSingle;
            txtProductTypeName.Enabled = false;
            txtProductTypeName.Location = new Point(336, 109);
            txtProductTypeName.Name = "txtProductTypeName";
            txtProductTypeName.ReadOnly = true;
            txtProductTypeName.RightToLeft = RightToLeft.Yes;
            txtProductTypeName.Size = new Size(299, 23);
            txtProductTypeName.TabIndex = 42;
            txtProductTypeName.TabStop = false;
            // 
            // lkSelectProductType
            // 
            lkSelectProductType.AutoSize = true;
            lkSelectProductType.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkSelectProductType.Location = new Point(305, 113);
            lkSelectProductType.Name = "lkSelectProductType";
            lkSelectProductType.RightToLeft = RightToLeft.Yes;
            lkSelectProductType.Size = new Size(25, 13);
            lkSelectProductType.TabIndex = 0;
            lkSelectProductType.TabStop = true;
            lkSelectProductType.Text = "أختر";
            lkSelectProductType.LinkClicked += lkSelectProductType_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label3.Location = new Point(603, 181);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(122, 25);
            label3.TabIndex = 44;
            label3.Text = "وحدة القياس : ";
            // 
            // txtBuyingPrice
            // 
            txtBuyingPrice.Location = new Point(12, 111);
            txtBuyingPrice.Name = "txtBuyingPrice";
            txtBuyingPrice.Size = new Size(94, 23);
            txtBuyingPrice.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label4.Location = new Point(123, 108);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.Yes;
            label4.Size = new Size(110, 25);
            label4.TabIndex = 45;
            label4.Text = "سعر الشراء : ";
            // 
            // txtSellingPrice
            // 
            txtSellingPrice.Location = new Point(12, 148);
            txtSellingPrice.Name = "txtSellingPrice";
            txtSellingPrice.Size = new Size(94, 23);
            txtSellingPrice.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label5.Location = new Point(133, 143);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.Yes;
            label5.Size = new Size(100, 25);
            label5.TabIndex = 47;
            label5.Text = "سعر البيع : ";
            // 
            // lkAddUnit
            // 
            lkAddUnit.AutoSize = true;
            lkAddUnit.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkAddUnit.Location = new Point(352, 188);
            lkAddUnit.Name = "lkAddUnit";
            lkAddUnit.RightToLeft = RightToLeft.Yes;
            lkAddUnit.Size = new Size(36, 13);
            lkAddUnit.TabIndex = 3;
            lkAddUnit.TabStop = true;
            lkAddUnit.Text = "إضافة";
            lkAddUnit.LinkClicked += lkAddUnit_LinkClicked;
            // 
            // chkAvilable
            // 
            chkAvilable.AutoSize = true;
            chkAvilable.Checked = true;
            chkAvilable.CheckState = CheckState.Checked;
            chkAvilable.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkAvilable.Location = new Point(23, 222);
            chkAvilable.Name = "chkAvilable";
            chkAvilable.Size = new Size(64, 29);
            chkAvilable.TabIndex = 7;
            chkAvilable.Text = "متاح";
            chkAvilable.UseVisualStyleBackColor = true;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(12, 186);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(94, 23);
            txtQuantity.TabIndex = 6;
            // 
            // lbStorageQuantity
            // 
            lbStorageQuantity.AutoSize = true;
            lbStorageQuantity.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lbStorageQuantity.Location = new Point(106, 181);
            lbStorageQuantity.Name = "lbStorageQuantity";
            lbStorageQuantity.RightToLeft = RightToLeft.Yes;
            lbStorageQuantity.Size = new Size(127, 25);
            lbStorageQuantity.TabIndex = 50;
            lbStorageQuantity.Text = "كمية المخزون : ";
            // 
            // frmAddUpdateProduct
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(724, 263);
            Controls.Add(txtQuantity);
            Controls.Add(lbStorageQuantity);
            Controls.Add(chkAvilable);
            Controls.Add(txtSellingPrice);
            Controls.Add(label5);
            Controls.Add(txtBuyingPrice);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lkSelectProductType);
            Controls.Add(txtProductTypeName);
            Controls.Add(lkAddUnit);
            Controls.Add(cmpUnit);
            Controls.Add(label2);
            Controls.Add(txtProductName);
            Controls.Add(label1);
            Controls.Add(lbTitle);
            Controls.Add(lbId);
            Controls.Add(label9);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmAddUpdateProduct";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private Label lbId;
        private Label label9;
        private Label lbTitle;
        private Label label1;
        private TextBox txtProductName;
        private Label label2;
        private ComboBox cmpUnit;
        private TextBox txtProductTypeName;
        private LinkLabel lkSelectProductType;
        private Label label3;
        private TextBox txtBuyingPrice;
        private Label label4;
        private TextBox txtSellingPrice;
        private Label label5;
        private LinkLabel lkAddUnit;
        private CheckBox chkAvilable;
        private TextBox txtQuantity;
        private Label lbStorageQuantity;
    }
}