namespace KK5
{
    partial class FrmTaordiLoad
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnsel = new System.Windows.Forms.Button();
            this.btncmmt = new System.Windows.Forms.Button();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnremark = new System.Windows.Forms.Button();
            this.btncncl = new System.Windows.Forms.Button();
            this.btnchgqty = new System.Windows.Forms.Button();
            this.btnfinish = new System.Windows.Forms.Button();
            this.btndeliverydone = new System.Windows.Forms.Button();
            this.btnexcel = new System.Windows.Forms.Button();
            this.btnChg = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnqury = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bachadate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.load_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.load_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_dest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.arrival = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matnrdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordi_size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recv_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordi_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duedate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordi_check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rmrk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ablad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vsbed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.route = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_no2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.btnsel);
            this.panel3.Controls.Add(this.btncmmt);
            this.panel3.Controls.Add(this.chkdt);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dtDateTo);
            this.panel3.Controls.Add(this.dtDatefrom);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1238, 60);
            this.panel3.TabIndex = 2;
            // 
            // btnsel
            // 
            this.btnsel.Location = new System.Drawing.Point(1008, 17);
            this.btnsel.Name = "btnsel";
            this.btnsel.Size = new System.Drawing.Size(75, 23);
            this.btnsel.TabIndex = 132;
            this.btnsel.Text = "선택ALL";
            this.btnsel.UseVisualStyleBackColor = true;
            this.btnsel.Click += new System.EventHandler(this.btnsel_Click);
            // 
            // btncmmt
            // 
            this.btncmmt.Location = new System.Drawing.Point(1149, 17);
            this.btncmmt.Name = "btncmmt";
            this.btncmmt.Size = new System.Drawing.Size(75, 23);
            this.btncmmt.TabIndex = 137;
            this.btncmmt.Text = "코멘트";
            this.btncmmt.UseVisualStyleBackColor = true;
            this.btncmmt.Visible = false;
            this.btncmmt.Click += new System.EventHandler(this.btncmmt_Click);
            // 
            // chkdt
            // 
            this.chkdt.AutoSize = true;
            this.chkdt.Location = new System.Drawing.Point(360, 22);
            this.chkdt.Name = "chkdt";
            this.chkdt.Size = new System.Drawing.Size(15, 14);
            this.chkdt.TabIndex = 130;
            this.chkdt.UseVisualStyleBackColor = true;
            this.chkdt.CheckedChanged += new System.EventHandler(this.chkdt_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(217, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 129;
            this.label10.Text = "~";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Enabled = false;
            this.dtDateTo.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(237, 19);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(112, 22);
            this.dtDateTo.TabIndex = 128;
            // 
            // dtDatefrom
            // 
            this.dtDatefrom.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatefrom.Location = new System.Drawing.Point(99, 19);
            this.dtDatefrom.Name = "dtDatefrom";
            this.dtDatefrom.Size = new System.Drawing.Size(116, 22);
            this.dtDatefrom.TabIndex = 127;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "상차일자";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnremark);
            this.panel2.Controls.Add(this.btncncl);
            this.panel2.Controls.Add(this.btnchgqty);
            this.panel2.Controls.Add(this.btnfinish);
            this.panel2.Controls.Add(this.btndeliverydone);
            this.panel2.Controls.Add(this.btnexcel);
            this.panel2.Controls.Add(this.btnChg);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqury);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1238, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnremark
            // 
            this.btnremark.Location = new System.Drawing.Point(1082, 9);
            this.btnremark.Name = "btnremark";
            this.btnremark.Size = new System.Drawing.Size(75, 23);
            this.btnremark.TabIndex = 138;
            this.btnremark.Text = "Remark";
            this.btnremark.UseVisualStyleBackColor = true;
            this.btnremark.Click += new System.EventHandler(this.btnremark_Click);
            // 
            // btncncl
            // 
            this.btncncl.Location = new System.Drawing.Point(930, 9);
            this.btncncl.Name = "btncncl";
            this.btncncl.Size = new System.Drawing.Size(75, 23);
            this.btncncl.TabIndex = 136;
            this.btncncl.Text = "예약취소";
            this.btncncl.UseVisualStyleBackColor = true;
            this.btncncl.Click += new System.EventHandler(this.btncncl_Click);
            // 
            // btnchgqty
            // 
            this.btnchgqty.Location = new System.Drawing.Point(1006, 9);
            this.btnchgqty.Name = "btnchgqty";
            this.btnchgqty.Size = new System.Drawing.Size(75, 23);
            this.btnchgqty.TabIndex = 135;
            this.btnchgqty.Text = "수량변경";
            this.btnchgqty.UseVisualStyleBackColor = true;
            this.btnchgqty.Click += new System.EventHandler(this.btnchgqty_Click);
            // 
            // btnfinish
            // 
            this.btnfinish.Location = new System.Drawing.Point(778, 9);
            this.btnfinish.Name = "btnfinish";
            this.btnfinish.Size = new System.Drawing.Size(75, 23);
            this.btnfinish.TabIndex = 134;
            this.btnfinish.Text = "상차완료";
            this.btnfinish.UseVisualStyleBackColor = true;
            this.btnfinish.Click += new System.EventHandler(this.btnfinish_Click);
            // 
            // btndeliverydone
            // 
            this.btndeliverydone.Location = new System.Drawing.Point(854, 9);
            this.btndeliverydone.Name = "btndeliverydone";
            this.btndeliverydone.Size = new System.Drawing.Size(75, 23);
            this.btndeliverydone.TabIndex = 133;
            this.btndeliverydone.Text = "배달완료";
            this.btndeliverydone.UseVisualStyleBackColor = true;
            this.btndeliverydone.Click += new System.EventHandler(this.btndeliverydone_Click);
            // 
            // btnexcel
            // 
            this.btnexcel.Location = new System.Drawing.Point(702, 9);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(75, 23);
            this.btnexcel.TabIndex = 132;
            this.btnexcel.Text = "EXCEL";
            this.btnexcel.UseVisualStyleBackColor = true;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // btnChg
            // 
            this.btnChg.Location = new System.Drawing.Point(626, 9);
            this.btnChg.Name = "btnChg";
            this.btnChg.Size = new System.Drawing.Size(75, 23);
            this.btnChg.TabIndex = 3;
            this.btnChg.Text = "차량변경";
            this.btnChg.UseVisualStyleBackColor = true;
            this.btnChg.Click += new System.EventHandler(this.btnChg_Click);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(1158, 9);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 2;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnqury
            // 
            this.btnqury.Location = new System.Drawing.Point(551, 9);
            this.btnqury.Name = "btnqury";
            this.btnqury.Size = new System.Drawing.Size(75, 23);
            this.btnqury.TabIndex = 1;
            this.btnqury.Text = "조회";
            this.btnqury.UseVisualStyleBackColor = true;
            this.btnqury.Click += new System.EventHandler(this.btnqury_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "상차 작업  ";
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
            this.panel1.Size = new System.Drawing.Size(1238, 736);
            this.panel1.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 99);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1238, 615);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 218);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1238, 397);
            this.panel5.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeight = 24;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.arrival,
            this.sdno,
            this.matnr,
            this.matnrdesc,
            this.ordi_size,
            this.qty,
            this.lgort,
            this.charg,
            this.cust,
            this.cust_name1,
            this.region,
            this.recv_dt,
            this.remark2,
            this.docnum,
            this.posnr,
            this.ordi_seq,
            this.duedate,
            this.ordi_check,
            this.rmrk,
            this.ablad,
            this.vsbed,
            this.route,
            this.car_no2,
            this.car_sno,
            this.cmmt});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1238, 397);
            this.dataGridView2.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bachadate,
            this.seq,
            this.car_no,
            this.car_man,
            this.load_vol,
            this.max_vol,
            this.load_qty,
            this.step,
            this.area_code,
            this.remark,
            this.car_dest});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1238, 218);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // bachadate
            // 
            this.bachadate.DataPropertyName = "bachadate";
            this.bachadate.HeaderText = "상차일자";
            this.bachadate.Name = "bachadate";
            this.bachadate.Width = 78;
            // 
            // seq
            // 
            this.seq.DataPropertyName = "seq";
            this.seq.HeaderText = "배차순번";
            this.seq.Name = "seq";
            this.seq.Width = 78;
            // 
            // car_no
            // 
            this.car_no.DataPropertyName = "car_no";
            this.car_no.HeaderText = "차량번호";
            this.car_no.Name = "car_no";
            this.car_no.ReadOnly = true;
            this.car_no.Width = 78;
            // 
            // car_man
            // 
            this.car_man.DataPropertyName = "car_man";
            this.car_man.HeaderText = "운전기사";
            this.car_man.Name = "car_man";
            this.car_man.ReadOnly = true;
            this.car_man.Width = 78;
            // 
            // load_vol
            // 
            this.load_vol.DataPropertyName = "load_vol";
            this.load_vol.HeaderText = "적재량(LT)";
            this.load_vol.Name = "load_vol";
            this.load_vol.ReadOnly = true;
            this.load_vol.Width = 91;
            // 
            // max_vol
            // 
            this.max_vol.DataPropertyName = "max_vol";
            this.max_vol.HeaderText = "최대VOL";
            this.max_vol.Name = "max_vol";
            this.max_vol.ReadOnly = true;
            this.max_vol.Width = 78;
            // 
            // load_qty
            // 
            this.load_qty.DataPropertyName = "load_qty";
            this.load_qty.HeaderText = "적재수량";
            this.load_qty.Name = "load_qty";
            this.load_qty.ReadOnly = true;
            this.load_qty.Width = 78;
            // 
            // step
            // 
            this.step.DataPropertyName = "step";
            this.step.HeaderText = "상태";
            this.step.Name = "step";
            this.step.ReadOnly = true;
            this.step.Width = 54;
            // 
            // area_code
            // 
            this.area_code.DataPropertyName = "area_code";
            this.area_code.HeaderText = "지역";
            this.area_code.Name = "area_code";
            this.area_code.ReadOnly = true;
            this.area_code.Width = 54;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "비고";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 54;
            // 
            // car_dest
            // 
            this.car_dest.DataPropertyName = "car_dest";
            this.car_dest.HeaderText = "최종목적지";
            this.car_dest.Name = "car_dest";
            this.car_dest.ReadOnly = true;
            this.car_dest.Width = 90;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 714);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1238, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // arrival
            // 
            this.arrival.DataPropertyName = "arrival";
            this.arrival.HeaderText = "도착지";
            this.arrival.Name = "arrival";
            this.arrival.ReadOnly = true;
            this.arrival.Width = 66;
            // 
            // sdno
            // 
            this.sdno.DataPropertyName = "sdno";
            this.sdno.HeaderText = "오더번호";
            this.sdno.Name = "sdno";
            this.sdno.ReadOnly = true;
            this.sdno.Width = 78;
            // 
            // matnr
            // 
            this.matnr.DataPropertyName = "matnr";
            this.matnr.HeaderText = "제품코드";
            this.matnr.Name = "matnr";
            this.matnr.ReadOnly = true;
            this.matnr.Width = 78;
            // 
            // matnrdesc
            // 
            this.matnrdesc.DataPropertyName = "matnrdesc";
            this.matnrdesc.HeaderText = "제품명";
            this.matnrdesc.Name = "matnrdesc";
            this.matnrdesc.ReadOnly = true;
            this.matnrdesc.Width = 66;
            // 
            // ordi_size
            // 
            this.ordi_size.DataPropertyName = "ordi_size";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ordi_size.DefaultCellStyle = dataGridViewCellStyle2;
            this.ordi_size.HeaderText = "내용량";
            this.ordi_size.Name = "ordi_size";
            this.ordi_size.ReadOnly = true;
            this.ordi_size.Width = 66;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.qty.HeaderText = "수량";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 54;
            // 
            // lgort
            // 
            this.lgort.DataPropertyName = "lgort";
            this.lgort.HeaderText = "Loc";
            this.lgort.Name = "lgort";
            this.lgort.ReadOnly = true;
            this.lgort.Width = 51;
            // 
            // charg
            // 
            this.charg.DataPropertyName = "charg";
            this.charg.HeaderText = "BatchNo";
            this.charg.Name = "charg";
            this.charg.ReadOnly = true;
            this.charg.Width = 78;
            // 
            // cust
            // 
            this.cust.DataPropertyName = "cust";
            this.cust.HeaderText = "거래처";
            this.cust.Name = "cust";
            this.cust.ReadOnly = true;
            this.cust.Visible = false;
            this.cust.Width = 66;
            // 
            // cust_name1
            // 
            this.cust_name1.DataPropertyName = "cust_name1";
            this.cust_name1.HeaderText = "거래처명";
            this.cust_name1.Name = "cust_name1";
            this.cust_name1.ReadOnly = true;
            this.cust_name1.Width = 78;
            // 
            // region
            // 
            this.region.DataPropertyName = "region";
            this.region.HeaderText = "지역";
            this.region.Name = "region";
            this.region.ReadOnly = true;
            this.region.Visible = false;
            this.region.Width = 54;
            // 
            // recv_dt
            // 
            this.recv_dt.DataPropertyName = "recv_dt";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.recv_dt.DefaultCellStyle = dataGridViewCellStyle4;
            this.recv_dt.HeaderText = "Shipment_date";
            this.recv_dt.Name = "recv_dt";
            this.recv_dt.ReadOnly = true;
            this.recv_dt.Width = 113;
            // 
            // remark2
            // 
            this.remark2.DataPropertyName = "remark";
            this.remark2.HeaderText = "Remark";
            this.remark2.Name = "remark2";
            this.remark2.ReadOnly = true;
            this.remark2.Width = 73;
            // 
            // docnum
            // 
            this.docnum.DataPropertyName = "docnum";
            this.docnum.HeaderText = "Doc No";
            this.docnum.Name = "docnum";
            this.docnum.ReadOnly = true;
            this.docnum.Width = 72;
            // 
            // posnr
            // 
            this.posnr.DataPropertyName = "posnr";
            this.posnr.HeaderText = "라인번호";
            this.posnr.Name = "posnr";
            this.posnr.ReadOnly = true;
            this.posnr.Width = 78;
            // 
            // ordi_seq
            // 
            this.ordi_seq.DataPropertyName = "ordi_seq";
            this.ordi_seq.HeaderText = "순번";
            this.ordi_seq.Name = "ordi_seq";
            this.ordi_seq.ReadOnly = true;
            this.ordi_seq.Visible = false;
            this.ordi_seq.Width = 54;
            // 
            // duedate
            // 
            this.duedate.DataPropertyName = "duedate";
            this.duedate.HeaderText = "Due Date";
            this.duedate.Name = "duedate";
            this.duedate.ReadOnly = true;
            this.duedate.Width = 81;
            // 
            // ordi_check
            // 
            this.ordi_check.DataPropertyName = "ordi_check";
            this.ordi_check.HeaderText = "Check";
            this.ordi_check.Name = "ordi_check";
            this.ordi_check.ReadOnly = true;
            this.ordi_check.Visible = false;
            this.ordi_check.Width = 66;
            // 
            // rmrk
            // 
            this.rmrk.DataPropertyName = "rmrk";
            this.rmrk.HeaderText = "Internal Comment";
            this.rmrk.Name = "rmrk";
            this.rmrk.ReadOnly = true;
            this.rmrk.Width = 130;
            // 
            // ablad
            // 
            this.ablad.DataPropertyName = "ablad";
            this.ablad.HeaderText = "택배회사";
            this.ablad.Name = "ablad";
            this.ablad.ReadOnly = true;
            this.ablad.Width = 78;
            // 
            // vsbed
            // 
            this.vsbed.DataPropertyName = "vsbed";
            this.vsbed.HeaderText = "배송조건";
            this.vsbed.Name = "vsbed";
            this.vsbed.ReadOnly = true;
            this.vsbed.Width = 78;
            // 
            // route
            // 
            this.route.DataPropertyName = "route";
            this.route.HeaderText = "운송경로";
            this.route.Name = "route";
            this.route.ReadOnly = true;
            this.route.Width = 78;
            // 
            // car_no2
            // 
            this.car_no2.DataPropertyName = "car_no";
            this.car_no2.HeaderText = "차량번호";
            this.car_no2.Name = "car_no2";
            this.car_no2.ReadOnly = true;
            this.car_no2.Visible = false;
            this.car_no2.Width = 78;
            // 
            // car_sno
            // 
            this.car_sno.DataPropertyName = "car_sno";
            this.car_sno.HeaderText = "차량순번";
            this.car_sno.Name = "car_sno";
            this.car_sno.ReadOnly = true;
            this.car_sno.Visible = false;
            this.car_sno.Width = 78;
            // 
            // cmmt
            // 
            this.cmmt.DataPropertyName = "cmmt";
            this.cmmt.HeaderText = "Shipping instruction";
            this.cmmt.Name = "cmmt";
            this.cmmt.ReadOnly = true;
            this.cmmt.Width = 141;
            // 
            // FrmTaordiLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 736);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTaordiLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "상차 작업";
            this.Load += new System.EventHandler(this.FrmTraordiLoad_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnqury;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bachadate;
        private System.Windows.Forms.DataGridViewTextBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn load_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn load_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn step;
        private System.Windows.Forms.DataGridViewTextBoxColumn area_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_dest;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnchgqty;
        private System.Windows.Forms.Button btnfinish;
        private System.Windows.Forms.Button btndeliverydone;
        private System.Windows.Forms.Button btnexcel;
        private System.Windows.Forms.Button btnChg;
        private System.Windows.Forms.Button btncncl;
        private System.Windows.Forms.Button btnremark;
        private System.Windows.Forms.Button btncmmt;
        private System.Windows.Forms.Button btnsel;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrival;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdno;
        private System.Windows.Forms.DataGridViewTextBoxColumn matnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn matnrdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordi_size;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn lgort;
        private System.Windows.Forms.DataGridViewTextBoxColumn charg;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn region;
        private System.Windows.Forms.DataGridViewTextBoxColumn recv_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark2;
        private System.Windows.Forms.DataGridViewTextBoxColumn docnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn posnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordi_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn duedate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordi_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn rmrk;
        private System.Windows.Forms.DataGridViewTextBoxColumn ablad;
        private System.Windows.Forms.DataGridViewTextBoxColumn vsbed;
        private System.Windows.Forms.DataGridViewTextBoxColumn route;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no2;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmmt;
    }
}