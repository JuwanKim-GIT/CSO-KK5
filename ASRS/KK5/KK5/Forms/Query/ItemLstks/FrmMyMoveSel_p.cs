using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK5
{
    public partial class FrmMyMoveSel_p : Form
    {
        public FrmMyMoveSel_p()
        {
            InitializeComponent();
        }
        public FrmMyMoveSel_p(string qty)
        {
            InitializeComponent();
            numericUpDown1.Value = Convert.ToDecimal(qty);
        }

        public string loca = "000000";
        public string qty = "0";
        private void FrmMyMoveSel_p_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loca = maskedTextBox1.Text;
            if (!checkBox1.Checked) qty = numericUpDown1.Value.ToString();
            else qty = "ALL";

            DialogResult = DialogResult.OK;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = !checkBox1.Checked;
        }
    }
}
