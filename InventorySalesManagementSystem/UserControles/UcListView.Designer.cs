namespace InventorySalesManagementSystem.UserControles
{
    partial class UcListView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgvData = new DataGridView();
            pnBottomBar = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnNext = new Button();
            btnPrevious = new Button();
            lbPageNumber = new Label();
            lbrowsCount = new Label();
            pnUpperBar = new Panel();
            btnCancelFilter = new Button();
            label1 = new Label();
            cmpSearchBy = new ComboBox();
            btnFilter = new Button();
            txtSearchValue = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            pnBottomBar.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            pnUpperBar.SuspendLayout();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.ColumnHeadersHeight = 25;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 35);
            dgvData.MultiSelect = false;
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RightToLeft = RightToLeft.Yes;
            dgvData.RowHeadersVisible = false;
            dgvData.RowHeadersWidth = 15;
            dgvData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvData.RowTemplate.Height = 20;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ShowCellErrors = false;
            dgvData.ShowCellToolTips = false;
            dgvData.ShowEditingIcon = false;
            dgvData.ShowRowErrors = false;
            dgvData.Size = new Size(980, 401);
            dgvData.TabIndex = 0;
            dgvData.CellMouseClick += dgvData_CellMouseClick;
            // 
            // pnBottomBar
            // 
            pnBottomBar.Controls.Add(tableLayoutPanel1);
            pnBottomBar.Dock = DockStyle.Bottom;
            pnBottomBar.Location = new Point(0, 436);
            pnBottomBar.Name = "pnBottomBar";
            pnBottomBar.Size = new Size(980, 34);
            pnBottomBar.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 2, 0);
            tableLayoutPanel1.Controls.Add(lbPageNumber, 1, 0);
            tableLayoutPanel1.Controls.Add(lbrowsCount, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(980, 34);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnNext);
            flowLayoutPanel1.Controls.Add(btnPrevious);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(655, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(322, 28);
            flowLayoutPanel1.TabIndex = 16;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.BackgroundImage = Properties.Resources.right_arrow__1_;
            btnNext.BackgroundImageLayout = ImageLayout.Stretch;
            btnNext.Location = new Point(289, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(30, 27);
            btnNext.TabIndex = 14;
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrevious.BackgroundImage = Properties.Resources.right_arrow__1_1;
            btnPrevious.BackgroundImageLayout = ImageLayout.Stretch;
            btnPrevious.Location = new Point(253, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Padding = new Padding(0, 0, 0, 5);
            btnPrevious.Size = new Size(30, 27);
            btnPrevious.TabIndex = 16;
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // lbPageNumber
            // 
            lbPageNumber.Dock = DockStyle.Fill;
            lbPageNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPageNumber.Location = new Point(330, 0);
            lbPageNumber.Name = "lbPageNumber";
            lbPageNumber.Size = new Size(319, 34);
            lbPageNumber.TabIndex = 6;
            lbPageNumber.Text = "1/??";
            lbPageNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbrowsCount
            // 
            lbrowsCount.Anchor = AnchorStyles.Left;
            lbrowsCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbrowsCount.Location = new Point(3, 5);
            lbrowsCount.Name = "lbrowsCount";
            lbrowsCount.RightToLeft = RightToLeft.Yes;
            lbrowsCount.Size = new Size(111, 23);
            lbrowsCount.TabIndex = 5;
            lbrowsCount.Text = "00 عنصر";
            lbrowsCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnUpperBar
            // 
            pnUpperBar.Controls.Add(btnCancelFilter);
            pnUpperBar.Controls.Add(label1);
            pnUpperBar.Controls.Add(cmpSearchBy);
            pnUpperBar.Controls.Add(btnFilter);
            pnUpperBar.Controls.Add(txtSearchValue);
            pnUpperBar.Dock = DockStyle.Top;
            pnUpperBar.Location = new Point(0, 0);
            pnUpperBar.Name = "pnUpperBar";
            pnUpperBar.Size = new Size(980, 35);
            pnUpperBar.TabIndex = 2;
            // 
            // btnCancelFilter
            // 
            btnCancelFilter.Anchor = AnchorStyles.Right;
            btnCancelFilter.BackgroundImage = Properties.Resources.cancel__1_;
            btnCancelFilter.BackgroundImageLayout = ImageLayout.Stretch;
            btnCancelFilter.Enabled = false;
            btnCancelFilter.Location = new Point(382, 2);
            btnCancelFilter.Name = "btnCancelFilter";
            btnCancelFilter.Size = new Size(30, 30);
            btnCancelFilter.TabIndex = 6;
            btnCancelFilter.UseVisualStyleBackColor = true;
            btnCancelFilter.Click += btnCancelFilter_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(903, 7);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(72, 25);
            label1.TabIndex = 5;
            label1.Text = "البحث : ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmpSearchBy
            // 
            cmpSearchBy.Anchor = AnchorStyles.Right;
            cmpSearchBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmpSearchBy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cmpSearchBy.FormattingEnabled = true;
            cmpSearchBy.Location = new Point(708, 4);
            cmpSearchBy.Name = "cmpSearchBy";
            cmpSearchBy.Size = new Size(189, 28);
            cmpSearchBy.TabIndex = 2;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Right;
            btnFilter.BackgroundImage = Properties.Resources.search__1_;
            btnFilter.BackgroundImageLayout = ImageLayout.Stretch;
            btnFilter.Location = new Point(418, 3);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(30, 30);
            btnFilter.TabIndex = 1;
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // txtSearchValue
            // 
            txtSearchValue.Anchor = AnchorStyles.Right;
            txtSearchValue.Location = new Point(454, 7);
            txtSearchValue.Name = "txtSearchValue";
            txtSearchValue.Size = new Size(248, 23);
            txtSearchValue.TabIndex = 0;
            // 
            // UcListView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvData);
            Controls.Add(pnUpperBar);
            Controls.Add(pnBottomBar);
            Name = "UcListView";
            Size = new Size(980, 470);
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            pnBottomBar.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            pnUpperBar.ResumeLayout(false);
            pnUpperBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private Panel pnBottomBar;
        private Panel pnUpperBar;
        private Button btnFilter;
        private TextBox txtSearchValue;
        private Label label1;
        private ComboBox cmpSearchBy;
        private Button btnCancelFilter;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbPageNumber;
        private Label lbrowsCount;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnNext;
        private Button btnPrevious;
    }
}
