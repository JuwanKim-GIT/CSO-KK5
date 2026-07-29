using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Data.Common;
using System.Transactions;
using System.IO;

namespace KK5
{
    public partial class FrmItemLstk : Form
    {
        #region --- MDI Child ----------------
        private static FrmItemLstk _instance;
        public static FrmItemLstk Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmItemLstk();

                return _instance;
            }
        }
        private void FrmItemLstk_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string modstr = string.Empty;
        public FrmItemLstk()
        {
            InitializeComponent();
          
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            this.Size = new Size(1280, 1024);
            this.FormClosed += FrmItemLstk_FormClosed;
            dv = dataGridView1;

            dv.RowPostPaint += Common.RowPostPaint;

            if(Config.UserLevel != "1")
            {
                btnjchg.Enabled = false;
                btnmv.Enabled = false;
                btnout.Enabled = false;
                if (Config.UserLevel != "3")  btnremark.Enabled = false;
                btnuse.Enabled = false;
                btnLabel.Enabled = false;
            }
        }

        #region Query ---------------------------------------
        string qsql = "SELECT  " +
                          " milstk.lstk_no , " +
                          " milstk.lstk_use , " +
                          " milstk.lstk_io , " +
                          " milstk.lstk_stat ," +
                          " miplti.plti_pltno ," +
                          " miplti.plti_prod ," +
                          " miplti.plti_oprod ," +
                          " miplti.plti_pdesc ," +
                          " miplti.plti_loc ," +
                          " miplti.plti_lot ," +
                          " miplti.plti_bestq ," +
                          " miplti.plti_pksz ," +
                          " miplti.plti_remark ," +
                          " miplti.plti_stok ," +
                          " miplti.plti_rqty ," +
                          " miplti.plti_idate ," +
                          " miplti.plti_itime ," +
                          " miplti.plti_flag " +
                          " FROM milstk ,  miplti  where lstk_no = plti_lstk ";

        #endregion
        
        private void FrmItemLstk_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "ALL";
          
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.Columns["plti_stok"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dv.Columns["plti_rqty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy/MM/dd");            

        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                (sender as DataGridView).CopyClipboardData();
        }


        private void query()
        {
            modstr = qsql;

            string ls = comboBox1.Text;
            if (ls != "ALL")
            {
                if (ls.Substring(0, 1) == "A") modstr = modstr + " and lstk_no like 'A%' ";
                if (ls.Substring(0, 1) == "F") modstr = modstr + " and lstk_no like 'F%' ";
                if (ls.Substring(0, 1) == "Y") modstr = modstr + " and lstk_no like 'Y%' ";
            }
            string pltno = tbPlt.Text.Trim();
            if (pltno != "")
            {
                modstr = modstr + " and plti_pltno like '" + pltno + "%'";
            }
            string ls_m = tbProd.Text.Trim();
            if (ls_m != "") modstr = modstr + " and plti_prod like '" + ls_m + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";

            string ls_lot1 = tbLot.Text.Trim();
            if (ls_lot1 != "") modstr = modstr + " and plti_lot like '" + ls_lot1 + "%'";

            if (checkBox1.Checked)
            {
                string datefrom = dateTimePicker1.Text.Replace("-", "/");
                string dateto = dateTimePicker2.Text.Replace("-", "/");
                modstr = modstr + " and plti_idate >= '" + datefrom + "'";
                modstr = modstr + " and plti_idate <= '" + dateto + "'";
            }

            string lsloc = comboBox2.SelectedItem.ToString();
            if (lsloc != "ALL")
            {
                lsloc = lsloc.Substring(0, 4);
                if (lsloc != "") modstr = modstr + " and plti_loc = '" + lsloc + "'";
            }

            string bestq = comboBox3.SelectedItem.ToString();
            if (bestq != "ALL")
            {
                bestq = bestq.Substring(0, 1);
                modstr = modstr + " and plti_bestq = '" + bestq + "'";
            }
     
            if (chkyardlstk.Checked)
            {
                modstr = modstr + " and plti_lstk <> 'Y000000'";
            }
            modstr = modstr + " order by miplti.plti_prod, miplti.plti_lot, miplti.plti_loc, miplti.plti_lstk ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv.DataSource = new SortableBindingList<ItemLstk>(db.ExecuteQuery<ItemLstk>(modstr).ToList());
                //var ss = db.ExecuteQuery<ItemLstk>(modstr).ToList();
                //dv.DataSource = ss;

                dv.TopLeftHeaderCell.Value = dv.RowCount.ToString();
                dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
        private void btnqry_Click(object sender, EventArgs e)
        {
            query();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 9 )
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    if (s == "0") e.Value = "";
                    else if (s == "I") e.Value = "입고";
                    else if (s == "$") e.Value = "출고";
                    else if (s == "M") e.Value = "이동";

                    e.FormattingApplied = true;
                }
            }
            else if (e.ColumnIndex == 10)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    if (s == "10") e.Value = "재고";
                    else if (s == "$R") e.Value = "출고예약";
                    else if (s == "$X") e.Value = "출고중";
                    else if (s == "$Z") e.Value = "출고완료";
                    else if (s == "$E") e.Value = "공출고";
                    else if (s == "IR") e.Value = "입고고예약";
                    else if (s == "IX") e.Value = "입고중";
                    else if (s == "ID") e.Value = "이중입고";

                    e.FormattingApplied = true;
                }
            }
         
        }

        private void btnremark_Click(object sender, EventArgs e)
        {

            if (dv.SelectedRows.Count == 0) return;

            int saverow = dv.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv.CurrentCell.RowIndex;

            string remark = string.Empty;
            using (FrmRemark_p p = new FrmRemark_p())
            {
                p.ShowDialog();
                if (DialogResult.OK == p.DialogResult)
                {
                    remark = p.tbRemark.Text;
                }
            }
                
            try
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                DBDataContext db = new DBDataContext(Config.DBCon);
                foreach (DataGridViewRow r in rr)
                {
                    string lstk = r.Cells["lstk_no"].Value.ToString();
                    string pltno = r.Cells["plti_pltno"].Value.ToString();
                    string prod = r.Cells["plti_prod"].Value.ToString();
                    string lot = r.Cells["plti_lot"].Value.ToString();
                    string loc = r.Cells["plti_loc"].Value.ToString();
                    string bestq = r.Cells["plti_bestq"].Value.ToString();

                    string lsql = @"update miplti set plti_remark = {0}
                                where plti_lstk = {1} 
                                and plti_pltno = {2} 
                                and plti_prod = {3} 
                                and plti_loc = {4} 
                                and plti_lot = {5} 
                                and plti_bestq = {6} ";
                    db.ExecuteCommand(lsql, remark, lstk, pltno, prod, loc, lot, bestq);

                    r.Cells["plti_remark"].Value = remark;
                }
               
                //query();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
       

            try
            {
                dv.FirstDisplayedScrollingRowIndex = saverow;
                dv.CurrentCell = dv.Rows[rowIndex].Cells[0];
            }
            catch (Exception E) { }
        }
        private void btnuse_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;
   
            string remark = string.Empty;
            string ls_use = "1";

            using (Frmuse_p p = new Frmuse_p())
            {
                p.ShowDialog();
                if (DialogResult.Cancel == p.DialogResult) return;
                if (p.radioButton1.Checked) ls_use = "1";
                else if (p.radioButton2.Checked) ls_use = "0";
                else return;
            }

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv.SelectedRows)
            {
                rr.Insert(0, r);
            }
            
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    foreach (DataGridViewRow r in rr)
                    {
                        string lstk = r.Cells["lstk_no"].Value.ToString();

                        string lsql = @"update milstk set lstk_use = {0} where lstk_no = {1} ";
                        db.ExecuteCommand(lsql, ls_use, lstk);

                        r.Cells["lstk_use"].Value = ls_use;
                    }
                }
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
  
        private void btnout_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            // check 자동창고

            if (MessageBox.Show("이동출고 하시겠읍니까?", "이동출고 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int saverow = dv.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv.CurrentCell.RowIndex;                    

            FrmOstnSel_p p = new FrmOstnSel_p();
            p.ShowDialog();

            string indx_tstn = "45";
            if (p.DialogResult == DialogResult.Cancel) return;

            if (p.radioButton1.Checked) indx_tstn = "43";
            if (p.radioButton2.Checked) indx_tstn = "45";

            DBDataContext ctx = new DBDataContext(Config.DBCon);
            string sql = "select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01'";

            string bonofs = "00000000", berors = "00000000";
            var ss = ctx.ExecuteQuery(sql);
            foreach (dynamic s in ss)
            {
                bonofs = s.cnvc_op_onof;
                berors = s.cnvc_op_eror;
            }
            string bonof = bonofs.Substring(0, 5);
            string beror = bonofs.Substring(0, 5);


            sql = "select scrc_onln, scrc_stop from tbscrc order by scrc_no";
            string[] onln = new string[5];// { "0", "0", "0", "0", "0" };
            string[] stop = new string[5];// { "0", "0", "0", "0", "0" };

            int hogi = 0;

            var sc = ctx.ExecuteQuery(sql);
            foreach (dynamic s in sc)
            {
                onln[hogi] = s.scrc_onln;
                stop[hogi] = s.scrc_stop;
                hogi++;
            }

            int lp = 0;
            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                string indx_lstk = r.Cells["lstk_no"].Value.ToString();
                string indx_pltn = r.Cells["plti_pltno"].Value.ToString();
                string lstk_use = r.Cells["lstk_use"].Value.ToString();
                string prod = r.Cells["plti_prod"].Value.ToString();
                string loc = r.Cells["plti_loc"].Value.ToString();
                string lot = r.Cells["plti_lot"].Value.ToString();
                string bestq = r.Cells["plti_bestq"].Value.ToString();
                int stok = Convert.ToInt32(r.Cells["plti_stok"].Value.ToString());
                int rqty = Convert.ToInt32(r.Cells["plti_rqty"].Value.ToString());
                string stat = r.Cells["lstk_stat"].Value.ToString();

                if (lstk_use == "0")
                {
                    if (MessageBox.Show("금지로 되어 있어 이동출고 불가합니다!~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }

                if (rqty > 0)
                {
                    if (MessageBox.Show("출고예약이 되어 있어 이동출고 불가합니다?~r~n계속하시겠읍니까?", "확인",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }

                if (indx_lstk.Substring(0, 1) != "A")
                {
                    if (MessageBox.Show("자동창고만 가능합니다!~r~n계속하시겠읍니까?", "확인",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }

                hogi = Convert.ToInt32(utils.f_gethogi(indx_lstk));
                if (onln[hogi - 1] != "1")
                {
                    if (MessageBox.Show("크레인 No:" + (hogi-1).ToString() + " 원격이 아닙니다~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }
                if (onln[hogi - 1] != "1")
                {
                    if (MessageBox.Show("크레인 No:" + (hogi - 1).ToString() + " 입출중지입니다.~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }
                if (bonof.Substring(hogi - 1, 1) != "1")
                {
                    if (MessageBox.Show("OP 판넬 No:" + (hogi - 1).ToString() + " 수동입니다.~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }
                if (beror.Substring(hogi - 1, 1) != "1")
                {
                    if (MessageBox.Show("OP 판넬 No:" + (hogi - 1).ToString() + " 에러입니다.~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }
                if (stat != "10")
                {
                    if (MessageBox.Show("보관위치 :" + indx_lstk + " 재고상태가 아닙니다.~r~n계속하시겠읍니까?", "확인",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        goto nn;
                }
                sql = @"select count(*) from miplti where plti_lstk = {0} and plti_pltno = {1} and plti_rqty > 0";
                int c = ctx.ExecuteQuery<int>(sql, indx_lstk, indx_pltn).SingleOrDefault();
                if (c != 0) break;
              
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {

                        string str = "update milstk set lstk_io = 'M', lstk_stat = '$X' where lstk_stat = '10' and lstk_no = '" + indx_lstk + "'";
                        int rc = ctx.ExecuteCommand(str);
                        if (rc == 0) goto nn;
                       
                        string indx_hogi = utils.f_gethogi(indx_lstk);
                        string indx_fstn = (Convert.ToInt32(indx_hogi) * 2).ToString("00");
                
                        string indx_jno = utils.f_get_indx_jno(ctx, '3');
                       
                        string indx_indx = indx_jno.Substring(indx_jno.Length - 4);

                        tbindx t = new tbindx();
                        t.indx_jno = indx_jno; t.indx_indx = indx_indx; t.indx_gubn = "A"; t.indx_jio = "$";
                        t.indx_hogi = indx_hogi; t.indx_fstn = indx_fstn; t.indx_tstn = indx_tstn;  t.indx_pltn = indx_pltn;
                        t.indx_lstk = indx_lstk; t.indx_xmov = "M"; t.indx_edat = ""; t.indx_sflg = "W"; t.indx_uflg = "0";

                        ctx.tbindxes.InsertOnSubmit(t);
                        ctx.SubmitChanges();
                        scope.Complete();

                        r.Cells["lstk_io"].Value = "M";
                        r.Cells["lstk_stat"].Value = "$X";
                    }

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Error");
                    return;
                }

                lp++;

                nn:;

                statusStrip1.Text = lp.ToString();
            }
           
        }
      
    
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void btnmv_Click(object sender, EventArgs e)
        {
            int ret = 0;
            string pltno, lstk, prod, lot, loc, bestq;
            decimal stok, rqty, sqty;
            string loca, qty, dloca;

            DataGridView dv = dataGridView1;

            if (dv.SelectedRows.Count == 0) return;

            DataGridViewRow SelRow = dv.SelectedRows[0];

            if (Convert.ToDecimal(SelRow.Cells["plti_rqty"].Value.ToString()) > 0)
            {
                MessageBox.Show("확인", "예약이 되어 있어 재고이동불가합니다");
                return;
            }
            loca = SelRow.Cells["lstk_no"].Value.ToString();
            qty = SelRow.Cells["plti_stok"].Value.ToString();

            if (loca.Substring(0, 1) != "Y") return;
            if (loca.Substring(1, 6) == "000000") return;
            FrmMyMoveSel_p p = new FrmMyMoveSel_p(qty);
            p.ShowDialog();
            if (p.DialogResult != DialogResult.OK)
            {
                p.Dispose();
                return;
            }
            
            dloca = "Y" + p.loca.Replace("-", "");
            qty = p.qty;
            p.Dispose();

            if (loca == p.loca) { MessageBox.Show("자기자신 이동불가합니다?", "확인"); return; }

            int lc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                lc = db.ExecuteQuery<int>("select count(*) from milstk where lstk_no = '" + dloca + "'").SingleOrDefault();
            }
                            
            if (lc == 0) { MessageBox.Show("야적셀위치가 존재하지않읍니다", "확인"); return; }
            if (qty == "ALL")
            {
                using (DBDataContext db = new DBDataContext())
                {
                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    db.Connection.open();
                    foreach (DataGridViewRow r in rr)
                    {
                        pltno = r.Cells["plti_pltno"].Value.ToString();
                        lstk = r.Cells["lstk_no"].Value.ToString();
                        prod = r.Cells["plti_prod"].Value.ToString();
                        loc = r.Cells["plti_loc"].Value.ToString();
                        lot = r.Cells["plti_lot"].Value.ToString();
                        bestq = r.Cells["plti_bestq"].Value.ToString();
                        stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                        rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                        sqty = stok;
                        try
                        {
                            using (db.Transaction = db.Connection.BeginTransaction())
                            {
                                ret = db.p_pltimove_yardtoyard(lstk, dloca, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
                                if (ret == 1)
                                {
                                    db.Transaction.Commit();
                                }
                                else
                                {
                                    db.Transaction.Rollback();
                                    break;
                                }
                            }
                        }
                        catch (Exception E) { MessageBox.Show(E.Message); }
                    }
                    db.Connection.Close();
                }
            }
            else
            {
                DataGridViewRow r = SelRow;

                pltno = r.Cells["plti_pltno"].Value.ToString();
                lstk = r.Cells["lstk_no"].Value.ToString();
                prod = r.Cells["plti_prod"].Value.ToString();
                loc = r.Cells["plti_loc"].Value.ToString();
                lot = r.Cells["plti_lot"].Value.ToString();
                bestq = r.Cells["plti_bestq"].Value.ToString();
                stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                sqty = Convert.ToDecimal(qty);

                using (DBDataContext db = new DBDataContext())
                {
                    db.Connection.open();
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            ret = db.p_pltimove_yardtoyard(lstk, dloca, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
                            if (ret == 1)
                            {
                                db.Transaction.Commit();
                            }
                            else
                            {
                                db.Transaction.Rollback();                                
                            }
                        }catch(Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                        }
                    }
                    db.Connection.Close();
                }                
            }
            if (ret != 1) MessageBox.Show("실패", "재고이동이 실패했읍니다.");
            query();
        }

        private void btnLabel_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            if (dv.SelectedRows.Count > 1)
            {
                MessageBox.Show("한개의 행만 선택하세요!");
                return;
            }
            string lstk = dv.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            string pltno = dv.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            if (pltno == "00000000") return;

            if (MessageBox.Show("라벨 재발행 하시겠읍니까?", "확인",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            
            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.p_labelprn(lstk, pltno, "1");
            }
            if (rc == -1) MessageBox.Show("상태변함");
            if (rc == -2) MessageBox.Show("상태변함2");
            if (rc == -3) MessageBox.Show("상태변함3");
            if (rc == -99) MessageBox.Show("중복발행");
            if (rc == 0) MessageBox.Show("DB Error");
            if (rc ==1) MessageBox.Show("OK");

        }
      
        private void btnexcel_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dv);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal stok = 0;
            decimal rqty = 0;
            decimal ltqty = 0;
            decimal ltqty1 = 0;   // add
            decimal pksz = 0;
            int cc = 0;
            if (dv.RowCount > 0)
            {

//                foreach (DataGridViewRow r in dataGridView1.Rows) // 총량 연산이 맞지 않아 수정함(220920)
//                {
//                    stok = stok + Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
//                    rqty = rqty + Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
//                    pksz = Convert.ToDecimal(r.Cells["plti_pksz"].Value.ToString());
//                    cc++;
//                    ltqty = ltqty + (stok + rqty) * pksz;
//                }

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    stok = stok + Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    rqty = rqty + Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    pksz = Convert.ToDecimal(r.Cells["plti_pksz"].Value.ToString());
                    cc++;
                    ltqty = ltqty + (stok + rqty) * pksz;
                }
                lblstock.Text = stok.ToString("#,###,##0");
                lblrqty.Text = rqty.ToString("#,###,##0");
                lblltqty.Text = ltqty.ToString("###,###,###,##0.000");

            }
            else
            {
                lblrqty.Text = "0";
                lblstock.Text = "0";
                lblrqty.Text = "0";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnjchg_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
                  

            using (FrmChangeType_p p = new FrmChangeType_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                if (p.radioButton1.Checked) ChangeProd();
                if (p.radioButton2.Checked) ChangeLoc();
                if (p.radioButton3.Checked) ChangeLot();
                if (p.radioButton4.Checked) ChangeQty();
                if (p.radioButton5.Checked) Changestatus();
            }
        }
        private void Changestatus()
        {
            string nbestq = "";
               
            int saverow = dv.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv.CurrentCell.RowIndex;
            using (FrmChangeStatus_p p = new FrmChangeStatus_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                if (p.radioButton1.Checked) nbestq = "";
                if (p.radioButton2.Checked) nbestq = "S";
                if (p.radioButton3.Checked) nbestq = "Q";
            }

            string pltno = "";
            string lstk = "";
            string loc = "";
            string lot = "";
            string bestq = "";

            string prod = "";
            decimal rqty = 0, stok = 0;
            string stat = "";
            string io = "";
            int rc = 0;
            int lp = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();

                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    loc = r.Cells["plti_loc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    io = r.Cells["lstk_io"].Value.ToString();
                    stat = r.Cells["lstk_stat"].Value.ToString();
                    if (nbestq == bestq) continue;

                    if (stat != "10") continue;
                    if (io != "0") continue;
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_changeStatus(prod, loc, lot, bestq, nbestq, pltno, lstk);
                            if (rc != 1)
                            {
                                db.Transaction.Rollback();
                                break;
                            }
                            db.Transaction.Commit();
                            lp++;

                            r.Cells["plti_bestq"].Value = nbestq;
                        }
                        catch(Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                            break;
                        }
                    }                 
                }
                db.Connection.Close();

                if (rc == -1) MessageBox.Show("날짜얻는데 실패...!");
                if (rc == -2) MessageBox.Show("제품코드가 존재하지 않읍니다...!");
                if (rc == -3) MessageBox.Show("재고상태변함...!");
                if (rc == -4) MessageBox.Show("재고상태변함 혹은 이력기록실패...!");
                if (rc == -5) MessageBox.Show("재고상태변함(old code)..!");
                if (rc == -99) MessageBox.Show("중복...!");
                MessageBox.Show(lp.ToString() + " 개의 record가 변경됨!!");
               
            }
        }

        private void ChangeProd()
        {
            string prod = "";
            string desc = "";
            decimal pksz = 0;
            using(FrmChangeProd_p p = new FrmChangeProd_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                pksz = Convert.ToDecimal(p.dataGridView1.SelectedRows[0].Cells["mast_vol"].Value.ToString());
                prod = p.dataGridView1.SelectedRows[0].Cells["mast_cd"].Value.ToString();
                desc = p.dataGridView1.SelectedRows[0].Cells["mast_desc"].Value.ToString();
            }

            string pltno = "";
            string lstk = "";
            string loc = "";
            string lot = "";
            string bestq = "";

            string pprod = "";
            decimal rqty = 0, stok = 0;
            string stat = "";
            string io = "";
            int rc = 0;
            int lp = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                db.Connection.open();
                foreach (DataGridViewRow r in rr)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    pprod = r.Cells["plti_prod"].Value.ToString();
                    loc = r.Cells["plti_loc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    io = r.Cells["lstk_io"].Value.ToString();
                    stat = r.Cells["lstk_stat"].Value.ToString();
                    if (pprod == prod) continue;

                    if (stat != "10") continue;
                    if (io != "0") continue;
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_changeprod(pprod, prod, loc, lot, bestq, pltno, lstk);
                            if (rc != 1)
                            {
                                db.Transaction.Rollback();
                                break;
                            }
                            db.Transaction.Commit();
                            lp++;

                            r.Cells["plti_prod"].Value = prod;
                            r.Cells["plti_pdesc"].Value = desc;
                            r.Cells["plti_pksz"].Value = pksz;
                        }
                        catch (Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                            break;
                        }
                    }
                }
                db.Connection.Close();

                if (rc == -1) MessageBox.Show("날짜얻는데 실패...!");
                if (rc == -2) MessageBox.Show("제품코드가 존재하지 않읍니다...!");
                if (rc == -3) MessageBox.Show("재고상태변함...!");
                if (rc == -4) MessageBox.Show("재고상태변함 혹은 이력기록실패...!");
                if (rc == -5) MessageBox.Show("재고상태변함(old code)..!");
                if (rc == -99) MessageBox.Show("중복...!");
                MessageBox.Show(lp.ToString() + " 개의 record가 변경됨!!");
                                       
            }
        }
  
        private void ChangeLoc()
        {
            string loc = "";
            using (FrmChangeLoc_p p = new FrmChangeLoc_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                loc = p.comboBox1.SelectedItem.ToString().Substring(0,4);
            }

            string pltno = "";
            string lstk = "";
            string prod = "";

            string lot = "";
            string bestq = "";
            string ploc = "";

            decimal rqty = 0, stok = 0;
            string stat = "";
            string io = "";
            int rc = 0;
            int lp = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    ploc = r.Cells["plti_loc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    io = r.Cells["lstk_io"].Value.ToString();
                    stat = r.Cells["lstk_stat"].Value.ToString();
                    if (ploc == loc) continue;

                    if (stat != "10") continue;
                    if (io != "0") continue;                    
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_changeloc(prod, ploc, loc, lot, bestq, pltno, lstk);
                            if (rc != 1)
                            {
                                db.Transaction.Rollback();
                                break;
                            }
                            db.Transaction.Commit();
                            lp++;

                            r.Cells["plti_loc"].Value = loc;
                        }
                        catch (Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                            break;
                        }
                    }
                }
                db.Connection.Close();
                if (rc == -1) MessageBox.Show("날짜얻는데 실패...!");
                if (rc == -2) MessageBox.Show("재고상태변함...!");
                if (rc == -3) MessageBox.Show("재고상태변함 혹은 이력기록실패...!");
                if (rc == -4) MessageBox.Show("재고상태변함(old code)..!");
                if (rc == -99) MessageBox.Show("중복...!");
                MessageBox.Show(lp.ToString() + " 개의 record가 변경됨!!");
               
                         
            }
        }
        private void ChangeLot()
        {
            string lot = "";
            using (FrmChangeLot_p p = new FrmChangeLot_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                lot = p.textBox2.Text;
            }
            
            string pltno = "";
            string lstk = "";
            string prod = "";

            string plot = "";
            string bestq = "";
            string loc = "";

            decimal rqty = 0, stok = 0;
            string stat = "";
            string io = "";
            int rc = 0;
            int lp = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    loc = r.Cells["plti_loc"].Value.ToString();
                    plot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    io = r.Cells["lstk_io"].Value.ToString();
                    stat = r.Cells["lstk_stat"].Value.ToString();
                    if (plot == lot) continue;

                    if (stat != "10") continue;
                    if (io != "0") continue;
                                        
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_changelot(prod, loc, plot, lot, bestq, pltno, lstk);
                            if (rc != 1)
                            {
                                db.Transaction.Rollback();
                                break;
                            }
                            db.Transaction.Commit();
                            lp++;

                            r.Cells["plti_lot"].Value = lot.ToUpper();
                        }
                        catch (Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                            break;
                        }
                    }
                }
                db.Connection.Close();

                if (rc == -1) MessageBox.Show("날짜얻는데 실패...!");
                if (rc == -2) MessageBox.Show("재고상태변함...!");
                if (rc == -3) MessageBox.Show("재고상태변함 혹은 이력기록실패...!");
                if (rc == -4) MessageBox.Show("재고상태변함(old code)..!");
                if (rc == -99) MessageBox.Show("중복...!");
                MessageBox.Show(lp.ToString() + " 개의 record가 변경됨!!");
              
                     
            }
        }
        private void ChangeQty()
        {
            decimal stok = 0;
            using (FrmChangeQty_p p = new FrmChangeQty_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                stok = p.numericUpDown1.Value;
            }

            string pltno = "";
            string lstk = "";
            string prod = "";

            string lot = "";
            string bestq = "";
            string loc = "";

            decimal rqty = 0, pstok = 0;
            string stat = "";
            string io = "";
            int rc = 0;
            int lp = 0;
            int saverow = dv.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv.CurrentCell.RowIndex;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    loc = r.Cells["plti_loc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    pstok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    io = r.Cells["lstk_io"].Value.ToString();
                    stat = r.Cells["lstk_stat"].Value.ToString();
                    if (pstok == stok) continue;

                    if (stat != "10") continue;
                    if (io != "0") continue;
                   
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_changeqty(prod, loc, lot, bestq, pltno, lstk, stok);
                            if (rc != 1)
                            {
                                db.Transaction.Rollback();
                                break;
                            }
                            db.Transaction.Commit();
                            lp++;

                            r.Cells["plti_stok"].Value = stok;
                        }
                        catch (Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                            break;
                        }
                    }
                }
                db.Connection.Close();
                if (rc == -1) MessageBox.Show("날짜얻는데 실패...!" + rc.ToString());
                if (rc == -2) MessageBox.Show("재고상태변함...!" + rc.ToString());
                if (rc == -3) MessageBox.Show("재고상태변함 혹은 이력기록실패...!" + rc.ToString());
                if (rc == -4) MessageBox.Show("재고상태변함..!"+ rc.ToString());
                if (rc == -99) MessageBox.Show("중복...!" + rc.ToString());
                MessageBox.Show(lp.ToString() + " 개의 record가 변경됨!!");
               
            }
        }

  
        private void chkyardlstk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbProd_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbProd.Text = dv.SelectedRows[0].Cells["plti_prod"].Value.ToString();
        }

        private void tbLot_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbLot.Text = dv.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            
        }

        private void tbPlt_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbPlt.Text = dv.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
        }

      
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Clipboard.SetDataObject(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv.SelectedRows[0].Cells["plti_pdesc"].Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
            dateTimePicker2.Enabled = checkBox1.Checked;
        }

        private void btnpksz_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("제품들을  제품코드 정보의 내용량으로 일치 시키겠읍니까?", "확인",
            // MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
      
            if (dv.SelectedRows.Count <= 0) return;
            decimal plti_pksz = 0m;

            using (FrmPKSZ_p p = new FrmPKSZ_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                plti_pksz = p.numericUpDown1.Value;
            }
            int rc = 0;
            int lp = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    string plti_pltno = r.Cells["plti_pltno"].Value.ToString();
                    string plti_lstk = r.Cells["lstk_no"].Value.ToString();
                    string plti_prod = r.Cells["plti_prod"].Value.ToString();
                    string plti_loc = r.Cells["plti_loc"].Value.ToString();
                    string plti_lot = r.Cells["plti_lot"].Value.ToString();
                    string plti_bestq = r.Cells["plti_bestq"].Value.ToString();

                    rc = db.ExecuteCommand(@"update miplti set plti_pksz = {0} 
                                        where plti_pltno = {1} and plti_lstk = {2} and plti_prod = {3} and plti_loc = {4} and plti_lot = {5} and plti_bestq = {6}",
                                        plti_pksz, plti_pltno, plti_lstk, plti_prod, plti_loc, plti_lot, plti_bestq);

                    if(rc > 0)
                    {
                        r.Cells["plti_pksz"].Value = plti_pksz;
                    }
                    lp++;
                }

            }
          
        }

        private void btnzero_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;

            long lp = 0;
            string plti_pltno = dv.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            string plti_lstk = dv.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            string plti_prod = dv.SelectedRows[0].Cells["plti_prod"].Value.ToString();
            string plti_loc = dv.SelectedRows[0].Cells["plti_loc"].Value.ToString();
            string plti_lot = dv.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            string plti_bestq = dv.SelectedRows[0].Cells["plti_bestq"].Value.ToString();
            decimal rqty = Convert.ToDecimal(dv.SelectedRows[0].Cells["plti_rqty"].Value.ToString());
            if (rqty == 0m) return;

            if (MessageBox.Show("선택된 - 예약량을 를 0으로 만드시겠읍니까?", "확인",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            int rc = 0;

         
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                rc = d.ExecuteCommand(@"update miplti set plti_rqty = 0 
                                        where plti_pltno = {0}
                                          and plti_lstk = {1}
                                          and plti_prod = {2}
                                          and plti_loc = {3}
                                          and plti_lot = {4}
                                          and plti_bestq = {5} ", plti_pltno, plti_lstk, plti_prod, plti_loc, plti_lot, plti_bestq);
            }
            if (rc > 0)
            {
                dv.SelectedRows[0].Cells["plti_rqty"].Value = 0m;       
            }
            if (rc <= 0) MessageBox.Show("실패!!!");

        }

        private void tbProd_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;

            long lp = 0;
            string plti_pltno = dv.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            string plti_lstk = dv.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            string plti_prod = dv.SelectedRows[0].Cells["plti_prod"].Value.ToString();
            string plti_loc = dv.SelectedRows[0].Cells["plti_loc"].Value.ToString();
            string plti_lot = dv.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            string plti_bestq = dv.SelectedRows[0].Cells["plti_bestq"].Value.ToString();
            decimal rqty = Convert.ToDecimal(dv.SelectedRows[0].Cells["plti_rqty"].Value.ToString());
            decimal stok = Convert.ToDecimal(dv.SelectedRows[0].Cells["plti_stok"].Value.ToString());
            if (stok != 0m || rqty !=  0m) return;

            if (MessageBox.Show("재고 0 삭제하시겠읍니까?", "확인",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            int rc = 0;

          
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                rc = d.ExecuteCommand(@"delete from miplti
                                        where plti_pltno = {0}
                                          and plti_lstk = {1}
                                          and plti_prod = {2}
                                          and plti_loc = {3}
                                          and plti_lot = {4}
                                          and plti_bestq = {5} 
                                          and plti_stok = 0 
                                          and plti_rqty = 0 ", plti_pltno, plti_lstk, plti_prod, plti_loc, plti_lot, plti_bestq);
            }
            if (rc > 0)
            {                
                dv.Rows.RemoveAt(dv.CurrentRow.Index);
            }
            if (rc <= 0) MessageBox.Show("실패!!!");
        }
    }
    public class ItemLstk
    {
        public string lstk_no { get; set; }
        public string lstk_use { get; set; }
        public string lstk_io { get; set; }
        public string lstk_stat { get; set; }
        public string plti_pltno { get; set; }
        public string plti_prod { get; set; }
        public string plti_oprod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal plti_pksz { get; set; }
        public string plti_remark { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_flag { get; set; }
    }
 }
