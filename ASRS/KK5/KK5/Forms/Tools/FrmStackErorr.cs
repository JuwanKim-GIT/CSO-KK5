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
    public partial class FrmStackErorr : Form
    {
        #region --- MDI Child ----------------
        private static FrmStackErorr _instance;
        public static FrmStackErorr Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmStackErorr();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmStackErorr_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        DataGridView dv;

        string orgsql = @"SELECT erht_date,   
         erht_time,   
         (substring(erht_date,1,4) + '-' + substring(erht_date,5,2) + '-'  + substring(erht_date,7,2) + ' ' + 
         substring(erht_time,1,2) + ':' + substring(erht_time,3,2) + ':' + substring(erht_time,5,2) ) as erht_dt, 
         erht_hogi,   
         erht_ercd,   
         erht_mesg,   
         erht_gubn,   
         erht_pltn,   
         erht_lstk,   
         erht_pos,   
         erht_xmov
        FROM tberht
        WHERE tberht.erht_date is not NULL ";

        public FrmStackErorr()
        {
            InitializeComponent();
            this.FormClosed += FrmStackErorr_FormClosed;
            
            dv = dataGridView1;
            dv.ReadOnly = true;
            dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv.AutoGenerateColumns = false;
            dv.CellFormatting += Dv_CellFormatting;
            dv.RowPostPaint += Common.RowPostPaint;

        }

        private void Dv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {   try
            {
               
                if (e.ColumnIndex == 6)
                {
                    if (e.Value != null)
                    {
                        string ls = e.Value.ToString();
                        if (ls == "R") e.Value = "RCP";
                        if (ls == "A") e.Value = "원격";
                        e.FormattingApplied = true;
                    }
                }
                if (e.ColumnIndex == 8)
                {
                    if (e.Value != null)
                    {
                        string ls = e.Value.ToString();
                        e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + "-" + ls.Substring(5, 2);
                        e.FormattingApplied = true;
                    }
                }
                if (e.ColumnIndex == 9)
                {
                    if (e.Value != null)
                    {
                        string ls = e.Value.ToString();
                        e.Value = ls.Substring(0, 2) + "-" + ls.Substring(2, 2);
                        e.FormattingApplied = true;
                    }
                }
                if (e.ColumnIndex == 10)
                {
                    if (e.Value != null)
                    {
                        string ls = e.Value.ToString();
                        if (ls == "I") e.Value = "입고";
                        if (ls == "O") e.Value = "출고";
                        if (ls == "M") e.Value = "이동";
                        if (ls == "$") e.Value = "출고";
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception E) { }

        }

        private void FrmStackErorr_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            retrieve();
        }   

        private void btnqry_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = orgsql;

            string date1 = dtDatefrom.Text.Replace("-", "");
            string date2 = dtDateTo.Text.Replace("-", "");
            if (!chkdt.Checked)
            {
                modstr = modstr + " and erht_date >= '" + date1 + "'";
            }
            else
            {
                modstr = modstr + " and erht_date >= '" + date1 + "'";
                modstr = modstr + " and erht_date <= '" + date2 + "'";
            }

            string hogi = comboBox1.SelectedItem.ToString().Substring(0,1);
            if(hogi != "A")
            {
                hogi = "0" + hogi;
                modstr = modstr + " and erht_hogi = '" + hogi + "'";
            }

            modstr = modstr + " order by erht_dt asc";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv.DataSource = new SortableBindingList<tberhtq>(db.ExecuteQuery<tberhtq>(modstr).ToList());

                dv.TopLeftHeaderCell.Value = dv.RowCount.ToString();
                dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }
    }
    public class tberhtq
    {
        public string erht_dt { get; set; }
        public string erht_date { get; set; }
        public string erht_time { get; set; }
        public string erht_hogi { get; set; }
        public string erht_ercd { get; set; }
        public string erht_mesg { get; set; }
        public string erht_gubn { get; set; }
        public string erht_pltn { get; set; }
        public string erht_lstk { get; set; }
        public string erht_pos { get; set; }
        public string erht_xmov { get; set; }
    }
}
