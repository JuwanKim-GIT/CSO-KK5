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
    public partial class FrmLoadCarGetQty_p : Form
    {
        decimal qty = 0;
        public FrmLoadCarGetQty_p(decimal qty)
        {
            this.qty = qty;
            InitializeComponent();

            this.qty = qty;
            numericTextox1.Text = qty.ToString("0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FromLoadCarGetQty_p_Load(object sender, EventArgs e)
        {          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal sqty = numericTextox2.Value;
            if (sqty > qty)
            {
                MessageBox.Show("선택수량이 너무 큽니다");
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
