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
    public partial class FrmMijchg : Form
    {
        #region --- MDI Child ----------------
        private static FrmMijchg _instance;
        public static FrmMijchg Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMijchg();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmMijchg_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        public FrmMijchg()
        {
            InitializeComponent();
            FormClosed += FrmMijchg_FormClosed;
            dataGridView1.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void FrmMijchg_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void retrieve()
        {
            string date1 = dtDatefrom.Text.Replace("-", "/");
            string date2 = dtDateTo.Text.Replace("-", "/");
            string modstr = sql;
            if (!chkdt.Checked)
            {
                modstr = modstr + " and plti_hdate >= '" + date1 + "'";
            }
            else
            {
                modstr = modstr + " and plti_hdate >= '" + date1 + "'";
                modstr = modstr + " and plti_hdate <= '" + date2 + "'";
            }
            string prod = tbprod.Text.Trim();
            if (prod != "")
            {
                modstr = modstr + " and plti_prod like '%" + prod + "%'";
            }

            string pdesc = tbpdesc.Text.Trim();
            if (pdesc != "")
            {
                modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";
            }
            string loc = comboBox1.SelectedItem.ToString();
            if (loc != "ALL")
            {
                loc = loc.Substring(0, 4);
                modstr = modstr + " and plti_loc = '" + loc + "'";
            }
            string ctype = comboBox2.SelectedItem.ToString();
            if (ctype != "ALL")
            {
                ctype = ctype.Substring(0, 1);
                modstr = modstr + " and plti_ctype = '" + ctype + "'";
            }

            string lot = tbLot.Text.Trim();
            if (lot != "")
            {
                modstr = modstr + " and plti_lot like '" + lot + "%'";
            }
            modstr = modstr + " order by seq";

            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dataGridView1.DataSource = new SortableBindingList<mijchgq>(db.ExecuteQuery<mijchgq>(modstr).ToList());
                //var q = db.ExecuteQuery<mijchgq>(modstr).ToList();
                //dataGridView1.DataSource = q;

                dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        string sql = @"select seq, 
                            plti_hdate + ' ' +  plti_htime + ' ' as hist_dt,   plti_hdate,  plti_htime,  plti_prod,  plti_pdesc,
                            plti_pltno,  plti_lstk,   plti_loc,   plti_lot, 
                            plti_bestq,  plti_pksz,   plti_stok,  plti_idate,
                            plti_itime,  plti_remark, plti_ctype, plti_ctype,   plti_12 
                       from mijchg where seq is not null ";

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    string ls = e.Value.ToString();
                    if (ls == "1") e.Value = "제품변경";
                    if (ls == "2") e.Value = "Loc변경";
                    if (ls == "3") e.Value = "배치변경";
                    if (ls == "4") e.Value = "수량변경";
                    if (ls == "5") e.Value = "상태변경";
                    e.FormattingApplied = true;
                }
            } 
        }
             

        private void tbLot_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbLot.Text = dataGridView1.SelectedRows[0].Cells["plti_lot"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }      

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbpdesc.Text = dataGridView1.SelectedRows[0].Cells["plti_pdesc"].Value.ToString();
        }

   
        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbprod.Text = dataGridView1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
        }
    }
    public class mijchgq
    {
        public decimal seq { get; set; }
        public string hist_dt { get; set; }
        public string plti_hdate { get; set;}
        public string plti_htime { get; set; }

        public string plti_ctype { get; set; }
        public string plti_12 { get; set; }
        public string plti_prod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_pltno { get; set; }
        public string plti_lstk { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal plti_pksz { get; set; }
        public decimal plti_stok { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_remark { get; set; }
    }
}
