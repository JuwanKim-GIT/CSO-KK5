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
    public partial class FrmLoadHistory2 : Form
    {
        #region --- MDI Child ----------------
        private static FrmLoadHistory2 _instance;
        public static FrmLoadHistory2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLoadHistory2();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmLoadHistory2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region --- SQL statement for query --------------
        string sqlm = @" SELECT haordi.docnum,   
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


        #endregion

        DataGridView dv1;
        public FrmLoadHistory2()
        {
            InitializeComponent();
            FormClosed += FrmLoadHistory2_FormClosed;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.CellFormatting += Dv1_CellFormatting; ;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = true;
            dv1.RowPostPaint += Common.RowPostPaint;

        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void FrmLoadHistory2_Load(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = sqlm;

            string fdate = dtDatefrom.Text.Replace("-", "/");
            string tdate = dtDateTo.Text.Replace("-", "/");
            if (!chkdt.Checked) modstr = modstr + " and bachadate = '" + fdate + "'";
            else
            {
                modstr = modstr + " and bachadate >= '" + fdate + "'";
                modstr = modstr + " and bachadate <= '" + tdate + "'";
            }
        
            string sdno1 = tbord.Text;
            if (sdno1 != "") modstr = modstr + " and sdno like '" + sdno1 + "%'";

            string prod1 = tbProd.Text;
            if (prod1 != "") modstr = modstr + " and matnr like '" + prod1 + "%'";
          
            string car_no = tbcar.Text.Trim();
            if (car_no != "") modstr = modstr + " and car_no = '" + car_no + "'";

            string seq = tbseq.Text.Trim();
            if (seq != "") modstr = modstr + " and car_sno = " + seq;

            string lot = tblot.Text.Trim();
            if (lot != "") modstr = modstr + " and charg like '%" + lot +"%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and matnrdesc like '%" + pdesc + "%'";

            modstr = modstr + " order by arrival, cust, sdno";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<haordiq>(db.ExecuteQuery<haordiq>(modstr).ToList());
                
                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }               
           
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SortableBindingList<haordiq> q = (SortableBindingList<haordiq>)dataGridView1.DataSource;
            lblltqty.Text = q.Sum(x => x.ordi_ltqty).ToString("###,##0.000");
            lblqty.Text = q.Sum(x => x.qty).ToString("###,##0");
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
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
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["car_sno"].Value.ToString());

            using (FrmPrintToExcel2 p = new FrmPrintToExcel2(bachadate, car_no, seq, true))
            {
                p.ShowDialog();
            }
        }

        private void tbProd_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbProd.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
        }

        private void tbord_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbord.Text = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
        }

        private void tblot_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tblot.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["matnrdesc"].Value.ToString();
        }

        private void btnexcel2_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dv1);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                (sender as DataGridView).CopyClipboardData();
        }
    }
}
