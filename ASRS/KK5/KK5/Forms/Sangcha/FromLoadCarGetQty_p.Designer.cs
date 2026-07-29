namespace KK5
{
    partial class FrmLoadCarGetQty_p
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
            this.numericTextox1 = new NumericTextBox.NumericTextox();
            this.numericTextox2 = new NumericTextBox.NumericTextox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "출하량";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "선택량";
            // 
            // numericTextox1
            // 
            this.numericTextox1.InvalidSound = NumericTextBox.NumericTextox.InvalidSoundEnum.None;
            this.numericTextox1.Location = new System.Drawing.Point(98, 44);
            this.numericTextox1.MaxValue = ((long)(9223372036854775807));
            this.numericTextox1.Name = "numericTextox1";
            this.numericTextox1.ReadOnly = true;
            this.numericTextox1.SepratedChar = ',';
            this.numericTextox1.Size = new System.Drawing.Size(100, 21);
            this.numericTextox1.TabIndex = 2;
            // 
            // numericTextox2
            // 
            this.numericTextox2.InvalidSound = NumericTextBox.NumericTextox.InvalidSoundEnum.None;
            this.numericTextox2.Location = new System.Drawing.Point(98, 85);
            this.numericTextox2.MaxLength = 5;
            this.numericTextox2.MaxValue = ((long)(100000));
            this.numericTextox2.Name = "numericTextox2";
            this.numericTextox2.SepratedChar = ',';
            this.numericTextox2.Size = new System.Drawing.Size(100, 21);
            this.numericTextox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 142);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmLoadCarGetQty_p
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 210);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericTextox2);
            this.Controls.Add(this.numericTextox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLoadCarGetQty_p";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "수량 선택";
            this.Load += new System.EventHandler(this.FromLoadCarGetQty_p_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NumericTextBox.NumericTextox numericTextox1;
        public NumericTextBox.NumericTextox numericTextox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}