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
    public partial class Frmhichng : Form
    {
        #region --- MDI Child ----------------
        private static Frmhichng _instance;
        public static Frmhichng Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frmhichng();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void Frmhichng_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

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
                                 vsolm,   
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
                       WHERE docnum is not null  and bwlvs in ('309', '321' ) ";
        DataGridView dv1, dv2;
        public Frmhichng()
        {
            InitializeComponent();
            FormClosed += Frmhichng_FormClosed;
            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.ReadOnly = true;
            dv1.CellFormatting += Dv1_CellFormatting;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;

            dv2 = dataGridView2;
            dv2.AutoGenerateColumns = false;
            dv2.ReadOnly = true;
            dv2.CellFormatting += Dv2_CellFormatting;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = false;

            dv1.SelectionChanged += Dv1_SelectionChanged; ;

            comboBox1.SelectedIndex = 0;
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            retrieve2();
        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void Frmhichng_Load(object sender, EventArgs e)
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
        private void tbDoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbDoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void tbmaterial_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbmaterial.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();

        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
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

            string bwlvs = comboBox1.SelectedItem.ToString();
            if (bwlvs != "ALL") modstr = modstr + " and bwlvs = '" + bwlvs.Substring(0, 3) + "'";

            string docnum = tbDoc.Text.Trim();
            if (docnum != "") modstr = modstr + " and docnum like '" + docnum + "%'";

            string prod = tbmaterial.Text.Trim();
            if (prod != "") modstr = modstr + " and matnr like '" + prod + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and maktx like '" + pdesc + "%'";

            string charg = tbbatch.Text.Trim();
            if (charg != "") modstr = modstr + " and charg like '" + charg + "%'";


            modstr = modstr + " order by hdate, htime,  docnum, tanum, tapos ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {                
                dv1.DataSource = new SortableBindingList<michnge>(db.ExecuteQuery<michnge>(modstr).ToList());

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtloc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtloc.Text = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();            
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.RowPostPaint(sender, e);
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

            string sql= @"SELECT a.wmtxkey,   
                                a.docnum,   
                                a.tanum,   
                                a.tapos,   
                                a.bwlvs,   
                                a.IO,   
                                a.lstk,   
                                a.pltno, 
                                b.matnr,
                                b.maktx,
                                b.lgort,
                                b.charg,
                                b.bestq,  
                                a.qty,   
                                a.flag,   
                                a.credat,   
                                a.cretim,   
                                a.remark  
                        FROM hiwmtx a join hiwmto b on a.docnum = b.docnum and a.tanum = b.tanum and a.tapos = b.tapos
                        WHERE a.docnum = {0} and a.tanum = {1}  and a.tapos = {2} ";


            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<hiwmtx>(sql, docnum, tanum, tapos).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
    }


    public class hiwmtx
    {
        public decimal wmtoxkey { get; set; }
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
        public string remark { get; set; }

    }



}