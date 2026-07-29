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

namespace KK5
{
    public partial class FrmRcpJob : Form
    {
        #region --- MDI Child ----------------
        private static FrmRcpJob _instance;
        public static FrmRcpJob Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmRcpJob();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        DataGridView dv1, dv2;
        private void FrmRcpJob_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
       

        string orgsql1 = @"  SELECT tbindx.indx_jno,   
         tbindx.indx_indx,   
         tbindx.indx_gubn,   
         tbindx.indx_jio,   
         tbindx.indx_hogi,   
         tbindx.indx_fstn,   
         tbindx.indx_tstn,   
         tbindx.indx_pltn,   
         tbindx.indx_lstk,   
         tbindx.indx_xmov,   
         tbindx.indx_edat,   
         tbindx.indx_sflg,   
         tbindx.indx_uflg,
         tbindx.indx_jio + tbindx.indx_sflg as indx_stat
    FROM tbindx  
   WHERE tbindx.indx_jno is not null    ";


        string orgsql2 = @"SELECT  miplti.plti_pltno ,
           miplti.plti_prod ,
           miplti.plti_pdesc ,
           miplti.plti_loc ,
           miplti.plti_lot ,
           miplti.plti_bestq ,
           miplti.plti_pksz ,
           miplti.plti_remark ,
           miplti.plti_stok ,
           miplti.plti_rqty ,
           miplti.plti_idate ,
           miplti.plti_itime ,
           miplti.plti_flag 
        FROM miplti      
        WHERE miplti.plti_pltno is not null ";
        public FrmRcpJob()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            dv1 = dataGridView1;
            dv2 = dataGridView2;
            dv1.AutoGenerateColumns = false;
            dv2.AutoGenerateColumns = false;
            dv1.RowPostPaint += Common.RowPostPaint;
            dv2.RowPostPaint += Common.RowPostPaint;

            this.FormClosed += FrmRcpJob_FormClosed;
            dv1.CellFormatting += Dv1_CellFormatting;
            dv2.CellFormatting += Dv2_CellFormatting;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.SelectionChanged += Dv1_SelectionChanged;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0)
            {
                dv2.DataSource = null;
                return;
            }
            
            retrieve2(dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString());

        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string ls;

