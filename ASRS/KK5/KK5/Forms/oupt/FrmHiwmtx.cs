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
    public partial class FrmHiwmtx : Form
    {
        private bool sortAscending = false;

    #region --- MDI Child ----------------
    private static FrmHiwmtx _instance;
        public static FrmHiwmtx Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmHiwmtx();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmHiwmtx_FormClosed(object sender, FormClosedEventArgs e)
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
                                 (substring(hdate,1,4) + '-' +  substring(hdate,5,2) + '-' +  substring(hdate,7,2) + ' ' +
                                  substring(htime,1,2) + ':' +  substring(htime,3,2) + ':' +  substring(htime,5,2) + ' ') as hist_dt   
                            FROM hiwmto  
                           WHERE docnum is not null and  IO = '$'   ";


        string sqls  = @"select wmtxkey, docnum, tanum, tapos,  pksz, bwlvs, IO, pltno, lstk, qty, flag, credat, cretim, remark from hiwmtx 
                         where  docnum = {0} and tanum = {1} and tapos = {2} ";
        #endregion

        DataGridView dv1, dv2;
        public FrmHiwmtx()
        {
            InitializeComponent();
            FormClosed += FrmHiwmtx_FormClosed;

            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionChanged += Dv1_SelectionChanged;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dv1.CellFormatting += Dv1_CellFormatting;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            //dv2.CellFormatting += Dv2_CellFormatting;
            dv2.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          
        }
        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            retrieve2();
        }

        private void FrmHiwmtx_Load(object sender, EventArgs e)
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
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and hdate <= '" + date2 + "'";
            }

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '" + tbdoc.Text.Trim() + "%'";
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '" + tbprod.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and maktx like '%" + txtpdesc.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '" + tbbatch.Text.Trim() + "%'";

            string bwlvs = comboBox1.SelectedItem.ToString().Substring(0, 3);
            if (bwlvs != "ALL") modstr = modstr + " and bwlvs = '" + bwlvs + "'";

            modstr = modstr + " and IO in ( '$' ) order by hdate, htime, maktx ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<miwmtox>(db.ExecuteQuery<miwmtox>(modstr).ToList());
                //var q = db.ExecuteQuery<miwmtox>(modstr).ToList();
                //dv1.DataSource = q;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
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

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

      

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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

        private void btncncl_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("기타 출고취소하여 바닥재고로 잡으시겠읍니까?", "ERP 출고취소",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());

            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string maktx = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["bestq"].Value.ToString();

            int rc = 0;
            int st = 0;
            int lp = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();

                foreach (DataGridViewRow r in dv2.SelectedRows)
                {
                    decimal wmtxkey = Convert.ToDecimal(r.Cells["wmtxkey_f"].Value.ToString());
                    decimal qty = Convert.ToDecimal(r.Cells["qty_f"].Value.ToString());
                    decimal pksz = Convert.ToDecimal(r.Cells["pksz_f"].Value.ToString());

                    string idate = DateTime.Now.ToString("yyyy/MM/dd");
                    string itime = DateTime.Now.ToString("hh:mm:ss");
                    string remark = r.Cells["remark_f"].Value.ToString();

                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            db.ExecuteCommand(@"delete from hiwmtx where wmtxkey = {0} ", wmtxkey);
                            db.ExecuteCommand(@"update hiwmto set fqty = fqty - {0} where docnum = {1} and tanum = {2} and tapos = {3} ", qty, docnum, tanum, tapos);

                            string sqlupd = @"update miplti
                                                 set plti_stok = plti_stok + {0} 
                                              where plti_pltno = '00000000' and plti_lstk = 'Y000000' 
                                              and plti_prod = {1} and plti_loc = {2} and plti_lot = {3} and plti_bestq = {4} ";

                            db.p_tilock();

                            rc = db.ExecuteCommand(sqlupd, qty, matnr, lgort, charg, bestq);
                            if (rc <= 0)
                            {
                                string sqlins = @"insert into miplti
                                        ( plti_pltno, plti_lstk,   plti_prod,  plti_loc,     plti_lot,       plti_bestq, 
                                          plti_pksz,  plti_stok,   plti_rqty,  plti_icust,   plti_cycl_date, plti_idate,
                                          plti_itime, plti_pdesc,  plti_oprod, plti_flag,    plti_label,     plti_remark )
                                  values ( '00000000', 'Y000000',   {0},        {1},          {2},            {3},
                                           {4},        {5},         0,           '',          {6},            {7},
                                           {8},        {9},         '',          '1',         '0',            {10} ) ";

                                rc = db.ExecuteCommand(sqlins, matnr, lgort, charg, bestq, pksz, qty, idate, idate, itime, maktx, remark);
                                if (rc <= 0)
                                {
                                    db.Transaction.Rollback();
                                    break;
                                }
                            }

                            db.ExecuteCommand(@"update miwmto set rqty = rqty - {0}, fqty = fqty - {0} where docnum = {1} and tanum = {2} and tapos = {3} 
                                                and 0 < (select count(*) from miwmto where docnum = {1} and tanum = {2} and tapos = {3}) ",
                                                qty, docnum, tanum, tapos);
                            
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

        private void retrieve2()
        {
            if (dv1.SelectedRows.Count == 0)
            {
                dv2.DataSource = null;
                return;
            }

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<hiwmtxq>(sqls, docnum, tanum, tapos).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
    }
    public class hiwmtxq
    {
        public decimal wmtxkey { get; set; }
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public int tapos { get; set; }
        public string bwlvs { get; set; }
        public string IO { get; set; }
        public string lstk { get; set; }
        public string pltno { get; set; }
        public decimal pksz { get; set; }
        public decimal qty { get; set; }
        public string flag { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string remark { get; set; }
    }
}
