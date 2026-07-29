using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Transactions;
using System.IO;
using System.Diagnostics;

namespace KK5
{
    public partial class FrmPltzYF : Form
    {
        public string igb = "F";

        DataGridView dv1, dv2;
        DBDataContext db;

        #region SQL statement--------------
         string orgsql = @"select         
         miplti.plti_pltno,   
         miplti.plti_lstk,   
         miplti.plti_prod,   
         miplti.plti_pdesc,   
         miplti.plti_loc,   
         miplti.plti_lot,   
         miplti.plti_bestq,   
         miplti.plti_pksz,   
         miplti.plti_remark,   
         miplti.plti_icust,   
         miplti.plti_stok,   
         (miplti.plti_stok * miplti.plti_pksz) as plti_ltqty,   
         miplti.plti_rqty,         
		 IIF(miplti.plti_stok >= isnull(mimast.mast_canqty, 0), isnull(mimast.mast_canqty, 0), miplti.plti_stok) as  plti_sqty,
         miplti.plti_cycl_date,   
         miplti.plti_idate,   
         miplti.plti_itime,   
         miplti.plti_flag,
         miplti.plti_label,
         miplti.plti_oprod,   
         miplti.plti_icust 
        FROM miplti left outer join mimast on miplti.plti_prod = mimast.mast_cd where plti_pltno is not null  ";
        #endregion

        public FrmPltzYF()
        {
            InitializeComponent();
            dv1 = dataGridView1;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.AutoGenerateColumns = false;
            //dv1.ReadOnly = true;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.AutoGenerateColumns = false;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = true;
            dv2.ReadOnly = true;
            dv2.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            db = new DBDataContext(Config.DBCon);
        }

        private void FrmPltzF_Load(object sender, EventArgs e)
        {
            //if (igb == "Y")
            //{
            //    btncirclemove.Enabled = false;
            //    if (Config.UserLevel != "1")
            //    {
            //        btnpltall.Enabled = false;
            //        btnMakepltone.Enabled = false;
            //        btnmove.Enabled = false;
            //        btnmovefy.Enabled = false;

            //        btnlabel.Enabled = false;
            //        btndeplt.Enabled = false;
            //        btnin.Enabled = false;                  
            //    }
            //}
            //else // if (igb == "F")
            //{
            //    if (Config.UserLevel != "1" && Config.UserLevel != "3")
            //    {
            //        btnpltall.Enabled = false;
            //        btnMakepltone.Enabled = false;
            //        btnmove.Enabled = false;
            //        btnmovefy.Enabled = false;

            //        btnlabel.Enabled = false;
            //        btndeplt.Enabled = false;
            //        btnin.Enabled = false;
            //    }
            //}
            retrieve();
        }
        private void btnqry_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (chk1.Checked)
            {
                tbprod2.Text = tbprod.Text;
                txtpdesc2.Text = txtpdesc.Text;
                tblot2.Text = tblot.Text;
                comboBox2.Text = comboBox1.Text;

                retrieve();
                retrieve2();
            }
            else { retrieve(); }
            
        }

        private int f_label_print(DBDataContext ctx)
        {
           
            string sql = @"select top 1 prn_pltno, prn_pdesc, prn_lot, prn_qty, prn_pksz, prn_mixcnt
                            from tbbprn   where  prn_no = '2' order by prn_pltno ";
          
            string pltno = "", pdesc = "", lot = "";
            decimal pksz = 0;
            int qty = 0, mixcnt = 0;
            int lp = 0;

            string ls_sdata = "";
            string ls_label = "";

            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                var q = d.ExecuteQuery("select stat_lr from tbstat where stat_key = '1'").Single();
                string lr = q.stat_lr;

                Thread.Sleep(1000);

                var p = d.ExecuteQuery(sql).SingleOrDefault();
                if (p == null) return 0;

                pltno = p.prn_pltno;
                pdesc = p.prn_pdesc;
                lot = p.prn_lot;
                qty = p.prn_qty;
                mixcnt = p.prn_mixcnt;
                pksz = p.prn_pksz;

                string str = p.prn_pdesc;
                //string[] strs = str.Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                //if (strs.Length != 0)
                //    str = strs[strs.Length - 1].Trim();              

                if (mixcnt == 1)
                {
                    if (lr.Trim() == "L") ls_sdata = utils.f_mk_label1(pltno, str, lot, qty, pksz);
                    else ls_sdata = utils.f_mk_label1_r(pltno, pdesc, lot, qty, pksz);
                }
                else
                {
                    if (lr.Trim() == "L") ls_sdata = utils.f_mk_label2(pltno, qty, mixcnt);
                    else ls_sdata = utils.f_mk_label2_r(pltno, qty, mixcnt);
                }
                ls_label = ls_label + ls_sdata + (char)14 + (char)10;
                                                
                try
                {
                    d.ExecuteCommand("delete from tbbprn where prn_no = '2' and prn_pltno ='" + pltno + "'");
                    d.ExecuteCommand("update miplti set plti_label = '1' where plti_pltno = '" + pltno + "'");
                       
                }
                catch (Exception E) {  MessageBox.Show(E.Message); }
                
                File.WriteAllText("temp.dat", ls_label);
                string args = @"/c copy " + Application.StartupPath + @"\temp.dat LPT1";
                Process.Start("cmd.exe", args);

            }

