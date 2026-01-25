namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    partial class ucProductSelector
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
            components = new System.ComponentModel.Container();
            flwpSoldProducts = new FlowLayoutPanel();
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            txtSearchValue1 = new TextBox();
            btnAdd = new Button();
            txtSearchValue2 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            lbTotal = new Label();
            SuspendLayout();
            // 
            // flwpSoldProducts
            // 
            flwpSoldProducts.Anchor = AnchorStyles.Right;
            flwpSoldProducts.BorderStyle = BorderStyle.FixedSingle;
            flwpSoldProducts.Location = new Point(409, 3);
            flwpSoldProducts.Name = "flwpSoldProducts";
            flwpSoldProducts.Size = new Size(984, 351);
            flwpSoldProducts.TabIndex = 0;
            // 
            // ucListView1
            // 
            ucListView1.AllowFilter = false;
            ucListView1.Location = new Point(3, 3);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(400, 385);
            ucListView1.TabIndex = 5;
            // 
            // txtSearchValue1
            // 
            txtSearchValue1.Anchor = AnchorStyles.Right;
            txtSearchValue1.Location = new Point(1021, 360);
            txtSearchValue1.Name = "txtSearchValue1";
            txtSearchValue1.RightToLeft = RightToLeft.Yes;
            txtSearchValue1.Size = new Size(372, 23);
            txtSearchValue1.TabIndex = 2;
            txtSearchValue1.TextChanged += txtSearchValue1_TextChanged;
            txtSearchValue1.KeyDown += txtSearchValue1_KeyDown;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.BackgroundImage = Properties.Resources.addIcon;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Location = new Point(746, 358);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(30, 30);
            btnAdd.TabIndex = 6;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtSearchValue2
            // 
            txtSearchValue2.Anchor = AnchorStyles.Right;
            txtSearchValue2.Location = new Point(782, 360);
            txtSearchValue2.Name = "txtSearchValue2";
            txtSearchValue2.RightToLeft = RightToLeft.Yes;
            txtSearchValue2.Size = new Size(233, 23);
            txtSearchValue2.TabIndex = 3;
            txtSearchValue2.TextChanged += txtSearchValue2_TextChanged;
            txtSearchValue2.KeyDown += txtSearchValue2_KeyDown;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(633, 356);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(107, 30);
            label2.TabIndex = 51;
            label2.Text = "الإجمالي : ";
            // 
            // lbTotal
            // 
            lbTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTotal.Location = new Point(471, 358);
            lbTotal.Name = "lbTotal";
            lbTotal.RightToLeft = RightToLeft.Yes;
            lbTotal.Size = new Size(169, 30);
            lbTotal.TabIndex = 52;
            lbTotal.Text = "0.00";
            // 
            // ucProductSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbTotal);
            Controls.Add(label2);
            Controls.Add(txtSearchValue2);
            Controls.Add(btnAdd);
            Controls.Add(txtSearchValue1);
            Controls.Add(flwpSoldProducts);
            Controls.Add(ucListView1);
            Enabled = false;
            Name = "ucProductSelector";
            Size = new Size(1396, 391);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flwpSoldProducts;
        private InventorySalesManagementSystem.UserControles.UcListView ucListView1;
        private TextBox txtSearchValue1;
        private Button btnAdd;
        private TextBox txtSearchValue2;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private Label lbTotal;
    }
}
