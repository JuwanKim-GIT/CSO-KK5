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
    public partial class FrmRemark_p : Form
    {
        public FrmRemark_p()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int l = Encoding.Default.GetBytes(tbRemark.Text).Length;
            if (l > 40)
            {
                MessageBox.Show("문자열이 너무깁니다 Max 40 한글 20!");
            }           
          
            this.DialogResult = DialogResult.OK;
        }

        private void FrmRemark_p_Load(object sender, EventArgs e)
        {

        }
    }
}
