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
            cms = new ContextMenuStrip(components);
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            changeActivationStateMenuStripItem = new ToolStripMenuItem();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // cms
            // 
            cms.ImageScalingSize = new Size(24, 24);
            cms.Items.AddRange(new ToolStripItem[] { updateMenustripItem, deleteMenustripItem, ShowMenustripItem, toolStripSeparator1, changeActivationStateMenuStripItem });
            cms.Name = "contextMenuStrip1";
            cms.RightToLeft = RightToLeft.Yes;
            cms.Size = new Size(195, 130);
            // 
            // updateMenustripItem
            // 
            updateMenustripItem.Alignment = ToolStripItemAlignment.Right;
            updateMenustripItem.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            updateMenustripItem.Image = Properties.Resources.EditIcon;
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
            deleteMenustripItem.Image = Properties.Resources.DeleteIcon;
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
            ShowMenustripItem.Image = Properties.Resources.ShowDetailsIcon;
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
            changeActivationStateMenuStripItem.Image = Properties.Resources.ActiovationButton;
            changeActivationStateMenuStripItem.Name = "changeActivationStateMenuStripItem";
            changeActivationStateMenuStripItem.RightToLeft = RightToLeft.Yes;
            changeActivationStateMenuStripItem.ShortcutKeyDisplayString = "";
            changeActivationStateMenuStripItem.Size = new Size(194, 30);
            changeActivationStateMenuStripItem.Tag = "";
            changeActivationStateMenuStripItem.Text = "تغيير حالة النشاط";
            changeActivationStateMenuStripItem.TextDirection = ToolStripTextDirection.Horizontal;
            changeActivationStateMenuStripItem.Click += changeActivationStateMenuStripItem_Click;
            // 
            // frmCustomerListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            MinimumSize = new Size(900, 700);
            Name = "frmCustomerListScreen";
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
        private ToolStripMenuItem changeActivationStateMenuStripItem;
    }
}