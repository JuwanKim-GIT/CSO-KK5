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
    public partial class FrmMiinpe : Form
    {
        #region --- MDI Child ----------------
        private static FrmMiinpe _instance;
        public static FrmMiinpe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMiinpe();

                return _instance;
            }
        }
        private void FrmMiinpe_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion


        DataGridView dv;
        DBDataContext db;
        #region  ----------- Select문-----------------
        string orgsql = @"SELECT docnum,   
         credat,   
         cretim,  
         (substring(credat,1,4) + '-' +  substring(credat,5,2) + '-' +  substring(credat,7,2) + ' ' +
          substring(cretim,1,2) + ':' +  substring(cretim,3,2) + ':' +  substring(cretim,5,2) + ' ') as credt, 
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
         vsolm,
         (vsolm - 0) as sqty,
         pksz,     
         (vsolm * pksz) as ltqty,
         nltyp,   
         maktx,   
         vfdat,   
         lgort,   
         rqty,   
         fqty,   
         flag,   
         io
    FROM miwmto
   WHERE miwmto.io = 'I' ";
        #endregion

        public FrmMiinpe()
        {
            InitializeComponent();

            this.FormClosed += FrmMiinpe_FormClosed;
            
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.CellFormatting += Dv_CellFormatting;
            dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dv.RowPostPaint += Common.RowPostPaint;

            if (Config.UserLevel != "1") btnreceipt.Enabled = false;
        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void FrmMiinpe_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 8;
            //retrieve();
        }
        private void retrieve()
        {
            string modstr = orgsql;
                    
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

            string docnum = tbDoc.Text.Trim();
            if (docnum != "") modstr = modstr + " and docnum like '" + docnum + "%'";

            string bwlvs = comboBox1.Text.Trim().Substring(0,3);
            if (bwlvs != "ALL") modstr = modstr + " and bwlvs = '" + bwlvs + "'";

            string prod = txtprod.Text.Trim();
            if (prod != "") modstr = modstr + " and matnr like '" + prod + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and maktx like '%" + pdesc + "%'";

            string charg = tbbatch.Text.Trim();
            if (charg != "") modstr = modstr + " and charg like '" + charg + "%'";

            modstr = modstr + " order by credat, cretim, maktx, lgort, charg ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv.DataSource = new SortableBindingList<miinpe>(db.ExecuteQuery<miinpe>(modstr).ToList());

                dv.TopLeftHeaderCell.Value = dv.RowCount.ToString();
                dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }             
        }

        private void btnqry_Click(object sender, EventArgs e)
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

        private void btnreceipt_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            if (MessageBox.Show("납입확정하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            // insert hiwmto
            // update insert miplri(바닥 재고)

            this.Cursor = Cursors.WaitCursor;
            string docnum;
            decimal tanum, vsolm;
            int tapos;
            decimal sqty = 0;

            int rc = 0;
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
                    docnum = r.Cells["docnum"].Value.ToString();
                    tanum = Convert.ToDecimal(r.Cells["tanum"].Value.ToString());
                    tapos = Convert.ToInt32(r.Cells["tapos"].Value.ToString());

                    vsolm = Convert.ToDecimal(r.Cells["vsolm"].Value.ToString());
                    sqty = Convert.ToDecimal(r.Cells["sqty"].Value.ToString());
                    if (vsolm < sqty) break;
                    if (sqty <= 0) break;

                    using (db.Transaction = db.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = db.P_miwmto_in2(docnum, tanum, tapos, sqty);

                            if (rc != 1) { db.Transaction.Rollback(); break; }
                            else
                            {
                                db.Transaction.Commit();
                                if (vsolm == sqty) dv.Rows.Remove(r);
                                else
                                {
                                    r.Cells["vsolm"].Value = vsolm - sqty;
                                    r.Cells["sqty"].Value = vsolm - sqty;
                                    if (vsolm - sqty == 0)
                                        dv.Rows.Remove(r);
                                }
                            }
                        }
                        catch (Exception E) { db.Transaction.Rollback(); this.Cursor = Cursors.Default; MessageBox.Show(E.Message); }
                    }
                }
                db.Connection.Close();
            }
            Cursor = Cursors.Default;
            if (rc != 1) MessageBox.Show("납입 실패...!(" + rc.ToString() + ")");
         
        }

        private void tbDoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbDoc.Text = dv.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            txtprod.Text = dv.SelectedRows[0].Cells["matnr"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            if (MessageBox.Show("삭제하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;
            string docnum;
            decimal tanum;
            int tapos;
            int lp = 0;
            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    docnum = r.Cells["docnum"].Value.ToString();
                    tanum = Convert.ToDecimal(r.Cells["tanum"].Value.ToString());
                    tapos = Convert.ToInt32(r.Cells["tapos"].Value.ToString());

                    db.ExecuteCommand(@"delete from miwmto where docnum = {0} and tanum = {1} and tapos = {2}", docnum, tanum, tapos);
                    lp++;
                    dv.Rows.Remove(r);
                }
            }
            Cursor = Cursors.Default;                  
           
        }

        private void btnpksz_Click(object sender, EventArgs e)
        {

            if (dv.SelectedRows.Count <= 0) return;
            decimal pksz = 0.000m;

            using (FrmPKSZ_p p = new FrmPKSZ_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                pksz = p.numericUpDown1.Value;
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
                    string docnum = r.Cells["docnum"].Value.ToString();
                    decimal tanum = Convert.ToDecimal(r.Cells["tanum"].Value.ToString());
                    int tapos = Convert.ToInt32(r.Cells["tapos"].Value.ToString());
                    string matnr = r.Cells["matnr"].Value.ToString();

                    rc = db.ExecuteCommand(@"update miwmto set pksz = {0} where docnum = {1} and tanum = {2} and tapos = {3} ", pksz, docnum, tanum, tapos);
                    lp++;
                    r.Cells["pksz"].Value = pksz;
                }
            }            
        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            long stoksum = 0;
            decimal volsum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stoksum = stoksum + Convert.ToInt32(dataGridView1.Rows[i].Cells["vsolm"].Value.ToString());
                volsum = volsum + Convert.ToDecimal(dataGridView1.Rows[i].Cells["ltqty"].Value.ToString());
            }
            lblqty.Text = string.Format("{0:n0}", stoksum);
            lblltqty.Text = string.Format("{0:n3}", volsum);
        }
    }

    public class miinpe
    {
        public string docnum { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string credt { get; set; }
        public string lgnum { get; set; }
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
        public string meins { get; set; }
        public string wdatu { get; set; }
        public string wenum { get; set; }
        public string vltyp { get; set; }
        public decimal vsolm { get; set; }
        public decimal sqty { get; set; }
        public decimal pksz { get; set; }
        public decimal ltqty { get; set; }
        public string nltyp { get; set; }
        public string maktx { get; set; }
        public string vfdat { get; set; }
        public string lgort { get; set; }
        public decimal rqty { get; set; }
        public decimal fqty { get; set; }
        public string flag { get; set; }
        public string io { get; set; }
        public string hdate { get; set; }
        public string htime { get; set; }

    }
}
