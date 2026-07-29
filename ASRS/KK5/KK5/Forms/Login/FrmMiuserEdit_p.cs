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
    public partial class FrmMiuserEdit_p : Form
    {
        public FrmMiuserEdit_p(string userid, string username, string role)
        {
            InitializeComponent();
            textBox1.Text = userid;
            textBox2.Text = username;
            if (role == "") comboBox1.SelectedIndex = 0;
            if (role == "1") comboBox1.SelectedIndex = 1;
            if (role == "2") comboBox1.SelectedIndex = 2;
            if (role == "3") comboBox1.SelectedIndex = 3;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text;
            string username = textBox2.Text;
            string role = "";
            if (comboBox1.SelectedIndex==0) role = "";
            if (comboBox1.SelectedIndex == 1) role = "1";
            if (comboBox1.SelectedIndex == 2) role = "2";
            if (comboBox1.SelectedIndex == 3) role = "3";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.ExecuteCommand("update miuser set username = {0}, role = {1} where userid = {2} ", username, role, userid);
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmMiuserEdit_p_Load(object sender, EventArgs e)
        {

        }
    }
}
