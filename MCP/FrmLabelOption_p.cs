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
    public partial class FrmLabelOption_p : Form
    {
        public FrmLabelOption_p()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmLabelOption_p_Load(object sender, EventArgs e)
        {
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string ls = db.ExecuteQuery<string>(@"select stat_lr from tbstat where stat_key = '1' ").SingleOrDefault();
                if (ls == "L") radioButton1.Checked = true; else radioButton2.Checked = true;
            }
        }
    }
}
