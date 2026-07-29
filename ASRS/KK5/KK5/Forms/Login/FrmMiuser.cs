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
    public partial class FrmMiuser : Form
    {
        #region --- MDI Child ----------------
        private static FrmMiuser _instance;
        public static FrmMiuser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMiuser();

                return _instance;
            }
        }
        private void FrmMiuser_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        public FrmMiuser()
        {
            InitializeComponent();
            FormClosed += FrmMiuser_FormClosed;
            dv = dataGridView1;
            dv.ReadOnly = true;
            dv.AutoGenerateColumns = false;
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dv.CellFormatting += Dv_CellFormatting;
        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                if (e.Value != null)
                {
                    switch (e.Value.ToString())
                    {
                        case "": e.Value = "조회";
                            break;
                        case "1":
                            e.Value = "관리자";
                            break;
                        case "2":
                            e.Value = "상차관리자";
                            break;
                        case "3":
                            e.Value = "공장입고자";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void FrmMiuser_Load(object sender, EventArgs e)
        {
            retrieve();
            if (Common.role != "1") btnedit.Enabled = false;
        }
        string sql = @"select * from miuser where userid is not null";
        private void retrieve()
        {
            string modstr = sql;

            string userid = tbuser.Text.Trim();
            if (userid != "") { modstr = modstr + " and userid like '" + userid + "%'"; }
        
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<miuserq>(modstr).ToList();
                dv.DataSource = q;
            }
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;

            string userid = dv.SelectedRows[0].Cells["userid"].Value.ToString();
            string username = dv.SelectedRows[0].Cells["username"].Value.ToString();
            string role = dv.SelectedRows[0].Cells["role"].Value.ToString();

            using (FrmMiuserEdit_p p = new FrmMiuserEdit_p(userid, username, role))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
            }
            retrieve();          
        }
    }
    public class miuserq
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string passwd { get; set; }
        public string role { get; set; }
        public DateTime? credt { get; set; }
    }
}
