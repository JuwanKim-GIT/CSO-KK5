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
    public partial class FrmPltiMoveToYloc_p : Form
    {
        public FrmPltiMoveToYloc_p()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ls = maskedTextBox1.Text;
            if (ls == "000000")
            {
                MessageBox.Show("야적위치를 제대로 입력바람...!");
                return;
            }
                
            if (ls.Trim().Length != 6)
            {
                MessageBox.Show("야적위치를 제대로 입력바람...! 6자리");
                return;
            }            
                
            DialogResult = DialogResult.OK;
        }
    }
}
