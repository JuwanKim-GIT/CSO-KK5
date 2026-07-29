namespace KK5
{
    partial class FrmMastSel_p
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mast_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_grp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mast_old = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.mast_desc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnsel = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnqry = new System.Windows.Forms.Button();
            this.tbdesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbmast = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.mast_canqty,
            this.mast_desc1});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 42);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(876, 523);
            this.dataGridView1.TabIndex = 5;
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
            // mast_desc1
            // 
            this.mast_desc1.DataPropertyName = "mast_desc1";
            this.mast_desc1.HeaderText = "구제품명";
            this.mast_desc1.Name = "mast_desc1";
            this.mast_desc1.ReadOnly = true;
            this.mast_desc1.Width = 78;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.btnsel);
            this.panel3.Controls.Add(this.btnexit);
            this.panel3.Controls.Add(this.btnqry);
            this.panel3.Controls.Add(this.tbdesc);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.tbmast);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(876, 42);
            this.panel3.TabIndex = 4;
            // 
            // btnsel
            // 
            this.btnsel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsel.Location = new System.Drawing.Point(712, 8);
            this.btnsel.Name = "btnsel";
            this.btnsel.Size = new System.Drawing.Size(75, 23);
            this.btnsel.TabIndex = 138;
            this.btnsel.Text = "선택";
            this.btnsel.UseVisualStyleBackColor = true;
            this.btnsel.Click += new System.EventHandler(this.btnsel_Click);
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(787, 8);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 137;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(636, 8);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 136;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // tbdesc
            // 
            this.tbdesc.Location = new System.Drawing.Point(276, 9);
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.Size = new System.Drawing.Size(295, 21);
            this.tbdesc.TabIndex = 135;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 134;
            this.label2.Text = "제품명";
            // 
            // tbmast
            // 
            this.tbmast.Location = new System.Drawing.Point(90, 9);
            this.tbmast.Name = "tbmast";
            this.tbmast.Size = new System.Drawing.Size(102, 21);
            this.tbmast.TabIndex = 133;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 132;
            this.label3.Text = "제품코드";
            // 
            // FrmMastSel_p
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 565);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Name = "FrmMastSel_p";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Material Code";
            this.Load += new System.EventHandler(this.FrmMastSel_p_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_grp;
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_old;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn mast_desc1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbdesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbmast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnsel;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnqry;
        public System.Windows.Forms.DataGridView dataGridView1;
    }
}