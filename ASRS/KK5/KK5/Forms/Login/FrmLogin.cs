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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            refreshCombobox();
        }
        //확인
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            if (textBox1.Text.Trim() == "") return;

            string user = comboBox1.SelectedItem.ToString();
            string pass = textBox1.Text.Trim();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.miusers.Where(x => x.userid == user && x.passwd == pass).SingleOrDefault();
                if (q == null)
                {
                    MessageBox.Show("Password not matched...!", "Waring");
                    return;
                }
                Common.userid = q.userid;
                Common.username = q.username;
                Common.role = q.role;
                DialogResult = DialogResult.OK;
            }
        }
        //등록
        private void button3_Click(object sender, EventArgs e)
        {
            using (FrmAddUser_p p = new FrmAddUser_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;                
            }
            refreshCombobox();
        }
        private void refreshCombobox()
        {
            comboBox1.Items.Clear();
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(@"select userid from miuser").ToList();
                foreach (var s in q)
                {
                    comboBox1.Items.Add(s.userid);
                }
            }
        }
        //수정
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            if (textBox1.Text.Trim() == "") return;

            string user = comboBox1.SelectedItem.ToString();
            string pass = textBox1.Text.Trim();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.miusers.Where(x => x.userid == user && x.passwd == pass).SingleOrDefault();
                if (q == null)
                {
                    MessageBox.Show("Password not matched...!", "Waring");
                    return;
                }
                using (FrmLoginEdit_p p = new FrmLoginEdit_p(user, q.username))
                {
                    p.ShowDialog();
                    if (p.DialogResult == DialogResult.Cancel) return;
                }
                MessageBox.Show("사용자 정보 수정 완료!!  로그인하세요...!");
                refreshCombobox();

            }

        }
    }
}
