namespace InventorySalesManagementSystem.Products.StockMovementLog
{
    partial class FrmStockMovementLogListScreen
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
            panel2 = new Panel();
            lbTitle = new Label();
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            cms = new ContextMenuStrip(components);
            ShowMenustripItem = new ToolStripMenuItem();
            panel2.SuspendLayout();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(lbTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1214, 100);
            panel2.TabIndex = 27;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Fill;
            lbTitle.Font = new Font("Calibri", 60F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(1214, 100);
            lbTitle.TabIndex = 19;
            lbTitle.Text = "سجلات حركة المخزون";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucListView1
            // 
            ucListView1.AllowDatePic = true;
            ucListView1.BackColor = SystemColors.Control;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 100);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(1214, 561);
            ucListView1.TabIndex = 28;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { ShowMenustripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(189, 56);
            // 
            // ShowMenustripItem
            // 
            ShowMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowMenustripItem.Image = Properties.Resources.ShowDetailsIcon;
            ShowMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            ShowMenustripItem.Name = "ShowMenustripItem";
            ShowMenustripItem.RightToLeft = RightToLeft.Yes;
            ShowMenustripItem.Size = new Size(188, 30);
            ShowMenustripItem.Text = "عرض البيانات";
            ShowMenustripItem.Click += ShowMenustripItem_Click;
            // 
            // FrmStockMovementLogListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(ucListView1);
            Controls.Add(panel2);
            MinimumSize = new Size(900, 700);
            Name = "FrmStockMovementLogListScreen";
            Load += FrmStockMovementLogListScreen_Load;
            panel2.ResumeLayout(false);
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Label lbTitle;
        private UserControles.UcListView ucListView1;
        private ContextMenuStrip cms;
        private ToolStripMenuItem ShowMenustripItem;
    }
}