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
            ucListView1 = new InventorySalesManagementSystem.UserControles.UcListView();
            txtSearchValue1 = new TextBox();
            btnAdd = new Button();
            txtSearchValue2 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            lbTotal = new Label();
            pnBottom = new Panel();
            btnClearZeros = new Button();
            panel2 = new Panel();
            flwpSoldProducts = new FlowLayoutPanel();
            pnBottom.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // ucListView1
            // 
            ucListView1.AllowFilter = false;
            ucListView1.Dock = DockStyle.Left;
            ucListView1.Location = new Point(0, 0);
            ucListView1.Name = "ucListView1";
            ucListView1.Size = new Size(403, 391);
            ucListView1.TabIndex = 2;
            // 
            // txtSearchValue1
            // 
            txtSearchValue1.Anchor = AnchorStyles.Right;
            txtSearchValue1.Location = new Point(618, 7);
            txtSearchValue1.Name = "txtSearchValue1";
            txtSearchValue1.RightToLeft = RightToLeft.Yes;
            txtSearchValue1.Size = new Size(372, 23);
            txtSearchValue1.TabIndex = 0;
            txtSearchValue1.TextChanged += txtSearchValue1_TextChanged;
            txtSearchValue1.KeyDown += txtSearchValue1_KeyDown;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.BackgroundImage = Properties.Resources.addIcon;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Location = new Point(343, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(30, 30);
            btnAdd.TabIndex = 4;
            btnAdd.TabStop = false;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtSearchValue2
            // 
            txtSearchValue2.Anchor = AnchorStyles.Right;
            txtSearchValue2.Location = new Point(379, 7);
            txtSearchValue2.Name = "txtSearchValue2";
            txtSearchValue2.RightToLeft = RightToLeft.Yes;
            txtSearchValue2.Size = new Size(233, 23);
            txtSearchValue2.TabIndex = 1;
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
            label2.Location = new Point(221, 3);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(107, 30);
            label2.TabIndex = 51;
            label2.Text = "الإجمالي : ";
            // 
            // lbTotal
            // 
            lbTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbTotal.Location = new Point(59, 5);
            lbTotal.Name = "lbTotal";
            lbTotal.RightToLeft = RightToLeft.Yes;
            lbTotal.Size = new Size(169, 30);
            lbTotal.TabIndex = 52;
            lbTotal.Text = "0.00";
            // 
            // pnBottom
            // 
            pnBottom.Controls.Add(btnClearZeros);
            pnBottom.Controls.Add(lbTotal);
            pnBottom.Controls.Add(label2);
            pnBottom.Controls.Add(txtSearchValue1);
            pnBottom.Controls.Add(txtSearchValue2);
            pnBottom.Controls.Add(btnAdd);
            pnBottom.Dock = DockStyle.Bottom;
            pnBottom.Location = new Point(0, 356);
            pnBottom.Name = "pnBottom";
            pnBottom.Size = new Size(993, 35);
            pnBottom.TabIndex = 53;
            // 
            // btnClearZeros
            // 
            btnClearZeros.Anchor = AnchorStyles.Right;
            btnClearZeros.BackgroundImage = Properties.Resources.clean;
            btnClearZeros.BackgroundImageLayout = ImageLayout.Stretch;
            btnClearZeros.Location = new Point(6, 2);
            btnClearZeros.Name = "btnClearZeros";
            btnClearZeros.Size = new Size(30, 30);
            btnClearZeros.TabIndex = 53;
            btnClearZeros.TabStop = false;
            btnClearZeros.UseVisualStyleBackColor = true;
            btnClearZeros.Click += btnClearZeros_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(flwpSoldProducts);
            panel2.Controls.Add(pnBottom);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(403, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(993, 391);
            panel2.TabIndex = 54;
            // 
            // flwpSoldProducts
            // 
            flwpSoldProducts.AutoScroll = true;
            flwpSoldProducts.BorderStyle = BorderStyle.FixedSingle;
            flwpSoldProducts.Dock = DockStyle.Fill;
            flwpSoldProducts.FlowDirection = FlowDirection.TopDown;
            flwpSoldProducts.Location = new Point(0, 0);
            flwpSoldProducts.Name = "flwpSoldProducts";
            flwpSoldProducts.Size = new Size(993, 356);
            flwpSoldProducts.TabIndex = 0;
            flwpSoldProducts.WrapContents = false;
            // 
            // ucProductSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(ucListView1);
            Enabled = false;
            Name = "ucProductSelector";
            Size = new Size(1396, 391);
            pnBottom.ResumeLayout(false);
            pnBottom.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private InventorySalesManagementSystem.UserControles.UcListView ucListView1;
        private TextBox txtSearchValue1;
        private Button btnAdd;
        private TextBox txtSearchValue2;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private Label lbTotal;
        private Panel pnBottom;
        private Panel panel2;
        private FlowLayoutPanel flwpSoldProducts;
        private Button btnClearZeros;
    }
}
