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
    public partial class FrmBankDisp : Form
    {
        #region --- MDI Child ----------------
        private static FrmBankDisp _instance;
        public static FrmBankDisp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmBankDisp();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        DataGridView dv1, dv2;
        private void FrmBankDisp_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        public FrmBankDisp()
        {
            InitializeComponent();
            FormClosed += FrmBankDisp_FormClosed;
            comboBox1.SelectedIndex = 0;
        }

        private void FrmBankDisp_Load(object sender, EventArgs e)
        {
            drawaxisframe();
            refreshview();
        }
       
      
        List<PictureBox> lbcells = new List<PictureBox>();
        int leftpos = 59;
        int toppos = 463;

        int leftadd = 25;
        int topadd = 27;
        int k = 0;
        private void drawaxisframe()
        {
            int w = 37;
            for (int i = 1; i <= 36; i++)
            {
                w = w + 25;
                for(int j = 1; j<= 11; j++)
                {
                    if (i == 1)
                    {
                        if (j < 4) continue;
                    }

                    PictureBox pic = new PictureBox();
                    pic.Image = Properties.Resources.empty;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Size = new Size(22, 25);

                    lbcells.Add(pic);
                    pic.Parent = this;
                    this.Controls.Add(pic);

                    pic.Top = 490 - (j * 28);
                    pic.Left = w;
                    pic.Refresh();
                    pic.BringToFront();
                    k++;
                }
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshview()
        {
            string bk = comboBox1.SelectedItem.ToString();
            DBDataContext db = new DBDataContext(Config.DBCon);

            string uuse="", stat = "";
            int i = 0;
            var q = db.ExecuteQuery(@"select lstk_use, lstk_stat from milstk 
                                      where substring(lstk_no,1,1) = 'A' and lstk_bk = '" + bk + "' order by lstk_no").ToList();
            
            foreach(var s in q)
            {
                uuse = s.lstk_use;
                stat = s.lstk_stat;
                if (bk == "02" || bk == "03" || bk == "04" || bk == "05")
                {
                    if (i == 8)
                    {
                        lbcells[8].Visible = false;
                        lbcells[9].Visible = false;
                        lbcells[10].Visible = false;
                        i = i + 3;
                    }                                            
                }
                lbcells[i].Visible = true;

                if (uuse == "1")
                {
                    if (stat == "10") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "$R") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "$X") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "$Z") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "IR") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "IX") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "IZ") lbcells[i].Image = Properties.Resources.plt;
                    else if (stat == "$E") lbcells[i].Image = Properties.Resources.emptyout;
                    else if (stat == "ID") lbcells[i].Image = Properties.Resources._double;
                    else lbcells[i].Image = Properties.Resources.empty;
                }
                else
                {
                    lbcells[i].Image = Properties.Resources.use;
                }
                lbcells[i].Refresh(); lbcells[i].BringToFront();

                i++;                         
              
            }
        }
    }
}