            if(e.ColumnIndex == 0)
            {
                if(e.Value != null)
                {
                    ls = e.Value.ToString();
                    e.Value = ls.Substring(0, 14) + "-" + ls.Substring(14, 4);
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (e.Value != null)
                {
                    ls = e.Value.ToString();
                    if (ls == "A") e.Value = "원격";
                    if (ls == "R") e.Value = "RCP";
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null)
                {
                    ls = e.Value.ToString();
                    if (ls == "I") e.Value = "입고";
                    if (ls == "$") e.Value = "출고";
                    if (ls == "M") e.Value = "이동";
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    ls = e.Value.ToString();
                    if (ls == "IP") e.Value = "입고대쓰기";
                    if (ls == "IM") e.Value = "입고이동중";
                    if (ls == "IW") e.Value = "입고대도착완료";
                    if (ls == "IX") e.Value = "S/C입고작업중";
                    if (ls == "$W") e.Value = "S/C출고대기중";
                    if (ls == "$X") e.Value = "S/C출고작업중";
                    if (ls == "$F") e.Value = "S/C출고완료";
                    if (ls == "$P") e.Value = "출고대도착완료";
                    if (ls == "$M") e.Value = "출고CV이동중";
                    if (ls == "MP") e.Value = "이동대기";
                    if (ls == "MM") e.Value = "C/V이동중";

                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 5)
            {
                if (e.Value != null)
                {
                    ls = e.Value.ToString();
                    if (ls == "I") e.Value = "입고";
                    if (ls == "$") e.Value = "출고";
                    if (ls == "M") e.Value = "이동출고";
                    if (ls == "N") e.Value = "순환이동";

                    e.FormattingApplied = true;
                }
            }
            //if (e.ColumnIndex == 9)
            //{
            //    if (e.Value != null)
            //    {
            //        ls = e.Value.ToString();
            //        e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + "-" + ls.Substring(5, 2);
            //        e.FormattingApplied = true;
            //    }
            //}
        }

        private void FrmRcpJob_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = orgsql1;

            string ls1 = comboBox1.SelectedItem.ToString();
            if (ls1.Substring(0, 1) == "1") modstr = modstr + " and indx_fstn = '22' ";
            if (ls1.Substring(0, 1) == "2") modstr = modstr + " and indx_fstn = '21' ";
            if (ls1.Substring(0, 1) == "3") modstr = modstr + " and indx_jio = '$' ";
            if (ls1.Substring(0, 1) == "4") modstr = modstr + " and indx_jio = 'M' ";

            string ls2 = comboBox2.SelectedItem.ToString();
            if (ls2.Substring(0, 1) == "1") modstr = modstr + " and indx_hogi = '1' ";
            if (ls2.Substring(0, 1) == "2") modstr = modstr + " and indx_hogi = '2' ";
            if (ls2.Substring(0, 1) == "3") modstr = modstr + " and indx_hogi = '3' ";
            if (ls2.Substring(0, 1) == "4") modstr = modstr + " and indx_hogi = '4' ";
            if (ls2.Substring(0, 1) == "5") modstr = modstr + " and indx_hogi = '5' ";

             modstr = modstr + " order by indx_jno ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<tbindxq>(modstr).ToList();
                dv1.DataSource = q;         
            }
            dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
            dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }      

        private void btndone_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (sflg.Trim() == "X")
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV작업완료");
                return;
            }
            string ls = gubn + jio;
            if (ls == "AI") ai_finish();
            if (ls == "A$") ao_finish();
            if (ls == "AM") am_finish();

            if (ls == "RI") ri_finish();
            if (ls == "R$") ro_finish();
            if (ls == "RM") rm_finish();

            retrieve();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (sflg.Trim() == "X")
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV작업취소");
                return;
            }
            string ls = gubn + jio;
            if (ls == "AI") ai_cancel();
            if (ls == "A$") ao_cancel();
            if (ls == "AM") am_cancel();

            if (ls == "RI") ri_cancel();
            if (ls == "R$") ro_cancel();
            if (ls == "RM") rm_cancel();

            retrieve();
        }

        private void btnReassign_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (dv2.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();
            string prod = dv2.SelectedRows[0].Cells["plti_prod"].Value.ToString();


            if (gubn != "A") return;  //원격아님
            if (jio != "I") return;   //입고아님
            if (sflg != "P") return;  //입고대쓰기가 아님
            if (lstk.Substring(0,1) != "A") return;  //자동창고가 아님           

            string mflag = string.Empty;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                mflag = db.ExecuteQuery<string>("select mast_flag from mimast where mast_cd = '" + prod + "'").SingleOrDefault(); ;
                if (mflag == null)
                {
                    MessageBox.Show("제품마스터 가 존재하지 않읍니다");
                    return;
                }
            }

            string nlstk = string.Empty;
            using (FrmRcpRsrvLstk_p p = new FrmRcpRsrvLstk_p(mflag))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel)
                {
                    p.Dispose();
                    return;
                }
                nlstk = p.maskedTextBox1.Text;
            }

            // new location 으로부터 호기와 입고station얻기
            string hogi="", tstn = "";
            switch (nlstk.Substring(1, 2))
            {
                case "01":
                case "02":
                    hogi = "1";
                    tstn = "01";
                    break;
                case "03":
                case "04":
                    hogi = "2";
                    tstn = "03";
                    break;
                case "05":
                case "06":
                    hogi = "3";
                    tstn = "05";
                    break;
                case "07":
                case "08":
                    hogi = "4";
                    tstn = "07";
                    break;
                case "09":
                case "10":
                    hogi = "5";
                    tstn = "09";
                    break;
                default:
                    return;
                    break;
            }

            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_reassign_cell(jno, gubn, jio, pltno, lstk, sflg, prod, nlstk, hogi, tstn);
                        if (rc == 1) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback(); MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();
                if (rc == -1) MessageBox.Show("데이타 상태가 변했읍니다(tbindx)");
                if (rc == -2) MessageBox.Show("목적셀의 상태가 변했읍니다(To location)");
                if (rc == -3) MessageBox.Show("시작셀 상태가 변했읍니다(from location)");
                if (rc == -4) MessageBox.Show("재고 상태가 변했읍니다(miplti)");
                if (rc == 1) MessageBox.Show("재할당 성공입니다...!");
            }
            retrieve();
        }

        private void btnoutwrite_Click(object sender, EventArgs e)
        {           
            string sql = @"INSERT INTO tbindx  
                                ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
                                  indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
                                  indx_edat,     indx_sflg,       indx_uflg )  
                            VALUES ( {0},         {1},            'R',             'M',           {2},
                                     {3},         '43',           '99999999',      'Y000000',     'N',
			                         '',          'P',            '0') ";

            string fstn = string.Empty;
            using (FrmRcpOstnWrite_p p = new FrmRcpOstnWrite_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel)
                {
                    p.Dispose();
                    return;
                }
                fstn = p.comboBox1.SelectedItem.ToString();
            }          

            string msg = "";
            DBDataContext db = new DBDataContext(Config.DBCon);            
            try
            {
                int lhno = Convert.ToInt32(fstn) / 2;
                string hogi = lhno.ToString("0");
                int rc = 0;
                using (TransactionScope sc = new TransactionScope())
                {
                    string jno = "";
                    rc = db.p_get_indx_jno("4", ref jno);
                    if (jno == "")
                    {
                        msg = "작업번호 얻기 실패";
                        return;
                    }
                    string indx = jno.Substring(jno.Length - 4, 4);
                    rc = db.ExecuteCommand(sql, jno, indx, hogi, fstn);
                    if (rc > 0)
                    {
                        db.SubmitChanges();
                        sc.Complete();
                        msg = "출고대 쓰기 성공";
                    }
                    else
                    {
                        msg = "출고대 쓰기 실패";
                    }
                }
                MessageBox.Show(msg);
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }

            retrieve();
        }

        private void btninptwrite_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tbindx  
                                ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
                                  indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
                                  indx_edat,     indx_sflg,       indx_uflg )  
                            VALUES ( {0},         {1},            'R',             'I',           {2},
                                    '21',         {3},            '99999999',      {4},           'I',
			                         '',          'W',             '0') ";
            string tstn = "";
            string lstk = "";
            string indx = "";
            using (FrmRcpIstnWrite_p p = new FrmRcpIstnWrite_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                           
                tstn = p.tstn;
                lstk = p.loca;
                indx = p.indx;
            }


            string hogi;  // get hogi
            switch (lstk.Substring(1, 2))
            {
                case "01": case "02":
                    hogi = "1";
                    break;
                case "03": case "04":
                    hogi = "2";
                    break;
                case "05": case "06":
                    hogi = "3";
                    break;
                case "07": case "08":
                    hogi = "4";
                    break;
                case "09": case "10":
                    hogi = "5";
                    break;
                default:
                    return;
                    break;
            }

            string msg = "";
            DBDataContext db = new DBDataContext(Config.DBCon);
            try
            {              
                int rc = 0;
                using (TransactionScope sc = new TransactionScope())
                {
                    string jno = "";
                    rc = db.p_get_indx_jno("4", ref jno);
                    if (jno == "")
                    {
                        msg = "작업번호 얻기 실패";
                        return;
                    }
                  
                    jno = jno.Substring(0, 14) + indx;
                   
                    rc = db.ExecuteCommand(sql, jno, indx, hogi, tstn, lstk);
                    if (rc > 0)
                    {                      
                        sc.Complete();
                        msg = "입고대 쓰기 성공";
                    }
                    else
                    {
                        msg = "입고대 쓰기 실패";
                    }
                }
                MessageBox.Show(msg);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            retrieve();
        }

        private void btntry_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();
            if(sflg != "M")
            {
                MessageBox.Show("CV 이동중만 가능함");
                return;
            }
            if (MessageBox.Show("재지시하겠읍니까?" + Environment.NewLine + Environment.NewLine + "21, 22에서만 가능함!", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = @"update tbindx set indx_sflg = 'P' where indx_jno = '" + jno + "' and indx_sflg = '" + sflg + "'";
            int rc = db.ExecuteCommand(sql);

            if (rc > 0)
            {
                db.SubmitChanges();
                MessageBox.Show("재지시 성공...!" + rc.ToString());
            }
            else MessageBox.Show("재지시 실패...!" + rc.ToString());

            retrieve();
        }

        private void retrieve2(string pltno)
        {
            string modstr = orgsql2;
            modstr = modstr + " and miplti.plti_pltno = '" + pltno + "' ";

            DBDataContext db = new DBDataContext(Config.DBCon);
            var q = db.ExecuteQuery<mipltiforRcp>(modstr).ToList();
            dv2.DataSource = q;

        }

        #region ---완료, 취소 처리들 ---
        private void ai_finish()  // 원격모드 강제입고완료
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("강제 입고완료하시겠읍니까?", "INV입고완료", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);

            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV입고완료");
                return;
            }
            sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_gubn = 'A' and indx_jio = 'I'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }
            rc = 1;
            try
            {
                string sql1 = 
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} and indx_gubn = 'A' and indx_jio = 'I'";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;
                   
                    db.ExecuteCommand(sql2);

                    db.SubmitChanges();
                    sc.Complete();                   
                }
                nn:;
                if (rc > 0) MessageBox.Show("강제입고완료처리 성공...!");
                else MessageBox.Show("강제입고완료처리 실패...!");
         
            }
            catch(Exception E) { MessageBox.Show(E.Message); }
         
        }

        private void ai_cancel()  // 원격모드 강제입고취소
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("강제 입고취소하시겠읍니까?", "INV입고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);

            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV입고취소");
                return;
            }
            sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_gubn = 'A' and indx_jio = 'I'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} and indx_gubn = 'A' and indx_jio = 'I'";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;
                    
                    db.ExecuteCommand(sql2);

                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("강제입고취소처리 성공...!");
                else MessageBox.Show("강제입고취소처리 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }
        }

        private void ao_finish()   // 원격모드 강제출고완료
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("선택된 파렛트를 출고완료 하시겠읍니까?" + Environment.NewLine + "수동으로 파렛트를 꺼냈읍니까 ? ", "출고",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV출고완료");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    if (pltno != "99999999")   // 2014/08/10
                    {
                        rc = db.ExecuteCommand(sql1, jno);
                        if (rc == 0) goto nn;
                    }
                    db.ExecuteCommand(sql2);

                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("강제출고완료처리 성공...!");
                else MessageBox.Show("강제출고완료처리 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }
        }

        private void ao_cancel()    // 원격모드 강제출고취소
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("선택된 파렛트를 출고취소 하시겠읍니까?", "INV출고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV출고취소");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    if (pltno != "99999999")   // 2014/08/10
                    {
                        rc = db.ExecuteCommand(sql1, jno);
                        if (rc == 0) goto nn;
                    }
                    db.ExecuteCommand(sql2);

                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("강제출고취소 성공...!");
                else MessageBox.Show("강제출고취소 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }

        private void am_finish()    // 아적이동 완료처리
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("야적존으로 파렛트를 이동완료 하시겠읍니까?", "INV이동=>야적완료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV야적이동완료");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }
                      
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;   
                                     
                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("INV야적이동완료 성공...!");
                else MessageBox.Show("INV야적이동완료 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }
        }

        private void am_cancel()    // 아적이동 취소처리
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("야적존으로 파렛트의 이동을 취소하시겠읍니까?", "INV이동=>야적취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "INV야적이동취소");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("INV야적이동취소 성공...!");
                else MessageBox.Show("INV야적이동취소 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }

        private void ri_finish()    // RCP자체입고완료
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 입고완료하시겠읍니까?", "RCP자체입고완료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP자체입고완료");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP자체입고완료 성공...!");
                else MessageBox.Show("RCP자체입고완료 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }
        }
        private void ri_cancel()    // RCP자체입고취소
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 입고취소하시겠읍니까?", "RCP자체입고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP자체입고취소");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP자체입고취소 성공...!");
                else MessageBox.Show("RCP자체입고취소 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }
        private void rm_finish()    // RCP자체이동작업 완료
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 이동작업을 완료하시겠읍니까?", "RCP이동=>야적",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP야적이동완료");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP야적이동완료 성공...!");
                else MessageBox.Show("RCP야적이동완료 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }

        private void rm_cancel()    // RCP자체이동작업 취소
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 이동작업을 취소하시겠읍니까?", "RCP이동=>야적취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP야적이동취소");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP야적이동취소 성공...!");
                else MessageBox.Show("RCP야적이동취소 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }
        private void ro_finish()    // RCP자체출고완료
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 출고작업을 완료하시겠읍니까?", "RCP자체출고완료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP자체출고완료");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'F',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP자체출고완료 성공...!");
                else MessageBox.Show("RCP자체출고완료 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ro_cancel()     // RCP자체출고취소
        {
            if (dv1.SelectedRows.Count == 0) return;

            string jno = dv1.SelectedRows[0].Cells["indx_jno"].Value.ToString();
            string gubn = dv1.SelectedRows[0].Cells["indx_gubn"].Value.ToString();
            string jio = dv1.SelectedRows[0].Cells["indx_jio"].Value.ToString();
            string hogi = dv1.SelectedRows[0].Cells["indx_hogi"].Value.ToString();
            string fstn = dv1.SelectedRows[0].Cells["indx_fstn"].Value.ToString();
            string tstn = dv1.SelectedRows[0].Cells["indx_tstn"].Value.ToString();
            string pltno = dv1.SelectedRows[0].Cells["indx_pltn"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["indx_lstk"].Value.ToString();
            string xmov = dv1.SelectedRows[0].Cells["indx_xmov"].Value.ToString();
            string sflg = dv1.SelectedRows[0].Cells["indx_sflg"].Value.ToString();

            if (MessageBox.Show("RCP자체 출고작업을 취소하시겠읍니까 ? ", "RCP자체출고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = "select count(*) from tbindx where indx_jno = '" + jno + "' and indx_sflg = 'X' ";
            int rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc > 0)
            {
                MessageBox.Show("모니터링에서 처리하세요", "RCP자체출고취소");
                return;
            }

            sql = "select count(*) from tbindx where indx_jno = '" + jno + "'";
            rc = db.ExecuteQuery<int>(sql).SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("상태가 변했읍니다", "확인");
                return;
            }

            rc = 1;
            try
            {
                string sql1 =
                    @"insert into tbevnt (
                             evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn, 
                             evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg )
                     select  indx_gubn, indx_jio,  indx_hogi, indx_fstn, indx_tstn,
                             indx_pltn, indx_lstk, indx_xmov, indx_sflg, 'C',        '0'
                     from tbindx where indx_jno = {0} ";

                string sql2 = @"delete from tbindx where indx_jno = '" + jno + "'";

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(sql1, jno);
                    if (rc == 0) goto nn;

                    db.ExecuteCommand(sql2);
                    db.SubmitChanges();
                    sc.Complete();
                }
                nn:;
                if (rc > 0) MessageBox.Show("RCP자체출고취소 성공...!");
                else MessageBox.Show("RCP자체출고취소 실패...!");

            }
            catch (Exception E) { MessageBox.Show(E.Message); }

        }

        #endregion


    }
    public class tbindxq
    {
        public string indx_jno { get; set; }
        public string indx_hogi { get; set; }
        public string indx_gubn { get; set; }
        public string indx_jio { get; set; }
        public string indx_stat { get; set; }
        public string indx_xmov { get; set; }
        public string indx_fstn { get; set; }
        public string indx_tstn { get; set; }
        public string indx_pltn { get; set; }
        public string indx_lstk { get; set; }
        public string indx_sflg { get; set; }
    }
    public class mipltiforRcp
    {
        public string plti_pltno { get; set; }
        public string plti_lstk { get; set; }
        public string plti_prod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal plti_pksz { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public string plti_remark { get; set; }
        public string plti_cycl_date { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_flag { get; set; }
    }
}
