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
    public partial class FrmNewCarSel_p : Form
    {
        string parcel = "";
        public FrmNewCarSel_p(string parcel)
        {
            InitializeComponent();
            this.parcel = parcel;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
        }
        public FrmNewCarSel_p()
        {
            InitializeComponent();            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            DialogResult = DialogResult.OK;
        }

        private void FrmNewCarSel_p_Load(object sender, EventArgs e)
        {
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<newtacarp>(@"select car_no, car_desc, car_man, max_vol, priority, area_code, remark 
                                                     from tacar where step in( '0', '') and uuse = '1' and parcel = '" + parcel + "'").ToList();
                
                dataGridView1.DataSource = q;
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            DialogResult = DialogResult.OK;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class newtacarp
    {
        public string car_no { get; set; }
        public string car_desc { get; set; }
        public string car_man { get; set; }
        public decimal load_vol { get; set; }
        public decimal max_vol { get; set; }
        public int priority { get; set; }
        public string area_code { get; set; }
        public string remark { get; set; }
    }


}
