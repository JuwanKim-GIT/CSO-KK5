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
    public partial class FrmMimvht : Form
    {
        #region --- MDI Child ----------------
        private static FrmMimvht _instance;
        public static FrmMimvht Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMimvht();

                return _instance;
            }
        }
        private void FrmMimvht_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        
        }
        #endregion

        DataGridView dv1;
    
        public FrmMimvht()
        {
            InitializeComponent();
            FormClosed += FrmMimvht_FormClosed;

            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dv1.ReadOnly = true;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.CellFormatting += Dv1_CellFormatting;
            dv1.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 13 || e.ColumnIndex == 14)
            {
                if (e.Value != null)
                {
                    string ls = e.Value.ToString();
                    e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + '-' + ls.Substring(5, 2);
                }
            }
            if (e.ColumnIndex == 15)
            {
                if (e.Value != null)
                {
                    string ls = e.Value.ToString();

                    if (ls == "I") e.Value = "입고";
                    if (ls == "$") e.Value = "출고";
                    if (ls == "M") e.Value = "이동출고";
                }
            }
        }

        private void FrmMimvht_Load(object sender, EventArgs e)
        {         
            retrieve();
        }
//        string sqlm = "Select mvhtkey,mvht_io_date, mvht_io_time, mvht_prod, mvht_proddesc from mimvht where mvhtkey is not null";
        string sqlm = "Select a.*, (a.mvht_io_date +  ' ' + a.mvht_io_time + '  ') as iodt,  (a.mvht_ioqty * a.mvht_pksz) as mvht_ltqty from mimvht a where a.mvhtkey is not null";
        private void retrieve()
        {
            string modstr = sqlm;

            string date1 = dtDatefrom.Text;
            string date2 = dtDateTo.Text;

            date1 = date1.Replace("-", "/");
            date2 = date2.Replace("-", "/");

            if (!chkdt.Checked)
            {
                if (date1 != "") modstr = modstr + " and mvht_io_date >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and mvht_io_date >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and mvht_io_date <= '" + date2 + "'";
            }
            
            string ls_m1 = tbprod.Text.Trim();
            if (ls_m1 != "") modstr = modstr + " and mvht_prod like '" + ls_m1 + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and mvht_proddesc like '%" + pdesc + "%'";

            string ls_lot1 = tblot.Text.Trim();
            if (ls_lot1 != "") modstr = modstr + " and mvht_lot like '" + ls_lot1 + "%'";

            string bestq = comboBox1.SelectedItem.ToString().Substring(0,1);
            if (bestq != "A") modstr = modstr + " and mvht_bestq = '" + bestq + "'";
            else modstr = modstr + " and mvht_bestq = ''";

            string loc = comboBox3.SelectedItem.ToString().Trim();
            if (loc != "ALL") modstr = modstr + " and mvht_loc = '" + loc.Substring(0,4) + "'";

            string ioflag = comboBox2.SelectedItem.ToString().Substring(0, 1);
            if (ioflag != "A") modstr = modstr + " and mvht_ioflag = '" + ioflag + "'";
            modstr = modstr + " order by mvht_io_date, mvht_io_time ";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dataGridView1.DataSource = new SortableBindingList<mimvhtq>(db.ExecuteQuery<mimvhtq>(modstr).ToList());
                //var q = db.ExecuteQuery<mimvhtq>(modstr).ToList();
                //dataGridView1.DataSource = q;

                dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }               
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbprod.Text = dv1.SelectedRows[0].Cells["mvht_prod"].Value.ToString();
        }

    
        private void tblot_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tblot.Text = dv1.SelectedRows[0].Cells["mvht_lot"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv1.SelectedRows[0].Cells["mvht_proddesc"].Value.ToString();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal stoksum = 0, ltqty = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stoksum = stoksum + Convert.ToDecimal(dataGridView1.Rows[i].Cells["mvht_ioqty"].Value.ToString());
                ltqty = ltqty + Convert.ToDecimal(dataGridView1.Rows[i].Cells["mvht_ltqty"].Value.ToString());
            }
            lblqty.Text = string.Format("{0:n0}", stoksum);
            lblltqty.Text = string.Format("{0:n3}", ltqty);
        }
    }
    public class mimvhtq
    {
        public decimal mvhtkey { get; set; }
        public string iodt { get; set; }
        public string mvht_io_date { get; set; }
        public string mvht_io_time { get; set; }
        public string mvht_prod { get; set; }
        public string mvht_proddesc { get; set; }
        public string mvht_loc { get; set; }
        public string mvht_lot { get; set; }
        public string mvht_bestq { get; set; }
        public string mvht_remark { get; set; }
        public decimal mvht_pksz { get; set; }
        public decimal mvht_ioqty { get; set; }
        public decimal mvht_ltqty { get; set; }
        public string mvht_pltno { get; set; }
        public string mvht_from_lstk { get; set; }
        public string mvht_to_lstk { get; set; }
        public string mvht_ioflag { get; set; }
    }
}
