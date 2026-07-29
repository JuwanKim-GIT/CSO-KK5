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
    public partial class Frmhiinpe : Form
    {
        #region --- MDI Child ----------------
        private static Frmhiinpe _instance;
        public static Frmhiinpe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frmhiinpe();

                return _instance;
            }
        }
        private void Frmhiinpe_FormClosed(object sender, FormClosedEventArgs e)
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
         (pksz * vsolm) as ltqty,   
         nltyp,   
         maktx,   
         vfdat,   
         lgort,   
         rqty,   
         fqty,   
         flag,   
         io,
         hdate,
         htime,
        (substring(hdate,1,4) + '-' +  substring(hdate,5,2) + '-' +  substring(hdate,7,2) + ' ' +
         substring(htime,1,2) + ':' +  substring(htime,3,2) + ':' +  substring(htime,5,2) + ' ') as hist_dt   
    FROM hiwmto
   WHERE hiwmto.io = 'I' ";
        #endregion
        public Frmhiinpe()
        {
            InitializeComponent();

            this.FormClosed += Frmhiinpe_FormClosed;

            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.CellFormatting += Dv_CellFormatting;
            dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dv.RowPostPaint += Common.RowPostPaint;
        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          
        }

        private void Frmhiinpe_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 8;
            retrieve();
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
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and hdate >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and hdate <= '" + date2 + "'";
            }

            string docnum = tbDoc.Text.Trim();
            if (docnum != "") modstr = modstr + " and docnum like '" + docnum + "%'";

            string bwlvs = comboBox1.Text.Trim().Substring(0, 3);
            if (bwlvs != "ALL") modstr = modstr + " and bwlvs = '" + bwlvs + "'";

            string prod = txtprod.Text.Trim();
            if (prod != "") modstr = modstr + " and matnr like '" + prod + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and maktx like '%" + pdesc + "%'";

            string charg = tbbatch.Text.Trim();
            if (charg != "") modstr = modstr + " and charg like '" + charg + "%'";

            modstr = modstr + " order by hdate, htime, maktx, lgort, charg ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv.DataSource = new SortableBindingList<hiinpe>(db.ExecuteQuery<hiinpe>(modstr).ToList());

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

        private void tbDoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbDoc.Text = dv.SelectedRows[0].Cells["docnum"].Value.ToString();
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
            if (dv.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void txtprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            txtprod.Text = dv.SelectedRows[0].Cells["matnr"].Value.ToString();
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

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv.SelectedRows[0].Cells["charg"].Value.ToString();
        }
    }
    public class hiinpe
    {
        public string docnum { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
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
        public decimal pksz { get; set; }
        public decimal vsolm { get; set; }
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
        public string hist_dt { get; set; }

    }
}
