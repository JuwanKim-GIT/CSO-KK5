using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;

namespace MCP
{
    public partial class FrmSCRC_p : Form
    {
       
        string hogi;
        tbscrcq sc;
        public FrmSCRC_p(string hogi)
        {
            InitializeComponent();
            this.hogi = hogi;
            panel1.BackColor = Color.FromArgb(192, 220, 192);        


        }

        private void FrmSCRC_p_Load(object sender, EventArgs e)
        {
            schogi.Text = hogi.Substring(1, 1) + "호기";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                sc = db.ExecuteQuery<tbscrcq>(@"select * from tbscrc where scrc_no = {0}", hogi).SingleOrDefault();

                if (sc.scrc_onln == "1") sc_onln.Text = "자동"; else sc_onln.Text = "수동";
                if (sc.scrc_pwron == "1") sc_pwron.Text = "ON"; else sc_pwron.Text = "OFF";

                if (sc.scrc_io == "I") sc_io.Text = "입고";
                if (sc.scrc_io == "$") sc_io.Text = "출고";
                if (sc.scrc_io == "") sc_io.Text = "대기";
                if (sc.scrc_io == "M") sc_io.Text = "이출"; // ??
                sc_mode.Text = sc.scrc_mode;

                if (sc.scrc_stat == "0001") sc_stat.Text = "하무대기";
                if (sc.scrc_stat == "0002") sc_stat.Text = "하유대기";
                if (sc.scrc_stat == "0007") sc_stat.Text = "작업중";
                if (sc.scrc_stat == "0008") sc_stat.Text = "에러발생";
                if (sc.scrc_stat == "0009") sc_stat.Text = "작업완료";
                if (sc.scrc_stat == "0000") sc_stat.Text = "비저위치";

                if (sc.scrc_eror == "0") sc_eror.Text = "정상";
                if (sc.scrc_eror == "D") sc_eror.Text = "이중";
                if (sc.scrc_eror == "E") sc_eror.Text = "공출";
                if (sc.scrc_eror == "G") sc_eror.Text = "기타";
                if (sc.scrc_eror == "Q") sc_eror.Text = "Data이상";
                sc_ercd.Text = sc.scrc_ecod;
                if (sc.scrc_emer == "0") sc_emer.Text = "정상";
                if (sc.scrc_emer == "1") sc_emer.Text = "비상";
                sc_gubn.Text = sc.scrc_gubn;
                sc_xmov.Text = sc.scrc_xmov;
                if (sc.scrc_palt == "1") sc_palt.Text = "유";
                if (sc.scrc_palt == "0") sc_palt.Text = "무";
                sc_posi.Text = sc.scrc_posi.Substring(0, 2) + "-" + sc.scrc_posi.Substring(2, 2);
                sc_indx.Text = sc.scrc_indx;
                sc_jno.Text = sc.scrc_jno;
                if (sc.scrc_lstk != "")
                    sc_lstk.Text = sc.scrc_lstk.Substring(0, 2) + "-" + sc.scrc_lstk.Substring(2, 2) + "-" + sc.scrc_lstk.Substring(4, 2);
                else sc_lstk.Text = "";

                if (sc.scrc_pltn != "") sc_pltno.Text = sc.scrc_pltn;
                else sc_pltno.Text = "";

                sc_fstn.Text = sc.scrc_fstn;
                sc_tstn.Text = sc.scrc_tstn;
                if (sc.scrc_stop == "1") sc_stop.Text = "중지"; else sc_stop.Text = "가동";
                if (sc.scrc_iuse == "0") sc_iuse.Text = "금지"; else sc_iuse.Text = "사용";
                if (sc.scrc_ouse == "0") sc_ouse.Text = "금지"; else sc_ouse.Text = "사용";
                sc_mesg.Text = sc.scrc_mesg;
                sc_comm.Text = sc.scrc_comm;
                sc_rset.Text = sc.scrc_rset;
            }
                
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //********************************************************************************************//
        //* Rack 출고완료시 ( 'X' -> 'P' ) 
        //********************************************************************************************//
        private void btnoutdone_Click(object sender, EventArgs e)
        {

            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "출고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_mode.Substring(1, 1) == "3")
            {
                MessageBox.Show("스택카에 처리할 Data가 없습니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "출고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_mode == "O1" || sc.scrc_mode == "O2"))
            {
                MessageBox.Show("스택카가 출고상태가 아님, 출고완료처리가 불가합니다!! " + Environment.NewLine +
                                "다시 조회후 처리바람.....!!", "출고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("스택카 출고완료처리란, 실PLT가 랙에서 출고되어, 컨베어상에 이동되었고," + Environment.NewLine +
                               "스택카는 데이타가 아직 작업중으로 남은경우나 또는, " + Environment.NewLine +
                               "스택카에러로 출고하던 실PLT을 해당 콘베어상으로 강제수동 이동한후, 데이타완료처리 할경우 입니다. " + Environment.NewLine +
                               "완료를 행하면 스풀이 콘베어상에 있는것으로 데이타  처리되므로,  " + Environment.NewLine +
                               "스택카나, 출고하려던 위치랙에는 반드시 실PLT가 없이, 콘베어상에 있어야 합니다.  " + Environment.NewLine +
                               "정말로,  스택카 출고완료 하시겠읍니까?(랙=>스택카=>콘베어로 이동됨)", "출고완료",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            bool ff = false;
            int st = 0;
           
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                try
                {
                    db.Connection.open();
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        rc = db.ExecuteCommand(@"update tbscrc 
                                                   set scrc_mode = 'O3', scrc_gubn = '',  scrc_io   = '',  scrc_ecod = '',  scrc_lstk = '', scrc_pltn = '', 
	                                                   scrc_jno  = '',   scrc_indx = '',  scrc_fstn = '',  scrc_tstn = '',
		                                               scrc_xmov = '',   scrc_mesg = '출고대 UNLOAD 완료 '
                                                where scrc_no = {0}
                                                and scrc_mode = {1}
                                                and scrc_jno  = {2}
                                                and scrc_lstk = {3}
                                                and scrc_rset = '0'
                                                and scrc_pltn = {4} ", hogi, sc.scrc_mode, sc.scrc_jno, sc.scrc_lstk, sc.scrc_pltn);
                        if (rc == 0)
                        {
                            db.Transaction.Rollback();
                            ff = true; st = 1;
                        }
                        else
                        {
                            string dts = "";
                            db.p_curgetdatetime14(ref dts);
                            string ls_lstk = "A" + sc.scrc_lstk;
                            string ls_scno = hogi.Substring(1, 1);

                            if (sc.scrc_pltn != "99999999")  // 2014/08/01 그냥출고
                            {
                                rc = db.ExecuteCommand(@"insert into tbevnt ( evnt_gubn,    evnt_jio,    evnt_hogi,    evnt_fstn,     evnt_tstn,
	                                                                          evnt_pltn,    evnt_lstk,   evnt_xmov,    evnt_sflg,     evnt_wflg,   evnt_uflg, evnt_wdate )
	                                                              values    ( {0},          '$',        {1},          {2},           {3}, 
   	                                                                          {4},           {5},       {6},          'X',           'F',          '0',       {7} )",
                                                                              sc.scrc_gubn, ls_scno, sc.scrc_fstn, sc.scrc_tstn,
                                                                              sc.scrc_pltn, ls_lstk, sc.scrc_xmov, dts);

                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 2;
                                }
                            }
                            if (rc > 0)
                            {
                                // 출고대 도착(CV쓰기 지시)
                                rc = db.ExecuteCommand(@"update tbindx set indx_edat = {0}, indx_sflg = 'P' where indx_jno = {1} and indx_pltn = {2}",
                                                           dts, sc.scrc_jno, sc.scrc_pltn);
                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 3;
                                }
                                else
                                {
                                    db.Transaction.Commit();
                                }
                            }
                        }
                    }
                }
                catch (Exception E)
                {
                    if (db.Transaction != null)
                        db.Transaction.Rollback();
                    MessageBox.Show(E.Message);
                }
                finally { db.Connection.Close(); }
            } 
            if (ff) MessageBox.Show("상태가 변했읍니다.!!" + st.ToString());
           
