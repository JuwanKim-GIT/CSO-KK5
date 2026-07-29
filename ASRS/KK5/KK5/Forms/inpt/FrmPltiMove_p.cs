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
    public partial class FrmPltiMove_p : Form
    {
        public FrmPltiMove_p()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                if (maskedTextBox1.Text.Trim().Length != 6)
                {
                    MessageBox.Show("야적위치를 바르게 입력하세요");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
