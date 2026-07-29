using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using System.Runtime.InteropServices;
using Microsoft.Office;

namespace KK5
{
    public partial class FrmTaordi : Form
    {
        #region --- MDI Child ----------------
        private static FrmTaordi _instance;
        public static FrmTaordi Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmTaordi();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmTaordi_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region ---sql statement ---------------------

        string sqlm = @" SELECT taordi.docnum,   
                             taordi.credat,   
                             taordi.cretim,   
                             taordi.sdno,   
                             taordi.route,   
                             taordi.routedesc,   
                             taordi.deltyp,   
                             taordi.deltypdesc,   
                             taordi.cust,   
                             taordi.cust_name1,   
                             taordi.cust_name2,   
                             taordi.street,   
                             taordi.post,   
                             taordi.city,   
                             taordi.tel,   
                             taordi.contry,   
                             taordi.region,   
                             taordi.wecust,   
                             taordi.wecust_name1,   
                             taordi.wecust_name2,   
                             taordi.westreet,   
                             taordi.wepost,   
                             taordi.wecity,   
                             taordi.wetel,   
                             taordi.wecontry,   
                             taordi.weregion,   
                             taordi.duedate,   
                             taordi.cmmt,   
                             taordi.rmrk,   
                             taordi.parcel,   
                             taordi.posnr,   
                             taordi.matnr,   
                             taordi.matnrdesc,   
                             taordi.lgort,   
                             taordi.charg,   
                             taordi.plant,   
                             taordi.qty,   
                             taordi.gwgt,   
                             taordi.nwgt,   
                             taordi.wunit,   
                             taordi.vol,   
                             taordi.vunit,   
                             taordi.pstyv,   
                             taordi.pstyvdesc,   
                             taordi.sono,   
                             taordi.soposnr,   
                             taordi.sodate,   
                             taordi.custpo,   
                             taordi.custpodate,   
                             taordi.rqty,   
                             taordi.fqty,   
                             taordi.flag,   
                             taordi.arrival,   
                             taordi.car_no,   
                             taordi.car_step,   
                             taordi.car_sno,   
                             taordi.print_step,   
                             taordi.ordi_seq,   
                             taordi.ordi_check,   
                             taordi.remark,   
                             taordi.bachadate,   
                             taordi.ordi_ltqty,   
                             taordi.ordi_size,   
                             taordi.recv_dt,   
                             taordi.hdate,   
                             taordi.htime,
                             taordi.vsbed,
                             taordi.ablad
                    FROM taordi
                    WHERE taordi.docnum is not null and taordi.charg <> '' ";

        string sql2 = @"SELECT 
                               taordi.arrival as arrival, 
                               taordi.sdno as sdno, 
                               max(taordi.cust_name1) as cust_name1, 
                               max(taordi.wecust_name1) as wecust_name1, 
                               max(taordi.recv_dt) as shipdate, 
                               max(taordi.rmrk) as rmrk, 
                               sum(taordi.qty) as qty, 
                               sum(taordi.ordi_ltqty) as ordi_ltqty
                        FROM taordi 
                        group by  taordi.arrival, taordi.sdno  ";

        string sql3 = @"SELECT taordi.sdno as sdno, 
                               taordi.cust_name1 as cust_name1, 
                               max(taordi.wecust_name1) as wecust_name1, 
                               max(taordi.recv_dt) as shipdate, 
                               max(taordi.arrival) as arrival, 
                               max(taordi.rmrk) as rmrk, 
                               sum(taordi.qty) as qty, 
                               sum(taordi.ordi_ltqty) as ordi_ltqty,
                               max(taordi.ordi_check) as ordi_check 
                        FROM taordi 
                        group by  taordi.sdno, taordi.cust_name1  ";


        #endregion
                

        DataTableSumSortableDGV dv1, dv2, dv3;
        public FrmTaordi()
        {
            InitializeComponent();
            FormClosed += FrmTaordi_FormClosed;
            dv1 = dataGridView1;
            dv1.AutoGenerateColumns = false;
            dv1.ReadOnly = true;
            dv1.RowPostPaint += Common.RowPostPaint;

            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = true;
            dv1.CellFormatting += Dv1_CellFormatting;

            dv2 = dataGridView2;
            dv2.AutoGenerateColumns = false;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.ReadOnly = true;
            dv2.RowPostPaint += Common.RowPostPaint;

            dv3 = dataGridView3;
            dv3.AutoGenerateColumns = false;
            dv3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv3.ReadOnly = true;
            dv3.RowPostPaint += Common.RowPostPaint;

            //if (Config.UserLevel != "2" )
            //{
            //    btnadd.Enabled = false;
            //    btncmmt.Enabled = false;
            //    btncu.Enabled = false;
            //    btndatechg.Enabled = false;
            //    btndel.Enabled = false;
            //    btnnew.Enabled = false;
            //    btnremark.Enabled = false;
            //    btncheck.Enabled = false;
            //    btncheckdel.Enabled = false;

            //}
        }
        private void FrmTaordi_Load(object sender, EventArgs e)
        {
            //querycombobox();
            retrieve();
        }
        private bool IsRepeatedCellValue(int rowIndex, int colIndex)
        {
            DataGridViewCell currCell =
               dv1.Rows[rowIndex].Cells[colIndex];
            DataGridViewCell prevCell =
               dv1.Rows[rowIndex - 1].Cells[colIndex];

            if ((currCell.Value == prevCell.Value) ||
               (currCell.Value != null && prevCell.Value != null &&
               currCell.Value.ToString() == prevCell.Value.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
          
        }
        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;
            if (e.RowIndex == 0) return;
            if (e.ColumnIndex > 2) return; 

            //if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex))
            //{
            //    e.Value = string.Empty;
            //    e.FormattingApplied = true;
            //}

            //if (e.RowIndex > 0 && e.ColumnIndex == 0){
            //    if (dv1.Rows[e.RowIndex - 1].Cells[0].Value.ToString() == e.Value.ToString()) {
            //        e.Value = "";
            //    }
            //    else if (e.RowIndex < dv1.Rows.Count - 1) {

            //        dv1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            //    }
            //    e.FormattingApplied = true;
            //}
        }

        private void btnqry_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (tab1.SelectedIndex == 0) retrieve();
            if (tab1.SelectedIndex == 1) retrieve2();
            if (tab1.SelectedIndex == 2) retrieve3();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (tab1.SelectedIndex == 0) retrieve();
            if (tab1.SelectedIndex == 1) retrieve2();
            if (tab1.SelectedIndex == 2) retrieve3();
        }

