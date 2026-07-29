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
    public partial class FrmTacarInsert_p : Form
    {
        public FrmTacarInsert_p()
        {
            InitializeComponent();
        }

        private void FrmTacarInsert_p_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string car_no = tbcd.Text.Trim();
            string car_desc = tbdesc.Text.Trim();
            string car_man = tbman.Text.Trim();
            string car_dest = tbdest.Text.Trim();
            decimal max_vol = numaxvol.Value;
            string remark = tbremark.Text.Trim();

            string uuse = "0";
            if (chkuse.Checked) uuse = "1";
            else uuse = "0";

            string area_code = tbarea.Text.Trim();

            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            else parcel = "";

            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                string sql = @" insert into tacar (car_no, car_desc, car_man, car_dest, max_vol, remark, uuse, area_code, parcel, hdate, htime) 
                                    values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} ) ";
                db.ExecuteCommand(sql, car_no, car_desc, car_man, car_dest, max_vol, remark, uuse, area_code, parcel, "", "");
                db.SubmitChanges();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }

            MessageBox.Show("등록 성공...!");
           
        }
    }
}
