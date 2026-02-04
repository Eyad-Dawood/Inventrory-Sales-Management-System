namespace InventorySalesManagementSystem.Invoices
{
    partial class frmAddBatchToInvoice
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
            ucInvoiceDetails1 = new InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.ucInvoiceDetails();
            lbTitle = new Label();
            ucAddTakeBatch1 = new InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.ucAddTakeBatch();
            btnCancel = new Button();
            btnSaveBatch = new Button();
            SuspendLayout();
            // 
            // ucInvoiceDetails1
            // 
            ucInvoiceDetails1.Anchor = AnchorStyles.Left;
            ucInvoiceDetails1.Enabled = false;
            ucInvoiceDetails1.Location = new Point(75, 61);
            ucInvoiceDetails1.Name = "ucInvoiceDetails1";
            ucInvoiceDetails1.Size = new Size(1528, 443);
            ucInvoiceDetails1.TabIndex = 5;
            // 
            // lbTitle
            // 
            lbTitle.Anchor = AnchorStyles.Left;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(544, 7);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(590, 49);
            lbTitle.TabIndex = 50;
            lbTitle.Text = "إضافة عملية شراء";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucAddTakeBatch1
            // 
            ucAddTakeBatch1.Anchor = AnchorStyles.Left;
            ucAddTakeBatch1.Enabled = false;
            ucAddTakeBatch1.Location = new Point(138, 510);
            ucAddTakeBatch1.Name = "ucAddTakeBatch1";
            ucAddTakeBatch1.Size = new Size(1402, 520);
            ucAddTakeBatch1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Left;
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(1630, 1012);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSaveBatch
            // 
            btnSaveBatch.Anchor = AnchorStyles.Left;
            btnSaveBatch.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSaveBatch.ForeColor = Color.FromArgb(0, 192, 0);
            btnSaveBatch.Location = new Point(1630, 976);
            btnSaveBatch.Name = "btnSaveBatch";
            btnSaveBatch.Size = new Size(96, 30);
            btnSaveBatch.TabIndex = 2;
            btnSaveBatch.Text = "حفظ";
            btnSaveBatch.UseVisualStyleBackColor = true;
            btnSaveBatch.Click += btnSave_Click;
            // 
            // frmAddBatchToInvoice
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1738, 1045);
            Controls.Add(btnCancel);
            Controls.Add(btnSaveBatch);
            Controls.Add(lbTitle);
            Controls.Add(ucInvoiceDetails1);
            Controls.Add(ucAddTakeBatch1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmAddBatchToInvoice";
            Load += frmAddBatchToInvoice_Load;
            ResumeLayout(false);
        }

        #endregion

        private SoldProducts.UserControles.ucInvoiceDetails ucInvoiceDetails1;
        private Label lbTitle;
        private SoldProducts.UserControles.ucAddTakeBatch ucAddTakeBatch1;
        private Button btnCancel;
        private Button btnSaveBatch;
    }
}