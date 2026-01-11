namespace InventorySalesManagementSystem.Customers
{
    partial class frmCustomerListScreen
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
            panel1 = new Panel();
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            panel4 = new Panel();
            btnAdd = new Button();
            cms = new ContextMenuStrip(components);
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            changeActivationStateMenuStripItem = new ToolStripMenuItem();
            WithDrawMenuStripItem = new ToolStripMenuItem();
            panel2 = new Panel();
            lbTitle = new Label();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripMenuItem1 = new ToolStripMenuItem();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            cms.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(ucListView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 129);
            panel1.Name = "panel1";
            panel1.Size = new Size(1214, 532);
            panel1.TabIndex = 0;
            // 
            // ucListView1
            // 
            ucListView1.BackColor = SystemColors.Control;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 0);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(1214, 532);
            ucListView1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnAdd);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 88);
            panel4.Name = "panel4";
            panel4.Size = new Size(1214, 41);
            panel4.TabIndex = 19;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.BackgroundImage = Properties.Resources.add__1_;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Location = new Point(1162, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 2;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { updateMenustripItem, deleteMenustripItem, ShowMenustripItem, toolStripSeparator1, changeActivationStateMenuStripItem, toolStripSeparator2, toolStripMenuItem1, WithDrawMenuStripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(195, 218);
            // 
            // updateMenustripItem
            // 
            updateMenustripItem.Alignment = ToolStripItemAlignment.Right;
            updateMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            updateMenustripItem.Image = Properties.Resources.edit__1_;
            updateMenustripItem.Name = "updateMenustripItem";
            updateMenustripItem.RightToLeft = RightToLeft.Yes;
            updateMenustripItem.ShortcutKeyDisplayString = "";
            updateMenustripItem.Size = new Size(194, 30);
            updateMenustripItem.Tag = "";
            updateMenustripItem.Text = "تعديل البيانات";
            updateMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            updateMenustripItem.Click += updateMenustripItem_Click;
            // 
            // deleteMenustripItem
            // 
            deleteMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            deleteMenustripItem.Image = Properties.Resources.bin__1_;
            deleteMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            deleteMenustripItem.Name = "deleteMenustripItem";
            deleteMenustripItem.RightToLeft = RightToLeft.Yes;
            deleteMenustripItem.Size = new Size(194, 30);
            deleteMenustripItem.Text = "حذف البيانات";
            deleteMenustripItem.Click += deleteMenustripItem_Click;
            // 
            // ShowMenustripItem
            // 
            ShowMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowMenustripItem.Image = Properties.Resources.driver_license__1_;
            ShowMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            ShowMenustripItem.Name = "ShowMenustripItem";
            ShowMenustripItem.RightToLeft = RightToLeft.Yes;
            ShowMenustripItem.Size = new Size(194, 30);
            ShowMenustripItem.Text = "عرض البيانات";
            ShowMenustripItem.Click += ShowMenustripItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(191, 6);
            // 
            // changeActivationStateMenuStripItem
            // 
            changeActivationStateMenuStripItem.Alignment = ToolStripItemAlignment.Right;
            changeActivationStateMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            changeActivationStateMenuStripItem.Image = Properties.Resources.power_button__1_;
            changeActivationStateMenuStripItem.Name = "changeActivationStateMenuStripItem";
            changeActivationStateMenuStripItem.RightToLeft = RightToLeft.Yes;
            changeActivationStateMenuStripItem.ShortcutKeyDisplayString = "";
            changeActivationStateMenuStripItem.Size = new Size(194, 30);
            changeActivationStateMenuStripItem.Tag = "";
            changeActivationStateMenuStripItem.Text = "تغيير حالة النشاط";
            changeActivationStateMenuStripItem.TextDirection = ToolStripTextDirection.Horizontal;
            changeActivationStateMenuStripItem.Click += changeActivationStateMenuStripItem_Click;
            // 
            // WithDrawMenuStripItem
            // 
            WithDrawMenuStripItem.Alignment = ToolStripItemAlignment.Right;
            WithDrawMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            WithDrawMenuStripItem.Image = Properties.Resources.power_button__1_;
            WithDrawMenuStripItem.Name = "WithDrawMenuStripItem";
            WithDrawMenuStripItem.RightToLeft = RightToLeft.Yes;
            WithDrawMenuStripItem.ShortcutKeyDisplayString = "";
            WithDrawMenuStripItem.Size = new Size(194, 30);
            WithDrawMenuStripItem.Tag = "";
            WithDrawMenuStripItem.Text = "سحب كمية";
            WithDrawMenuStripItem.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // panel2
            // 
            panel2.Controls.Add(lbTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1214, 88);
            panel2.TabIndex = 21;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Fill;
            lbTitle.Font = new Font("Calibri", 60F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(1214, 88);
            lbTitle.TabIndex = 19;
            lbTitle.Text = "شاشة العملاء";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(191, 6);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            toolStripMenuItem1.Image = Properties.Resources.power_button__1_;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.RightToLeft = RightToLeft.Yes;
            toolStripMenuItem1.ShortcutKeyDisplayString = "";
            toolStripMenuItem1.Size = new Size(194, 30);
            toolStripMenuItem1.Tag = "";
            toolStripMenuItem1.Text = "إضافة كمية";
            toolStripMenuItem1.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // frmCustomerListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            MinimumSize = new Size(900, 700);
            Name = "frmCustomerListScreen";
            Load += frmCustomerListScreen_Load;
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            cms.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private InventorySalesManagementSystem.UserControles.UcListView ucListView1;
        private Panel panel4;
        private Button btnAdd;
        private ContextMenuStrip cms;
        private ToolStripMenuItem updateMenustripItem;
        private ToolStripMenuItem deleteMenustripItem;
        private ToolStripMenuItem ShowMenustripItem;
        private Panel panel2;
        private Label lbTitle;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem changeActivationStateMenuStripItem;
        private ToolStripMenuItem WithDrawMenuStripItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripMenuItem1;
    }
}