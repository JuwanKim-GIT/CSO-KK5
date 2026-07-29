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
    public partial class FrmEtcLoadHistory1 : Form
    {
        #region --- MDI Child ----------------
        private static FrmEtcLoadHistory1 _instance;
        public static FrmEtcLoadHistory1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmEtcLoadHistory1();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        DataGridView dv1, dv2;
        private void FrmEtcLoadHistory1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
        #region --- SQL statement for query --------------
   
        string sqlcar = @"SELECT  duedate ,
                               car_no ,
                               seq ,
                               car_man ,
                               car_dest ,
                               max_vol ,
                               load_vol ,
                               load_qty ,
                               step ,
                               remark ,
                               bachaDate ,
                               area_code ,
                               uuse ,
                               car_desc
                         FROM hacar WHERE bachadate is not null and flag = '1' ";

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
                           car_no, 
                           car_step,
                           car_sno,
                           print_step,
                           bachadate,
                           arrival,
                           ordi_seq,
                           ordi_check
                           from hawmto where docnum is not null and io = '$' ";
        
        #endregion
        public FrmEtcLoadHistory1()
        {
            InitializeComponent();
            FormClosed += FrmEtcLoadHistory1_FormClosed;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv2 = dataGridView2;
            dv2.ReadOnly = true;
            dv2.AutoGenerateColumns = false;
            dv2.RowPostPaint += Common.RowPostPaint;
        }
        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0)
            {
                dv2.DataSource = null;
                return;
            }
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string sql = sqls;

                sql = sql + " and bachadate = '" + bachadate + "'";
                sql = sql + " and car_sno = " + seq.ToString("0");
                sql = sql + " and car_no = '" + car_no + "'";
                             
                dv2.DataSource = new SortableBindingList<hawmtoq>(db.ExecuteQuery<hawmtoq>(sql).ToList());

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void FrmEtcLoadHistory1_Load(object sender, EventArgs e)
        {
            dtbachadate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void retrieve()
        {
            string bachadate = dtbachadate.Text.Replace("-", "/");

            string modstr = sqlcar;
            modstr = modstr + " and bachadate = '" + bachadate + "'";
            string car_no = tbcar.Text.Trim();
            if (car_no != "") modstr = modstr + " and car_no ='" + car_no + "'";
            string seq = nuseq.Value.ToString("0");
            if (seq != "0") modstr = modstr + " and seq = " + seq;
          

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<hacarq>(modstr).ToList();
                dv1.DataSource = q;
            }
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());

            using (FrmPrintToExcel3 p = new FrmPrintToExcel3(bachadate, car_no, seq, true))
            {
                p.ShowDialog();
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    public class hawmtoq
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
        public string bigo { get; set; }
        public string ordi_check { get; set; }
        public decimal pksz { get; set; }
        public int ordi_seq { get; set; }
        public string bachadate { get; set; }
        public string car_no { get; set; }
        public string car_step { get; set; }
        public int car_sno { get; set; }
        public string print_step { get; set; }
    }
}
