using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;

namespace MCP
{
    public partial class FrmSCContol_p : Form
    {
        public FrmSCContol_p()
        {
            InitializeComponent();
        }

        private void FrmSCContol_p_Load(object sender, EventArgs e)
        {
            string ls_hogi = "";
            string ls_stop = "";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {

                ls_stop = d.ExecuteQuery<string>(@"Select scrc_stop from tbscrc where scrc_no = '01'").SingleOrDefault();
                if (ls_stop == "0") checkBox1.Checked = true; else checkBox2.Checked = false;

                ls_stop = d.ExecuteQuery<string>(@"Select scrc_stop from tbscrc where scrc_no = '02'").SingleOrDefault();
                if (ls_stop == "0") checkBox3.Checked = true; else checkBox4.Checked = true;

                ls_stop = d.ExecuteQuery<string>(@"Select scrc_stop from tbscrc where scrc_no = '03'").SingleOrDefault();
                if (ls_stop == "0") checkBox5.Checked = true; else checkBox6.Checked = true;

                ls_stop = d.ExecuteQuery<string>(@"Select scrc_stop from tbscrc where scrc_no = '04'").SingleOrDefault();
                if (ls_stop == "0") checkBox7.Checked = true; else checkBox8.Checked = true;

                ls_stop = d.ExecuteQuery<string>(@"Select scrc_stop from tbscrc where scrc_no = '05'").SingleOrDefault();
                if (ls_stop == "0") checkBox9.Checked = true; else checkBox10.Checked = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] stop = new string[5];
           
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                if (checkBox1.Checked) stop[0] = "0";
                else stop[0] = "1";
                d.ExecuteCommand(@"update tbscrc set scrc_stop = {0} where scrc_no = '01'", stop[0]);
          
                if (checkBox3.Checked) stop[1] = "0";
                else stop[1] = "1";
                d.ExecuteCommand(@"update tbscrc set scrc_stop = {0} where scrc_no = '02'", stop[1]);

                if (checkBox5.Checked) stop[2] = "0";
                else stop[2] = "1";
                d.ExecuteCommand(@"update tbscrc set scrc_stop = {0} where scrc_no = '03'", stop[2]);

                if (checkBox7.Checked) stop[3] = "0";
                else stop[3] = "1";
                d.ExecuteCommand(@"update tbscrc set scrc_stop = {0} where scrc_no = '04'", stop[3]);

                if (checkBox9.Checked) stop[4] = "0";
                else stop[4] = "1";
                d.ExecuteCommand(@"update tbscrc set scrc_stop = {0} where scrc_no = '05'", stop[4]);

            }
           
            DialogResult = DialogResult.OK;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) checkBox6.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) checkBox1.Checked = false;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) checkBox4.Checked = false;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) checkBox3.Checked = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) checkBox5.Checked = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) checkBox8.Checked = false;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) checkBox7.Checked = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) checkBox10.Checked = false;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) checkBox9.Checked = false;
        }
    }
}
