namespace InventorySalesManagementSystem.Products.ProductsTypes
{
    partial class frmProductTypeListScreen
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
            panel4 = new Panel();
            btnSelect = new Button();
            btnAdd = new Button();
            cms = new ContextMenuStrip(components);
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            panel1 = new Panel();
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            cms.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(lbTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(604, 55);
            panel2.TabIndex = 22;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Fill;
            lbTitle.Font = new Font("Calibri", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(604, 55);
            lbTitle.TabIndex = 19;
            lbTitle.Text = "شاشة الموديلات";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnSelect);
            panel4.Controls.Add(btnAdd);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 55);
            panel4.Name = "panel4";
            panel4.Size = new Size(604, 41);
            panel4.TabIndex = 23;
            // 
            // btnSelect
            // 
            btnSelect.Anchor = AnchorStyles.Right;
            btnSelect.BackgroundImage = Properties.Resources.CheckIcon;
            btnSelect.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect.Enabled = false;
            btnSelect.Location = new Point(518, 0);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(40, 40);
            btnSelect.TabIndex = 3;
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Visible = false;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.BackgroundImage = Properties.Resources.addIcon;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Location = new Point(564, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 2;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { updateMenustripItem, deleteMenustripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(175, 64);
            // 
            // updateMenustripItem
            // 
            updateMenustripItem.Alignment = ToolStripItemAlignment.Right;
            updateMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            updateMenustripItem.Image = Properties.Resources.EditIcon;
            updateMenustripItem.Name = "updateMenustripItem";
            updateMenustripItem.RightToLeft = RightToLeft.Yes;
            updateMenustripItem.ShortcutKeyDisplayString = "";
            updateMenustripItem.Size = new Size(174, 30);
            updateMenustripItem.Tag = "";
            updateMenustripItem.Text = "تعديل البيانات";
            updateMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            updateMenustripItem.Click += updateMenustripItem_Click;
            // 
            // deleteMenustripItem
            // 
            deleteMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            deleteMenustripItem.Image = Properties.Resources.DeleteIcon;
            deleteMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            deleteMenustripItem.Name = "deleteMenustripItem";
            deleteMenustripItem.RightToLeft = RightToLeft.Yes;
            deleteMenustripItem.Size = new Size(174, 30);
            deleteMenustripItem.Text = "حذف البيانات";
            deleteMenustripItem.Click += deleteMenustripItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(ucListView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 96);
            panel1.Name = "panel1";
            panel1.Size = new Size(604, 311);
            panel1.TabIndex = 24;
            // 
            // ucListView1
            // 
            ucListView1.BackColor = SystemColors.Control;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 0);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(604, 311);
            ucListView1.TabIndex = 0;
            // 
            // frmProductTypeListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(604, 407);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmProductTypeListScreen";
            Load += frmProductTypeListScreen_Load;
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            cms.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label lbTitle;
        private Panel panel4;
        private Button btnAdd;
        private ContextMenuStrip cms;
        private ToolStripMenuItem updateMenustripItem;
        private ToolStripMenuItem deleteMenustripItem;
        private Panel panel1;
        private UserControles.UcListView ucListView1;
        private Button btnSelect;
    }
}