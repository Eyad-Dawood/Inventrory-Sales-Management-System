namespace InventorySalesManagementSystem
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            Workers = new ToolStripMenuItem();
            Customers = new ToolStripMenuItem();
            Products = new ToolStripMenuItem();
            Invoices = new ToolStripMenuItem();
            Payments = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            PriceLogs = new ToolStripMenuItem();
            StorageLog = new ToolStripMenuItem();
            pnMain = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.ImageScalingSize = new Size(30, 30);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, Products, Invoices, Payments, toolStripMenuItem2 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1401, 53);
            menuStrip1.TabIndex = 14;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { Workers, Customers });
            toolStripMenuItem1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            toolStripMenuItem1.Image = Properties.Resources.group__1_;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(186, 49);
            toolStripMenuItem1.Text = "الأشخاص";
            // 
            // Workers
            // 
            Workers.Image = Properties.Resources.workers__1_;
            Workers.Name = "Workers";
            Workers.Size = new Size(203, 50);
            Workers.Text = "العمال";
            Workers.Click += Workers_Click;
            // 
            // Customers
            // 
            Customers.Image = Properties.Resources.customer__1_;
            Customers.Name = "Customers";
            Customers.Size = new Size(203, 50);
            Customers.Text = "العملاء";
            Customers.Click += Customers_Click;
            // 
            // Products
            // 
            Products.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            Products.Image = Properties.Resources.box__1_;
            Products.Name = "Products";
            Products.Size = new Size(178, 49);
            Products.Text = "المنتجات";
            Products.Click += Products_Click;
            // 
            // Invoices
            // 
            Invoices.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            Invoices.Image = Properties.Resources.invoice__1___1_;
            Invoices.Name = "Invoices";
            Invoices.Size = new Size(156, 49);
            Invoices.Text = "الفواتير";
            Invoices.Click += Invoices_Click;
            // 
            // Payments
            // 
            Payments.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            Payments.Image = Properties.Resources.wallet__2___1_;
            Payments.Name = "Payments";
            Payments.Size = new Size(204, 49);
            Payments.Text = "المدفوعات";
            Payments.Click += Payments_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { PriceLogs, StorageLog });
            toolStripMenuItem2.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            toolStripMenuItem2.Image = Properties.Resources.folder__1_;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(175, 49);
            toolStripMenuItem2.Text = "السجلات";
            // 
            // PriceLogs
            // 
            PriceLogs.Image = Properties.Resources.price_tag__1_;
            PriceLogs.Name = "PriceLogs";
            PriceLogs.Size = new Size(204, 50);
            PriceLogs.Text = "الأسعار";
            PriceLogs.Click += PriceLogs_Click;
            // 
            // StorageLog
            // 
            StorageLog.Image = Properties.Resources.boxes__1_;
            StorageLog.Name = "StorageLog";
            StorageLog.Size = new Size(204, 50);
            StorageLog.Text = "المخزون";
            StorageLog.Click += StorageLog_Click;
            // 
            // pnMain
            // 
            pnMain.Dock = DockStyle.Fill;
            pnMain.Location = new Point(0, 53);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(1401, 715);
            pnMain.TabIndex = 15;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1401, 768);
            Controls.Add(pnMain);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1278, 807);
            Name = "FrmMain";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem Products;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem Payments;
        private ToolStripMenuItem Invoices;
        private ToolStripMenuItem Workers;
        private ToolStripMenuItem Customers;
        private ToolStripMenuItem PriceLogs;
        private ToolStripMenuItem StorageLog;
        private Panel pnMain;
    }
}
