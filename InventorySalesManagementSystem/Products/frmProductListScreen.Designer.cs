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
            cms = new ContextMenuStrip(components);
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            AddQuantityMenustripItem = new ToolStripMenuItem();
            WithdrawMenustripItem = new ToolStripMenuItem();
            changeIsAvilableStateMenuStripItem = new ToolStripMenuItem();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowSecondSearchBox = true;
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { updateMenustripItem, deleteMenustripItem, ShowMenustripItem, toolStripSeparator1, AddQuantityMenustripItem, WithdrawMenustripItem, changeIsAvilableStateMenuStripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(188, 190);
            // 
            // updateMenustripItem
            // 
            updateMenustripItem.Alignment = ToolStripItemAlignment.Right;
            updateMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            updateMenustripItem.Image = Properties.Resources.EditIcon;
            updateMenustripItem.Name = "updateMenustripItem";
            updateMenustripItem.RightToLeft = RightToLeft.Yes;
            updateMenustripItem.ShortcutKeyDisplayString = "";
            updateMenustripItem.Size = new Size(187, 30);
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
            deleteMenustripItem.Size = new Size(187, 30);
            deleteMenustripItem.Text = "حذف البيانات";
            deleteMenustripItem.Click += deleteMenustripItem_Click;
            // 
            // ShowMenustripItem
            // 
            ShowMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowMenustripItem.Image = Properties.Resources.ShowDetailsIcon;
            ShowMenustripItem.ImageScaling = ToolStripItemImageScaling.None;
            ShowMenustripItem.Name = "ShowMenustripItem";
            ShowMenustripItem.RightToLeft = RightToLeft.Yes;
            ShowMenustripItem.Size = new Size(187, 30);
            ShowMenustripItem.Text = "عرض البيانات";
            ShowMenustripItem.Click += ShowMenustripItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(184, 6);
            // 
            // AddQuantityMenustripItem
            // 
            AddQuantityMenustripItem.Alignment = ToolStripItemAlignment.Right;
            AddQuantityMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            AddQuantityMenustripItem.Image = Properties.Resources.depositIcon;
            AddQuantityMenustripItem.Name = "AddQuantityMenustripItem";
            AddQuantityMenustripItem.RightToLeft = RightToLeft.Yes;
            AddQuantityMenustripItem.ShortcutKeyDisplayString = "";
            AddQuantityMenustripItem.Size = new Size(187, 30);
            AddQuantityMenustripItem.Tag = "";
            AddQuantityMenustripItem.Text = "إضافة كمية";
            AddQuantityMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            AddQuantityMenustripItem.Click += AddQuantityMenustripItem_Click;
            // 
            // WithdrawMenustripItem
            // 
            WithdrawMenustripItem.Alignment = ToolStripItemAlignment.Right;
            WithdrawMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            WithdrawMenustripItem.Image = Properties.Resources.WithDrawIcon;
            WithdrawMenustripItem.Name = "WithdrawMenustripItem";
            WithdrawMenustripItem.RightToLeft = RightToLeft.Yes;
            WithdrawMenustripItem.ShortcutKeyDisplayString = "";
            WithdrawMenustripItem.Size = new Size(187, 30);
            WithdrawMenustripItem.Tag = "";
            WithdrawMenustripItem.Text = "سحب كمية";
            WithdrawMenustripItem.TextDirection = ToolStripTextDirection.Horizontal;
            WithdrawMenustripItem.Click += WithdrawMenustripItem_Click;
            // 
            // changeIsAvilableStateMenuStripItem
            // 
            changeIsAvilableStateMenuStripItem.Alignment = ToolStripItemAlignment.Right;
            changeIsAvilableStateMenuStripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            changeIsAvilableStateMenuStripItem.Image = Properties.Resources.ActiovationButton;
            changeIsAvilableStateMenuStripItem.Name = "changeIsAvilableStateMenuStripItem";
            changeIsAvilableStateMenuStripItem.RightToLeft = RightToLeft.Yes;
            changeIsAvilableStateMenuStripItem.ShortcutKeyDisplayString = "";
            changeIsAvilableStateMenuStripItem.Size = new Size(187, 30);
            changeIsAvilableStateMenuStripItem.Tag = "";
            changeIsAvilableStateMenuStripItem.Text = "تغيير حالة المنتج";
            changeIsAvilableStateMenuStripItem.TextDirection = ToolStripTextDirection.Horizontal;
            changeIsAvilableStateMenuStripItem.Click += changeIsAvilableStateMenuStripItem_Click;
            // 
            // frmProductListScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1214, 661);
            MinimumSize = new Size(900, 700);
            Name = "frmProductListScreen";
            Controls.SetChildIndex(ucListView1, 0);
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
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