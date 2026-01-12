namespace InventorySalesManagementSystem.Products
{
    partial class frmShowProduct
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
            ucProductShow1 = new ucProductShow();
            SuspendLayout();
            // 
            // ucProductShow1
            // 
            ucProductShow1.Enabled = false;
            ucProductShow1.Location = new Point(9, 10);
            ucProductShow1.Name = "ucProductShow1";
            ucProductShow1.Size = new Size(844, 127);
            ucProductShow1.TabIndex = 0;
            // 
            // frmShowProduct
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 143);
            Controls.Add(ucProductShow1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmShowProduct";
            ResumeLayout(false);
        }

        #endregion

        private ucProductShow ucProductShow1;
    }
}