namespace KK5
{
    partial class FrmTacarInsert_p
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkparcel = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkuse = new System.Windows.Forms.CheckBox();
            this.numaxvol = new System.Windows.Forms.NumericUpDown();
            this.tbman = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbcd = new System.Windows.Forms.TextBox();
            this.tbarea = new System.Windows.Forms.TextBox();
            this.tbremark = new System.Windows.Forms.TextBox();
            this.tbdest = new System.Windows.Forms.TextBox();
            this.tbdesc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numaxvol)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "닫기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkparcel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.chkuse);
            this.panel1.Controls.Add(this.numaxvol);
            this.panel1.Controls.Add(this.tbman);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.tbcd);
            this.panel1.Controls.Add(this.tbarea);
            this.panel1.Controls.Add(this.tbremark);
            this.panel1.Controls.Add(this.tbdest);
            this.panel1.Controls.Add(this.tbdesc);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 312);
            this.panel1.TabIndex = 33;
            // 
            // chkparcel
            // 
            this.chkparcel.AutoSize = true;
            this.chkparcel.Location = new System.Drawing.Point(119, 269);
            this.chkparcel.Name = "chkparcel";
            this.chkparcel.Size = new System.Drawing.Size(15, 14);
            this.chkparcel.TabIndex = 40;
            this.chkparcel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 67;
            this.label5.Text = "택배유무";
            // 
            // chkuse
            // 
            this.chkuse.AutoSize = true;
            this.chkuse.Checked = true;
            this.chkuse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkuse.Location = new System.Drawing.Point(119, 214);
            this.chkuse.Name = "chkuse";
            this.chkuse.Size = new System.Drawing.Size(15, 14);
            this.chkuse.TabIndex = 30;
            this.chkuse.UseVisualStyleBackColor = true;
            // 
            // numaxvol
            // 
            this.numaxvol.DecimalPlaces = 2;
            this.numaxvol.Location = new System.Drawing.Point(118, 145);
            this.numaxvol.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numaxvol.Name = "numaxvol";
            this.numaxvol.Size = new System.Drawing.Size(95, 21);
            this.numaxvol.TabIndex = 20;
            this.numaxvol.ThousandsSeparator = true;
            // 
            // tbman
            // 
            this.tbman.Location = new System.Drawing.Point(118, 83);
            this.tbman.MaxLength = 20;
            this.tbman.Name = "tbman";
            this.tbman.Size = new System.Drawing.Size(73, 21);
            this.tbman.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 87);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 59;
            this.label15.Text = "운전기사";
            // 
            // tbcd
            // 
            this.tbcd.Location = new System.Drawing.Point(118, 25);
            this.tbcd.MaxLength = 4;
            this.tbcd.Name = "tbcd";
            this.tbcd.Size = new System.Drawing.Size(61, 21);
            this.tbcd.TabIndex = 1;
            // 
            // tbarea
            // 
            this.tbarea.Location = new System.Drawing.Point(119, 238);
            this.tbarea.MaxLength = 20;
            this.tbarea.Name = "tbarea";
            this.tbarea.Size = new System.Drawing.Size(103, 21);
            this.tbarea.TabIndex = 35;
            // 
            // tbremark
            // 
            this.tbremark.Location = new System.Drawing.Point(119, 181);
            this.tbremark.MaxLength = 100;
            this.tbremark.Name = "tbremark";
            this.tbremark.Size = new System.Drawing.Size(208, 21);
            this.tbremark.TabIndex = 25;
            // 
            // tbdest
            // 
            this.tbdest.Location = new System.Drawing.Point(118, 113);
            this.tbdest.MaxLength = 20;
            this.tbdest.Name = "tbdest";
            this.tbdest.Size = new System.Drawing.Size(73, 21);
            this.tbdest.TabIndex = 15;
            // 
            // tbdesc
            // 
            this.tbdesc.Location = new System.Drawing.Point(118, 54);
            this.tbdesc.MaxLength = 20;
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.Size = new System.Drawing.Size(219, 21);
            this.tbdesc.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 242);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 43;
            this.label13.Text = "지역코드";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "사용여부";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "Remark";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "Max적재용량";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "도착지";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "차량명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "차량번호";
            // 
            // FrmTacarInsert_p
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 389);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTacarInsert_p";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "차량 등록 ";
            this.Load += new System.EventHandler(this.FrmTacarInsert_p_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numaxvol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbman;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbcd;
        private System.Windows.Forms.TextBox tbarea;
        private System.Windows.Forms.TextBox tbremark;
        private System.Windows.Forms.TextBox tbdest;
        private System.Windows.Forms.TextBox tbdesc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkparcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkuse;
        private System.Windows.Forms.NumericUpDown numaxvol;
    }
}