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
            Controls.Add(txtNote);
            Controls.Add(label3);
            Controls.Add(txtReciver);
            Controls.Add(lbTakeNameLable);
            Enabled = false;
            Name = "ucAddTakeBatch";
            Size = new Size(1402, 520);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtReciver;
        private Label lbTakeNameLable;
        private TextBox txtNote;
        private Label label3;
        private ucProductSelector ucProductSelector1;
    }
}
