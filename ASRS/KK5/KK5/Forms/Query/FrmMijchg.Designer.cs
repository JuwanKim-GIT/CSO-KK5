namespace KK5
{
    partial class FrmMijchg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbLot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbpdesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbprod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnqry = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hist_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_hdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_htime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_ctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pltno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_bestq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstk_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_stok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_idate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_itime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLot
            // 
            this.tbLot.Location = new System.Drawing.Point(1000, 15);
            this.tbLot.Name = "tbLot";
            this.tbLot.Size = new System.Drawing.Size(101, 21);
            this.tbLot.TabIndex = 138;
            this.tbLot.DoubleClick += new System.EventHandler(this.tbLot_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(940, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 137;
            this.label6.Text = "Batch No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(703, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 12);
            this.label5.TabIndex = 135;
            this.label5.Text = "Loc";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 24;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.hist_dt,
            this.plti_hdate,
            this.plti_htime,
            this.plti_ctype,
            this.plti_12,
            this.plti_pltno,
            this.plti_prod,
            this.plti_pdesc,
            this.plti_lot,
            this.plti_loc,
            this.plti_bestq,
            this.lstk_no,
            this.plti_pksz,
            this.plti_stok,
            this.plti_remark,
            this.plti_idate,
            this.plti_itime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1184, 523);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 523);
            this.panel3.TabIndex = 7;
            // 
            // tbpdesc
            // 
            this.tbpdesc.Location = new System.Drawing.Point(743, 42);
            this.tbpdesc.Name = "tbpdesc";
            this.tbpdesc.Size = new System.Drawing.Size(285, 21);
            this.tbpdesc.TabIndex = 132;
            this.tbpdesc.DoubleClick += new System.EventHandler(this.txtpdesc_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(693, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 131;
            this.label3.Text = "제품명";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.tbprod);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.chkdt);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.dtDateTo);
            this.panel2.Controls.Add(this.dtDatefrom);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbLot);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbpdesc);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 79);
            this.panel2.TabIndex = 6;
            // 
            // tbprod
            // 
            this.tbprod.Location = new System.Drawing.Point(409, 42);
            this.tbprod.Name = "tbprod";
            this.tbprod.Size = new System.Drawing.Size(103, 21);
            this.tbprod.TabIndex = 162;
            this.tbprod.DoubleClick += new System.EventHandler(this.tbprod_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 161;
            this.label7.Text = "제품코드";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 159;
            this.label2.Text = "변경유형";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ALL",
            "1: 제품변경",
            "2: LOC변경",
            "3: 배치변경",
            "4: 수량변경",
            "5: 상태변경"});
            this.comboBox2.Location = new System.Drawing.Point(409, 13);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(139, 20);
            this.comboBox2.TabIndex = 158;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ALL",
            "0010:Fin Good AWS",
            "0035:Return Goods",
            "0050:Obsolete Stok",
            "0060:Rework",
            "0070:Damaged",
            "0080:Quarantined",
            "0090:Scap",
            "2000:Finished Goods",
            "SKUM",
            "SKUD",
            "SKUQ"});
            this.comboBox1.Location = new System.Drawing.Point(743, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(169, 20);
            this.comboBox1.TabIndex = 157;
            // 
            // chkdt
            // 
            this.chkdt.AutoSize = true;
            this.chkdt.Location = new System.Drawing.Point(308, 16);
            this.chkdt.Name = "chkdt";
            this.chkdt.Size = new System.Drawing.Size(15, 14);
            this.chkdt.TabIndex = 156;
            this.chkdt.UseVisualStyleBackColor = true;
            this.chkdt.CheckedChanged += new System.EventHandler(this.chkdt_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 155;
            this.label10.Text = "~";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Enabled = false;
            this.dtDateTo.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(189, 11);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(112, 22);
            this.dtDateTo.TabIndex = 154;
            // 
            // dtDatefrom
            // 
            this.dtDatefrom.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatefrom.Location = new System.Drawing.Point(51, 11);
            this.dtDatefrom.Name = "dtDatefrom";
            this.dtDatefrom.Size = new System.Drawing.Size(116, 22);
            this.dtDatefrom.TabIndex = 153;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 152;
            this.label9.Text = "일자";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1097, 12);
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
            this.label1.Size = new System.Drawing.Size(251, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "AWS 재고변경이력";
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(946, 12);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 2;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnqry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 43);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1022, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // seq
            // 
            this.seq.DataPropertyName = "seq";
            this.seq.HeaderText = "seq";
            this.seq.Name = "seq";
            this.seq.Visible = false;
            this.seq.Width = 51;
            // 
            // hist_dt
            // 
            this.hist_dt.DataPropertyName = "hist_dt";
            this.hist_dt.HeaderText = "변경시간";
            this.hist_dt.Name = "hist_dt";
            this.hist_dt.ReadOnly = true;
            this.hist_dt.Width = 78;
            // 
            // plti_hdate
            // 
            this.plti_hdate.DataPropertyName = "plti_hdate";
            this.plti_hdate.HeaderText = "변경일자";
            this.plti_hdate.Name = "plti_hdate";
            this.plti_hdate.ReadOnly = true;
            this.plti_hdate.Visible = false;
            this.plti_hdate.Width = 78;
            // 
            // plti_htime
            // 
            this.plti_htime.DataPropertyName = "plti_htime";
            this.plti_htime.HeaderText = "변경시각";
            this.plti_htime.Name = "plti_htime";
            this.plti_htime.ReadOnly = true;
            this.plti_htime.Visible = false;
            this.plti_htime.Width = 78;
            // 
            // plti_ctype
            // 
            this.plti_ctype.DataPropertyName = "plti_ctype";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.plti_ctype.DefaultCellStyle = dataGridViewCellStyle2;
            this.plti_ctype.HeaderText = "유형";
            this.plti_ctype.Name = "plti_ctype";
            this.plti_ctype.ReadOnly = true;
            this.plti_ctype.Width = 54;
            // 
            // plti_12
            // 
            this.plti_12.DataPropertyName = "plti_12";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.plti_12.DefaultCellStyle = dataGridViewCellStyle3;
            this.plti_12.HeaderText = "번호";
            this.plti_12.Name = "plti_12";
            this.plti_12.Width = 54;
            // 
            // plti_pltno
            // 
            this.plti_pltno.DataPropertyName = "plti_pltno";
            this.plti_pltno.HeaderText = "파렛번호";
            this.plti_pltno.Name = "plti_pltno";
            this.plti_pltno.ReadOnly = true;
            this.plti_pltno.Width = 78;
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
            // plti_pdesc
            // 
            this.plti_pdesc.DataPropertyName = "plti_pdesc";
            this.plti_pdesc.HeaderText = "제품명";
            this.plti_pdesc.Name = "plti_pdesc";
            this.plti_pdesc.ReadOnly = true;
            this.plti_pdesc.Width = 66;
            // 
            // plti_lot
            // 
            this.plti_lot.DataPropertyName = "plti_lot";
            this.plti_lot.HeaderText = "Batch No";
            this.plti_lot.Name = "plti_lot";
            this.plti_lot.ReadOnly = true;
            this.plti_lot.Width = 82;
            // 
            // plti_loc
            // 
            this.plti_loc.DataPropertyName = "plti_loc";
            this.plti_loc.HeaderText = "Loc";
            this.plti_loc.Name = "plti_loc";
            this.plti_loc.ReadOnly = true;
            this.plti_loc.Width = 51;
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
            this.lstk_no.DataPropertyName = "plti_lstk";
            this.lstk_no.HeaderText = "보관위치";
            this.lstk_no.Name = "lstk_no";
            this.lstk_no.ReadOnly = true;
            this.lstk_no.Width = 78;
            // 
            // plti_pksz
            // 
            this.plti_pksz.DataPropertyName = "plti_pksz";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#.00";
            this.plti_pksz.DefaultCellStyle = dataGridViewCellStyle4;
            this.plti_pksz.HeaderText = "내용량";
            this.plti_pksz.Name = "plti_pksz";
            this.plti_pksz.ReadOnly = true;
            this.plti_pksz.Width = 66;
            // 
            // plti_stok
            // 
            this.plti_stok.DataPropertyName = "plti_stok";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,###,##0";
            this.plti_stok.DefaultCellStyle = dataGridViewCellStyle5;
            this.plti_stok.HeaderText = "재고량";
            this.plti_stok.Name = "plti_stok";
            this.plti_stok.ReadOnly = true;
            this.plti_stok.Width = 66;
            // 
            // plti_remark
            // 
            this.plti_remark.DataPropertyName = "plti_remark";
            this.plti_remark.HeaderText = "Remark";
            this.plti_remark.Name = "plti_remark";
            this.plti_remark.ReadOnly = true;
            this.plti_remark.Width = 73;
            // 
            // plti_idate
            // 
            this.plti_idate.DataPropertyName = "plti_idate";
            this.plti_idate.HeaderText = "입고일자";
            this.plti_idate.Name = "plti_idate";
            this.plti_idate.ReadOnly = true;
            this.plti_idate.Width = 78;
            // 
            // plti_itime
            // 
            this.plti_itime.DataPropertyName = "plti_itime";
            this.plti_itime.HeaderText = "입고시각";
            this.plti_itime.Name = "plti_itime";
            this.plti_itime.ReadOnly = true;
            this.plti_itime.Width = 78;
            // 
            // FrmMijchg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 667);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMijchg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AWS 재고변경이력";
            this.Load += new System.EventHandler(this.FrmMijchg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbLot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbpdesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnqry;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbprod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn hist_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_hdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_htime;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_ctype;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_12;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pltno;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_bestq;
        private System.Windows.Forms.DataGridViewTextBoxColumn lstk_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_stok;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_idate;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_itime;
    }
}