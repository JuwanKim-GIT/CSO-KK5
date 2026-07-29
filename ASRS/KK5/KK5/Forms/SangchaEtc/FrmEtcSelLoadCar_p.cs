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
    public partial class FrmEtcSelLoadCar_p : Form
    {
        public FrmEtcSelLoadCar_p()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DialogResult = DialogResult.OK;
        }

        private void FrmEtcSelLoadCar_p_Load(object sender, EventArgs e)
        {
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<newtacarp>(@"select car_no, car_desc, car_man, load_vol, max_vol, priority, area_code, remark 
                                                 from tacar where step = '1' and uuse = '1' and flag = '1' " ).ToList();
                dataGridView1.DataSource = q;
            }
        }
    }
}
