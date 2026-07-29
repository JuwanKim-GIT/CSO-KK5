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
    public partial class FrmMimast_add_p : Form
    {
        public FrmMimast_add_p()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mast_cd = tbcd.Text.Trim();
            string mast_desc = tbdesc.Text.Trim();
            string mast_type = tbtype.Text.Trim();
            string mast_grp = tbgrp.Text.Trim();
            string mast_old = tbold.Text.Trim();
            string mast_bunit = tbbu.Text.Trim();
            string mast_szdm = tbsz.Text.Trim();
            decimal mast_gwgt = nugross.Value;

            decimal mast_nwgt = nunet.Value;
            string mast_wunit = tbwunit.Text.Trim();
            decimal mast_vol = nuvol.Value;
            string mast_vunit = tbvunit.Text.Trim();
         
            string mast_desc1 = tbdesc1.Text.Trim();
            int mast_canqty = (int)nucan.Value;

            string mast_flag = cbflag.Text.Trim().Substring(0,1);

            if (mast_cd == "")
            {
                MessageBox.Show("제품코드를 입력하세요...!");
                return;
            }
            string ls = DateTime.Now.ToString("yyyyMMddhhmmss");
            string mast_date = ls.Substring(0, 8);
            string mast_time = ls.Substring(8, 6);

            if (mast_vol == 0)
            {
                MessageBox.Show("내용량을 입력하세요...!");
                return;
            }
          

            string sql = @" insert into mimast ( mast_cd, mast_desc, mast_type, mast_grp, mast_old, mast_bunit, mast_szdm, mast_gwgt, mast_nwgt, 
                                                 mast_wunit, mast_vol, mast_vunit, mast_date, mast_time, mast_flag, mast_desc1, mast_canqty )
                            values ( {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16} ) ";

            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                db.ExecuteCommand(sql, mast_cd, mast_desc, mast_type, mast_grp, mast_old, mast_bunit, mast_szdm, mast_gwgt, mast_nwgt,
                                       mast_wunit, mast_vol, mast_vunit, mast_date, mast_time, mast_flag, mast_desc1, mast_canqty);

                db.SubmitChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("등록 성공...!");

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmMimast_add_p_Load(object sender, EventArgs e)
        {
            cbflag.SelectedIndex = 0;
        }
    }
}
