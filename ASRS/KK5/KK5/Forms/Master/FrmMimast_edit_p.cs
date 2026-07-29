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
    public partial class FrmMimast_edit_p : Form
    {
        mimastq m;
        public FrmMimast_edit_p(mimastq m)
        {
            InitializeComponent();
            this.m = m;
        }

        private void FrmMimast_edit_p_Load(object sender, EventArgs e)
        {
            tbcd.Text = m.mast_cd;
            tbdesc.Text = m.mast_desc;
            tbtype.Text = m.mast_type;
            tbgrp.Text = m.mast_grp;
            tbold.Text = m.mast_old;
            tbbu.Text = m.mast_bunit;
            tbsz.Text = m.mast_szdm;
            nugross.Value = m.mast_gwgt;
            nunet.Value = m.mast_nwgt;
            tbwunit.Text = m.mast_wunit;
            nuvol.Value = m.mast_vol;
            tbvunit.Text = m.mast_vunit;
           
            cbflag.SelectedIndex = Convert.ToInt32(m.mast_flag);
            tbdesc1.Text = m.mast_desc1;
            nucan.Value = m.mast_canqty;

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
            string mast_flag = cbflag.Text.Trim().Substring(0,1);
            string mast_desc1 = tbdesc1.Text.Trim();

            if (tbdesc1.Text.Trim().Length > 24)
                mast_desc1 = tbdesc1.Text.Trim().Substring(0,24);

            int mast_canqty = (int)nucan.Value;

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
            

            string sql = @" update mimast set mast_desc = {0}, mast_type = {1}, mast_grp = {2},  mast_old = {3}, mast_bunit = {4},
                                              mast_szdm = {5}, mast_gwgt = {6}, mast_nwgt = {7}, mast_wunit = {8}, mast_vol = {9}, 
                                              mast_vunit = {10}, mast_flag = {11}, mast_desc1 = {12}, mast_canqty = {13} 
                            where mast_cd = {14} ";

            try
            {
                DBDataContext db = new DBDataContext(Config.DBCon);
                db.ExecuteCommand(sql, mast_desc, mast_type, mast_grp, mast_old, mast_bunit, mast_szdm, mast_gwgt, mast_nwgt,
                                       mast_wunit, mast_vol, mast_vunit, mast_flag, mast_desc1, mast_canqty, mast_cd);

                db.SubmitChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("수정 성공...!");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
