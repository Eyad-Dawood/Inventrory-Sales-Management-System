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
            updateMenustripItem = new ToolStripMenuItem();
            deleteMenustripItem = new ToolStripMenuItem();
            ShowMenustripItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            changeActivationStateMenuStripItem = new ToolStripMenuItem();
            gpCraft = new GroupBox();
            chkOpen = new CheckBox();
            chkClose = new CheckBox();
            groupBox1 = new GroupBox();
            chkSell = new CheckBox();
            chkEvaluation = new CheckBox();
            cms.SuspendLayout();
            gpCraft.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowCancelButton = false;
            ucListView1.AllowEmptyFilter = true;
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
            // 
            // gpCraft
            // 
            gpCraft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gpCraft.Controls.Add(chkOpen);
            gpCraft.Controls.Add(chkClose);
            gpCraft.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gpCraft.Location = new Point(538, 128);
            gpCraft.Name = "gpCraft";
            gpCraft.RightToLeft = RightToLeft.Yes;
            gpCraft.Size = new Size(143, 40);
            gpCraft.TabIndex = 40;
            gpCraft.TabStop = false;
            // 
            // chkOpen
            // 
            chkOpen.AutoSize = true;
            chkOpen.Checked = true;
            chkOpen.CheckState = CheckState.Checked;
            chkOpen.Location = new Point(73, 11);
            chkOpen.Name = "chkOpen";
            chkOpen.Size = new Size(64, 23);
            chkOpen.TabIndex = 1;
            chkOpen.Text = "مفتوح";
            chkOpen.UseVisualStyleBackColor = true;
            // 
            // chkClose
            // 
            chkClose.AutoSize = true;
            chkClose.Location = new Point(4, 12);
            chkClose.Name = "chkClose";
            chkClose.Size = new Size(60, 23);
            chkClose.TabIndex = 0;
            chkClose.Text = "مغلق";
            chkClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(chkSell);
            groupBox1.Controls.Add(chkEvaluation);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.Location = new Point(404, 128);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.Yes;
            groupBox1.Size = new Size(132, 40);
            groupBox1.TabIndex = 41;
            groupBox1.TabStop = false;
            // 
            // chkSell
            // 
            chkSell.AutoSize = true;
            chkSell.Checked = true;
            chkSell.CheckState = CheckState.Checked;
            chkSell.Location = new Point(76, 12);
            chkSell.Name = "chkSell";
            chkSell.Size = new Size(46, 23);
            chkSell.TabIndex = 1;
            chkSell.Text = "بيع";
            chkSell.UseVisualStyleBackColor = true;
            // 
            // chkEvaluation
            // 
            chkEvaluation.AutoSize = true;
            chkEvaluation.Location = new Point(6, 12);
            chkEvaluation.Name = "chkEvaluation";
            chkEvaluation.Size = new Size(64, 23);
            chkEvaluation.TabIndex = 0;
            chkEvaluation.Text = "تسعير";
            chkEvaluation.UseVisualStyleBackColor = true;
            // 
            // frmInvoiceListScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(groupBox1);
            Controls.Add(gpCraft);
            MinimumSize = new Size(900, 700);
            Name = "frmInvoiceListScreen";
            Controls.SetChildIndex(ucListView1, 0);
            Controls.SetChildIndex(gpCraft, 0);
            Controls.SetChildIndex(groupBox1, 0);
            cms.ResumeLayout(false);
            gpCraft.ResumeLayout(false);
            gpCraft.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip cms;
        private ToolStripMenuItem updateMenustripItem;
        private ToolStripMenuItem deleteMenustripItem;
        private ToolStripMenuItem ShowMenustripItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem changeActivationStateMenuStripItem;
        private GroupBox gpCraft;
        private CheckBox chkOpen;
        private CheckBox chkClose;
        private GroupBox groupBox1;
        private CheckBox chkSell;
        private CheckBox chkEvaluation;
    }
}