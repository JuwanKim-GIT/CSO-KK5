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
    public partial class FrmEtcLoadComment_p : Form
    {
        
        public FrmEtcLoadComment_p()
        {
            InitializeComponent();
        }
        public FrmEtcLoadComment_p(string docnum, string comment)
        {
            InitializeComponent();
            textBox1.Text = comment;
            label1.Text = docnum + " IDOC Internal comment";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FrmEtcLoadComment_p_Load(object sender, EventArgs e)
        {

        }
    }
}
