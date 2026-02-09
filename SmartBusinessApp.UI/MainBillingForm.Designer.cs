namespace SmartBusinessApp.UI
{
    partial class MainBillingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBillingForm));
            cmbBranch = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            cmbCustomer = new ComboBox();
            dtpBillDate = new DateTimePicker();
            label4 = new Label();
            dgvBillItems = new DataGridView();
            colSNo = new DataGridViewTextBoxColumn();
            colProduct = new DataGridViewComboBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colRate = new DataGridViewTextBoxColumn();
            colGross = new DataGridViewTextBoxColumn();
            colTaxPercent = new DataGridViewTextBoxColumn();
            colTaxAmount = new DataGridViewTextBoxColumn();
            colNetAmount = new DataGridViewTextBoxColumn();
            btnSave = new Button();
            label3 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvBillItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // cmbBranch
            // 
            cmbBranch.Font = new Font("Times New Roman", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmbBranch.FormattingEnabled = true;
            cmbBranch.Location = new Point(203, 80);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new Size(268, 33);
            cmbBranch.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(76, 83);
            label1.Name = "label1";
            label1.Size = new Size(93, 30);
            label1.TabIndex = 1;
            label1.Text = "Branch";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(160, 392);
            label2.Name = "label2";
            label2.Size = new Size(0, 25);
            label2.TabIndex = 2;
            label2.TextChanged += txtGross;
            // 
            // cmbCustomer
            // 
            cmbCustomer.Font = new Font("Times New Roman", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(203, 128);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(268, 33);
            cmbCustomer.TabIndex = 4;
            // 
            // dtpBillDate
            // 
            dtpBillDate.Font = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            dtpBillDate.Location = new Point(203, 32);
            dtpBillDate.Name = "dtpBillDate";
            dtpBillDate.Size = new Size(268, 33);
            dtpBillDate.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ControlLightLight;
            label4.Font = new Font("Times New Roman", 13F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(83, 32);
            label4.Name = "label4";
            label4.Size = new Size(64, 30);
            label4.TabIndex = 9;
            label4.Text = "Date";
            label4.Click += label4_Click;
            // 
            // dgvBillItems
            // 
            dgvBillItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBillItems.BackgroundColor = SystemColors.InactiveBorder;
            dgvBillItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBillItems.Columns.AddRange(new DataGridViewColumn[] { colSNo, colProduct, colQty, colRate, colGross, colTaxPercent, colTaxAmount, colNetAmount });
            dgvBillItems.GridColor = SystemColors.ControlLight;
            dgvBillItems.Location = new Point(76, 206);
            dgvBillItems.Name = "dgvBillItems";
            dgvBillItems.RowHeadersVisible = false;
            dgvBillItems.RowHeadersWidth = 62;
            dgvBillItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBillItems.Size = new Size(1435, 393);
            dgvBillItems.TabIndex = 10;
            // 
            // colSNo
            // 
            colSNo.HeaderText = "S.No";
            colSNo.MinimumWidth = 8;
            colSNo.Name = "colSNo";
            colSNo.ReadOnly = true;
            // 
            // colProduct
            // 
            colProduct.HeaderText = "Product";
            colProduct.MinimumWidth = 8;
            colProduct.Name = "colProduct";
            // 
            // colQty
            // 
            colQty.HeaderText = "Qty";
            colQty.MinimumWidth = 8;
            colQty.Name = "colQty";
            // 
            // colRate
            // 
            colRate.HeaderText = "Rate";
            colRate.MinimumWidth = 8;
            colRate.Name = "colRate";
            colRate.ReadOnly = true;
            // 
            // colGross
            // 
            colGross.HeaderText = "Gross";
            colGross.MinimumWidth = 8;
            colGross.Name = "colGross";
            colGross.ReadOnly = true;
            // 
            // colTaxPercent
            // 
            colTaxPercent.HeaderText = "Tax%";
            colTaxPercent.MinimumWidth = 8;
            colTaxPercent.Name = "colTaxPercent";
            colTaxPercent.ReadOnly = true;
            // 
            // colTaxAmount
            // 
            colTaxAmount.HeaderText = "Tax";
            colTaxAmount.MinimumWidth = 8;
            colTaxAmount.Name = "colTaxAmount";
            colTaxAmount.ReadOnly = true;
            // 
            // colNetAmount
            // 
            colNetAmount.HeaderText = "Net";
            colNetAmount.MinimumWidth = 8;
            colNetAmount.Name = "colNetAmount";
            colNetAmount.ReadOnly = true;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(1399, 131);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 48);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 13F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(76, 131);
            label3.Name = "label3";
            label3.Size = new Size(71, 30);
            label3.TabIndex = 12;
            label3.Text = "Party";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.Location = new Point(1694, 32);
            button1.Name = "button1";
            button1.Size = new Size(42, 42);
            button1.TabIndex = 13;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(2, -4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1878, 774);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // MainBillingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 678);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(btnSave);
            Controls.Add(dgvBillItems);
            Controls.Add(label4);
            Controls.Add(dtpBillDate);
            Controls.Add(cmbCustomer);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbBranch);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainBillingForm";
            Text = "Form1";
            Load += MainBillingForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBillItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbBranch;
        private Label label1;
        private Label label2;
        private ComboBox cmbCustomer;
        private DateTimePicker dtpBillDate;
        private Label label4;
        private DataGridView dgvBillItems;
        private Button btnSave;
        private DataGridViewTextBoxColumn colSNo;
        private DataGridViewComboBoxColumn colProduct;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colRate;
        private DataGridViewTextBoxColumn colGross;
        private DataGridViewTextBoxColumn colTaxPercent;
        private DataGridViewTextBoxColumn colTaxAmount;
        private DataGridViewTextBoxColumn colNetAmount;
        private Label label3;
        private Button button1;
        private PictureBox pictureBox1;
    }
}
