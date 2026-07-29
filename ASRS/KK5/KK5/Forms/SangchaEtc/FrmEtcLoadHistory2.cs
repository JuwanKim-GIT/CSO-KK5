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
    public partial class FrmEtcLoadHistory2 : Form
    {
        #region --- MDI Child ----------------
        private static FrmEtcLoadHistory2 _instance;
        public static FrmEtcLoadHistory2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmEtcLoadHistory2();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmLoadHistory2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region sql select

        string sqlm = @"Select
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
        DataGridView dv1;
        public FrmEtcLoadHistory2()
        {
            InitializeComponent();
            FormClosed += FrmLoadHistory2_FormClosed;

            dv1 = dataGridView1;
            dv1.ReadOnly = true;
            dv1.AutoGenerateColumns = false;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = true;
            dv1.RowPostPaint += Common.RowPostPaint;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEtcLoadHistory2_Load(object sender, EventArgs e)
        {
            dtDatefrom.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtDateTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void btnqry_Click(object sender, EventArgs e)
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

            string docnum = tbdoc.Text;
            if (docnum != "") modstr = modstr + " and docnum like '%" + docnum + "%'";

            string prod = tbProd.Text;
            if (prod != "") modstr = modstr + " and matnr like '%" + prod + "%'";

            string car_no = tbcar.Text.Trim();
            if (car_no != "") modstr = modstr + " and car_no = '" + car_no + "'";

            string seq = tbseq.Text.Trim();
            if (seq != "") modstr = modstr + " and car_sno = " + seq;

            string lot = tblot.Text.Trim();
            if (lot != "") modstr = modstr + " and charg like '%" + lot + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and maktx like '%" + pdesc + "%'";

            modstr = modstr + " order by docnum, tanum";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<hawmtoq>(db.ExecuteQuery<hawmtoq>(modstr).ToList());

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SortableBindingList<hawmtoq> q = (SortableBindingList<hawmtoq>)dataGridView1.DataSource;
            lblltqty.Text = q.Sum(x => x.pksz * x.vsolm).ToString("###,##0.000");
            lblqty.Text = q.Sum(x => x.vsolm).ToString("###,##0");
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                (sender as DataGridView).CopyClipboardData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            string bachadate = dv1.SelectedRows[0].Cells["bachadate"].Value.ToString();
            string car_no = dv1.SelectedRows[0].Cells["car_no"].Value.ToString();
            int seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["car_sno"].Value.ToString());

            using (FrmPrintToExcel3 p = new FrmPrintToExcel3(bachadate, car_no, seq, true))
            {
                p.ShowDialog();
            }
        }

        private void tbProd_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbProd.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
        }

        private void tbdoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbdoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void tblot_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tblot.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void tbProd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
