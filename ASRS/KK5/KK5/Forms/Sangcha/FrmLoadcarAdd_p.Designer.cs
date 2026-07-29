namespace KK5
{
    partial class FrmLoadcarAdd_p
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.car_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.load_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(741, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(663, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "선택";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.car_no,
            this.car_desc,
            this.car_man,
            this.load_vol,
            this.max_vol,
            this.priority,
            this.area_code,
            this.remark});
            this.dataGridView1.Location = new System.Drawing.Point(9, 32);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 343);
            this.dataGridView1.TabIndex = 13;
            // 
            // car_no
            // 
            this.car_no.DataPropertyName = "car_no";
            this.car_no.HeaderText = "차량번호";
            this.car_no.Name = "car_no";
            this.car_no.ReadOnly = true;
            this.car_no.Width = 78;
            // 
            // car_desc
            // 
            this.car_desc.DataPropertyName = "car_desc";
            this.car_desc.HeaderText = "차량명";
            this.car_desc.Name = "car_desc";
            this.car_desc.ReadOnly = true;
            this.car_desc.Width = 66;
            // 
            // car_man
            // 
            this.car_man.DataPropertyName = "car_man";
            this.car_man.HeaderText = "차량기사";
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
            this.max_vol.HeaderText = "최대적재용량";
            this.max_vol.Name = "max_vol";
            this.max_vol.ReadOnly = true;
            this.max_vol.Width = 102;
            // 
            // priority
            // 
            this.priority.DataPropertyName = "priority";
            this.priority.HeaderText = "우선순위";
            this.priority.Name = "priority";
            this.priority.ReadOnly = true;
            this.priority.Width = 78;
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
            this.remark.HeaderText = "Remark";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 73;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "차량선택(누적추가)";
            // 
            // FrmLoadcarAdd_p
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 387);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "FrmLoadcarAdd_p";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "기존 차량 선택(누적추가)";
            this.Load += new System.EventHandler(this.FrmLoadcarAdd_p_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn load_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn priority;
        private System.Windows.Forms.DataGridViewTextBoxColumn area_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.Label label1;
    }
}