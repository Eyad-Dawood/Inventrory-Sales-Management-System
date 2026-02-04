namespace InventorySalesManagementSystem.Invoices
{
    partial class frmInvoiceListScreen
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
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            AddNewBatchMenustripItem = new ToolStripMenuItem();
            CloseInvoiceMenustripItem = new ToolStripMenuItem();
            spEvalucation = new ToolStripSeparator();
            addRefundMenuStripItem = new ToolStripMenuItem();
            ConvertEvaluationToSaleInvoiceMenuStripItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            DiscountMenuStripItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            PayMenuStripItem = new ToolStripMenuItem();
            PayRefundMenuStripItem = new ToolStripMenuItem();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowEmptyFilter = true;
            ucListView1.AllowInvoiceFilter = true;
            ucListView1.Size = new Size(1262, 526);
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(1182, 0);
            // 
            // lbTitle
            // 
            lbTitle.Size = new Size(1262, 95);
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(1222, 0);
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { ShowMenustripItem, toolStripSeparator1, AddNewBatchMenustripItem, CloseInvoiceMenustripItem, spEvalucation, addRefundMenuStripItem, ConvertEvaluationToSaleInvoiceMenuStripItem, toolStripSeparator2, DiscountMenuStripItem, toolStripSeparator3, PayMenuStripItem, PayRefundMenuStripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(217, 268);
            cms.Opening += cms_Opening;
            // 
            // ShowMenustripItem
            // 
            ShowMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowMenustripItem.Image = Properties.Resources.ShowDetailsIcon;
            ShowMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            ShowMenustripItem.Name = "ShowMenustripItem";
            ShowMenustripItem.RightToLeft = RightToLeft.Yes;
            ShowMenustripItem.Size = new Size(216, 30);
            ShowMenustripItem.Text = "عرض البيانات";
            ShowMenustripItem.Click += ShowMenustripItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(213, 6);
            // 
            // AddNewBatchMenustripItem
            // 
            AddNewBatchMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            AddNewBatchMenustripItem.Image = Properties.Resources.addIcon;
            AddNewBatchMenustripItem.Name = "AddNewBatchMenustripItem";
            AddNewBatchMenustripItem.RightToLeft = RightToLeft.Yes;
            AddNewBatchMenustripItem.Size = new Size(216, 30);
            AddNewBatchMenustripItem.Text = "إضافة عملية شراء";
            AddNewBatchMenustripItem.Click += AddNewBatchMenuStripItem_Click;
            // 
            // CloseInvoiceMenustripItem
            // 
            CloseInvoiceMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            CloseInvoiceMenustripItem.Image = Properties.Resources.ActiovationButton;
            CloseInvoiceMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            CloseInvoiceMenustripItem.Name = "CloseInvoiceMenustripItem";
            CloseInvoiceMenustripItem.RightToLeft = RightToLeft.Yes;
            CloseInvoiceMenustripItem.Size = new Size(216, 30);
            CloseInvoiceMenustripItem.Text = "إغلاق الفاتورة";
            CloseInvoiceMenustripItem.Click += CloseInvoiceMenustripItem_Click;
            // 
            // spEvalucation
            // 
            spEvalucation.Name = "spEvalucation";
            spEvalucation.Size = new Size(213, 6);
            // 
            // addRefundMenuStripItem
            // 
            addRefundMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            addRefundMenuStripItem.Image = Properties.Resources.Refund;
            addRefundMenuStripItem.Name = "addRefundMenuStripItem";
            addRefundMenuStripItem.RightToLeft = RightToLeft.Yes;
            addRefundMenuStripItem.Size = new Size(216, 30);
            addRefundMenuStripItem.Text = "إضافة مرتجع";
            addRefundMenuStripItem.Click += addRefundMenuStripItem_Click;
            // 
            // ConvertEvaluationToSaleInvoiceMenuStripItem
            // 
            ConvertEvaluationToSaleInvoiceMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ConvertEvaluationToSaleInvoiceMenuStripItem.Image = Properties.Resources.BuyingIcon;
            ConvertEvaluationToSaleInvoiceMenuStripItem.Name = "ConvertEvaluationToSaleInvoiceMenuStripItem";
            ConvertEvaluationToSaleInvoiceMenuStripItem.RightToLeft = RightToLeft.Yes;
            ConvertEvaluationToSaleInvoiceMenuStripItem.Size = new Size(216, 30);
            ConvertEvaluationToSaleInvoiceMenuStripItem.Text = "تحويل إلى فاتورة بيع";
            ConvertEvaluationToSaleInvoiceMenuStripItem.Click += ConvertEvaluationToSaleInvoiceMenuStripItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(213, 6);
            // 
            // DiscountMenuStripItem
            // 
            DiscountMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            DiscountMenuStripItem.Image = Properties.Resources.Discount;
            DiscountMenuStripItem.Name = "DiscountMenuStripItem";
            DiscountMenuStripItem.RightToLeft = RightToLeft.Yes;
            DiscountMenuStripItem.Size = new Size(216, 30);
            DiscountMenuStripItem.Text = "خصم";
            DiscountMenuStripItem.Click += DiscountMenuStripItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(213, 6);
            // 
            // PayMenuStripItem
            // 
            PayMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            PayMenuStripItem.Image = Properties.Resources.wallet__1_;
            PayMenuStripItem.Name = "PayMenuStripItem";
            PayMenuStripItem.RightToLeft = RightToLeft.Yes;
            PayMenuStripItem.Size = new Size(216, 30);
            PayMenuStripItem.Text = "دفع";
            PayMenuStripItem.Click += PayMenuStripItem_Click;
            // 
            // PayRefundMenuStripItem
            // 
            PayRefundMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            PayRefundMenuStripItem.Image = Properties.Resources.transfer__1_;
            PayRefundMenuStripItem.Name = "PayRefundMenuStripItem";
            PayRefundMenuStripItem.RightToLeft = RightToLeft.Yes;
            PayRefundMenuStripItem.Size = new Size(216, 30);
            PayRefundMenuStripItem.Text = "دفع المرتجع";
            PayRefundMenuStripItem.Click += PayRefundMenuStripItem_Click;
            // 
            // frmInvoiceListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 661);
            MinimumSize = new Size(900, 700);
            Name = "frmInvoiceListScreen";
            Controls.SetChildIndex(ucListView1, 0);
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip cms;
        private ToolStripMenuItem ShowMenustripItem;
        private ToolStripMenuItem AddNewBatchMenustripItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem CloseInvoiceMenustripItem;
        private ToolStripSeparator spEvalucation;
        private ToolStripMenuItem ConvertEvaluationToSaleInvoiceMenuStripItem;
        private ToolStripMenuItem addRefundMenuStripItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem DiscountMenuStripItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem PayMenuStripItem;
        private ToolStripMenuItem PayRefundMenuStripItem;
    }
}