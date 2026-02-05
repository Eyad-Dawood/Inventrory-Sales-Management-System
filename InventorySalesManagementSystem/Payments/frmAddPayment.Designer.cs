namespace InventorySalesManagementSystem.Payments
{
    partial class frmAddPayment
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
            txtAdditionalNotes = new TextBox();
            label3 = new Label();
            btnCancel = new Button();
            txtamount = new TextBox();
            label1 = new Label();
            lbTitle = new Label();
            lbInvoiceID = new Label();
            label9 = new Label();
            lbCustomer = new Label();
            lkshowCustomer = new LinkLabel();
            label2 = new Label();
            lkShowInvoice = new LinkLabel();
            txtFrom = new TextBox();
            lbTakeNameLable = new Label();
            txtTo = new TextBox();
            label4 = new Label();
            lbPaied = new Label();
            lbDueAmount = new Label();
            lbDiscount = new Label();
            lbRemaining = new Label();
            label29 = new Label();
            label14 = new Label();
            label18 = new Label();
            label27 = new Label();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtAdditionalNotes
            // 
            txtAdditionalNotes.Anchor = AnchorStyles.None;
            txtAdditionalNotes.Location = new Point(10, 236);
            txtAdditionalNotes.Multiline = true;
            txtAdditionalNotes.Name = "txtAdditionalNotes";
            txtAdditionalNotes.RightToLeft = RightToLeft.Yes;
            txtAdditionalNotes.Size = new Size(312, 89);
            txtAdditionalNotes.TabIndex = 68;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(318, 236);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(114, 30);
            label3.TabIndex = 69;
            label3.Text = "ملاحظات : ";
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(587, 353);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 66;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtamount
            // 
            txtamount.Location = new Point(567, 133);
            txtamount.Name = "txtamount";
            txtamount.Size = new Size(115, 23);
            txtamount.TabIndex = 64;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(701, 128);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(84, 30);
            label1.TabIndex = 67;
            label1.Text = "المبلغ : ";
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(792, 59);
            lbTitle.TabIndex = 70;
            lbTitle.Text = "دفع الفاتورة";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbInvoiceID
            // 
            lbInvoiceID.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbInvoiceID.ForeColor = Color.Red;
            lbInvoiceID.Location = new Point(528, 94);
            lbInvoiceID.Name = "lbInvoiceID";
            lbInvoiceID.RightToLeft = RightToLeft.Yes;
            lbInvoiceID.Size = new Size(128, 25);
            lbInvoiceID.TabIndex = 72;
            lbInvoiceID.Text = "----";
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(671, 96);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(114, 21);
            label9.TabIndex = 71;
            label9.Text = "معرف الفاتورة : ";
            // 
            // lbCustomer
            // 
            lbCustomer.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbCustomer.Location = new Point(122, 59);
            lbCustomer.Margin = new Padding(4, 0, 4, 0);
            lbCustomer.Name = "lbCustomer";
            lbCustomer.RightToLeft = RightToLeft.Yes;
            lbCustomer.Size = new Size(534, 28);
            lbCustomer.TabIndex = 93;
            lbCustomer.Text = "----";
            // 
            // lkshowCustomer
            // 
            lkshowCustomer.Location = new Point(81, 66);
            lkshowCustomer.Name = "lkshowCustomer";
            lkshowCustomer.Size = new Size(34, 15);
            lkshowCustomer.TabIndex = 94;
            lkshowCustomer.TabStop = true;
            lkshowCustomer.Text = "عرض";
            lkshowCustomer.LinkClicked += lkshowCustomer_LinkClicked;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(673, 59);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(116, 25);
            label2.TabIndex = 92;
            label2.Text = "إسم العميل : ";
            // 
            // lkShowInvoice
            // 
            lkShowInvoice.Location = new Point(488, 101);
            lkShowInvoice.Name = "lkShowInvoice";
            lkShowInvoice.Size = new Size(34, 15);
            lkShowInvoice.TabIndex = 95;
            lkShowInvoice.TabStop = true;
            lkShowInvoice.Text = "عرض";
            lkShowInvoice.LinkClicked += lkShowInvoice_LinkClicked;
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(424, 177);
            txtFrom.Name = "txtFrom";
            txtFrom.RightToLeft = RightToLeft.Yes;
            txtFrom.Size = new Size(284, 23);
            txtFrom.TabIndex = 96;
            // 
            // lbTakeNameLable
            // 
            lbTakeNameLable.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTakeNameLable.Location = new Point(712, 170);
            lbTakeNameLable.Name = "lbTakeNameLable";
            lbTakeNameLable.RightToLeft = RightToLeft.Yes;
            lbTakeNameLable.Size = new Size(79, 30);
            lbTakeNameLable.TabIndex = 97;
            lbTakeNameLable.Text = "من يد : ";
            // 
            // txtTo
            // 
            txtTo.Location = new Point(12, 177);
            txtTo.Name = "txtTo";
            txtTo.RightToLeft = RightToLeft.Yes;
            txtTo.Size = new Size(296, 23);
            txtTo.TabIndex = 98;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label4.Location = new Point(314, 170);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.Yes;
            label4.Size = new Size(86, 30);
            label4.TabIndex = 99;
            label4.Text = "إلى يد : ";
            // 
            // lbPaied
            // 
            lbPaied.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbPaied.Location = new Point(442, 245);
            lbPaied.Margin = new Padding(4, 0, 4, 0);
            lbPaied.Name = "lbPaied";
            lbPaied.RightToLeft = RightToLeft.Yes;
            lbPaied.Size = new Size(203, 28);
            lbPaied.TabIndex = 101;
            lbPaied.Text = "----";
            // 
            // lbDueAmount
            // 
            lbDueAmount.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbDueAmount.Location = new Point(442, 210);
            lbDueAmount.Margin = new Padding(4, 0, 4, 0);
            lbDueAmount.Name = "lbDueAmount";
            lbDueAmount.RightToLeft = RightToLeft.Yes;
            lbDueAmount.Size = new Size(203, 28);
            lbDueAmount.TabIndex = 109;
            lbDueAmount.Text = "----";
            // 
            // lbDiscount
            // 
            lbDiscount.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbDiscount.Location = new Point(442, 279);
            lbDiscount.Margin = new Padding(4, 0, 4, 0);
            lbDiscount.Name = "lbDiscount";
            lbDiscount.RightToLeft = RightToLeft.Yes;
            lbDiscount.Size = new Size(203, 28);
            lbDiscount.TabIndex = 103;
            lbDiscount.Text = "----";
            // 
            // lbRemaining
            // 
            lbRemaining.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbRemaining.Location = new Point(442, 314);
            lbRemaining.Margin = new Padding(4, 0, 4, 0);
            lbRemaining.Name = "lbRemaining";
            lbRemaining.RightToLeft = RightToLeft.Yes;
            lbRemaining.Size = new Size(203, 28);
            lbRemaining.TabIndex = 107;
            lbRemaining.Text = "----";
            // 
            // label29
            // 
            label29.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label29.Location = new Point(637, 209);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.RightToLeft = RightToLeft.Yes;
            label29.Size = new Size(146, 25);
            label29.TabIndex = 108;
            label29.Text = "المبلغ المستحق : ";
            // 
            // label14
            // 
            label14.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(692, 243);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.RightToLeft = RightToLeft.Yes;
            label14.Size = new Size(91, 25);
            label14.TabIndex = 100;
            label14.Text = "المدفوع : ";
            // 
            // label18
            // 
            label18.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(713, 277);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.RightToLeft = RightToLeft.Yes;
            label18.Size = new Size(68, 25);
            label18.TabIndex = 102;
            label18.Text = "خصم : ";
            // 
            // label27
            // 
            label27.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label27.Location = new Point(662, 314);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.RightToLeft = RightToLeft.Yes;
            label27.Size = new Size(121, 25);
            label27.TabIndex = 106;
            label27.Text = "باقي السداد : ";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.ForestGreen;
            btnSave.Location = new Point(689, 353);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(92, 30);
            btnSave.TabIndex = 110;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click_1;
            // 
            // frmAddPayment
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(792, 395);
            Controls.Add(btnSave);
            Controls.Add(lbPaied);
            Controls.Add(lbDueAmount);
            Controls.Add(lbDiscount);
            Controls.Add(lbRemaining);
            Controls.Add(label29);
            Controls.Add(label14);
            Controls.Add(label18);
            Controls.Add(label27);
            Controls.Add(txtTo);
            Controls.Add(label4);
            Controls.Add(txtFrom);
            Controls.Add(lbTakeNameLable);
            Controls.Add(lkShowInvoice);
            Controls.Add(lbCustomer);
            Controls.Add(lkshowCustomer);
            Controls.Add(label2);
            Controls.Add(lbInvoiceID);
            Controls.Add(label9);
            Controls.Add(lbTitle);
            Controls.Add(txtAdditionalNotes);
            Controls.Add(label3);
            Controls.Add(btnCancel);
            Controls.Add(txtamount);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmAddPayment";
            Load += frmAddPayment_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAdditionalNotes;
        private Label label3;
        private Button btnCancel;
        private TextBox txtamount;
        private Label label1;
        private Label lbTitle;
        private Label lbInvoiceID;
        private Label label9;
        private Label lbCustomer;
        private LinkLabel lkshowCustomer;
        private Label label2;
        private LinkLabel lkShowInvoice;
        private TextBox txtFrom;
        private Label lbTakeNameLable;
        private TextBox txtTo;
        private Label label4;
        private Label lbPaied;
        private Label lbDueAmount;
        private Label lbDiscount;
        private Label lbRemaining;
        private Label label29;
        private Label label14;
        private Label label18;
        private Label label27;
        private Button btnSave;
    }
}