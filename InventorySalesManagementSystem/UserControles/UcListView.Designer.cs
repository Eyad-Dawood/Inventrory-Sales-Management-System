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
            lbrowsCount = new Label();
            lbPageNumber = new Label();
            btnPrevious = new Button();
            btnNext = new Button();
            pnUpperBar = new Panel();
            btnCancelFilter = new Button();
            label1 = new Label();
            cmpSearchBy = new ComboBox();
            btnFilter = new Button();
            txtSearchValue = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            pnBottomBar.SuspendLayout();
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
            dgvData.Size = new Size(980, 402);
            dgvData.TabIndex = 0;
            dgvData.CellMouseClick += dgvData_CellMouseClick;
            // 
            // pnBottomBar
            // 
            pnBottomBar.Controls.Add(lbrowsCount);
            pnBottomBar.Controls.Add(lbPageNumber);
            pnBottomBar.Controls.Add(btnPrevious);
            pnBottomBar.Controls.Add(btnNext);
            pnBottomBar.Dock = DockStyle.Bottom;
            pnBottomBar.Location = new Point(0, 437);
            pnBottomBar.Name = "pnBottomBar";
            pnBottomBar.Size = new Size(980, 33);
            pnBottomBar.TabIndex = 1;
            // 
            // lbrowsCount
            // 
            lbrowsCount.Anchor = AnchorStyles.Left;
            lbrowsCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbrowsCount.Location = new Point(1, 6);
            lbrowsCount.Name = "lbrowsCount";
            lbrowsCount.RightToLeft = RightToLeft.Yes;
            lbrowsCount.Size = new Size(126, 23);
            lbrowsCount.TabIndex = 4;
            lbrowsCount.Text = "00 عنصر";
            lbrowsCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbPageNumber
            // 
            lbPageNumber.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbPageNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPageNumber.Location = new Point(370, 4);
            lbPageNumber.Name = "lbPageNumber";
            lbPageNumber.Size = new Size(241, 23);
            lbPageNumber.TabIndex = 2;
            lbPageNumber.Text = "1/??";
            lbPageNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrevious.BackgroundImage = Properties.Resources.right_arrow__1_1;
            btnPrevious.BackgroundImageLayout = ImageLayout.Stretch;
            btnPrevious.Location = new Point(903, 4);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(34, 26);
            btnPrevious.TabIndex = 1;
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.BackgroundImage = Properties.Resources.right_arrow__1_;
            btnNext.BackgroundImageLayout = ImageLayout.Stretch;
            btnNext.Location = new Point(943, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(34, 26);
            btnNext.TabIndex = 0;
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
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
            pnUpperBar.ResumeLayout(false);
            pnUpperBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private Panel pnBottomBar;
        private Button btnPrevious;
        private Button btnNext;
        private Label lbPageNumber;
        private Label lbrowsCount;
        private Panel pnUpperBar;
        private Button btnFilter;
        private TextBox txtSearchValue;
        private Label label1;
        private ComboBox cmpSearchBy;
        private Button btnCancelFilter;
    }
}
