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
using System.Transactions.Configuration;

namespace KK5
{
    public partial class FrmMiordi : Form
    {
        #region --- MDI Child ----------------
        private static FrmMiordi _instance;
        public static FrmMiordi Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMiordi();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmMiordi_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region --- sqlstatement ----------------
        string sqlm = @"  SELECT docnum,   
                                 credat,   
                                 cretim,   
                                 sdno,   
                                 route,   
                                 routedesc,   
                                 deltyp,   
                                 deltypdesc,   
                                 cust,   
                                 cust_name1,   
                                 cust_name2,   
                                 street,   
                                 post,   
                                 city,   
                                 tel,   
                                 contry,   
                                 region,   
                                 wecust,   
                                 wecust_name1,   
                                 wecust_name2,   
                                 westreet,   
                                 wepost,   
                                 wecity,   
                                 wetel,   
                                 wecontry,   
                                 weregion,   
                                 duedate,   
                                 cmmt,   
                                 rmrk,   
                                 parcel,   
                                 posnr,   
                                 matnr,   
                                 matnrdesc,   
                                 lgort,   
                                 charg,   
                                 plant,   
                                 qty,   
                                 gwgt,   
                                 nwgt,   
                                 wunit,   
                                 vol,   
                                 vunit,   
                                 pstyv,   
                                 pstyvdesc,   
                                 sono,   
                                 soposnr,   
                                 sodate,   
                                 custpo,   
                                 custpodate,   
                                 rqty,   
                                 fqty,   
                                 flag,   
                                 arrival,   
                                 car_no,   
                                 car_step,   
                                 car_sno,   
                                 print_step,   
                                 ordi_seq,   
                                 ordi_check,   
                                 remark,   
                                 bachadate,   
                                 ordi_ltqty,   
                                 ordi_size,   
                                 recv_dt,   
                                 hdate,   
                                 htime  
                            FROM miordi  
                           WHERE docnum is not null and lgort <> '2000' ";
        // tab1
        string sqls1 = @" SELECT lstk_no,   
                                lstk_io,   
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
                          and (lstk_stat in( '10', '$R' ))
                          and (plti_stok > 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot = {2})
                          and (plti_bestq = {3})  
                        UNION 
                         SELECT plti_lstk as lstk_no,   
                                '0' as lstk_io,   
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
                          and ((plti_lstk like 'Y%') )
                          and (plti_flag = '1') ";
                         // and(plti_pltno = '00000000')

        string sqls11 = @" SELECT lstk_no,   
                                lstk_io,   
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
                          and (lstk_stat in( '10', '$R' ))
                          and (plti_stok > 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot = {2})
                          and (plti_bestq = {3})";

        string sqls12 = @" SELECT plti_lstk as lstk_no,   
                                '0' as lstk_io,   
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
                          and ((plti_lstk like 'Y%') )
                          and (plti_flag = '1') ";
        //and(plti_pltno = '00000000')


        //string sqls2 = @"select ordxkey, docnum, sdno, posnr, pltno, lstk, qty, pksz, flag, credat, cretim, remark, idate, itime, oprod from tiordx 
        //                 where flag in ('$R') order by lstk ";
         //isnull(b.matnrdesc, '') as matnrdesc, 
        string sqls2 = @"select a.ordxkey, a.docnum, a.sdno, a.posnr, a.pltno, a.lstk, a.qty, a.pksz, a.flag, 
                                a.credat, a.cretim, a.remark, a.idate, a.itime, a.oprod, 
                                isnull(b.wecust_name1, '') as wecust_name1,
                                isnull(b.matnr, '') as matnr, 
                                isnull(b.matnrdesc, '') as matnrdesc,
                                isnull(b.lgort, '') as lgort, 
                                isnull(b.charg, '') as charg
                         from tiordx a inner join miordi b on a.docnum = b.docnum and a.sdno = b.sdno and a.posnr = b.posnr
                         where a.flag = '$R' and b.lgort <> '' and b.charg <> '0' and b.qty <> 0 order by a.lstk " ;

