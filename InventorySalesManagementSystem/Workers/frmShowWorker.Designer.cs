namespace InventorySalesManagementSystem.Workers
{
    partial class frmShowWorker
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
            ucWorkerShow1 = new ucWorkerShow();
            SuspendLayout();
            // 
            // ucWorkerShow1
            // 
            ucWorkerShow1.Enabled = false;
            ucWorkerShow1.Location = new Point(8, 7);
            ucWorkerShow1.Name = "ucWorkerShow1";
            ucWorkerShow1.Size = new Size(748, 116);
            ucWorkerShow1.TabIndex = 0;
            // 
            // frmShowWorke
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 134);
            Controls.Add(ucWorkerShow1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "frmShowWorke";
            ResumeLayout(false);
        }

        #endregion

        private ucWorkerShow ucWorkerShow1;
    }
}