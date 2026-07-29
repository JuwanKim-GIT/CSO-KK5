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
    public partial class FrmLoadcarAdd_p : Form
    {
        string parcel = "";
        public FrmLoadcarAdd_p(string parcel)
        {
            InitializeComponent();
            this.parcel = parcel;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
        }

        private void FrmLoadcarAdd_p_Load(object sender, EventArgs e)
        {

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<newtacarp>(@"select car_no, car_desc, car_man, load_vol, max_vol, priority, area_code, remark 
                                                     from tacar where step in ('1','2') and uuse = '1' and flag = '' and parcel = '" + parcel + "'").ToList();
                dataGridView1.DataSource = q;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
