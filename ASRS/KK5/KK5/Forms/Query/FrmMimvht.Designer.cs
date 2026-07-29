namespace KK5
{
    partial class FrmMimvht
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.txtpdesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.tblot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbprod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblltqty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnqury = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mvhtkey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iodt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_io_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_io_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_proddesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_bestq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_ioqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_ltqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_pltno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_from_lstk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_to_lstk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_ioflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvht_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.comboBox3);
            this.panel3.Controls.Add(this.txtpdesc);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.comboBox2);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Controls.Add(this.chkdt);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dtDateTo);
            this.panel3.Controls.Add(this.dtDatefrom);
            this.panel3.Controls.Add(this.tblot);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.tbprod);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1145, 86);
            this.panel3.TabIndex = 2;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
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
            "SKUQ",
            "SKUF",
            "SKUG",
            "SKUC"});
            this.comboBox3.Location = new System.Drawing.Point(937, 47);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(163, 20);
            this.comboBox3.TabIndex = 141;
            // 
            // txtpdesc
            // 
            this.txtpdesc.Location = new System.Drawing.Point(610, 15);
            this.txtpdesc.Name = "txtpdesc";
            this.txtpdesc.Size = new System.Drawing.Size(226, 21);
            this.txtpdesc.TabIndex = 140;
            this.txtpdesc.DoubleClick += new System.EventHandler(this.txtpdesc_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(551, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 139;
            this.label8.Text = "제품코드";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(575, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 138;
            this.label7.Text = "구분";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ALL",
            "I: 입고",
            "$: 출고",
            "M: 이출"});
            this.comboBox2.Location = new System.Drawing.Point(610, 47);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 20);
            this.comboBox2.TabIndex = 137;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 136;
            this.label4.Text = "구분";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ALL",
            "S",
            "Q"});
            this.comboBox1.Location = new System.Drawing.Point(430, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(84, 20);
            this.comboBox1.TabIndex = 135;
            // 
            // chkdt
            // 
            this.chkdt.AutoSize = true;
            this.chkdt.Location = new System.Drawing.Point(329, 18);
            this.chkdt.Name = "chkdt";
            this.chkdt.Size = new System.Drawing.Size(15, 14);
            this.chkdt.TabIndex = 134;
            this.chkdt.UseVisualStyleBackColor = true;
            this.chkdt.CheckedChanged += new System.EventHandler(this.chkdt_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(191, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 133;
            this.label10.Text = "~";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Enabled = false;
            this.dtDateTo.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(211, 14);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(112, 22);
            this.dtDateTo.TabIndex = 132;
            // 
            // dtDatefrom
            // 
            this.dtDatefrom.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatefrom.Location = new System.Drawing.Point(73, 14);
            this.dtDatefrom.Name = "dtDatefrom";
            this.dtDatefrom.Size = new System.Drawing.Size(116, 22);
            this.dtDatefrom.TabIndex = 131;
            // 
            // tblot
            // 
            this.tblot.Location = new System.Drawing.Point(937, 15);
            this.tblot.Name = "tblot";
            this.tblot.Size = new System.Drawing.Size(79, 21);
            this.tblot.TabIndex = 11;
            this.tblot.DoubleClick += new System.EventHandler(this.tblot_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(874, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Batch No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(874, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Loc";
            // 
            // tbprod
            // 
            this.tbprod.Location = new System.Drawing.Point(430, 15);
            this.tbprod.Name = "tbprod";
            this.tbprod.Size = new System.Drawing.Size(103, 21);
            this.tbprod.TabIndex = 4;
            this.tbprod.DoubleClick += new System.EventHandler(this.tbprod_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "제품코드";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "입출일자";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.dataGridView1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1145, 665);
            this.panel6.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.lblltqty);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.lblqty);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 631);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1145, 34);
            this.panel5.TabIndex = 2;
            // 
            // lblltqty
            // 
            this.lblltqty.BackColor = System.Drawing.Color.White;
            this.lblltqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblltqty.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblltqty.Location = new System.Drawing.Point(303, 4);
            this.lblltqty.Name = "lblltqty";
            this.lblltqty.Size = new System.Drawing.Size(123, 24);
            this.lblltqty.TabIndex = 3;
            this.lblltqty.Text = "0";
            this.lblltqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(234, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "입출량(LT)";
            // 
            // lblqty
            // 
            this.lblqty.BackColor = System.Drawing.Color.White;
            this.lblqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblqty.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblqty.Location = new System.Drawing.Point(84, 5);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(123, 24);
            this.lblqty.TabIndex = 1;
            this.lblqty.Text = "0";
            this.lblqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "입출수량";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Snow;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Ivory;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mvhtkey,
            this.iodt,
            this.mvht_io_date,
            this.mvht_io_time,
            this.mvht_prod,
            this.mvht_proddesc,
            this.mvht_loc,
            this.mvht_lot,
            this.mvht_bestq,
            this.mvht_pksz,
            this.mvht_ioqty,
            this.mvht_ltqty,
            this.mvht_pltno,
            this.mvht_from_lstk,
            this.mvht_to_lstk,
            this.mvht_ioflag,
            this.mvht_remark});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1145, 665);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnqury);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1145, 42);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(983, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnqury
            // 
            this.btnqury.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqury.Location = new System.Drawing.Point(907, 10);
            this.btnqury.Name = "btnqury";
            this.btnqury.Size = new System.Drawing.Size(75, 23);
            this.btnqury.TabIndex = 2;
            this.btnqury.Text = "조회";
            this.btnqury.UseVisualStyleBackColor = true;
            this.btnqury.Click += new System.EventHandler(this.btnqury_Click);
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1058, 10);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 1;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "제품 이동 이력";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 793);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1145, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1145, 815);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 128);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1145, 665);
            this.panel4.TabIndex = 3;
            // 
            // mvhtkey
            // 
            this.mvhtkey.DataPropertyName = "mvhtkey";
            this.mvhtkey.HeaderText = "mvhtkey";
            this.mvhtkey.Name = "mvhtkey";
            this.mvhtkey.ReadOnly = true;
            this.mvhtkey.Visible = false;
            this.mvhtkey.Width = 77;
            // 
            // iodt
            // 
            this.iodt.DataPropertyName = "iodt";
            this.iodt.HeaderText = "입출시간";
            this.iodt.Name = "iodt";
            this.iodt.ReadOnly = true;
            this.iodt.Width = 78;
            // 
            // mvht_io_date
            // 
            this.mvht_io_date.DataPropertyName = "mvht_io_date";
            this.mvht_io_date.HeaderText = "입출일자";
            this.mvht_io_date.Name = "mvht_io_date";
            this.mvht_io_date.ReadOnly = true;
            this.mvht_io_date.Visible = false;
            this.mvht_io_date.Width = 78;
            // 
            // mvht_io_time
            // 
            this.mvht_io_time.DataPropertyName = "mvht_io_time";
            this.mvht_io_time.HeaderText = "입출시각";
            this.mvht_io_time.Name = "mvht_io_time";
            this.mvht_io_time.ReadOnly = true;
            this.mvht_io_time.Visible = false;
            this.mvht_io_time.Width = 78;
            // 
            // mvht_prod
            // 
            this.mvht_prod.DataPropertyName = "mvht_prod";
            this.mvht_prod.HeaderText = "제품코드";
            this.mvht_prod.Name = "mvht_prod";
            this.mvht_prod.ReadOnly = true;
            this.mvht_prod.Width = 78;
            // 
            // mvht_proddesc
            // 
            this.mvht_proddesc.DataPropertyName = "mvht_proddesc";
            this.mvht_proddesc.HeaderText = "제품명";
            this.mvht_proddesc.Name = "mvht_proddesc";
            this.mvht_proddesc.ReadOnly = true;
            this.mvht_proddesc.Width = 66;
            // 
            // mvht_loc
            // 
            this.mvht_loc.DataPropertyName = "mvht_loc";
            this.mvht_loc.HeaderText = "Loc";
            this.mvht_loc.Name = "mvht_loc";
            this.mvht_loc.ReadOnly = true;
            this.mvht_loc.Width = 51;
            // 
            // mvht_lot
            // 
            this.mvht_lot.DataPropertyName = "mvht_lot";
            this.mvht_lot.HeaderText = "Bactch No";
            this.mvht_lot.Name = "mvht_lot";
            this.mvht_lot.ReadOnly = true;
            this.mvht_lot.Width = 89;
            // 
            // mvht_bestq
            // 
            this.mvht_bestq.DataPropertyName = "mvht_bestq";
            this.mvht_bestq.HeaderText = "구분";
            this.mvht_bestq.Name = "mvht_bestq";
            this.mvht_bestq.ReadOnly = true;
            this.mvht_bestq.Width = 54;
            // 
            // mvht_pksz
            // 
            this.mvht_pksz.DataPropertyName = "mvht_pksz";
            this.mvht_pksz.HeaderText = "내용량";
            this.mvht_pksz.Name = "mvht_pksz";
            this.mvht_pksz.ReadOnly = true;
            this.mvht_pksz.Width = 66;
            // 
            // mvht_ioqty
            // 
            this.mvht_ioqty.DataPropertyName = "mvht_ioqty";
            this.mvht_ioqty.HeaderText = "입출수량";
            this.mvht_ioqty.Name = "mvht_ioqty";
            this.mvht_ioqty.ReadOnly = true;
            this.mvht_ioqty.Width = 78;
            // 
            // mvht_ltqty
            // 
            this.mvht_ltqty.DataPropertyName = "mvht_ltqty";
            this.mvht_ltqty.HeaderText = "입출량(LT)";
            this.mvht_ltqty.Name = "mvht_ltqty";
            this.mvht_ltqty.ReadOnly = true;
            this.mvht_ltqty.Width = 91;
            // 
            // mvht_pltno
            // 
            this.mvht_pltno.DataPropertyName = "mvht_pltno";
            this.mvht_pltno.HeaderText = "파렛번호";
            this.mvht_pltno.Name = "mvht_pltno";
            this.mvht_pltno.ReadOnly = true;
            this.mvht_pltno.Width = 78;
            // 
            // mvht_from_lstk
            // 
            this.mvht_from_lstk.DataPropertyName = "mvht_from_lstk";
            this.mvht_from_lstk.HeaderText = "From위치";
            this.mvht_from_lstk.Name = "mvht_from_lstk";
            this.mvht_from_lstk.ReadOnly = true;
            this.mvht_from_lstk.Width = 83;
            // 
            // mvht_to_lstk
            // 
            this.mvht_to_lstk.DataPropertyName = "mvht_to_lstk";
            this.mvht_to_lstk.HeaderText = "To위치";
            this.mvht_to_lstk.Name = "mvht_to_lstk";
            this.mvht_to_lstk.ReadOnly = true;
            this.mvht_to_lstk.Width = 69;
            // 
            // mvht_ioflag
            // 
            this.mvht_ioflag.DataPropertyName = "mvht_ioflag";
            this.mvht_ioflag.HeaderText = "이동구분";
            this.mvht_ioflag.Name = "mvht_ioflag";
            this.mvht_ioflag.ReadOnly = true;
            this.mvht_ioflag.Width = 78;
            // 
            // mvht_remark
            // 
            this.mvht_remark.DataPropertyName = "mvht_remark";
            this.mvht_remark.HeaderText = "Remark";
            this.mvht_remark.Name = "mvht_remark";
            this.mvht_remark.ReadOnly = true;
            this.mvht_remark.Width = 73;
            // 
            // FrmMimvht
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 815);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMimvht";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "제품 이동이력";
            this.Load += new System.EventHandler(this.FrmMimvht_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tblot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbprod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnqury;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtpdesc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblltqty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvhtkey;
        private System.Windows.Forms.DataGridViewTextBoxColumn iodt;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_io_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_io_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_proddesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_bestq;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_ioqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_ltqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_pltno;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_from_lstk;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_to_lstk;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_ioflag;
        private System.Windows.Forms.DataGridViewTextBoxColumn mvht_remark;
    }
}