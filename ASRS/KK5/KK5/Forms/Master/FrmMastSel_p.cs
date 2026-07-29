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
    public partial class FrmMastSel_p : Form
    {
        string orgsel = "select mast_cd, mast_desc, mast_type, mast_grp, mast_old, mast_bunit, mast_szdm, " +
                        " mast_gwgt, mast_nwgt, mast_wunit, mast_vol, mast_vunit, mast_date, mast_time, mast_flag, mast_desc1, mast_canqty " +
                        " from mimast where mast_cd is not null ";

        public FrmMastSel_p()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            string modstr = orgsel;

            string mast = tbmast.Text.Trim();
            if (mast != "") modstr = modstr + " and mast_cd like '" + mast + "%'";

            string desc = tbdesc.Text.Trim();
            if (desc != "") modstr = modstr + " and mast_desc like '%" + desc + "%'";
            modstr = modstr + " Order by mast_cd ";
            Cursor = Cursors.WaitCursor;
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                dataGridView1.DataSource = new SortableBindingList<mimastq>(db.ExecuteQuery<mimastq>(modstr).ToList());
                //var q = db.ExecuteQuery<mimastq>(modstr).ToList();
                //dv.DataSource = q;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnsel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FrmMastSel_p_Load(object sender, EventArgs e)
        {

        }
    }
}
