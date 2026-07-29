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
    public partial class FrmInptBarcode_p : Form
    {
        public FrmInptBarcode_p()
        {
            InitializeComponent();
        }

        private void FrmInptBarcode_p_Load(object sender, EventArgs e)
        {

            button1.Click += button1_Click;
            button2.Click += button2_Click;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int cc = 0;
            string ls_pltno = textBox1.Text;

            if (ls_pltno.Length < 8)
            {
                MessageBox.Show("8자리 올바른 숫자를 입력하세요");
                return;
            }
            try
            {
                int li_pltno = Convert.ToInt32(ls_pltno);
                if (li_pltno == 0)
                {
                    MessageBox.Show("8자리 올바른 숫자를 입력하세요");
                    return;
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }

            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    cc = db.ExecuteQuery<int>(@"select count(*) from tibarc where barc_flag = '1'").SingleOrDefault();
                    if (cc > 0)
                    {
                        MessageBox.Show("이미 스캐너가 정상적으로 읽었읍니다..!!");
                        return;
                    }
                    cc = db.ExecuteQuery<int>(@"select count(*) from miplti where plti_pltno = {0} and substring(plti_lstk, 1,1) in ('Y', 'F') ", ls_pltno).SingleOrDefault();
                    if (cc == 0)
                    {
                        MessageBox.Show("파렛트번호 잘못입력했읍니다...!!");
                        return;
                    }
                    cc = db.ExecuteQuery<int>(@"select count(*) from miplti where plti_pltno = {0} and substring(plti_lstk, 1,1) in ('Y', 'F') and plti_rqty > 0", ls_pltno)
                                               .SingleOrDefault();
                    if (cc == 0)
                    {
                        MessageBox.Show("출고 예약이 되어 있읍니다");
                        MessageBox.Show("출고예약 취소후 행하세요...!!");
                        return;
                    }

                    db.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag  = '1', barc_msg = '수입력 OK' where barc_flag <> '1", ls_pltno);

                    string dts = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                    dts = dts.Replace("-", "/");
                    string ls_date = dts.Substring(0, 10);
                    string ls_time = dts.Substring(11, 8);

                    db.ExecuteCommand(@"insert into tbberr(err_date, err_time, err_pltno, err_act, err_mmsg)values( {0},{1},{2},{3},{4} )",
                                                           ls_date, ls_time, ls_pltno, "1", "바코드 수입력 처리");

                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

     
    }
}
