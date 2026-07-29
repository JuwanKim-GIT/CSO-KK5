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
    public partial class FrmLinkTable : Form
    {
        #region --- MDI Child ----------------
        private static FrmLinkTable _instance;
        public static FrmLinkTable Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLinkTable();
                else
                    _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmLinkTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

   
        public FrmLinkTable()
        {
            InitializeComponent();
            this.FormClosed += FrmLinkTable_FormClosed;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void query()
        {
            DBDataContext db = new DBDataContext(Config.DBCon);
            var q = db.ExecuteQuery("Select cnvc_ch01, cnvc_ch02, cnvc_ch03, cnvc_ch04, cnvc_ch05, cnvc_ch06 from tbcnvc where cnvc_mode = '01'").Single();
            string cnvc_ch01 = q.cnvc_ch01;
            string cnvc_ch02 = q.cnvc_ch02;
            string cnvc_ch03 = q.cnvc_ch03;
            string cnvc_ch04 = q.cnvc_ch04;
            string cnvc_ch05 = q.cnvc_ch05;
            string cnvc_ch06 = q.cnvc_ch06;

            char[] ch01 = cnvc_ch01.ToCharArray();
            char[] ch02 = cnvc_ch02.ToCharArray();
            char[] ch03 = cnvc_ch03.ToCharArray();
            char[] ch04 = cnvc_ch04.ToCharArray();
            char[] ch05 = cnvc_ch05.ToCharArray();
            char[] ch06 = cnvc_ch06.ToCharArray();

            if (ch01[0] == '1') c101.Active = true; else c101.Active = false;
            if (ch01[1] == '1') c102.Active = true; else c102.Active = false;
            if (ch01[2] == '1') c103.Active = true; else c103.Active = false;
            if (ch01[3] == '1') c104.Active = true; else c104.Active = false;
            if (ch01[4] == '1') c105.Active = true; else c105.Active = false;
            if (ch01[5] == '1') c106.Active = true; else c106.Active = false;
            if (ch01[6] == '1') c107.Active = true; else c107.Active = false;
            if (ch01[7] == '1') c108.Active = true; else c108.Active = false;
            if (ch01[8] == '1') c109.Active = true; else c109.Active = false;
            if (ch01[9] == '1') c110.Active = true; else c110.Active = false;
            if (ch01[10] == '1') c111.Active = true; else c111.Active = false;
            if (ch01[11] == '1') c112.Active = true; else c112.Active = false;
            if (ch01[12] == '1') c113.Active = true; else c113.Active = false;
            if (ch01[13] == '1') c114.Active = true; else c114.Active = false;
            if (ch01[14] == '1') c115.Active = true; else c115.Active = false;
            if (ch01[15] == '1') c116.Active = true; else c116.Active = false;

            if (ch02[0] == '1') c201.Active = true; else c201.Active = false;
            if (ch02[1] == '1') c202.Active = true; else c202.Active = false;
            if (ch02[2] == '1') c203.Active = true; else c203.Active = false;
            if (ch02[3] == '1') c204.Active = true; else c204.Active = false;
            if (ch02[4] == '1') c205.Active = true; else c205.Active = false;
            if (ch02[5] == '1') c206.Active = true; else c206.Active = false;
            if (ch02[6] == '1') c207.Active = true; else c207.Active = false;
            if (ch02[7] == '1') c208.Active = true; else c208.Active = false;
            if (ch02[8] == '1') c209.Active = true; else c209.Active = false;
            if (ch02[9] == '1') c210.Active = true; else c210.Active = false;
            if (ch02[10] == '1') c211.Active = true; else c211.Active = false;
            if (ch02[11] == '1') c212.Active = true; else c212.Active = false;
            if (ch02[12] == '1') c213.Active = true; else c213.Active = false;
            if (ch02[13] == '1') c214.Active = true; else c214.Active = false;
            if (ch02[14] == '1') c215.Active = true; else c215.Active = false;
            if (ch02[15] == '1') c216.Active = true; else c216.Active = false;

            if (ch03[0] == '1') c301.Active = true; else c301.Active = false;
            if (ch03[1] == '1') c302.Active = true; else c302.Active = false;
            if (ch03[2] == '1') c303.Active = true; else c303.Active = false;
            if (ch03[3] == '1') c304.Active = true; else c304.Active = false;
            if (ch03[4] == '1') c305.Active = true; else c305.Active = false;
            if (ch03[5] == '1') c306.Active = true; else c306.Active = false;
            if (ch03[6] == '1') c307.Active = true; else c307.Active = false;
            if (ch03[7] == '1') c308.Active = true; else c308.Active = false;
            if (ch03[8] == '1') c309.Active = true; else c309.Active = false;
            if (ch03[9] == '1') c310.Active = true; else c310.Active = false;
            if (ch03[10] == '1') c311.Active = true; else c311.Active = false;
            if (ch03[11] == '1') c312.Active = true; else c312.Active = false;
            if (ch03[12] == '1') c313.Active = true; else c313.Active = false;
            if (ch03[13] == '1') c314.Active = true; else c314.Active = false;
            if (ch03[14] == '1') c315.Active = true; else c315.Active = false;
            if (ch03[15] == '1') c316.Active = true; else c316.Active = false;


            if (ch04[0] == '1') c401.Active = true; else c401.Active = false;
            if (ch04[1] == '1') c402.Active = true; else c402.Active = false;
            if (ch04[2] == '1') c403.Active = true; else c403.Active = false;
            if (ch04[3] == '1') c404.Active = true; else c404.Active = false;
            if (ch04[4] == '1') c405.Active = true; else c405.Active = false;
            if (ch04[5] == '1') c406.Active = true; else c406.Active = false;
            if (ch04[6] == '1') c407.Active = true; else c407.Active = false;
            if (ch04[7] == '1') c408.Active = true; else c408.Active = false;
            if (ch04[8] == '1') c409.Active = true; else c409.Active = false;
            if (ch04[9] == '1') c410.Active = true; else c410.Active = false;
            if (ch04[10] == '1') c411.Active = true; else c411.Active = false;
            if (ch04[11] == '1') c412.Active = true; else c412.Active = false;
            if (ch04[12] == '1') c413.Active = true; else c413.Active = false;
            if (ch04[13] == '1') c414.Active = true; else c414.Active = false;
            if (ch04[14] == '1') c415.Active = true; else c415.Active = false;
            if (ch04[15] == '1') c416.Active = true; else c416.Active = false;

            if (ch05[0] == '1') c501.Active = true; else c501.Active = false;
            if (ch05[1] == '1') c502.Active = true; else c502.Active = false;
            if (ch05[2] == '1') c503.Active = true; else c503.Active = false;
            if (ch05[3] == '1') c504.Active = true; else c504.Active = false;
            if (ch05[4] == '1') c505.Active = true; else c505.Active = false;
            if (ch05[5] == '1') c506.Active = true; else c506.Active = false;
            if (ch05[6] == '1') c507.Active = true; else c507.Active = false;
            if (ch05[7] == '1') c508.Active = true; else c508.Active = false;
            if (ch05[8] == '1') c509.Active = true; else c509.Active = false;
            if (ch05[9] == '1') c510.Active = true; else c510.Active = false;
            if (ch05[10] == '1') c511.Active = true; else c511.Active = false;
            if (ch05[11] == '1') c512.Active = true; else c512.Active = false;
            if (ch05[12] == '1') c513.Active = true; else c513.Active = false;
            if (ch05[13] == '1') c514.Active = true; else c514.Active = false;
            if (ch05[14] == '1') c515.Active = true; else c515.Active = false;
            if (ch05[15] == '1') c516.Active = true; else c516.Active = false;

            if (ch06[0] == '1') c601.Active = true; else c601.Active = false;
            if (ch06[1] == '1') c602.Active = true; else c602.Active = false;
            if (ch06[2] == '1') c603.Active = true; else c603.Active = false;
            if (ch06[3] == '1') c604.Active = true; else c604.Active = false;
            if (ch06[4] == '1') c605.Active = true; else c605.Active = false;
            if (ch06[5] == '1') c606.Active = true; else c606.Active = false;
            if (ch06[6] == '1') c607.Active = true; else c607.Active = false;
            if (ch06[7] == '1') c608.Active = true; else c608.Active = false;
            if (ch06[8] == '1') c609.Active = true; else c609.Active = false;
            if (ch06[9] == '1') c610.Active = true; else c610.Active = false;
            if (ch06[10] == '1') c611.Active = true; else c611.Active = false;
            if (ch06[11] == '1') c612.Active = true; else c612.Active = false;
            if (ch06[12] == '1') c613.Active = true; else c613.Active = false;
            if (ch06[13] == '1') c614.Active = true; else c614.Active = false;
            if (ch06[14] == '1') c615.Active = true; else c615.Active = false;
            if (ch06[15] == '1') c616.Active = true; else c616.Active = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            query();
        }

        private void FrmLinkTable_Load(object sender, EventArgs e)
        {
            query();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string aa = "213";
            char[] cc = new char[3];

            DBDataContext db = new DBDataContext(Config.DBCon);
            var q = db.ExecuteQuery< testblss>("Select id, a, b from testbl");

            foreach(var s in q)
            {
                //string a = s.a.ToString();
               // if (s.a == null) MessageBox.Show("ss");
                string b = s.b.ToString();                
            }

        }
    }
    public class testblss
    {
        public int id { get; set; }
  
        public string a { get; set; }

        public char[] b { get; set; }

    }
}
