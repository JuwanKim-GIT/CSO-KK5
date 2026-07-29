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
    public partial class FrmAdvMiplti : Form
    {
        #region --- MDI Child ----------------
        private static FrmAdvMiplti _instance;
        public static FrmAdvMiplti Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmAdvMiplti();

                return _instance;
            }
        }
        private void FrmAdvMiplti_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        decimal pksz = 0;
        string oprod = "";
        public FrmAdvMiplti()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 8;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            FormClosed += FrmAdvMiplti_FormClosed;
        }

        private void FrmAdvMiplti_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (FrmMastSel_p p = new FrmMastSel_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;

                if (p.dataGridView1.SelectedRows.Count <= 0) return;
                tbprod.Text = p.dataGridView1.SelectedRows[0].Cells["mast_cd"].Value.ToString();
                tbpdesc.Text = p.dataGridView1.SelectedRows[0].Cells["mast_desc"].Value.ToString();          

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string prod = tbprod.Text;
            string pdesc = tbpdesc.Text;
            if (pdesc == "") return;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery("Select top 1 mast_cd, mast_desc, mast_old, mast_vol from mimast where mast_cd = '" + prod + "'").SingleOrDefault();
                if (q == null)
                {
                    tbpdesc.Text = "";
                    pdesc = "";
                    MessageBox.Show("mast code Not Found...!");
                    return;
                }

                tbpdesc.Text = q.mast_desc;
                prod = q.mast_cd;
                pdesc = q.mast_desc;

                pksz = q.mast_vol;
                oprod = ""; 
                string loc = comboBox1.SelectedItem.ToString().Substring(0, 4);
                string lot = tblot.Text;
                if (lot == "")
                {
                    MessageBox.Show("Lot missing...!");
                    return;
                }
                string remark = tbremark.Text;
                if (remark.Length > 40) remark.Substring(0, 40);

                string place = comboBox2.SelectedItem.ToString().Substring(0,1);
                decimal stok = numericUpDown1.Value;
                if (stok <= 0) return;

                string lstk = "F000000";
                if (place == "F") lstk = "F000000";
                if (place == "Y") lstk = "Y000000";

                string bestq = comboBox3.SelectedItem.ToString();

                string ls = "";
                db.p_curgetdatetime19(ref ls);
                string idate = ls.Substring(0, 10);
                string itime = ls.Substring(11, 8);
                int rc = 0;
                int st = 0;
                string sqlupd = string.Empty;

                db.Connection.open();

                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        sqlupd = @"update miplti set plti_stok = plti_stok + {0} 
                                 where plti_pltno = '00000000' and plti_lstk = {1} and plti_prod = {2} and plti_loc = {3} and plti_lot = {4} and plti_bestq = {5} ";
                        rc = db.ExecuteCommand(sqlupd, stok, lstk, prod, loc, lot, bestq);
                        if (rc == 0)
                        {
                            string sqlins = @"insert into miplti
                                                    ( plti_pltno, plti_lstk,   plti_prod,  plti_loc,     plti_lot,       plti_bestq, 
                                                    plti_pksz,  plti_stok,   plti_rqty,  plti_icust,   plti_cycl_date, plti_idate,
                                                    plti_itime, plti_pdesc,  plti_oprod, plti_flag,    plti_label,     plti_remark )
                                            values ( '00000000', {0},         {1},        {2},          {3},            {4},
                                                    {5},        {6},         0,           '',          {7},            {8},
                                                    {9},        {10},        {11},       '1',          '0',            {12} ) ";
                            rc = db.ExecuteCommand(sqlins, lstk, prod, loc, lot, bestq, pksz, stok, idate, idate, itime, pdesc, oprod, remark);
                            if (rc > 0) db.Transaction.Commit();
                            else db.Transaction.Rollback();
                        }
                        else
                        {
                            st = 1;
                            db.Transaction.Commit();
                        }
                    }
                    catch (Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
                if (rc == 0) MessageBox.Show("재고 등록 실패...!");
                if ( rc > 0) MessageBox.Show("재고 등록 성공...!");
                
            }
        }
    }
}
