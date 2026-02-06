namespace InventorySalesManagementSystem.Products
{
    partial class ucProductShow
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
            lbID = new Label();
            label9 = new Label();
            label1 = new Label();
            lbSellingPrice = new Label();
            label5 = new Label();
            lbBuyingPrice = new Label();
            label3 = new Label();
            label6 = new Label();
            lbProfit = new Label();
            label4 = new Label();
            lbTotalSales = new Label();
            label11 = new Label();
            panel1 = new Panel();
            rtbName = new RichTextBox();
            panel2 = new Panel();
            rtbQuantity = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            chkAviable = new CheckBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbID
            // 
            lbID.Dock = DockStyle.Fill;
            lbID.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbID.ForeColor = Color.Red;
            lbID.Location = new Point(325, 0);
            lbID.Name = "lbID";
            lbID.RightToLeft = RightToLeft.Yes;
            lbID.Size = new Size(392, 38);
            lbID.TabIndex = 48;
            lbID.Text = "----";
            // 
            // label9
            // 
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(723, 0);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            label9.Size = new Size(119, 38);
            label9.TabIndex = 47;
            label9.Text = "المعرف : ";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(724, 38);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(117, 34);
            label1.TabIndex = 50;
            label1.Text = "الإسم : ";
            // 
            // lbSellingPrice
            // 
            lbSellingPrice.Dock = DockStyle.Fill;
            lbSellingPrice.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbSellingPrice.Location = new Point(26, 38);
            lbSellingPrice.Margin = new Padding(4, 0, 4, 0);
            lbSellingPrice.Name = "lbSellingPrice";
            lbSellingPrice.RightToLeft = RightToLeft.Yes;
            lbSellingPrice.Size = new Size(178, 34);
            lbSellingPrice.TabIndex = 53;
            lbSellingPrice.Text = "----";
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(212, 38);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.Yes;
            label5.Size = new Size(106, 34);
            label5.TabIndex = 52;
            label5.Text = "سعر البيع : ";
            // 
            // lbBuyingPrice
            // 
            lbBuyingPrice.Dock = DockStyle.Fill;
            lbBuyingPrice.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbBuyingPrice.Location = new Point(26, 0);
            lbBuyingPrice.Margin = new Padding(4, 0, 4, 0);
            lbBuyingPrice.Name = "lbBuyingPrice";
            lbBuyingPrice.RightToLeft = RightToLeft.Yes;
            lbBuyingPrice.Size = new Size(178, 38);
            lbBuyingPrice.TabIndex = 55;
            lbBuyingPrice.Text = "----";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(212, 0);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(106, 38);
            label3.TabIndex = 54;
            label3.Text = "سعر الشراء : ";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(212, 100);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.Yes;
            label6.Size = new Size(106, 27);
            label6.TabIndex = 56;
            label6.Text = "الكمية : ";
            // 
            // lbProfit
            // 
            lbProfit.Dock = DockStyle.Fill;
            lbProfit.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbProfit.Location = new Point(26, 72);
            lbProfit.Margin = new Padding(4, 0, 4, 0);
            lbProfit.Name = "lbProfit";
            lbProfit.RightToLeft = RightToLeft.Yes;
            lbProfit.Size = new Size(178, 28);
            lbProfit.TabIndex = 61;
            lbProfit.Text = "----";
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(212, 72);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.Yes;
            label4.Size = new Size(106, 28);
            label4.TabIndex = 60;
            label4.Text = "الربح : ";
            // 
            // lbTotalSales
            // 
            lbTotalSales.Dock = DockStyle.Fill;
            lbTotalSales.Font = new Font("Calibri", 15F, FontStyle.Bold);
            lbTotalSales.Location = new Point(326, 72);
            lbTotalSales.Margin = new Padding(4, 0, 4, 0);
            lbTotalSales.Name = "lbTotalSales";
            lbTotalSales.RightToLeft = RightToLeft.Yes;
            lbTotalSales.Size = new Size(390, 28);
            lbTotalSales.TabIndex = 63;
            lbTotalSales.Text = "----";
            // 
            // label11
            // 
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(724, 72);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.RightToLeft = RightToLeft.Yes;
            label11.Size = new Size(117, 28);
            label11.TabIndex = 62;
            label11.Text = "إجمالي البيع : ";
            // 
            // panel1
            // 
            panel1.Controls.Add(rtbName);
            panel1.Dock = DockStyle.Fill;
            panel1.Enabled = false;
            panel1.Location = new Point(325, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(392, 28);
            panel1.TabIndex = 64;
            // 
            // rtbName
            // 
            rtbName.BorderStyle = BorderStyle.None;
            rtbName.Dock = DockStyle.Fill;
            rtbName.Font = new Font("Calibri", 15F, FontStyle.Bold);
            rtbName.Location = new Point(0, 0);
            rtbName.Name = "rtbName";
            rtbName.ReadOnly = true;
            rtbName.RightToLeft = RightToLeft.Yes;
            rtbName.Size = new Size(392, 28);
            rtbName.TabIndex = 65;
            rtbName.Text = "----";
            // 
            // panel2
            // 
            panel2.Controls.Add(rtbQuantity);
            panel2.Dock = DockStyle.Fill;
            panel2.Enabled = false;
            panel2.Location = new Point(25, 103);
            panel2.Name = "panel2";
            panel2.Size = new Size(180, 21);
            panel2.TabIndex = 65;
            // 
            // rtbQuantity
            // 
            rtbQuantity.BorderStyle = BorderStyle.None;
            rtbQuantity.Dock = DockStyle.Fill;
            rtbQuantity.Font = new Font("Calibri", 15F, FontStyle.Bold);
            rtbQuantity.Location = new Point(0, 0);
            rtbQuantity.Name = "rtbQuantity";
            rtbQuantity.ReadOnly = true;
            rtbQuantity.RightToLeft = RightToLeft.Yes;
            rtbQuantity.Size = new Size(180, 21);
            rtbQuantity.TabIndex = 65;
            rtbQuantity.Text = "----";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.810811F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89.1891861F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 398F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 124F));
            tableLayoutPanel1.Controls.Add(label9, 4, 0);
            tableLayoutPanel1.Controls.Add(label1, 4, 1);
            tableLayoutPanel1.Controls.Add(label11, 4, 2);
            tableLayoutPanel1.Controls.Add(lbID, 3, 0);
            tableLayoutPanel1.Controls.Add(panel1, 3, 1);
            tableLayoutPanel1.Controls.Add(lbTotalSales, 3, 2);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label5, 2, 1);
            tableLayoutPanel1.Controls.Add(label4, 2, 2);
            tableLayoutPanel1.Controls.Add(label6, 2, 3);
            tableLayoutPanel1.Controls.Add(lbBuyingPrice, 1, 0);
            tableLayoutPanel1.Controls.Add(lbSellingPrice, 1, 1);
            tableLayoutPanel1.Controls.Add(lbProfit, 1, 2);
            tableLayoutPanel1.Controls.Add(panel2, 1, 3);
            tableLayoutPanel1.Controls.Add(chkAviable, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 53.125F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 46.875F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel1.Size = new Size(845, 127);
            tableLayoutPanel1.TabIndex = 66;
            // 
            // chkAviable
            // 
            chkAviable.AutoSize = true;
            chkAviable.Dock = DockStyle.Fill;
            chkAviable.Location = new Point(3, 103);
            chkAviable.Name = "chkAviable";
            chkAviable.Size = new Size(16, 21);
            chkAviable.TabIndex = 66;
            chkAviable.UseVisualStyleBackColor = true;
            // 
            // ucProductShow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Enabled = false;
            Name = "ucProductShow";
            Size = new Size(845, 127);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lbID;
        private Label label9;
        private Label label1;
        private Label lbSellingPrice;
        private Label label5;
        private Label lbBuyingPrice;
        private Label label3;
        private Label label6;
        private Label lbProfit;
        private Label label4;
        private Label lbTotalSales;
        private Label label11;
        private Panel panel1;
        private RichTextBox rtbName;
        private Panel panel2;
        private RichTextBox rtbQuantity;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox chkAviable;
    }
}
