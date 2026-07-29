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
    public partial class FrmIMode_p : Form
    {
        public FrmIMode_p()
        {
            InitializeComponent();
        }

        private void FrmIMode_p_Load(object sender, EventArgs e)
        {

            button1.Click += button1_Click;
            button2.Click += button2_Click;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string ls = db.ExecuteQuery<string>(@"select stat_imode from tbstat where stat_key = '1' ").SingleOrDefault();
                if (ls == "0") radioButton1.Checked = true; else radioButton2.Checked = true;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int cc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                if (radioButton1.Checked == true)
                {
                    db.ExecuteCommand(@"update tbstat set stat_imode = '0' where stat_key = '1' ");
                }
                else
                {
                    cc = db.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn in ('21', '22') and indx_tstn <> '24' and indx_sflg = 'P'").SingleOrDefault();
                    if (cc > 0)
                    {
                        MessageBox.Show("기존에 파렛트 선택입고가 있어 바코드 입고모드 변경불가...!");
                        return;
                    }
                    db.ExecuteCommand(@"update tbstat set stat_imode = '1' where stat_key = '1' ");
                }
            }          

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

   
    }
}
