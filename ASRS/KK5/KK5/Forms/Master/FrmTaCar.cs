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
    public partial class FrmTaCar : Form
    {
        #region --- MDI Child ----------------
        private static FrmTaCar _instance;
        public static FrmTaCar Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmTaCar();

                return _instance;
            }
        }
        private void FrmTaCar_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;
        string orgsel = "select car_no, car_desc, car_man, car_dest, max_vol, load_vol, load_qty, step, remark, uuse, area_code, parcel " +
                        " from tacar where car_no is not null ";
        public FrmTaCar()
        {
            InitializeComponent();
            dv = dataGridView1;
            dv.AutoGenerateColumns = false;
            dv.ReadOnly = true;
            dv.BackgroundColor = Color.FromKnownColor(KnownColor.Info);
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.MultiSelect = false;
            dv.CellFormatting += Dv_CellFormatting;
            this.FormClosed += FrmTaCar_FormClosed;

            if (Config.UserLevel != "1" && Config.UserLevel != "2")
            {
                btninsert.Enabled = false;
                btnedit.Enabled = false;
                btndel.Enabled = false;
            }
        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if(e.Value != null)
                {
                    if (e.Value.ToString() == "1") e.Value = ""; else e.Value = "사용중지";
                    e.FormattingApplied = true;
                }
            }else if(e.ColumnIndex == 10)
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "1") e.Value = "택배"; else e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        private void FrmTaCar_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = orgsel;

            string car = tbcar.Text.Trim();
            if (car != "") modstr = modstr + " and car_no like '" + car + "%'";
            modstr = modstr + " Order by car_no ";
            Cursor = Cursors.WaitCursor;
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                var q = db.ExecuteQuery<tacarq>(modstr).ToList();
                dataGridView1.DataSource = q;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            
            if (dv.SelectedRows.Count == 0) return;
            if (MessageBox.Show("삭제 하시겠읍니끼?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string car_no = dv.SelectedRows[0].Cells["car_no"].Value.ToString();
            
            string sql = " delete from tacar where car_no = '" + car_no + "'";
            int r = 0;
            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                r = db.ExecuteCommand(sql);
                db.SubmitChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("삭제 완료...!" + r.ToString());
            retrieve();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            FrmTacarInsert_p p = new FrmTacarInsert_p();
            p.ShowDialog();
            p.Dispose();
            retrieve();

        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dv.SelectedRows.Count == 0) return;
            tacarq t = new tacarq();
            t.car_no = dv.SelectedRows[0].Cells["car_no"].Value.ToString();
            t.car_desc = dv.SelectedRows[0].Cells["car_desc"].Value.ToString();
            t.car_man = dv.SelectedRows[0].Cells["car_man"].Value.ToString();
            t.car_dest = dv.SelectedRows[0].Cells["car_dest"].Value.ToString();
            t.max_vol = Convert.ToDecimal(dv.SelectedRows[0].Cells["max_vol"].Value.ToString());
            t.remark = dv.SelectedRows[0].Cells["remark"].Value.ToString();
            t.uuse = dv.SelectedRows[0].Cells["uuse"].Value.ToString();
            t.area_code = dv.SelectedRows[0].Cells["area_code"].Value.ToString();
            t.parcel = dv.SelectedRows[0].Cells["parcel"].Value.ToString();

            FrmTacarEdit_p p = new FrmTacarEdit_p(t);
            p.ShowDialog();
            p.Dispose();
            retrieve();
        }
    }
    public class tacarq
    {
        public string car_no { get; set; }
        public string car_desc { get; set; }
        public string car_man { get; set; }
        public string car_dest { get; set; }
        public decimal max_vol { get; set; }
        public decimal load_vol { get; set; }
        public decimal load_qty { get; set; }
        public string step { get; set; }
        public string remark { get; set; }
        public string uuse { get; set; }
        public string area_code { get; set; }
        public string parcel { get; set; }
    }
}
