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
    public partial class FrmBarError : Form
    {
        #region --- MDI Child ----------------
        private static FrmBarError _instance;
        public static FrmBarError Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmBarError();

                return _instance;
            }
        }
        private void FrmBarError_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        public FrmBarError()
        {
            InitializeComponent();
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.CellFormatting += Dv_CellFormatting;
            dv.RowPostPaint += Common.RowPostPaint;

            this.FormClosed += FrmBarError_FormClosed;
        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 4) return;
            if (e.Value == null) return;
            string act = e.Value.ToString();
            
            if (act == "0") e.Value = "순환이동";
            if (act == "1") e.Value = "바코드수입력";
            if (act == "2") e.Value = "바코드재리딩";
            e.FormattingApplied = true;
        }

        string orgsql = "  SELECT err_date, err_time, err_pltno, err_msg, err_act, err_mmsg From tbberr WHERE err_date is not null ";

        private void FrmBarError_Load(object sender, EventArgs e)
        {
         
        }
        private void query()
        {
            DBDataContext db = new DBDataContext(Config.DBCon);

            string modstr = orgsql;

            string date1 = dtDatefrom.Text;
            string date2 = dtDateTo.Text;

            date1 = date1.Replace("-", "/");
            date2 = date2.Replace("-", "/");

            if (!chkdt.Checked)
            {
                if (date1 != "") modstr = modstr + " and err_date >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and err_date >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and err_date <= '" + date2 + "'";
            }
            string pltno = tbpltno.Text.Trim();
            if (pltno != "")
            {
                modstr = modstr + " and err_pltno like '" + pltno + "%'";
            }
            dv.DataSource = new SortableBindingList<tbberrq>(db.ExecuteQuery<tbberrq>(modstr).ToList());
            //var q = db.ExecuteQuery<tbberrq>(modstr).ToList();
            //dv.DataSource = q;

        }
        private void btnqry_Click(object sender, EventArgs e)
        {
            query();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void tbpltno_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbpltno.Text = dataGridView1.SelectedRows[0].Cells["err_pltno"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }
    }
    public class tbberrq
    {
        public string err_date { get; set; }
        public string err_time { get; set; }
        public string err_pltno { get; set; }
        public string err_msg { get; set; }
        public string err_act { get; set; }
        public string err_mmsg { get; set; }
    }
}
