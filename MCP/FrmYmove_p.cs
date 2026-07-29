using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;

namespace MCP
{
    public partial class FrmYmove_p : Form
    {
        public FrmYmove_p()
        {
            InitializeComponent();
        }

        private void FrmYmove_p_Load(object sender, EventArgs e)
        {
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string ls_imode = db.ExecuteQuery<string>(@"select stat_imode from tbstat where stat_key = '1'").SingleOrDefault();
                if (ls_imode != "1")
                {
                    MessageBox.Show("바코드모드에서만 가능합니다...!");
                    return;
                }

                int cc = db.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '24' and indx_sflg = 'P'").SingleOrDefault();
                if (cc > 0)
                {
                    MessageBox.Show("이미 RCP 지시가 나갔읍니다...!");
                    return;
                }

                var cv = db.ExecuteQuery<tbcnvcq>(@"select cnvc_24_rqst, cnvc_remote, cnvc_stop, cnvc_comm, cnvc_op_onof, cnvc_op_eror, cnvc_buf_palt, cnvc_jobno
                                                 from tbcnvc where cnvc_mode = '01' ").SingleOrDefault();
                string ls_24data = cv.cnvc_jobno;
                ls_24data = ls_24data.Substring(104, 4);

                cc = db.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '24' and indx_indx = {0}", ls_24data).SingleOrDefault();
                if (cc > 0)
                {
                    MessageBox.Show("이미 RCP 지시가 나갔읍니다2...!");
                    return;
                }
                if (cv.cnvc_24_rqst != "1")
                {
                    MessageBox.Show("투입요청 신호가 없읍니다...!");
                    return;
                }
                if (cv.cnvc_op_onof.Substring(5, 1) != "1")
                {
                    MessageBox.Show("해당 컨베어구간 수동입니다...!");
                    return;
                }
                if (cv.cnvc_op_eror.Substring(5, 1) != "0")
                {
                    MessageBox.Show("해당 컨베어구간 에러입니다...!");
                    return;
                }
                if (cv.cnvc_buf_palt.Substring(23, 1) != "1")
                {
                    MessageBox.Show("해당 컨베어구간 화물없읍니다...!");
                    return;
                }
                try
                {
                    string jno = "";
                    int rc = db.p_get_indx_jno("4", ref jno);
                    string indx = jno.Substring(jno.Length - 4, 4);

                    db.ExecuteCommand(@"INSERT INTO tbindx  
		                        (  indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
   		                            indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
	     	                        indx_edat,     indx_sflg,       indx_uflg )  
                            values( {0},           {1},             'A',             'M',           '0',
                                    '24',          '43',            '',               '',           'N',
                                    '',            'P',             '0' )", jno, indx);

                    string dtstr = "";
                    db.p_curgetdatetime19(ref dtstr);
                    string ls_date = dtstr.Substring(0, 10);
                    string ls_time = dtstr.Substring(11, 8);

                    string mmsg = comboBox1.SelectedItem.ToString();
                    db.ExecuteCommand(@"insert into tbberr(err_date, err_time, err_act, err_mmsg)
                                    values ( {0}, {1},  {2}, {3} )", ls_date, ls_time, '0', mmsg);                    

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                DialogResult = DialogResult.OK;
            }
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
