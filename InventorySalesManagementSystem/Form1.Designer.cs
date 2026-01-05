namespace InventorySalesManagementSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            ucListView1 = new InventorySalesManagementSystem.Customers.UcListView();
            ucAddUpdatePerson1 = new InventorySalesManagementSystem.People.UcAddUpdatePerson();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(48, 533);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ucListView1
            // 
            ucListView1.Location = new Point(32, 55);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(935, 422);
            ucListView1.TabIndex = 2;
            ucListView1.Load += ucListView1_Load;
            // 
            // ucAddUpdatePerson1
            // 
            ucAddUpdatePerson1.Location = new Point(379, 493);
            ucAddUpdatePerson1.Name = "ucAddUpdatePerson1";
            ucAddUpdatePerson1.Size = new Size(632, 208);
            ucAddUpdatePerson1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(298, 607);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 735);
            Controls.Add(button2);
            Controls.Add(ucAddUpdatePerson1);
            Controls.Add(button1);
            Controls.Add(ucListView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Customers.UcListView ucListView1;
        private People.UcAddUpdatePerson ucAddUpdatePerson1;
        private Button button2;
    }
}
