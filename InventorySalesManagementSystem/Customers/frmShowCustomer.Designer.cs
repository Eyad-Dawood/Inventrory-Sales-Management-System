namespace InventorySalesManagementSystem.Customers
{
    partial class frmShowCustomer
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
            ucCustomerShow1 = new ucCustomerShow();
            SuspendLayout();
            // 
            // ucCustomerShow1
            // 
            ucCustomerShow1.Enabled = false;
            ucCustomerShow1.Location = new Point(9, 6);
            ucCustomerShow1.Name = "ucCustomerShow1";
            ucCustomerShow1.Size = new Size(748, 116);
            ucCustomerShow1.TabIndex = 0;
            // 
            // frmShowCustomer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 131);
            Controls.Add(ucCustomerShow1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmShowCustomer";
            ResumeLayout(false);
        }

        #endregion

        private ucCustomerShow ucCustomerShow1;
    }
}