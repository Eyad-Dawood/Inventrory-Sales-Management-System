namespace InventorySalesManagementSystem.General.General_Forms
{
    partial class frmBaseListScreen
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
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            panel4 = new Panel();
            btnSelect = new Button();
            btnAdd = new Button();
            panel2 = new Panel();
            lbTitle = new Label();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.BackColor = SystemColors.Control;
            ucListView1.Dock = DockStyle.Fill;
            ucListView1.Location = new Point(0, 135);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(1214, 526);
            ucListView1.TabIndex = 24;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnSelect);
            panel4.Controls.Add(btnAdd);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 95);
            panel4.Name = "panel4";
            panel4.Size = new Size(1214, 40);
            panel4.TabIndex = 26;
            // 
            // btnSelect
            // 
            btnSelect.BackgroundImage = Properties.Resources.CheckIcon;
            btnSelect.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect.Dock = DockStyle.Right;
            btnSelect.Location = new Point(1134, 0);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(40, 40);
            btnSelect.TabIndex = 4;
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += BtnSelect_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackgroundImage = Properties.Resources.addIcon;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Dock = DockStyle.Right;
            btnAdd.Location = new Point(1174, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 2;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(lbTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1214, 95);
            panel2.TabIndex = 25;
            // 
            // lbTitle
            // 
            lbTitle.Dock = DockStyle.Fill;
            lbTitle.Font = new Font("Calibri", 60F, FontStyle.Bold);
            lbTitle.Location = new Point(0, 0);
            lbTitle.Margin = new Padding(4, 0, 4, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.RightToLeft = RightToLeft.Yes;
            lbTitle.Size = new Size(1214, 95);
            lbTitle.TabIndex = 19;
            lbTitle.Text = "العنوان";
            lbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmBaseListScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1214, 661);
            Controls.Add(ucListView1);
            Controls.Add(panel4);
            Controls.Add(panel2);
            MinimumSize = new Size(450, 350);
            Name = "frmBaseListScreen";
            Load += frmBaseListScreen_Load;
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }





        #endregion

        protected UserControles.UcListView ucListView1;
        private Panel panel4;
        private Panel panel2;
        protected Button btnSelect;
        protected Label lbTitle;
        protected Button btnAdd;
    }
}