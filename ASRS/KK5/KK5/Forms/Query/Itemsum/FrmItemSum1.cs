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
    public partial class FrmItemSum1 : Form
    {
        #region --- MDI Child ----------------
        private static FrmItemSum1 _instance;
        public static FrmItemSum1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmItemSum1();

                return _instance;
            }
        }
        private void FrmItemSum1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

      
        string sql =
            "SELECT plti_prod , plti_pdesc , plti_loc , plti_lot , plti_bestq , " +
            " max(plti_pksz) as plti_pksz, sum(plti_stok) as plti_stok,  sum(plti_rqty) as plti_rqty, sum((plti_stok + plti_rqty) * plti_pksz) as plti_volume " +
            " from miplti where plti_prod is not null ";
            
        string sqlgrp = " Group by plti_prod , plti_pdesc , plti_loc , plti_lot , plti_bestq ";

        

        public FrmItemSum1()
        {
            InitializeComponent();
            this.FormClosed += FrmItemSum1_FormClosed;
            comboBox2.SelectedIndex = 0;
            
            dataGridView1.RowPostPaint += Common.RowPostPaint;
        }

        private void FrmItemSum1_Load(object sender, EventArgs e)
        {
          
            dataGridView1.AutoGenerateColumns = false;
            comboBox1.SelectedIndex = 0;
            //dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
      

            retrieve();
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                (sender as DataGridView).CopyClipboardData();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void retrieve()
        {
            string modstr = sql;
            string ls = comboBox1.Text;

            if (ls != "ALL")
            {
                if (ls.Substring(0, 1) == "A") modstr = modstr + " and substring(plti_lstk, 1,1) = 'A' ";
                if (ls.Substring(0, 1) == "F") modstr = modstr + " and substring(plti_lstk, 1,1) = 'F' ";
                if (ls.Substring(0, 1) == "Y") modstr = modstr + " and substring(plti_lstk, 1,1) = 'Y' ";
            }

            string ls_m1 = tbprod.Text;
            if (ls_m1 != "") modstr = modstr + " and plti_prod like '" + ls_m1 + "%'";

            string ls_lot1 = tblot.Text;
            string ls_lot2 = tblot2.Text;
           
            if (!checkBox1.Checked)
            {
                if (ls_lot1 != "") modstr = modstr + " and plti_lot like '" + ls_lot1 + "%'";
            }
            else
            {
                if (ls_lot1 != "") modstr = modstr + " and plti_lot >= '" + ls_lot1 + "'";
                if (ls_lot2 != "") modstr = modstr + " and plti_lot <= '" + ls_lot2 + "'";
            }

            string pdesc = txtpdesc.Text;
            if (pdesc != "") modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";

            string bestq = comboBox2.SelectedItem.ToString().Substring(0, 1);

            if (bestq != "A") modstr = modstr + " and plti_bestq = '" + bestq + "'";

            string loc = "";

            if (!chkall.Checked)
            {
                if (chk0010.Checked) loc = loc + "'0010' ";
                if (chk0035.Checked) loc = loc + "'0035' ";
                if (chk0050.Checked) loc = loc + "'0050' ";
                if (chk0060.Checked) loc = loc + "'0060' ";
                if (chk0070.Checked) loc = loc + "'0070' ";
                if (chk0080.Checked) loc = loc + "'0080' ";
                if (chk0090.Checked) loc = loc + "'0090' ";
                if (chk2000.Checked) loc = loc + "'2000' ";
                if (chkskum.Checked) loc = loc + "'SKUM' ";
                if (chkskud.Checked) loc = loc + "'SKUD' ";
                if (chkskug.Checked) loc = loc + "'SKUG' ";
                if (chkskuf.Checked) loc = loc + "'SKUF' ";
                if (chkskuq.Checked) loc = loc + "'SKUQ' ";

                if (loc != "")
                {
                    if (loc.Length > 7)
                    {
                        loc = loc.Replace(' ', ',');
                        loc = loc.Substring(0, loc.Length - 1);
                    }

                    modstr = modstr + " and plti_loc in ( " + loc + " )";
                }
            }
            modstr = modstr + sqlgrp;

            modstr = modstr + "order by plti_pdesc , plti_loc , plti_lot , plti_bestq ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dataGridView1.DataSource = new SortableBindingList<mipltisum1>(db.ExecuteQuery<mipltisum1>(modstr).ToList());
                //var q = db.ExecuteQuery<mipltisum1>(modstr).ToList();
                //dataGridView1.DataSource = q;

                dataGridView1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

            //DataGridViewRow rr = new DataGridViewRow();
            //rr.Cells["plti_prod"].Value = "aaa23123a";
            //int r = dataGridView1.Rows.Add(rr);

        }  

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            long stoksum = 0, rqtysum = 0;
            decimal volsum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                stoksum = stoksum + Convert.ToInt32(dataGridView1.Rows[i].Cells["plti_stok"].Value.ToString());
                rqtysum = rqtysum + Convert.ToInt32(dataGridView1.Rows[i].Cells["plti_rqty"].Value.ToString());
                volsum = volsum + Convert.ToDecimal(dataGridView1.Rows[i].Cells["plti_volume"].Value.ToString());
            }
            lblstok.Text = string.Format("{0:n0}", stoksum);
            lblrqty.Text = string.Format("{0:n0}", rqtysum);
            lbltotal.Text = string.Format("{0:n3}", volsum);

            //SortableBindingList<mipltisum1> q = (SortableBindingList<mipltisum1>)dataGridView1.DataSource;
            //lblstok.Text  = q.Sum(x => x.plti_stok).ToString("###,##0");

        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tbprod.Text = dataGridView1.SelectedRows[0].Cells["plti_prod"].Value.ToString();
        }    

        private void tblot_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tblot.Text = dataGridView1.SelectedRows[0].Cells["plti_lot"].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dataGridView1.SelectedRows[0].Cells["plti_pdesc"].Value.ToString();
        }

        private void tblot2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            tblot2.Text = dataGridView1.SelectedRows[0].Cells["plti_lot"].Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tblot2.Enabled = checkBox1.Checked;
        }


        private void chk0010_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0010.Checked) chkall.Checked = false;
        }

        private void chk0035_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0035.Checked) chkall.Checked = false;
        }

        private void chk0050_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0050.Checked) chkall.Checked = false;
        }


        private void chk0070_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0070.Checked) chkall.Checked = false;
        }

        private void chk0080_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0080.Checked) chkall.Checked = false;
        }

        private void chk0090_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0090.Checked) chkall.Checked = false;
        }

        private void chk2000_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2000.Checked) chkall.Checked = false;
        }

        private void chkskum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkskum.Checked) chkall.Checked = false;
        }

        private void chkskud_CheckedChanged(object sender, EventArgs e)
        {
            if (chkskud.Checked) chkall.Checked = false;
        }

        private void chkskug_CheckedChanged(object sender, EventArgs e)
        {
            if (chkskug.Checked) chkall.Checked = false;
        }

        private void chkskuf_CheckedChanged(object sender, EventArgs e)
        {
            if (chkskuf.Checked) chkall.Checked = false;
        }

        private void chkskuq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkskuq.Checked) chkall.Checked = false;
        }
        private void chk0060_CheckedChanged(object sender, EventArgs e)
        {
            if (chk0060.Checked) chkall.Checked = false;
        }

        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked)
            {
                chk0010.Checked = false;
                chk0035.Checked = false;
                chk0050.Checked = false;
                chk0060.Checked = false;
                chk0070.Checked = false;
                chk0080.Checked = false;
                chk0090.Checked = false;
                chk2000.Checked = false;
                chkskum.Checked = false;
                chkskud.Checked = false;
                chkskug.Checked = false;
                chkskuf.Checked = false;
                chkskuq.Checked = false;                
            }
        }

    }
    public class mipltisum1
    {
        public string plti_prod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal plti_pksz { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public decimal plti_volume { get; set; }
    }
}