        #region --- 조회 -----------------------
        private void retrieve()
        {
            tbsumqty.Text = "";

            string modstr = sqlm;

            string fdate = dtDatefrom.Text.Replace("-", "");
            string tdate = dtDateTo.Text.Replace("-", "");
            if (!chkdt.Checked) modstr = modstr + " and credat >= '" + fdate + "'";
            else
            {
                modstr = modstr + " and credat >= '" + fdate + "'";
                modstr = modstr + " and credat <= '" + tdate + "'";
            }
            modstr = modstr + " and car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
          
            modstr = modstr + querywhere();
            modstr = modstr + " order by arrival  ";    

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = QueryToDataTable.ToDataTable<taordiq>(db.ExecuteQuery<taordiq>(modstr).ToList());

                dv1.SumColumnIndices.Add(36);

                dv1.LabelColumnIndex = 52;
                dv1.LabelColumnText = "Total";

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
    
            }
        }
        private void retrieve_arr(string arr, string order)
        {
            tbsumqty.Text = "";

            string modstr = sqlm;

            string fdate = dtDatefrom.Text.Replace("-", "");
            string tdate = dtDateTo.Text.Replace("-", "");
            if (!chkdt.Checked) modstr = modstr + " and credat >= '" + fdate + "'";
            else
            {
                modstr = modstr + " and credat >= '" + fdate + "'";
                modstr = modstr + " and credat <= '" + tdate + "'";
            }

            modstr = modstr + querywhere();
            modstr = modstr + " and arrival in (" + arr + ")";
            modstr = modstr + " and sdno in (" + order + ")";
            modstr = modstr + " and car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
            modstr = modstr + " order by arrival, sdno  ";


            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = QueryToDataTable.ToDataTable<taordiq>(db.ExecuteQuery<taordiq>(modstr).ToList());

                dv1.SumColumnIndices.Add(36);


                dv1.LabelColumnIndex = 52;
                dv1.LabelColumnText = "Total";

                dv1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

        }
        private void retrieve(string sdnos)
        {
            tbsumqty.Text = "";

            string modstr = sqlm;

            string fdate = dtDatefrom.Text.Replace("-", "");
            string tdate = dtDateTo.Text.Replace("-", "");
            if (!chkdt.Checked) modstr = modstr + " and credat >= '" + fdate + "'";
            else
            {
                modstr = modstr + " and credat >= '" + fdate + "'";
                modstr = modstr + " and credat <= '" + tdate + "'";
            }       

            modstr = modstr + querywhere();
            modstr = modstr + " and sdno in (" + sdnos + ")";
            modstr = modstr + " and car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
            modstr = modstr + " order by arrival  ";
                     

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = QueryToDataTable.ToDataTable<taordiq>(db.ExecuteQuery<taordiq>(modstr).ToList());

                dv1.SumColumnIndices.Add(36);


                dv1.LabelColumnIndex = 52;
                dv1.LabelColumnText = "Total";

                dv1.TopLeftHeaderCell.Value = dataGridView1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
          
        }
        private void retrieve2()
        {
            string modstr = "";

            if (!chk75.Checked)
            {
                modstr = @"SELECT arrival, sdno, max(cust_name1) as cust_name1, max(recv_dt) as shipdate,
                                  sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(wecust_name1) as wecust_name1, max(rmrk) as rmrk
                            FROM taordi
                            where taordi.car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
            }
            else
            {
                modstr = @" SELECT arrival, sdno, max(cust_name1) as cust_name1, max(recv_dt) as shipdate, 
                                   sum(iif(ordi_size>=7.5, qty, 0)) as qty, sum(ordi_ltqty) as ordi_ltqty, max(wecust_name1) as wecust_name1, max(rmrk) as rmrk
                            FROM taordi 
                            where taordi.car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
            }


            modstr = modstr + querywhere();


            string fdate = dtDatefrom.Text.Replace("-", "");
            string tdate = dtDateTo.Text.Replace("-", "");

            if (!chkdt.Checked) modstr = modstr + " and taordi.credat >= '" + fdate + "'";
            else
            {
                modstr = modstr + " and taordi.credat >= '" + fdate + "'";
                modstr = modstr + " and taordi.credat <= '" + tdate + "'";
            }

            string ls_gr = " group by arrival, sdno  ";
            modstr = modstr + ls_gr;
            modstr = modstr + " order by arrival, sdno, cust_name1 ";


            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv2.DataSource = QueryToDataTable.ToDataTable<taordi2>(db.ExecuteQuery<taordi2>(modstr).ToList());

                dv2.TopLeftHeaderCell.Value = dataGridView2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void retrieve3()
        {
            string modstr = "";
            
            if (!chk75.Checked)
            {
                modstr = @" SELECT sdno, cust_name1, max(recv_dt) as shipdate,
                                   sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(wecust_name1) as wecust_name1, max(rmrk) as rmrk, max(ordi_check) as ordi_check
                            FROM taordi  
                            where taordi.car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";

            }else
            {
                modstr = @" SELECT sdno, cust_name1, max(recv_dt) as shipdate, 
                                   sum(iif(ordi_size>=7.5, qty, 0)) as qty, sum(ordi_ltqty) as ordi_ltqty, max(wecust_name1) as wecust_name1,
                                   max(rmrk) as rmrk, max(ordi_check) as ordi_check
                            FROM taordi 
                            where taordi.car_no = '' and charg <> '' and lgort <> '' and qty > 0 ";
            }

            string fdate = dtDatefrom.Text.Replace("-", "");          
            string tdate = dtDateTo.Text.Replace("-", "");            

            if (!chkdt.Checked) modstr = modstr + " and taordi.credat >= '" + fdate + "'";
            else
            {
                modstr = modstr + " and taordi.credat >= '" + fdate + "'";
                modstr = modstr + " and taordi.credat <= '" + tdate + "'";
            }

            modstr = modstr + querywhere();
            modstr = modstr + " group by sdno, cust_name1 ";
            modstr = modstr + " order by sdno, cust_name1 ";


            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {                
                dv3.DataSource = QueryToDataTable.ToDataTable<taordi3>(db.ExecuteQuery<taordi3>(modstr).ToList());             

                //dv3.LabelColumnIndex = 52;
                //dv3.LabelColumnText = "Total";

                dv3.TopLeftHeaderCell.Value = dataGridView3.RowCount.ToString();
                dv3.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private string querywhere()
        {
            string modstr = "";

            if (tbdoc.Text.Trim() != "") modstr = modstr + " and docnum like '%" + tbdoc.Text.Trim() + "%'";
            if (tbord.Text.Trim() != "") modstr = modstr + " and sdno like '%" + tbord.Text.Trim() + "%'";
            if (tbprod.Text.Trim() != "") modstr = modstr + " and matnr like '%" + tbprod.Text.Trim() + "%'";
            if (txtpdesc.Text.Trim() != "") modstr = modstr + " and matnrdesc like '%" + txtpdesc.Text.Trim() + "%'";
            if (tbbatch.Text.Trim() != "") modstr = modstr + " and charg like '%" + tbbatch.Text.Trim() + "%'";
            if (txtarr.Text.Trim() != "") modstr = modstr + " and arrival like '%" + txtarr.Text.Trim() + "%'";

            if (chkparcel.Checked) modstr = modstr + " and parcel = '1' ";
            else modstr = modstr + " and parcel <> '1' ";

            if (comboBox1.SelectedIndex >= 0)
            {
                string ls_check = comboBox1.SelectedItem.ToString();
                modstr = modstr + " and ordi_check ='" + ls_check + "' ";
            }

            return modstr;
        }
        private void querycombobox()
        {          
            comboBox1.SuspendLayout();
            comboBox1.Items.Clear();
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<string>(@"select ordi_check from taordi group by ordi_check");
                foreach (string s in q)
                {
                    comboBox1.Items.Add(s);
                }            
            }
            comboBox1.ResumeLayout();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

        }
     
        private string wf_getarrs()
        {
            string lsr = "";
            string parr = "-1x";
            string arr = "";
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                arr = r.Cells["arrival2"].Value.ToString().Trim();
                if (lsr == "")
                {
                    lsr = "'" + r.Cells["arrival2"].Value.ToString().Trim() + "'";
                }
                else
                {
                    if (parr != arr)
                        lsr = lsr + ",'" + r.Cells["arrival2"].Value.ToString().Trim() + "'";
                }
                parr = arr;
            }

            return lsr;
        }
        private string wf_getorders2()
        {
            string lsr = "";
            string psdno = "p";
            string sdno = "";
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                sdno = r.Cells["sdno2"].Value.ToString().Trim();
                if (sdno == "") continue;

                if (lsr == "")
                {
                    lsr = "'" + r.Cells["sdno2"].Value.ToString().Trim() + "'";
                }
                else
                {
                    if (psdno != sdno)
                        lsr = lsr + ",'" + r.Cells["sdno2"].Value.ToString().Trim() + "'";
                }
                psdno = sdno;
            }

            return lsr;
        }
        private string wf_getorders3()
        {
            string lsr = "";
            string psdno = "p";
            string sdno = "";
            foreach (DataGridViewRow r in dv3.SelectedRows)
            {
                sdno = r.Cells["sdno3"].Value.ToString().Trim();
                if (sdno == "") continue;

                if (lsr == "")
                {
                    lsr = "'" + r.Cells["sdno3"].Value.ToString().Trim() + "'";
                }
                else
                {
                    if (psdno != sdno)
                        lsr = lsr + ",'" + r.Cells["sdno3"].Value.ToString().Trim() + "'";
                }
                psdno = sdno;
            }

            return lsr;
        }
        #endregion

