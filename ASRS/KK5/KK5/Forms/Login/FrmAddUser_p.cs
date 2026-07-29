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
    public partial class FrmAddUser_p : Form
    {
        public FrmAddUser_p()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text.Trim();
            string username = textBox2.Text.Trim();
            string passwd = textBox3.Text.Trim();
            string cpasswd = textBox4.Text.Trim();

            if (userid == "")
            {
                MessageBox.Show("사용자ID missing...!");
                return;
            }
            if (username == "")
            {
                MessageBox.Show("사용자명 missing...!");
                return;
            }

            if (passwd == "")
            {
                MessageBox.Show("사용자암호 missing...!");
                return;
            }
            if (cpasswd == "")
            {
                MessageBox.Show("사용자암호 confirm missing...!");
                return;
            }

            if (passwd != cpasswd)
            {
                MessageBox.Show("사용자암호 다릅니다...!");
                return;
            }
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"insert into miuser (userid, username, passwd, role) 
                                        values ({0}, {1}, {2}, {3} )", userid, username, passwd, "");

                }
            }
            catch (Exception E)
            {
                MessageBox.Show("사용자 ID 중복입니다...!");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void FrmAddUser_p_Load(object sender, EventArgs e)
        {

        }
    }
}
