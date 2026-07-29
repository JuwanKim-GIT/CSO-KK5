using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCP
{
    public partial class FrmSCUse_p : Form
    {
        string hogi;
        public FrmSCUse_p(string hogi)
        {
            InitializeComponent();
            this.hogi = hogi;
        }

        private void FrmSCUse_p_Load(object sender, EventArgs e)
        {
            label1.Text = hogi;
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(@"select scrc_iuse, scrc_ouse from tbscrc where scrc_no = '" + hogi + "'").SingleOrDefault();
                if (q.scrc_iuse == "0") radioButton1.Checked = true; else radioButton2.Checked = true;
                if (q.scrc_ouse == "0") radioButton3.Checked = true; else radioButton4.Checked = true;
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