            return lp;
        }
        private void btnyloc_Click(object sender, EventArgs e)
        {
            //-- 야적위치로 재고이동
            if (dv1.SelectedRows.Count == 0) return;

            FrmPltiMoveToYloc_p p = new FrmPltiMoveToYloc_p();
            p.ShowDialog();
            if (p.DialogResult == DialogResult.Cancel)
            {
                p.Dispose(); return;
            }
            string yloc = "Y" + p.maskedTextBox1.Text;
            p.Dispose();
            dv1.EndEdit();

            string pltno = dv1.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["plti_lstk"].Value.ToString();
            string prod = dv1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
            string loc = dv1.SelectedRows[0].Cells["plti_loc"].Value.ToString();
            string lot = dv1.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["plti_bestq"].Value.ToString();
            decimal stok = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_stok"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_rqty"].Value.ToString());
            decimal sqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_sqty"].Value.ToString());

            if (sqty <= 0)
            {
                MessageBox.Show("선택수량이 잘못되었읍니다");
                return;
            }

            if (sqty > stok)
            {
                MessageBox.Show("선택량이 재고보다 더큽니다");
                return;
            }

            if (rqty > 0)
            {
                MessageBox.Show("예약이되어 있어 이동불가합니다");
                return;
            }
            int rc = 0;

            //using (TransactionScope sc = new TransactionScope())
            //{
            //    rc = db.p_pltimove_yloc(lstk, yloc, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
            //    if (rc == 1)
            //    {                   
            //        sc.Complete();
            //    }
            //}
            db.Connection.open();                
            using (db.Transaction = db.Connection.BeginTransaction())
            {
                try
                {
                    rc = db.p_pltimove_yloc(lstk, yloc, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
                    if (rc == 1) db.Transaction.Commit();
                    else db.Transaction.Rollback();
                }
                catch(Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
            }
            db.Connection.Close();       

            if (rc != 1)
            {
                if (rc == -3) MessageBox.Show("야적위치가 존재하지 않읍니다." + rc.ToString());
                else MessageBox.Show("실패했읍니다." + rc.ToString());
            }
            //if (rc == 1) MessageBox.Show("성공했읍니다.");

            retrieve();

        }

        private void btnmovefy_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (MessageBox.Show("재고이동하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            dv1.EndEdit();

         
            string pltno = dv1.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["plti_lstk"].Value.ToString();
            string prod = dv1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
            string loc = dv1.SelectedRows[0].Cells["plti_loc"].Value.ToString();
            string lot = dv1.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["plti_bestq"].Value.ToString();
            decimal stok = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_stok"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_rqty"].Value.ToString());
            decimal sqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_sqty"].Value.ToString());

            if (sqty <= 0)
            {
                MessageBox.Show("선택수량이 잘못되었읍니다");
                return;
            }

            if (sqty > stok)
            {
                MessageBox.Show("선택량이 재고보다 더큽니다");
                return;
            }

            if (rqty > 0)
            {
                MessageBox.Show("예약이되어 있어 이동불가합니다");
                return;
            }
            int rc = 0;
          
            //using (TransactionScope sc = new TransactionScope())
            //{
            //    rc = db.p_pltimove_fy(lstk, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
            //    if (rc == 1)
            //    {
            //        sc.Complete();
            //    }
            //}
            db.Connection.open();                
            using (db.Transaction = db.Connection.BeginTransaction())
            {
                try
                {
                    rc = db.p_pltimove_fy(lstk, pltno, prod, loc, lot, bestq, stok, rqty, sqty);
                    if (rc == 1)
                    {
                        db.Transaction.Commit();
                        dv1.Rows.Remove(dv1.SelectedRows[0]);
                    }
                    else db.Transaction.Rollback();
                }
                catch(Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
            }
            db.Connection.Close();

            if (rc != 1) MessageBox.Show("실패했읍니다." + rc.ToString());
            //if (rc == 1) MessageBox.Show("성공했읍니다.");
           
            retrieve();           
        }

        private void btnMakepltone_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (MessageBox.Show("파렛트화하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            dv1.EndEdit();

            int saverow = dv1.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv1.CurrentCell.RowIndex;

            string pltno = dv1.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
            string lstk = dv1.SelectedRows[0].Cells["plti_lstk"].Value.ToString();
            string prod = dv1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
            string loc = dv1.SelectedRows[0].Cells["plti_loc"].Value.ToString();
            string lot = dv1.SelectedRows[0].Cells["plti_lot"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["plti_bestq"].Value.ToString();
            decimal stok = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_stok"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_rqty"].Value.ToString());
            decimal sqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["plti_sqty"].Value.ToString());

            if (stok < sqty || sqty <= 0)
            {
                MessageBox.Show("선택수량을 바르게 입력하세요!");
                return;
            }
            if (rqty > 0)
            {
                MessageBox.Show("예약이 되어 있어 불가함");
                return;
            }

            int canqty = 1;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                canqty = d.ExecuteQuery<int>("Select mast_canqty from mimast where mast_cd = '" + prod + "'").SingleOrDefault();
            }

         
            if (sqty > canqty)
            {
                //MessageBox.Show("최대 적재수량은 " + canqty.ToString() + " 입니다");
                //return;
                if (MessageBox.Show("최대 적재수량은 " + canqty.ToString() + " 입니다" + Environment.NewLine + "허용하시겠읍니까?",
                    "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            }
            string lsm = "";
            int labelyn = 0;


            using (FrmMakePltz_p p = new FrmMakePltz_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.radioButton1.Checked) lsm = "1";
                if (p.radioButton2.Checked) lsm = "2";
                if (p.checkBox1.Checked) labelyn = 1; else labelyn = 0;
            }

            string prnno = "1";
            if (Config.UserLevel == "3") prnno = "2";

            int rc = 0;
            if (lsm == "1")  //  -- 신규
            {   
                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_pltznew_n(pltno, lstk, prod, loc, lot, bestq, stok, sqty, labelyn, prnno);
                        if (rc == 1)
                        {
                            db.Transaction.Commit();

                            dv1.SelectedRows[0].Cells["plti_stok"].Value = stok - sqty;
                            if (stok - sqty > canqty) dv1.SelectedRows[0].Cells["plti_sqty"].Value = (decimal)canqty;
                            else dv1.SelectedRows[0].Cells["plti_sqty"].Value = stok;
                        }
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
                db.Connection.Close();                

                if (rc == -1) MessageBox.Show("제품코드가 정의되어 있지 않읍니다." + rc.ToString());
                if (rc == -2 || rc == -3 || rc == -4) MessageBox.Show("재고 상태변함...!" + rc.ToString());
                if (rc == -99) MessageBox.Show("재고 상태변함...!" + rc.ToString());
                if (rc == -999) MessageBox.Show("라벨중복발행 입니다...!" + rc.ToString());
                if (rc == 1)
                {
                    if(labelyn == 1)
                    {
                        if (prnno == "2") f_label_print(db);
                    }                 
                }
                btnqry_Click(this, new EventArgs());
              
            }
            else             //  -- 기존 꽈대기
            {
                if (dv2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("아래의 기존 파렛트를 선택하세요!");
                    return;
                }

                string pltno2 = dv2.SelectedRows[0].Cells["dv2pltno"].Value.ToString();
                string lstk2 = dv2.SelectedRows[0].Cells["dv2lstk"].Value.ToString();
                string prod2 = dv2.SelectedRows[0].Cells["dv2prod"].Value.ToString();
                string loc2 = dv2.SelectedRows[0].Cells["dv2loc"].Value.ToString();
                string lot2 = dv2.SelectedRows[0].Cells["dv2lot"].Value.ToString();
                string bestq2 = dv2.SelectedRows[0].Cells["dv2bestq"].Value.ToString();
                decimal stok2 = Convert.ToDecimal(dv2.SelectedRows[0].Cells["dv2stok"].Value.ToString());
                decimal rqty2 = Convert.ToDecimal(dv2.SelectedRows[0].Cells["dv2rqty"].Value.ToString());

                tbpltno2.Text = pltno2;
                chk1.Checked = false;

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_pltzadd_n(pltno, pltno2, lstk, prod, loc, lot, bestq, stok, sqty, labelyn, prnno);
                        if (rc == 1)
                        {
                            db.Transaction.Commit();

                            dv1.SelectedRows[0].Cells["plti_stok"].Value = stok - sqty;                           
                            if (stok - sqty > canqty) dv1.SelectedRows[0].Cells["plti_sqty"].Value = canqty;
                            else dv1.SelectedRows[0].Cells["plti_sqty"].Value = stok;
                        }
                        else db.Transaction.Rollback();
                    }
                    catch (Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
                db.Connection.Close();

                if (rc == -1) MessageBox.Show("제품코드가 정의되어 있지 않읍니다." + rc.ToString());
                if (rc == -2 || rc == -3 || rc == -4) MessageBox.Show("재고 상태변함...!" + rc.ToString());
                if (rc == -99) MessageBox.Show("재고 상태변함...!" + rc.ToString());
                if (rc == -999) MessageBox.Show("라벨중복발행 입니다...!" + rc.ToString());
                if (rc == 1)
                {
                    if (labelyn == 1)
                    {
                        if (prnno == "2") f_label_print(db);
                    }                 
                }               
                btnqry_Click(this, new EventArgs());             
            }

        }

        private void btnqury2_Click(object sender, EventArgs e)
        {
            retrieve2();
        }


        private void retrieve()
        {
            string modstr = orgsql;

            if (igb == "F") modstr = modstr + " and plti_pltno = '00000000' and plti_lstk = 'F000000' ";
            if (igb == "Y") modstr = modstr + " and plti_pltno = '00000000' and plti_lstk = 'Y000000' ";

            string ls_m1 = tbprod.Text.Trim();
            if (ls_m1 != "") modstr = modstr + " and plti_prod like '" + ls_m1 + "%'";

            string loc = comboBox1.SelectedItem.ToString();
            if (loc != "ALL")
            {
                loc = loc.Substring(0, 4);
                modstr = modstr + " and plti_loc = '" + loc + "'";
            }

            string lot = tblot.Text.Trim();
            if (lot != "") modstr = modstr + " and plti_lot like '" + lot + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<miplti_YF>(d.ExecuteQuery<miplti_YF>(modstr).ToList());

                //var q = d.ExecuteQuery<miplti_YF>(modstr).ToList();
                //SortableBindingList<miplti_YF> b = q;
                //dv1.DataSource = q;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void deplt_partial()
        {
            decimal stock = Convert.ToDecimal(dv2.SelectedRows[0].Cells["dv2stok"].Value.ToString());
            int rc = 0;

            decimal sqty = 0;
            string chk = "0";

            using (FrmDePLT_p p = new FrmDePLT_p(stock))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.checkBox1.Checked) chk = "1";
                sqty = p.numericUpDown1.Value;

                if (sqty == 0)
                {
                    MessageBox.Show("수량이 0입니다");
                    return;
                }
                if (stock < sqty)
                {
                    MessageBox.Show("선택수량이 너무 큽니다");
                    return;
                }
            }

            if (chk == "1") deplt_all();
            else
            {             
                DataGridViewRow r = dv2.SelectedRows[0];
                
                string pltno = r.Cells["dv2pltno"].Value.ToString();
                string lstk = r.Cells["dv2lstk"].Value.ToString();
                string prod = r.Cells["dv2prod"].Value.ToString();
                string loc = r.Cells["dv2loc"].Value.ToString();
                string lot = r.Cells["dv2lot"].Value.ToString();
                string bestq = r.Cells["dv2bestq"].Value.ToString();
                decimal  stok = Convert.ToDecimal(r.Cells["dv2stok"].Value.ToString());
                decimal rqty = Convert.ToDecimal(r.Cells["dv2rqty"].Value.ToString());
                if (rqty > 0)
                {
                    MessageBox.Show(pltno + " 파렛번호 예약이 되어 있어 해체 불가합니다.");
                    return;
                }
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_deplt_n(pltno, lstk, prod, loc, lot, bestq, stok, sqty);
                        if (rc == 1) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch (Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
                db.Connection.Close();
                if (rc != 1) MessageBox.Show("실패!" + rc.ToString());

                retrieve();
                retrieve2();
            }

        }
        private void deplt_all()
        {
            if (MessageBox.Show("선택된제품들을 해체하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string pltno, lstk, loc, lot, prod, bestq;
            decimal stok, rqty;
            int rc = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                pltno = r.Cells["dv2pltno"].Value.ToString();
                lstk = r.Cells["dv2lstk"].Value.ToString();
                prod = r.Cells["dv2prod"].Value.ToString();
                loc = r.Cells["dv2loc"].Value.ToString();
                lot = r.Cells["dv2lot"].Value.ToString();
                bestq = r.Cells["dv2bestq"].Value.ToString();
                stok = Convert.ToDecimal(r.Cells["dv2stok"].Value.ToString());
                rqty = Convert.ToDecimal(r.Cells["dv2rqty"].Value.ToString());
                if (rqty > 0)
                {
                    MessageBox.Show(pltno + " 파렛번호 예약이 되어 있어 해체 불가합니다.");
                    break;
                }
               
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_deplt(pltno, lstk, prod, loc, lot, bestq, stok);
                        if (rc == 1) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch (Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
                db.Connection.Close();
            }
            if (rc != 1) MessageBox.Show("실패!" + rc.ToString());
         
            retrieve();
            retrieve2();

        }
        private void btndeplt_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;

            if (dv2.SelectedRows.Count == 1)
            {
                deplt_partial();
                return;
            }
            deplt_all();

        }

        private void btnmove_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count == 0) return;
            string dloca = "";
            string loca = "Y000000";

            using (FrmPltiMove_p p = new FrmPltiMove_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                if (p.radioButton1.Checked) dloca = "Y" + p.maskedTextBox1.Text;
                if (p.radioButton2.Checked) dloca = "A" + p.maskedTextBox1.Text;
            }

            if (igb == "F") loca = "F000000";
            if (igb == "Y") loca = "Y000000";
            if (loca == dloca)
            {
                MessageBox.Show("같은위치 이동불가!");
                return;
            }

            int rc = db.ExecuteQuery<int>("select count(*) from milstk where lstk_no = '" + dloca + "'").SingleOrDefault();
            if (rc == 0)
            {
                MessageBox.Show("목적지 위치가 등록되어 있지 않읍니다!");
                return;
            }
            if (dloca.Substring(0, 1) == "A")
            {
                rc = db.ExecuteQuery<int>("select count(*) from miplti where plti_lstk = '" + dloca + "'").SingleOrDefault();
                if (rc != 0)
                {
                    MessageBox.Show("자동창고 빈셀이 아닙니다.(miplti)");
                    return;
                }
                rc = db.ExecuteQuery<int>("select count(*) from milstk where lstk_no = '" + dloca + "' and lstk_io = '0' and lstk_stat = '00' ").SingleOrDefault();
                if (rc == 0)
                {
                    MessageBox.Show("자동창고 빈셀이 아닙니다.(milstk)");
                    return;
                }
            }
            string pltno = "";
            string ppltno = "p";
            foreach (DataGridViewRow r in dv2.SelectedRows)            {

                pltno = r.Cells["dv2pltno"].Value.ToString();

                rc = db.ExecuteQuery<int>("select count(*) from miplti where plti_pltno = '" + pltno + "' and plti_rqty > 0").SingleOrDefault();
                if (rc > 0)
                {
                    MessageBox.Show("파렛트(" + pltno + "출고예약이 되어 있어 이동불가...!");
                    return;
                }
                if (ppltno == pltno) continue;

                ppltno = pltno;

                rc = db.ExecuteCommand("update miplti set plti_lstk = '" + dloca + "' where plti_pltno ='" + pltno + "' and plti_lstk = '" + loca + "'");
                if (rc == 0)
                {
                    MessageBox.Show("파렛트(" + pltno + "이동실패..!");
                    return;
                }
                rc = db.ExecuteCommand("update milstk set lstk_stat = '10', lstk_io = '0' where lstk_no = '" + dloca + "'");
                if (rc == 0)
                {
                    MessageBox.Show("파렛트(" + pltno + "이동실패..!");
                    return;
                }
               
                if (dloca.Substring(0, 1) == "A") break;
            }           
            retrieve2();
        }

        private void btnin_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;
            if (MessageBox.Show("선택된 파렛트를 컨베이어에 올려놓았읍니까?", "입고",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            if (dv2.SelectedRows.Count > 1)
            {
                MessageBox.Show("한 줄만 선택하세요...!");
                return;
            }
            int lp = 0;

            string flstk = "";
            string fstn = "";
            string pltno = "";
            string ppltno = "p";
            string fygubun = "";
            string prod = "";

            if (igb == "F")
            {
                flstk = "F000000";
                fstn = "22";
                fygubun = "1";
            }
            else
            {
                flstk = "Y000000";
                fstn = "21";
                fygubun = "2";
            }
            int rc = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                pltno = r.Cells["dv2pltno"].Value.ToString();
                prod = r.Cells["dv2prod"].Value.ToString();

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.p_inptexec(pltno, flstk, prod, fstn, fygubun);
                    if (rc == 1)
                    {                      
                        sc.Complete();
                        lp++;                      
                    }
                }
                if (rc == -1) MessageBox.Show("상태변함..!");
                if (rc == -2) MessageBox.Show("출고예약 되어 있음..!");
                if (rc == -3) MessageBox.Show("제품코드 등록바람..!");
                if (rc == -4) MessageBox.Show("바코드 입고모드이므로 파렛트 선택입고 불가...");
                if (rc == -5 || rc == -55) MessageBox.Show("빈셀없음...! 혹은 크레인 입고상태 가능여부확인바람");
                if (rc == -6) MessageBox.Show("보관위치 상태변함..!");
                if (rc == -7) MessageBox.Show("시간얻기 실패...!");
                if (rc == -8) MessageBox.Show("재고상태 상태변함2...!");
                if (rc == -9) MessageBox.Show("작업번호 얻기 실패...!");
                if (rc != 1)
                {
                    MessageBox.Show(rc.ToString() + " 기타 에러");
                    return;
                }
            }           
            retrieve2();
        }

        private void btncirclemove_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count == 0) return;
            if (MessageBox.Show("야적으로 이동하기위하여 선택된 파렛트를 컨베이어에 올려놓았읍니까?", "야적이동",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string pltno = dv2.SelectedRows[0].Cells["dv2pltno"].Value.ToString();
            string lstk = dv2.SelectedRows[0].Cells["dv2lstk"].Value.ToString();
            string fstn = "";

            if (igb == "F")
            {
                lstk = "F000000";
                fstn = "22";
            }
            else
            {
                lstk = "Y000000";
                fstn = "21";
            }
                       
            int rc = 0;
            db.Connection.open();                
            using (db.Transaction = db.Connection.BeginTransaction())
            {
                try
                {
                    rc = db.p_movepltno_fy_yf(pltno, lstk, fstn);
                    if (rc == 1)
                    {
                        db.Transaction.Commit();
                    }
                    else db.Transaction.Rollback();
                }
                catch(Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
            }
            db.Connection.Close();

            if (rc == -1) MessageBox.Show("상태변함..!");
            if (rc == -2) MessageBox.Show("출고예약 되어 있음..!");
            if (rc == -3) MessageBox.Show("순환이동중..!");
            if (rc == -4) MessageBox.Show("바코드 입고모드이므로 파렛트 선택입고 불가...");
            if (rc == -5) MessageBox.Show("상태변함 update...");

            retrieve2(); 
        }

        private void btnlabel_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count == 0) return;

            if (MessageBox.Show("라벨발행하시겠읍니까?", "라벨발행",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string pltno = "";
            string prod = "", pdesc = "", lot = "";
            decimal pksz = 0;
            string msg = "";
            int pltcnt = 0;
            decimal stokqty = 0;

            DataGridViewRow r = dv2.SelectedRows[0];

            pltno = r.Cells["dv2pltno"].Value.ToString();
            prod = r.Cells["dv2prod"].Value.ToString();

            pdesc = db.ExecuteQuery<string>("select mast_desc1 from mimast where mast_cd = '" + prod + "'").SingleOrDefault();
            if (pdesc == "")
            {
                MessageBox.Show("제품 코드가 존재하지 않읍니다...!");
                return;
            }
        
            lot = r.Cells["dv2lot"].Value.ToString().Trim();
            pksz = Convert.ToDecimal(r.Cells["dv2pksz"].Value.ToString());

            pltcnt = db.ExecuteQuery<int>(@"select count(*), sum(plti_stok) from miplti where substring(plti_lstk,1,1) in ('Y', 'F') and  plti_pltno = '" + pltno + "'").SingleOrDefault();
            if (pltcnt == 0)
            {
                msg = "파렛번호:[" + pltno + "] 가 없읍니다..!";
                return;
            }
            stokqty = db.ExecuteQuery<decimal>("select sum(plti_stok) from miplti where substring(plti_lstk,1,1) in ('Y', 'F') and plti_pltno = '" + pltno + "'").SingleOrDefault();
          
            string bcrprn = "1";
            //if (igb == "F") bcrprn = "2";

            //string bcrprn = "2";
            if (Config.UserLevel == "3") bcrprn = "2";

            db.Connection.open();                
            using (db.Transaction = db.Connection.BeginTransaction())
            {
                try
                {
                    int rc = db.ExecuteCommand("update miplti set plti_label = '1' where plti_pltno = '" + pltno + "'");

                    string sql = @" INSERT INTO tbbprn 
                                                    ( prn_no, prn_pltno, prn_prod, prn_pdesc, prn_lot, prn_pksz, prn_qty, prn_mixcnt)
                                            values ( {0},    {1}, {2}, {3}, {4}, {5}, {6}, {7} ) ";

                    if (pltcnt == 1)
                        db.ExecuteCommand(sql, bcrprn, pltno, prod, pdesc, lot, pksz, stokqty, pltcnt);
                    else
                        db.ExecuteCommand(sql, bcrprn, pltno, "", "", "", 0.00, stokqty, pltcnt);

                    if (rc == 1) db.Transaction.Commit();
                    else db.Transaction.Rollback();
                }
                catch(Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message + Environment.NewLine + "중복발행"); }
            }
            db.Connection.Close();          
          
            if (bcrprn == "2") f_label_print(db);
        
            MessageBox.Show("라벨발행 명령 성공...!");
        }

        private void btnpltall_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            if (MessageBox.Show("선택된 전체를 파렛트화하시겠읍니까?", "전체파렛트화",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int labelyn = 1;
            using (FrmPltzAll_p p = new FrmPltzAll_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.checkBox1.Checked) labelyn = 1; else labelyn = 0;
            }

            string pltno, lstk, prod, loc, lot, bestq;
            decimal stok, rqty, sqty;
            int canqty = 0;
            int rc = 0, lp = 0;

            string prnno = "1";
            if (Config.UserLevel == "3") prnno = "2";

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.Connection.open();
                foreach (DataGridViewRow r in rr)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    lstk = r.Cells["plti_lstk"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    loc = r.Cells["plti_loc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString();
                    bestq = r.Cells["plti_bestq"].Value.ToString();
                    stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                    rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                    sqty = Convert.ToDecimal(r.Cells["plti_sqty"].Value.ToString());
                    if (rqty > 0) continue;

                    if (stok < sqty || sqty <= 0)
                    {
                        MessageBox.Show("제품코드 =" + prod + " 선택수량을 바르게 입력하세요!");
                        break;
                    }

                    canqty = d.ExecuteQuery<int>(@"select mast_canqty from mimast where mast_cd = '" + prod + "'").SingleOrDefault();
                    if (canqty == 0)
                    {
                        if (MessageBox.Show("제품코드가 없거나 적재수량이 정의되지 안았읍니다..." + Environment.NewLine + " 파렛트화하시겠읍니까?", "확인",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                        
                    }

                    using (d.Transaction = d.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = d.p_pltzerall_n(pltno, lstk, prod, loc, lot, bestq, stok, sqty, labelyn, prnno);
                            if (rc == 1)
                            {
                                d.Transaction.Commit(); lp++;

                                //if (stok == sqty) dv1.Rows.Remove(r);
                                //else
                                //{
                                //    r.Cells["plti_stok"].Value = stok - sqty;
                                //    if (stok - sqty > canqty) r.Cells["plti_sqty"].Value = (decimal)canqty;
                                //    else r.Cells["plti_sqty"].Value = stok - sqty;
                                //}
                            }
                            else
                            {
                                d.Transaction.Rollback(); break;
                            }
                        }
                        catch (Exception E) { d.Transaction.Rollback(); MessageBox.Show(E.Message); }
                    }
                                       
                    if (rc == 1)
                    {
                        if (labelyn == 1)                        {
                          
                            if (prnno == "2")
                            {
                                int g = d.ExecuteQuery<int>("Select count(*) from tbbprn where prn_no = '2'").SingleOrDefault();

                                for (int k= 0; k < g; k++)
                                    f_label_print(d);
                            }
                        }
                    }
                }
                d.Connection.Close();
            }
          
            if (rc == -1) MessageBox.Show("제품코드등록바람..!");
            if (rc == -2) MessageBox.Show("선택수량 없음..!");
            if (rc == -3) MessageBox.Show("선택수량 너무큼.!");
            if (rc == -4) MessageBox.Show("상태변함..");
            if (rc == -5) MessageBox.Show("상태변함2...");
            if (rc == -6) MessageBox.Show("파렛번호 얻기 실패...");
            if (rc == -7) MessageBox.Show("파렛번호 얻기 실패2...");
            if (rc == -999) MessageBox.Show("파렛번호 이미 발행");
            
            btnqry_Click(this, new EventArgs());
          
        }

        private void retrieve2()
        {
            string modstr = orgsql;

            string ls_pltno2 = tbpltno2.Text.Trim();
            string ls_prod2 = tbprod2.Text.Trim();

            if (igb == "F") modstr = modstr + " and plti_pltno <> '00000000' and plti_lstk = 'F000000' ";
            if (igb == "Y") modstr = modstr + " and plti_pltno <> '00000000' and plti_lstk = 'Y000000' ";

            if (ls_pltno2 != "") modstr = modstr + " and plti_pltno like '" + ls_pltno2 + "%'";
            if (ls_prod2 != "") modstr = modstr + " and plti_prod like '" + ls_prod2 + "%'";

            string loc2 = comboBox2.SelectedItem.ToString();
            if (loc2 != "ALL") modstr = modstr + " and plti_loc = '" + loc2.Substring(0,4) + "'";

            string lot2 = tblot2.Text.Trim();
            if (lot2 != "") modstr = modstr + " and plti_lot like '" + lot2 + "%'";

            string pdesc = txtpdesc2.Text.Trim();
            if (pdesc != "") modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";

            modstr = modstr + " and  plti_flag <> 'N' order by plti_pltno ";

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                var q = d.ExecuteQuery<miplti_YF>(modstr).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal stoksum = 0, ltqty = 0;
          

            for (int i = 0; i < dataGridView1.Rows.Count ; i++)
            {
                stoksum = stoksum + Convert.ToDecimal(dataGridView1.Rows[i].Cells["plti_stok"].Value.ToString());
                ltqty = ltqty + Convert.ToDecimal(dataGridView1.Rows[i].Cells["plti_ltqty"].Value.ToString());
            }
            lblqty.Text = string.Format("{0:n0}", stoksum);
            lblltqty.Text = string.Format("{0:n3}", ltqty);           

        }


        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbprod.Text = dv1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
        }

        private void tblot_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tblot.Text = dv1.SelectedRows[0].Cells["plti_lot"].Value.ToString();
        }

        private void tbprod2_DoubleClick(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;
            tbprod2.Text = dv2.SelectedRows[0].Cells["dv2prod"].Value.ToString();
        }

        private void tblot2_DoubleClick(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;
            tblot2.Text = dv2.SelectedRows[0].Cells["dv2lot"].Value.ToString();
        }

        private void tbpltno2_DoubleClick(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;
            tbpltno2.Text = dv2.SelectedRows[0].Cells["dv2pltno"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["plti_pdesc"].Value.ToString();
        }

        private void txtpdesc2_DoubleClick(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;
            txtpdesc2.Text = dv2.SelectedRows[0].Cells["dv2pdesc"].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["plti_sqty"].Value = 
                    dataGridView1.Rows[e.RowIndex].Cells["plti_stok"].Value.ToString();
                }
            }
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dv1);
        }

    }
    public class miplti_YF
    {
        public string plti_pltno { get; set; }
        public string plti_lstk { get; set; }
        public string plti_prod { get; set; }
        public string plti_oprod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal? plti_pksz { get; set; }

        public string plti_remark { get; set; }
        public decimal plti_ltqty { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public decimal plti_sqty { get; set; }

        public string plti_cycl_date { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_flag { get; set; }
        public string plti_icust { get; set; }
        public string plti_label { get; set; }
    }
}
