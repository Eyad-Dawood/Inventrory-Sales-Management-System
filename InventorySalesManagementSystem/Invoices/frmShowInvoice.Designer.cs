namespace InventorySalesManagementSystem.Invoices
{
    partial class frmShowInvoice
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
            SuspendLayout();
            // 
            // ucInvoiceDetails1
            // 
            ucInvoiceDetails1.Dock = DockStyle.Fill;
            ucInvoiceDetails1.Enabled = false;
            ucInvoiceDetails1.Location = new Point(0, 65);
            ucInvoiceDetails1.Name = "ucInvoiceDetails1";
            ucInvoiceDetails1.Size = new Size(1191, 467);
            ucInvoiceDetails1.TabIndex = 0;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(1191, 65);
            lbTitle.TabIndex = 50;
            lbTitle.Text = "بيانات الفاتورة";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmShowInvoice
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1191, 532);
            Controls.Add(ucInvoiceDetails1);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmShowInvoice";
            Load += frmShowInvoice_Load;
            ResumeLayout(false);
        }

        #endregion

        private SoldProducts.UserControles.ucInvoiceDetails ucInvoiceDetails1;
        private Label lbTitle;
    }
}