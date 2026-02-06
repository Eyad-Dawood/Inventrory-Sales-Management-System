namespace InventorySalesManagementSystem.Invoices
{
    partial class frmInvoiceDiscount
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
            txtamount = new TextBox();
            label1 = new Label();
            txtAdditionalNotes = new TextBox();
            label3 = new Label();
            thebutton = new Button();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(246, 138);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtamount
            // 
            txtamount.Location = new Point(228, 29);
            txtamount.Name = "txtamount";
            txtamount.Size = new Size(115, 23);
            txtamount.TabIndex = 15;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(362, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(84, 30);
            label1.TabIndex = 18;
            label1.Text = "المبلغ : ";
            // 
            // txtAdditionalNotes
            // 
            txtAdditionalNotes.Anchor = AnchorStyles.Right;
            txtAdditionalNotes.Location = new Point(12, 59);
            txtAdditionalNotes.Multiline = true;
            txtAdditionalNotes.Name = "txtAdditionalNotes";
            txtAdditionalNotes.RightToLeft = RightToLeft.Yes;
            txtAdditionalNotes.Size = new Size(331, 69);
            txtAdditionalNotes.TabIndex = 62;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(334, 60);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(114, 30);
            label3.TabIndex = 63;
            label3.Text = "ملاحظات : ";
            // 
            // thebutton
            // 
            thebutton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            thebutton.ForeColor = Color.Green;
            thebutton.Location = new Point(348, 140);
            thebutton.Name = "thebutton";
            thebutton.Size = new Size(95, 28);
            thebutton.TabIndex = 64;
            thebutton.Text = "حفظ";
            thebutton.UseVisualStyleBackColor = true;
            thebutton.Click += thebutton_Click;
            // 
            // frmInvoiceDiscount
            // 
            AcceptButton = thebutton;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(455, 175);
            Controls.Add(thebutton);
            Controls.Add(txtAdditionalNotes);
            Controls.Add(label3);
            Controls.Add(btnCancel);
            Controls.Add(txtamount);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmInvoiceDiscount";
            Load += frmInvoiceAdditionalFees_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private TextBox txtamount;
        private Label label1;
        private TextBox txtAdditionalNotes;
        private Label label3;
        private Button thebutton;
    }
}