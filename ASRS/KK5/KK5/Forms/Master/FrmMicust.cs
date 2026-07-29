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
    public partial class FrmMicust : Form
    {
        #region --- MDI Child ----------------
        private static FrmMicust _instance;
        public static FrmMicust Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMicust();

                return _instance;
            }
        }
        private void FrmMicust_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string orgsel = "select cust_cd, cust_desc from micust where cust_cd is not null ";

        public FrmMicust()
        {
            InitializeComponent();
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.FormClosed += FrmMicust_FormClosed;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrimMicust_Load(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = orgsel;

            string cust = tbcust.Text.Trim();
            if (cust != "") modstr = modstr + " and cust_cd like '" + cust + "%'";

            string desc = tbdesc.Text.Trim();
            if (desc != "") modstr = modstr + " and cust_desc like '%" + desc + "%'";
            modstr = modstr + " Order by cust_cd ";
            Cursor =  Cursors.WaitCursor;
            try
            {               
                DBDataContext db = new DBDataContext(Config.DBCon);
                var q = db.ExecuteQuery<micustq>(modstr).ToList();
                dv.DataSource = q;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }
    }
    public class micustq
    {
        public string cust_cd { get; set; }
        public string cust_desc { get; set; }
    }
}
