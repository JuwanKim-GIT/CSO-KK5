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
    public partial class FrmMiarea : Form
    {
        #region --- MDI Child ----------------
        private static FrmMiarea _instance;
        public static FrmMiarea Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMiarea();

                return _instance;
            }
        }
        private void FrmMiarea_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        string orgsel = "select area_code, area_name from miarea where area_code is not null ";
        public FrmMiarea()
        {
            InitializeComponent();
            this.FormClosed += FrmMiarea_FormClosed;
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        public void retrieve()
        {
            string modstr = orgsel;

            string area = tbarea.Text.Trim();
            if (area != "") modstr = modstr + " and area_code like '" + area + "%'";
            modstr = modstr + " Order by area_code ";
            Cursor = Cursors.WaitCursor;
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                var q = db.ExecuteQuery<miareaq>(modstr).ToList();
                dataGridView1.DataSource = q;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FrmMiarea_Load(object sender, EventArgs e)
        {
            retrieve();
            
        }
    }
    public class miareaq
    {
        public string area_code { get; set; }
        public string area_name { get; set; }

    }
}
