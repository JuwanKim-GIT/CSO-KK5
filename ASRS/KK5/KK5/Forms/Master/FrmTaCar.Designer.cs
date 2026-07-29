namespace KK5
{
    partial class FrmTaCar
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
            this.btninsert = new System.Windows.Forms.Button();
            this.btnedit = new System.Windows.Forms.Button();
            this.btndel = new System.Windows.Forms.Button();
            this.btnqry = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbcar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.car_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_dest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.load_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.load_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uuse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parcel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnexit.Location = new System.Drawing.Point(1063, 7);
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
            this.label1.Size = new System.Drawing.Size(128, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "차량 정보";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btninsert);
            this.panel2.Controls.Add(this.btnedit);
            this.panel2.Controls.Add(this.btndel);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqry);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1150, 39);
            this.panel2.TabIndex = 0;
            // 
            // btninsert
            // 
            this.btninsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btninsert.Location = new System.Drawing.Point(832, 7);
            this.btninsert.Name = "btninsert";
            this.btninsert.Size = new System.Drawing.Size(75, 23);
            this.btninsert.TabIndex = 9;
            this.btninsert.Text = "등록";
            this.btninsert.UseVisualStyleBackColor = true;
            this.btninsert.Click += new System.EventHandler(this.btninsert_Click);
            // 
            // btnedit
            // 
            this.btnedit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnedit.Location = new System.Drawing.Point(909, 7);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(75, 23);
            this.btnedit.TabIndex = 8;
            this.btnedit.Text = "수정";
            this.btnedit.UseVisualStyleBackColor = true;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // btndel
            // 
            this.btndel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndel.Location = new System.Drawing.Point(986, 7);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(75, 23);
            this.btndel.TabIndex = 7;
            this.btndel.Text = "삭제";
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(755, 7);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 5;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1150, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tbcar);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1150, 42);
            this.panel3.TabIndex = 1;
            // 
            // tbcar
            // 
            this.tbcar.Location = new System.Drawing.Point(90, 8);
            this.tbcar.MaxLength = 4;
            this.tbcar.Name = "tbcar";
            this.tbcar.Size = new System.Drawing.Size(73, 21);
            this.tbcar.TabIndex = 133;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 132;
            this.label3.Text = "차량번호";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.car_no,
            this.car_desc,
            this.car_man,
            this.car_dest,
            this.max_vol,
            this.load_vol,
            this.load_qty,
            this.step,
            this.uuse,
            this.area_code,
            this.parcel,
            this.Remark});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1150, 547);
            this.dataGridView1.TabIndex = 3;
            // 
            // car_no
            // 
            this.car_no.DataPropertyName = "car_no";
            this.car_no.HeaderText = "차량번호";
            this.car_no.Name = "car_no";
            this.car_no.ReadOnly = true;
            // 
            // car_desc
            // 
            this.car_desc.DataPropertyName = "car_desc";
            this.car_desc.HeaderText = "차량명";
            this.car_desc.Name = "car_desc";
            this.car_desc.ReadOnly = true;
            this.car_desc.Width = 120;
            // 
            // car_man
            // 
            this.car_man.DataPropertyName = "car_man";
            this.car_man.HeaderText = "운전기사";
            this.car_man.Name = "car_man";
            this.car_man.ReadOnly = true;
            // 
            // car_dest
            // 
            this.car_dest.DataPropertyName = "car_dest";
            this.car_dest.HeaderText = "도착지";
            this.car_dest.Name = "car_dest";
            this.car_dest.ReadOnly = true;
            // 
            // max_vol
            // 
            this.max_vol.DataPropertyName = "max_vol";
            this.max_vol.HeaderText = "Max용량";
            this.max_vol.Name = "max_vol";
            this.max_vol.ReadOnly = true;
            // 
            // load_vol
            // 
            this.load_vol.DataPropertyName = "load_vol";
            this.load_vol.HeaderText = "적재량";
            this.load_vol.Name = "load_vol";
            this.load_vol.ReadOnly = true;
            // 
            // load_qty
            // 
            this.load_qty.DataPropertyName = "load_qty";
            this.load_qty.HeaderText = "적재수량";
            this.load_qty.Name = "load_qty";
            this.load_qty.ReadOnly = true;
            // 
            // step
            // 
            this.step.DataPropertyName = "step";
            this.step.HeaderText = "진행STEP";
            this.step.Name = "step";
            this.step.ReadOnly = true;
            // 
            // uuse
            // 
            this.uuse.DataPropertyName = "uuse";
            this.uuse.HeaderText = "사용유무";
            this.uuse.Name = "uuse";
            this.uuse.ReadOnly = true;
            // 
            // area_code
            // 
            this.area_code.DataPropertyName = "area_code";
            this.area_code.HeaderText = "지역코드";
            this.area_code.Name = "area_code";
            this.area_code.ReadOnly = true;
            // 
            // parcel
            // 
            this.parcel.DataPropertyName = "parcel";
            this.parcel.HeaderText = "택배유무";
            this.parcel.Name = "parcel";
            this.parcel.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remark.DataPropertyName = "remark";
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
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
            this.panel1.Size = new System.Drawing.Size(1150, 650);
            this.panel1.TabIndex = 2;
            // 
            // FrmTaCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTaCar";
            this.Text = "차량 정보";
            this.Load += new System.EventHandler(this.FrmTaCar_Load);
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
        private System.Windows.Forms.TextBox tbcar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btninsert;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_dest;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn load_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn load_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn step;
        private System.Windows.Forms.DataGridViewTextBoxColumn uuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn area_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
    }
}