        //string sqls3 = @"select ordxkey, docnum, sdno, posnr, pltno, lstk, qty, pksz, flag, credat, cretim, remark, idate, itime, oprod from tiordx 
        //                 where flag in ('$R') order by lstk ";
        string sqls3 = @"select a.ordxkey, a.docnum, a.sdno, a.posnr, a.pltno, a.lstk, a.qty, a.pksz, a.flag, 
                                a.credat, a.cretim, a.remark, a.idate, a.itime, a.oprod, 
                                isnull(b.wecust_name1, '') as wecust_name1,
                                isnull(b.matnr, '') as matnr, 
                                isnull(b.matnrdesc, '') as matnrdesc, 
                                isnull(b.lgort, '') as lgort, 
                                isnull(b.charg, '') as charg
                         from tiordx a inner join miordi b on a.docnum = b.docnum and a.sdno = b.sdno and a.posnr = b.posnr 
                         where a.flag = '$Z' and b.lgort <> '' and b.charg <> '0' and b.qty <> 0 order by a.lstk ";
        #endregion

        DataGridView dv1, dv2, dv3, dv4;

        private void btnqury_Click(object sender, EventArgs e)
        {
         
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public FrmMiordi()
        {
            InitializeComponent();


            FormClosed += FrmMiordi_FormClosed;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionChanged += Dv1_SelectionChanged;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.RowPostPaint += Common.RowPostPaint;

            tab1.SelectedIndexChanged += Dv1_SelectionChanged;

            dv2 = dataGridView2;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = true;
            dv2.ReadOnly = true;
            dv2.RowPostPaint += Common.RowPostPaint;

            dv3 = dataGridView3;
            dv3.ReadOnly = true;
            dv3.AutoGenerateColumns = false;
            dv3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv3.MultiSelect = true;
            dv3.RowPostPaint += Common.RowPostPaint;

            dv4 = dataGridView4;
            dv4.ReadOnly = true;
            dv4.AutoGenerateColumns = false;
            dv4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv4.MultiSelect = true;
            dv4.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;

            dtDatefrom.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtDateTo.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            if (Config.UserLevel != "1")
            {
                btndel.Enabled = false;
                btn_c.Enabled = false;
                btn_e.Enabled = false;
                btn_w.Enabled = false;
                btn_r.Enabled = false;
            }
        }

        private void Dv4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
            //try
            //{
            //    if (e.ColumnIndex == 1)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + "-" + ls.Substring(5, 2);
            //            e.FormattingApplied = true;
            //        }
            //    }
            
            //}
            //catch (Exception E)
            //{
            //    //MessageBox.Show(E.Message);
            //}
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 8)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            e.Value = ls.Substring(0, 4) + "/" + ls.Substring(4, 2) + "/" + ls.Substring(6, 2);
            //            e.FormattingApplied = true;
            //        }
            //    }
            //    if (e.ColumnIndex == 12)
            //    {
            //        if (e.Value != null)
            //        {
            //            string ls = e.Value.ToString();
            //            if (ls == "1")
            //            {
            //                e.Value = "택배";
            //                e.CellStyle.ForeColor = Color.Red;
            //            }
            //            else
            //            {
            //                e.Value = "";
            //            }
            //            e.FormattingApplied = true;
            //        }
            //    }
            //}
            //catch (Exception E)
            //{
            //    //MessageBox.Show(E.Message);
            //}
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) retrieve1();
            if (tab1.SelectedIndex == 1) retrieve2();
            if (tab1.SelectedIndex == 2) retrieve3();
        }

        private void FrmMiordi_Load(object sender, EventArgs e)
        {
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand("delete from miordi where lgort = '' or charg = '0' or qty = 0 ");
            }

            retrieve();
        }
    
        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        #region --- 예약 루틴------------
        private void btn_r_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                int c = d.ExecuteQuery<int>("Select count(*) from tiwmtx where flag = '$R' ").SingleOrDefault();
                if (c > 0)
                {
                    MessageBox.Show("기타출고에 출고예약이 남아 있읍니다." + Environment.NewLine + Environment.NewLine +
                                    "실행시키던지 취소하고 정상출고 예약바랍니다.!" + Environment.NewLine +
                                    "정상출고예약과 기타출고예약은 동시에 안됩니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            string sel = "";
            using (FrmRsrvOption_p p = new FrmRsrvOption_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.radioButton1.Checked) sel = "1";
                if (p.radioButton2.Checked) sel = "2";
            }
            if (sel == "1") ue_rsrv_order();
            if (sel == "2") ue_rsrv_spec();

            retrieve();        

        }
  
        private void ue_rsrv_spec()
        {
            if (dv2.SelectedRows.Count<= 0) return;
            if (tab1.SelectedIndex != 0) return;


            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());
            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = "";
        
            decimal oq = Convert.ToDecimal(dv1.SelectedRows[0].Cells["qty"].Value.ToString());
            decimal rq = Convert.ToDecimal(dv1.SelectedRows[0].Cells["rqty"].Value.ToString());
            decimal oqty = 0;
            int lp = 0;
           
            decimal sq = oq - rq;
            if (sq == 0) return;

            string sql = @"select count(*) from miordi 
                          where  docnum = {0} and sdno = {1} and posnr = {2} and qty = {3} and rqty = {4} and qty - rqty > 0 ";

            DBDataContext db = new DBDataContext(Config.DBCon);

            int rc = db.ExecuteCommand(sql, docnum, sdno, posnr, oq, rq);
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
		                      and plti_bestq not in ('S', 'Q') ";

            string sql2 = @"INSERT INTO tiordx ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  credat,  cretim,   remark,   pksz,  idate,  itime,  oprod )  
                                        values ( {0},     {1},   {2},     {3},    {4},    {5},    '$R',   {6},     {7},     {8},      {9},   {10},        {11},        {12} ) ";

            string sql3 = @"update miordi set rqty = rqty + {0}	where  docnum = {1}	and sdno = {2} and posnr = {3} ";

           
            Cursor = Cursors.WaitCursor;
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    //db.p_tilock(); // readcommitted

                    string idate, itime, oprod;
                    decimal pksz;

                    foreach (DataGridViewRow r in dv2.SelectedRows)
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
                            rc = db.ExecuteCommand(sql1, stok, pltno, lstk, matnr, lgort, charg);
                            if (rc == 0) { ret = 1; break; }

                            sq = sq - stok;
                            oqty = stok;
                        }
                        else
                        {
                            rc = db.ExecuteCommand(sql1, sq, pltno, lstk, matnr, lgort, charg);
                            if (rc == 0) { ret = 1; break; }

                            oqty = sq;
                            sq = 0;
                        }
                        if (lstk.Substring(0, 1) == "A")
                        {
                            db.ExecuteCommand("update milstk set lstk_io = '$', lstk_stat = '$R' where lstk_no = '" + lstk + "'");
                        }
                        rc = db.ExecuteCommand(sql2, docnum, sdno, posnr, lstk, pltno, oqty, rdat, rtim, remark, pksz, idate,itime, oprod);
                        if (rc == 0) { ret = 3; break; }

                        rc = db.ExecuteCommand(sql3, oqty, docnum, sdno, posnr);
                        if (rc == 0) { ret = 4; break; }

                        lp++;

                        if (sq <= 0) break;
                    }
                    if (lp > 0) sc.Complete();

                } // end of scope

                if (ret == 1 || ret == 2) MessageBox.Show("재고상태가 변했읍니다.(update miplti)");
                if (ret == 3) MessageBox.Show("tiordx insert실패");
                if (ret == 3) MessageBox.Show("오더상태가 변했읍니다.(update miordi)");

                MessageBox.Show(lp.ToString() + " 건이 예약되었읍니다...!");
            }
            catch (Exception E)  { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }

        }
   
        private void ue_rsrv_order()
        {
            string credat = dv1.SelectedRows[0].Cells["credat"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int rc = 0;

            Cursor = Cursors.WaitCursor;            
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();              
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.p_rsrv_order(sdno, credat);
                        if (rc > 0)
                        {
                            db.Transaction.Commit();
                        }
                        else { db.Transaction.Rollback(); }
                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback(); Cursor = Cursors.Default;
                        MessageBox.Show(E.Message);
                    }                    
                }
                db.Connection.Close();           
            }
            MessageBox.Show(rc.ToString() + " 건이 예약되었읍니다...!");
        }
        #endregion

        #region --- 취소 루틴들 -------
        private void btn_c_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 1) ue_rsrv_cancel();
            if (tab1.SelectedIndex == 2) ue_cnfm_cancel();
        }

        private void ue_rsrv_cancel()
        {            
            if (tab1.SelectedIndex != 1) return;
            if (dv3.SelectedRows.Count <= 0)
            {
                MessageBox.Show("취소할 항목을 아래에서 선택하세요...!");
                return;
            }
            if (MessageBox.Show("예약취소하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            
            decimal ordxkey = 0, oqty = 0;
            string docnum, sdno, pltno, loca;
            int posnr = 0;        

            int ret = 0, rc = 0, lp = 0;
           
            Cursor = Cursors.WaitCursor;
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    foreach (DataGridViewRow r in dv3.SelectedRows)
                    {
                        ordxkey = Convert.ToDecimal(r.Cells["ordxkey_r"].Value.ToString());
                        docnum = r.Cells["docnum_r"].Value.ToString();
                        sdno = r.Cells["sdno_r"].Value.ToString();
                        posnr = Convert.ToInt32(r.Cells["posnr_r"].Value.ToString());
                        pltno = r.Cells["pltno_r"].Value.ToString();
                        loca = r.Cells["lstk_r"].Value.ToString();
                        oqty = Convert.ToDecimal(r.Cells["qty_r"].Value.ToString());

                        using (TransactionScope sc = new TransactionScope())
                        {
                            rc = db.p_rsrv_cancel(docnum, sdno, posnr, ordxkey, pltno, loca, oqty);
                            if (rc == 1)
                            {                           
                                sc.Complete();
                                lp++;
                            }
                            else break;
                        }
                    }
                }
                Cursor = Cursors.Default;

                if (rc == -1) MessageBox.Show("상태변함 miordi " + rc.ToString());
                if (rc == -2) MessageBox.Show("상태변함 tiordx " + rc.ToString());
                if (rc == -3) MessageBox.Show("상태변함 miplti " + rc.ToString());
                if (rc == -4) MessageBox.Show("상태변함 update miordi " + rc.ToString());
                if (rc != 1) MessageBox.Show("실패했읍니다. 상태변함" + rc.ToString());

                MessageBox.Show(lp.ToString() + " 건이 예약 취소되었읍니다...!");
                retrieve();
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }
        }
        private void ue_rsrv_cancel2()
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (tab1.SelectedIndex != 1) return;
            if (dv3.SelectedRows.Count == 0)
            {
                MessageBox.Show("취소할 항목을 아래에서 선택하세요...!");
                return;
            }
            if (MessageBox.Show("예약취소하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;


            decimal ordxkey = 0, oqty = 0;
            string docnum, sdno, pltno, loca;
            int posnr = 0;

            string sql1 = @"update miplti 
                            set plti_stok = plti_stok + {0}, 
		                        plti_rqty = plti_rqty - {0}
		                    where plti_pltno = {1}
		                      and plti_lstk = {2}
 	                          and plti_prod = {3}
		                      and plti_loc = {4}
		                      and plti_lot = {5}
		                      and plti_bestq not in( 'S', 'Q' ) ";


            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();

            int ret = 0, rc = 0, lp = 0;
            DBDataContext db = new DBDataContext(Config.DBCon);
            Cursor = Cursors.WaitCursor;
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    foreach (DataGridViewRow r in dv3.SelectedRows)
                    {

                        ordxkey = Convert.ToDecimal(r.Cells["ordxkey_r"].Value.ToString());
                        docnum = r.Cells["docnum_r"].Value.ToString();
                        sdno = r.Cells["sdno_r"].Value.ToString();
                        posnr = Convert.ToInt32(r.Cells["posnr_r"].Value.ToString());
                        pltno = r.Cells["pltno_r"].Value.ToString();
                        loca = r.Cells["lstk_r"].Value.ToString();
                        oqty = Convert.ToDecimal(r.Cells["qty_r"].Value.ToString());

                        rc = db.ExecuteCommand(@"delete from tiordx where ordxkey = {0} and flag = '$R'", ordxkey);
                        rc = db.ExecuteCommand(sql1, oqty, pltno, loca, matnr, lgort, charg);

                        if (loca.Substring(0, 1) == "A")
                        {
                            rc = db.ExecuteQuery<int>("select count(*) from tiwmtx where lstk = '" + loca + "'").SingleOrDefault();
                            if (rc == 0)
                            {
                                rc = db.ExecuteQuery<int>(@"select count(*) from miplti 
                                                    where plti_lstk = '" + loca + "' and plti_pltno = '" + pltno + "' and plti_rqty > 0").SingleOrDefault();
                                if (rc == 0)
                                {
                                    db.ExecuteCommand(@"update milstk set lstk_io = '0', lstk_stat = '10' where lstk_no = '" + loca + "'");
                                }
                            }
                        }
                        db.ExecuteCommand(@"update miordi rqty = rqty - {0} where docnum = {1} and sdno = {2} and posnr = {3} ");
                        lp++;
                    }
                    sc.Complete();
                }
                Cursor = Cursors.Default;

                MessageBox.Show(lp.ToString() + " 건이 예약 취소되었읍니다...!");
                retrieve();
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }

        }
        private void ue_cnfm_cancel()
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (tab1.SelectedIndex != 2) return;
            if (dv4.SelectedRows.Count == 0)
            {
                MessageBox.Show("확정 취소할 항목을 아래에서 선택하세요...!");
                return;
            }
            if (MessageBox.Show("확정 취소하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            decimal ordxkey = 0, oqty = 0, pksz = 0;
            string docnum, sdno, pltno, lstk, remark, idate, itime, oprod;
            int posnr = 0;      

            int ret = 0, rc = 0, lp = 0;

            DBDataContext db = new DBDataContext(Config.DBCon);
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

                    ordxkey = Convert.ToDecimal(r.Cells["ordxkey_f"].Value.ToString());
                    docnum = r.Cells["docnum_f"].Value.ToString();
                    sdno = r.Cells["sdno_f"].Value.ToString();
                    posnr = Convert.ToInt32(r.Cells["posnr_f"].Value.ToString());
                    pltno = r.Cells["pltno_f"].Value.ToString();
                    lstk = r.Cells["lstk_f"].Value.ToString();
                    oqty = Convert.ToDecimal(r.Cells["qty_f"].Value.ToString());

                    remark = r.Cells["remark_f"].Value.ToString();
                    pksz = Convert.ToDecimal(r.Cells["pksz_f"].Value.ToString());
                    idate = r.Cells["idate_f"].Value.ToString();
                    itime = r.Cells["itime_f"].Value.ToString();
                    oprod = r.Cells["oprod_f"].Value.ToString();

                    using (TransactionScope sc = new TransactionScope())
                    {
                        rc = db.p_out_cnfm_cancel(docnum, sdno, posnr, ordxkey, pltno, lstk, oqty, pksz, remark, idate, itime, oprod);
                        if (rc == 1)
                        {                         
                            sc.Complete();
                            lp++;
                        }
                        else break;
                    }
                }
                Cursor = Cursors.Default;
                if (rc != 1) MessageBox.Show("실패했읍니다. 상태변함");

                MessageBox.Show(lp.ToString() + " 건이 확정 취소되었읍니다...!");
                retrieve();
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }


        }

        #endregion

        #region  --- 실행및 확정루틴

        private void btn_w_Click(object sender, EventArgs e)
        {
           
            if (tab1.SelectedIndex != 1) return;
            if (dv3.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("아래 예약된 제품들을 모두 실행하시겠읍니까", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0;
            DBDataContext db = new DBDataContext(Config.DBCon);
            Cursor = Cursors.WaitCursor;
            try
            {
                string credat = "";  // delete
             
                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.p_out_exec(credat);
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
            finally { Cursor = Cursors.Default; }

            retrieve();
        }
        private void btn_e_Click(object sender, EventArgs e)
        {          
            //if (dv1.SelectedRows.Count <= 0) return;
            if (tab1.SelectedIndex != 2) return;
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
                            decimal ordxkey = Convert.ToDecimal(r.Cells["ordxkey_f"].Value.ToString());
                            string docnum = r.Cells["docnum_f"].Value.ToString();
                            string sdno = r.Cells["sdno_f"].Value.ToString();
                            int posnr = Convert.ToInt32(r.Cells["posnr_f"].Value.ToString());

                            db.ExecuteCommand("delete from tiordx where ordxkey = {0} and docnum = {1} and sdno = {2} and posnr = {3} ", ordxkey, docnum, sdno, posnr);
                            db.ExecuteCommand("delete from miordi where docnum = {0} and sdno = {1} and posnr = {2} and fqty >= qty", docnum, sdno, posnr);

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

        #endregion


        #region --- Retrieve 루틴 -----
        private void retrieve()
        {
            string modstr = sqlm;

            string date1 = dtDatefrom.Text;
            string date2 = dtDateTo.Text;

            //date1 = date1.Replace("-", "");
            //date2 = date2.Replace("-", "");

            if (!chkdt.Checked)
            {
                if (date1 != "") modstr = modstr + " and recv_dt >= {d'" + date1 + "'}";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and recv_dt >= {d'" + date1 + "'}";
                if (date2 != "") modstr = modstr + " and recv_dt <= {d'" + date2 + "'}";
            }

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '" + tbdoc.Text.Trim() + "%'";            
            if (tbord.Text.Trim() != "") modstr = modstr + " and sdno like '" + tbord.Text.Trim() + "%'";                      
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '" + tbprod.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '" + tbbatch.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and matnrdesc like '%" + txtpdesc.Text.Trim() + "%'";
            if (txtcustname.Text.Trim() != "") modstr = modstr + " and cust_name1 like '%" + txtcustname.Text.Trim() + "%'";

            if (!checkBox1.Checked)
            {
                modstr = modstr + " and lgort <> '' and charg <> '0' and qty <> 0 and lgort <> '2000' ";
            }else
            {
                modstr = modstr + " and (lgort = '' or charg = '0' or qty = 0  or lgort = '2000') ";
            }
            if (radioButton1.Checked)
                modstr = modstr + " order by cust_name1, recv_dt ";
            if (radioButton2.Checked)
                modstr = modstr + " order by recv_dt, cust_name1 ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                SortableBindingList<miordiq> q =  new SortableBindingList<miordiq>(db.ExecuteQuery<miordiq>(modstr).ToList());
                dv1.DataSource = q;

                //var q = db.ExecuteQuery<miordiq>(modstr).ToList();
                //dv1.DataSource = q;
                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (tab1.SelectedIndex == 0) retrieve1();
            if (tab1.SelectedIndex == 1) retrieve2();
            if (tab1.SelectedIndex == 2) retrieve3();
        }

        private void btnsel_Click(object sender, EventArgs e)
        {
            dv2.SelectAll();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal qty = 0, rqty = 0, fqty = 0;
            decimal qty2 = 0, rqty2 = 0, fqty2 = 0, vol2 = 0;
            foreach (DataGridViewRow r in dv1.Rows)
            {
               qty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
               fqty = Convert.ToDecimal(r.Cells["fqty"].Value.ToString());

               if (fqty >= qty) r.DefaultCellStyle.BackColor = Color.DarkKhaki;

                qty2 = qty2 + Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                rqty2 = rqty2 + Convert.ToDecimal(r.Cells["rqty"].Value.ToString());
                fqty2 = fqty2 + Convert.ToDecimal(r.Cells["fqty"].Value.ToString());
                vol2 = vol2 + Convert.ToDecimal(r.Cells["vol"].Value.ToString());
            }

            lblltqty.Text = string.Format("{0:n3}", vol2);
            lblqty.Text = string.Format("{0:n0}", qty2);
            lblrqty.Text = string.Format("{0:n0}", rqty2);
            lblfqty.Text = string.Format("{0:n0}", fqty2);

            Cursor = Cursors.Arrow;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            retrieve1();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            decimal qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["qty"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["rqty"].Value.ToString());
            if (rqty > 0) return;

            if (qty == 0m || lgort.Trim() == "" || charg.Trim() == "0" || rqty == 0)
            {
                if (MessageBox.Show("삭제하시겠읍니까?", "확인",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                int rc = 0;
                Cursor = Cursors.WaitCursor;
                try
                {
                    using (DBDataContext db = new DBDataContext(Config.DBCon))
                    {
                        rc = db.ExecuteCommand(@"delete from taordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from haordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from hiordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from miordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);

                        if (rc <= 0)
                            MessageBox.Show("삭제 실패!");
                        else
                            dv1.Rows.Remove(dv1.SelectedRows[0]);
                    }
                }
                finally { Cursor = Cursors.Default; }              
                //retrieve();
            }
        }
      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;

            Clipboard.SetText(dataGridView1.CurrentCell.Value.ToString());

        }

        private void tbdoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbdoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void tbord_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbord.Text = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbprod.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["matnrdesc"].Value.ToString();
        }

        private void btnsel2_Click(object sender, EventArgs e)
        {
            dv3.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dv4.SelectAll();
        }

        private void Page1_Click(object sender, EventArgs e)
        {

        }

        private void btneror_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("에러처리하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"delete from tiordx 
                                   where docnum is null or
                                   sdno is null or
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

        private void btndel2_Click(object sender, EventArgs e)
        {

            if (dv1.SelectedRows.Count <= 0) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            decimal qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["qty"].Value.ToString());
            decimal rqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["rqty"].Value.ToString());
            if (rqty == 0) return;

            if (rqty > 0)
            {
                if (MessageBox.Show("진짜 삭제하시겠읍니까?", "확인",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                if (MessageBox.Show("장말 삭제하시겠읍니까?", "확인",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                int rc = 0;
                Cursor = Cursors.WaitCursor;
                try
                {
                    using (DBDataContext db = new DBDataContext(Config.DBCon))
                    {
                        rc = db.ExecuteCommand(@"delete from taordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from haordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from hiordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);
                        rc = db.ExecuteCommand(@"delete from miordi where docnum = {0} and sdno = {1} and posnr = {2}", docnum, sdno, posnr);

                        if (rc <= 0)
                            MessageBox.Show("삭제 실패!");
                        else
                            dv1.Rows.Remove(dv1.SelectedRows[0]);
                    }
                }
                finally { Cursor = Cursors.Default; }
                //retrieve();
            }
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtcustname_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtcustname.Text = dv1.SelectedRows[0].Cells["cust_name1"].Value.ToString();
        }

        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) retrieve1();
            if (tab1.SelectedIndex == 1) retrieve2();
            if (tab1.SelectedIndex == 2) retrieve3();
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

            string ls = comboBox1.Text;
            string sql = sqls1;

            if (ls.Substring(0, 2) == "A:")
            {
                sql = sqls11;
                sql = sql + " and ( substring(plti_lstk,1,1) = 'A' ) order by lstk_no ";
            }
            if (ls.Substring(0, 2) == "Y:")
            {
                sql = sqls12;
                sql = sql + " order by lstk_no ";
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<miplti_tab1>(sql, prod, loc, lot, bestq).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

        }
        private void retrieve2()
        {
            //if (dv1.SelectedRows.Count == 0)
            //{
            //    dv3.DataSource = null;
            //    return;
            //}

            //string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            //string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            //int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());         

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<tiodxq>(sqls2).ToList();
                dv3.DataSource = q;

                dv3.TopLeftHeaderCell.Value = dv3.RowCount.ToString();
                dv3.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
        private void retrieve3()
        {
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {                
                var q = db.ExecuteQuery<tiodxq>(sqls3).ToList();
                dv4.DataSource = q;

                dv4.TopLeftHeaderCell.Value = dv4.RowCount.ToString();
                dv4.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

        }
        #endregion
    }

    public class miordiq
    {
        public string docnum { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string sdno { get; set; }
        
        public string route { get; set; }
        public string routedesc { get; set; }
        public string deltyp { get; set; }
        public string deltypdesc { get; set; }
        public string cust { get; set; }
        public string cust_name1 { get; set; }
        public string cust_name2 { get; set; }
        public string street { get; set; }
        public string post { get; set; }
        public string city { get; set; }
        public string tel { get; set; }
        public string contry { get; set; }
        public string region { get; set; }
        public string wecust { get; set; }
        public string wecust_name1 { get; set; }
        public string wecust_name2 { get; set; }
        public string westreet { get; set; }
        public string wepost { get; set; }
        public string wecity { get; set; }
        public string wetel { get; set;} 
        public string wecontry { get; set; }
        public string weregion { get; set; }
        public string duedate { get; set; }
        public string cmmt { get; set; }
        public string rmrk { get; set; }
        public string parcel { get; set; }
        public int posnr { get; set; }
        public string matnr { get; set; }
        public string matnrdesc { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public string plant { get; set; }
        public decimal qty { get; set; }
        public decimal gwgt { get; set; }
        public decimal nwgt { get; set; }
        public string wunit { get; set; }
        public decimal vol { get; set; }
        public string vunit { get; set; }
        public string pstyv { get; set; }
        public string pstyvdesc { get; set; }
        public string sono { get; set; }
        public int soposnr { get; set; }
        public string sodate { get; set; }
        public string custpo { get; set; }
        public string custpodate { get; set; }
        public decimal rqty { get; set; }
        public decimal fqty { get; set; }
        public string flag { get; set; }
        public string arrival { get; set; }
        public string car_no { get; set; }
        public string car_step { get; set; }
        public int car_sno { get; set; }
        public string print_step { get; set; }
        public int ordi_seq { get; set; }
        public string ordi_check { get; set; }
        public string remark { get; set; }
        public string bachadate { get; set; }
        public decimal ordi_ltqty { get; set; }
        public decimal ordi_size { get; set; }
        public DateTime recv_dt { get; set; }
        public string hdate { get; set; }
        public string htime { get; set; }
        public string vgbel { get; set; }
        public string vsbed { get; set; }
        public string ablad { get; set; }

        public string hist_dt { get; set; }

    }

    public class miplti_tab1
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

    public class tiodxq
    {
        public decimal ordxkey { get; set; }
        public string docnum { get; set; }
        public string sdno { get; set; }
        public int posnr { get; set; }
        public string pltno { get; set; }
        public string lstk { get; set; }
        public string matnr { get; set; }
        public string matnrdesc { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public decimal qty { get; set; }
        public string flag { get; set; }
        public decimal pksz { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string remark { get; set; }
        public string idate { get; set; }
        public string itime { get; set; }
        public string oprod { get; set; }
        public string wecust_name1 { get; set; }
    }

}
