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
            ucInvoiceDetails1.Enabled = false;
            ucInvoiceDetails1.Location = new Point(12, 80);
            ucInvoiceDetails1.Name = "ucInvoiceDetails1";
            ucInvoiceDetails1.Size = new Size(1527, 443);
            ucInvoiceDetails1.TabIndex = 0;
            // 
            // lbTitle
            // 
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(667, 28);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(241, 49);
            lbTitle.TabIndex = 50;
            lbTitle.Text = "بيانات الفاتورة";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmShowInvoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1544, 532);
            Controls.Add(lbTitle);
            Controls.Add(ucInvoiceDetails1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmShowInvoice";
            ResumeLayout(false);
        }

        #endregion

        private SoldProducts.UserControles.ucInvoiceDetails ucInvoiceDetails1;
        private Label lbTitle;
    }
}