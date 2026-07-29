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
    public partial class FrmPath_p : Form
    {
        public FrmPath_p()
        {
            InitializeComponent();
        }

        private void FrmPath_p_Load(object sender, EventArgs e)
        {

            button1.Click += button1_Click;
            button2.Click += button2_Click;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string ls = db.ExecuteQuery<string>(@"select stat_ipath from tbstat where stat_key = '1' ").SingleOrDefault();
                if (ls == "0") radioButton1.Checked = true;
                if (ls == "1") radioButton2.Checked = true;
                if (ls == "2") radioButton3.Checked = true;
                if (ls == "3") radioButton4.Checked = true;
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

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
