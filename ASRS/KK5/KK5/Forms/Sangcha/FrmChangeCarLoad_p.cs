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
    public partial class FrmChangeCarLoad_p : Form
    {
        public FrmChangeCarLoad_p(string car)
        {
            InitializeComponent();
            textBox1.Text = car;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") return;
            if (textBox1.Text.Trim() == textBox2.Text.Trim())
            {
                MessageBox.Show("차량이 동일합니다...!");
                return;
            }
                DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmChangeCarLoad_p_Load(object sender, EventArgs e)
        {

        }
    }
}
