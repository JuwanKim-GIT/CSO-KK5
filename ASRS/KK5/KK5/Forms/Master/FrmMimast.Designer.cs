namespace KK5
{
    partial class FrmMimast
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
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnmodify = new System.Windows.Forms.Button();
            this.btninsert = new System.Windows.Forms.Button();
            this.btnqry = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbdesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbmast = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mast_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_grp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_old = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_desc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_bunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_szdm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_gwgt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_nwgt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_wunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_vunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_canqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1031, 7);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 6;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("GulimChe", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "제품 정보";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.btndelete);
            this.panel2.Controls.Add(this.btnmodify);
            this.panel2.Controls.Add(this.btninsert);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqry);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1118, 39);
            this.panel2.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(955, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Excel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndelete.Location = new System.Drawing.Point(879, 7);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(75, 23);
            this.btndelete.TabIndex = 9;
            this.btndelete.Text = "삭제";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnmodify
            // 
            this.btnmodify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnmodify.Location = new System.Drawing.Point(803, 7);
            this.btnmodify.Name = "btnmodify";
            this.btnmodify.Size = new System.Drawing.Size(75, 23);
            this.btnmodify.TabIndex = 8;
            this.btnmodify.Text = "수정";
            this.btnmodify.UseVisualStyleBackColor = true;
            this.btnmodify.Click += new System.EventHandler(this.btnmodify_Click);
            // 
            // btninsert
            // 
            this.btninsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btninsert.Location = new System.Drawing.Point(727, 7);
            this.btninsert.Name = "btninsert";
            this.btninsert.Size = new System.Drawing.Size(75, 23);
            this.btninsert.TabIndex = 7;
            this.btninsert.Text = "등록";
            this.btninsert.UseVisualStyleBackColor = true;
            this.btninsert.Click += new System.EventHandler(this.btninsert_Click);
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(651, 7);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 5;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 667);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1118, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dateTimePicker2);
            this.panel3.Controls.Add(this.dateTimePicker1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.tbdesc);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.tbmast);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1118, 42);
            this.panel3.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(304, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 140;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 12);
            this.label5.TabIndex = 139;
            this.label5.Text = "~";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(194, 8);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(102, 21);
            this.dateTimePicker2.TabIndex = 138;
            this.dateTimePicker2.Value = new System.DateTime(2020, 2, 6, 0, 0, 0, 0);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(62, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(106, 21);
            this.dateTimePicker1.TabIndex = 137;
            this.dateTimePicker1.Value = new System.DateTime(2020, 2, 6, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 136;
            this.label4.Text = "일자";
            // 
            // tbdesc
            // 
            this.tbdesc.Location = new System.Drawing.Point(587, 8);
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.Size = new System.Drawing.Size(295, 21);
            this.tbdesc.TabIndex = 135;
            this.tbdesc.DoubleClick += new System.EventHandler(this.tbdesc_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(537, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 134;
            this.label2.Text = "제품명";
            // 
            // tbmast
            // 
            this.tbmast.Location = new System.Drawing.Point(401, 8);
            this.tbmast.Name = "tbmast";
            this.tbmast.Size = new System.Drawing.Size(102, 21);
            this.tbmast.TabIndex = 133;
            this.tbmast.DoubleClick += new System.EventHandler(this.tbmast_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 132;
            this.label3.Text = "제품코드";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mast_cd,
            this.mast_desc,
            this.mast_type,
            this.mast_grp,
            this.mast_old,
            this.mast_desc1,
            this.mast_bunit,
            this.mast_szdm,
            this.mast_gwgt,
            this.mast_nwgt,
            this.mast_wunit,
            this.mast_vol,
            this.mast_vunit,
            this.mast_date,
            this.mast_time,
            this.mast_flag,
            this.mast_canqty});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 81);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1118, 586);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // mast_cd
            // 
            this.mast_cd.DataPropertyName = "mast_cd";
            this.mast_cd.HeaderText = "제품코드";
            this.mast_cd.Name = "mast_cd";
            this.mast_cd.ReadOnly = true;
            this.mast_cd.Width = 78;
            // 
            // mast_desc
            // 
            this.mast_desc.DataPropertyName = "mast_desc";
            this.mast_desc.HeaderText = "제품명";
            this.mast_desc.Name = "mast_desc";
            this.mast_desc.ReadOnly = true;
            this.mast_desc.Width = 66;
            // 
            // mast_type
            // 
            this.mast_type.DataPropertyName = "mast_type";
            this.mast_type.HeaderText = "제품TYPE";
            this.mast_type.Name = "mast_type";
            this.mast_type.ReadOnly = true;
            this.mast_type.Width = 86;
            // 
            // mast_grp
            // 
            this.mast_grp.DataPropertyName = "mast_grp";
            this.mast_grp.HeaderText = "제품그룹";
            this.mast_grp.Name = "mast_grp";
            this.mast_grp.ReadOnly = true;
            this.mast_grp.Width = 78;
            // 
            // mast_old
            // 
            this.mast_old.DataPropertyName = "mast_old";
            this.mast_old.HeaderText = "구코드";
            this.mast_old.Name = "mast_old";
            this.mast_old.ReadOnly = true;
            this.mast_old.Width = 66;
            // 
            // mast_desc1
            // 
            this.mast_desc1.DataPropertyName = "mast_desc1";
            this.mast_desc1.HeaderText = "구제품명";
            this.mast_desc1.Name = "mast_desc1";
            this.mast_desc1.ReadOnly = true;
            this.mast_desc1.Width = 78;
            // 
            // mast_bunit
            // 
            this.mast_bunit.DataPropertyName = "mast_bunit";
            this.mast_bunit.HeaderText = "BaseUnit";
            this.mast_bunit.Name = "mast_bunit";
            this.mast_bunit.ReadOnly = true;
            this.mast_bunit.Width = 80;
            // 
            // mast_szdm
            // 
            this.mast_szdm.DataPropertyName = "mast_szdm";
            this.mast_szdm.HeaderText = "SizeDim";
            this.mast_szdm.Name = "mast_szdm";
            this.mast_szdm.ReadOnly = true;
            this.mast_szdm.Width = 77;
            // 
            // mast_gwgt
            // 
            this.mast_gwgt.DataPropertyName = "mast_gwgt";
            this.mast_gwgt.HeaderText = "Gross중량";
            this.mast_gwgt.Name = "mast_gwgt";
            this.mast_gwgt.ReadOnly = true;
            this.mast_gwgt.Width = 88;
            // 
            // mast_nwgt
            // 
            this.mast_nwgt.DataPropertyName = "mast_nwgt";
            this.mast_nwgt.HeaderText = "Net중량";
            this.mast_nwgt.Name = "mast_nwgt";
            this.mast_nwgt.ReadOnly = true;
            this.mast_nwgt.Width = 73;
            // 
            // mast_wunit
            // 
            this.mast_wunit.DataPropertyName = "mast_wunit";
            this.mast_wunit.HeaderText = "무게단위";
            this.mast_wunit.Name = "mast_wunit";
            this.mast_wunit.ReadOnly = true;
            this.mast_wunit.Width = 78;
            // 
            // mast_vol
            // 
            this.mast_vol.DataPropertyName = "mast_vol";
            this.mast_vol.HeaderText = "내용량";
            this.mast_vol.Name = "mast_vol";
            this.mast_vol.ReadOnly = true;
            this.mast_vol.Width = 66;
            // 
            // mast_vunit
            // 
            this.mast_vunit.DataPropertyName = "mast_vunit";
            this.mast_vunit.HeaderText = "볼륨단위";
            this.mast_vunit.Name = "mast_vunit";
            this.mast_vunit.ReadOnly = true;
            this.mast_vunit.Width = 78;
            // 
            // mast_date
            // 
            this.mast_date.DataPropertyName = "mast_date";
            this.mast_date.HeaderText = "생성일자";
            this.mast_date.Name = "mast_date";
            this.mast_date.ReadOnly = true;
            this.mast_date.Width = 78;
            // 
            // mast_time
            // 
            this.mast_time.DataPropertyName = "mast_time";
            this.mast_time.HeaderText = "생성시각";
            this.mast_time.Name = "mast_time";
            this.mast_time.ReadOnly = true;
            this.mast_time.Width = 78;
            // 
            // mast_flag
            // 
            this.mast_flag.DataPropertyName = "mast_flag";
            this.mast_flag.HeaderText = "적재구분";
            this.mast_flag.Name = "mast_flag";
            this.mast_flag.ReadOnly = true;
            this.mast_flag.Width = 78;
            // 
            // mast_canqty
            // 
            this.mast_canqty.DataPropertyName = "mast_canqty";
            this.mast_canqty.HeaderText = "적재수량";
            this.mast_canqty.Name = "mast_canqty";
            this.mast_canqty.ReadOnly = true;
            this.mast_canqty.Width = 78;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 689);
            this.panel1.TabIndex = 2;
            // 
            // FrmMimast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 689);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMimast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "제품 정보";
            this.Load += new System.EventHandler(this.FrmMimast_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnqry;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbdesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbmast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnmodify;
        private System.Windows.Forms.Button btninsert;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_grp;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_old;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_desc1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_bunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_szdm;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_gwgt;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_nwgt;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_wunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_vunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_canqty;
    }
}