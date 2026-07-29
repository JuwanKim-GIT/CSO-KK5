using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;

namespace KK5
{
    public partial class FrmLabelprn : Form
    {
        #region --- MDI Child ----------------
        private static FrmLabelprn _instance;
        public static FrmLabelprn Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLabelprn();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmLabelprn_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string modstr = string.Empty;

        #region Query ---------------------------------------
        string qsql = "SELECT  " +
                          " milstk.lstk_no , " +
                          " milstk.lstk_use , " +
                          " milstk.lstk_io , " +
                          " milstk.lstk_stat ," +
                          " miplti.plti_pltno ," +
                          " miplti.plti_prod ," +
                          " miplti.plti_oprod ," +
                          " miplti.plti_pdesc ," +
                          " miplti.plti_loc ," +
                          " miplti.plti_lot ," +
                          " miplti.plti_bestq ," +
                          " miplti.plti_pksz ," +
                          " miplti.plti_remark ," +
                          " miplti.plti_stok ," +
                          " miplti.plti_rqty ," +
                          " miplti.plti_idate ," +
                          " miplti.plti_itime ," +
                          " miplti.plti_flag " +
                          " FROM milstk ,  miplti  where lstk_no = plti_lstk ";

        #endregion

        public FrmLabelprn()
        {
            InitializeComponent();
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.FormClosed += FrmLabelprn_FormClosed;
            dv.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 0;

            if (Config.UserLevel != "1") btnlabel.Enabled = false;
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            query();
        }

        private void FrmLabelprn_Load(object sender, EventArgs e)
        {        

            comboBox1.Text = "ALL";
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.Columns["plti_stok"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dv.Columns["plti_rqty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Sort(plti_pltno, ListSortDirection.Ascending);
        }
        private void query()
        {
            modstr = qsql;

            string ls = comboBox1.Text;

            if (ls != "ALL")
            {
                if (ls.Substring(0, 1) == "A") modstr = modstr + " and lstk_no like 'A%' ";
                if (ls.Substring(0, 1) == "F") modstr = modstr + " and lstk_no like 'F%' ";
                if (ls.Substring(0, 1) == "Y") modstr = modstr + " and lstk_no like 'Y%' ";
            }

            string ls_m1 = tbProd.Text.Trim();
            if (ls_m1 != "") modstr = modstr + " and plti_prod like '" + ls_m1 + "%'";

            string pdesc = txtpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and plti_pdesc like '%" + pdesc + "%'";

            string ls_lot1 = tbLot.Text.Trim();
            if (ls_lot1 != "") modstr = modstr + " and plti_lot like '" + ls_lot1 + "%'";

            string bestq = comboBox2.SelectedItem.ToString().Substring(0,1);
            if (bestq != "A") modstr = modstr + " and plti_bestq = '" + bestq + "'";

            string loc = tbLoc.Text.Trim();
            if (loc != "") modstr = modstr + " and plti_loc like '" + loc + "%'";

            string plt = tbPlt.Text.Trim();
            if (plt != "") modstr = modstr + " and plti_pltno like '" + plt + "%'";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv.DataSource = new SortableBindingList<ItemLstk>(db.ExecuteQuery<ItemLstk>(modstr).ToList());
                //var ss = db.ExecuteQuery<ItemLstk>(modstr).ToList();
                //dv.DataSource = ss;
            }            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlabel_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;

            string pltno = "";
            string ppltno = "p";
            string prod = "", pdesc = "", lot = "";
            decimal pksz = 0;
            string msg = "";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int lp = 0;
                foreach (DataGridViewRow r in dv.SelectedRows)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    prod = r.Cells["plti_prod"].Value.ToString();
                    pdesc = r.Cells["plti_pdesc"].Value.ToString();
                    lot = r.Cells["plti_lot"].Value.ToString().Trim();
                    pksz = Convert.ToDecimal(r.Cells["plti_pksz"].Value.ToString());

                    if (pltno == "00000000") continue;
                    if (pltno == ppltno) continue;

                    int pltcnt = db.ExecuteQuery<int>("select count(*) from miplti where plti_pltno = '" + pltno + "'").SingleOrDefault();
                    if (pltcnt == 0)
                    {
                        msg = "파렛번호:[" + pltno + "] 가 없읍니다..!";
                        break;
                    }
                    decimal stokqty = db.ExecuteQuery<decimal>("select sum(plti_stok) from miplti where plti_pltno = '" + pltno + "'").SingleOrDefault();

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            int rc = db.ExecuteCommand("update miplti set plti_label = '1' where plti_pltno = '" + pltno + "'");

                            string sql = @" INSERT INTO tbbprn 
                                                   ( prn_no, prn_pltno, prn_prod, prn_pdesc, prn_lot, prn_pksz, prn_qty, prn_mixcnt)
                                            values ( {0},    {1}, {2}, {3}, {4}, {5}, {6}, {7} ) ";

                            if (pltcnt == 1)
                                db.ExecuteCommand(sql, '1', pltno, prod, pdesc, lot, pksz, stokqty, pltcnt);
                            else
                                db.ExecuteCommand(sql, '1', pltno, "", "", "", 0.000, stokqty, pltcnt);

                            db.SubmitChanges();
                            scope.Complete();
                            lp++;
                        }

                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        return;
                    }
                }
                MessageBox.Show(lp.ToString() + " 개의 라벨발행 명령 성공...!");
            } //using db
        }

        private void tbProd_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbProd.Text = dv.SelectedRows[0].Cells["plti_prod"].Value.ToString();
        }

        private void tbLoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbLoc.Text = dv.SelectedRows[0].Cells["plti_loc"].Value.ToString();
        }

        private void tbLot_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbLot.Text = dv.SelectedRows[0].Cells["plti_lot"].Value.ToString();
        }

        private void tbPlt_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            tbPlt.Text = dv.SelectedRows[0].Cells["plti_pltno"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count <= 0) return;
            txtpdesc.Text = dv.SelectedRows[0].Cells["plti_pdesc"].Value.ToString();
        }
    }
}
