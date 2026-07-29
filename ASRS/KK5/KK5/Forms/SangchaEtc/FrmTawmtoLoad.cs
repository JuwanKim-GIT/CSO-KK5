using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK5
{
    public partial class FrmTawmtoLoad : Form
    {
        #region --- MDI Child ----------------
        private static FrmTawmtoLoad _instance;
        public static FrmTawmtoLoad Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmTawmtoLoad();
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
                         FROM tacar WHERE tacar.bachadate is not null ";

        string sqls = @"Select
                           docnum,
                           credat,
                           cretim,
                           tanum,
                           bwlvs,
                           trart,
                           bname,
                           tapos,
                           matnr,
                           charg,
                           bestq,
                           sobkz,
                           lsonr,
                           meins,
                           wdatu,
                           wenum,
                           vltyp,
                           vsolm,
                           nltyp,
                           maktx,
                           vfdat,
                           lgort,
                           io,
                           pksz,
                           remark,
                           ordi_seq,
                           ordi_check,
                           car_no,
                           car_step,
                           car_sno,
                           print_step,
                           remark,
                           bigo
                           from tawmto where docnum is not null and io = '$' "; 
        #endregion
        private void FrmTawmtoLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv1, dv2;
        public FrmTawmtoLoad()
        {
            InitializeComponent();

            FormClosed += FrmTawmtoLoad_FormClosed; ;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = true;
            dv2.RowPostPaint += Common.RowPostPaint;

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
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
            modstr = modstr + " and step not in ( '0', '' ) and flag = '1' ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<LoadedTcar>(modstr).ToList();
                dv1.DataSource = q;
            }
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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
            modstr = modstr + " and car_sno = " + seq.ToString();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv2.DataSource = QueryToDataTable.ToDataTable<tawmtoq>(db.ExecuteQuery<tawmtoq>(modstr).ToList());

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            return;
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
                                sql = @"update tawmto set car_no = {0} where car_no = {1} and bachadate = {2} and car_sno = {3}";
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
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();
            }
            if (ff) MessageBox.Show("상태가 변했읍니다");
            else MessageBox.Show("차량변경 성공되었읍니다...!");

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

            string sql = @"update tacar set step = '2' where bachadate = {0} and car_no = {1} and seq = {2} and step = '1' and flag = '1' ";
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
            if (ls_step != "2") return;

            if (MessageBox.Show("배달완료처리 하시겠읍니까?", "배달완료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                     

            string dstr = "";
            int rc = 0, st = 0;
            bool ff = false;
           
          
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();

                db.p_curgetdatetime14(ref dstr);

                string hdate = dstr.Substring(0, 8);
                string htime = dstr.Substring(8, 6);

                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        // 차량이력 insert-> 이력일자 Update -> 기존것 update clear
                        rc = db.ExecuteCommand(@"insert into hacar 
                                                 select * from tacar 
                                                 where bachadate = {0} and car_no = {1} and seq = {2} and step = {3} and flag = '1' ",
                                                 bachadate, car_no, ll_seq, ls_step);
                        if (rc == 0)
                        {
                            st = 1;
                            ff = true;
                            db.Transaction.Rollback();
                        }
                        else
                        {
                            db.ExecuteCommand(@"update hacar set hdate = {0}, htime = {1} where bachadate = {2} and car_no = {3} and seq = {4} ",
                                                hdate, htime, bachadate, car_no, ll_seq);

                            db.ExecuteCommand(@"update tacar set step ='0', bachadate = '', seq = 0, load_qty = 0, load_vol = 0, car_dest = '',	 remark = '', flag = ''
                                                where bachadate = {0} and car_no = {1} and seq = {2} and step = {3}",
                                                bachadate, car_no, ll_seq, ls_step);


                            // 상차이력  insert-> 이력일자 Update -> 기존것 삭제
                            rc = db.ExecuteCommand(@"insert into hawmto  
                                                     select * from tawmto 
                                                     where bachadate = {0} and car_no = {1} and car_sno = {2} ",bachadate, car_no, ll_seq);
                            if (rc == 0)
                            {
                                st = 2;
                                ff = true;
                                db.Transaction.Rollback();
                            }
                            else
                            {
                                rc = db.ExecuteCommand(@"update hawmto set hdate = {0}, htime = {1} where bachadate = {2} and car_no = {3} and car_sno = {4} ",
                                                         hdate, htime, bachadate, car_no, ll_seq);

                                db.ExecuteCommand(@"delete from tawmto where bachadate = {0} and car_no = {1} and car_sno = {2} ",
                                                         bachadate, car_no, ll_seq);
                                db.Transaction.Commit();
                            }
                        }
                        if (!ff)
                        {                          
                            MessageBox.Show("배달완료 OK...!");
                        }
                    }
                    catch(Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                } // end of transaction
                db.Connection.Close();
            } // end of using db

            if (ff)
            {
                Cursor = Cursors.Default;
                if (st == 1) MessageBox.Show("차량이력 기록실패입입니다.");
                if (st == 2) MessageBox.Show("상차이력 기록실패입입니다.");
            }

            retrieve();
        }

        private void btncncl_Click(object sender, EventArgs e)
        {

            if (dv1.SelectedRows.Count <= 0) return;
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
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    rc = db.s_loadcncl_all_etc(bachadate, car_no, seq, load_qty);
                    if (rc == 1)
                    {
                        db.Transaction.Commit();
                    }
                    else
                    {
                        db.Transaction.Rollback();
                    }
                }
            }
            if (rc != 1)
                MessageBox.Show("상태가 변했읍니다." + rc.ToString());
            else
                MessageBox.Show("해당차량 상차취소 되었읍니다...!");

        }

        private void loadcncl_each()
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count <= 0) return;

            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            string step = dv1.SelectedRows[0].Cells["step"].Value.ToString();
            decimal load_qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["load_qty"].Value.ToString());

            if (step != "1") return;

            
            if (MessageBox.Show("선택하신제품들을 상차 개별취소처리 하시겠읍니까?", "상차개별취소",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0;
            bool ff = false;
            int st = 0;
            int lp = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int lc = db.ExecuteQuery<int>(@"select count(*) from tacar where bachadate = {0} and car_no = {1} and seq = {2} and load_qty = {3} and step in ('1', '2') and flag = '1' ",
                                                bachadate, car_no, seq, load_qty).SingleOrDefault();
                if (lc == 0)
                {
                    MessageBox.Show("상태가 변했읍니다...!");
                    return;
                }
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow r in rr)
                        {
                            string docnum = r.Cells["docnum"].Value.ToString();
                            decimal tanum = Convert.ToDecimal(r.Cells["tanum"].Value.ToString());
                            int tapos = Convert.ToInt32(r.Cells["tapos"].Value.ToString());
                            int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                            int car_sno = Convert.ToInt32(r.Cells["car_sno"].Value.ToString());
                            decimal pksz = Convert.ToDecimal(r.Cells["pksz"].Value.ToString());

                            decimal ordi_oqty = Convert.ToDecimal(r.Cells["vsolm"].Value.ToString());
                            decimal ordi_ltqty = ordi_oqty * pksz;

                            rc = db.ExecuteCommand(@"update tawmto 
                                                     set car_no = '', bachadate = '', car_sno = 0, car_step = '0', print_step = '0' 
                                                     where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3} and bachadate = {4} and car_no = {5} and car_sno = {6} ",
                                                     docnum, tanum, tapos, ordi_seq, bachadate, car_no, car_sno);
                            if (rc == 0)
                            {
                                st = 1;
                                ff = true;
                                break;
                            }
                            else
                            {
                                rc = db.ExecuteCommand(@"update tacar set load_qty = load_qty - {0}, load_vol = load_vol - {1} where bachadate = {2} and car_no = {3} and seq = {4} ",
                                                         ordi_oqty, ordi_ltqty, bachadate, car_no, car_sno);
                                if (rc == 0)
                                {
                                    st = 2;
                                    ff = true;
                                    break;
                                }
                            }
                            lp++;
                        } // end of foreach

                        if (!ff)
                        {
                            db.ExecuteCommand(@"update tacar 
                                              set load_qty = 0, load_vol = 0, bachadate = '', seq = 0, remark = '', duedate = '', step = '0', flag = ''	
                                            where car_no = {0} 
                                              and 0 < (select count(*) from tacar where car_no = {0} and ( load_qty <= 0 or load_vol <= 0 ) and flag = '1' ) ", car_no);

                            db.Transaction.Commit();
                        }
                        else
                        {
                            db.Transaction.Rollback();
                        }
                        if (ff) MessageBox.Show("상태가 변했읍니다...!");
                        else MessageBox.Show(lp.ToString() + " 개 행이 취소되었읍니다..!");
                    }            
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }             
                }// end of trans
            } // end of db

            retrieve();
        }

        private void btncmmt_Click(object sender, EventArgs e)
        {
            if (dv2.SelectedRows.Count <= 0) return;

            string docnum = dv2.SelectedRows[0].Cells["docnum"].Value.ToString();
            string remark = dv2.SelectedRows[0].Cells["remark2"].Value.ToString();
                    
            string nrmrk = "";

            using (FrmEtcLoadComment_p p = new FrmEtcLoadComment_p(docnum, remark))
            {
                if (p.ShowDialog() == DialogResult.Cancel) return;
                nrmrk = p.textBox1.Text;
            }
            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.ExecuteCommand(@"update tawmto set remark ={0} where docnum = {1} and car_no <> '' ", nrmrk, docnum);
            }
            if (rc > 0) MessageBox.Show(rc.ToString() + " 개의 레코드가 반영되었읍니다");
            else MessageBox.Show(" 상태가 변했읍니다");

            retrieve();
        }

        private void btnchgqty_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count > 1)
            {
                MessageBox.Show("레코드 한줄만 선택하세요...!");
                return;
            }
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int sqty = 0;
            int qty = (int)Convert.ToDecimal(dv2.SelectedRows[0].Cells["vsolm"].Value.ToString());

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
            decimal tanum = Convert.ToDecimal(dv2.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv2.SelectedRows[0].Cells["tapos"].Value.ToString());
            int ordi_seq = Convert.ToInt32(dv2.SelectedRows[0].Cells["ordi_seq"].Value.ToString());
            decimal pksz = Convert.ToDecimal(dv2.SelectedRows[0].Cells["pksz"].Value.ToString());
         

            int cncl_qty = qty - sqty;
            decimal cncl_ltqty = (qty - sqty) * pksz;
            int ret = 0, rc = 0;
            bool ff = false;
            int st = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.ExecuteCommand(@"update tacar 
                                                set load_vol = load_vol - {0}, 
                                                    load_qty = load_qty - {1} 
                                              where car_no = {2} and step in ( '1', '2' )", cncl_ltqty, cncl_qty, car_no);
                        if (rc != 0)
                        {
                            ret = db.ExecuteQuery<int>(@"select isnull(max(ordi_seq),0) + 1 
                                                     from tawmto 
                                                     where docnum = {0} and tanum = {1} and tapos = {2} ", docnum, tanum, tapos).SingleOrDefault();

                            rc = db.ExecuteCommand(@"  
                           INSERT INTO tawmto
                                    (docnum,     credat,       cretim,     lgnum,      tanum,        bwlvs,       trart,      bname,       tapos,      matnr,      plant,
                                     charg,      bestq,        sobkz,      lsonr,      meins,        wdatu,       wenum,      vltyp,       vsolm,      nltyp,      maktx,
                                     vfdat,      lgort,        flag,       hdate,      htime,        pksz,        arrival,    car_no,       io,         rqty,       fqty,
                                     car_step,   car_sno,      ordi_seq,   ordi_size,  print_step,   ordi_check,  remark,     bachadate,   recv_dt )
                            select   docnum,     credat,       cretim,     lgnum,      tanum,        bwlvs,       trart,      bname,       tapos,      matnr,      plant,
                                     charg,      bestq,        sobkz,      lsonr,      meins,        wdatu,       wenum,      vltyp,       {0},        nltyp,      maktx,
                                     vfdat,      lgort,        flag,        hdate,     htime,        pksz,        arrival,    '',           io,        rqty,       fqty,
                                     '0',        0,             {1},        pksz,      '0',          ordi_check,  remark,     '',          recv_dt
                           from  tawmto where docnum = {2} and tanum = {3} and tapos = {4} and ordi_seq = {5} ", cncl_qty, ret, docnum, tanum, tapos, ordi_seq);
                            if (rc != 0)  // ok
                            {
                                rc = db.ExecuteCommand(@"update tawmto set vsolm = {0} 
                                                         where docnum = {1} and tanum = {2} and tapos = {3} and ordi_seq = {4} ", sqty, docnum, tanum, tapos, ordi_seq);
                                if (rc == 0) { db.Transaction.Rollback(); ff = true; st = 3; }
                                else
                                {
                                    db.Transaction.Commit();
                                }
                            }
                            else { db.Transaction.Rollback(); ff = true; st = 2; }
                        }
                        else { db.Transaction.Rollback(); ff = true; st = 1; }

                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
              
                } // end of transactionscope

                if (ff) MessageBox.Show("상태가 변했읍니다...!" + st.ToString());
                else MessageBox.Show("수량 조정 성공입니다...!");
            }

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

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {               
                db.ExecuteCommand(@"update tawmto set print_step = '1' where bachadate = {0} and car_no = {1} and car_sno = {2} ", bachadate, car_no, seq);
            }

            using (FrmPrintToExcel3 p = new FrmPrintToExcel3(bachadate, car_no, seq, false))
            {
                p.ShowDialog();
            }
        }

        private void FrmTawmtoLoad_Load(object sender, EventArgs e)
        {
            retrieve();
        }
    }
}
