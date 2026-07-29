namespace KK5
{
    partial class FrmAdvMiplti
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbprod = new System.Windows.Forms.TextBox();
            this.tbpdesc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tblot = new System.Windows.Forms.TextBox();
            this.tbremark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "LOC";
            // 
            // tbprod
            // 
            this.tbprod.Location = new System.Drawing.Point(96, 38);
            this.tbprod.MaxLength = 18;
            this.tbprod.Name = "tbprod";
            this.tbprod.Size = new System.Drawing.Size(134, 21);
            this.tbprod.TabIndex = 0;
            // 
            // tbpdesc
            // 
            this.tbpdesc.Location = new System.Drawing.Point(295, 38);
            this.tbpdesc.MaxLength = 18;
            this.tbpdesc.Name = "tbpdesc";
            this.tbpdesc.ReadOnly = true;
            this.tbpdesc.Size = new System.Drawing.Size(309, 21);
            this.tbpdesc.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0010:Fin Good AWS",
            "0035:Return Goods",
            "0050:Obsolete Stok",
            "0060:Rework",
            "0070:Damaged",
            "0080:Quarantined",
            "0090:Scap",
            "2000:Finished Goods",
            "SKUM",
            "SKUD",
            "SKUQ"});
            this.comboBox1.Location = new System.Drawing.Point(96, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Batch No";
            // 
            // tblot
            // 
            this.tblot.Location = new System.Drawing.Point(96, 109);
            this.tblot.MaxLength = 10;
            this.tblot.Name = "tblot";
            this.tblot.Size = new System.Drawing.Size(134, 21);
            this.tblot.TabIndex = 4;
            // 
            // tbremark
            // 
            this.tbremark.Location = new System.Drawing.Point(96, 188);
            this.tbremark.MaxLength = 40;
            this.tbremark.Name = "tbremark";
            this.tbremark.Size = new System.Drawing.Size(245, 21);
            this.tbremark.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Remark";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Qty";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(96, 150);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(134, 21);
            this.numericUpDown1.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Y: 야적",
            "F: 공장"});
            this.comboBox2.Location = new System.Drawing.Point(96, 231);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(134, 20);
            this.comboBox2.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Place";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(397, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "확인";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(543, 230);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "닫기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(269, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "status /S/Q";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "",
            "S",
            "Q"});
            this.comboBox3.Location = new System.Drawing.Point(355, 108);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 20);
            this.comboBox3.TabIndex = 17;
            // 
            // FrmAdvMiplti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 310);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbremark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tblot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbpdesc);
            this.Controls.Add(this.tbprod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmAdvMiplti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Stock entry";
            this.Load += new System.EventHandler(this.FrmAdvMiplti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbprod;
        private System.Windows.Forms.TextBox tbpdesc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tblot;
        private System.Windows.Forms.TextBox tbremark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox3;
    }
}