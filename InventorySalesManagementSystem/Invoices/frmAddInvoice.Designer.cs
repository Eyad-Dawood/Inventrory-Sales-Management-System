namespace InventorySalesManagementSystem.Invoices
{
    partial class frmAddInvoice
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
            gpCraft = new GroupBox();
            rdEvaluation = new RadioButton();
            rdSale = new RadioButton();
            lbTitle = new Label();
            panel3 = new Panel();
            panel1 = new Panel();
            panel4 = new Panel();
            rtbCustomer = new RichTextBox();
            lkSelectCustomer = new LinkLabel();
            label2 = new Label();
            panel2 = new Panel();
            panel5 = new Panel();
            rtbWorker = new RichTextBox();
            lkDeleteWorker = new LinkLabel();
            lkSelectWorker = new LinkLabel();
            label1 = new Label();
            ucAddTakeBatch1 = new InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.ucAddTakeBatch();
            btnCancel = new Button();
            btnSave = new Button();
            txtAdditional = new TextBox();
            label4 = new Label();
            txtAdditionalNotes = new TextBox();
            label3 = new Label();
            panel6 = new Panel();
            gpCraft.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // gpCraft
            // 
            gpCraft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gpCraft.Controls.Add(rdEvaluation);
            gpCraft.Controls.Add(rdSale);
            gpCraft.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gpCraft.Location = new Point(526, 7);
            gpCraft.Name = "gpCraft";
            gpCraft.RightToLeft = RightToLeft.Yes;
            gpCraft.Size = new Size(148, 72);
            gpCraft.TabIndex = 2;
            gpCraft.TabStop = false;
            gpCraft.Text = "نوع الفاتورة";
            // 
            // rdEvaluation
            // 
            rdEvaluation.Location = new Point(6, 34);
            rdEvaluation.Name = "rdEvaluation";
            rdEvaluation.Size = new Size(77, 29);
            rdEvaluation.TabIndex = 1;
            rdEvaluation.TabStop = true;
            rdEvaluation.Text = "تسعير";
            rdEvaluation.UseVisualStyleBackColor = true;
            rdEvaluation.CheckedChanged += rdEvaluation_CheckedChanged;
            // 
            // rdSale
            // 
            rdSale.Checked = true;
            rdSale.Location = new Point(89, 34);
            rdSale.Name = "rdSale";
            rdSale.Size = new Size(53, 29);
            rdSale.TabIndex = 0;
            rdSale.TabStop = true;
            rdSale.Text = "بيع";
            rdSale.UseVisualStyleBackColor = true;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(783, 66);
            lbTitle.TabIndex = 49;
            lbTitle.Text = "إضافة فاتورة";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(ucAddTakeBatch1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 66);
            panel3.Name = "panel3";
            panel3.Size = new Size(783, 466);
            panel3.TabIndex = 55;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(lkSelectCustomer);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(17, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(371, 36);
            panel1.TabIndex = 53;
            // 
            // panel4
            // 
            panel4.Controls.Add(rtbCustomer);
            panel4.Enabled = false;
            panel4.Location = new Point(32, 8);
            panel4.Name = "panel4";
            panel4.Size = new Size(250, 23);
            panel4.TabIndex = 65;
            // 
            // rtbCustomer
            // 
            rtbCustomer.BorderStyle = BorderStyle.None;
            rtbCustomer.Dock = DockStyle.Fill;
            rtbCustomer.Font = new Font("Calibri", 13.25F, FontStyle.Bold);
            rtbCustomer.Location = new Point(0, 0);
            rtbCustomer.Name = "rtbCustomer";
            rtbCustomer.ReadOnly = true;
            rtbCustomer.RightToLeft = RightToLeft.Yes;
            rtbCustomer.Size = new Size(250, 23);
            rtbCustomer.TabIndex = 65;
            rtbCustomer.Text = "----";
            // 
            // lkSelectCustomer
            // 
            lkSelectCustomer.AutoSize = true;
            lkSelectCustomer.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkSelectCustomer.Location = new Point(3, 13);
            lkSelectCustomer.Name = "lkSelectCustomer";
            lkSelectCustomer.RightToLeft = RightToLeft.Yes;
            lkSelectCustomer.Size = new Size(25, 13);
            lkSelectCustomer.TabIndex = 51;
            lkSelectCustomer.TabStop = true;
            lkSelectCustomer.Text = "أختر";
            lkSelectCustomer.LinkClicked += lkSelectCustomer_LinkClicked;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(276, 3);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(92, 30);
            label2.TabIndex = 50;
            label2.Text = "العميل : ";
            // 
            // panel2
            // 
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(lkDeleteWorker);
            panel2.Controls.Add(lkSelectWorker);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(17, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(371, 36);
            panel2.TabIndex = 54;
            // 
            // panel5
            // 
            panel5.Controls.Add(rtbWorker);
            panel5.Enabled = false;
            panel5.Location = new Point(84, 7);
            panel5.Name = "panel5";
            panel5.Size = new Size(198, 23);
            panel5.TabIndex = 66;
            // 
            // rtbWorker
            // 
            rtbWorker.BorderStyle = BorderStyle.None;
            rtbWorker.Dock = DockStyle.Fill;
            rtbWorker.Font = new Font("Calibri", 13.25F, FontStyle.Bold);
            rtbWorker.Location = new Point(0, 0);
            rtbWorker.Name = "rtbWorker";
            rtbWorker.ReadOnly = true;
            rtbWorker.RightToLeft = RightToLeft.Yes;
            rtbWorker.Size = new Size(198, 23);
            rtbWorker.TabIndex = 65;
            rtbWorker.Text = "----";
            // 
            // lkDeleteWorker
            // 
            lkDeleteWorker.ActiveLinkColor = Color.Blue;
            lkDeleteWorker.AutoSize = true;
            lkDeleteWorker.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkDeleteWorker.LinkColor = Color.Red;
            lkDeleteWorker.Location = new Point(3, 12);
            lkDeleteWorker.Name = "lkDeleteWorker";
            lkDeleteWorker.RightToLeft = RightToLeft.Yes;
            lkDeleteWorker.Size = new Size(33, 13);
            lkDeleteWorker.TabIndex = 53;
            lkDeleteWorker.TabStop = true;
            lkDeleteWorker.Text = "احذف";
            lkDeleteWorker.LinkClicked += lkDeleteWorker_LinkClicked;
            // 
            // lkSelectWorker
            // 
            lkSelectWorker.AutoSize = true;
            lkSelectWorker.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkSelectWorker.Location = new Point(42, 13);
            lkSelectWorker.Name = "lkSelectWorker";
            lkSelectWorker.RightToLeft = RightToLeft.Yes;
            lkSelectWorker.Size = new Size(25, 13);
            lkSelectWorker.TabIndex = 51;
            lkSelectWorker.TabStop = true;
            lkSelectWorker.Text = "أختر";
            lkSelectWorker.LinkClicked += lkSelectWorker_LinkClicked;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(276, 2);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(91, 30);
            label1.TabIndex = 50;
            label1.Text = "العامل : ";
            // 
            // ucAddTakeBatch1
            // 
            ucAddTakeBatch1.Dock = DockStyle.Fill;
            ucAddTakeBatch1.Enabled = false;
            ucAddTakeBatch1.Location = new Point(0, 0);
            ucAddTakeBatch1.Name = "ucAddTakeBatch1";
            ucAddTakeBatch1.Size = new Size(783, 466);
            ucAddTakeBatch1.TabIndex = 55;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(680, 49);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 57;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(680, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 56;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtAdditional
            // 
            txtAdditional.Anchor = AnchorStyles.Right;
            txtAdditional.Location = new Point(294, 15);
            txtAdditional.Name = "txtAdditional";
            txtAdditional.Size = new Size(94, 23);
            txtAdditional.TabIndex = 58;
            txtAdditional.Leave += txtAdditional_Leave;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(394, 15);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.Yes;
            label4.Size = new Size(56, 21);
            label4.TabIndex = 59;
            label4.Text = "خصم : ";
            // 
            // txtAdditionalNotes
            // 
            txtAdditionalNotes.Anchor = AnchorStyles.Right;
            txtAdditionalNotes.Location = new Point(85, 43);
            txtAdditionalNotes.Multiline = true;
            txtAdditionalNotes.Name = "txtAdditionalNotes";
            txtAdditionalNotes.RightToLeft = RightToLeft.Yes;
            txtAdditionalNotes.Size = new Size(331, 36);
            txtAdditionalNotes.TabIndex = 60;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(405, 44);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(112, 30);
            label3.TabIndex = 61;
            label3.Text = "التفاصيل : ";
            // 
            // panel6
            // 
            panel6.Controls.Add(txtAdditionalNotes);
            panel6.Controls.Add(gpCraft);
            panel6.Controls.Add(label3);
            panel6.Controls.Add(btnSave);
            panel6.Controls.Add(txtAdditional);
            panel6.Controls.Add(btnCancel);
            panel6.Controls.Add(label4);
            panel6.Dock = DockStyle.Bottom;
            panel6.Location = new Point(0, 532);
            panel6.Name = "panel6";
            panel6.Size = new Size(783, 88);
            panel6.TabIndex = 62;
            // 
            // frmAddInvoice
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(783, 620);
            Controls.Add(panel3);
            Controls.Add(lbTitle);
            Controls.Add(panel6);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmAddInvoice";
            Load += frmAddInvoice_Load;
            gpCraft.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gpCraft;
        private RadioButton rdEvaluation;
        private RadioButton rdSale;
        private Label lbTitle;
        private Panel panel3;
        private Panel panel2;
        private LinkLabel lkSelectWorker;
        private Label label1;
        private Panel panel1;
        private LinkLabel lkSelectCustomer;
        private Label label2;
        private Button btnCancel;
        private Button btnSave;
        private LinkLabel lkDeleteWorker;
        private Panel panel4;
        private RichTextBox rtbCustomer;
        private Panel panel5;
        private RichTextBox rtbWorker;
        private SoldProducts.UserControles.ucAddTakeBatch ucAddTakeBatch1;
        private TextBox txtAdditional;
        private Label label4;
        private TextBox txtAdditionalNotes;
        private Label label3;
        private Panel panel6;
    }
}