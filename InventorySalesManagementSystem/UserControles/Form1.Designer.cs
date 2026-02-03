namespace InventorySalesManagementSystem.UserControles
{
    partial class Form1
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
            pnSearchBoxes = new Panel();
            pnUpperButtons = new Panel();
            SuspendLayout();
            // 
            // pnSearchBoxes
            // 
            pnSearchBoxes.Location = new Point(23, 90);
            pnSearchBoxes.Name = "pnSearchBoxes";
            pnSearchBoxes.Size = new Size(1018, 72);
            pnSearchBoxes.TabIndex = 10;
            // 
            // pnUpperButtons
            // 
            pnUpperButtons.Location = new Point(807, 235);
            pnUpperButtons.Name = "pnUpperButtons";
            pnUpperButtons.Size = new Size(383, 175);
            pnUpperButtons.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1347, 546);
            Controls.Add(pnUpperButtons);
            Controls.Add(pnSearchBoxes);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Panel pnSearchBoxes;
        private Panel pnUpperButtons;
    }
}