        #region ----SQL Statement ---------------------
        string sqltaordi_insert =
                         @"  INSERT INTO taordi  
                                 ( docnum,        credat,         cretim,             sdno,              route,   
                                   routedesc,     deltyp,         deltypdesc,         cust,              cust_name1,   
                                   cust_name2,    street,         post,               city,              tel,   
                                   contry,        region,         wecust,             wecust_name1,      wecust_name2,   
                                   westreet,      wepost,         wecity,             wetel,             wecontry,   
                                   weregion,      duedate,        cmmt,               rmrk,              parcel,   
                                   posnr,         matnr,          matnrdesc,          lgort,             charg,   
                                   plant,         qty,            gwgt,               nwgt,              wunit,   
                                   vol,           vunit,          pstyv,              pstyvdesc,         sono,   
                                   soposnr,       sodate,         custpo,             custpodate,        rqty,   
                                   fqty,          flag,           arrival,            car_no,            car_step,   
                                   car_sno,       print_step,     ordi_seq,           ordi_check,        remark,   
                                   bachadate,     ordi_ltqty,     ordi_size,          recv_dt,           hdate,   htime,
                                   vgbel,         vsbed,          ablad )  
                             select          
  	                               docnum,        credat,         cretim,             sdno,              route,   
                                   routedesc,     deltyp,         deltypdesc,         cust,              cust_name1,   
                                   cust_name2,    street,         post,               city,              tel,   
                                   contry,        region,         wecust,             wecust_name1,      wecust_name2,   
                                   westreet,      wepost,         wecity,             wetel,             wecontry,   
                                   weregion,      duedate,        cmmt,               rmrk,              parcel,   
                                   posnr,         matnr,          matnrdesc,          lgort,             charg,   
                                   plant,         {0},            gwgt,               nwgt,              wunit,   
                                   vol,           vunit,          pstyv,              pstyvdesc,         sono,   
                                   soposnr,       sodate,         custpo,             custpodate,        rqty,   
                                   fqty,          flag,           arrival,            car_no,            car_step,   
                                   car_sno,       print_step,     {1},                ordi_check,        remark,   
                                   bachadate,     {2},            ordi_size,          recv_dt,           hdate,   htime, 
                                   vgbel,         vsbed,          ablad         
                             from  taordi where docnum = {3} and sdno = {4} and posnr = {5} and ordi_seq = {6} ";

        string sql_updt1 = @"update taordi 
		                        set car_no = {0},  
                                bachadate = {1},  
                                qty = qty - {2}, 
                                ordi_ltqty = ordi_ltqty - {3}, 
                                car_sno = {4}, 
                                car_step = '0', 
                                print_step = '0'
	  		                 where docnum = {5}
			                   and sdno = {6}
			                   and posnr = {7}
			                   and ordi_seq = {8}";

        string sql_updt2 = @"update taordi 
		                        set car_no = {0},  
                                bachadate = {1},  
                                car_sno = {2}, 
                                car_step = '0', 
                                print_step = '0'
	  		                 where docnum = {3}
			                   and sdno = {4}
			                   and posnr = {5}
			                   and ordi_seq = {6}";


        #endregion

