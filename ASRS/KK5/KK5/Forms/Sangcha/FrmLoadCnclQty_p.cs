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
    public partial class FrmLoadCnclQty_p : Form
    {
        public FrmLoadCnclQty_p(int qty)
        {
            InitializeComponent();
            textBox1.Text = qty.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            long qty = Convert.ToInt32(textBox1.Text);
            if (numericTextox1.Text == "") return;

            long qty2 = numericTextox1.Value;
            if (qty2 >= qty)
            {
                MessageBox.Show("조정수량이 너무 크거나 같읍니다....!");
                return;
            }
            DialogResult = DialogResult.OK;

        }

        private void FrmLoadCnclQty_p_Load(object sender, EventArgs e)
        {

        }
    }
}
