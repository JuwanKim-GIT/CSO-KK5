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
    public partial class FrmOutOption_p : Form
    {
        public FrmOutOption_p()
        {
            InitializeComponent();
        }

        private void FrmOutOption_p_Load(object sender, EventArgs e)
        {
            button1.Click += button1_Click;
            button2.Click += button2_Click;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string ls = db.ExecuteQuery<string>(@"select stat_out from tbstat where stat_key = '1' ").SingleOrDefault();
                if (ls == "0") radioButton1.Checked = true; else radioButton2.Checked = true;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

      
    }
}