        #region --- new car load ----------------
        private void NewCarTab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count == 0) return;

            string ls_opt = "1"; //  전부선택
            using (FrmAllorSelect_p p = new FrmAllorSelect_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                if (p.radioButton1.Checked) ls_opt = "1";
                else ls_opt = "0";

                if (ls_opt == "1") dv1.SelectAll();
            }

            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            string car_no = "";

            using (FrmNewCarSel_p p1 = new FrmNewCarSel_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;

                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString(); ;
            }


            DBDataContext db = new DBDataContext(Config.DBCon);
            decimal max_vol = 0;
            max_vol = db.ExecuteQuery<decimal>(@"select max_vol from tacar where car_no = {0} and step in ('0', '') and flag = '' and parcel = {1} ", car_no, parcel).SingleOrDefault();
            if (max_vol == 0)
            {
                MessageBox.Show("상태가 변했읍니다..!");
                return;
            }
            
            decimal rand_Seq = db.p_getrand();
            string duedate = dv1.SelectedRows[0].Cells["duedate"].Value.ToString();
            string bachadate = "";
            db.p_curgetdatetime10(ref bachadate);

            Cursor = Cursors.WaitCursor;
            decimal sum_oqty = 0;
            int need_qty = 0, jan_qty = 0, rc = 0, ret = 0, cnt = 0;
            decimal sum_ltqty = 0, jan_ltqty = 0;
            bool finish = false, ff = false;
            int st = 0;
            try
            {
                db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;
                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;

                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    //MessageBox.Show(ordi_size.ToString());

                    if (ordi_size == 0) break;                   
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                    int ordi_oqty = (int)Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());

                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (int)((max_vol - sum_ltqty) / ordi_size);
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {
                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3} ", docnum, sdno, posnr, ordi_seq).SingleOrDefault();
                               
                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { st = 1;  ff = true; break; }
                               

                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { st = 2;  ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { st = 3;  ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { st = 4;  ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0 || cnt == 0)  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!" + st.ToString());
                    return;
                }

                int seq = db.s_getbachasno(bachadate);  // 배차순번 얻음
                if (seq <= 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("f_getbachasno return error...!");
                    return;
                }
                //  배차순번 update
                db.ExecuteCommand(@"update taordi set car_sno = {0} where car_no = {1} and bachaDate = {2} and car_sno = {3} ", seq, car_no, bachadate, rand_Seq);

                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set bachadate = {0}, seq = {1}, step = {2}, load_vol = {3}, load_qty = {4} where car_no = {5} and step = '0' and flag = '' ",
                                                                   bachadate, seq, lstep, sum_ltqty, sum_oqty, car_no);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {              
               Cursor = Cursors.Default;
            }
            retrieve();
        }
        private void NewCarTab2()
        {            
            string lsr = wf_getarrs();
            if (lsr == "") return;

            string lsr2 = wf_getorders2();
            if (lsr2 == "") return;
           
            retrieve_arr(lsr, lsr2);

            if (dv1.Rows.Count == 0) return;

            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            string car_no = "";
            using (FrmNewCarSel_p p1 = new FrmNewCarSel_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }
            dv1.SelectAll();

            DBDataContext db = new DBDataContext(Config.DBCon);
            decimal max_vol = 0;
            max_vol = db.ExecuteQuery<decimal>(@"select max_vol from tacar where car_no = {0} and step in ('0', '') and flag = '' and parcel = {1} ", car_no, parcel).SingleOrDefault();
            if (max_vol == 0)
            {
                MessageBox.Show("상태가 변했읍니다1..!");
                return;
            }

            int rand_Seq = db.ExecuteQuery<int>(@"select cast(floor(rand() * 2000 + 1000) as int) from tbstat").SingleOrDefault();

            string duedate = dv1.SelectedRows[0].Cells["duedate"].Value.ToString();
            string bachadate = "";
            db.p_curgetdatetime10(ref bachadate);

            Cursor = Cursors.WaitCursor;
            decimal sum_oqty = 0, need_qty = 0, jan_qty = 0;
            int rc = 0, ret = 0, cnt = 0;
            decimal sum_ltqty = 0, jan_ltqty = 0;
            bool finish = false, ff = false;

            try
            {
                if (db.Connection.State != ConnectionState.Open) db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;

                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    if (ordi_size == 0) break;

                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                    decimal ordi_oqty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());
                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (max_vol - sum_ltqty) / ordi_size;
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {

                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", docnum, sdno, posnr, ordi_seq).SingleOrDefault();

                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0 || cnt == 0)  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!");
                    return;
                }

                int seq = db.s_getbachasno(bachadate);  // 배차순번 얻음
                if (seq <= 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("f_getbachasno return error...!");
                    return;
                }
                //  배차순번 update
                db.ExecuteCommand(@"update taordi set car_sno = {0} where car_no = {1} and bachaDate = {2} and car_sno = {3} ", seq, car_no, bachadate, rand_Seq);

                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set bachadate = {0}, seq = {1}, step = {2}, load_vol = {3}, load_qty = {4} where car_no = {5} and step in ('0', '') and flag = '' ",
                                                                   bachadate, seq, lstep, sum_ltqty, sum_oqty, car_no);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            retrieve2();
        }
        private void NewCarTab3()
        {
            string lsr = wf_getorders3();
            if (lsr == "") return;
          
            retrieve(lsr);

            if (dv1.Rows.Count == 0) return;
                        
            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            string car_no = "";
            using (FrmNewCarSel_p p1 = new FrmNewCarSel_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString(); 
            }
            dv1.SelectAll();

            DBDataContext db = new DBDataContext(Config.DBCon);
            decimal max_vol = 0;
            max_vol = db.ExecuteQuery<decimal>(@"select max_vol from tacar where car_no = {0} and step in ('0', '') and flag = '' and parcel = {1} ", car_no, parcel).SingleOrDefault();
            if (max_vol == 0)
            {
                MessageBox.Show("상태가 변했읍니다1..!");
                return;
            }

            int rand_Seq = db.ExecuteQuery<int>(@"select cast(floor(rand() * 2000 + 1000) as int) from tbstat").SingleOrDefault();
            
            string duedate = dv1.SelectedRows[0].Cells["duedate"].Value.ToString();
            string bachadate = "";
            db.p_curgetdatetime10(ref bachadate);

            Cursor = Cursors.WaitCursor;
            decimal sum_oqty = 0, need_qty = 0, jan_qty = 0;
            int  rc = 0, ret = 0, cnt = 0;
            decimal sum_ltqty = 0, jan_ltqty = 0;
            bool finish = false, ff = false;

            try
            {
                if (db.Connection.State != ConnectionState.Open) db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;

                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    if (ordi_size == 0) break;
                  
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                    decimal ordi_oqty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());
                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (max_vol - sum_ltqty) / ordi_size;
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {
                               
                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", docnum, sdno, posnr, ordi_seq).SingleOrDefault();

                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, rand_Seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0 || cnt == 0)  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!");
                    return;
                }

                int seq = db.s_getbachasno(bachadate);  // 배차순번 얻음
                if (seq <= 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("f_getbachasno return error...!");
                    return;
                }
                //  배차순번 update
                db.ExecuteCommand(@"update taordi set car_sno = {0} where car_no = {1} and bachaDate = {2} and car_sno = {3} ", seq, car_no, bachadate, rand_Seq);

                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set bachadate = {0}, seq = {1}, step = {2}, load_vol = {3}, load_qty = {4} where car_no = {5} and step in ('0', '') and flag = '' ",
                                                                   bachadate, seq, lstep, sum_ltqty, sum_oqty, car_no);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            retrieve3();
        }

        #endregion

        #region  ----- delete check --------------
        private void checktab1del()
        {
            if (comboBox1.Items.Count == 0) return;
            if (dv1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("삭제할 행을 선택하세요?");
                return;
            }
        
            string ordi_check = dv1.SelectedRows[0].Cells["ordi_check"].Value.ToString();
            if (ordi_check == "") return;

            if (MessageBox.Show("check = " + ordi_check + " 삭제하시겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0, lp = 0;
            bool ff = false;
            DBDataContext db = new DBDataContext(Config.DBCon);
            Cursor = Cursors.WaitCursor;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                string docnum = r.Cells["docnum"].Value.ToString();
                if (docnum == "") continue;
                string sdno = r.Cells["sdno"].Value.ToString();
                int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());

                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(@"delete from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3} and car_no = '' and ordi_check = {4} ",
                                            docnum, sdno, posnr, ordi_seq, ordi_check);
                    if (rc == 0)  { ff = true;  break; }
                    db.SubmitChanges();
                    sc.Complete();
                    lp++;
                }
            }
            Cursor = Cursors.Default;
            if (ff) { MessageBox.Show("상태가변했읍니다."); }
            MessageBox.Show(lp.ToString() + " 개의 행이 삭제되었읍니다");
            retrieve();
        }
        private void checktab3del()
        {
            if (tab1.SelectedIndex != 2) return;
            if (comboBox1.Items.Count == 0) return;
            if (dv3.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 오더행을 선택하세요?");
                return;
            }

            string ordi_check = comboBox1.SelectedItem.ToString();
            if (ordi_check == "") return;

            if (MessageBox.Show("check = " + ordi_check + " 삭제하시겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int rc = 0, lp = 0;
            bool ff = false;
            DBDataContext db = new DBDataContext(Config.DBCon);
            Cursor = Cursors.WaitCursor;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv3.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {                
                string sdno = r.Cells["sdno3"].Value.ToString();
                
                using (TransactionScope sc = new TransactionScope())
                {
                    rc = db.ExecuteCommand(@"delete from taordi where sdno = {0} and car_no = '' and ordi_check = {1} ", sdno, ordi_check);
                    if (rc == 0) { ff = true; break; }
                    db.SubmitChanges();
                    sc.Complete();
                    lp++;
                }
            }
            Cursor = Cursors.Default;
            if (ff) { MessageBox.Show("상태가변했읍니다."); }
            MessageBox.Show(lp.ToString() + " 개의 행이 삭제되었읍니다");
            retrieve();

            //querycombobox();
        }
        #endregion
          
        #region ------- load car ----------------
        private void loadcar_tab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count == 0) return;
            
            string parcel = "";
            if (chkparcel.Checked) parcel = "1";

            FrmSelLoadCar_p p1 = new FrmSelLoadCar_p(parcel);
            p1.ShowDialog();
            if (p1.DialogResult == DialogResult.Cancel)
            {
                p1.Close();
                return;
            }
            string car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString(); ;
            p1.Close();

            DBDataContext db = new DBDataContext(Config.DBCon);
            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') and flag = '' and parcel = '" + parcel + "'";
            var q = db.ExecuteQuery(sql);
            if (q.Count() == 0)
            {
                MessageBox.Show("조회후 다시 실행하세요...!");
                return;
            }
            foreach (var s in q)
            {
                bachadate = s.bachadate;
                seq = s.seq;
                max_vol = s.max_vol;
                sum_ltqty = s.load_vol;
                sum_oqty = s.load_qty;
                break;
            }
            decimal save_ltqty = sum_ltqty;
            decimal save_oqty = sum_oqty;

            int need_qty = 0, jan_qty = 0;
            decimal jan_ltqty = 0;

            Cursor = Cursors.WaitCursor;
            bool finish = false, ff = false;
            int rc = 0, ret = 0, cnt = 0, rand_Seq = 0;
            try
            {
                if (db.Connection.State != ConnectionState.Open) db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;

                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;

                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    if (ordi_size == 0) break;
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                 
                    int ordi_oqty = (int)Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                   
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());
                  
                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (int)((max_vol - sum_ltqty) / ordi_size);
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {
                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", docnum, sdno, posnr, ordi_seq).SingleOrDefault();

                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }

                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0 )  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!");
                    return;
                }

  
                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set load_vol = {0}, load_qty = {1}, step = {2} 
                                           where car_no = {3} and step = '1' and load_vol = {4} and load_qty = {5} and flag = '' ",
                                                            sum_ltqty, sum_oqty, lstep, 
                                                            car_no, save_ltqty, save_oqty);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            retrieve();
        }
        private void loadcar_tab2()
        {
            if (dv2.SelectedRows.Count == 0) return;

            string lsr = wf_getarrs();
            if (lsr == "") return;

            string lsr2 = wf_getorders2();
            if (lsr2 == "") return;

            retrieve_arr(lsr, lsr2);

            if (dv1.Rows.Count == 0) return;


            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            string car_no = "";
            using (FrmSelLoadCar_p p1 = new FrmSelLoadCar_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }
            dv1.SelectAll();


            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') and flag <> '1' and parcel = '" + parcel + "'";
            var q = db.ExecuteQuery(sql).SingleOrDefault();
            if (q == null)
            {
                MessageBox.Show("조회후 다시 실행하세요...!");
                return;
            }
            bachadate = q.bachadate;
            seq = q.seq;
            max_vol = q.max_vol;
            sum_ltqty = q.load_vol;
            sum_oqty = q.load_qty;

            decimal save_ltqty = sum_ltqty;
            decimal save_oqty = sum_oqty;

            decimal need_qty = 0, jan_qty = 0;
            decimal jan_ltqty = 0;

            Cursor = Cursors.WaitCursor;
            bool finish = false, ff = false;
            int rc = 0, ret = 0, cnt = 0, rand_Seq = 0;
            try
            {
                if (db.Connection.State != ConnectionState.Open) db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;

                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    if (ordi_size == 0) break;

                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                    decimal ordi_oqty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());

                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (int)((max_vol - sum_ltqty) / ordi_size);
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {
                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", docnum, sdno, posnr, ordi_seq).SingleOrDefault();

                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }

                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0)  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!");
                    return;
                }


                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set load_vol = {0}, load_qty = {1}, step = {2} 
                                           where car_no = {3} and step = '1' and load_vol = {4} and load_qty = {5} and flag = '' ",
                                                            sum_ltqty, sum_oqty, lstep,
                                                            car_no, save_ltqty, save_oqty);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            retrieve2();
        }
        private void loadcar_tab3()
        {
            if (dv3.SelectedRows.Count == 0) return;

            string lsr = wf_getorders3();
            if (lsr == "") return;
            retrieve(lsr);
            
            if (dv1.Rows.Count == 0) return;
        

            string parcel = "";
            if (chkparcel.Checked) parcel = "1";
            string car_no = "";
            using (FrmSelLoadCar_p p1 = new FrmSelLoadCar_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();
            }
            dv1.SelectAll();
           
            
            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            DBDataContext db = new DBDataContext(Config.DBCon);
            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') and flag = '' and parcel = '" + parcel + "'";
            var q = db.ExecuteQuery(sql).SingleOrDefault();
            if (q == null)
            {
                MessageBox.Show("조회후 다시 실행하세요...!");
                return;
            }
            bachadate = q.bachadate;
            seq = q.seq;
            max_vol = q.max_vol;
            sum_ltqty = q.load_vol;
            sum_oqty = q.load_qty;

            decimal save_ltqty = sum_ltqty;
            decimal save_oqty = sum_oqty;

            decimal need_qty = 0, jan_qty = 0;
            decimal jan_ltqty = 0;

            Cursor = Cursors.WaitCursor;
            bool finish = false, ff = false;
            int rc = 0, ret = 0, cnt = 0, rand_Seq = 0;
            try
            {
                if (db.Connection.State != ConnectionState.Open) db.Connection.open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                foreach (DataGridViewRow r in rr)
                {
                    if (sum_ltqty >= max_vol) break;

                    string docnum = r.Cells["docnum"].Value.ToString();
                    if (docnum == "") continue;
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    decimal ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                    if (ordi_size == 0) break;

                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                    decimal ordi_oqty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    decimal ordi_ltqty = Convert.ToDecimal(r.Cells["ordi_ltqty"].Value.ToString());

                    if (sum_ltqty + ordi_ltqty > max_vol)
                    {
                        need_qty = (int)((max_vol - sum_ltqty) / ordi_size);
                        if (need_qty == 0)
                        {
                            finish = true;
                            break;
                        }
                        else
                        {
                            jan_qty = ordi_oqty - need_qty;
                            if (jan_qty == 0) finish = true;

                            if (!finish)
                            {
                                jan_ltqty = ordi_size * jan_qty;
                                rc = db.ExecuteQuery<int>(
                                    @"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", docnum, sdno, posnr, ordi_seq).SingleOrDefault();

                                ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }

                                ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, rand_Seq, docnum, sdno, posnr, ordi_seq);
                                if (ret == 0) { ff = true; break; }
                                cnt++;
                            }
                        }
                        if (finish)
                        {
                            ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; break; }
                        }
                        sum_oqty = sum_oqty + ordi_oqty - jan_qty;
                        sum_ltqty = sum_ltqty + ordi_ltqty - jan_ltqty;
                        break;

                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; break; }
                    }
                    cnt++;
                    sum_oqty = sum_oqty + ordi_oqty;
                    sum_ltqty = sum_ltqty + ordi_ltqty;

                    if (finish) break;
                } // end of foreach

                if (ff) // fault 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("조회후 다시 실행하세요...!");
                    return;
                }
                if (sum_oqty <= 0)  // 예약 여부 check
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("차량 예약이 실패했읍니다...!");
                    return;
                }


                string lstep = "1";
                if (finish) lstep = "2";

                ret = db.ExecuteCommand(@"update tacar set load_vol = {0}, load_qty = {1}, step = {2} 
                                           where car_no = {3} and step = '1' and load_vol = {4} and load_qty = {5} and flag = '' ",
                                                            sum_ltqty, sum_oqty, lstep,
                                                            car_no, save_ltqty, save_oqty);
                if (ret == 0)
                {
                    db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                    MessageBox.Show("tacar 상태가 변했읍니다..!");
                    return;
                }
                db.Transaction.Commit(); db.Transaction.Dispose(); db.Connection.Close();

            }
            catch (Exception E)
            {
                db.Transaction.Rollback(); db.Transaction.Dispose(); db.Connection.Close();
                MessageBox.Show(E.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            retrieve3();
        }
        private void loadcar_add_tab1()
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count == 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한줄만 선택하세요...!");
                return;
            }
            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            if (docnum == "") return;

            string parcel = "";
            if (chkparcel.Checked) parcel = "1";

            string car_no = "";
            using (FrmLoadcarAdd_p p1 = new FrmLoadcarAdd_p(parcel))
            {
                p1.ShowDialog();
                if (p1.DialogResult == DialogResult.Cancel) return;
                car_no = p1.dataGridView1.SelectedRows[0].Cells["car_no"].Value.ToString();                
            }

            DBDataContext db = new DBDataContext(Config.DBCon);
            string bachadate = "";
            int seq = 0;
            decimal max_vol = 0, sum_oqty = 0, sum_ltqty = 0;

            string sql = @"select bachadate, seq, max_vol, load_vol, load_qty from tacar 
                            where car_no ='" + car_no + "' and uuse = '1' and max_vol > load_vol and step in ('1') and flag = '' and parcel = '" + parcel + "'";
            var q = db.ExecuteQuery(sql).SingleOrDefault();
            if (q == null)
            {
                MessageBox.Show("조회후 다시 실행하세요...!");
                return;
            }
            
            bachadate = q.bachadate;
            seq = q.seq;
            max_vol = q.max_vol;
            sum_ltqty = q.load_vol;
            sum_oqty = q.load_qty;
            
            decimal qty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["qty"].Value.ToString());
            decimal ll_qty = 0;
            using (FrmLoadCarGetQty_p p = new FrmLoadCarGetQty_p(qty))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                ll_qty = p.numericTextox2.Value;
            }

            docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            int posnr = Convert.ToInt32(dv1.SelectedRows[0].Cells["posnr"].Value.ToString());
            decimal ordi_size = Convert.ToDecimal(dv1.SelectedRows[0].Cells["ordi_size"].Value.ToString());
            int ordi_seq = Convert.ToInt32(dv1.SelectedRows[0].Cells["ordi_seq"].Value.ToString());
            decimal ordi_oqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["qty"].Value.ToString());
            decimal ordi_ltqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["ordi_ltqty"].Value.ToString());

            decimal jan_qty = 0, jan_ltqty = 0;
            string ls_step;
            int rc = 0, cnt = 0;
            bool ff = false;
            if (sum_ltqty + (ll_qty * ordi_size) >= max_vol) ls_step = "2";
            else ls_step = "1";

            int ret = db.ExecuteCommand(@"update tacar set load_vol = load_vol + {0} * {1}, load_qty = load_qty + {2}, step = {3} 
                                           where car_no = {4} and step in ( '1', '2' ) and flag = '' ",
                                 ll_qty, ordi_size, ll_qty, ls_step, car_no);
            if (ret == 0)
            {
                MessageBox.Show("상태가 변했읍니다...!");
                return;
            }
            
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    if (ordi_oqty > ll_qty)
                    {
                        jan_qty = ordi_oqty - ll_qty;
                        jan_ltqty = jan_qty * ordi_size;

                        rc = db.ExecuteQuery<int>(@"select isnull(max(ordi_seq),0) + 1 from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3}", 
                             docnum, sdno, posnr, ordi_seq).SingleOrDefault();
                

                        ret = db.ExecuteCommand(sqltaordi_insert, jan_qty, rc, jan_ltqty, docnum, sdno, posnr, ordi_seq);
                        if (ret == 0) { ff = true; }
                        {
                            ret = db.ExecuteCommand(sql_updt1, car_no, bachadate, jan_qty, jan_ltqty, seq, docnum, sdno, posnr, ordi_seq);
                            if (ret == 0) { ff = true; }
                        }
                    }
                    else
                    {
                        ret = db.ExecuteCommand(sql_updt2, car_no, bachadate, seq, docnum, sdno, posnr, ordi_seq);
                    }
                    db.SubmitChanges();
                    sc.Complete();
                }
                if (ff) MessageBox.Show("상태가 변했읍니다2...");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            retrieve();
        }
        #endregion

        #region ------- click process ---------------
        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }
         
        private void btnnew_Click(object sender, EventArgs e)
        {            
            if (comboBox1.Items.Count > 0)
                if (comboBox1.SelectedItem.ToString() != "") return;

            if (tab1.SelectedIndex == 0) NewCarTab1();
            if (tab1.SelectedIndex == 1) NewCarTab2();
            if (tab1.SelectedIndex == 2) NewCarTab3();
        }

        private void btncu_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") return;
            if (tab1.SelectedIndex == 0) loadcar_tab1();
            if (tab1.SelectedIndex == 1) loadcar_tab2();
            if (tab1.SelectedIndex == 2) loadcar_tab3();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") return;
            if (tab1.SelectedIndex == 0) loadcar_add_tab1();
        }

        private void btncheck_Click(object sender, EventArgs e)
        {         
            if (tab1.SelectedIndex == 0) tab1check();
            if (tab1.SelectedIndex == 2) tab3check();
        }

        private void btncheckdel_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) checktab1del();
            if (tab1.SelectedIndex == 2) checktab3del();
        }

        private void btndatechg_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;

            string ls_date = "";

            using (FrmChangeDuedate_p p = new FrmChangeDuedate_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                ls_date = p.dtDueDate.Text.Replace("-", "");
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    int rc = 0, lp = 0;
                    bool ff = false;

                    List<DataGridViewRow> rr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dv1.SelectedRows)
                    {
                        rr.Insert(0, r);
                    }

                    foreach (DataGridViewRow r in rr)
                    {
                        string docnum = r.Cells["docnum"].Value.ToString();
                        if (docnum == "") continue;

                        string sdno = r.Cells["sdno"].Value.ToString();
                        int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                        int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());

                        rc = db.ExecuteCommand(@"update taordi set duedate = {0} where docnum = {1} and sdno = {2} and posnr = {3} and ordi_seq = {4} and car_no = '' ",
                                            ls_date, docnum, sdno, posnr, ordi_seq);
                        if (rc == 0) { ff = false; break; }
                        db.SubmitChanges();
                        lp++;
                    }
                    if (ff) MessageBox.Show("상태가 변했읍니다...!");
                    MessageBox.Show(lp.ToString() + " 개의 행이 변경되었읍니다...!");
                    retrieve();
                }
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
            finally { Cursor = Cursors.Default; }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            querycombobox();
        }

        private void btnremark_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex != 0) return;

            if (dv1.SelectedRows.Count <= 0) return;
            string remark = "";
            using(FrmTaordiRemark_p p = new FrmTaordiRemark_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                remark = p.textBox1.Text;
            }
            int rc = 0, lp = 0;

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            bool ff = false;
            foreach(DataGridViewRow r in rr)
            {
                string docnum = r.Cells["docnum"].Value.ToString();
                if (docnum == "") continue;

                string sdno = r.Cells["sdno"].Value.ToString();
                int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    rc = db.ExecuteCommand(@"update taordi set remark = {0} where docnum = {1} and sdno = {2} and posnr = {3} and ordi_seq = {4} and car_no = '' ",
                                             remark, docnum, sdno, posnr, ordi_seq);
                    if (rc == 0) { ff = true; break; }
                    lp++;
                }
            }
            if (ff) MessageBox.Show("상태변함...!");
            else MessageBox.Show(lp.ToString() + " 개의 행이 변경됨...OK!");
            retrieve();
        }

        private void btncmmt_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;

            string duedate = dv1.SelectedRows[0].Cells["duedate"].Value.ToString();
            string sdno = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            string rmrk = dv1.SelectedRows[0].Cells["rmrk"].Value.ToString();
            string parcel = dv1.SelectedRows[0].Cells["parcel"].Value.ToString();
            string cmmt = dv1.SelectedRows[0].Cells["cmmt"].Value.ToString();
            using (FrmLoadCarCmmt_p p = new FrmLoadCarCmmt_p(sdno, rmrk, parcel, cmmt))
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                rmrk = p.textBox1.Text;
                cmmt = p.richTextBox1.Text;
                if (p.checkBox1.Checked) parcel = "1"; else parcel = "";
            }
            int rc = 0;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.ExecuteCommand(@"update taordi set rmrk = {0}, cmmt = {1}, parcel = {2}
                                         where duedate = {3} and sdno = {4} and car_no = '' ",
                                         rmrk, cmmt, parcel, duedate, sdno);
                if (rc == 0) MessageBox.Show("오더 상태가 변했읍니다...!");
                else MessageBox.Show("변경 OK...!");
            }
            retrieve();
        }
        private void selsum1()
        {
            decimal sum = 0;
            string docnum = "";
            decimal ordi_size = 0;
            decimal qty = 0;

            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                docnum = r.Cells["docnum"].Value.ToString();
                if (docnum == "") continue;
                ordi_size = Convert.ToDecimal(r.Cells["ordi_size"].Value.ToString());
                qty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                if (chk75.Checked)
                {
                    if (ordi_size >= 7.5m) sum = sum + ordi_size * qty;
                }
                else sum = sum + ordi_size * qty;
            }
            tbsumqty.Text = sum.ToString("#,###,##0.00");
        }
        private void selsum2()
        {
            decimal sum = 0;
            foreach (DataGridViewRow r in dv2.SelectedRows)
            {
                sum = sum + Convert.ToDecimal(r.Cells["ordi_ltqty2"].Value.ToString());
            }
            tbsumqty.Text = sum.ToString("#,###,##0.00");
        }
        private void selsum3()
        {
            decimal sum = 0;
            foreach (DataGridViewRow r in dv3.SelectedRows)
            {
                sum = sum + Convert.ToDecimal(r.Cells["ordi_ltqty3"].Value.ToString());
            }
            tbsumqty.Text = sum.ToString("#,###,##0.00");
        }
        private void btnsumqty_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0) selsum1();
            if (tab1.SelectedIndex == 1) selsum2();
            if (tab1.SelectedIndex == 2) selsum3();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex != 0) return;
            if (dv1.SelectedRows.Count <= 0) return;

         
            //if (qty == 0 || lgort == "" || charg == "")
            //{
            if (MessageBox.Show("삭제하시겠읍니까?", "확인",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            int rc = 0;
            int cc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                foreach(DataGridViewRow r in dv1.SelectedRows)
                {
                    string docnum = r.Cells["docnum"].Value.ToString();
                    string sdno = r.Cells["sdno"].Value.ToString();
                    int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                    string lgort = r.Cells["lgort"].Value.ToString();
                    string charg = r.Cells["charg"].Value.ToString();
                    decimal qty = Convert.ToDecimal(r.Cells["qty"].Value.ToString());
                    int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());

                    rc = db.ExecuteCommand(@"delete from taordi where docnum = {0} and sdno = {1} and posnr = {2} and ordi_seq = {3} ", docnum, sdno, posnr, ordi_seq);

                    if (rc <= 0) break;
                    cc++;
                    //dv1.Rows.Remove(r);
                }
                    
            }

            if (rc > 0) MessageBox.Show(cc.ToString() + " 개 삭제 OK!");
            else MessageBox.Show(cc.ToString() + " 개  삭제...!");

            retrieve();
            //}
        }

        private void tbdoc_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                tbdoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            }
        }

        private void tbord_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                tbord.Text = dv1.SelectedRows[0].Cells["sdno"].Value.ToString();
            }
            if (tab1.SelectedIndex == 1)
            {
                if (dv2.SelectedRows.Count <= 0) return;
                tbord.Text = dv2.SelectedRows[0].Cells["sdno2"].Value.ToString();
            }
            if (tab1.SelectedIndex == 3)
            {
                if (dv3.SelectedRows.Count <= 0) return;
                tbord.Text = dv3.SelectedRows[0].Cells["sdno3"].Value.ToString();
            }
        }

        private void tbprod_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                tbprod.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            }
            else
            {
                tbprod.Text = "";
            } 
        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                tbbatch.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            }
            else
            {
                tbbatch.Text = "";
            }
        }


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void txtpdesc_DoubleClick(object sender, EventArgs e)
        {
            if(tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                txtpdesc.Text = dv1.SelectedRows[0].Cells["matnrdesc"].Value.ToString();
            }
            else
            {
                txtpdesc.Text = "";
            }
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void dataTableSumSortableDGV1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.Default;
        }
             
        private void txtarr_DoubleClick(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (dv1.SelectedRows.Count <= 0) return;
                txtarr.Text = dv1.SelectedRows[0].Cells["arrival"].Value.ToString();
            }
            else if (tab1.SelectedIndex == 1)
            {
                if (dv2.SelectedRows.Count <= 0) return;
                txtarr.Text = dv2.SelectedRows[0].Cells["arrival2"].Value.ToString();
            }
            else
            {
                txtarr.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
           // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            worksheet.Columns.AutoFit();
            // storing header part in Excel  
            for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  
            workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

        }

        private void tbord_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region ----- check 삽입 -----------
        private void tab1check()
        {
            if (dv1.SelectedRows.Count <= 0) return;
            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            if (docnum == "") return;

            string ordi_check = "";
            using(FrmCheck_p p = new FrmCheck_p())
            {
                if (p.ShowDialog() == DialogResult.Cancel) return;
                ordi_check = p.textBox1.Text;                   
            }          
          
            int rc = 0;
            bool ff = false;
            Cursor = Cursors.WaitCursor;
            DBDataContext db = new DBDataContext(Config.DBCon);

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv1.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                docnum = r.Cells["docnum"].Value.ToString();
                if (docnum == "") continue;
                string sdno = r.Cells["sdno"].Value.ToString();
                int posnr = Convert.ToInt32(r.Cells["posnr"].Value.ToString());
                int ordi_seq = Convert.ToInt32(r.Cells["ordi_seq"].Value.ToString());
                string sql = @"update taordi set ordi_check = {0} where docnum = {1} and sdno = {2} and posnr = {3} and ordi_seq = {4} and car_no = '' ";
                rc = db.ExecuteCommand(sql, ordi_check, docnum, sdno, posnr, ordi_seq);
                if (rc == 0) { ff = true;  break; }
                db.SubmitChanges();
            }
            Cursor = Cursors.Default;
            if (ff) MessageBox.Show("상태가 변했읍니다...!");
            //querycombobox();
            retrieve();
        }
        private void tab3check()
        {
            if (dv3.SelectedRows.Count == 0) return;

            string ordi_check = "";
            using (FrmCheck_p p = new FrmCheck_p())
            {
                if (p.ShowDialog() == DialogResult.Cancel) return;
                ordi_check = p.textBox1.Text;
            }

            int rc = 0;
            bool ff = false;
            Cursor = Cursors.WaitCursor;
            DBDataContext db = new DBDataContext(Config.DBCon);

            List<DataGridViewRow> rr = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dv3.SelectedRows)
            {
                rr.Insert(0, r);
            }

            foreach (DataGridViewRow r in rr)
            {
                string sdno = r.Cells["sdno3"].Value.ToString();

                rc = db.ExecuteCommand(@"update taordi set ordi_check = {0}	where sdno = {1} and car_no = ''", ordi_check, sdno);
                if (rc == 0) { ff = true; break; }
                db.SubmitChanges();
            }
            Cursor = Cursors.Default;
            if (ff) MessageBox.Show("상태가 변했읍니다...!");
            //querycombobox();
            retrieve3();
        }
        #endregion

    }



    #region --- 화면 UI Griddataview----------------
    public class taordiq
    {
        public string docnum { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public string sdno { get; set; }
        public string route { get; set; }
        public string routedesc { get; set; }
        public string deltyp { get; set; }
        public string deltypdesc { get; set; }
        public string cust { get; set; }
        public string cust_name1 { get; set; }
        public string cust_name2 { get; set; }
        public string street { get; set; }
        public string post { get; set; }
        public string city { get; set; }
        public string tel { get; set; }
        public string contry { get; set; }
        public string region { get; set; }
        public string wecust { get; set; }
        public string wecust_name1 { get; set; }
        public string wecust_name2 { get; set; }
        public string westreet { get; set; }
        public string wepost { get; set; }
        public string wecity { get; set; }
        public string wetel { get; set; }
        public string wecontry { get; set; }
        public string weregion { get; set; }
        public string duedate { get; set; }
        public string cmmt { get; set; }
        public string rmrk { get; set; }
        public string parcel { get; set; }
        public int posnr { get; set; }
        public string matnr { get; set; }
        public string matnrdesc { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public string plant { get; set; }
        public decimal qty { get; set; }
        public decimal gwgt { get; set; }
        public decimal nwgt { get; set; }
        public string wunit { get; set; }
        public decimal vol { get; set; }
        public string vunit { get; set; }
        public string pstyv { get; set; }
        public string pstyvdesc { get; set; }
        public string sono { get; set; }
        public int soposnr { get; set; }
        public string sodate { get; set; }
        public string custpo { get; set; }
        public string custpodate { get; set; }
        public decimal rqty { get; set; }
        public decimal fqty { get; set; }
        public string flag { get; set; }
        public string arrival { get; set; }
        public string car_no { get; set; }
        public string car_step { get; set; }
        public int car_sno { get; set; }
        public string print_step { get; set; }
        public int ordi_seq { get; set; }
        public string ordi_check { get; set; }
        public string remark { get; set; }
        public string bachadate { get; set; }
        public decimal ordi_ltqty { get; set; }
        public decimal ordi_size { get; set; }
        public DateTime recv_dt { get; set; }
        public string hdate { get; set; }
        public string htime { get; set; }
        public string vsbed { get; set; }
        public string ablad { get; set; }
       
    }
    public class taordi2
    {
        public string arrival { get; set; }
        public string sdno { get; set; }
        public string cust_name1 { get; set; }
        public string wecust_name1 { get; set; }
        public decimal qty { get; set; }
        public decimal ordi_ltqty { get; set; }
        public DateTime shipdate { get; set; }
        public string rmrk { get; set; }
        public string tel { get; set; }
    }

    public class taordi3
    {
        public string sdno { get; set; }
        public string cust_name1 { get; set; }
        public string wecust_name1 { get; set; }
        public decimal qty { get; set; }
        public decimal ordi_ltqty { get; set; }
        public DateTime shipdate { get; set; }
        public string rmrk { get; set; }
        public string tel { get; set; }
        public string ordi_check { get; set; }
    }
    #endregion
    public class GroupByGrid : DataGridView
    {

        protected override void OnCellFormatting(
           DataGridViewCellFormattingEventArgs args)
        {
            // Call home to base
            base.OnCellFormatting(args);

            // First row always displays
            if (args.RowIndex == 0)
                return;


            //if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
            //{
            //    args.Value = string.Empty;
            //    args.FormattingApplied = true;
            //}
        }

        private bool IsRepeatedCellValue(int rowIndex, int colIndex)
        {
            DataGridViewCell currCell =
               Rows[rowIndex].Cells[colIndex];
            DataGridViewCell prevCell =
               Rows[rowIndex - 1].Cells[colIndex];

            if ((currCell.Value == prevCell.Value) ||
               (currCell.Value != null && prevCell.Value != null &&
               currCell.Value.ToString() == prevCell.Value.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnCellPainting(
           DataGridViewCellPaintingEventArgs args)
        {
            base.OnCellPainting(args);

            //args.AdvancedBorderStyle.Bottom =
            //   DataGridViewAdvancedCellBorderStyle.None;

            //// Ignore column and row headers and first row
            //if (args.RowIndex < 1 || args.ColumnIndex < 0)
            //    return;

            //if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
            //{
            //    args.AdvancedBorderStyle.Top =
            //       DataGridViewAdvancedCellBorderStyle.None;
            //}
            //else
            //{
            //    args.AdvancedBorderStyle.Top = AdvancedCellBorderStyle.Top;
            //}
        }
    }
}
