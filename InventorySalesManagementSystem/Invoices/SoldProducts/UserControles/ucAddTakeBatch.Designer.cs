namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    partial class ucAddTakeBatch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtReciver = new TextBox();
            lbTakeNameLable = new Label();
            txtNote = new TextBox();
            label3 = new Label();
            gbInvoiceDetails = new GroupBox();
            lbOpenDate = new Label();
            label7 = new Label();
            lbInvoiceType = new Label();
            label4 = new Label();
            lbCustomerName = new Label();
            label2 = new Label();
            lbId = new Label();
            label9 = new Label();
            ucProductSelector1 = new ucProductSelector();
            gbInvoiceDetails.SuspendLayout();
            SuspendLayout();
            // 
            // txtReciver
            // 
            txtReciver.Location = new Point(763, 13);
            txtReciver.Name = "txtReciver";
            txtReciver.RightToLeft = RightToLeft.Yes;
            txtReciver.Size = new Size(528, 23);
            txtReciver.TabIndex = 1;
            // 
            // lbTakeNameLable
            // 
            lbTakeNameLable.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTakeNameLable.Location = new Point(1297, 10);
            lbTakeNameLable.Name = "lbTakeNameLable";
            lbTakeNameLable.RightToLeft = RightToLeft.Yes;
            lbTakeNameLable.Size = new Size(102, 30);
            lbTakeNameLable.TabIndex = 39;
            lbTakeNameLable.Text = "المستلم : ";
            // 
            // txtNote
            // 
            txtNote.Location = new Point(763, 61);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.RightToLeft = RightToLeft.Yes;
            txtNote.Size = new Size(535, 59);
            txtNote.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(1294, 61);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(104, 30);
            label3.TabIndex = 45;
            label3.Text = "ملحوظة : ";
            // 
            // gbInvoiceDetails
            // 
            gbInvoiceDetails.Controls.Add(lbOpenDate);
            gbInvoiceDetails.Controls.Add(label7);
            gbInvoiceDetails.Controls.Add(lbInvoiceType);
            gbInvoiceDetails.Controls.Add(label4);
            gbInvoiceDetails.Controls.Add(lbCustomerName);
            gbInvoiceDetails.Controls.Add(label2);
            gbInvoiceDetails.Controls.Add(lbId);
            gbInvoiceDetails.Controls.Add(label9);
            gbInvoiceDetails.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbInvoiceDetails.Location = new Point(3, 10);
            gbInvoiceDetails.Name = "gbInvoiceDetails";
            gbInvoiceDetails.RightToLeft = RightToLeft.Yes;
            gbInvoiceDetails.Size = new Size(674, 110);
            gbInvoiceDetails.TabIndex = 46;
            gbInvoiceDetails.TabStop = false;
            gbInvoiceDetails.Text = "معلومات الفاتورة";
            // 
            // lbOpenDate
            // 
            lbOpenDate.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbOpenDate.Location = new Point(7, 78);
            lbOpenDate.Margin = new Padding(4, 0, 4, 0);
            lbOpenDate.Name = "lbOpenDate";
            lbOpenDate.RightToLeft = RightToLeft.Yes;
            lbOpenDate.Size = new Size(254, 28);
            lbOpenDate.TabIndex = 65;
            lbOpenDate.Text = "----";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(275, 78);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(113, 25);
            label7.TabIndex = 64;
            label7.Text = "تاريخ الفتح  : ";
            // 
            // lbInvoiceType
            // 
            lbInvoiceType.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbInvoiceType.Location = new Point(411, 79);
            lbInvoiceType.Margin = new Padding(4, 0, 4, 0);
            lbInvoiceType.Name = "lbInvoiceType";
            lbInvoiceType.RightToLeft = RightToLeft.Yes;
            lbInvoiceType.Size = new Size(133, 28);
            lbInvoiceType.TabIndex = 63;
            lbInvoiceType.Text = "----";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(549, 82);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.Yes;
            label4.Size = new Size(118, 25);
            label4.TabIndex = 62;
            label4.Text = "نوع الفاتورة : ";
            // 
            // lbCustomerName
            // 
            lbCustomerName.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbCustomerName.Location = new Point(64, 53);
            lbCustomerName.Margin = new Padding(4, 0, 4, 0);
            lbCustomerName.Name = "lbCustomerName";
            lbCustomerName.RightToLeft = RightToLeft.Yes;
            lbCustomerName.Size = new Size(481, 28);
            lbCustomerName.TabIndex = 61;
            lbCustomerName.Text = "----";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(553, 53);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(116, 25);
            label2.TabIndex = 60;
            label2.Text = "إسم العميل : ";
            // 
            // lbId
            // 
            lbId.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lbId.ForeColor = Color.Red;
            lbId.Location = new Point(363, 26);
            lbId.Name = "lbId";
            lbId.RightToLeft = RightToLeft.Yes;
            lbId.Size = new Size(162, 25);
            lbId.TabIndex = 37;
            lbId.Text = "----";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(531, 26);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(133, 25);
            label9.TabIndex = 36;
            label9.Text = "معرف الفاتورة : ";
            // 
            // ucProductSelector1
            // 
            ucProductSelector1.Enabled = false;
            ucProductSelector1.Location = new Point(3, 129);
            ucProductSelector1.Name = "ucProductSelector1";
            ucProductSelector1.Size = new Size(1396, 391);
            ucProductSelector1.TabIndex = 47;
            // 
            // ucAddTakeBatch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucProductSelector1);
            Controls.Add(gbInvoiceDetails);
            Controls.Add(txtNote);
            Controls.Add(label3);
            Controls.Add(txtReciver);
            Controls.Add(lbTakeNameLable);
            Enabled = false;
            Name = "ucAddTakeBatch";
            Size = new Size(1402, 520);
            gbInvoiceDetails.ResumeLayout(false);
            gbInvoiceDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtReciver;
        private Label lbTakeNameLable;
        private TextBox txtNote;
        private Label label3;
        private GroupBox gbInvoiceDetails;
        private Label lbId;
        private Label label9;
        private Label lbCustomerName;
        private Label label2;
        private Label lbOpenDate;
        private Label label7;
        private Label lbInvoiceType;
        private Label label4;
        private ucProductSelector ucProductSelector1;
    }
}
