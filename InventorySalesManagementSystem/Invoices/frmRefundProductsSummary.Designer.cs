namespace InventorySalesManagementSystem.Invoices
{
    partial class frmRefundProductsSummary
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
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowFilter = false;
            ucListView1.AllowPaging = false;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 0);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(769, 550);
            ucListView1.TabIndex = 1;
            // 
            // frmRefundProductsSummary
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(769, 550);
            Controls.Add(ucListView1);
            MinimumSize = new Size(785, 589);
            Name = "frmRefundProductsSummary";
            Load += frmRefundProductsSummary_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserControles.UcListView ucListView1;
    }
}