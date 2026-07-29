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
    public partial class FrmEtcWmto : Form
    {
        #region --- MDI Child ----------------
        private static FrmEtcWmto _instance;
        public static FrmEtcWmto Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmEtcWmto();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmEtcWmto_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region Select 
        string sqltab1 = @"Select  
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
           ordi_check  
           from tawmto where docnum is not null and io = '$' ";

        string sqltab2 = @"Select  
           docnum,
           tanum,   
           sum(vsolm) as vsolm, 
           sum(vsolm * isnull(pksz, 0)) as ltqty,   
           sum(iif(pksz >= 7.5, vsolm, 0)) as vsolm75, 
           sum(vsolm * iif(pksz >= 7.5, isnull(pksz, 0), 0)) as ltqty75,   
           max(remark) as remark
           from tawmto 
           where docnum is not null and io = '$'  ";

        #endregion

        DataGridView dv1, dv2;
        public FrmEtcWmto()
        {
            InitializeComponent();
            FormClosed += FrmEtcWmto_FormClosed;

            dtDatefrom.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtDateTo.Text = DateTime.Today.ToString("yyyy-MM-dd");

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowPostPaint += Common.RowPostPaint;

            dv1 = dataGridView1;

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = true;
            dataGridView2.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;

        }
        private void querycombobox()
        {
            comboBox1.SuspendLayout();
            comboBox1.Items.Clear();
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<string>(@"select ordi_check from tawmto where car_no = '' group by ordi_check");
                foreach (string s in q)
                {
                    comboBox1.Items.Add(s);
                }
            }
            comboBox1.ResumeLayout();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

        }
        private void FrmEtcWmto_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            retrieve();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (tab1.SelectedIndex == 0) retrieve();
            if (tab1.SelectedIndex == 1) retrieve2();
        }
        private string querywhere()
        {
            string modstr = "";

            string date1 = dtDatefrom.Text.Replace("-", "");
            string date2 = dtDateTo.Text.Replace("-", "");
            if (!chkdt.Checked)
                modstr = modstr + " and credat >= '" + date1 + "'";
            else
            {
                modstr = modstr + " and credat >= '" + date1 + "'";
                modstr = modstr + " and credat <= '" + date2 + "'";
            }
            modstr = modstr + " and car_no = '' ";

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '%" + tbdoc.Text.Trim() + "%'";
            if (tbord.Text.Trim() != "") modstr = modstr + " and tanum = " + tbord.Text.Trim();
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '%" + tbprod.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and maktx like '%" + txtpdesc.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '%" + tbbatch.Text.Trim() + "%'";
          
            if (comboBox1.SelectedIndex >= 0)
            {
                string ls_check = comboBox1.SelectedItem.ToString();
                modstr = modstr + " and ordi_check = '" + ls_check + "' ";
            }

            return modstr;
        }
        private void retrieve()
        {
            string modstr = sqltab1;

            modstr = modstr + querywhere();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {              
                dataGridView1.DataSource = QueryToDataTable.ToDataTable<tawmtoq>(db.ExecuteQuery<tawmtoq>(modstr).ToList());

                dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void retrieve(string str)
        {
            string modstr = sqltab1;

            modstr = modstr + querywhere();
            modstr = modstr + " and docnum in (" + str + ")";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dataGridView1.DataSource = QueryToDataTable.ToDataTable<tawmtoq>(db.ExecuteQuery<tawmtoq>(modstr).ToList());

                dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void retrieve2()
        {
            string modstr = sqltab2;
            modstr = modstr + querywhere();
            modstr = modstr + " group by Docnum, tanum ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dataGridView2.DataSource = QueryToDataTable.ToDataTable<tawmtogrp>(db.ExecuteQuery<tawmtogrp>(modstr).ToList());

                dataGridView2.TopLeftHeaderCell.Value = dataGridView2.RowCount.ToString();
                dataGridView2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        private void tbdoc_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbdoc.Text = dataGridView1.SelectedRows[0].Cells["docnum1"].Value.ToString();
        }

        private void tbord_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dataGridView1.SelectedRows.Count <= 0) return;
                tbord.Text = dataGridView1.SelectedRows[0].Cells["tanum1"].Value.ToString();
            }
            //if (tab1.SelectedIndex == 1)
            //{
            //    if (dataGridView1.SelectedRows.Count <= 0) return;
            //    tbord.Text = dataGridView1.SelectedRows[0].Cells["ordi_no"].Value.ToString();
            //}
        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbprod.Text = dataGridView1.SelectedRows[0].Cells["matnr1"].Value.ToString();
        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbbatch.Text = dataGridView1.SelectedRows[0].Cells["charg1"].Value.ToString();
        }
        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dataGridView1.SelectedRows[0].Cells["maktx1"].Value.ToString();
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Arrow;

            decimal qty = 0;
            decimal tqty = 0;
            decimal ltqty = 0;
            decimal pksz = 0;
            int cc = 0;
            if (dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    qty = Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());
                    pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                    if (chk75.Checked)
                    {
                        if (pksz >= 7.5m)
                        {
                            tqty = tqty + qty;
                            ltqty = ltqty + qty * pksz;
                        }
                    }
                    else
                    {
                        tqty = tqty + qty;
                        ltqty = ltqty + qty * pksz;
                    }
                    cc++;
                }

                lblqty.Text = tqty.ToString("#,###,##0");
                lblltqty.Text = ltqty.ToString("#,###,##0.000");

            }
            else
            {
                lblqty.Text = "0";
                lblltqty.Text = "0.000";
            }
        }
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Arrow;

            decimal qty = 0;
            decimal tqty = 0;
            decimal ltqty = 0;
            decimal tltqty = 0;

            if (dataGridView2.RowCount > 0)
            {
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (chk75.Checked)
                    {
                        qty = Convert.ToDecimal(r.Cells["vsolm75"].Value.ToString());
                        ltqty = Convert.ToDecimal(r.Cells["ltqty75"].Value.ToString());

                        tqty = tqty + qty;
                        tltqty = tltqty + ltqty;
                    }
                    else
                    {
                        qty = Convert.ToDecimal(r.Cells["vsolm2"].Value.ToString());
                        ltqty = Convert.ToDecimal(r.Cells["ltqty2"].Value.ToString());
                        tqty = tqty + qty;
                        tltqty = tltqty + ltqty;
                    }
                        
                }

                lblqty2.Text = tqty.ToString("#,###,##0");
                lblltqty2.Text = tltqty.ToString("#,###,##0.000");
            }
            else
            {
                lblqty2.Text = "0";
                lblltqty2.Text = "0.000";
            }
        }
        private void btndel_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex != 0) return;

            DataGridView dv1 = dataGridView1;

            if (dv1.SelectedRows.Count <= 0) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum1"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum1"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos1"].Value.ToString());
            string lgort = dv1.SelectedRows[0].Cells["lgort1"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg1"].Value.ToString();
            decimal qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm1"].Value.ToString());

            if (MessageBox.Show("삭제하시겠읍니까?", "확인",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.ExecuteCommand(@"delete from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and car_no = '' ", docnum, tanum, tapos);
                if (rc > 0)
                    db.ExecuteCommand(@"delete from hawmto where docnum = {0} and tanum = {1} and tapos = {2} ", docnum, tanum, tapos);
            }
            if (rc > 0) MessageBox.Show("삭제 OK!");
            else MessageBox.Show("삭제 실패!");

            retrieve();

        }

        private void btncu_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") return;
            if (tab1.SelectedIndex == 0) loadcar_tab1();
            if (tab1.SelectedIndex == 1) loadcar_tab2();
        }
        private void loadcar_tab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;

            string car_no = "";

            using (FrmEtcSelLoadCar_p p1 = new FrmEtcSelLoadCar_p())
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString(); ;
            }
            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            string sql = @"select Top 1 bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(sql);
                if (q == null || q.Count() == 0)
                {
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }

                foreach (var s in q)
                {
                    bachadate = s.bachadate;
                    seq = s.seq;
                    max_vol = s.max_vol;
                    sum_ltqty = s.load_vol;
                    sum_oqty = s.load_qty;
                    break;
                }
            }

            decimal save_ltqty = sum_ltqty;
            decimal save_oqty = sum_oqty;

            int need_qty = 0, jan_qty = 0;
            decimal jan_ltqty = 0;

            Cursor = Cursors.WaitCursor;
            bool finish = false, ff = false;
            int rc = 0, ret = 0, cnt = 0, rand_Seq = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (DataGridViewRow r in rr)
                        {
                            if (sum_ltqty >= max_vol) break;

                            string docnum = r.Cells["docnum1"].Value.ToString();
                            decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                            decimal tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                            decimal pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                            if (pksz == 0) break;
                            int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());

                            int ordi_oqty = (int)Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());

                            decimal ordi_ltqty = ordi_oqty * pksz;

                            if (sum_ltqty + ordi_ltqty > max_vol)
                            {
                                need_qty = (int)((max_vol - sum_ltqty) / pksz);
                                if (need_qty == 0)
                                {
                                    finish = true;
                                    break;
                                }
                                else
                                {
                                    jan_qty = ordi_oqty - need_qty;
                                    if (jan_qty == 0) finish = true;

                                    if (!finish)
                                    {
                                        jan_ltqty = pksz * jan_qty;
                                        rc = db.ExecuteQuery<int>(
                                            @"select isnull(max(ordi_seq),0) + 1 from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3}", docnum, tanum, tapos, ordi_seq).SingleOrDefault();

                                        ret = db.ExecuteCommand(sqltawmto_insert, jan_qty, rc, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }

                                        ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }
                                        cnt++;
                                    }
                                }
                                if (finish)
                                {
                                    ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, tanum, tapos, ordi_seq);
                                    if (ret == 0) { ff = true; break; }
                                }
                                sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                                sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                                break;

                            }
                            else
                            {
                                ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, tanum, tapos, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                            }
                            cnt++;
                            sum_oqty = sum_oqty + ordi_oqty;
                            sum_ltqty = sum_ltqty + ordi_ltqty;

                            if (finish) break;
                        } // end of foreach
                        if (ff) // fault 여부 check
                        {
                            db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                            MessageBox.Show("조회후 다시 실행하세요...!");
                            return;
                        }
                        if (sum_oqty <= 0)  // 예약 여부 check
                        {
                            db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                            MessageBox.Show("차량 예약이 실패했읍니다...!");
                            return;
                        }

                        string lstep = "1";
                        if (finish) lstep = "2";

                        ret = db.ExecuteCommand(@"update tacar set load_vol = {0}, load_qty = {1}, step = {2} 
                                                  where car_no = {3} and step = '1' and load_vol = {4} and load_qty = {5}",
                                                                    sum_ltqty, sum_oqty, lstep,
                                                                    car_no, save_ltqty, save_oqty);
                        if (ret == 0)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("tacar 상태가 변했읍니다..!");
                            return;
                        }
                        db.Transaction.Commit();

                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();
            }

            retrieve();
        }

        private void loadcar_tab2()
        {
            if (dv2.SelectedRows.Count == 0) return;

            string lsr = wf_getorders();
            if (lsr == "") return;
            retrieve(lsr);

            if (dv1.Rows.Count == 0) return;
                    
            string car_no = "";
            using (FrmEtcSelLoadCar_p p1 = new FrmEtcSelLoadCar_p())
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }
            dv1.SelectAll();


            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(sql).SingleOrDefault();
                if (q == null)
                {
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                bachadate = q.bachadate;
                seq = q.seq;
                max_vol = q.max_vol;
                sum_ltqty = q.load_vol;
                sum_oqty = q.load_qty;

            }

            decimal save_ltqty = sum_ltqty;
            decimal save_oqty = sum_oqty;

            decimal need_qty = 0, jan_qty = 0;
            decimal jan_ltqty = 0;

            Cursor = Cursors.WaitCursor;
            bool finish = false, ff = false;
            int rc = 0, ret = 0, cnt = 0, rand_Seq = 0;
        
            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();

                using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (DataGridViewRow r in rr)
                        {
                            if (sum_ltqty >= max_vol) break;

                            string docnum = r.Cells["docnum1"].Value.ToString();
                            decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                            int tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                            decimal pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                            if (pksz == 0) break;

                            int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());
                            decimal ordi_oqty = Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());
                            decimal ordi_ltqty = ordi_oqty * pksz;

                            if (sum_ltqty + ordi_ltqty > max_vol)
                            {
                                need_qty = (int)((max_vol - sum_ltqty) / pksz);
                                if (need_qty == 0)
                                {
                                    finish = true;
                                    break;
                                }
                                else
                                {
                                    jan_qty = ordi_oqty - need_qty;
                                    if (jan_qty == 0) finish = true;

                                    if (!finish)
                                    {
                                        jan_ltqty = pksz * jan_qty;
                                        rc = db.ExecuteQuery<int>(
                                            @"select isnull(max(ordi_seq),0) + 1 from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3}", docnum, tanum, tapos, ordi_seq).SingleOrDefault();

                                        ret = db.ExecuteCommand(sqltawmto_insert, jan_qty, rc, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }

                                        ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }
                                        cnt++;
                                    }
                                }
                                if (finish)
                                {
                                    ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, tanum, tapos, ordi_seq);
                                    if (ret == 0) { ff = true; break; }
                                }
                                sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                                sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                                break;

                            }
                            else
                            {
                                ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, tanum, tapos, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                            }
                            cnt++;
                            sum_oqty = sum_oqty + ordi_oqty;
                            sum_ltqty = sum_ltqty + ordi_ltqty;

                            if (finish) break;
                        } // end of foreach

                        if (ff) // fault 여부 check
                        {
                            db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                            MessageBox.Show("조회후 다시 실행하세요...!");
                            return;
                        }
                        if (sum_oqty <= 0)  // 예약 여부 check
                        {
                            db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                            MessageBox.Show("차량 예약이 실패했읍니다...!");
                            return;
                        }


                        string lstep = "1";
                        if (finish) lstep = "2";

                        ret = db.ExecuteCommand(@"update tacar set load_vol = {0}, load_qty = {1}, step = {2} 
                                              where car_no = {3} and step = '1' and load_vol = {4} and load_qty = {5}",
                                                                    sum_ltqty, sum_oqty, lstep,
                                                                    car_no, save_ltqty, save_oqty);
                        if (ret == 0)
                        {
                            db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                            MessageBox.Show("tacar 상태가 변했읍니다..!");
                            return;
                        }
                        db.Transaction.Commit();

                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();
            }            
          
            retrieve2();
        }
        private void btnsumqty_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex != 0) return;

            decimal sum = 0;
            decimal pksz = 0;
            decimal qty = 0;

            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                qty = Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());
                pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                if (chk75.Checked)
                {
                    if (pksz >= 7.5m) sum = sum + pksz * qty;
                }
                else sum = sum + pksz * qty;
            }
            tbsumqty.Text = sum.ToString("#,###,##0.000");
        }

        private void btncmmt_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dataGridView1.SelectedRows.Count <= 0) return;

                string docnum = dataGridView1.SelectedRows[0].Cells["docnum1"].Value.ToString();
                string comment = dataGridView1.SelectedRows[0].Cells["remark1"].Value.ToString();

                using (FrmEtcLoadComment_p p = new FrmEtcLoadComment_p(docnum, comment))
                {
                    p.ShowDialog();
                    if (p.DialogResult != DialogResult.OK) return;
                    comment = p.textBox1.Text;
                }

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    int rc = db.ExecuteCommand(@"update tawmto set remark = {0} where docnum = {1} and io = '$' ", comment, docnum);
                }

                retrieve();
            }

            if (tab1.SelectedIndex == 1)
            {
                if (dataGridView2.SelectedRows.Count <= 0) return;

                string docnum = dataGridView2.SelectedRows[0].Cells["docnum2"].Value.ToString();
                string comment = dataGridView2.SelectedRows[0].Cells["remark2"].Value.ToString();

                using (FrmEtcLoadComment_p p = new FrmEtcLoadComment_p(docnum, comment))
                {
                    p.ShowDialog();
                    if (p.DialogResult != DialogResult.OK) return;
                    comment = p.textBox1.Text;
                }

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    int rc = db.ExecuteCommand(@"update tawmto set remark = {0} where docnum = {1} and io = '$' ", comment, docnum);
                }

                retrieve2();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            querycombobox();
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) tab1check();
            //if (tab1.SelectedIndex == 1) tab2check();
        }
        private void btncheckdel_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) checktab1del();
            if (tab1.SelectedIndex == 1) checktab2del();
        }
        private void tab1check()
        {
            if (dv1.SelectedRows.Count <= 0) return;

            string ordi_check = "";
            using (FrmCheck_p p = new FrmCheck_p())
            {
                if (p.ShowDialog() == DialogResult.Cancel) return;
                ordi_check = p.textBox1.Text;
            }

            int rc = 0;

            bool ff = false;


            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }
            Cursor = Cursors.WaitCursor;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                foreach (DataGridViewRow r in rr)
                {
                    string docnum = r.Cells["docnum1"].Value.ToString();
                    decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                    int tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());

                    string sql = @"update tawmto set ordi_check = {0} where docnum = {1} and tanum = {2} and tapos = {3} and ordi_seq = {4} and car_no = '' ";
                    rc = db.ExecuteCommand(sql, ordi_check, docnum, tanum, tapos, ordi_seq);
                    if (rc == 0) { ff = true; break; }
                }
            }
            Cursor = Cursors.Default;

            if (ff) MessageBox.Show("상태가 변했읍니다...!");

            retrieve();
        }
        private void checktab1del()
        {
            if (comboBox1.Items.Count <= 0) return;
            if (dv1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("삭제할 행을 선택하세요?");
                return;
            }

            string ordi_check = dv1.SelectedRows[0].Cells["ordi_check1"].Value.ToString();
            if (ordi_check == "") return;

            if (MessageBox.Show("check = " + ordi_check + " 삭제하시겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0, lp = 0;
            bool ff = false;

            Cursor = Cursors.WaitCursor;
            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                foreach (DataGridViewRow r in rr)
                {
                    string docnum = r.Cells["docnum1"].Value.ToString();
                    decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                    int tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());

                    rc = db.ExecuteCommand(@"delete from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3} and car_no = '' and ordi_check = {4} ",
                                            docnum, tanum, tapos, ordi_seq, ordi_check);
                    if (rc == 0) { ff = true; break; }
                    lp++;
                }
            }
            Cursor = Cursors.Default;
            if (ff) { MessageBox.Show("상태가변했읍니다."); }
            MessageBox.Show(lp.ToString() + " 개의 행이 삭제되었읍니다");

            retrieve();
        }
        private void checktab2del()
        {
            if (tab1.SelectedIndex != 1) return;
            if (comboBox1.Items.Count == 0) return;
            if (dv2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("삭제할 오더행을 선택하세요?");
                return;
            }

            string ordi_check = comboBox1.SelectedItem.ToString().Trim();
            if (ordi_check == "") return;

            if (MessageBox.Show("check = " + ordi_check + " 삭제하시겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0, lp = 0;
            bool ff = false;
            Cursor = Cursors.WaitCursor;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                foreach (DataGridViewRow r in rr)
                {
                    decimal tanum = Convert.ToDecimal(r.Cells["tanum2"].Value.ToString());

                    rc = db.ExecuteCommand(@"delete from tawmto where tanum = {0} and car_no in ( '', 0' ) and ordi_check = {1} ", tanum, ordi_check);
                    if (rc == 0) { ff = true; break; }
                    lp++;
                }
            }
                        
            if (ff) { MessageBox.Show("상태가변했읍니다."); }
            MessageBox.Show(lp.ToString() + " 개의 행이 삭제되었읍니다");

            querycombobox();
            retrieve();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
                if (comboBox1.SelectedItem.ToString() != "") return;

            if (tab1.SelectedIndex == 0) NewCarTab1();
            if (tab1.SelectedIndex == 1) NewCarTab3();
        }
        private void NewCarTab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;

            string ls_opt = "1"; //  전부선택
            using (FrmAllorSelect_p p = new FrmAllorSelect_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                if (p.radioButton1.Checked) ls_opt = "1";
                else ls_opt = "0";

                if (ls_opt == "1") dv1.SelectAll();
            }
            string car_no = "";
            using (FrmEtcNewCarSel_p p1 = new FrmEtcNewCarSel_p())
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }

            decimal? max_vol = 0;
            decimal rand_Seq = 0;
            string bachadate = "";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                max_vol = db.ExecuteQuery<decimal>(@"select max_vol from tacar where car_no = {0} and step in ( '0', '' ) ", car_no).SingleOrDefault();
                if (max_vol == null || max_vol == 0)
                {
                    MessageBox.Show("상태가 변했읍니다..!");
                    return;
                }
                rand_Seq = db.p_getrand();
                db.p_curgetdatetime10(ref bachadate);
            }
          
            Cursor = Cursors.WaitCursor;
            decimal sum_oqty = 0;
            int need_qty = 0, jan_qty = 0, rc = 0, ret = 0, cnt = 0;
            decimal sum_ltqty = 0, jan_ltqty = 0;
            bool finish = false, ff = false;
            int st = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    #region Foreach
                    foreach (DataGridViewRow r in rr)
                    {
                        if (sum_ltqty >= max_vol) break;
                        string docnum = r.Cells["docnum1"].Value.ToString();
                        decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                        int tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                        decimal pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                        if (pksz == 0m) break;

                        int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());
                        int ordi_oqty = (int)Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());
                        decimal ordi_ltqty = pksz * ordi_oqty;

                        if (sum_ltqty + ordi_ltqty > max_vol)
                        {
                            need_qty = (int)((max_vol - sum_ltqty) / pksz);
                            if (need_qty == 0)
                            {
                                finish = true;
                                break;
                            }
                            else
                            {
                                jan_qty = ordi_oqty - need_qty;
                                if (jan_qty == 0) finish = true;

                                if (!finish)
                                {
                                    jan_ltqty = pksz * jan_qty;
                                    rc = db.ExecuteQuery<int>(
                                        @"select isnull(max(ordi_seq),0) + 1 from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3} ", docnum, tanum, tapos, ordi_seq).SingleOrDefault();

                                    ret = db.ExecuteCommand(sqltawmto_insert, jan_qty, rc, docnum, tanum, tapos, ordi_seq);
                                    if (ret == 0) { st = 1; ff = true; break; }


                                    ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                    if (ret == 0) { st = 2; ff = true; break; }
                                    cnt++;
                                }
                            }
                            if (finish)
                            {
                                ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                if (ret == 0) { st = 3; ff = true; break; }
                            }
                            sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                            sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                            break;

                        }
                        else
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, tanum, tapos, ordi_seq);
                            if (ret == 0) { st = 4; ff = true; break; }
                        }
                        cnt++;
                        sum_oqty = sum_oqty + ordi_oqty;
                        sum_ltqty = sum_ltqty + ordi_ltqty;

                        if (finish) break;

                    } // end of foreach
                    #endregion

                    if (ff) // fault 여부 check
                    {
                        db.Transaction.Rollback(); 
                        MessageBox.Show("조회후 다시 실행하세요...!");
                        return;
                    }
                    if (sum_oqty <= 0 || cnt == 0)  // 예약 여부 check
                    {
                        db.Transaction.Rollback(); 
                        MessageBox.Show("차량 예약이 실패했읍니다...!" + st.ToString());
                        return;
                    }

                    int seq = db.s_getbachasno(bachadate);  // 배차순번 얻음
                    if (seq <= 0)
                    {
                        db.Transaction.Rollback(); 
                        MessageBox.Show("f_getbachasno return error...!");
                        return;
                    }
                    //  배차순번 update
                    ret = db.ExecuteCommand(@"update tawmto set car_sno = {0} where car_no = {1} and bachaDate = {2} and car_sno = {3} ", seq, car_no, bachadate, rand_Seq);
                    if (ret == 0)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show("tawmto update Error..!");
                        return;
                    }
                    string lstep = "1";
                    if (finish) lstep = "2";

                    ret = db.ExecuteCommand(@"update tacar set bachadate = {0}, seq = {1}, step = {2}, load_vol = {3}, load_qty = {4}, flag = '1'
                                              where car_no = {5} and step = '0' ",
                                              bachadate, seq, lstep, sum_ltqty, sum_oqty, car_no);
                    if (ret == 0)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show("tacar update Error..!");
                        return;
                    }
                    db.Transaction.Commit();
                } // end of transact
            } // end of using db            
                    
            retrieve();
        }
        private void NewCarTab3()
        {
            string lsr = wf_getorders();
            if (lsr == "") return;
          
            retrieve(lsr);
            if (dv1.Rows.Count == 0) return;


            string car_no = "";
            using (FrmNewCarSel_p p1 = new FrmNewCarSel_p())
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }
            dv1.SelectAll();

           
            string bachadate = "";
            decimal max_vol = 0;
            int rand_Seq = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                max_vol = db.ExecuteQuery<decimal>(@"select max_vol from tacar where car_no = {0} and step in ('0', '') ", car_no).SingleOrDefault();
                if (max_vol == 0)
                {
                    MessageBox.Show("상태가 변했읍니다1..!");
                    return;
                }

                rand_Seq = db.ExecuteQuery<int>(@"select cast(floor(rand() * 2000 + 1000) as int) from tbstat").SingleOrDefault();
                db.p_curgetdatetime10(ref bachadate);
            }

            Cursor = Cursors.WaitCursor;
            decimal sum_oqty = 0, need_qty = 0, jan_qty = 0;
            int rc = 0, ret = 0, cnt = 0;
            decimal sum_ltqty = 0, jan_ltqty = 0;
            bool finish = false, ff = false;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (DataGridViewRow r in rr)
                        {
                            if (sum_ltqty >= max_vol) break;

                            string docnum = r.Cells["docnum1"].Value.ToString();
                            decimal tanum = Convert.ToDecimal(r.Cells["tanum1"].Value.ToString());
                            int tapos = Convert.ToInt32(r.Cells["tapos1"].Value.ToString());
                            decimal pksz = Convert.ToDecimal(r.Cells["pksz1"].Value.ToString());
                            if (pksz == 0m) break;

                            int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq1"].Value.ToString());
                            decimal ordi_oqty = Convert.ToDecimal(r.Cells["vsolm1"].Value.ToString());
                            decimal ordi_ltqty = ordi_oqty * pksz;

                            if (sum_ltqty + ordi_ltqty > max_vol)
                            {
                                need_qty = (max_vol - sum_ltqty) / pksz;
                                if (need_qty == 0)
                                {
                                    finish = true;
                                    break;
                                }
                                else
                                {
                                    jan_qty = ordi_oqty - need_qty;
                                    if (jan_qty == 0) finish = true;

                                    if (!finish)
                                    {

                                        jan_ltqty = pksz * jan_qty;
                                        rc = db.ExecuteQuery<int>(
                                            @"select isnull(max(ordi_seq),0) + 1 from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3}", docnum, tanum, tapos, ordi_seq).SingleOrDefault();

                                        ret = db.ExecuteCommand(sqltawmto_insert, jan_qty, rc, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }
                                        ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                        if (ret == 0) { ff = true; break; }
                                        cnt++;
                                    }
                                }
                                if (finish)
                                {
                                    ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                    if (ret == 0) { ff = true; break; }
                                }
                                sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                                sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                                break;

                            }
                            else
                            {
                                ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, tanum, tapos, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                            }
                            cnt++;
                            sum_oqty = sum_oqty + ordi_oqty;
                            sum_ltqty = sum_ltqty + ordi_ltqty;

                            if (finish) break;
                        } // end of foreach

                        if (ff) // fault 여부 check
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("조회후 다시 실행하세요...!");
                            return;
                        }
                        if (sum_oqty <= 0 || cnt == 0)  // 예약 여부 check
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("차량 예약이 실패했읍니다...!");
                            return;
                        }

                        int seq = db.s_getbachasno(bachadate);  // 배차순번 얻음
                        if (seq <= 0)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("f_getbachasno return error...!");
                            return;
                        }
                        //  배차순번 update
                        db.ExecuteCommand(@"update tawmto set car_sno = {0} where car_no = {1} and bachaDate = {2} and car_sno = {3} ", seq, car_no, bachadate, rand_Seq);

                        string lstep = "1";
                        if (finish) lstep = "2";

                        ret = db.ExecuteCommand(@"update tacar set bachadate = {0}, seq = {1}, step = {2}, load_vol = {3}, load_qty = {4}, flag = '1' 
                                                  where car_no = {5} and step in ('0', '') ",
                                                  bachadate, seq, lstep, sum_ltqty, sum_oqty, car_no);
                        if (ret == 0)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("tacar 상태가 변했읍니다..!");
                            return;
                        }
                        db.Transaction.Commit();
                    }
                    catch(Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                        return;
                    }
                }
            }
            retrieve2();
        }
        private string wf_getorders()
        {
            string lsr = "";
            string pdocnum = "-1";
            string docnum = "";
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                docnum = r.Cells["docnum2"].Value.ToString();
                if (lsr == "")
                {
                    lsr = r.Cells["docnum2"].Value.ToString();
                }
                else
                {
                    if (pdocnum != docnum)
                        lsr = lsr + "," + r.Cells["docnum2"].Value.ToString();
                }
                pdocnum = docnum;
            }

            return lsr;
        }
        #region ----SQL insert / update Statement ---------------------
        string sqltawmto_insert = @"
               INSERT INTO tawmto
                        (docnum,     credat,       cretim,     lgnum,      tanum,        bwlvs,       trart,      bname,       tapos,      matnr,      plant,
                         charg,      bestq,        sobkz,      lsonr,      meins,        wdatu,       wenum,      vltyp,       vsolm,      nltyp,      maktx,
                         vfdat,      lgort,        flag,       hdate,      htime,        pksz,       arrival,     car_no,      io,         rqty,       fqty,
                         car_step,   car_sno,      ordi_seq,   ordi_size,  print_step,   ordi_check,  remark,     bachadate,   recv_dt )
                select   docnum,     credat,       cretim,     lgnum,      tanum,        bwlvs,       trart,      bname,       tapos,      matnr,      plant,
                         charg,      bestq,        sobkz,      lsonr,      meins,        wdatu,       wenum,      vltyp,       {0},        nltyp,      maktx,
                         vfdat,      lgort,        flag,        hdate,     htime,        pksz,        arrival,    car_no,       io,        rqty,       fqty, 
                         car_step,   car_sno,      {1},         ordi_size,  print_step,   ordi_check,  remark,     bachadate,   recv_dt
               from  tawmto where docnum = {2} and tanum = {3} and posnr = {4} and ordi_seq = {5} ";

        string sql_updt1 = @"update tawmto 
		                        set car_no = {0},  
                                bachadate = {1},  
                                vsolm = vsolm - {2},                                 
                                car_sno = {3}, 
                                car_step = '0', 
                                print_step = '0'
	  		                 where docnum = {4}
			                   and tanum = {5}
			                   and tapos = {6}
			                   and ordi_seq = {7}";

        string sql_updt2 = @"update tawmto 
		                        set car_no = {0},  
                                bachadate = {1},  
                                car_sno = {2}, 
                                car_step = '0', 
                                print_step = '0'
	  		                 where docnum = {3}
			                   and tanum = {4}
			                   and tapos = {5}
			                   and ordi_seq = {6}";

   
        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) retrieve();
            if (tab1.SelectedIndex == 1) retrieve2();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") return;
            if (tab1.SelectedIndex == 0) loadcar_add_tab1();
        }
        private void loadcar_add_tab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count == 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한줄만 선택하세요...!");
                return;
            }
            string docnum = dv1.SelectedRows[0].Cells["docnum1"].Value.ToString();

            string car_no = "";
            using (FrmEtcLoadCarAdd_p p1 = new FrmEtcLoadCarAdd_p())
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }

           
            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(sql).SingleOrDefault();
                if (q == null)
                {
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }

                bachadate = q.bachadate;
                seq = q.seq;
                max_vol = q.max_vol;
                sum_ltqty = q.load_vol;
                sum_oqty = q.load_qty;
            }

            decimal qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm1"].Value.ToString());
            decimal ll_qty = 0;
            using (FrmLoadCarGetQty_p p = new FrmLoadCarGetQty_p(qty))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                ll_qty = p.numericTextox2.Value;
            }

            docnum = dv1.SelectedRows[0].Cells["docnum1"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum1"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos1"].Value.ToString());
            decimal pksz = Convert.ToDecimal(dv1.SelectedRows[0].Cells["pksz1"].Value.ToString());
            int ordi_seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["ordi_seq1"].Value.ToString());
            decimal ordi_oqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm1"].Value.ToString());
            decimal ordi_ltqty = ordi_oqty * pksz;

            decimal jan_qty = 0, jan_ltqty = 0;
            string ls_step;
            int rc = 0, cnt = 0;
            bool ff = false;
            if (sum_ltqty + (ll_qty * pksz) >= max_vol) ls_step = "2";
            else ls_step = "1";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int ret = db.ExecuteCommand(@"update tacar set load_vol = load_vol + {0} * {1}, load_qty = load_qty + {2}, step = {3} 
                                               where car_no = {4} and step in ( '1', '2' )",
                                     ll_qty, pksz, ll_qty, ls_step, car_no);
                if (ret == 0)
                {
                    MessageBox.Show("상태가 변했읍니다...!");
                    return;
                }
                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        if (ordi_oqty > ll_qty)
                        {
                            jan_qty = ordi_oqty - ll_qty;
                            jan_ltqty = jan_qty * pksz;

                            rc = db.ExecuteQuery<int>(@"select isnull(max(ordi_seq),0) + 1 from tawmto where docnum = {0} and tanum = {1} and tapos = {2} and ordi_seq = {3}",
                                 docnum, tanum, tapos, ordi_seq).SingleOrDefault();

                            ret = db.ExecuteCommand(sqltawmto_insert, jan_qty, rc, docnum, tanum, tapos, ordi_seq);
                            if (ret == 0) { ff = true; }
                            {
                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, seq, docnum, tanum, tapos, ordi_seq);
                                if (ret == 0) { ff = true; }
                            }
                        }
                        else
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, tanum, tapos, ordi_seq);
                            if (ret == 0) { ff = true; }
                        }
                        if (ff)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show("상태가 변했읍니다2...");
                        }
                        else
                            db.Transaction.Commit();

                    }
                    catch (Exception E)
                    {
                        db.Transaction.Rollback();
                        MessageBox.Show(E.Message);
                    }
                }
                db.Connection.Close();               
            }
            retrieve();
        }

        #endregion

     
    }
    public class tawmtoq
    {
        public string docnum { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public decimal tanum { get; set; }
        public string bwlvs { get; set; }
        public string trart { get; set; }
        public string bname { get; set; }
        public int tapos { get; set; }
        public string matnr { get; set; }
        public string plant { get; set; }
        public string charg { get; set; }
        public string bestq { get; set; }
        public string sobkz { get; set; }
        public string lsonr { get; set; }
        public string wdatu { get; set; }
        public string wenum { get; set; }
        public string vltyp { get; set; }
        public decimal vsolm { get; set; }
        public string nltyp { get; set; }
        public string maktx { get; set; }
        public string vfdat { get; set; }
        public string lgort { get; set; }
        public string io { get; set; }
        public string flag { get; set; }
        public string remark { get; set; }
        public string ordi_check { get; set; }
        public decimal pksz { get; set; }
        public int ordi_seq { get; set; }
    }
    public class tawmtogrp
    {
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public decimal vsolm { get; set; }
        public decimal ltqty { get; set; }
        public decimal vsolm75 { get; set; }
        public decimal ltqty75 { get; set; }
        public string remark { get; set; }
    }
}
