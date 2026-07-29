namespace KK5
{
    partial class FrmMiinpe
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblltqty = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbbatch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtpdesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtprod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnqry = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnpksz = new System.Windows.Forms.Button();
            this.btndel = new System.Windows.Forms.Button();
            this.btnreceipt = new System.Windows.Forms.Button();
            this.credt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cretim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tapos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwlvs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maktx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ltqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vsolm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sobkz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lsonr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vltyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nltyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vfdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 153;
            this.label2.Text = "입고유형";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Info;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "101:Good Receipts",
            "202:Reverse Gi",
            "521:GR for Sales Stock(Tinting)",
            "302:Other Goods Receipts-Plant",
            "312:Other Goods Receipts",
            "651:Customer Return GR",
            "552:Reverse GI for Scrap",
            "256:GR for Consignment Pick-Up",
            "ALL"});
            this.comboBox1.Location = new System.Drawing.Point(454, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(329, 20);
            this.comboBox1.TabIndex = 152;
            // 
            // chkdt
            // 
            this.chkdt.AutoSize = true;
            this.chkdt.Location = new System.Drawing.Point(340, 19);
            this.chkdt.Name = "chkdt";
            this.chkdt.Size = new System.Drawing.Size(15, 14);
            this.chkdt.TabIndex = 149;
            this.chkdt.UseVisualStyleBackColor = true;
            this.chkdt.CheckedChanged += new System.EventHandler(this.chkdt_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(197, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 148;
            this.label10.Text = "~";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Enabled = false;
            this.dtDateTo.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(217, 16);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(112, 22);
            this.dtDateTo.TabIndex = 147;
            // 
            // dtDatefrom
            // 
            this.dtDatefrom.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatefrom.Location = new System.Drawing.Point(79, 16);
            this.dtDatefrom.Name = "dtDatefrom";
            this.dtDatefrom.Size = new System.Drawing.Size(116, 22);
            this.dtDatefrom.TabIndex = 146;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 145;
            this.label9.Text = "수신일자";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.credt,
            this.credat,
            this.cretim,
            this.docnum,
            this.tanum,
            this.tapos,
            this.bwlvs,
            this.matnr,
            this.maktx,
            this.charg,
            this.lgort,
            this.pksz,
            this.ltqty,
            this.vsolm,
            this.sqty,
            this.trart,
            this.sobkz,
            this.lsonr,
            this.wenum,
            this.vltyp,
            this.nltyp,
            this.vfdat});
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1237, 495);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 124);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1237, 539);
            this.panel3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Khaki;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblltqty);
            this.panel4.Controls.Add(this.lblqty);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 501);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1237, 38);
            this.panel4.TabIndex = 5;
            // 
            // lblltqty
            // 
            this.lblltqty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblltqty.BackColor = System.Drawing.Color.White;
            this.lblltqty.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblltqty.Location = new System.Drawing.Point(1096, 8);
            this.lblltqty.Name = "lblltqty";
            this.lblltqty.Size = new System.Drawing.Size(107, 23);
            this.lblltqty.TabIndex = 7;
            this.lblltqty.Text = "0";
            this.lblltqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblqty
            // 
            this.lblqty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblqty.BackColor = System.Drawing.Color.White;
            this.lblqty.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblqty.Location = new System.Drawing.Point(904, 8);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(91, 23);
            this.lblqty.TabIndex = 6;
            this.lblqty.Text = "0";
            this.lblqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1036, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "총량(LT)";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(854, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "수량";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.tbbatch);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtpdesc);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtprod);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbDoc);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.chkdt);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.dtDateTo);
            this.panel2.Controls.Add(this.dtDatefrom);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1237, 81);
            this.panel2.TabIndex = 6;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // tbbatch
            // 
            this.tbbatch.Location = new System.Drawing.Point(1101, 12);
            this.tbbatch.Name = "tbbatch";
            this.tbbatch.Size = new System.Drawing.Size(98, 21);
            this.tbbatch.TabIndex = 172;
            this.tbbatch.DoubleClick += new System.EventHandler(this.tbbatch_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1039, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 171;
            this.label8.Text = "Batch_no";
            // 
            // txtpdesc
            // 
            this.txtpdesc.Location = new System.Drawing.Point(887, 47);
            this.txtpdesc.Name = "txtpdesc";
            this.txtpdesc.Size = new System.Drawing.Size(312, 21);
            this.txtpdesc.TabIndex = 170;
            this.txtpdesc.DoubleClick += new System.EventHandler(this.txtpdesc_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(837, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 169;
            this.label5.Text = "제품명";
            // 
            // txtprod
            // 
            this.txtprod.Location = new System.Drawing.Point(454, 47);
            this.txtprod.Name = "txtprod";
            this.txtprod.Size = new System.Drawing.Size(114, 21);
            this.txtprod.TabIndex = 168;
            this.txtprod.DoubleClick += new System.EventHandler(this.txtprod_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 167;
            this.label3.Text = "제품코드";
            // 
            // tbDoc
            // 
            this.tbDoc.Location = new System.Drawing.Point(886, 14);
            this.tbDoc.Name = "tbDoc";
            this.tbDoc.Size = new System.Drawing.Size(138, 21);
            this.tbDoc.TabIndex = 164;
            this.tbDoc.DoubleClick += new System.EventHandler(this.tbDoc_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(828, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 12);
            this.label4.TabIndex = 163;
            this.label4.Text = "Docnum";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 663);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1237, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1150, 12);
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
            this.label1.Size = new System.Drawing.Size(160, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "ERP 입고 수신";
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(922, 12);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 2;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnpksz);
            this.panel1.Controls.Add(this.btndel);
            this.panel1.Controls.Add(this.btnreceipt);
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnqry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 43);
            this.panel1.TabIndex = 4;
            // 
            // btnpksz
            // 
            this.btnpksz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpksz.Location = new System.Drawing.Point(1074, 12);
            this.btnpksz.Name = "btnpksz";
            this.btnpksz.Size = new System.Drawing.Size(75, 23);
            this.btnpksz.TabIndex = 7;
            this.btnpksz.Text = "수정";
            this.btnpksz.UseVisualStyleBackColor = true;
            this.btnpksz.Click += new System.EventHandler(this.btnpksz_Click);
            // 
            // btndel
            // 
            this.btndel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndel.Location = new System.Drawing.Point(581, 12);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(75, 23);
            this.btndel.TabIndex = 6;
            this.btndel.Text = "삭제";
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // btnreceipt
            // 
            this.btnreceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnreceipt.Location = new System.Drawing.Point(998, 12);
            this.btnreceipt.Name = "btnreceipt";
            this.btnreceipt.Size = new System.Drawing.Size(75, 23);
            this.btnreceipt.TabIndex = 5;
            this.btnreceipt.Text = "납입확정";
            this.btnreceipt.UseVisualStyleBackColor = true;
            this.btnreceipt.Click += new System.EventHandler(this.btnreceipt_Click);
            // 
            // credt
            // 
            this.credt.DataPropertyName = "credt";
            this.credt.HeaderText = "수신시간";
            this.credt.Name = "credt";
            this.credt.ReadOnly = true;
            this.credt.Width = 78;
            // 
            // credat
            // 
            this.credat.DataPropertyName = "credat";
            this.credat.HeaderText = "수신일자";
            this.credat.Name = "credat";
            this.credat.ReadOnly = true;
            this.credat.Visible = false;
            this.credat.Width = 78;
            // 
            // cretim
            // 
            this.cretim.DataPropertyName = "cretim";
            this.cretim.HeaderText = "수신시각";
            this.cretim.Name = "cretim";
            this.cretim.ReadOnly = true;
            this.cretim.Visible = false;
            this.cretim.Width = 78;
            // 
            // docnum
            // 
            this.docnum.DataPropertyName = "docnum";
            this.docnum.HeaderText = "Doc No";
            this.docnum.Name = "docnum";
            this.docnum.ReadOnly = true;
            this.docnum.Width = 72;
            // 
            // tanum
            // 
            this.tanum.DataPropertyName = "tanum";
            this.tanum.HeaderText = "Trans No";
            this.tanum.Name = "tanum";
            this.tanum.ReadOnly = true;
            this.tanum.Width = 83;
            // 
            // tapos
            // 
            this.tapos.DataPropertyName = "tapos";
            this.tapos.HeaderText = "Line No";
            this.tapos.Name = "tapos";
            this.tapos.ReadOnly = true;
            this.tapos.Width = 74;
            // 
            // bwlvs
            // 
            this.bwlvs.DataPropertyName = "bwlvs";
            this.bwlvs.HeaderText = "입고유형";
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
            // charg
            // 
            this.charg.DataPropertyName = "charg";
            this.charg.HeaderText = "Batch no";
            this.charg.Name = "charg";
            this.charg.ReadOnly = true;
            this.charg.Width = 80;
            // 
            // lgort
            // 
            this.lgort.DataPropertyName = "lgort";
            this.lgort.HeaderText = "Loc";
            this.lgort.Name = "lgort";
            this.lgort.ReadOnly = true;
            this.lgort.Width = 51;
            // 
            // pksz
            // 
            this.pksz.DataPropertyName = "pksz";
            this.pksz.HeaderText = "내용량";
            this.pksz.Name = "pksz";
            this.pksz.Width = 66;
            // 
            // ltqty
            // 
            this.ltqty.DataPropertyName = "ltqty";
            this.ltqty.HeaderText = "요청량(LT)";
            this.ltqty.Name = "ltqty";
            this.ltqty.ReadOnly = true;
            this.ltqty.Width = 91;
            // 
            // vsolm
            // 
            this.vsolm.DataPropertyName = "vsolm";
            this.vsolm.HeaderText = "요청수량";
            this.vsolm.Name = "vsolm";
            this.vsolm.ReadOnly = true;
            this.vsolm.Width = 78;
            // 
            // sqty
            // 
            this.sqty.DataPropertyName = "sqty";
            dataGridViewCellStyle2.Format = "N0";
            this.sqty.DefaultCellStyle = dataGridViewCellStyle2;
            this.sqty.HeaderText = "선택수량";
            this.sqty.Name = "sqty";
            this.sqty.Width = 78;
            // 
            // trart
            // 
            this.trart.DataPropertyName = "trart";
            this.trart.HeaderText = "ship구분";
            this.trart.Name = "trart";
            this.trart.ReadOnly = true;
            this.trart.Width = 78;
            // 
            // sobkz
            // 
            this.sobkz.DataPropertyName = "sobkz";
            this.sobkz.HeaderText = "SO구분";
            this.sobkz.Name = "sobkz";
            this.sobkz.ReadOnly = true;
            this.sobkz.Width = 71;
            // 
            // lsonr
            // 
            this.lsonr.DataPropertyName = "lsonr";
            this.lsonr.HeaderText = "SO No";
            this.lsonr.Name = "lsonr";
            this.lsonr.ReadOnly = true;
            this.lsonr.Width = 67;
            // 
            // wenum
            // 
            this.wenum.DataPropertyName = "wenum";
            this.wenum.HeaderText = "입고번호";
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
            this.nltyp.HeaderText = "Target";
            this.nltyp.Name = "nltyp";
            this.nltyp.ReadOnly = true;
            this.nltyp.Width = 66;
            // 
            // vfdat
            // 
            this.vfdat.DataPropertyName = "vfdat";
            this.vfdat.HeaderText = "유효기간";
            this.vfdat.Name = "vfdat";
            this.vfdat.ReadOnly = true;
            this.vfdat.Width = 78;
            // 
            // FrmMiinpe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 685);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMiinpe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ERP 입고 수신";
            this.Load += new System.EventHandler(this.FrmMiinpe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnqry;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnreceipt;
        private System.Windows.Forms.TextBox tbDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtpdesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtprod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.Button btnpksz;
        private System.Windows.Forms.TextBox tbbatch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblltqty;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn credt;
        private System.Windows.Forms.DataGridViewTextBoxColumn credat;
        private System.Windows.Forms.DataGridViewTextBoxColumn cretim;
        private System.Windows.Forms.DataGridViewTextBoxColumn docnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tanum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tapos;
        private System.Windows.Forms.DataGridViewTextBoxColumn bwlvs;
        private System.Windows.Forms.DataGridViewTextBoxColumn matnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn maktx;
        private System.Windows.Forms.DataGridViewTextBoxColumn charg;
        private System.Windows.Forms.DataGridViewTextBoxColumn lgort;
        private System.Windows.Forms.DataGridViewTextBoxColumn pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn ltqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn vsolm;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn trart;
        private System.Windows.Forms.DataGridViewTextBoxColumn sobkz;
        private System.Windows.Forms.DataGridViewTextBoxColumn lsonr;
        private System.Windows.Forms.DataGridViewTextBoxColumn wenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn vltyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn nltyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn vfdat;
    }
}