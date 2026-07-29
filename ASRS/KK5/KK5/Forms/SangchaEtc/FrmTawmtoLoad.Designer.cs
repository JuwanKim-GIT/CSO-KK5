namespace KK5
{
    partial class FrmTawmtoLoad
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
            this.car_dest = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnsel = new System.Windows.Forms.Button();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btncmmt = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnexit = new System.Windows.Forms.Button();
            this.btncncl = new System.Windows.Forms.Button();
            this.btnchgqty = new System.Windows.Forms.Button();
            this.btnfinish = new System.Windows.Forms.Button();
            this.btndeliverydone = new System.Windows.Forms.Button();
            this.btnexcel = new System.Windows.Forms.Button();
            this.btnChg = new System.Windows.Forms.Button();
            this.btnqury = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.docnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tapos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwlvs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maktx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bestq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vsolm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wdatu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vltyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nltyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vfdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordi_check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lsonr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sobkz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordi_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bachadate2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_no2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.print_step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // car_dest
            // 
            this.car_dest.DataPropertyName = "car_dest";
            this.car_dest.HeaderText = "최종목적지";
            this.car_dest.Name = "car_dest";
            this.car_dest.ReadOnly = true;
            this.car_dest.Width = 90;
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
            this.dataGridView1.Size = new System.Drawing.Size(1356, 218);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 704);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1356, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.btnsel);
            this.panel3.Controls.Add(this.chkdt);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dtDateTo);
            this.panel3.Controls.Add(this.dtDatefrom);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1356, 60);
            this.panel3.TabIndex = 2;
            // 
            // btnsel
            // 
            this.btnsel.Location = new System.Drawing.Point(899, 23);
            this.btnsel.Name = "btnsel";
            this.btnsel.Size = new System.Drawing.Size(75, 23);
            this.btnsel.TabIndex = 132;
            this.btnsel.Text = "선택ALL";
            this.btnsel.UseVisualStyleBackColor = true;
            this.btnsel.Click += new System.EventHandler(this.btnsel_Click);
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
            // btncmmt
            // 
            this.btncmmt.Location = new System.Drawing.Point(952, 9);
            this.btncmmt.Name = "btncmmt";
            this.btncmmt.Size = new System.Drawing.Size(70, 23);
            this.btncmmt.TabIndex = 137;
            this.btncmmt.Text = "코멘트";
            this.btncmmt.UseVisualStyleBackColor = true;
            this.btncmmt.Click += new System.EventHandler(this.btncmmt_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btncncl);
            this.panel2.Controls.Add(this.btncmmt);
            this.panel2.Controls.Add(this.btnchgqty);
            this.panel2.Controls.Add(this.btnfinish);
            this.panel2.Controls.Add(this.btndeliverydone);
            this.panel2.Controls.Add(this.btnexcel);
            this.panel2.Controls.Add(this.btnChg);
            this.panel2.Controls.Add(this.btnqury);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1356, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(1023, 9);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(70, 23);
            this.btnexit.TabIndex = 138;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btncncl
            // 
            this.btncncl.Location = new System.Drawing.Point(800, 9);
            this.btncncl.Name = "btncncl";
            this.btncncl.Size = new System.Drawing.Size(75, 23);
            this.btncncl.TabIndex = 136;
            this.btncncl.Text = "예약취소";
            this.btncncl.UseVisualStyleBackColor = true;
            this.btncncl.Click += new System.EventHandler(this.btncncl_Click);
            // 
            // btnchgqty
            // 
            this.btnchgqty.Location = new System.Drawing.Point(876, 9);
            this.btnchgqty.Name = "btnchgqty";
            this.btnchgqty.Size = new System.Drawing.Size(75, 23);
            this.btnchgqty.TabIndex = 135;
            this.btnchgqty.Text = "수량변경";
            this.btnchgqty.UseVisualStyleBackColor = true;
            this.btnchgqty.Click += new System.EventHandler(this.btnchgqty_Click);
            // 
            // btnfinish
            // 
            this.btnfinish.Location = new System.Drawing.Point(648, 9);
            this.btnfinish.Name = "btnfinish";
            this.btnfinish.Size = new System.Drawing.Size(75, 23);
            this.btnfinish.TabIndex = 134;
            this.btnfinish.Text = "상차완료";
            this.btnfinish.UseVisualStyleBackColor = true;
            this.btnfinish.Click += new System.EventHandler(this.btnfinish_Click);
            // 
            // btndeliverydone
            // 
            this.btndeliverydone.Location = new System.Drawing.Point(724, 9);
            this.btndeliverydone.Name = "btndeliverydone";
            this.btndeliverydone.Size = new System.Drawing.Size(75, 23);
            this.btndeliverydone.TabIndex = 133;
            this.btndeliverydone.Text = "배달완료";
            this.btndeliverydone.UseVisualStyleBackColor = true;
            this.btndeliverydone.Click += new System.EventHandler(this.btndeliverydone_Click);
            // 
            // btnexcel
            // 
            this.btnexcel.Location = new System.Drawing.Point(572, 9);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(75, 23);
            this.btnexcel.TabIndex = 132;
            this.btnexcel.Text = "EXCEL";
            this.btnexcel.UseVisualStyleBackColor = true;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // btnChg
            // 
            this.btnChg.Location = new System.Drawing.Point(496, 9);
            this.btnChg.Name = "btnChg";
            this.btnChg.Size = new System.Drawing.Size(75, 23);
            this.btnChg.TabIndex = 3;
            this.btnChg.Text = "차량변경";
            this.btnChg.UseVisualStyleBackColor = true;
            this.btnChg.Click += new System.EventHandler(this.btnChg_Click);
            // 
            // btnqury
            // 
            this.btnqury.Location = new System.Drawing.Point(421, 9);
            this.btnqury.Name = "btnqury";
            this.btnqury.Size = new System.Drawing.Size(75, 23);
            this.btnqury.TabIndex = 1;
            this.btnqury.Text = "조회";
            this.btnqury.UseVisualStyleBackColor = true;
            this.btnqury.Click += new System.EventHandler(this.btnqury_Click);
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
            this.panel1.Size = new System.Drawing.Size(1356, 726);
            this.panel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 99);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1356, 605);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 218);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1356, 387);
            this.panel5.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.docnum,
            this.tanum,
            this.tapos,
            this.bwlvs,
            this.matnr,
            this.maktx,
            this.lgort,
            this.charg,
            this.bestq,
            this.pksz,
            this.vsolm,
            this.wdatu,
            this.wenum,
            this.vltyp,
            this.nltyp,
            this.trart,
            this.bname,
            this.vfdat,
            this.remark2,
            this.ordi_check,
            this.lsonr,
            this.sobkz,
            this.ordi_seq,
            this.bachadate2,
            this.car_no2,
            this.car_step,
            this.car_sno,
            this.print_step,
            this.bigo});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.GridColor = System.Drawing.Color.Silver;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1356, 387);
            this.dataGridView2.TabIndex = 2;
            // 
            // docnum
            // 
            this.docnum.DataPropertyName = "docnum";
            this.docnum.HeaderText = "DOC No";
            this.docnum.Name = "docnum";
            this.docnum.Width = 76;
            // 
            // tanum
            // 
            this.tanum.DataPropertyName = "tanum";
            this.tanum.HeaderText = "납품번호";
            this.tanum.Name = "tanum";
            this.tanum.Width = 78;
            // 
            // tapos
            // 
            this.tapos.DataPropertyName = "tapos";
            this.tapos.HeaderText = "라인번호";
            this.tapos.Name = "tapos";
            this.tapos.ReadOnly = true;
            this.tapos.Width = 78;
            // 
            // bwlvs
            // 
            this.bwlvs.DataPropertyName = "bwlvs";
            this.bwlvs.HeaderText = "출고유형";
            this.bwlvs.Name = "bwlvs";
            this.bwlvs.ReadOnly = true;
            this.bwlvs.Width = 78;
            // 
            // matnr
            // 
            this.matnr.DataPropertyName = "matnr";
            this.matnr.HeaderText = "제품코드";
            this.matnr.Name = "matnr";
            this.matnr.ReadOnly = true;
            this.matnr.Width = 78;
            // 
            // maktx
            // 
            this.maktx.DataPropertyName = "maktx";
            this.maktx.HeaderText = "제품명";
            this.maktx.Name = "maktx";
            this.maktx.ReadOnly = true;
            this.maktx.Width = 66;
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
            this.charg.HeaderText = "배치번호";
            this.charg.Name = "charg";
            this.charg.ReadOnly = true;
            this.charg.Width = 78;
            // 
            // bestq
            // 
            this.bestq.DataPropertyName = "bestq";
            this.bestq.HeaderText = "구분";
            this.bestq.Name = "bestq";
            this.bestq.ReadOnly = true;
            this.bestq.Width = 54;
            // 
            // pksz
            // 
            this.pksz.DataPropertyName = "pksz";
            this.pksz.HeaderText = "내용량";
            this.pksz.Name = "pksz";
            this.pksz.Width = 66;
            // 
            // vsolm
            // 
            this.vsolm.DataPropertyName = "vsolm";
            this.vsolm.HeaderText = "수량";
            this.vsolm.Name = "vsolm";
            this.vsolm.ReadOnly = true;
            this.vsolm.Width = 54;
            // 
            // wdatu
            // 
            this.wdatu.DataPropertyName = "wdatu";
            this.wdatu.HeaderText = "납입일자";
            this.wdatu.Name = "wdatu";
            this.wdatu.ReadOnly = true;
            this.wdatu.Width = 78;
            // 
            // wenum
            // 
            this.wenum.DataPropertyName = "wenum";
            this.wenum.HeaderText = "납입번호";
            this.wenum.Name = "wenum";
            this.wenum.ReadOnly = true;
            this.wenum.Width = 78;
            // 
            // vltyp
            // 
            this.vltyp.DataPropertyName = "vltyp";
            this.vltyp.HeaderText = "Source";
            this.vltyp.Name = "vltyp";
            this.vltyp.ReadOnly = true;
            this.vltyp.Width = 70;
            // 
            // nltyp
            // 
            this.nltyp.DataPropertyName = "nltyp";
            this.nltyp.HeaderText = "Dest";
            this.nltyp.Name = "nltyp";
            this.nltyp.ReadOnly = true;
            this.nltyp.Width = 55;
            // 
            // trart
            // 
            this.trart.DataPropertyName = "trart";
            this.trart.HeaderText = "ShipType";
            this.trart.Name = "trart";
            this.trart.ReadOnly = true;
            this.trart.Width = 84;
            // 
            // bname
            // 
            this.bname.DataPropertyName = "bname";
            this.bname.HeaderText = "담당자";
            this.bname.Name = "bname";
            this.bname.ReadOnly = true;
            this.bname.Width = 66;
            // 
            // vfdat
            // 
            this.vfdat.DataPropertyName = "vfdat";
            this.vfdat.HeaderText = "유효기간";
            this.vfdat.Name = "vfdat";
            this.vfdat.ReadOnly = true;
            this.vfdat.Width = 78;
            // 
            // remark2
            // 
            this.remark2.DataPropertyName = "remark";
            this.remark2.HeaderText = "Comment";
            this.remark2.Name = "remark2";
            this.remark2.ReadOnly = true;
            this.remark2.Width = 85;
            // 
            // ordi_check
            // 
            this.ordi_check.DataPropertyName = "ordi_check";
            this.ordi_check.HeaderText = "Check";
            this.ordi_check.Name = "ordi_check";
            this.ordi_check.ReadOnly = true;
            this.ordi_check.Width = 66;
            // 
            // lsonr
            // 
            this.lsonr.DataPropertyName = "lsonr";
            this.lsonr.HeaderText = "lsonr";
            this.lsonr.Name = "lsonr";
            this.lsonr.ReadOnly = true;
            this.lsonr.Width = 58;
            // 
            // sobkz
            // 
            this.sobkz.DataPropertyName = "sobkz";
            this.sobkz.HeaderText = "sobkz";
            this.sobkz.Name = "sobkz";
            this.sobkz.ReadOnly = true;
            this.sobkz.Width = 64;
            // 
            // ordi_seq
            // 
            this.ordi_seq.DataPropertyName = "ordi_seq";
            this.ordi_seq.HeaderText = "ordi_seq";
            this.ordi_seq.Name = "ordi_seq";
            this.ordi_seq.ReadOnly = true;
            this.ordi_seq.Visible = false;
            this.ordi_seq.Width = 78;
            // 
            // bachadate2
            // 
            this.bachadate2.DataPropertyName = "bachadate";
            this.bachadate2.HeaderText = "배차일자";
            this.bachadate2.Name = "bachadate2";
            this.bachadate2.ReadOnly = true;
            this.bachadate2.Visible = false;
            this.bachadate2.Width = 78;
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
            // car_step
            // 
            this.car_step.DataPropertyName = "car_step";
            this.car_step.HeaderText = "car_step";
            this.car_step.Name = "car_step";
            this.car_step.ReadOnly = true;
            this.car_step.Visible = false;
            this.car_step.Width = 78;
            // 
            // car_sno
            // 
            this.car_sno.DataPropertyName = "car_sno";
            this.car_sno.HeaderText = "car_sno";
            this.car_sno.Name = "car_sno";
            this.car_sno.ReadOnly = true;
            this.car_sno.Visible = false;
            this.car_sno.Width = 75;
            // 
            // print_step
            // 
            this.print_step.DataPropertyName = "print_step";
            this.print_step.HeaderText = "print_step";
            this.print_step.Name = "print_step";
            this.print_step.ReadOnly = true;
            this.print_step.Width = 84;
            // 
            // bigo
            // 
            this.bigo.DataPropertyName = "bigo";
            this.bigo.HeaderText = "bigo";
            this.bigo.Name = "bigo";
            this.bigo.ReadOnly = true;
            this.bigo.Width = 54;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.LightYellow;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(15, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(210, 23);
            this.label13.TabIndex = 221;
            this.label13.Text = "기타출고 상차 작업";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.LightYellow;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(17, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(213, 23);
            this.label14.TabIndex = 220;
            this.label14.Text = "기타출고 상차 List";
            // 
            // FrmTawmtoLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 726);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTawmtoLoad";
            this.Text = "FrmTawmtoLoad";
            this.Load += new System.EventHandler(this.FrmTawmtoLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn car_dest;
        private System.Windows.Forms.DataGridView dataGridView1;
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnsel;
        private System.Windows.Forms.Button btncmmt;
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btncncl;
        private System.Windows.Forms.Button btnchgqty;
        private System.Windows.Forms.Button btnfinish;
        private System.Windows.Forms.Button btndeliverydone;
        private System.Windows.Forms.Button btnexcel;
        private System.Windows.Forms.Button btnChg;
        private System.Windows.Forms.Button btnqury;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn docnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tapos;
        private System.Windows.Forms.DataGridViewTextBoxColumn bwlvs;
        private System.Windows.Forms.DataGridViewTextBoxColumn matnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn maktx;
        private System.Windows.Forms.DataGridViewTextBoxColumn lgort;
        private System.Windows.Forms.DataGridViewTextBoxColumn charg;
        private System.Windows.Forms.DataGridViewTextBoxColumn bestq;
        private System.Windows.Forms.DataGridViewTextBoxColumn pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn vsolm;
        private System.Windows.Forms.DataGridViewTextBoxColumn wdatu;
        private System.Windows.Forms.DataGridViewTextBoxColumn wenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn vltyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn nltyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn trart;
        private System.Windows.Forms.DataGridViewTextBoxColumn bname;
        private System.Windows.Forms.DataGridViewTextBoxColumn vfdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordi_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn lsonr;
        private System.Windows.Forms.DataGridViewTextBoxColumn sobkz;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordi_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn bachadate2;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no2;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_step;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn print_step;
        private System.Windows.Forms.DataGridViewTextBoxColumn bigo;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}