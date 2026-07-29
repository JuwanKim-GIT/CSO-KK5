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
    public partial class FrmTacarEdit_p : Form
    {
        tacarq t;
        public FrmTacarEdit_p(tacarq t)
        {
            InitializeComponent();
            this.t = t;
        }

        private void FrmTacarEdit_p_Load(object sender, EventArgs e)
        {
            tbcd.Text = t.car_no;
            tbdesc.Text = t.car_desc;
            tbman.Text = t.car_man;
            tbdest.Text = t.car_dest;
            numaxvol.Value = t.max_vol;
            tbremark.Text = t.remark;
            if (t.uuse == "1") chkuse.Checked = true; else chkuse.Checked = false;
            if (t.parcel == "1") chkparcel.Checked = true; else chkparcel.Checked = false;
            tbarea.Text = t.area_code;           

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
                string sql = @" update tacar set car_desc = {0}, car_man = {1}, car_dest = {2}, 
                                                 max_vol = {3}, remark = {4}, uuse = {5}, area_code = {6}, parcel = {7}
                                where car_no = {8} ";
                 
                
                db.ExecuteCommand(sql, car_desc, car_man, car_dest, max_vol, remark, uuse, area_code, parcel, car_no);
                db.SubmitChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }

            MessageBox.Show("수정 성공...!");

            DialogResult = DialogResult.OK;
        }
    }
}
