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
    public partial class FrmRcpIstnWrite_p : Form
    {
        public string tstn="", loca="", indx="";

        public FrmRcpIstnWrite_p()
        {
            InitializeComponent();
        }

        private void FrmRcpIstnWrite_p_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;

            if (maskedTextBox1.Text == "000000") return;

            decimal d = numericUpDown1.Value;
            
            indx = d.ToString("0000");
            loca = "A" + maskedTextBox1.Text.Replace("-", "") ;
            
            DBDataContext db = new DBDataContext(Config.DBCon);
            int rc = db.ExecuteQuery<int>(@"select count(*) from milstk where lstk_no = {0}", loca).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("그런위치는 존재하지 않읍니다", "확인");
                return;
            }
            if(indx == "0000")
            {
                MessageBox.Show("순번 지대로 입력하세요", "확인");
                return;
            }
            tstn = comboBox1.SelectedItem.ToString();
            
            string bk = loca.Substring(1, 2);

            if (bk == "01" || bk == "02") if (tstn != "01") { MessageBox.Show("입고대번호와 입고위치가 맞지 않읍니다"); return; }
            if (bk == "02" || bk == "03") if (tstn != "03") { MessageBox.Show("입고대번호와 입고위치가 맞지 않읍니다"); return; }
            if (bk == "04" || bk == "05") if (tstn != "05") { MessageBox.Show("입고대번호와 입고위치가 맞지 않읍니다"); return; }
            if (bk == "06" || bk == "07") if (tstn != "07") { MessageBox.Show("입고대번호와 입고위치가 맞지 않읍니다"); return; }
            if (bk == "08" || bk == "09") if (tstn != "09") { MessageBox.Show("입고대번호와 입고위치가 맞지 않읍니다"); return; }


            //istn, loca, indx
            DialogResult = DialogResult.OK;
        }
    }
}
