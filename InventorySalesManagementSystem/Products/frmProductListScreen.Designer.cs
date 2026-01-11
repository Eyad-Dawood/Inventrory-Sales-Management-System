namespace InventorySalesManagementSystem.Products
{
    partial class frmProductListScreen
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
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            panel4 = new Panel();
            btnAdd = new Button();
            panel2 = new Panel();
            lbTitle = new Label();
            cms = new ContextMenuStrip(components);
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            changeIsAvilableStateMenuStripItem = new ToolStripMenuItem();
            WithdrawMenustripItem = new ToolStripMenuItem();
            AddQuantityMenustripItem = new ToolStripMenuItem();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowSecondSearchBox = true;
            ucListView1.BackColor = SystemColors.Control;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 129);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(1214, 532);
            ucListView1.TabIndex = 22;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnAdd);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 88);
            panel4.Name = "panel4";
            panel4.Size = new Size(1214, 41);
            panel4.TabIndex = 23;
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
            // panel2
            // 
            panel2.Controls.Add(lbTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1214, 88);
            panel2.TabIndex = 24;
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
            lbTitle.Text = "شاشة المنتجات";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { updateMenustripItem, deleteMenustripItem, ShowMenustripItem, toolStripSeparator1, AddQuantityMenustripItem, WithdrawMenustripItem, changeIsAvilableStateMenuStripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(189, 212);
            // 
            // updateMenustripItem
            // 
            updateMenustripItem.Alignment = ToolStripItemAlignment.Right;
            updateMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            updateMenustripItem.Image = Properties.Resources.edit__1_;
            updateMenustripItem.Name = "updateMenustripItem";
            updateMenustripItem.RightToLeft = RightToLeft.Yes;
            updateMenustripItem.ShortcutKeyDisplayString = "";
            updateMenustripItem.Size = new Size(188, 30);
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
            deleteMenustripItem.Size = new Size(188, 30);
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
            ShowMenustripItem.Size = new Size(188, 30);
            ShowMenustripItem.Text = "عرض البيانات";
            ShowMenustripItem.Click += ShowMenustripItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(185, 6);
            // 
            // changeIsAvilableStateMenuStripItem
            // 
            changeIsAvilableStateMenuStripItem.Alignment = ToolStripItemAlignment.Right;
            changeIsAvilableStateMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            changeIsAvilableStateMenuStripItem.Image = Properties.Resources.power_button__1_;
            changeIsAvilableStateMenuStripItem.Name = "changeIsAvilableStateMenuStripItem";
            changeIsAvilableStateMenuStripItem.RightToLeft = RightToLeft.Yes;
            changeIsAvilableStateMenuStripItem.ShortcutKeyDisplayString = "";
            changeIsAvilableStateMenuStripItem.Size = new Size(188, 30);
            changeIsAvilableStateMenuStripItem.Tag = "";
            changeIsAvilableStateMenuStripItem.Text = "تغيير حالة المنتج";
            changeIsAvilableStateMenuStripItem.TextDirection = ToolStripTextDirection.Horizontal;
            changeIsAvilableStateMenuStripItem.Click += changeIsAvilableStateMenuStripItem_Click;
            // 
            // WithdrawMenustripItem
            // 
            WithdrawMenustripItem.Alignment = ToolStripItemAlignment.Right;
            WithdrawMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            WithdrawMenustripItem.Image = Properties.Resources.power_button__1_;
            WithdrawMenustripItem.Name = "WithdrawMenustripItem";
            WithdrawMenustripItem.RightToLeft = RightToLeft.Yes;
            WithdrawMenustripItem.ShortcutKeyDisplayString = "";
            WithdrawMenustripItem.Size = new Size(188, 30);
            WithdrawMenustripItem.Tag = "";
            WithdrawMenustripItem.Text = "سحب كمية";
            WithdrawMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            WithdrawMenustripItem.Click += WithdrawMenustripItem_Click;
            // 
            // AddQuantityMenustripItem
            // 
            AddQuantityMenustripItem.Alignment = ToolStripItemAlignment.Right;
            AddQuantityMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            AddQuantityMenustripItem.Image = Properties.Resources.power_button__1_;
            AddQuantityMenustripItem.Name = "AddQuantityMenustripItem";
            AddQuantityMenustripItem.RightToLeft = RightToLeft.Yes;
            AddQuantityMenustripItem.ShortcutKeyDisplayString = "";
            AddQuantityMenustripItem.Size = new Size(188, 30);
            AddQuantityMenustripItem.Tag = "";
            AddQuantityMenustripItem.Text = "إضافة كمية";
            AddQuantityMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            AddQuantityMenustripItem.Click += AddQuantityMenustripItem_Click;
            // 
            // frmProductListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(ucListView1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            MinimumSize = new Size(900, 700);
            Name = "frmProductListScreen";
            Load += frmProductListScreen_Load;
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UserControles.UcListView ucListView1;
        private Panel panel4;
        private Button btnAdd;
        private Panel panel2;
        private Label lbTitle;
        private ContextMenuStrip cms;
        private ToolStripMenuItem updateMenustripItem;
        private ToolStripMenuItem deleteMenustripItem;
        private ToolStripMenuItem ShowMenustripItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem changeIsAvilableStateMenuStripItem;
        private ToolStripMenuItem AddQuantityMenustripItem;
        private ToolStripMenuItem WithdrawMenustripItem;
    }
}