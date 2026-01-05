namespace InventorySalesManagementSystem.Customers
{
    partial class frmAddUpdateCustomer
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
            lbTitle = new Label();
            uc_AddUpdatePerson1 = new InventorySalesManagementSystem.People.UcAddUpdatePerson();
            btnSave = new Button();
            btnCancel = new Button();
            lb_CustomerId = new Label();
            label9 = new Label();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(218, 9);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(241, 49);
            lbTitle.TabIndex = 15;
            lbTitle.Text = "إضافة عميل";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uc_AddUpdatePerson1
            // 
            uc_AddUpdatePerson1.Location = new Point(18, 93);
            uc_AddUpdatePerson1.Name = "uc_AddUpdatePerson1";
            uc_AddUpdatePerson1.Size = new Size(632, 208);
            uc_AddUpdatePerson1.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(545, 307);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 1;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(443, 307);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lb_CustomerId
            // 
            lb_CustomerId.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lb_CustomerId.ForeColor = Color.Red;
            lb_CustomerId.Location = new Point(352, 65);
            lb_CustomerId.Name = "lb_CustomerId";
            lb_CustomerId.RightToLeft = RightToLeft.Yes;
            lb_CustomerId.Size = new Size(179, 25);
            lb_CustomerId.TabIndex = 32;
            lb_CustomerId.Text = "??";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(542, 65);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(129, 25);
            label9.TabIndex = 31;
            label9.Text = "معرف العميل : ";
            // 
            // frmAddUpdateCustomer
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(682, 344);
            Controls.Add(lb_CustomerId);
            Controls.Add(label9);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(uc_AddUpdatePerson1);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmAddUpdateCustomer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private People.UcAddUpdatePerson uc_AddUpdatePerson1;
        private Button btnSave;
        private Button btnCancel;
        private Label lb_CustomerId;
        private Label label9;
    }
}