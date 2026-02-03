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
            groupBox1 = new GroupBox();
            chkInvoice = new CheckBox();
            chkRefund = new CheckBox();
            cms.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowCancelButton = false;
            ucListView1.AllowDatePic = true;
            ucListView1.AllowEmptyFilter = true;
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
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(chkInvoice);
            groupBox1.Controls.Add(chkRefund);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.Location = new Point(402, 129);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.Yes;
            groupBox1.Size = new Size(138, 40);
            groupBox1.TabIndex = 42;
            groupBox1.TabStop = false;
            // 
            // chkInvoice
            // 
            chkInvoice.Checked = true;
            chkInvoice.CheckState = CheckState.Checked;
            chkInvoice.Location = new Point(72, 12);
            chkInvoice.Name = "chkInvoice";
            chkInvoice.Size = new Size(63, 23);
            chkInvoice.TabIndex = 1;
            chkInvoice.Text = "فاتورة";
            chkInvoice.UseVisualStyleBackColor = true;
            // 
            // chkRefund
            // 
            chkRefund.Checked = true;
            chkRefund.CheckState = CheckState.Checked;
            chkRefund.Location = new Point(6, 12);
            chkRefund.Name = "chkRefund";
            chkRefund.Size = new Size(62, 23);
            chkRefund.TabIndex = 0;
            chkRefund.Text = "مرتجع";
            chkRefund.UseVisualStyleBackColor = true;
            // 
            // frmPaymentsListScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1214, 661);
            Controls.Add(groupBox1);
            Name = "frmPaymentsListScreen";
            Controls.SetChildIndex(ucListView1, 0);
            Controls.SetChildIndex(groupBox1, 0);
            cms.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip cms;
        private ToolStripMenuItem ShowInvoiceToolMenuStrip;
        private GroupBox groupBox1;
        private CheckBox chkInvoice;
        private CheckBox chkRefund;
        private ToolStripMenuItem ShowCustomerMenuToolStrip;
    }
}