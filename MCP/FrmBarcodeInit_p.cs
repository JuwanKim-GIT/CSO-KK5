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
    public partial class FrmBarcodeInit_p : Form
    {
        public FrmBarcodeInit_p()
        {
            InitializeComponent();
        }

        private void FrmBarcodeInit_p_Load(object sender, EventArgs e)
        {
            button1.Click += button1_Click;
            button2.Click += button2_Click;          

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    string ls = db.ExecuteQuery<string>(@"select stat_imode from tbstat where stat_key = '1' ").SingleOrDefault();
                    if (ls != "1")
                    {
                        MessageBox.Show("바코드입고모드가 아닙니다");
                        return;
                    }

                    var q = db.ExecuteQuery(@"select barc_pltno, barc_flag from tibarc where barc_key = '1' ").SingleOrDefault();
                    if (q.barc_flag != "1")
                    {
                        MessageBox.Show("이미처리 되거나 다시 읽는 중입니다");
                       // return;
                    }
                    string ls_pltno = q.barc_pltno;

                    string ls_job = db.ExecuteQuery<string>(@"select cnvc_jobno from tbcnvc where cnvc_mode = '01' ").SingleOrDefault();
                    string ls_jobno = ls_job.Substring(104, 4);
                    ;
                    int cc = db.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '24' and indx_indx = {0} ", ls_jobno).SingleOrDefault();
                    if (cc > 0)
                    {
                        MessageBox.Show("이미 PLC에 전송되어 바코드 재지시 불가!");
                        return;
                    }

                    string dts = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                    dts = dts.Replace("-", "/");
                    string ls_date = dts.Substring(0, 10);
                    string ls_time = dts.Substring(11, 8);

                   
                    db.ExecuteCommand(@"update tibarc set barc_flag = '0', barc_msg = '', cvc_msg = '', barc_pltno = '' where barc_key = '1' ");
                  
                    db.ExecuteCommand(@"insert into tbberr(err_date, err_time, err_pltno, err_act, err_mmsg)values( {0},{1},{2},{3},{4})",
                                                           ls_date, ls_time, ls_pltno, "2", "");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    
    }
}
