namespace KK5
{
    partial class FrmNewCarSel_p
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.car_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "신규차량선택";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.car_no,
            this.car_desc,
            this.car_man,
            this.max_vol,
            this.priority,
            this.area_code,
            this.remark});
            this.dataGridView1.Location = new System.Drawing.Point(6, 35);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(812, 343);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
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
            // 
            // car_man
            // 
            this.car_man.DataPropertyName = "car_man";
            this.car_man.HeaderText = "차량기사";
            this.car_man.Name = "car_man";
            this.car_man.ReadOnly = true;
            // 
            // max_vol
            // 
            this.max_vol.DataPropertyName = "max_vol";
            this.max_vol.HeaderText = "최대적재용량";
            this.max_vol.Name = "max_vol";
            this.max_vol.ReadOnly = true;
            // 
            // priority
            // 
            this.priority.DataPropertyName = "priority";
            this.priority.HeaderText = "우선순위";
            this.priority.Name = "priority";
            this.priority.ReadOnly = true;
            // 
            // area_code
            // 
            this.area_code.DataPropertyName = "area_code";
            this.area_code.HeaderText = "지역";
            this.area_code.Name = "area_code";
            this.area_code.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "Remark";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "선택";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(743, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmNewCarSel_p
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 390);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "FrmNewCarSel_p";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "신규차량선택";
            this.Load += new System.EventHandler(this.FrmNewCarSel_p_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn priority;
        private System.Windows.Forms.DataGridViewTextBoxColumn area_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}