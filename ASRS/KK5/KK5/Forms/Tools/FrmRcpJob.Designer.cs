namespace KK5
{
    partial class FrmRcpJob
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
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReassign = new System.Windows.Forms.Button();
            this.btnoutwrite = new System.Windows.Forms.Button();
            this.btninptwrite = new System.Windows.Forms.Button();
            this.btntry = new System.Windows.Forms.Button();
            this.btndone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnqury = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.indx_jno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_hogi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_gubn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_jio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_xmov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_fstn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_tstn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_pltn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_lstk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indx_sflg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plti_pltno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_bestq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_pksz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_stok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_rqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_idate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_itime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_lstk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plti_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.comboBox2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1091, 60);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 135;
            this.label3.Text = "호기";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ALL",
            "1호기",
            "2호기",
            "3호기",
            "4호기",
            "5호기"});
            this.comboBox2.Location = new System.Drawing.Point(264, 16);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 134;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "작업구분";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ALL",
            "1:공장입고",
            "2:메인입고",
            "3:출고",
            "4:이동"});
            this.comboBox1.Location = new System.Drawing.Point(83, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "  RCP JOB";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReassign);
            this.panel2.Controls.Add(this.btnoutwrite);
            this.panel2.Controls.Add(this.btninptwrite);
            this.panel2.Controls.Add(this.btntry);
            this.panel2.Controls.Add(this.btndone);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqury);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1091, 39);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnReassign
            // 
            this.btnReassign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReassign.Location = new System.Drawing.Point(559, 8);
            this.btnReassign.Name = "btnReassign";
            this.btnReassign.Size = new System.Drawing.Size(75, 23);
            this.btnReassign.TabIndex = 8;
            this.btnReassign.Text = "재할당";
            this.btnReassign.UseVisualStyleBackColor = true;
            this.btnReassign.Click += new System.EventHandler(this.btnReassign_Click);
            // 
            // btnoutwrite
            // 
            this.btnoutwrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnoutwrite.Location = new System.Drawing.Point(635, 8);
            this.btnoutwrite.Name = "btnoutwrite";
            this.btnoutwrite.Size = new System.Drawing.Size(75, 23);
            this.btnoutwrite.TabIndex = 7;
            this.btnoutwrite.Text = "출고대쓰기";
            this.btnoutwrite.UseVisualStyleBackColor = true;
            this.btnoutwrite.Click += new System.EventHandler(this.btnoutwrite_Click);
            // 
            // btninptwrite
            // 
            this.btninptwrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btninptwrite.Location = new System.Drawing.Point(711, 8);
            this.btninptwrite.Name = "btninptwrite";
            this.btninptwrite.Size = new System.Drawing.Size(75, 23);
            this.btninptwrite.TabIndex = 6;
            this.btninptwrite.Text = "입고대쓰기";
            this.btninptwrite.UseVisualStyleBackColor = true;
            this.btninptwrite.Click += new System.EventHandler(this.btninptwrite_Click);
            // 
            // btntry
            // 
            this.btntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntry.Location = new System.Drawing.Point(786, 8);
            this.btntry.Name = "btntry";
            this.btntry.Size = new System.Drawing.Size(75, 23);
            this.btntry.TabIndex = 5;
            this.btntry.Text = "재지시";
            this.btntry.UseVisualStyleBackColor = true;
            this.btntry.Click += new System.EventHandler(this.btntry_Click);
            // 
            // btndone
            // 
            this.btndone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndone.Location = new System.Drawing.Point(861, 8);
            this.btndone.Name = "btndone";
            this.btndone.Size = new System.Drawing.Size(75, 23);
            this.btndone.TabIndex = 4;
            this.btndone.Text = "완료";
            this.btndone.UseVisualStyleBackColor = true;
            this.btndone.Click += new System.EventHandler(this.btndone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(936, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1011, 8);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 2;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnqury
            // 
            this.btnqury.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqury.Location = new System.Drawing.Point(483, 8);
            this.btnqury.Name = "btnqury";
            this.btnqury.Size = new System.Drawing.Size(75, 23);
            this.btnqury.TabIndex = 1;
            this.btnqury.Text = "조회";
            this.btnqury.UseVisualStyleBackColor = true;
            this.btnqury.Click += new System.EventHandler(this.btnqury_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 833);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1091, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plti_pltno,
            this.plti_prod,
            this.plti_pdesc,
            this.plti_loc,
            this.plti_lot,
            this.plti_bestq,
            this.plti_pksz,
            this.plti_stok,
            this.plti_rqty,
            this.plti_idate,
            this.plti_itime,
            this.plti_remark,
            this.plti_lstk,
            this.plti_flag});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1091, 190);
            this.dataGridView2.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 544);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1091, 190);
            this.panel5.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 99);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1091, 734);
            this.panel4.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indx_jno,
            this.indx_hogi,
            this.indx_gubn,
            this.indx_jio,
            this.indx_stat,
            this.indx_xmov,
            this.indx_fstn,
            this.indx_tstn,
            this.indx_pltn,
            this.indx_lstk,
            this.indx_sflg});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1091, 544);
            this.dataGridView1.TabIndex = 3;
            // 
            // indx_jno
            // 
            this.indx_jno.DataPropertyName = "indx_jno";
            this.indx_jno.HeaderText = "작업순번";
            this.indx_jno.Name = "indx_jno";
            this.indx_jno.ReadOnly = true;
            this.indx_jno.Width = 150;
            // 
            // indx_hogi
            // 
            this.indx_hogi.DataPropertyName = "indx_hogi";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.indx_hogi.DefaultCellStyle = dataGridViewCellStyle1;
            this.indx_hogi.HeaderText = "    호기";
            this.indx_hogi.Name = "indx_hogi";
            this.indx_hogi.ReadOnly = true;
            this.indx_hogi.Width = 80;
            // 
            // indx_gubn
            // 
            this.indx_gubn.DataPropertyName = "indx_gubn";
            this.indx_gubn.HeaderText = "원격유무";
            this.indx_gubn.Name = "indx_gubn";
            this.indx_gubn.ReadOnly = true;
            // 
            // indx_jio
            // 
            this.indx_jio.DataPropertyName = "indx_jio";
            this.indx_jio.HeaderText = "작업종류";
            this.indx_jio.Name = "indx_jio";
            this.indx_jio.ReadOnly = true;
            // 
            // indx_stat
            // 
            this.indx_stat.DataPropertyName = "indx_stat";
            this.indx_stat.HeaderText = "상태";
            this.indx_stat.Name = "indx_stat";
            this.indx_stat.ReadOnly = true;
            // 
            // indx_xmov
            // 
            this.indx_xmov.DataPropertyName = "indx_xmov";
            this.indx_xmov.HeaderText = "INV작업";
            this.indx_xmov.Name = "indx_xmov";
            this.indx_xmov.ReadOnly = true;
            // 
            // indx_fstn
            // 
            this.indx_fstn.DataPropertyName = "indx_fstn";
            this.indx_fstn.HeaderText = "From";
            this.indx_fstn.Name = "indx_fstn";
            this.indx_fstn.ReadOnly = true;
            // 
            // indx_tstn
            // 
            this.indx_tstn.DataPropertyName = "indx_tstn";
            this.indx_tstn.HeaderText = "To";
            this.indx_tstn.Name = "indx_tstn";
            this.indx_tstn.ReadOnly = true;
            // 
            // indx_pltn
            // 
            this.indx_pltn.DataPropertyName = "indx_pltn";
            this.indx_pltn.HeaderText = "파렛번호";
            this.indx_pltn.Name = "indx_pltn";
            this.indx_pltn.ReadOnly = true;
            // 
            // indx_lstk
            // 
            this.indx_lstk.DataPropertyName = "indx_lstk";
            this.indx_lstk.HeaderText = "보관위치";
            this.indx_lstk.Name = "indx_lstk";
            this.indx_lstk.ReadOnly = true;
            // 
            // indx_sflg
            // 
            this.indx_sflg.DataPropertyName = "indx_sflg";
            this.indx_sflg.HeaderText = "indx_sflg";
            this.indx_sflg.Name = "indx_sflg";
            this.indx_sflg.ReadOnly = true;
            this.indx_sflg.Visible = false;
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
            this.panel1.Size = new System.Drawing.Size(1091, 855);
            this.panel1.TabIndex = 1;
            // 
            // plti_pltno
            // 
            this.plti_pltno.DataPropertyName = "plti_pltno";
            this.plti_pltno.HeaderText = "파렛번호";
            this.plti_pltno.Name = "plti_pltno";
            this.plti_pltno.Width = 78;
            // 
            // plti_prod
            // 
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
            // plti_loc
            // 
            this.plti_loc.DataPropertyName = "plti_loc";
            this.plti_loc.HeaderText = "Loc";
            this.plti_loc.Name = "plti_loc";
            this.plti_loc.ReadOnly = true;
            this.plti_loc.Width = 51;
            // 
            // plti_lot
            // 
            this.plti_lot.DataPropertyName = "plti_lot";
            this.plti_lot.HeaderText = "Batch No";
            this.plti_lot.Name = "plti_lot";
            this.plti_lot.ReadOnly = true;
            this.plti_lot.Width = 82;
            // 
            // plti_bestq
            // 
            this.plti_bestq.DataPropertyName = "plti_bestq";
            this.plti_bestq.HeaderText = "구분";
            this.plti_bestq.Name = "plti_bestq";
            this.plti_bestq.ReadOnly = true;
            this.plti_bestq.Width = 54;
            // 
            // plti_pksz
            // 
            this.plti_pksz.DataPropertyName = "plti_pksz";
            this.plti_pksz.HeaderText = "내용량";
            this.plti_pksz.Name = "plti_pksz";
            this.plti_pksz.ReadOnly = true;
            this.plti_pksz.Width = 66;
            // 
            // plti_stok
            // 
            this.plti_stok.DataPropertyName = "plti_stok";
            this.plti_stok.HeaderText = "재고";
            this.plti_stok.Name = "plti_stok";
            this.plti_stok.ReadOnly = true;
            this.plti_stok.Width = 54;
            // 
            // plti_rqty
            // 
            this.plti_rqty.DataPropertyName = "plti_rqty";
            this.plti_rqty.HeaderText = "예약량";
            this.plti_rqty.Name = "plti_rqty";
            this.plti_rqty.ReadOnly = true;
            this.plti_rqty.Width = 66;
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
            this.plti_itime.Width = 78;
            // 
            // plti_remark
            // 
            this.plti_remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.plti_remark.DataPropertyName = "plti_remark";
            this.plti_remark.HeaderText = "Remark";
            this.plti_remark.Name = "plti_remark";
            this.plti_remark.ReadOnly = true;
            // 
            // plti_lstk
            // 
            this.plti_lstk.DataPropertyName = "plti_lstk";
            this.plti_lstk.HeaderText = "보관위치";
            this.plti_lstk.Name = "plti_lstk";
            this.plti_lstk.ReadOnly = true;
            this.plti_lstk.Visible = false;
            this.plti_lstk.Width = 78;
            // 
            // plti_flag
            // 
            this.plti_flag.DataPropertyName = "plti_flag";
            this.plti_flag.HeaderText = "상태";
            this.plti_flag.Name = "plti_flag";
            this.plti_flag.ReadOnly = true;
            this.plti_flag.Visible = false;
            this.plti_flag.Width = 54;
            // 
            // FrmRcpJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 855);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRcpJob";
            this.Text = "RCP 작업";
            this.Load += new System.EventHandler(this.FrmRcpJob_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReassign;
        private System.Windows.Forms.Button btnoutwrite;
        private System.Windows.Forms.Button btninptwrite;
        private System.Windows.Forms.Button btntry;
        private System.Windows.Forms.Button btndone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnqury;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_jno;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_hogi;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_gubn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_jio;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_stat;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_xmov;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_fstn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_tstn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_pltn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_lstk;
        private System.Windows.Forms.DataGridViewTextBoxColumn indx_sflg;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pltno;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_bestq;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_pksz;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_stok;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_rqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_idate;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_itime;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_lstk;
        private System.Windows.Forms.DataGridViewTextBoxColumn plti_flag;
    }
}