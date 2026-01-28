namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    partial class ucSoldProductCard
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
            panel1 = new Panel();
            rtbName = new RichTextBox();
            btnRemove = new Button();
            numericUpDown1 = new NumericUpDown();
            panel2 = new Panel();
            rtbQuantity = new RichTextBox();
            panel3 = new Panel();
            rtbPricePerUnit = new RichTextBox();
            panel4 = new Panel();
            rtbTotal = new RichTextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(rtbName);
            panel1.Enabled = false;
            panel1.Location = new Point(545, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(425, 25);
            panel1.TabIndex = 65;
            // 
            // rtbName
            // 
            rtbName.BorderStyle = BorderStyle.None;
            rtbName.Dock = DockStyle.Fill;
            rtbName.Font = new Font("Calibri", 12.25F, FontStyle.Bold);
            rtbName.Location = new Point(0, 0);
            rtbName.Name = "rtbName";
            rtbName.ReadOnly = true;
            rtbName.RightToLeft = RightToLeft.Yes;
            rtbName.Size = new Size(425, 25);
            rtbName.TabIndex = 65;
            rtbName.Text = "----";
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.Left;
            btnRemove.BackgroundImage = Properties.Resources.minus__1_;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
            btnRemove.Location = new Point(1, 3);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(30, 30);
            btnRemove.TabIndex = 67;
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 4;
            numericUpDown1.Font = new Font("Segoe UI", 12F);
            numericUpDown1.Location = new Point(449, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(90, 29);
            numericUpDown1.TabIndex = 68;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(rtbQuantity);
            panel2.Enabled = false;
            panel2.Location = new Point(350, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(93, 25);
            panel2.TabIndex = 66;
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
            rtbQuantity.Size = new Size(93, 25);
            rtbQuantity.TabIndex = 65;
            rtbQuantity.Text = "----";
            // 
            // panel3
            // 
            panel3.Controls.Add(rtbPricePerUnit);
            panel3.Enabled = false;
            panel3.Location = new Point(155, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(178, 25);
            panel3.TabIndex = 69;
            // 
            // rtbPricePerUnit
            // 
            rtbPricePerUnit.BorderStyle = BorderStyle.None;
            rtbPricePerUnit.Dock = DockStyle.Fill;
            rtbPricePerUnit.Font = new Font("Calibri", 15F, FontStyle.Bold);
            rtbPricePerUnit.Location = new Point(0, 0);
            rtbPricePerUnit.Name = "rtbPricePerUnit";
            rtbPricePerUnit.ReadOnly = true;
            rtbPricePerUnit.RightToLeft = RightToLeft.Yes;
            rtbPricePerUnit.Size = new Size(178, 25);
            rtbPricePerUnit.TabIndex = 65;
            rtbPricePerUnit.Text = "----";
            // 
            // panel4
            // 
            panel4.Controls.Add(rtbTotal);
            panel4.Enabled = false;
            panel4.Location = new Point(37, 5);
            panel4.Name = "panel4";
            panel4.Size = new Size(112, 25);
            panel4.TabIndex = 70;
            // 
            // rtbTotal
            // 
            rtbTotal.BorderStyle = BorderStyle.None;
            rtbTotal.Dock = DockStyle.Fill;
            rtbTotal.Font = new Font("Calibri", 15F, FontStyle.Bold);
            rtbTotal.Location = new Point(0, 0);
            rtbTotal.Name = "rtbTotal";
            rtbTotal.ReadOnly = true;
            rtbTotal.RightToLeft = RightToLeft.Yes;
            rtbTotal.Size = new Size(112, 25);
            rtbTotal.TabIndex = 65;
            rtbTotal.Text = "0.00";
            // 
            // ucSoldProductCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(numericUpDown1);
            Controls.Add(btnRemove);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Enabled = false;
            Name = "ucSoldProductCard";
            Size = new Size(973, 35);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtBuyingPrice;
        private Panel panel1;
        private RichTextBox rtbName;
        private Button btnRemove;
        private NumericUpDown numericUpDown1;
        private Panel panel2;
        private RichTextBox rtbQuantity;
        private MaskedTextBox maskedTextBox1;
        private Panel panel3;
        private RichTextBox rtbPricePerUnit;
        private Panel panel4;
        private RichTextBox rtbTotal;
    }
}
