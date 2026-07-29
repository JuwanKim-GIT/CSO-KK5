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
    public partial class FrmMiwmtx : Form
    {
        #region --- MDI Child ----------------
        private static FrmMiwmtx _instance;
        public static FrmMiwmtx Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMiwmtx();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmWmtox_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
        #region --- sqlstatement -------------
        string sqlm = @"  SELECT docnum,   
                                 credat,   
                                 cretim,   
                                 lgnum,   
                                 tanum,   
                                 bwlvs,   
                                 trart,   
                                 bname,   
                                 tapos,   
                                 matnr,   
                                 plant,   
                                 charg,   
                                 bestq,   
                                 sobkz,   
                                 lsonr,   
                                 meins,   
                                 wdatu,   
                                 wenum,   
                                 vltyp,
                                 pksz,   
                                 vsolm,   
                                 (vsolm * pksz) as ltqty,   
                                 nltyp,   
                                 maktx,   
                                 vfdat,   
                                 lgort,   
                                 io,   
                                 rqty,   
                                 fqty,   
                                 flag,   
                                 hdate,   
                                 htime,
                                 (substring(credat,1,4) + '-' +  substring(credat,5,2) + '-' +  substring(credat,7,2) + ' ' +
                                  substring(cretim,1,2) + ':' +  substring(cretim,3,2) + ':' +  substring(cretim,5,2) + ' ') as credt 
                            FROM miwmto  
                           WHERE docnum is not null    ";

        // tab1
        string sqls1 = @" SELECT lstk_no,   
                                lstk_stat,   
                                plti_pltno,   
                                plti_prod,
                                plti_loc,      
                                plti_lot,      
                                plti_bestq,      
                                plti_pdesc,       
                                plti_pksz,     
                                plti_remark,   
                                plti_stok,     
                                plti_rqty,     
                                plti_cycl_date,
                                plti_idate,
                                plti_itime,
                                plti_oprod
                         FROM milstk, miplti   
                        Where (lstk_no = plti_lstk)
                          and (lstk_stat in ('10', '$R' ))
                          and (plti_stok > 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot = {2})
                          and (plti_bestq = {3})
                          and (plti_flag = '1')  
                        union
                        SELECT plti_lstk as lstk_no,   
                                '10' as lstk_stat,   
                                plti_pltno,   
                                plti_prod,
                                plti_loc,      
                                plti_lot,      
                                plti_bestq,      
                                plti_pdesc,       
                                plti_pksz,     
                                plti_remark,   
                                plti_stok,     
                                plti_rqty,     
                                plti_cycl_date,
                                plti_idate,
                                plti_itime,
                                plti_oprod
                         FROM miplti   
                        Where (plti_stok > 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot =  {2})
                          and (plti_bestq = {3})
                          and (plti_pltno = '00000000')
                          and (plti_lstk like 'Y%' )
                          and (plti_flag = '1')  order by lstk_no";


        string sqls2 = @"select a.wmtxkey, a.docnum, a.tanum, a.tapos,  a.bwlvs, a.IO, a.pltno, a.lstk, a.qty, a.flag, a.idate, a.itime, a.credat, a.cretim, a.remark, a.oprod,
                                b.matnr,   b.maktx,  b.lgort,   b.charg,   b.bestq
                         from tiwmtx a join miwmto b on a.docnum = b.docnum and a.tanum = b.tanum and a.tapos = b.tapos  
                         where a.flag = '$R' and a.IO = '$' ";

        string sqls3 = @"select a.wmtxkey, a.docnum, a.tanum, a.tapos,  a.bwlvs, a.IO, a.pltno, a.lstk, a.qty, a.flag, a.idate, a.itime, a.credat, a.cretim, a.remark, a.oprod,
                                b.matnr,   b.maktx,  b.lgort,   b.charg,   b.bestq 
                         from tiwmtx a join miwmto b on a.docnum = b.docnum and a.tanum = b.tanum and a.tapos = b.tapos  
                         where a.flag = '$Z'  and a.IO = '$' ";

        #endregion
        DataGridView dv1, dv2, dv3, dv4;

        public FrmMiwmtx()
        {
            InitializeComponent();
            FormClosed += FrmWmtox_FormClosed;

            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionChanged += Dv1_SelectionChanged; ;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.CellPainting += Dv1_CellPainting;
            Tab1.SelectedIndexChanged += Dv1_SelectionChanged;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = true;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            dv2.RowPostPaint += Common.RowPostPaint;

            dv3 = dataGridView3;
            dv3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv3.MultiSelect = true;
            dv3.ReadOnly = true;
            dv3.AutoGenerateColumns = false;
            dv3.RowPostPaint += Common.RowPostPaint;

            dv4 = dataGridView4;
            dv4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv4.MultiSelect = true;
            dv4.ReadOnly = true;
            dv4.AutoGenerateColumns = false;
            dv4.RowPostPaint += Common.RowPostPaint;


            dv1.CellFormatting += Dv1_CellFormatting;
            dv2.CellFormatting += Dv2_CellFormatting;
            dv3.CellFormatting += Dv3_CellFormatting;


            comboBox1.SelectedIndex = 0;

            if (Config.UserLevel != "1")
            {
                btndel.Enabled = false;
               
                btn_e.Enabled = false;
                btn_w.Enabled = false;
            }
        }

        private void Dv1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
         
        }

        private void Dv3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + "-" + ls.Substring(5, 2);
            //            e.FormattingApplied = true;
            //        }
            //    }

            //    if (e.ColumnIndex == 7)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            if (ls == "$Z") e.Value = "완료";
            //            if (ls == "$X") e.Value = "실행중";
            //            e.FormattingApplied = true;
            //        }
            //    }

            //    if (e.ColumnIndex == 9)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            e.Value = ls.Substring(0, 4) + "/" + ls.Substring(4, 2) + "/" + ls.Substring(6, 2);
            //            e.FormattingApplied = true;
            //        }
            //    }
            //    if (e.ColumnIndex == 10)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            e.Value = ls.Substring(0, 2) + ":" + ls.Substring(2, 2) + ":" + ls.Substring(4, 2);
            //            e.FormattingApplied = true;
            //        }
            //    }
            //}
            //catch (Exception E)
            //{
            //    //MessageBox.Show(E.Message);
            //}
        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if (Tab1.SelectedIndex == 0) retrieve1();
            if (Tab1.SelectedIndex == 1) retrieve2();
            if (Tab1.SelectedIndex == 2) retrieve3();
        }

        private void FrmWmtox_Load(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {          

            string modstr = sqlm;

            string date1 = dtDatefrom.Text;
            string date2 = dtDateTo.Text;

            date1 = date1.Replace("-", "");
            date2 = date2.Replace("-", "");

            if (!chkdt.Checked)
            {
                if (date1 != "") modstr = modstr + " and credat >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and credat >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and credat <= '" + date2 + "'";
            }

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '" + tbdoc.Text.Trim() + "%'";
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '" + tbprod.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and maktx like '%" + txtpdesc.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '" + tbbatch.Text.Trim() + "%'";

            string bwlvs = comboBox1.SelectedItem.ToString().Substring(0,3);
            if (bwlvs != "ALL") modstr = modstr + " and bwlvs ='" + bwlvs +"'";

            modstr = modstr + " and IO in ( '$' ) order by credt, maktx ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<miwmtox>(db.ExecuteQuery<miwmtox>(modstr).ToList());
                //var q = db.ExecuteQuery<miwmtox>(modstr).ToList();
                //dv1.DataSource = q;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

            if (Tab1.SelectedIndex == 0) retrieve1();
            if (Tab1.SelectedIndex == 1) retrieve2();
            if (Tab1.SelectedIndex == 2) retrieve3();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void btn_w_Click(object sender, EventArgs e)
        {
            #region --- old exec -----
            //if (dv1.SelectedRows.Count == 0) return;
            //if (Tab1.SelectedIndex != 0) return;
            //if (dv2.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("아래의 제품들을 선택하세요...");
            //    return;
            //}

            //if (MessageBox.Show("아래의 선택된 제품들을 실행하시겠읍니까?", "확인",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            //string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            //decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            //int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            //string bwlvs = dv1.SelectedRows[0].Cells["bwlvs"].Value.ToString();
            //decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            //decimal uqty = 0;
            //int rc = 0;
            //int lp = 0;
            //DBDataContext db = new DBDataContext(Config.DBCon);
            //Cursor = Cursors.WaitCursor;
            //try
            //{
            //    foreach (DataGridViewRow r in dv2.SelectedRows)
            //    {
            //        string pltno =r.Cells["plti_pltno"].Value.ToString();
            //        string lstk =r.Cells["lstk_no"].Value.ToString();
            //        string prod = r.Cells["plti_prod"].Value.ToString();
            //        string loc = r.Cells["plti_loc"].Value.ToString();
            //        string lot = r.Cells["plti_lot"].Value.ToString();

            //        string bestq = r.Cells["plti_bestq"].Value.ToString();
            //        decimal stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
            //        if (vsolm > stok)
            //        {
            //            uqty = stok;
            //            vsolm = vsolm - stok;
            //        }
            //        else
            //        {
            //            uqty = vsolm;
            //            vsolm = 0;
            //        }

            //        decimal pksz = Convert.ToDecimal(r.Cells["plti_pksz"].Value.ToString());

            //        string cdate = r.Cells["plti_cycl_date"].Value.ToString();
            //        string idate = r.Cells["plti_idate"].Value.ToString();
            //        string itime = r.Cells["plti_itime"].Value.ToString();
            //        string oprod = r.Cells["plti_oprod"].Value.ToString();
            //        string remark = r.Cells["plti_remark"].Value.ToString();

            //        using (TransactionScope sc = new TransactionScope())
            //        {
            //            rc = db.p_etc_exec_spec(docnum, tanum, tapos, bwlvs, pltno, lstk, prod, loc, lot, bestq, uqty, pksz, idate, itime, oprod, remark);
            //            if (rc == 1)
            //            {
            //                db.SubmitChanges();
            //                sc.Complete();
            //                lp++;                           
            //            }
            //        }
            //        if (rc != 1)
            //        {
            //            MessageBox.Show("상태 변함...!" + rc.ToString());
            //            break;
            //        }
            //        if (vsolm <= 0) break;
            //    }
            //}
            //catch(Exception E) { MessageBox.Show(E.Message); }
            //finally { Cursor = Cursors.Default; }

            //MessageBox.Show(lp.ToString() + " 건이 실행되었읍니다...!");
            //retrieve();
            #endregion
                        
            if (Tab1.SelectedIndex != 1) return;
            if (dv3.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("아래 예약된 제품들을 모두 실행하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                try
                {
                    using (TransactionScope sc = new TransactionScope())
                    {
                        rc = db.p_etc_out_exec();
                        if (rc > 0)
                        {
                            db.SubmitChanges();
                            sc.Complete();
                        }
                    }
                    if (rc > 0) MessageBox.Show(rc.ToString() + " 건이 실행되었읍니다...!");
                    if (rc <= 0) MessageBox.Show("실패했읍니다. 상태변함" + rc.ToString());

                }
                catch (Exception E) { MessageBox.Show(E.Message); }

            }
            retrieve();

        }
        private void btn_c_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (Tab1.SelectedIndex != 1) return;
            if (dv4.SelectedRows.Count <= 0)
            {
                MessageBox.Show("아래의 제품들을 선택하세요...");
                return;
            }

            if (MessageBox.Show("아래의 선택된 제품들을 출고확정 취소하시겠읍니까?", "확정 취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            string bwlvs = dv1.SelectedRows[0].Cells["bwlvs"].Value.ToString();
            decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());

            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string maktx = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["bestq"].Value.ToString();

            int rc = 0;
            int lp = 0;
            DBDataContext db = new DBDataContext(Config.DBCon);
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (DataGridViewRow r in dv4.SelectedRows)
                {
                    decimal wmtxkey = Convert.ToDecimal(r.Cells["wmtxkey_f"].Value.ToString());
                    string pltno = r.Cells["pltno_f"].Value.ToString();
                    string lstk = r.Cells["lstk_f"].Value.ToString();

                    decimal stok = Convert.ToDecimal(r.Cells["qty_f"].Value.ToString());
                    decimal pksz = Convert.ToDecimal(r.Cells["pksz_f"].Value.ToString());
                
                    string idate = r.Cells["idate_f"].Value.ToString();
                    string itime = r.Cells["itime_f"].Value.ToString();

                    string oprod = r.Cells["oprod_f"].Value.ToString();
                    string remark = r.Cells["remark_f"].Value.ToString();

                    using (TransactionScope sc = new TransactionScope())
                    {
                        rc = db.p_etc_cnfm_cancel(docnum, tanum, tapos, wmtxkey, pltno, lstk, matnr, maktx, lgort, charg, bestq, stok, pksz, idate, itime, oprod, remark); 
                        if (rc == 1)
                        {
                            db.SubmitChanges();
                            sc.Complete();
                            lp++;
                        }
                    }
                    if (rc != 1)
                    {
                        MessageBox.Show("상태 변함...!");
                        break;
                    }
                }
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }

            MessageBox.Show(lp.ToString() + " 건이 출고확정 취소...!");
            retrieve();

        }
     

        private void btn_e_Click(object sender, EventArgs e)
        {
            #region old case
            //if (Tab1.SelectedIndex != 2) return;
            //if (dv4.SelectedRows.Count <= 0) return;
            //if (dv4.SelectedRows.Count <= 0) return;

            //if (MessageBox.Show("위의 선택된 오더를 출고확정하시겠읍니까?", "출고확정",
            //              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            //int rc = 0;
            //int lp = 0;
            //int lc = 0;
            //DBDataContext db = new DBDataContext(Config.DBCon);
            //Cursor = Cursors.WaitCursor;
            //try
            //{
            //    foreach (DataGridViewRow r in dv1.SelectedRows)
            //    {
            //        string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            //        decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            //        int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            //        decimal fqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["fqty"].Value.ToString());
            //        decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            //        if (vsolm > fqty)
            //        {

            //            lc = db.ExecuteCommand("Select count(*) from tiwmtx where docnum = {0} and tanum = {1} and tapos = {2} and flag <> '$Z' ", docnum, tanum, tapos);
            //            if (lc > 0)
            //            {
            //                MessageBox.Show("아직 출고중인 제품이 있읍니다...!");
            //                break;
            //            }
            //        }

            //        using (TransactionScope sc = new TransactionScope())
            //        {
            //            rc = db.p_etc_cnfm(docnum, tanum, tapos);
            //            if (rc == 1)
            //            {
            //                db.SubmitChanges();
            //                sc.Complete();
            //                lp++;
            //            }
            //        }
            //        if (rc != 1)
            //        {
            //            Cursor = Cursors.Default;
            //            MessageBox.Show("상태 변함...!");
            //            break;
            //        }
            //    }
            //}
            //catch (Exception E) { MessageBox.Show(E.Message); }
            //finally { Cursor = Cursors.Default; }

            //MessageBox.Show(lp.ToString() + " 건이 확정되었읍니다...!");
            //retrieve();
            #endregion

            if (Tab1.SelectedIndex != 2) return;
            if (dv4.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("출고 완료된 제품들을 모두 확정 하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0;
            int lp = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv4.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    foreach (DataGridViewRow r in rr)
                    {
                        using (TransactionScope sc = new TransactionScope())
                        {
                            decimal wmtxkey = Convert.ToDecimal(r.Cells["wmtxkey_f"].Value.ToString());
                            string docnum = r.Cells["docnum_f"].Value.ToString();
                            decimal tanum = Convert.ToDecimal(r.Cells["tanum_f"].Value.ToString());
                            int tapos = Convert.ToInt32(r.Cells["tapos_f"].Value.ToString());

                            db.ExecuteCommand("delete from tiwmtx where wmtxkey = {0} and docnum = {1} and tanum = {2} and tapos = {3} ", wmtxkey, docnum, tanum, tapos);
                            db.ExecuteCommand("delete from miwmto where docnum = {0} and tanum = {1} and tapos = {2} and fqty >= vsolm", docnum, tanum, tapos);

                            lp++;
                            sc.Complete();
                        }
                    }
                    Cursor = Cursors.Default;
                    if (lp > 0) MessageBox.Show(lp.ToString() + " 건이 확정되었읍니다...!");
                    if (lp == 0) MessageBox.Show("실패했읍니다. 상태변함");
                }
                catch (Exception E) { MessageBox.Show(E.Message); }
                finally { Cursor = Cursors.Default; }
            }
            retrieve();

        }
        
        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }

   
        private void btndel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["rqty"].Value.ToString());
            if (rqty > 0) return;

            //if (vsolm == 0 || lgort == "" || charg == "")
            //{
                if (MessageBox.Show("삭제하시겠읍니까?", "확인",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                int rc = 0;
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    rc = db.ExecuteCommand(@"delete from miwmto where docnum = {0} and tanum = {1} and tapos = {2}", docnum, tanum, tapos);
                }
                if (rc > 0) MessageBox.Show("삭제 OK!");
                else MessageBox.Show("삭제 실패!");

                retrieve();
            //}
        }

        private void tbdoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbdoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbprod.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void btnRsrv_Click(object sender, EventArgs e)
        {
            if (Tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;
            
            string sel = "";
            using (FrmEtcRsrvOpt_p p = new FrmEtcRsrvOpt_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.radioButton1.Checked) sel = "1";
                if (p.radioButton2.Checked) sel = "2";
            }

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                int c = d.ExecuteQuery<int>("Select count(*) from tiordx where flag = '$R' ").SingleOrDefault();
                if (c > 0)
                {
                    MessageBox.Show("정상출고에 출고예약이 남아 있읍니다." + Environment.NewLine + Environment.NewLine +
                                    "실행시키던지 취소하고 기타출고 예약바랍니다.!" + Environment.NewLine +
                                    "정상출고예약과 기타출고예약은 동시에 안됩니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Stop);


                    return;
                }
            }
            if (sel == "1") ue_rsrv_upper_line2();
            if (sel == "2") ue_rsrv_spec();

        }
        private void ue_rsrv_upper_line2()
        {
            if (Tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("상위 선택된 행들을 예약 하시겠읍니까?", "예약확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }
            
            //check 상위라인

            Cursor = Cursors.WaitCursor;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                foreach (DataGridViewRow r in rr)
                {

                    string matnr = r.Cells["matnr"].Value.ToString();
                    string lgort = r.Cells["lgort"].Value.ToString();
                    string charg = r.Cells["charg"].Value.ToString();
                    string bestq = r.Cells["bestq"].Value.ToString();

                    string docnum = r.Cells["docnum"].Value.ToString();
                    decimal tanum = Convert.ToDecimal(r.Cells["tanum"].Value.ToString());
                    int tapos = Convert.ToInt32(r.Cells["tapos"].Value.ToString());

                    decimal vsolm = Convert.ToDecimal(r.Cells["vsolm"].Value.ToString());
                    decimal rqty = Convert.ToDecimal(r.Cells["rqty"].Value.ToString());
                    decimal oq = vsolm - rqty;
                    int rc = 0;
                    int lp = 0;
                    decimal? oqty = 0;
                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.p_etc_rsrv_uline2(docnum, tanum, tapos, matnr, lgort, charg, bestq, ref oqty);
                            if (rc > 0)
                            {
                                if (oqty == null || oqty == 0) db.Transaction.Rollback();
                                else
                                {                                   
                                    db.Transaction.Commit(); lp++;
                                    r.Cells["rqty"].Value = rqty + oqty;                                    
                                }
                            }
                            else
                            {
                                db.Transaction.Rollback(); 
                            }
                        }
                        catch (Exception E) { Cursor = Cursors.Default; db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                    }                
                }
                db.Connection.Close();
            }
            Cursor = Cursors.Default;

            dv1_DataBindingComplete();
            retrieve();
           
        }
        private void ue_rsrv_spec()
        {
            int saverow = 0;
            int rowIndex = 0;

            if (dv2.SelectedRows.Count == 0) return;
            if (Tab1.SelectedIndex != 0) return;

            rowIndex = dv1.CurrentCell.RowIndex;
            saverow = dv1.FirstDisplayedScrollingRowIndex;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = "";
            string bwlvs = dv1.SelectedRows[0].Cells["bwlvs"].Value.ToString();

            decimal oq = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            decimal rq = Convert.ToDecimal(dv1.SelectedRows[0].Cells["rqty"].Value.ToString());
            decimal oqty = 0;
            int lp = 0;

            decimal sq = oq - rq;
            if (sq == 0) return;

            string sql = @"select count(*) from miwmto where  docnum = {0} and tanum = {1} and tapos = {2} and vsolm = {3} and rqty = {4} and vsolm - rqty > 0 ";
            DBDataContext db = new DBDataContext(Config.DBCon);

            int rc = db.ExecuteCommand(sql, docnum, tanum, tapos, oq, rq);
            if (rc == 0)
            {
                MessageBox.Show("오더 상태가 변했음! 조회해 보세요!");
                return;
            }
            string dts = "";
            db.p_curgetdatetime14(ref dts);

            string rdat = dts.Substring(0, 8);
            string rtim = dts.Substring(0, 6);
            string lstk = "", pltno = "", remark = "";
            decimal stok = 0, rqty = 0;

            int ret = 0;
            string sql1 = @"update miplti 
                            set plti_stok = plti_stok - {0}, 
		                        plti_rqty = plti_rqty + {0}
		                    where plti_pltno = {1}
		                      and plti_lstk = {2}
 	                          and plti_prod = {3}
		                      and plti_loc = {4}
		                      and plti_lot = {5}
		                      and plti_stok = {6}
		                      and plti_rqty = {7}
		                      and plti_bestq not in ('S', 'Q') 
                              and 1 = (select count(*) from milstk where lstk_no = {2} and lstk_stat in ('10', '$R')) ";


            string sql2 = @"INSERT INTO tiwmtx ( docnum,  tanum,  tapos,  lstk,   pltno,   qty,    flag,  credat,  cretim,   remark,   pksz,  idate,  itime,  oprod,   bwlvs,       io )  
                                        values ( {0},     {1},   {2},     {3},    {4},    {5},    '$R',   {6},     {7},     {8},      {9},   {10},        {11},        {12},  {13}, {14} ) ";

            string sql3 = @"update miwmto set rqty = rqty + {0}	where  docnum = {1}	and tanum = {2} and tapos = {3} ";
            Cursor = Cursors.WaitCursor;
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    db.p_tilock(); // readcommitted

                    string idate, itime, oprod;
                    decimal pksz;

                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv2.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    foreach (DataGridViewRow r in rr)
                    {
                        lstk = r.Cells["lstk_no"].Value.ToString();
                        if (lstk == "F000000") continue;

                        pltno = r.Cells["plti_pltno"].Value.ToString();
                        stok = Convert.ToDecimal(r.Cells["plti_stok"].Value.ToString());
                        rqty = Convert.ToDecimal(r.Cells["plti_rqty"].Value.ToString());
                        pksz = Convert.ToDecimal(r.Cells["plti_pksz"].Value.ToString());
                        idate = r.Cells["plti_idate"].Value.ToString();
                        itime = r.Cells["plti_itime"].Value.ToString();
                        oprod = r.Cells["plti_oprod"].Value.ToString();
                        remark = r.Cells["plti_remark"].Value.ToString();

                        if (sq > stok)
                        {
                            rc = db.ExecuteCommand(sql1, stok, pltno, lstk, matnr, lgort, charg, stok, rqty);
                            if (rc == 0) { ret = 1; break; }

                            sq = sq - stok;
                            oqty = stok;
                        }
                        else
                        {
                            rc = db.ExecuteCommand(sql1, sq, pltno, lstk, matnr, lgort, charg, stok, rqty);
                            if (rc == 0) { ret = 1; break; }

                            oqty = sq;
                            sq = 0;
                        }
                        if (lstk.Substring(0, 1) == "A")
                        {
                            db.ExecuteCommand("update milstk set lstk_io = '$', lstk_stat = '$R' where lstk_no = '" + lstk + "'");
                        }
                        rc = db.ExecuteCommand(sql2, docnum, tanum, tapos, lstk, pltno, oqty, rdat, rtim, remark, pksz, idate, itime, oprod, bwlvs, "$");
                        if (rc == 0) { ret = 3; break; }

                        rc = db.ExecuteCommand(sql3, oqty, docnum, tanum, tapos);
                        if (rc == 0) { ret = 4; break; }

                        lp++;

                        if (sq <= 0) break;
                    }
                    if (lp > 0) sc.Complete();

                } // end of scope

                if (ret == 1 || ret == 2) MessageBox.Show("재고상태가 변했읍니다.(update miplti)");
                if (ret == 3) MessageBox.Show("tiordx insert실패");
                if (ret == 4) MessageBox.Show("오더상태가 변했읍니다.(update miordi)");

                MessageBox.Show(lp.ToString() + " 건이 예약되었읍니다...!");


            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }

            retrieve();

        }

        private void btncncl_Click(object sender, EventArgs e)
        {
            ue_rsrv_cancel();
        }

        private void btnsel_r_Click(object sender, EventArgs e)
        {
            dv4.SelectAll();
        }

        private void ue_rsrv_cancel()
        {          
            if (Tab1.SelectedIndex != 1) return;
            if (dv3.SelectedRows.Count <= 0)
            {
                MessageBox.Show("취소할 항목을 아래에서 선택하세요...!");
                return;
            }
            if (MessageBox.Show("예약취소하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            decimal wmtxkey = 0, tanum = 0, oqty = 0;
            int tapos;
            string docnum, pltno, loca;

            int ret = 0, rc = 0, lp = 0;
         
            Cursor = Cursors.WaitCursor;
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv3.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    foreach (DataGridViewRow r in rr)
                    {

                        wmtxkey = Convert.ToDecimal(r.Cells["wmtxkey_r"].Value.ToString());
                        docnum = r.Cells["docnum_r"].Value.ToString();
                        tanum = Convert.ToDecimal(r.Cells["tanum_r"].Value.ToString());
                        tapos = Convert.ToInt32(r.Cells["tapos_r"].Value.ToString());
                        pltno = r.Cells["pltno_r"].Value.ToString();
                        loca = r.Cells["lstk_r"].Value.ToString();
                        oqty = Convert.ToDecimal(r.Cells["qty_r"].Value.ToString());

                        using (TransactionScope sc = new TransactionScope())
                        {
                            rc = db.p_etc_rsrv_cancel(docnum, tanum, tapos, wmtxkey, pltno, loca, oqty);
                            if (rc == 1)
                            {
                                db.SubmitChanges();
                                sc.Complete();
                                lp++;
                            }
                            else break;
                        }
                    }
                }
                Cursor = Cursors.Default;

                if (rc == -1) MessageBox.Show("상태변함 miwmto " + rc.ToString());
                if (rc == -2) MessageBox.Show("상태변함 tiwmtx " + rc.ToString());
                if (rc == -3) MessageBox.Show("상태변함 miplti " + rc.ToString());
                if (rc == -4) MessageBox.Show("상태변함 update miwmto " + rc.ToString());
                if (rc != 1) MessageBox.Show("실패했읍니다. 상태변함" + rc.ToString());
                               
                retrieve();
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            dv4.SelectAll();
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dv1_DataBindingComplete();
        }
        private void dv1_DataBindingComplete()
        {
            decimal qty = 0, rqty = 0, fqty = 0;
            decimal qty2 = 0, rqty2 = 0, fqty2 = 0, vol2 = 0;
            foreach (DataGridViewRow r in dv1.Rows)
            {
                qty = Convert.ToDecimal(r.Cells["vsolm"].Value.ToString());
                fqty = Convert.ToDecimal(r.Cells["fqty"].Value.ToString());

                if (fqty >= qty) r.DefaultCellStyle.BackColor = Color.DarkKhaki;

                qty2 = qty2 + Convert.ToDecimal(r.Cells["vsolm"].Value.ToString());
                rqty2 = rqty2 + Convert.ToDecimal(r.Cells["rqty"].Value.ToString());
                fqty2 = fqty2 + Convert.ToDecimal(r.Cells["fqty"].Value.ToString());
                vol2 = vol2 + Convert.ToDecimal(r.Cells["ltqty"].Value.ToString());
            }

            lblltqty.Text = string.Format("{0:n3}", vol2);
            lblqty.Text = string.Format("{0:n0}", qty2);
            lblrqty.Text = string.Format("{0:n0}", rqty2);
            lblfqty.Text = string.Format("{0:n0}", fqty2);
        }
      
      
        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void btneror_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("에러처리하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"delete from tiwmtx 
                                   where docnum is null or
                                   tanum is null or
                                   tapos is null or
                                   pltno is null or
                                   lstk is null or
                                   qty is null or
                                   flag is null or
                                   credat is null or
                                   cretim is null or
                                   pksz is null or
                                   remark is null or
                                   oprod is null or
                                   idate is null or
                                   itime is null");               
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("예약에러 처리하시겠읍니까?" + Environment.NewLine +
                                 "정상출고 예약이 있어 기출고예약불가합니다의 경우에 행하세요.", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                rc = d.ExecuteCommand("delete from tiordx where flag = '$R' ");

            }
            MessageBox.Show("삭제건=" + rc.ToString());
            

        }

        private void retrieve1()
        {
            if (dv1.SelectedRows.Count <= 0)
            {
                dv2.DataSource = null;
                return;
            }

            string prod = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string loc = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string lot = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = "";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<miplti_wmto>(sqls1, prod, loc, lot, bestq).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

        }
        private void retrieve2()
        {           
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<tiwmtxq>(sqls2).ToList();
                dv3.DataSource = q;

                dv3.TopLeftHeaderCell.Value = dv3.RowCount.ToString();
                dv3.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void retrieve3()
        {
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<tiwmtxq>(sqls3).ToList();
                dv4.DataSource = q;

                dv4.TopLeftHeaderCell.Value = dv4.RowCount.ToString();
                dv4.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
    public class miwmtox
    {
        public string credt { get; set; }
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public int tapos { get; set; }
        public string bwlvs { get; set; }
        public string matnr { get; set; }
        public string maktx { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public string bestq { get; set; }
        public string vltyp { get; set; }
        public string nltyp { get; set; }
        public string trart { get; set; }
        public decimal pksz { get; set; }
        public decimal vsolm { get; set; }
        public decimal ltqty { get; set; }
        public string sobkz { get; set; }
        public string lsonr { get; set; }
        public string wdatu { get; set; }
        public string wenum { get; set; }
        public string vfdat { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public decimal rqty { get; set; }
        public decimal fqty { get; set; }
        public string flag { get; set; }
        public string hdate { get; set; }
        public string htime { get; set; }

        public string hist_dt { get; set; }
    }
    public class miplti_wmto
    {
        public string lstk_no { get; set; }
        public string plti_pltno { get; set; }
        public string plti_prod { get; set; }
        public string plti_oprod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal? plti_pksz { get; set; }
        public string plti_remark { get; set; }
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
    public class tiwmtxq
    {
        public decimal wmtxkey { get; set; }
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public int tapos { get; set; }
        public string bwlvs { get; set; }
        public string IO { get; set; }
        public string lstk { get; set; }
        public string pltno { get; set; }
        public string matnr { get; set; }
        public string maktx { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public string bestq { get; set; }
        public decimal qty { get; set; }
        public string flag { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public decimal pksz { get; set; }
        public string oprod { get; set; }
        public string idate { get; set; }
        public string itime { get; set; }
        public string remark { get; set; }

    }
}
