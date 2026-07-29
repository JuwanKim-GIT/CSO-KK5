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
    public partial class FrmLoginEdit_p : Form
    {
        public FrmLoginEdit_p(string user, string username)
        {
            InitializeComponent();
            textBox1.Text = user;
            textBox2.Text = username;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text.Trim();
            string username = textBox2.Text.Trim();
            string passwd = textBox3.Text.Trim();
            string cpasswd = textBox4.Text.Trim();

       
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
                    db.ExecuteCommand(@"update miuser set username = {0}, passwd = {1} where userid = {2} ", username, passwd, userid);

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message );
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
