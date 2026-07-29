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
    public partial class FrmMidest : Form
    {
        #region --- MDI Child ----------------
        private static FrmMidest _instance;
        public static FrmMidest Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMidest();

                return _instance;
            }
        }
        private void FrmMidest_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string orgsel = " select arrival, area_code from midest where arrival is not null ";
        public FrmMidest()
        {
            InitializeComponent();
            
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.FormClosed += FrmMidest_FormClosed;
        }
        private void FrmMidest_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void retrieve()
        {
            string modstr = orgsel;

       
            string arrival = tbarrival.Text.Trim();
            if (arrival != "") modstr = modstr + " and arrival like '" + arrival + "%'";
            modstr = modstr + " Order by arrival ";
            Cursor = Cursors.WaitCursor;
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                var q = db.ExecuteQuery<midestq>(modstr).ToList();
                dataGridView1.DataSource = q;
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

    public class midestq
    {
        public string arrival { get; set; }
        public string area_code { get; set; }
    }
}
