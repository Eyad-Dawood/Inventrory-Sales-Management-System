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
            ucProductSelector1 = new ucProductSelector();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtReciver
            // 
            txtReciver.Anchor = AnchorStyles.Right;
            txtReciver.Location = new Point(282, 10);
            txtReciver.Name = "txtReciver";
            txtReciver.RightToLeft = RightToLeft.Yes;
            txtReciver.Size = new Size(278, 23);
            txtReciver.TabIndex = 1;
            // 
            // lbTakeNameLable
            // 
            lbTakeNameLable.Anchor = AnchorStyles.Right;
            lbTakeNameLable.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTakeNameLable.Location = new Point(566, 7);
            lbTakeNameLable.Name = "lbTakeNameLable";
            lbTakeNameLable.RightToLeft = RightToLeft.Yes;
            lbTakeNameLable.Size = new Size(102, 30);
            lbTakeNameLable.TabIndex = 39;
            lbTakeNameLable.Text = "المستلم : ";
            // 
            // txtNote
            // 
            txtNote.Anchor = AnchorStyles.Right;
            txtNote.Location = new Point(282, 58);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.RightToLeft = RightToLeft.Yes;
            txtNote.Size = new Size(285, 59);
            txtNote.TabIndex = 2;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(563, 58);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(104, 30);
            label3.TabIndex = 45;
            label3.Text = "ملحوظة : ";
            // 
            // ucProductSelector1
            // 
            ucProductSelector1.Dock = DockStyle.Fill;
            ucProductSelector1.Enabled = false;
            ucProductSelector1.Location = new Point(0, 123);
            ucProductSelector1.Name = "ucProductSelector1";
            ucProductSelector1.Size = new Size(670, 397);
            ucProductSelector1.TabIndex = 47;
            // 
            // panel1
            // 
            panel1.Controls.Add(lbTakeNameLable);
            panel1.Controls.Add(txtNote);
            panel1.Controls.Add(txtReciver);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(670, 123);
            panel1.TabIndex = 48;
            // 
            // ucAddTakeBatch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucProductSelector1);
            Controls.Add(panel1);
            Enabled = false;
            Name = "ucAddTakeBatch";
            Size = new Size(670, 520);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtReciver;
        private Label lbTakeNameLable;
        private TextBox txtNote;
        private Label label3;
        private ucProductSelector ucProductSelector1;
        private Panel panel1;
    }
}
