using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;

namespace KK5
{
    public partial class FrmTaordiLoad : Form
    {
        #region --- MDI Child ----------------
        private static FrmTaordiLoad _instance;
        public static FrmTaordiLoad Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmTaordiLoad();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        #region ---  sql statements   -----------------

        string sqlm = @"SELECT  tacar.duedate ,
                               tacar.car_no ,
                               tacar.seq ,
                               tacar.car_man ,
                               tacar.car_dest ,
                               tacar.max_vol ,
                               tacar.load_vol ,
                               tacar.load_qty ,
                               tacar.step ,
                               tacar.remark ,
                               tacar.bachaDate ,
                               tacar.area_code ,
                               tacar.uuse ,
                               tacar.car_desc
                         FROM tacar WHERE tacar.bachadate is not null and flag = '' ";

        string sqls = @" SELECT taordi.docnum,   
                             taordi.credat,   
                             taordi.cretim,   
                             taordi.sdno,   
                             taordi.route,   
                             taordi.routedesc,   
                             taordi.deltyp,   
                             taordi.deltypdesc,   
                             taordi.cust,   
                             taordi.cust_name1,   
                             taordi.cust_name2,   
                             taordi.street,   
                             taordi.post,   
                             taordi.city,   
                             taordi.tel,   
                             taordi.contry,   
                             taordi.region,   
                             taordi.wecust,   
                             taordi.wecust_name1,   
                             taordi.wecust_name2,   
                             taordi.westreet,   
                             taordi.wepost,   
                             taordi.wecity,   
                             taordi.wetel,   
                             taordi.wecontry,   
                             taordi.weregion,   
                             taordi.duedate,   
                             taordi.cmmt,   
                             taordi.rmrk,   
                             taordi.parcel,   
                             taordi.posnr,   
                             taordi.matnr,   
                             taordi.matnrdesc,   
                             taordi.lgort,   
                             taordi.charg,   
                             taordi.plant,   
                             taordi.qty,   
                             taordi.gwgt,   
                             taordi.nwgt,   
                             taordi.wunit,   
                             taordi.vol,   
                             taordi.vunit,   
                             taordi.pstyv,   
                             taordi.pstyvdesc,   
                             taordi.sono,   
                             taordi.soposnr,   
                             taordi.sodate,   
                             taordi.custpo,   
                             taordi.custpodate,   
                             taordi.rqty,   
                             taordi.fqty,   
                             taordi.flag,   
                             taordi.arrival,   
                             taordi.car_no,   
                             taordi.car_step,   
                             taordi.car_sno,   
                             taordi.print_step,   
                             taordi.ordi_seq,   
                             taordi.ordi_check,   
                             taordi.remark,   
                             taordi.bachadate,   
                             taordi.ordi_ltqty,   
                             taordi.ordi_size,   
                             taordi.recv_dt,   
                             taordi.hdate,   
                             taordi.htime
                    FROM taordi
                    WHERE taordi.docnum is not null  ";
        #endregion
        private void FrmTaordiLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv1, dv2;
        public FrmTaordiLoad()
        {
            InitializeComponent();
            FormClosed += FrmTaordiLoad_FormClosed; ;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.CellFormatting += Dv1_CellFormatting;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.SelectionChanged += Dv1_SelectionChanged;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            dv2.CellFormatting += Dv2_CellFormatting;
            dv2.RowPostPaint += Common.RowPostPaint;

            //if (Config.UserLevel != "2")
            //{
            //    btnChg.Enabled = false;
            //    btncmmt.Enabled = false;
            //    btncncl.Enabled = false;
            //    btndeliverydone.Enabled = false;
            //    btnfinish.Enabled = false;
            //    btnexcel.Enabled = false;
            //    btnsel.Enabled = false;
            //    btnchgqty.Enabled = false;
            //    btnChg.Enabled = false;
            //}
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 7)
            {
                if(e.Value != null)
                {
                    string ls = e.Value.ToString();
                    if (ls == "1") e.Value = "상차중";
                    if (ls == "2") e.Value = "상차완료";
                    e.FormattingApplied = true;
                }
            }

        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }


        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTraordiLoad_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            retrieve();
        }
        private void retrieve()
        {
            string modstr = sqlm;

            string date1 = dtDatefrom.Text.Replace("-", "/");
            string date2 = dtDateTo.Text.Replace("-", "/");

            if (chkdt.Checked)
            {
                modstr = modstr + " and bachadate >= '" + date1 + "'";
                modstr = modstr + " and bachadate <= '" + date2 + "'";
            }
            else
            {
                modstr = modstr + " and bachadate = '" + date1 + "'";
            }
            modstr = modstr + " and step <> '0' and flag = '' ";
           
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<LoadedTcar>(modstr).ToList();
                dv1.DataSource = q;

            }
        }

        private void btnChg_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            DataGridViewRow r = dv1.SelectedRows[0];
            string bachadate = r.Cells["bachadate"].Value.ToString();
            string car_no = r.Cells["car_no"].Value.ToString();
            int ll_seq = Convert.ToInt32(r.Cells["seq"].Value.ToString());
            string lstep = r.Cells["step"].Value.ToString();
            string ncar_no = "";

            using (FrmChangeCarLoad_p p = new FrmChangeCarLoad_p(car_no))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                ncar_no = p.textBox2.Text;
            }
            decimal new_max_vol = 0;
            string sql = "";

            decimal load_vol = 0;
            int load_qty = 0, seq = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                sql = "select step, max_vol from tacar where car_no = '" + ncar_no + "' and step = '0' ";
                var q = db.ExecuteQuery(sql).ToList();
                if (q.Count == 0) { MessageBox.Show("해당차량이 없읍니다...!"); return; }
                foreach (var s in q)
                {
                    lstep = s.step;
                    new_max_vol = s.max_vol;
                }
                if (lstep != "0")
                {
                    MessageBox.Show("대상 차량이 대기상태가 아닙니다...!");
                    return;
                }
                sql = "select bachadate, seq, load_vol, load_qty from tacar where car_no = '" + car_no + "'";
                var q2 = db.ExecuteQuery(sql).SingleOrDefault();
                if (q2 == null) { MessageBox.Show("상태가 변했읍니다1"); return; }

                bachadate = q2.bachadate;
                seq = q2.seq;
                load_vol = q2.load_vol;
                load_qty = q2.load_qty;
            }

            string full = "";
            if (load_vol > new_max_vol) full = "2";
            else full = "1";

            bool ff = false;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        //new car alloc
                        sql = @"update tacar set load_vol = {0}, load_qty = {1}, step = {2},  bachaDate = {3}, seq = {4} where car_no = {5} and step = {6} ";

                        int ret = db.ExecuteCommand(sql, load_vol, load_qty, full, bachadate, seq, ncar_no, lstep);
                        if (ret == 0)
                        {
                            db.Transaction.Rollback();
                            ff = true;
                        }
                        else
                        {
                            // old car clear
                            sql = @"update tacar set load_qty = 0, load_vol = 0, step = '0', bachadate = '', seq = 0 where car_no = {0} ";
                            ret = db.ExecuteCommand(sql, car_no);
                            if (ret == 0)
                            {
                                db.Transaction.Rollback();
                                ff = true;
                            }
                            else
                            {
                                sql = @"update taordi set car_no = {0} where car_no = {1} and bachadate = {2} and car_sno = {3}";
                                ret = db.ExecuteCommand(sql, ncar_no, car_no, bachadate, seq);
                                if (ret == 0)
                                {
                                    db.Transaction.Rollback();
                                    ff = true;
                                }
                                else
                                {
                                    db.Transaction.Commit();
                                }
                            }
                        }
                    }
                    catch(Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();
            }
            if (ff) MessageBox.Show("상태가 변했읍니다");
            else    MessageBox.Show("차량변경 성공되었읍니다...!");

            retrieve();
        }

        private void btnfinish_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            string ls_step = dv1.SelectedRows[0].Cells["step"].Value.ToString();

            if (ls_step != "1") return;

            if (MessageBox.Show("상차완료처리 하시겠읍니까?", "상차완료",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string sql = @"update tacar set step = '2' where bachadate = {0} and car_no = {1} and seq = {2} and step = '1'";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int ret = db.ExecuteCommand(sql, bachadate, car_no, seq);
                if (ret == 0) { MessageBox.Show("상태가 변했읍니다...!"); }
            }                          
            retrieve();
        }

        private void btndeliverydone_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;

            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int ll_seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            string ls_step = dv1.SelectedRows[0].Cells["step"].Value.ToString();
            if (ls_step !=  "2") return;

            if (MessageBox.Show("배달완료처리 하시겠읍니까?", "배달완료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);

            string dstr = "";
            db.p_curgetdatetime14(ref dstr);

            string hdate = dstr.Substring(0, 8);
            string htime = dstr.Substring(8, 6);

            int rc = 0, st = 0;
            bool ff = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                using (TransactionScope sc = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    // 차량이력 insert-> 이력일자 Update -> 기존것 update clear
                    rc = db.ExecuteCommand(@"insert into hacar 
                                             select * from tacar 
                                             where bachadate = {0} and car_no = {1} and seq = {2} and step = {3}",
                                             bachadate, car_no, ll_seq, ls_step);
                    if (rc == 0) { st = 1; ff = true; }
                    else
                    {
                        db.ExecuteCommand(@"update hacar set hdate = {0}, htime = {1} where bachadate = {2} and car_no = {3} and seq = {4}",
                                            hdate, htime, bachadate, car_no, ll_seq);

                        db.ExecuteCommand(@"update tacar set step ='0', bachadate = '', seq = 0, load_qty = 0, load_vol = 0, car_dest = '',	 remark = ''
                                            where bachadate = {0} and car_no = {1} and seq = {2} and step = {3}",
                                            bachadate, car_no, ll_seq, ls_step);


                        // 상차이력  insert-> 이력일자 Update -> 기존것 삭제
                        rc = db.ExecuteCommand(@"insert into haordi 
                                                 select * from taordi 
                                                 where bachadate = {0} and car_no = {1} and car_sno = {2} ",
                                                 bachadate, car_no, ll_seq);
                        if (rc == 0) { st = 2; ff = true; }
                        else
                        {
                            rc = db.ExecuteCommand(@"update haordi set hdate = {0}, htime = {1} where bachadate = {2} and car_no = {3} and car_sno = {4} ",
                                                     hdate, htime, bachadate, car_no, ll_seq);

                            db.ExecuteCommand(@"delete from taordi where bachadate = {0} and car_no = {1} and car_sno = {2} ",
                                                     bachadate, car_no, ll_seq);
                        }
                    }
                    if (!ff)
                    {
                        sc.Complete();

                        Cursor = Cursors.Default;
                        MessageBox.Show("배달완료 OK...!");
                    }
                } // end of transaction

                if (ff)
                {
                    Cursor = Cursors.Default;
                    if (st == 1) MessageBox.Show("차량이력 기록실패입입니다.");
                    if (st == 2) MessageBox.Show("상차이력 기록실패입입니다.");
                }
            }
            catch (Exception E)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(E.Message);
                if (st == 1) MessageBox.Show("차량이력 기록실패입입니다.");
                if (st == 2) MessageBox.Show("상차이력 기록실패입입니다.");
            }
          
            retrieve();
        }

        private void btncncl_Click(object sender, EventArgs e)
        {
          
            if (dv1.SelectedRows.Count == 0) return;
            string ls = "";
            using (FrmLoadCancel_p p = new FrmLoadCancel_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
               
                if (p.radioButton1.Checked) ls = "carby";
                else ls = "eachby";
            }

            if (ls == "eachby") loadcncl_each();
            else loadcncl_all();

            retrieve();
        }
        private void loadcncl_all()
        {
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            decimal load_qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["load_qty"].Value.ToString());
            
            int rc = 0;
            DBDataContext db = new DBDataContext(Config.DBCon);
            using (TransactionScope sc = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {              
                rc = db.s_loadcncl_all(bachadate, car_no, seq, load_qty);
              
                if (rc == 1)
                {
                    db.SubmitChanges();
                    sc.Complete();
                }
            }
            if (rc != 1)
                MessageBox.Show("상태가 변했읍니다." + rc.ToString());
            else
                MessageBox.Show("해당차량 상차취소 되었읍니다...!");
        }
        private void loadcncl_each()
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (dv2.SelectedRows.Count == 0) return;

            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            string step = dv1.SelectedRows[0].Cells["step"].Value.ToString();
            decimal load_qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["load_qty"].Value.ToString());

            if (step != "1") return;

            if (MessageBox.Show("선택하신제품들을 상차 개별취소처리 하시겠읍니까?", "상차개별취소",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            DBDataContext db = new DBDataContext(Config.DBCon);
            int lc = db.ExecuteQuery<int>(@"select count(*) from tacar where bachadate = {0} and car_no = {1} and seq = {2} and load_qty = {3} and step in ('1', '2') ",
                                                                              bachadate, car_no, seq, load_qty).SingleOrDefault();
            if (lc == 0)
            {
                MessageBox.Show("상태가 변했읍니다...!");
                return;
            }
            int rc = 0;
            bool ff = false;
            int st = 0;
            int lp = 0;
            try
            {
                using (TransactionScope sc = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv2.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    foreach (DataGridViewRow r in rr)
                    {
                    
                        string docnum = r.Cells["docnum"].Value.ToString();
                        string sdno = r.Cells["sdno"].Value.ToString();
                        int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                        int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                        int car_sno = Convert.ToInt32(r.Cells["car_sno"].Value.ToString());

                        decimal ordi_oqty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                        decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());
                        rc = db.ExecuteCommand(@"update taordi 
                                                    set car_no = '', bachadate = '', car_sno = 0, car_step = '0', print_step = '0' 
                                                 where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3} and bachadate = {4} and car_no = {5} and car_sno = {6} ",
                                                 docnum, sdno, posnr, ordi_seq, bachadate, car_no, car_sno);
                        if (rc == 0) { st = 1; ff = true; break; }
                        else
                        {
                            rc = db.ExecuteCommand(@"update tacar set load_qty = load_qty - {0}, load_vol = load_vol - {1}
                                                     where bachadate = {2} and car_no = {3} and seq = {4} ",
                                                     ordi_oqty, ordi_ltqty, bachadate, car_no, car_sno);
                            if (rc == 0) { st = 2; ff = true; break; }
                        }
                        lp++;
                    }
                    if (!ff)
                    {
                        db.ExecuteCommand(@"update tacar 
                                              set load_qty = 0, load_vol = 0, bachadate = '', seq = 0, remark = '', duedate = '', step = '0'	
                                            where car_no = {0} 
                                              and 0 < (select count(*) from tacar where car_no = {0} and ( load_qty <= 0 or load_vol <= 0 ) ) ", car_no);
                        
                        sc.Complete();                       
                    }                   
                }
                if (ff) MessageBox.Show("상태가 변했읍니다...!");
                else MessageBox.Show(lp.ToString()+ " 개 행이 취소되었읍니다..!");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
            retrieve();
        }
       
        private void btnchgqty_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (dv2.SelectedRows.Count == 0) return;
            if (dv2.SelectedRows.Count > 1)
            {
                MessageBox.Show("레코드 한줄만 선택하세요...!");
                return;
            }
            int sqty = 0;
            int  qty = (int) Convert.ToDecimal(dv2.SelectedRows[0].Cells["qty"].Value.ToString());

            using (FrmLoadCnclQty_p p = new FrmLoadCnclQty_p(qty))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                sqty = (int)p.numericTextox1.Value;
                if (sqty == 0)
                {
                    loadcncl_each();
                    retrieve();
                    return;
                }

            }
            string docnum = dv2.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv2.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv2.SelectedRows[0].Cells["posnr"].Value.ToString());
            int ordi_seq = Convert.ToInt32(dv2.SelectedRows[0].Cells["ordi_seq"].Value.ToString());
            decimal ordi_size = Convert.ToDecimal(dv2.SelectedRows[0].Cells["ordi_size"].Value.ToString());
            string car_no = dv2.SelectedRows[0].Cells["car_no2"].Value.ToString();

            int cncl_qty = qty - sqty;
            decimal cncl_ltqty = (qty - sqty) * ordi_size;
            int ret = 0, rc = 0;
            bool ff = false;
            int st = 0;
            DBDataContext db = new DBDataContext(Config.DBCon);
            try
            {
                using (TransactionScope sc = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    rc = db.ExecuteCommand(@"update tacar 
                                                set load_vol = load_vol - {0}, 
                                                    load_qty = load_qty - {1} 
                                              where car_no = {2} and step in ( '1', '2' )",
                                              cncl_ltqty, cncl_qty, car_no);
                    if (rc != 0)
                    {
                        ret = db.ExecuteQuery<int>(@"select isnull(max(ordi_seq),0) + 1 
                                                     from taordi 
                                                     where docnum = {0} and sdno = {1} and posnr = {2} ", docnum, sdno, posnr).SingleOrDefault();

                        rc = db.ExecuteCommand(@"  
                             INSERT INTO taordi  
                                 ( docnum,        credat,         cretim,             sdno,              route,   
                                   routedesc,     deltyp,         deltypdesc,         cust,              cust_name1,   
                                   cust_name2,    street,         post,               city,              tel,   
                                   contry,        region,         wecust,             wecust_name1,      wecust_name2,   
                                   westreet,      wepost,         wecity,             wetel,             wecontry,   
                                   weregion,      duedate,        cmmt,               rmrk,              parcel,   
                                   posnr,         matnr,          matnrdesc,          lgort,             charg,   
                                   plant,         qty,            gwgt,               nwgt,              wunit,   
                                   vol,           vunit,          pstyv,              pstyvdesc,         sono,   
                                   soposnr,       sodate,         custpo,             custpodate,        rqty,   
                                   fqty,          flag,           arrival,            car_no,            car_step,   
                                   car_sno,       print_step,     ordi_seq,           ordi_check,        remark,   
                                   bachadate,     ordi_ltqty,     ordi_size,          recv_dt,           hdate,    vgbel, 
                                   htime,         vsbed,          ablad )  
                             select          
  	                               docnum,        credat,         cretim,             sdno,              route,   
                                   routedesc,     deltyp,         deltypdesc,         cust,              cust_name1,   
                                   cust_name2,    street,         post,               city,              tel,   
                                   contry,        region,         wecust,             wecust_name1,      wecust_name2,   
                                   westreet,      wepost,         wecity,             wetel,             wecontry,   
                                   weregion,      duedate,        cmmt,               rmrk,              parcel,   
                                   posnr,         matnr,          matnrdesc,          lgort,             charg,   
                                   plant,         {0},            gwgt,               nwgt,              wunit,   
                                   vol,           vunit,          pstyv,              pstyvdesc,         sono,   
                                   soposnr,       sodate,         custpo,             custpodate,        rqty,   
                                   fqty,          flag,           arrival,            '',                '0',   
                                   0,             print_step,     {1},                ordi_check,        remark,   
                                   '',            {2},            ordi_size,          recv_dt,           hdate,   vgbel, 
                                   htime,         vsbed,          ablad           
                             from  taordi where docnum = {3} and sdno = {4} and posnr = {5} and ordi_seq = {6} ", cncl_qty, ret, cncl_ltqty, docnum, sdno, posnr, ordi_seq);
                        if (rc != 0)  // ok
                        {
                            rc = db.ExecuteCommand(@"update taordi set qty = {0}, ordi_ltqty = {0} * {1} 
                                                     where docnum = {2} and sdno = {3} and posnr = {4} and ordi_seq = {5} ",
                                                     sqty, ordi_size, docnum, sdno, posnr, ordi_seq);
                            if (rc == 0) { ff = true; st = 3; }
                            else
                            {
                                sc.Complete();
                            }
                        }
                        else { ff = true; st = 2; }
                    }
                    else { ff = true; st = 1; }

                } // end of transactionscope
                if (ff) MessageBox.Show("상태가 변했읍니다...!" + st.ToString());
                else MessageBox.Show("수량 조정 성공입니다...!");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
            retrieve();

        }

        private void btnremark_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count == 0) return;

            string remark = dv2.SelectedRows[0].Cells["remark2"].Value.ToString();
            string nremark = "";
            using (FLoadCarRemark_p p = new FLoadCarRemark_p(remark))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                nremark = p.textBox1.Text;
            }

            int lp = 0;
            int rc = 0;
            bool ff = false;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv2.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    string docnum = r.Cells["docnum"].Value.ToString();
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());

                    rc = db.ExecuteCommand(@" update taordi set remark = {0} where docnum = {1} and sdno = {2} and posnr = {3} and ordi_seq = {4} ",
                                              nremark, docnum, sdno, posnr, ordi_seq);
                    if (rc == 0) { ff = true; break; }
                    lp++;
                }
                if (ff) MessageBox.Show("상태가 변했읍니다...!");
                else MessageBox.Show(lp.ToString() + " 개의 레코드가 반영되었읍니다...!");
            }                

            retrieve();
        }

        private void btncmmt_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count == 0) return;

            string duedate = dv2.SelectedRows[0].Cells["duedate"].Value.ToString();
            string rmrk = dv2.SelectedRows[0].Cells["rmrk"].Value.ToString();
            string cmmt = dv2.SelectedRows[0].Cells["cmmt"].Value.ToString();
            string parcel = dv2.SelectedRows[0].Cells["parcel"].Value.ToString();
            string sdno = dv2.SelectedRows[0].Cells["sdno"].Value.ToString();

            string ncmmt = "";
            string nrmrk = "";
            string nparcel = "";
            using (FrmLoadCarCmmt_p p = new FrmLoadCarCmmt_p(sdno, rmrk, parcel, cmmt))
            {
                if (p.ShowDialog() == DialogResult.Cancel) return;
                nrmrk = p.textBox1.Text;
                if (p.checkBox1.Checked) nparcel = "1"; else nparcel = "";
                ncmmt = p.richTextBox1.Text;
            }
            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.ExecuteCommand(@"update taordi set rmrk ={0}, cmmt = {1}, parcel = {2} where duedate = {3} and sdno = {4} and car_no <> '' ", 
                                     nrmrk, ncmmt, nparcel, duedate, sdno);
            }
            if(rc > 0) MessageBox.Show(rc.ToString() + " 개의 레코드가 반영되었읍니다");
            else MessageBox.Show(" 상태가 변했읍니다");

            retrieve();
        }

        private void btnsel_Click(object sender, EventArgs e)
        {
            if (dv2.Rows.Count <= 0) return;
            dv2.SelectAll();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
           
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.ExecuteCommand(@"update taordi set print_step = '1' where bachadate = {0} and car_no = {1} and car_sno = {2}", bachadate, car_no, seq);
            }

            using (FrmPrintToExcel2 p = new FrmPrintToExcel2(bachadate, car_no, seq, false))
            {
                p.ShowDialog();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0)
            {
                dv2.DataSource = null;
                return;
            }
            
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();

            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());

            string modstr = sqls;

            modstr = modstr + " and bachadate = '" + bachadate + "'";
            modstr = modstr + " and car_no = '" + car_no + "'";
            modstr = modstr + " and car_sno = " + seq.ToString() ;
            modstr = modstr + " order by arrival, sdno, matnrdesc, ordi_size desc";

            using (DBDataContext ctx = new DBDataContext(Config.DBCon))
            {
                dv2.DataSource = new SortableBindingList<taordiq>(ctx.ExecuteQuery<taordiq>(modstr).ToList());

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }                
           
            return;       
        }

    }
    public class LoadedTcar
    {
        public string bachadate { get; set; }
        public int seq { get; set; }
        public string car_no { get; set; }
        public string car_man { get; set; }
        public string car_desc { get; set; }
        public decimal load_vol { get; set; }
        public decimal max_vol { get; set; }
        public decimal load_qty { get; set; }
        public string step { get; set; }
        public string area_code { get; set; }
        public string remark { get; set; }
        public string car_dest { get; set; }
    }

}
