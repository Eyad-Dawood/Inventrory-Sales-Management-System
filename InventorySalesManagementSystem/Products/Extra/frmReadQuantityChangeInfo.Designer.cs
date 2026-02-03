namespace InventorySalesManagementSystem.Products.Extra
{
    partial class frmReadQuantityChangeInfo
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
            btnSave = new Button();
            txtQuantity = new TextBox();
            label1 = new Label();
            cmpReason = new ComboBox();
            label2 = new Label();
            txtNote = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(274, 144);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 38;
            btnSave.Text = "تأكيد";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(79, 9);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(206, 23);
            txtQuantity.TabIndex = 37;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(292, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(86, 30);
            label1.TabIndex = 39;
            label1.Text = "الكمية : ";
            // 
            // cmpReason
            // 
            cmpReason.DropDownStyle = ComboBoxStyle.DropDownList;
            cmpReason.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmpReason.FormattingEnabled = true;
            cmpReason.Location = new Point(82, 40);
            cmpReason.Name = "cmpReason";
            cmpReason.RightToLeft = RightToLeft.Yes;
            cmpReason.Size = new Size(203, 29);
            cmpReason.TabIndex = 40;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(289, 39);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(89, 30);
            label2.TabIndex = 41;
            label2.Text = "السبب : ";
            // 
            // txtNote
            // 
            txtNote.Location = new Point(12, 74);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.RightToLeft = RightToLeft.Yes;
            txtNote.Size = new Size(273, 59);
            txtNote.TabIndex = 42;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(281, 74);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(104, 30);
            label3.TabIndex = 43;
            label3.Text = "ملحوظة : ";
            // 
            // frmReadQuantityChangeInfo
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(381, 179);
            Controls.Add(txtNote);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cmpReason);
            Controls.Add(btnSave);
            Controls.Add(txtQuantity);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmReadQuantityChangeInfo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSave;
        private TextBox txtQuantity;
        private Label label1;
        private ComboBox cmpReason;
        private Label label2;
        private TextBox txtNote;
        private Label label3;
    }
}