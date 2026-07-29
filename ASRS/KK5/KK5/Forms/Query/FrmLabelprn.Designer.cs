namespace KK5
{
    partial class FrmLabelprn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnlabel = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnqry = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbPlt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbProd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.plti_pltno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_oprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_bestq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstk_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstk_use = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.plti_pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_stok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_rqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_idate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_itime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstk_stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstk_io = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtpdesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnlabel);
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnqry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1258, 43);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1095, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnlabel
            // 
            this.btnlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnlabel.Location = new System.Drawing.Point(1019, 12);
            this.btnlabel.Name = "btnlabel";
            this.btnlabel.Size = new System.Drawing.Size(75, 23);
            this.btnlabel.TabIndex = 5;
            this.btnlabel.Text = "재발행";
            this.btnlabel.UseVisualStyleBackColor = true;
            this.btnlabel.Click += new System.EventHandler(this.btnlabel_Click);
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1171, 12);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 4;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("GulimChe", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "라벨 재발행";
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(943, 12);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 2;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 780);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1258, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtpdesc);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.tbPlt);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbLot);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tbLoc);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbProd);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1258, 85);
            this.panel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1080, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 155;
            this.label4.Text = "구분";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ALL",
            "S",
            "Q"});
            this.comboBox2.Location = new System.Drawing.Point(1122, 9);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(101, 20);
            this.comboBox2.TabIndex = 154;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 153;
            this.label2.Text = "창고";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "A:자동창고",
            "F:공장동",
            "Y:야적",
            "ALL"});
            this.comboBox1.Location = new System.Drawing.Point(49, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 152;
            // 
            // tbPlt
            // 
            this.tbPlt.Location = new System.Drawing.Point(249, 47);
            this.tbPlt.Name = "tbPlt";
            this.tbPlt.Size = new System.Drawing.Size(106, 21);
            this.tbPlt.TabIndex = 143;
            this.tbPlt.DoubleClick += new System.EventHandler(this.tbPlt_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 142;
            this.label8.Text = "파렛번호";
            // 
            // tbLot
            // 
            this.tbLot.Location = new System.Drawing.Point(898, 8);
            this.tbLot.Name = "tbLot";
            this.tbLot.Size = new System.Drawing.Size(96, 21);
            this.tbLot.TabIndex = 138;
            this.tbLot.DoubleClick += new System.EventHandler(this.tbLot_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(838, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 137;
            this.label6.Text = "Bach No";
            // 
            // tbLoc
            // 
            this.tbLoc.Location = new System.Drawing.Point(749, 9);
            this.tbLoc.Name = "tbLoc";
            this.tbLoc.Size = new System.Drawing.Size(55, 21);
            this.tbLoc.TabIndex = 136;
            this.tbLoc.DoubleClick += new System.EventHandler(this.tbLoc_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(719, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 12);
            this.label5.TabIndex = 135;
            this.label5.Text = "Loc";
            // 
            // tbProd
            // 
            this.tbProd.Location = new System.Drawing.Point(249, 10);
            this.tbProd.Name = "tbProd";
            this.tbProd.Size = new System.Drawing.Size(145, 21);
            this.tbProd.TabIndex = 132;
            this.tbProd.DoubleClick += new System.EventHandler(this.tbProd_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 131;
            this.label3.Text = "제품코드";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 128);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1258, 652);
            this.panel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeight = 24;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plti_pltno,
            this.plti_prod,
            this.plti_oprod,
            this.plti_pdesc,
            this.plti_lot,
            this.plti_loc,
            this.plti_bestq,
            this.lstk_no,
            this.lstk_use,
            this.plti_pksz,
            this.plti_stok,
            this.plti_rqty,
            this.plti_remark,
            this.plti_idate,
            this.plti_itime,
            this.plti_flag,
            this.lstk_stat,
            this.lstk_io});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1258, 652);
            this.dataGridView1.TabIndex = 4;
            // 
            // plti_pltno
            // 
            this.plti_pltno.DataPropertyName = "plti_pltno";
            this.plti_pltno.HeaderText = "파렛번호";
            this.plti_pltno.Name = "plti_pltno";
            this.plti_pltno.ReadOnly = true;
            this.plti_pltno.Width = 80;
            // 
            // plti_prod
            // 
            this.plti_prod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.plti_prod.DataPropertyName = "plti_prod";
            this.plti_prod.HeaderText = "제품코드";
            this.plti_prod.Name = "plti_prod";
            this.plti_prod.ReadOnly = true;
            this.plti_prod.Width = 78;
            // 
            // plti_oprod
            // 
            this.plti_oprod.DataPropertyName = "plti_oprod";
            this.plti_oprod.HeaderText = "구제품";
            this.plti_oprod.Name = "plti_oprod";
            this.plti_oprod.ReadOnly = true;
            // 
            // plti_pdesc
            // 
            this.plti_pdesc.DataPropertyName = "plti_pdesc";
            this.plti_pdesc.HeaderText = "제품명";
            this.plti_pdesc.Name = "plti_pdesc";
            this.plti_pdesc.ReadOnly = true;
            this.plti_pdesc.Width = 300;
            // 
            // plti_lot
            // 
            this.plti_lot.DataPropertyName = "plti_lot";
            this.plti_lot.HeaderText = "Bach No";
            this.plti_lot.Name = "plti_lot";
            this.plti_lot.ReadOnly = true;
            this.plti_lot.Width = 80;
            // 
            // plti_loc
            // 
            this.plti_loc.DataPropertyName = "plti_loc";
            this.plti_loc.HeaderText = "Loc";
            this.plti_loc.Name = "plti_loc";
            this.plti_loc.ReadOnly = true;
            this.plti_loc.Width = 40;
            // 
            // plti_bestq
            // 
            this.plti_bestq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.plti_bestq.DataPropertyName = "plti_bestq";
            this.plti_bestq.HeaderText = "구분";
            this.plti_bestq.Name = "plti_bestq";
            this.plti_bestq.ReadOnly = true;
            this.plti_bestq.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.plti_bestq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.plti_bestq.Width = 35;
            // 
            // lstk_no
            // 
            this.lstk_no.DataPropertyName = "lstk_no";
            this.lstk_no.HeaderText = "보관위치";
            this.lstk_no.Name = "lstk_no";
            this.lstk_no.ReadOnly = true;
            this.lstk_no.Width = 80;
            // 
            // lstk_use
            // 
            this.lstk_use.DataPropertyName = "lstk_use";
            this.lstk_use.FalseValue = "1";
            this.lstk_use.FillWeight = 30F;
            this.lstk_use.HeaderText = "금지";
            this.lstk_use.Name = "lstk_use";
            this.lstk_use.ReadOnly = true;
            this.lstk_use.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lstk_use.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.lstk_use.TrueValue = "0";
            this.lstk_use.Width = 40;
            // 
            // plti_pksz
            // 
            this.plti_pksz.DataPropertyName = "plti_pksz";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "#.00";
            this.plti_pksz.DefaultCellStyle = dataGridViewCellStyle6;
            this.plti_pksz.HeaderText = "내용량";
            this.plti_pksz.Name = "plti_pksz";
            this.plti_pksz.ReadOnly = true;
            this.plti_pksz.Width = 80;
            // 
            // plti_stok
            // 
            this.plti_stok.DataPropertyName = "plti_stok";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "#,###,##0";
            this.plti_stok.DefaultCellStyle = dataGridViewCellStyle7;
            this.plti_stok.HeaderText = "재고량";
            this.plti_stok.Name = "plti_stok";
            this.plti_stok.ReadOnly = true;
            this.plti_stok.Width = 80;
            // 
            // plti_rqty
            // 
            this.plti_rqty.DataPropertyName = "plti_rqty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "#,###,##0";
            this.plti_rqty.DefaultCellStyle = dataGridViewCellStyle8;
            this.plti_rqty.HeaderText = "예약량";
            this.plti_rqty.Name = "plti_rqty";
            this.plti_rqty.ReadOnly = true;
            this.plti_rqty.Width = 80;
            // 
            // plti_remark
            // 
            this.plti_remark.DataPropertyName = "plti_remark";
            this.plti_remark.HeaderText = "Remark";
            this.plti_remark.Name = "plti_remark";
            this.plti_remark.ReadOnly = true;
            this.plti_remark.Width = 150;
            // 
            // plti_idate
            // 
            this.plti_idate.DataPropertyName = "plti_idate";
            this.plti_idate.HeaderText = "입고일자";
            this.plti_idate.Name = "plti_idate";
            this.plti_idate.ReadOnly = true;
            this.plti_idate.Width = 80;
            // 
            // plti_itime
            // 
            this.plti_itime.DataPropertyName = "plti_itime";
            this.plti_itime.HeaderText = "입고시각";
            this.plti_itime.Name = "plti_itime";
            this.plti_itime.ReadOnly = true;
            this.plti_itime.Width = 80;
            // 
            // plti_flag
            // 
            this.plti_flag.DataPropertyName = "plti_flag";
            this.plti_flag.HeaderText = "재고상태";
            this.plti_flag.Name = "plti_flag";
            this.plti_flag.ReadOnly = true;
            // 
            // lstk_stat
            // 
            this.lstk_stat.DataPropertyName = "lstk_stat";
            this.lstk_stat.HeaderText = "Rack상태";
            this.lstk_stat.Name = "lstk_stat";
            // 
            // lstk_io
            // 
            this.lstk_io.DataPropertyName = "lstk_io";
            this.lstk_io.HeaderText = "입출구분";
            this.lstk_io.Name = "lstk_io";
            // 
            // txtpdesc
            // 
            this.txtpdesc.Location = new System.Drawing.Point(455, 11);
            this.txtpdesc.Name = "txtpdesc";
            this.txtpdesc.Size = new System.Drawing.Size(235, 21);
            this.txtpdesc.TabIndex = 157;
            this.txtpdesc.DoubleClick += new System.EventHandler(this.txtpdesc_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(411, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 156;
            this.label7.Text = "제품명";
            // 
            // FrmLabelprn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 802);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLabelprn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "라벨재발행";
            this.Load += new System.EventHandler(this.FrmLabelprn_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnqry;
        private System.Windows.Forms.Button btnlabel;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tbPlt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbProd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pltno;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_oprod;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_bestq;
        private System.Windows.Forms.DataGridViewTextBoxColumn lstk_no;
        private System.Windows.Forms.DataGridViewCheckBoxColumn lstk_use;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_stok;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_rqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_idate;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_itime;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn lstk_stat;
        private System.Windows.Forms.DataGridViewTextBoxColumn lstk_io;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtpdesc;
        private System.Windows.Forms.Label label7;
    }
}