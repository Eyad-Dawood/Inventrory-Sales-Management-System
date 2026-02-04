namespace InventorySalesManagementSystem.Payments
{
    partial class frmPaymentsListScreen
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
            components = new System.ComponentModel.Container();
            cms = new ContextMenuStrip(components);
            ShowCustomerMenuToolStrip = new ToolStripMenuItem();
            ShowInvoiceToolMenuStrip = new ToolStripMenuItem();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowDatePic = true;
            ucListView1.AllowEmptyFilter = true;
            ucListView1.AllowPaymentFilter = true;
            ucListView1.Size = new Size(1214, 576);
            // 
            // btnSelect
            // 
            btnSelect.Visible = false;
            // 
            // btnAdd
            // 
            btnAdd.Visible = false;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { ShowCustomerMenuToolStrip, ShowInvoiceToolMenuStrip });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(174, 64);
            // 
            // ShowCustomerMenuToolStrip
            // 
            ShowCustomerMenuToolStrip.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowCustomerMenuToolStrip.Image = Properties.Resources.ShowDetailsIcon;
            ShowCustomerMenuToolStrip.ImageScaling = ToolStripItemImageScaling.None;
            ShowCustomerMenuToolStrip.Name = "ShowCustomerMenuToolStrip";
            ShowCustomerMenuToolStrip.RightToLeft = RightToLeft.Yes;
            ShowCustomerMenuToolStrip.Size = new Size(173, 30);
            ShowCustomerMenuToolStrip.Text = "عرض العميل";
            ShowCustomerMenuToolStrip.Click += ShowCustomerMenuToolStrip_Click;
            // 
            // ShowInvoiceToolMenuStrip
            // 
            ShowInvoiceToolMenuStrip.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowInvoiceToolMenuStrip.Image = Properties.Resources.invoice__2_;
            ShowInvoiceToolMenuStrip.Name = "ShowInvoiceToolMenuStrip";
            ShowInvoiceToolMenuStrip.RightToLeft = RightToLeft.Yes;
            ShowInvoiceToolMenuStrip.Size = new Size(173, 30);
            ShowInvoiceToolMenuStrip.Text = "عرض الفاتورة";
            ShowInvoiceToolMenuStrip.Click += ShowInvoiceToolMenuStrip_Click;
            // 
            // frmPaymentsListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 711);
            MinimumSize = new Size(900, 700);
            Name = "frmPaymentsListScreen";
            Controls.SetChildIndex(ucListView1, 0);
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip cms;
        private ToolStripMenuItem ShowInvoiceToolMenuStrip;
        private ToolStripMenuItem ShowCustomerMenuToolStrip;
    }
}