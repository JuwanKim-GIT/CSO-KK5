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
    public partial class FrmLoadHistory1 : Form
    {
        #region --- MDI Child ----------------
        private static FrmLoadHistory1 _instance;
        public static FrmLoadHistory1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLoadHistory1();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmLoadHistory1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region --- SQL statement for query --------------
        string sqls = @" SELECT haordi.docnum,   
                             haordi.credat,   
                             haordi.cretim,   
                             haordi.sdno,   
                             haordi.route,   
                             haordi.routedesc,   
                             haordi.deltyp,   
                             haordi.deltypdesc,   
                             haordi.cust,   
                             haordi.cust_name1,   
                             haordi.cust_name2,   
                             haordi.street,   
                             haordi.post,   
                             haordi.city,   
                             haordi.tel,   
                             haordi.contry,   
                             haordi.region,   
                             haordi.wecust,   
                             haordi.wecust_name1,   
                             haordi.wecust_name2,   
                             haordi.westreet,   
                             haordi.wepost,   
                             haordi.wecity,   
                             haordi.wetel,   
                             haordi.wecontry,   
                             haordi.weregion,   
                             haordi.duedate,   
                             haordi.cmmt,   
                             haordi.rmrk,   
                             haordi.parcel,   
                             haordi.posnr,   
                             haordi.matnr,   
                             haordi.matnrdesc,   
                             haordi.lgort,   
                             haordi.charg,   
                             haordi.plant,   
                             haordi.qty,   
                             haordi.gwgt,   
                             haordi.nwgt,   
                             haordi.wunit,   
                             haordi.vol,   
                             haordi.vunit,   
                             haordi.pstyv,   
                             haordi.pstyvdesc,   
                             haordi.sono,   
                             haordi.soposnr,   
                             haordi.sodate,   
                             haordi.custpo,   
                             haordi.custpodate,   
                             haordi.rqty,   
                             haordi.fqty,   
                             haordi.flag,   
                             haordi.arrival,   
                             haordi.car_no,   
                             haordi.car_step,   
                             haordi.car_sno,   
                             haordi.print_step,   
                             haordi.ordi_seq,   
                             haordi.ordi_check,   
                             haordi.remark,   
                             haordi.bachadate,   
                             haordi.ordi_ltqty,   
                             haordi.ordi_size,   
                             haordi.recv_dt,   
                             haordi.hdate,   
                             haordi.htime,
                             haordi.vsbed,
                             haordi.ablad
                    FROM haordi
                    WHERE haordi.docnum is not null  ";

        string sqlcar = @"SELECT  hacar.dueDate ,
                               hacar.car_no ,
                               hacar.seq ,
                               hacar.car_man ,
                               hacar.car_dest ,
                               hacar.max_vol ,
                               hacar.load_vol ,
                               hacar.load_qty ,
                               hacar.step ,
                               hacar.remark ,
                               hacar.bachaDate ,
                               hacar.area_code ,
                               hacar.uuse ,
                               hacar.car_desc
                         FROM hacar WHERE hacar.bachadate is not null and flag = '' ";
        #endregion

        DataGridView dv1, dv2;
        public FrmLoadHistory1()
        {
            InitializeComponent();
            FormClosed += FrmLoadHistory1_FormClosed;

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
        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            if(dv1.SelectedRows.Count == 0)
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

                dv2.DataSource = new SortableBindingList<haordiq>(db.ExecuteQuery<haordiq>(sql).ToList());

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void FrmLoadHistory1_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["seq"].Value.ToString());
       
            using (FrmPrintToExcel2 p = new FrmPrintToExcel2(bachadate, car_no, seq, true))
            {
                p.ShowDialog();
            }
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
    }
    public class haordiq
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
        public string wetel { get; set; }
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
        public string vsbed { get; set; }
        public string ablad { get; set; }
    }
    public class hacarq
    {
        public string bachadate { get; set; }
        public int seq { get; set; }
        public string car_no { get; set; }
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
