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
    public partial class FrmRcpRsrvLstk_p : Form
    {
      
        private string atype;
        public FrmRcpRsrvLstk_p(string type)
        {
            InitializeComponent();
            atype = type;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            string hogi = (comboBox1.SelectedIndex + 1).ToString("0");

            
            string sql = @"select top 1 lstk_srch from milstk 
                            where lstk_hogi = {0}
                            and   lstk_io = '0'
                            and   lstk_stat = '00'
                            and   lstk_use = '1'
                            and   lstk_type = {1} order by lstk_srch ";

            DBDataContext db = new DBDataContext(Config.DBCon);
            string srch = db.ExecuteQuery<string>(sql, hogi, atype).SingleOrDefault();
            if (srch == null)
            {
                maskedTextBox1.Text = "000000";
                MessageBox.Show("빈셀이 없읍니다...!");
                return;
            }
            else
            {
                maskedTextBox1.Text = srch.Substring(4, 2) + srch.Substring(2, 2) + srch.Substring(0, 2);
            }
        }

        private void FrmRcpReassign_p_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (maskedTextBox1.Text == "000000") return;

            DialogResult = DialogResult.OK;
        }
    }
}
