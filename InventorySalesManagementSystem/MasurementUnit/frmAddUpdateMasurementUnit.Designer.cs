namespace InventorySalesManagementSystem.MasurementUnits
{
    partial class frmAddUpdateMasurementUnit
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
            btnCancel = new Button();
            btnSave = new Button();
            txtUnitName = new TextBox();
            lb_Unit = new Label();
            label9 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(118, 108);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 35;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(220, 108);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 34;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtUnitName
            // 
            txtUnitName.Location = new Point(20, 61);
            txtUnitName.Name = "txtUnitName";
            txtUnitName.RightToLeft = RightToLeft.Yes;
            txtUnitName.Size = new Size(206, 23);
            txtUnitName.TabIndex = 33;
            // 
            // lb_Unit
            // 
            lb_Unit.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lb_Unit.ForeColor = Color.Red;
            lb_Unit.Location = new Point(166, 10);
            lb_Unit.Name = "lb_Unit";
            lb_Unit.RightToLeft = RightToLeft.Yes;
            lb_Unit.Size = new Size(60, 25);
            lb_Unit.TabIndex = 38;
            lb_Unit.Text = "---";
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(232, 10);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(84, 25);
            label9.TabIndex = 37;
            label9.Text = "المعرف : ";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(238, 61);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(81, 30);
            label1.TabIndex = 36;
            label1.Text = "الإسم : ";
            // 
            // frmAddUpdateMasurementUnit
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(339, 149);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtUnitName);
            Controls.Add(lb_Unit);
            Controls.Add(label9);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmAddUpdateMasurementUnit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private TextBox txtUnitName;
        private Label lb_Unit;
        private Label label9;
        private Label label1;
    }
}