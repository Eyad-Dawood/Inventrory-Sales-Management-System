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
            components = new System.ComponentModel.Container();
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
            tlpUpper = new TableLayoutPanel();
            btnSearch = new Button();
            dtpLogDate = new DateTimePicker();
            chkUseDateFilter = new CheckBox();
            btnCancelFilter = new Button();
            txtSearchValue1 = new TextBox();
            cmpSearchBy = new ComboBox();
            txtSearchValue2 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            pnBottomBar.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            pnUpperBar.SuspendLayout();
            tlpUpper.SuspendLayout();
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
            dgvData.Location = new Point(0, 39);
            dgvData.MultiSelect = false;
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RightToLeft = RightToLeft.Yes;
            dgvData.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvData.RowHeadersVisible = false;
            dgvData.RowHeadersWidth = 15;
            dgvData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvData.RowTemplate.Height = 20;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ShowCellErrors = false;
            dgvData.ShowEditingIcon = false;
            dgvData.ShowRowErrors = false;
            dgvData.Size = new Size(1049, 397);
            dgvData.TabIndex = 1;
            dgvData.CellMouseDown += dgvData_CellMouseDoun;
            dgvData.CellToolTipTextNeeded += dgvData_CellToolTipTextNeeded;
            // 
            // pnBottomBar
            // 
            pnBottomBar.Controls.Add(tableLayoutPanel1);
            pnBottomBar.Dock = DockStyle.Bottom;
            pnBottomBar.Location = new Point(0, 436);
            pnBottomBar.Name = "pnBottomBar";
            pnBottomBar.Size = new Size(1049, 34);
            pnBottomBar.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 2, 0);
            tableLayoutPanel1.Controls.Add(lbPageNumber, 1, 0);
            tableLayoutPanel1.Controls.Add(lbrowsCount, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1049, 34);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnNext);
            flowLayoutPanel1.Controls.Add(btnPrevious);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(701, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(345, 28);
            flowLayoutPanel1.TabIndex = 16;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.BackgroundImage = Properties.Resources.RightArrow;
            btnNext.BackgroundImageLayout = ImageLayout.Stretch;
            btnNext.Location = new Point(312, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(30, 27);
            btnNext.TabIndex = 4;
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrevious.BackgroundImage = Properties.Resources.LeftArrow;
            btnPrevious.BackgroundImageLayout = ImageLayout.Stretch;
            btnPrevious.Location = new Point(276, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Padding = new Padding(0, 0, 0, 5);
            btnPrevious.Size = new Size(30, 27);
            btnPrevious.TabIndex = 5;
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // lbPageNumber
            // 
            lbPageNumber.Dock = DockStyle.Fill;
            lbPageNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPageNumber.Location = new Point(352, 0);
            lbPageNumber.Name = "lbPageNumber";
            lbPageNumber.Size = new Size(343, 34);
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
            pnUpperBar.Controls.Add(tlpUpper);
            pnUpperBar.Dock = DockStyle.Top;
            pnUpperBar.Location = new Point(0, 0);
            pnUpperBar.Name = "pnUpperBar";
            pnUpperBar.Size = new Size(1049, 39);
            pnUpperBar.TabIndex = 2;
            // 
            // tlpUpper
            // 
            tlpUpper.AutoSize = true;
            tlpUpper.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpUpper.ColumnCount = 7;
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 221F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 219F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 225F));
            tlpUpper.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlpUpper.Controls.Add(btnSearch, 0, 0);
            tlpUpper.Controls.Add(dtpLogDate, 3, 0);
            tlpUpper.Controls.Add(chkUseDateFilter, 2, 0);
            tlpUpper.Controls.Add(btnCancelFilter, 1, 0);
            tlpUpper.Controls.Add(txtSearchValue1, 5, 0);
            tlpUpper.Controls.Add(cmpSearchBy, 6, 0);
            tlpUpper.Controls.Add(txtSearchValue2, 4, 0);
            tlpUpper.Dock = DockStyle.Right;
            tlpUpper.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpUpper.Location = new Point(92, 0);
            tlpUpper.Name = "tlpUpper";
            tlpUpper.RowCount = 1;
            tlpUpper.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpUpper.Size = new Size(957, 39);
            tlpUpper.TabIndex = 11;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Right;
            btnSearch.BackgroundImage = Properties.Resources.SearchIcon;
            btnSearch.BackgroundImageLayout = ImageLayout.Stretch;
            btnSearch.Location = new Point(3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(30, 30);
            btnSearch.TabIndex = 11;
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpLogDate
            // 
            dtpLogDate.Anchor = AnchorStyles.Right;
            dtpLogDate.Format = DateTimePickerFormat.Short;
            dtpLogDate.Location = new Point(98, 8);
            dtpLogDate.Name = "dtpLogDate";
            dtpLogDate.RightToLeft = RightToLeft.Yes;
            dtpLogDate.Size = new Size(212, 23);
            dtpLogDate.TabIndex = 9;
            dtpLogDate.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dtpLogDate.Visible = false;
            dtpLogDate.ValueChanged += dtpLogDate_ValueChanged;
            // 
            // chkUseDateFilter
            // 
            chkUseDateFilter.Dock = DockStyle.Fill;
            chkUseDateFilter.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            chkUseDateFilter.Location = new Point(75, 3);
            chkUseDateFilter.Name = "chkUseDateFilter";
            chkUseDateFilter.Size = new Size(14, 33);
            chkUseDateFilter.TabIndex = 10;
            chkUseDateFilter.UseVisualStyleBackColor = true;
            chkUseDateFilter.Visible = false;
            // 
            // btnCancelFilter
            // 
            btnCancelFilter.Anchor = AnchorStyles.Right;
            btnCancelFilter.BackgroundImage = Properties.Resources.CancelIcon;
            btnCancelFilter.BackgroundImageLayout = ImageLayout.Stretch;
            btnCancelFilter.Location = new Point(39, 4);
            btnCancelFilter.Name = "btnCancelFilter";
            btnCancelFilter.Size = new Size(30, 30);
            btnCancelFilter.TabIndex = 6;
            btnCancelFilter.UseVisualStyleBackColor = true;
            btnCancelFilter.Click += btnCancelFilter_Click;
            // 
            // txtSearchValue1
            // 
            txtSearchValue1.Anchor = AnchorStyles.Right;
            txtSearchValue1.Location = new Point(540, 8);
            txtSearchValue1.Name = "txtSearchValue1";
            txtSearchValue1.RightToLeft = RightToLeft.Yes;
            txtSearchValue1.Size = new Size(214, 23);
            txtSearchValue1.TabIndex = 2;
            txtSearchValue1.TextChanged += txtSearchValue1_TextChanged;
            // 
            // cmpSearchBy
            // 
            cmpSearchBy.Anchor = AnchorStyles.Right;
            cmpSearchBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmpSearchBy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cmpSearchBy.FormattingEnabled = true;
            cmpSearchBy.Location = new Point(765, 5);
            cmpSearchBy.Name = "cmpSearchBy";
            cmpSearchBy.Size = new Size(189, 28);
            cmpSearchBy.TabIndex = 1;
            // 
            // txtSearchValue2
            // 
            txtSearchValue2.Anchor = AnchorStyles.Right;
            txtSearchValue2.Location = new Point(316, 8);
            txtSearchValue2.Name = "txtSearchValue2";
            txtSearchValue2.RightToLeft = RightToLeft.Yes;
            txtSearchValue2.Size = new Size(213, 23);
            txtSearchValue2.TabIndex = 1;
            txtSearchValue2.Visible = false;
            txtSearchValue2.TextChanged += txtSearchValue2_TextChanged;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // UcListView
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(dgvData);
            Controls.Add(pnUpperBar);
            Controls.Add(pnBottomBar);
            Name = "UcListView";
            Size = new Size(1049, 470);
            Load += UcListView_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            pnBottomBar.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            pnUpperBar.ResumeLayout(false);
            pnUpperBar.PerformLayout();
            tlpUpper.ResumeLayout(false);
            tlpUpper.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private Panel pnBottomBar;
        private Panel pnUpperBar;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbPageNumber;
        private Label lbrowsCount;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnNext;
        private Button btnPrevious;
        private TableLayoutPanel tlpUpper;
        private Button btnCancelFilter;
        private ComboBox cmpSearchBy;
        private TextBox txtSearchValue1;
        private CheckBox chkUseDateFilter;
        private TextBox txtSearchValue2;
        private DateTimePicker dtpLogDate;
        private System.Windows.Forms.Timer timer1;
        private Button btnSearch;
    }
}
