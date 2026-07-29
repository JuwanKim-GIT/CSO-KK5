namespace KK5
{
    partial class FrmStackErorr
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
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnqry = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkdt = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDatefrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.erht_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_hogi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_ercd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_mesg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_gubn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_pltn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_lstk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erht_xmov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(1180, 7);
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
            this.label1.Size = new System.Drawing.Size(182, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "스택카 에러이력";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqry);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1267, 39);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1105, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(1030, 7);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 5;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 642);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1267, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Controls.Add(this.chkdt);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dtDateTo);
            this.panel3.Controls.Add(this.dtDatefrom);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1267, 42);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 133;
            this.label3.Text = "호기";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ALL",
            "1호기",
            "2호기",
            "3호기",
            "4호기",
            "5호기"});
            this.comboBox1.Location = new System.Drawing.Point(473, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 132;
            // 
            // chkdt
            // 
            this.chkdt.AutoSize = true;
            this.chkdt.Location = new System.Drawing.Point(334, 13);
            this.chkdt.Name = "chkdt";
            this.chkdt.Size = new System.Drawing.Size(15, 14);
            this.chkdt.TabIndex = 131;
            this.chkdt.UseVisualStyleBackColor = true;
            this.chkdt.CheckedChanged += new System.EventHandler(this.chkdt_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(191, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 130;
            this.label10.Text = "~";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Enabled = false;
            this.dtDateTo.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(211, 10);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(112, 22);
            this.dtDateTo.TabIndex = 129;
            // 
            // dtDatefrom
            // 
            this.dtDatefrom.CalendarFont = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtDatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatefrom.Location = new System.Drawing.Point(73, 10);
            this.dtDatefrom.Name = "dtDatefrom";
            this.dtDatefrom.Size = new System.Drawing.Size(116, 22);
            this.dtDatefrom.TabIndex = 128;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "발생일자";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.erht_dt,
            this.erht_date,
            this.erht_time,
            this.erht_hogi,
            this.erht_ercd,
            this.erht_mesg,
            this.erht_gubn,
            this.erht_pltn,
            this.erht_lstk,
            this.erht_pos,
            this.erht_xmov});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1267, 561);
            this.dataGridView1.TabIndex = 3;
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
            this.panel1.Size = new System.Drawing.Size(1267, 664);
            this.panel1.TabIndex = 1;
            // 
            // erht_dt
            // 
            this.erht_dt.DataPropertyName = "erht_dt";
            this.erht_dt.FillWeight = 150F;
            this.erht_dt.HeaderText = "발생시간";
            this.erht_dt.Name = "erht_dt";
            this.erht_dt.ReadOnly = true;
            this.erht_dt.Width = 78;
            // 
            // erht_date
            // 
            this.erht_date.DataPropertyName = "erht_date";
            this.erht_date.HeaderText = "발생일자";
            this.erht_date.Name = "erht_date";
            this.erht_date.ReadOnly = true;
            this.erht_date.Visible = false;
            this.erht_date.Width = 78;
            // 
            // erht_time
            // 
            this.erht_time.DataPropertyName = "erht_time";
            this.erht_time.HeaderText = "발생시각";
            this.erht_time.Name = "erht_time";
            this.erht_time.ReadOnly = true;
            this.erht_time.Visible = false;
            this.erht_time.Width = 78;
            // 
            // erht_hogi
            // 
            this.erht_hogi.DataPropertyName = "erht_hogi";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.erht_hogi.DefaultCellStyle = dataGridViewCellStyle1;
            this.erht_hogi.HeaderText = "호기";
            this.erht_hogi.Name = "erht_hogi";
            this.erht_hogi.ReadOnly = true;
            this.erht_hogi.Width = 54;
            // 
            // erht_ercd
            // 
            this.erht_ercd.DataPropertyName = "erht_ercd";
            this.erht_ercd.HeaderText = "에러코드";
            this.erht_ercd.Name = "erht_ercd";
            this.erht_ercd.ReadOnly = true;
            this.erht_ercd.Width = 78;
            // 
            // erht_mesg
            // 
            this.erht_mesg.DataPropertyName = "erht_mesg";
            this.erht_mesg.HeaderText = "에러메세지";
            this.erht_mesg.Name = "erht_mesg";
            this.erht_mesg.ReadOnly = true;
            this.erht_mesg.Width = 90;
            // 
            // erht_gubn
            // 
            this.erht_gubn.DataPropertyName = "erht_gubn";
            this.erht_gubn.HeaderText = "구분";
            this.erht_gubn.Name = "erht_gubn";
            this.erht_gubn.ReadOnly = true;
            this.erht_gubn.Width = 54;
            // 
            // erht_pltn
            // 
            this.erht_pltn.DataPropertyName = "erht_pltn";
            this.erht_pltn.HeaderText = "파렛번호";
            this.erht_pltn.Name = "erht_pltn";
            this.erht_pltn.ReadOnly = true;
            this.erht_pltn.Width = 78;
            // 
            // erht_lstk
            // 
            this.erht_lstk.DataPropertyName = "erht_lstk";
            this.erht_lstk.HeaderText = "보관위치";
            this.erht_lstk.Name = "erht_lstk";
            this.erht_lstk.ReadOnly = true;
            this.erht_lstk.Width = 78;
            // 
            // erht_pos
            // 
            this.erht_pos.DataPropertyName = "erht_pos";
            this.erht_pos.HeaderText = "에러위치";
            this.erht_pos.Name = "erht_pos";
            this.erht_pos.ReadOnly = true;
            this.erht_pos.Width = 78;
            // 
            // erht_xmov
            // 
            this.erht_xmov.DataPropertyName = "erht_xmov";
            this.erht_xmov.HeaderText = "작업종류";
            this.erht_xmov.Name = "erht_xmov";
            this.erht_xmov.ReadOnly = true;
            this.erht_xmov.Width = 78;
            // 
            // FrmStackErorr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 664);
            this.Controls.Add(this.panel1);
            this.Name = "FrmStackErorr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "스택카에러이력";
            this.Load += new System.EventHandler(this.FrmStackErorr_Load);
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
        private System.Windows.Forms.CheckBox chkdt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDatefrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_hogi;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_ercd;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_mesg;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_gubn;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_pltn;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_lstk;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn erht_xmov;
    }
}