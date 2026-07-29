namespace KK5
{
    partial class FrmLstkCelinfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gubun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RowSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnqry = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnExcel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 433);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1026, 388);
            this.panel3.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Beige;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gubun,
            this.Bank1,
            this.Bank2,
            this.Bank3,
            this.Bank4,
            this.Bank5,
            this.Bank6,
            this.Bank7,
            this.Bank8,
            this.Bank9,
            this.Bank10,
            this.RowSum});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1026, 378);
            this.dataGridView1.TabIndex = 1;
            // 
            // gubun
            // 
            this.gubun.DataPropertyName = "gubun";
            this.gubun.HeaderText = "구분";
            this.gubun.Name = "gubun";
            this.gubun.ReadOnly = true;
            // 
            // Bank1
            // 
            this.Bank1.DataPropertyName = "Bank1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Bank1.HeaderText = "Bank1";
            this.Bank1.Name = "Bank1";
            this.Bank1.ReadOnly = true;
            this.Bank1.Width = 80;
            // 
            // Bank2
            // 
            this.Bank2.DataPropertyName = "Bank2";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Bank2.HeaderText = "Bank2";
            this.Bank2.Name = "Bank2";
            this.Bank2.ReadOnly = true;
            this.Bank2.Width = 80;
            // 
            // Bank3
            // 
            this.Bank3.DataPropertyName = "Bank3";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Bank3.HeaderText = "Bank3";
            this.Bank3.Name = "Bank3";
            this.Bank3.ReadOnly = true;
            this.Bank3.Width = 80;
            // 
            // Bank4
            // 
            this.Bank4.DataPropertyName = "Bank4";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Bank4.HeaderText = "Bank4";
            this.Bank4.Name = "Bank4";
            this.Bank4.ReadOnly = true;
            this.Bank4.Width = 80;
            // 
            // Bank5
            // 
            this.Bank5.DataPropertyName = "Bank5";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank5.DefaultCellStyle = dataGridViewCellStyle5;
            this.Bank5.HeaderText = "Bank5";
            this.Bank5.Name = "Bank5";
            this.Bank5.ReadOnly = true;
            this.Bank5.Width = 80;
            // 
            // Bank6
            // 
            this.Bank6.DataPropertyName = "Bank6";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank6.DefaultCellStyle = dataGridViewCellStyle6;
            this.Bank6.HeaderText = "Bank6";
            this.Bank6.Name = "Bank6";
            this.Bank6.ReadOnly = true;
            this.Bank6.Width = 80;
            // 
            // Bank7
            // 
            this.Bank7.DataPropertyName = "Bank7";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank7.DefaultCellStyle = dataGridViewCellStyle7;
            this.Bank7.HeaderText = "Bank7";
            this.Bank7.Name = "Bank7";
            this.Bank7.ReadOnly = true;
            this.Bank7.Width = 80;
            // 
            // Bank8
            // 
            this.Bank8.DataPropertyName = "Bank8";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank8.DefaultCellStyle = dataGridViewCellStyle8;
            this.Bank8.HeaderText = "Bank8";
            this.Bank8.Name = "Bank8";
            this.Bank8.ReadOnly = true;
            this.Bank8.Width = 80;
            // 
            // Bank9
            // 
            this.Bank9.DataPropertyName = "Bank9";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank9.DefaultCellStyle = dataGridViewCellStyle9;
            this.Bank9.HeaderText = "Bank9";
            this.Bank9.Name = "Bank9";
            this.Bank9.ReadOnly = true;
            this.Bank9.Width = 80;
            // 
            // Bank10
            // 
            this.Bank10.DataPropertyName = "Bank10";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bank10.DefaultCellStyle = dataGridViewCellStyle10;
            this.Bank10.HeaderText = "Bank10";
            this.Bank10.Name = "Bank10";
            this.Bank10.ReadOnly = true;
            this.Bank10.Width = 80;
            // 
            // RowSum
            // 
            this.RowSum.DataPropertyName = "RowSum";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RowSum.DefaultCellStyle = dataGridViewCellStyle11;
            this.RowSum.HeaderText = "합계";
            this.RowSum.Name = "RowSum";
            this.RowSum.ReadOnly = true;
            this.RowSum.Width = 80;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1026, 10);
            this.panel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExcel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.btnqry);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 45);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("GulimChe", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Cell 종합정보";
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(939, 11);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 12;
            this.btnexit.Text = "닫기";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnqry
            // 
            this.btnqry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnqry.Location = new System.Drawing.Point(788, 11);
            this.btnqry.Name = "btnqry";
            this.btnqry.Size = new System.Drawing.Size(75, 23);
            this.btnqry.TabIndex = 11;
            this.btnqry.Text = "조회";
            this.btnqry.UseVisualStyleBackColor = true;
            this.btnqry.Click += new System.EventHandler(this.btnqry_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1026, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(864, 11);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 14;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // FrmLstkCelinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 433);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLstkCelinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "자동창고Cell 종합정보";
            this.Load += new System.EventHandler(this.FrmLstkCelinfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnqry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gubun;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank10;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowSum;
        private System.Windows.Forms.Button btnExcel;
    }
}