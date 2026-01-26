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
            gpCraft.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // gpCraft
            // 
            gpCraft.Controls.Add(rdEvaluation);
            gpCraft.Controls.Add(rdSale);
            gpCraft.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gpCraft.Location = new Point(1171, 606);
            gpCraft.Name = "gpCraft";
            gpCraft.RightToLeft = RightToLeft.Yes;
            gpCraft.Size = new Size(148, 72);
            gpCraft.TabIndex = 2;
            gpCraft.TabStop = false;
            gpCraft.Text = "نوع الفاتورة";
            // 
            // rdEvaluation
            // 
            rdEvaluation.AutoSize = true;
            rdEvaluation.Location = new Point(6, 34);
            rdEvaluation.Name = "rdEvaluation";
            rdEvaluation.Size = new Size(77, 29);
            rdEvaluation.TabIndex = 1;
            rdEvaluation.TabStop = true;
            rdEvaluation.Text = "تسعير";
            rdEvaluation.UseVisualStyleBackColor = true;
            // 
            // rdSale
            // 
            rdSale.AutoSize = true;
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
            lbTitle.Font = new Font("Calibri", 30F, FontStyle.Bold);
            lbTitle.Location = new Point(598, 9);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(241, 49);
            lbTitle.TabIndex = 49;
            lbTitle.Text = "إضافة فاتورة";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(ucAddTakeBatch1);
            panel3.Location = new Point(12, 69);
            panel3.Name = "panel3";
            panel3.Size = new Size(1413, 537);
            panel3.TabIndex = 55;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(lkSelectCustomer);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(17, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(742, 36);
            panel1.TabIndex = 53;
            // 
            // panel4
            // 
            panel4.Controls.Add(rtbCustomer);
            panel4.Enabled = false;
            panel4.Location = new Point(32, 8);
            panel4.Name = "panel4";
            panel4.Size = new Size(622, 23);
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
            rtbCustomer.Size = new Size(622, 23);
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
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(647, 3);
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
            panel2.Size = new Size(742, 36);
            panel2.TabIndex = 54;
            // 
            // panel5
            // 
            panel5.Controls.Add(rtbWorker);
            panel5.Enabled = false;
            panel5.Location = new Point(84, 7);
            panel5.Name = "panel5";
            panel5.Size = new Size(569, 23);
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
            rtbWorker.Size = new Size(569, 23);
            rtbWorker.TabIndex = 65;
            rtbWorker.Text = "----";
            // 
            // lkDeleteWorker
            // 
            lkDeleteWorker.ActiveLinkColor = Color.Blue;
            lkDeleteWorker.AutoSize = true;
            lkDeleteWorker.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lkDeleteWorker.LinkColor = Color.Red;
            lkDeleteWorker.Location = new Point(14, 12);
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
            lkSelectWorker.Location = new Point(53, 13);
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
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(647, 2);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(91, 30);
            label1.TabIndex = 50;
            label1.Text = "العامل : ";
            // 
            // ucAddTakeBatch1
            // 
            ucAddTakeBatch1.Enabled = false;
            ucAddTakeBatch1.Location = new Point(3, 3);
            ucAddTakeBatch1.Name = "ucAddTakeBatch1";
            ucAddTakeBatch1.ShowInvoiceDetails = false;
            ucAddTakeBatch1.Size = new Size(1406, 520);
            ucAddTakeBatch1.TabIndex = 55;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(1325, 648);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 57;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(1325, 612);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 56;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // frmAddInvoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1431, 689);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lbTitle);
            Controls.Add(gpCraft);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmAddInvoice";
            Load += frmAddInvoice_Load;
            gpCraft.ResumeLayout(false);
            gpCraft.PerformLayout();
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel5.ResumeLayout(false);
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
    }
}