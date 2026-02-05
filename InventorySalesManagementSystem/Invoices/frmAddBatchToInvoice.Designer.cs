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
            lbTitle = new Label();
            ucAddTakeBatch1 = new InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.ucAddTakeBatch();
            btnCancel = new Button();
            btnSaveBatch = new Button();
            panel1 = new Panel();
            lkShowInvoice = new LinkLabel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(752, 49);
            lbTitle.TabIndex = 50;
            lbTitle.Text = "إضافة عملية شراء";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucAddTakeBatch1
            // 
            ucAddTakeBatch1.Dock = DockStyle.Fill;
            ucAddTakeBatch1.Enabled = false;
            ucAddTakeBatch1.Location = new Point(0, 49);
            ucAddTakeBatch1.Name = "ucAddTakeBatch1";
            ucAddTakeBatch1.Size = new Size(752, 449);
            ucAddTakeBatch1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(551, 7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSaveBatch
            // 
            btnSaveBatch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveBatch.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSaveBatch.ForeColor = Color.FromArgb(0, 192, 0);
            btnSaveBatch.Location = new Point(653, 7);
            btnSaveBatch.Name = "btnSaveBatch";
            btnSaveBatch.Size = new Size(96, 30);
            btnSaveBatch.TabIndex = 2;
            btnSaveBatch.Text = "حفظ";
            btnSaveBatch.UseVisualStyleBackColor = true;
            btnSaveBatch.Click += btnSave_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(lkShowInvoice);
            panel1.Controls.Add(btnSaveBatch);
            panel1.Controls.Add(btnCancel);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 498);
            panel1.Name = "panel1";
            panel1.Size = new Size(752, 38);
            panel1.TabIndex = 51;
            // 
            // lkShowInvoice
            // 
            lkShowInvoice.Location = new Point(0, 7);
            lkShowInvoice.Name = "lkShowInvoice";
            lkShowInvoice.Size = new Size(111, 30);
            lkShowInvoice.TabIndex = 52;
            lkShowInvoice.TabStop = true;
            lkShowInvoice.Text = "عرض الفاتورة";
            lkShowInvoice.TextAlign = ContentAlignment.MiddleCenter;
            lkShowInvoice.LinkClicked += lkShowInvoice_LinkClicked;
            // 
            // frmAddBatchToInvoice
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(752, 536);
            Controls.Add(ucAddTakeBatch1);
            Controls.Add(panel1);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmAddBatchToInvoice";
            Load += frmAddBatchToInvoice_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lbTitle;
        private SoldProducts.UserControles.ucAddTakeBatch ucAddTakeBatch1;
        private Button btnCancel;
        private Button btnSaveBatch;
        private Panel panel1;
        private LinkLabel lkShowInvoice;
    }
}