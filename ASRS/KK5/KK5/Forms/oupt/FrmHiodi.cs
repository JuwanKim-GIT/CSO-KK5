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
    public partial class FrmHiodi : Form
    {
        #region --- MDI Child ----------------
        private static FrmHiodi _instance;
        public static FrmHiodi Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmHiodi();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmHiodi_FormClosed(object sender, FormClosedEventArgs e)
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
                                 htime,
                                 vsbed,
                                 ablad ,
                                  (substring(hdate,1,4) + '-' +  substring(hdate,5,2) + '-' +  substring(hdate,7,2) + ' ' +
                                  substring(htime,1,2) + ':' +  substring(htime,3,2) + ':' +  substring(htime,5,2) + ' ') as hist_dt   

                            FROM hiordi  
                           WHERE docnum is not null ";
       
        string sqls = @"select a.ordxkey, a.docnum, a.sdno, a.posnr, a.pltno, a.lstk, a.qty, a.credat, a.cretim, a.pksz, a.remark, 
                               b.matnr, b.matnrdesc, b.lgort, b.charg
                         from hiordx a join hiordi b on a.docnum = b.docnum and a.sdno = b.sdno and a.posnr = b.posnr
                         where a.docnum = {0} and a.sdno = {1} and a.posnr = {2} ";

        #endregion
        DataGridView dv1, dv2;

        public FrmHiodi()
        {
            InitializeComponent();
            FormClosed += FrmHiodi_FormClosed;

            dv1 = dataGridView1;

            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionChanged += Dv1_SelectionChanged1;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.ReadOnly = true;
            dv1.CellFormatting += Dv1_CellFormatting;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.AutoGenerateColumns = false;
            dv2.ReadOnly = true;
            dv2.MultiSelect = true;
            dv2.CellFormatting += Dv2_CellFormatting;
           // dv2.RowPostPaint += Common.RowPostPaint;

            if (Config.UserLevel != "1") button1.Enabled = false;
        }
     
        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void Dv1_SelectionChanged1(object sender, EventArgs e)
        {
            retrieve2();
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

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        private void FrmHiodi_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve2()
        {
            if (dv1.SelectedRows.Count == 0)
            {
                dv2.DataSource = null;
                return;
            }

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<hiodxq>(sqls, docnum, sdno, posnr).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void chkdt_CheckedChanged_1(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
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

        private void button1_Click(object sender, EventArgs e)
        {
            CancelErp();
        }
        private void CancelErp()
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("출고취소하여 바닥재고로 잡으시겠읍니까?", "ERP 출고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());

            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string matnrdesc = dv1.SelectedRows[0].Cells["matnrdesc"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = "";

            int rc = 0;
            int st = 0;
            int lp = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();

                foreach (DataGridViewRow r in dv2.SelectedRows)
                {
                    decimal ordxkey = Convert.ToDecimal(r.Cells["ordxkey_f"].Value.ToString());
                    decimal qty = Convert.ToDecimal(r.Cells["qty_f"].Value.ToString());
                    decimal pksz = Convert.ToDecimal(r.Cells["pksz_f"].Value.ToString());

                    string idate = DateTime.Now.ToString("yyyy/MM/dd");
                    string itime = DateTime.Now.ToString("hh:mm:ss");
                    string remark = r.Cells["remark_f"].Value.ToString();

                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            db.ExecuteCommand(@"delete from hiordx where ordxkey = {0} ", ordxkey);

                            db.ExecuteCommand(@"update hiordi set fqty = fqty - {0} where docnum = {1} and sdno = {2} and posnr = {3} ", qty, docnum, sdno, posnr);

                            string sqlupd = @"update miplti set plti_stok = plti_stok + {0} 
                                where plti_pltno = '00000000' and plti_lstk = 'Y000000' 
                                and plti_prod = {1} and plti_loc = {2} and plti_lot = {3} and plti_bestq = '' ";

                            db.p_tilock();

                            rc = db.ExecuteCommand(sqlupd, qty, matnr, lgort, charg);
                            if (rc == 0)
                            {
                                string sqlins = @"insert into miplti
                                        ( plti_pltno, plti_lstk,   plti_prod,  plti_loc,     plti_lot,       plti_bestq, 
                                        plti_pksz,  plti_stok,   plti_rqty,  plti_icust,   plti_cycl_date, plti_idate,
                                        plti_itime, plti_pdesc,  plti_oprod, plti_flag,    plti_label,     plti_remark )
                                values ( '00000000', 'Y000000',   {0},        {1},          {2},            {3},
                                        {4},        {5},         0,           '',          {6},            {7},
                                        {8},        {9},         '',          '1',         '0',            {10} ) ";

                                rc = db.ExecuteCommand(sqlins, matnr, lgort, charg, "", pksz, qty, idate, idate, itime, matnrdesc, remark);
                                if (rc <= 0)
                                { 
                                    db.Transaction.Rollback();
                                    break;                                   
                                }
                            }

                            db.ExecuteCommand(@"update miordi set rqty = rqty - {0}, fqty = fqty - {1} where docnum = {2} and sdno = {3} and posnr = {4} 
                                                and 0 < (select count(*) from miordi where docnum = {2} and sdno = {3} and posnr = {4}) ",
                                                    qty, qty, docnum, sdno, posnr);
                           
                            db.Transaction.Commit();

                            lp++;
                        }
                        catch (Exception E)
                        {
                            db.Transaction.Rollback();
                            MessageBox.Show(E.Message);
                        }
                    } // end trans
                } // foreach
                db.Connection.Close();
            } //using db

            MessageBox.Show(lp.ToString() + " 개의 행 출고취소 !");
            retrieve();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["matnrdesc"].Value.ToString();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Decimal qty = 0, fqty = 0, vol = 0;            

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                vol = vol + Convert.ToDecimal(dataGridView1.Rows[i].Cells["vol"].Value.ToString());
                qty = qty + Convert.ToDecimal(dataGridView1.Rows[i].Cells["qty"].Value.ToString());
                fqty = fqty + Convert.ToDecimal(dataGridView1.Rows[i].Cells["fqty"].Value.ToString());
            }
            lblltqty.Text = string.Format("{0:n3}", vol);
            lblqty.Text = string.Format("{0:n0}", qty);
            lblfqty.Text = string.Format("{0:n0}", fqty);
         
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
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and hdate <= '" + date2 + "'";
            }

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '" + tbdoc.Text.Trim() + "%'";
            if (tbord.Text.Trim() != "") modstr = modstr + " and sdno like '" + tbord.Text.Trim() + "%'";
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '" + tbprod.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and matnrdesc like '%" + txtpdesc.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '" + tbbatch.Text.Trim() + "%'";

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<miordiq>(d.ExecuteQuery<miordiq>(modstr).ToList());
                
                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
    }
    public class hiodxq
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
        public decimal pksz { get; set; }
        public string flag { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string remark { get; set; }
        public string idate { get; set; }
        public string itime { get; set; }
        public string oprod { get; set; }
    }
}
