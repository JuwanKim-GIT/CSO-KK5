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
    public partial class FrmLoadCarCmmt_p : Form
    {
        
        public FrmLoadCarCmmt_p(string sdno, string rmrk, string parcel, string cmmt)
        {
            InitializeComponent();

            textBox1.Text = rmrk;
            if (parcel == "1") checkBox1.Checked = true;

            richTextBox1.Text = cmmt;
            groupBox1.Text = "오더정보(" + sdno + ")";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 250)
            {
                MessageBox.Show("Remark가 너무 깁니다.");
                return;
            }

            if (richTextBox1.Text.Length > 250)
            {
                MessageBox.Show("Comment가 너무 깁니다.");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void FrmLoadCarCmmt_p_Load(object sender, EventArgs e)
        {

        }
    }
}
