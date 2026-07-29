using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace KK5
{
    public partial class FrmMimast : Form
    {
        #region --- MDI Child ----------------
        private static FrmMimast _instance;
        public static FrmMimast Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMimast();

                return _instance;
            }
        }
        private void FrmMimast_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string orgsel = "select mast_cd, mast_desc, mast_type, mast_grp, mast_old, mast_bunit, mast_szdm, " +
                        " mast_gwgt, mast_nwgt, mast_wunit, mast_vol, mast_vunit, mast_date, mast_time, mast_flag, mast_desc1, mast_canqty " +
                        " from mimast where mast_cd is not null ";

        public FrmMimast()
        {
            InitializeComponent();
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.MultiSelect = false;
            this.FormClosed += FrmMimast_FormClosed;
           
        }

        private void FrmMimast_Load(object sender, EventArgs e)
        {
            if (Config.UserLevel != "1")
            {
                btninsert.Enabled = false;
                btnmodify.Enabled = false;
                btndelete.Enabled = false;
            }
            dateTimePicker1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dateTimePicker2.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
        public void retrieve()
        {
            string modstr = orgsel;

            string mast = tbmast.Text.Trim();
            if (mast != "") modstr = modstr + " and mast_cd like '" + mast + "%'";

            string desc = tbdesc.Text.Trim();
            if (desc != "") modstr = modstr + " and mast_desc like '%" + desc + "%'";

      
            string fdate = dateTimePicker1.Text.Replace("-", "");
            string tdate = dateTimePicker2.Text.Replace("-", "");
            if (checkBox1.Checked)
            {
                modstr = modstr + " and mast_date >= '" + fdate + "'";
                modstr = modstr + " and mast_date <= '" + tdate + "'";
            }

            modstr = modstr + " Order by mast_cd ";

            Cursor = Cursors.WaitCursor;
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.Connection.open();
                    dv.DataSource = new SortableBindingList<mimastq>(db.ExecuteQuery<mimastq>(modstr).ToList());

                    dv.TopLeftHeaderCell.Value = dv.RowCount.ToString();
                    dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
               
            }
        }
        public void retrieve(string mastcode)
        {
            string modstr = orgsel;

            tbmast.Text = mastcode;
            string mast = tbmast.Text.Trim();
            if (mast != "") modstr = modstr + " and mast_cd like '" + mast + "%'";

            string desc = tbdesc.Text.Trim();
            if (desc != "") modstr = modstr + " and mast_desc like '" + desc + "%'";
            modstr = modstr + " Order by mast_cd ";
            Cursor = Cursors.WaitCursor;
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    dv.DataSource = new SortableBindingList<mimastq>(db.ExecuteQuery<mimastq>(modstr).ToList());

                    dv.TopLeftHeaderCell.Value = dv.RowCount.ToString();
                    dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;
            if (MessageBox.Show("삭제 하시겠읍니끼?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string mast_cd = dv.SelectedRows[0].Cells["mast_cd"].Value.ToString();
            string sql = " delete from mimast where mast_cd = '" + mast_cd + "'";
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                db.ExecuteCommand(sql);
                db.SubmitChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("삭제 완료...!");
            retrieve();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            using (FrmMimast_add_p p = new FrmMimast_add_p())
            {
                if (p.ShowDialog() == DialogResult.Cancel) return; 
                p.Dispose();
                retrieve(p.tbcd.Text.Trim());
            }
            retrieve();
        }

        private void btnmodify_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;

            mimastq m = new mimastq();
            m.mast_cd = dv.SelectedRows[0].Cells["mast_cd"].Value.ToString();
            m.mast_desc = dv.SelectedRows[0].Cells["mast_desc"].Value.ToString();
            m.mast_type = dv.SelectedRows[0].Cells["mast_type"].Value.ToString();
            m.mast_grp = dv.SelectedRows[0].Cells["mast_grp"].Value.ToString();
            m.mast_old = dv.SelectedRows[0].Cells["mast_old"].Value.ToString();
            m.mast_bunit = dv.SelectedRows[0].Cells["mast_bunit"].Value.ToString();
            m.mast_desc = dv.SelectedRows[0].Cells["mast_desc"].Value.ToString();
            m.mast_szdm = dv.SelectedRows[0].Cells["mast_szdm"].Value.ToString();
            m.mast_gwgt = Convert.ToDecimal(dv.SelectedRows[0].Cells["mast_gwgt"].Value.ToString());
            m.mast_nwgt = Convert.ToDecimal(dv.SelectedRows[0].Cells["mast_nwgt"].Value.ToString());
            m.mast_wunit = dv.SelectedRows[0].Cells["mast_wunit"].Value.ToString();
            m.mast_vol = Convert.ToDecimal(dv.SelectedRows[0].Cells["mast_vol"].Value.ToString());
            m.mast_vunit = dv.SelectedRows[0].Cells["mast_vunit"].Value.ToString();
            m.mast_flag = dv.SelectedRows[0].Cells["mast_flag"].Value.ToString();
            m.mast_desc1 = dv.SelectedRows[0].Cells["mast_desc1"].Value.ToString();
            m.mast_canqty = Convert.ToInt32(dv.SelectedRows[0].Cells["mast_canqty"].Value.ToString());

            using (FrmMimast_edit_p p = new FrmMimast_edit_p(m))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                retrieve(p.tbcd.Text.Trim());
            }

        }      

        private void tbdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbdesc.Text = dv.SelectedRows[0].Cells["mast_desc"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.RowPostPaint(sender, e);
        }    

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
            dateTimePicker2.Enabled = checkBox1.Checked;
        }

   
        private void tbdesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbmast_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbmast.Text = dv.SelectedRows[0].Cells["mast_cd"].Value.ToString();
        }
    }
    public class mimastq
    {
        public string mast_cd { get; set; }
        public string mast_desc { get; set; }
        public string mast_type { get; set; }
        public string mast_grp { get; set; }
        public string mast_old { get; set; }
        public string mast_bunit { get; set; }
        public string mast_szdm { get; set; }
        public decimal mast_gwgt { get; set; }
        public decimal mast_nwgt { get; set; }
        public string mast_wunit { get; set; }
        public decimal mast_vol { get; set; }
        public string mast_vunit { get; set; }
        public string mast_date { get; set; }
        public string mast_time { get; set; }
        public string mast_flag { get; set; }
        public string mast_desc1{ get; set; }
        public int mast_canqty { get; set; }
    }
}
