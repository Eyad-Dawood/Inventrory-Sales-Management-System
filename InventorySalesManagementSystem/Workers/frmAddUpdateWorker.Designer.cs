namespace InventorySalesManagementSystem.Workers
{
    partial class frmAddUpdateWorker
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
            lbId = new Label();
            label9 = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            uc_AddUpdatePerson1 = new InventorySalesManagementSystem.People.UcAddUpdatePerson();
            lbTitle = new Label();
            gpCraft = new GroupBox();
            chkPainter = new CheckBox();
            chkCarpenter = new CheckBox();
            gpCraft.SuspendLayout();
            SuspendLayout();
            // 
            // lbId
            // 
            lbId.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lbId.ForeColor = Color.Red;
            lbId.Location = new Point(346, 61);
            lbId.Name = "lbId";
            lbId.RightToLeft = RightToLeft.Yes;
            lbId.Size = new Size(179, 25);
            lbId.TabIndex = 38;
            lbId.Text = "---";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(536, 61);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(128, 25);
            label9.TabIndex = 37;
            label9.Text = "معرف العامل : ";
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(437, 345);
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
            btnSave.Location = new Point(539, 345);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 34;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // uc_AddUpdatePerson1
            // 
            uc_AddUpdatePerson1.Location = new Point(12, 89);
            uc_AddUpdatePerson1.Name = "uc_AddUpdatePerson1";
            uc_AddUpdatePerson1.Size = new Size(632, 208);
            uc_AddUpdatePerson1.TabIndex = 33;
            // 
            // lbTitle
            // 
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(212, 5);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(241, 49);
            lbTitle.TabIndex = 36;
            lbTitle.Text = "إضافة عامل";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gpCraft
            // 
            gpCraft.Controls.Add(chkPainter);
            gpCraft.Controls.Add(chkCarpenter);
            gpCraft.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gpCraft.Location = new Point(27, 303);
            gpCraft.Name = "gpCraft";
            gpCraft.Size = new Size(231, 45);
            gpCraft.TabIndex = 39;
            gpCraft.TabStop = false;
            gpCraft.Text = "الحرفة";
            // 
            // chkPainter
            // 
            chkPainter.AutoSize = true;
            chkPainter.Location = new Point(72, 16);
            chkPainter.Name = "chkPainter";
            chkPainter.Size = new Size(74, 29);
            chkPainter.TabIndex = 1;
            chkPainter.Text = "نقاش";
            chkPainter.UseVisualStyleBackColor = true;
            // 
            // chkCarpenter
            // 
            chkCarpenter.AutoSize = true;
            chkCarpenter.Location = new Point(6, 16);
            chkCarpenter.Name = "chkCarpenter";
            chkCarpenter.Size = new Size(61, 29);
            chkCarpenter.TabIndex = 0;
            chkCarpenter.Text = "نجار";
            chkCarpenter.UseVisualStyleBackColor = true;
            // 
            // frmAddUpdateWorker
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(679, 384);
            Controls.Add(gpCraft);
            Controls.Add(lbId);
            Controls.Add(label9);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(uc_AddUpdatePerson1);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmAddUpdateWorker";
            RightToLeft = RightToLeft.Yes;
            gpCraft.ResumeLayout(false);
            gpCraft.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbId;
        private Label label9;
        private Button btnCancel;
        private Button btnSave;
        private People.UcAddUpdatePerson uc_AddUpdatePerson1;
        private Label lbTitle;
        private GroupBox gpCraft;
        private CheckBox chkPainter;
        private CheckBox chkCarpenter;
    }
}