            DialogResult = DialogResult.OK;
        }

        //********************************************************************************************//
        //* Rack 입고완료시 ( 'X' -> 'Z' ) 
        //********************************************************************************************//
        private void btninfinisj_Click(object sender, EventArgs e)
        {

            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "입고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_mode.Substring(1, 1) == "3")
            {
                MessageBox.Show("스택카에 처리할 Data가 없습니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "입고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_mode == "I1" || sc.scrc_mode == "I2"))
            {
                MessageBox.Show("스택카가 입고상태가 아님, 입고완료처리가 불가합니다!! " + Environment.NewLine +
                                "다시 조회후 처리바람.....!!", "입고완료", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("스택카 입고완료처리란, 실PLT가 컨베어상에서 스택카로 이동되어, 랙에 입고됬고," + Environment.NewLine +
                               "스택카는 데이타가 아직, 작업중으로 남은경우나 또는, " + Environment.NewLine +
                               "스택카 에러로 입고하던, 실PLT을 해당 랙위치로 강제수동 입고한후, 데이타완료처리 할경우 입니다.. " + Environment.NewLine +
                               "완료를 행하면 PLT가 랙입고위치에 있는것으로 데이타  처리되므로,,  " + Environment.NewLine +
                               "입고콘베어나, 스택카상에는 반드시 실PLT가 없이, 입고위치랙에 있어야 합니다.   " + Environment.NewLine +
                               "정말로,  스택카 입고완료 하시겠읍니까?(콘베어=>스택카=>랙로 이동됨)", "입고완료",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            bool ff = false;
            int st = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                try
                {
                    db.Connection.open();
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        rc = db.ExecuteCommand(@"update tbscrc 
                                                   set scrc_mode = 'I3', scrc_gubn = '',  scrc_io   = '',  scrc_ecod = '',  scrc_lstk = '', scrc_pltn = '', 
	                                                   scrc_jno  = '',   scrc_indx = '',  scrc_fstn = '',  scrc_tstn = '',
		                                               scrc_xmov = '',   scrc_mesg = 'RACK 입고 완료'
                                                where scrc_no = {0}
                                                and scrc_mode = {1}
                                                and scrc_jno  = {2}
                                                and scrc_lstk = {3}
                                                and scrc_rset = '0'
                                                and scrc_pltn = {4} ", hogi, sc.scrc_mode, sc.scrc_jno, sc.scrc_lstk, sc.scrc_pltn);
                        if (rc == 0)
                        {
                            db.Transaction.Rollback();
                            ff = true; st = 1;
                        }
                        else
                        {
                            string dts = "";
                            db.p_curgetdatetime14(ref dts);
                            string ls_lstk = "A" + sc.scrc_lstk;
                            string ls_scno = hogi.Substring(1, 1);
                            if (sc.scrc_gubn != "R") // RCP 수동처리가 아니면
                            {
                                rc = db.ExecuteCommand(@"insert into tbevnt ( evnt_gubn,    evnt_jio,    evnt_hogi,    evnt_fstn,     evnt_tstn,
	                                                                          evnt_pltn,    evnt_lstk,   evnt_xmov,    evnt_sflg,     evnt_wflg,   evnt_uflg, evnt_wdate )
	                                                            values      ( {0},          'I',        {1},          {2},           {3}, 
   	                                                                          {4},           {5},       {6},          'X',           'F',          '0',       {7} )",
                                                                              sc.scrc_gubn, ls_scno, sc.scrc_fstn, sc.scrc_tstn,
                                                                              sc.scrc_pltn, ls_lstk, sc.scrc_xmov, dts);

                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 2;
                                }
                            }
                            if (rc > 0)
                            {
                                rc = db.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_pltn = {1}", sc.scrc_jno, sc.scrc_pltn);
                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 3;
                                }
                                else
                                {
                                    db.Transaction.Commit();
                                }
                            }
                        }
                    }                
                }
                catch (Exception E)
                {
                    if (db.Transaction != null)
                        db.Transaction.Rollback();

                    MessageBox.Show(E.Message);
                }
                finally { db.Connection.Close(); }
            }

            if (ff) MessageBox.Show("상태가 변했읍니다.!!" + st.ToString());
            DialogResult = DialogResult.OK;
        }

        //********************************************************************************************//
        //* Rack 입고취소시 ( 'X' -> 'C' ) 
        //********************************************************************************************//
        private void btnincncl_Click(object sender, EventArgs e)
        {

            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "입고취소", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_mode.Substring(1, 1) == "3")
            {
                MessageBox.Show("스택카에 처리할 Data가 없습니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "입고취소", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_mode == "I1" || sc.scrc_mode == "I2"))
            {
                MessageBox.Show("스택카가 입고상태가 아님, 입고완료처리가 불가합니다!! " + Environment.NewLine +
                                "다시 조회후 처리바람.....!!", "입고취소", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("스택카 입고취소처리란, 스택카에서 작업중인 실PLT를 출고대로 이동시켜, 도로 야적장으로 이동할 경우," + Environment.NewLine +
                                "어떤원인이든 입고하던, 실PLT을 취소시켜, 도로 출고대로 강제수동 이동시켜 빼내는 처리 입니다,  " + Environment.NewLine +
                                "처리를 행하면 PLT가 야적위치에 있는것으로 데이타  처리되므로,  또한 반드시" + Environment.NewLine +
                                "실PLT를 야적장으로 이동시키려면, 해당 출고대에 강제 이동 데이타를 등록해야 합니다.   " + Environment.NewLine +
                                "정말로,  스택카 입고취소를 하시겠읍니까?(스택카=>콘베어=>야적장으로 이동됨)", "입고취소",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            bool ff = false;
            int st = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                try
                {
                    db.Connection.open();
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        rc = db.ExecuteCommand(@"update tbscrc 
                                                   set scrc_mode = 'I3', scrc_gubn = '',  scrc_io   = '',  scrc_ecod = '',  scrc_lstk = '', scrc_pltn = '', 
	                                                   scrc_jno  = '',   scrc_indx = '',  scrc_fstn = '',  scrc_tstn = '',
		                                               scrc_xmov = '',   scrc_mesg = 'RACK 입고 취소됨',    scrc_rset = '0'
                                                where scrc_no = {0}
                                                and scrc_mode = {1}
                                                and scrc_jno  = {2}
                                                and scrc_lstk = {3}
                                                and scrc_rset = '0'
                                                and scrc_pltn = {4} ", hogi, sc.scrc_mode, sc.scrc_jno, sc.scrc_lstk, sc.scrc_pltn);
                        if (rc == 0)
                        {
                            db.Transaction.Rollback();
                            ff = true; st = 1;
                        }
                        else
                        {
                            string dts = "";
                            db.p_curgetdatetime14(ref dts);
                            string ls_lstk = "A" + sc.scrc_lstk;
                            string ls_scno = hogi.Substring(1, 1);
                            if (sc.scrc_gubn != "R") // RCP 수동처리가 아니면
                            {
                                rc = db.ExecuteCommand(@"insert into tbevnt ( evnt_gubn,    evnt_jio,    evnt_hogi,    evnt_fstn,     evnt_tstn,
	                                                                          evnt_pltn,    evnt_lstk,   evnt_xmov,    evnt_sflg,     evnt_wflg,   evnt_uflg, evnt_wdate )
	                                                            values      ( {0},          'I',        {1},          {2},           {3}, 
   	                                                                          {4},           {5},       {6},          'X',           'C',          '0',       {7} )",
                                                                              sc.scrc_gubn, ls_scno, sc.scrc_fstn, sc.scrc_tstn,
                                                                              sc.scrc_pltn, ls_lstk, sc.scrc_xmov, dts);
                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 2;
                                    return;
                                }
                            }
                            db.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_pltn = {1}", sc.scrc_jno, sc.scrc_pltn);
                            db.Transaction.Commit();                             
                        }
                    }
                }
                catch (Exception E)
                {
                    if (db.Transaction != null)
                        db.Transaction.Rollback();

                    MessageBox.Show(E.Message);
                }
                finally { db.Connection.Close(); }
            }

            if (ff) MessageBox.Show("상태가 변했읍니다.!!" + st.ToString());
            DialogResult = DialogResult.OK;
        }

        //********************************************************************************************//
        //* Rack 공출시 처리 ( 'X' -> 'E' ) 
        //********************************************************************************************//
        private void btnempt_Click(object sender, EventArgs e)
        {            
            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "공출처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_mode.Substring(1, 1) == "3")
            {
                MessageBox.Show("스택카에 처리할 Data가 없습니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "공출처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_mode == "O1" || sc.scrc_mode == "O2"))
            {
                MessageBox.Show("스택카가 출고상태가 아님, 공출고 처리가 불가합니다!!!! " + Environment.NewLine +
                                "다시 조회후 처리바람.....!!", "공출처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("스택카 공출처리란, 스택카에서 공출된 데이타건을 처리할경우 사용합니다," + Environment.NewLine +
                                "처리를 행하면, 해당 출고건이 공출처리된 것으로 처리됩니다 " + Environment.NewLine +
                                "정말로,  공출처리를 하시겠읍니까?", "공출처리",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            bool ff = false;
            int st = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                try
                {
                    db.Connection.open();
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        rc = db.ExecuteCommand(@"update tbscrc 
                                                   set scrc_mode = 'O3', scrc_gubn = '',  scrc_io   = '',  scrc_ecod = '',  scrc_lstk = '', scrc_pltn = '', 
	                                                   scrc_jno  = '',   scrc_indx = '',  scrc_fstn = '',  scrc_tstn = '',
		                                               scrc_xmov = '',   scrc_mesg = '공출처리완료',        scrc_rset = '0'
                                                where scrc_no = {0}
                                                and scrc_mode = {1}
                                                and scrc_jno  = {2}
                                                and scrc_lstk = {3}
                                                and scrc_rset = '0'
                                                and scrc_pltn = {4} ", hogi, sc.scrc_mode, sc.scrc_jno, sc.scrc_lstk, sc.scrc_pltn);
                        if (rc == 0)
                        {
                            db.Transaction.Rollback();
                            ff = true; st = 1;
                        }
                        else
                        {
                            string dts = "";
                            db.p_curgetdatetime14(ref dts);
                            string ls_lstk = "A" + sc.scrc_lstk;
                            string ls_scno = hogi.Substring(1, 1);
                            if (sc.scrc_pltn != "99999999") // 2014/08/01 그냥출고
                            {
                                rc = db.ExecuteCommand(@"insert into tbevnt ( evnt_gubn,    evnt_jio,    evnt_hogi,    evnt_fstn,     evnt_tstn,
	                                                                          evnt_pltn,    evnt_lstk,   evnt_xmov,    evnt_sflg,     evnt_wflg,   evnt_uflg, evnt_wdate )
	                                                            values      ( {0},          '$',        {1},          {2},           {3}, 
   	                                                                          {4},           {5},       {6},          'X',           'E',          '0',       {7} )",
                                                                              sc.scrc_gubn, ls_scno, sc.scrc_fstn, sc.scrc_tstn,
                                                                              sc.scrc_pltn, ls_lstk, sc.scrc_xmov, dts);
                                 if (rc == 0)
                                 {
                                     db.Transaction.Rollback();
                                     ff = true; st = 2;
                                 }
                            }

                            if (rc > 0)
                            {
                                rc = db.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_pltn = {1}", sc.scrc_jno, sc.scrc_pltn);
                                if (rc == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true; st = 3;
                                }
                                else
                                {
                                    db.Transaction.Commit();
                                }
                            }
                        }
                    }
                }
                catch (Exception E)
                {
                    if (db.Transaction != null)
                        db.Transaction.Rollback();

                    MessageBox.Show(E.Message);
                }
                finally { db.Connection.Close(); }
            }
            if (ff) MessageBox.Show("상태가 변했읍니다.!!" + st.ToString());
           
            DialogResult = DialogResult.OK;

        }

        //********************************************************************************************//
        //* Rack 입,출 재지시 처리
        //********************************************************************************************//
        private void btnretry_Click(object sender, EventArgs e)
        {
            if (sc.scrc_comm != "1")
            {
                MessageBox.Show("스택카 상태가 통신정지 상태입니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_onln != "1")
            {
                MessageBox.Show("스택카 상태가 원격모드(자동상태)일때만 가능합니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_pwron != "1")
            {
                MessageBox.Show("스택카 상태가 전원차단 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_emer != "0")
            {
                MessageBox.Show("스택카가 비상정지 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_eror != "0")
            {
                MessageBox.Show("스택카가 에러상태입니다...!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_stop != "0")
            {
                MessageBox.Show("스택카 상태가 사용금지입니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_mode == "I1" || sc.scrc_mode == "O1"))
            {
                MessageBox.Show("스택카의 작업이 입고(I1)나 출고(O1)시만 가능함..!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            if (MessageBox.Show("재지시처리란 스택카에 다시 지령을 주는것으로, 원점상태나 대기상태일때만 가능합니다, " + Environment.NewLine +
                                "단, 재지시는 스택카를 에러 조치후, 원점 복귀시킨후 하기 바랍니다.  " + Environment.NewLine +
                                "정말로, 스택카에 재지시처리 하시겠읍니까..?", "재지시처리",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            string ls_rmode = "";
            switch (sc.scrc_mode)
            {
                case "I1":
                case "I2":
                    ls_rmode = "I0";
                    break;
                case "O1":
                case "O2":
                    ls_rmode = "O0";
                    break;
                default:
                    ls_rmode = "I0";
                    break;
            }
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var cvc = db.ExecuteQuery("select cnvc_op_onof, cnvc_op_eror, cnvc_stop, cnvc_comm from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (cvc == null) return;
                string ls_comm = cvc.cnvc_comm;
                if (ls_comm != "1")
                {
                    MessageBox.Show("콘베어 상태가 통신정지 상태입니다.!! " + Environment.NewLine +
                                    "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                int li_hogi = Convert.ToInt32(hogi);
                string ls_onln = cvc.cnvc_op_onof;
                string ls_eror = cvc.cnvc_op_eror;
                if (ls_onln.Substring(li_hogi - 1, 1) == "0" || ls_eror.Substring(li_hogi - 1, 1) == "1")
                {
                    MessageBox.Show("해당콘베어가 수동이나 에러이므로 불가합니다...!!" + Environment.NewLine +
                                    "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
                int rc = db.ExecuteCommand(@"update tbscrc 
                                        set scrc_mode = {0}, scrc_mesg = '재지시처리', scrc_rset = '0'
                                         where scrc_no   = {1}
                                           and scrc_mode = {2}
                                           and scrc_stat = {3}
                                           and scrc_onln  = '1'
                                           and scrc_pwron = '1'
                                           and scrc_emer  = '0'
                                           and scrc_eror  = '0'
                                           and scrc_jno  = {4}
                                           and scrc_lstk = {5}
                                           and scrc_pltn = {6}
                                           and scrc_rset  = '0'
                                           and scrc_stop  = '0'
                                           and scrc_comm  = '1' ",
                                               ls_rmode, hogi, sc.scrc_mode, sc.scrc_stat, sc.scrc_jno, sc.scrc_lstk, sc.scrc_pltn);
                if (rc == 0)
                {
                    MessageBox.Show("스택카가 상태가 이미 변함... 조회후 다시 처리 바람..!!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;

            }
        }

        //********************************************************************************************//
        //* 스택카 HOME이동 처리
        //********************************************************************************************//
        private void btnhome_Click(object sender, EventArgs e)
        {
            if (sc.scrc_comm != "1")
            {
                MessageBox.Show("스택카 상태가 통신정지 상태입니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_onln != "1")
            {
                MessageBox.Show("스택카 상태가 원격모드(자동상태)일때만 가능합니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_pwron != "1")
            {
                MessageBox.Show("스택카 상태가 전원차단 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_emer != "0")
            {
                MessageBox.Show("스택카가 비상정지 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_stat != "0001")
            {
                MessageBox.Show("스택카 상태가 하무대기가 아니므로, 불가합니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_eror != "0")
            {
                MessageBox.Show("스택카가 에러상태입니다...!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_mode.Substring(1, 1) != "3")
            {
                MessageBox.Show("스택카에 작업 데이타가 있습니다, HOME이동 불가..!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_stop != "0")
            {
                MessageBox.Show("스택카 상태가 사용금지입니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "HOME이동처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            if (MessageBox.Show("스택카를 HOME으로 불러 들이는 처리로서,원점상태나 대기상태일때만 가능합니다, " + Environment.NewLine +
                                "또한 에러 상태가 아닐때 가능합니다. " + Environment.NewLine +
                                "정말로, HOME이동 처리 하시겠읍니까..?", "HOME이동처리",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var cvc = db.ExecuteQuery("select cnvc_op_onof, cnvc_op_eror, cnvc_stop, cnvc_comm from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (cvc == null) return;
                string ls_comm = cvc.cnvc_comm;
                if (ls_comm != "1")
                {
                    MessageBox.Show("콘베어 상태가 통신정지 상태입니다.!! " + Environment.NewLine +
                                    "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                int li_hogi = Convert.ToInt32(hogi);
                string ls_onln = cvc.cnvc_op_onof;
                string ls_eror = cvc.cnvc_op_eror;
                if (ls_onln.Substring(li_hogi - 1, 1) == "0" || ls_eror.Substring(li_hogi - 1, 1) == "1")
                {
                    MessageBox.Show("해당콘베어가 수동이나 에러이므로 불가합니다...!!" + Environment.NewLine +
                                    "다시 조회후 처리바람.....!!", "재지시처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
                int rc = db.ExecuteCommand(@"update tbscrc 
                                        set scrc_mode = 'H0', scrc_mesg = 'HOME이동지령', scrc_rset = '0'
                                         where scrc_no   = {0}
                                           and scrc_onln  = '1'
                                           and scrc_pwron = '1'
                                           and scrc_emer  = '0'
                                           and scrc_eror  = '0'
                                           and scrc_mode = {1}
                                           and scrc_stat = {2}
                                           and scrc_rset  = '0'
                                           and scrc_stop = '0'
                                           and scrc_comm = '1' ",
                                               hogi, sc.scrc_mode, sc.scrc_stat);
                if (rc == 0)
                {
                    MessageBox.Show("스택카가 상태가 이미 변함... 조회후 다시 처리 바람..!!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;
            }
        }
        //********************************************************************************************//
        //* 스택카 공출 리셋 처리
        //********************************************************************************************//
        private void btnerrreset_Click(object sender, EventArgs e)
        {
            if (sc.scrc_comm != "1")
            {
                MessageBox.Show("스택카 상태가 통신정지 상태입니다.!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "리셋처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_onln != "1")
            {
                MessageBox.Show("스택카 상태가 원격모드(자동상태)일때만 가능합니다!! " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "리셋처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_pwron != "1")
            {
                MessageBox.Show("스택카 상태가 전원차단 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "리셋처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_emer != "0")
            {
                MessageBox.Show("스택카가 비상정지 상태입니다 " + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "리셋처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!(sc.scrc_stat == "0008" && sc.scrc_eror != "0"))
            {
                MessageBox.Show("스택카 상태가 에러상태가 아닙니다. 에러 리셋이 불가합니다!!" + Environment.NewLine +
                                 "다시 조회후 처리바람.....!!", "리셋처리", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (sc.scrc_stat == "0008" && sc.scrc_eror == "D")
            {
                //   if messagebox("이중입고리셋처리", "스택카의 이중입고상태를 리셋하는 처리로서, 이중시만 가능합니다, ~r~n" + &
                //                              "정말로, 이중입고 리셋 처리 하시겠읍니까..??", question!, yesno!, 2) = 2 then
                //		return
                //   end if
                //   ls_rset = '1'
                return;
            }
            string ls_rset = "0";
            if (sc.scrc_stat == "0008" && sc.scrc_eror == "E")
            {
                if (MessageBox.Show("스택카의 공출상태를 리셋하는 처리로서, 공출상태시만 가능합니다," + Environment.NewLine +
                                 "정말로, 공출 리셋 처리 하시겠읍니까..?", "공출리셋처리",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                ls_rset = "2";
            }
            if (sc.scrc_stat == "0008" && sc.scrc_eror == "Q")
            {
                if (MessageBox.Show("스택카의 DATA이상상태를 리셋하는 처리입니다, " + Environment.NewLine +
                                    "정말로, DATA이상 리셋 처리 하시겠읍니까..?", "DATA이상리셋",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                ls_rset = "3";
            }
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int rc = db.ExecuteCommand(@"update tbscrc 
                                        set scrc_rset = {0}, scrc_mesg = '에러리셋 대기'
                                          where scrc_no   = {1}
                                           and scrc_onln  = '1'
                                           and scrc_pwron = '1'
                                           and scrc_mode = {2}
                                           and scrc_stat = {3}
                                           and scrc_emer  = '0'
                                           and scrc_rset  = '0'
                                           and scrc_comm  = '1'",
                               ls_rset, hogi, sc.scrc_mode, sc.scrc_stat);
                if (rc == 0)
                {
                    MessageBox.Show("스택카가 상태가 이미 변함... 조회후 다시 처리 바람..!!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;
            }
            
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            FrmSCUse_p p = new FrmSCUse_p(hogi);
            p.ShowDialog();
            if (p.DialogResult != DialogResult.OK)
            {
                p.Dispose();return;
            }
            string iuse = "1", ouse = "1";
            if (p.radioButton1.Checked) iuse = "0"; else iuse = "1";
            if (p.radioButton3.Checked) ouse = "0"; else ouse = "1";

            p.Dispose();
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"update tbscrc set scrc_iuse = {0}, scrc_ouse = {1} where scrc_no = {2}", iuse, ouse, hogi);
            }
            DialogResult = DialogResult.OK;

        }
    }
    public class tbscrcq
    {
        public string scrc_no{get; set;}

        public string scrc_mode{get; set;}

        public string scrc_gubn{get; set;}

        public string scrc_io{get; set;}

        public string scrc_onln{get; set;}

        public string scrc_pwron{get; set;}

        public string scrc_emer{get; set;}

        public string scrc_stat{get; set;}

        public string scrc_palt{get; set;}

        public string scrc_posi{get; set;}

        public string scrc_eror{get; set;}

        public string scrc_ecod{get; set;}

        public string scrc_stop{get; set;}

        public string scrc_iuse{get; set;}

        public string scrc_ouse{get; set;}

        public string scrc_lstk{get; set;}

        public string scrc_pltn{get; set;}

        public string scrc_jno{get; set;}

        public string scrc_indx{get; set;}

        public string scrc_fstn{get; set;}

        public string scrc_tstn{get; set;}

        public string scrc_xmov{get; set;}

        public string scrc_mesg{get; set;}

        public string scrc_chdt{get; set;}

        public string scrc_comm{get; set;}

        public string scrc_rset{get; set;}
    }
}
