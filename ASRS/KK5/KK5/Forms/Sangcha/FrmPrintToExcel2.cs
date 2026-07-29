using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Spire.Xls;
using System.Collections.Concurrent;

namespace KK5
{
    public partial class FrmPrintToExcel2 : Form
    {
        public FrmPrintToExcel2()
        {
            InitializeComponent();
        }
        #region -- variables definition ---------------
        Workbook workbook = new Workbook();
        Worksheet sheet0 = null;
        Worksheet sheet1 = null;
        Worksheet sheet2 = null;

        int row = 20;

        string tacarSql = string.Empty;
        string taordiSql = string.Empty;
        string taordiSql1 = string.Empty;
        string taordiSql2 = string.Empty;
        string update_taordiSql = string.Empty;

        Boolean lt75 = false;
        int opt = 0;

        string soPrev = string.Empty;
        string soCurr = string.Empty;

        tacarp rowHeader = null;
        #endregion

        string bachadate = "";
        string car_no = "";
        int seq = 0;
        bool hist = false;

        public FrmPrintToExcel2(string bacha, string car, int seq, bool hist)
        {
            InitializeComponent();

            this.bachadate = bacha;
            this.car_no = car;
            this.seq = seq;
            this.hist = hist;
        }

        private void FrmPrintToExcel2_Load(object sender, EventArgs e)
        {
            string ls_opt = string.Empty;
            if (System.IO.File.Exists("printopt.ini"))
            {
                ls_opt = System.IO.File.ReadAllText("printopt.ini").Trim();
                if (ls_opt == "0") radioButton1.Checked = true;
                if (ls_opt == "1") radioButton2.Checked = true;
                if (ls_opt == "2") radioButton3.Checked = true;
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (radioButton1.Checked)
                System.IO.File.WriteAllText("printopt.ini", "0");
            if (radioButton2.Checked)
                System.IO.File.WriteAllText("printopt.ini", "1");
            if (radioButton3.Checked)
                System.IO.File.WriteAllText("printopt.ini", "2");

            PrintExcel();
        }
        private void PrintExcel()            ///////////////////
        {
            if (radioButton1.Checked) opt = 0;
            if (radioButton2.Checked) opt = 1;
            if (radioButton3.Checked) opt = 2;
            if (checkBox1.Checked) lt75 = true;

            MakeSql();
           
            workbook.LoadFromFile("sample2.xlsx");
           
            //sheet = workbook.Worksheets[0];
            
            if (!PrintHeader())
            {
                MessageBox.Show("차량번호가 존재하지 않읍니다...!");
                Close();
                return;
            }
            try
            {              
                sheet0 = workbook.Worksheets[0];
                if (opt == 0)
                {                   
                    PrintBody(sheet0);
                    sheet0.DeleteRow(7, 13);
                    sheet0.Name = rowHeader.car_no + "_오더별";


                }
                else if (opt == 1)
                {
                    PrintBody2(sheet0);
                    sheet0.DeleteRow(7, 13);
                    sheet0.Name = rowHeader.car_no + "_도착지별";
                }
                else
                {
                    sheet1 = workbook.Worksheets.AddCopy(sheet0);

                    row = 20;
                    opt = 0;
                    MakeSql();
                    PrintBody(sheet0);
                    sheet0.Name = rowHeader.car_no + "_오더별";                   

                    row = 20;
                    opt = 1;
                    MakeSql();                    
                    PrintBody2(sheet1);

                    sheet0.DeleteRow(7, 13);
                    sheet1.DeleteRow(7, 13);
                    sheet1.Name = rowHeader.car_no + "_도착지별";
                 
                }
                int rc = 0;
                string msg = string.Empty;
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    rc = db.ExecuteCommand(update_taordiSql);
                    if (rc == 0) MessageBox.Show("에러", "Update 실패...!");
                }
                DateTime date = DateTime.Now;

                string year = date.Year.ToString("0000") + "년";
                string mon = date.Month.ToString("00") + "월";
                string day = date.Day.ToString("00") + "일";
                string dir;
                bachadate = bachadate.Replace("/", "");

                if (!hist)
                    dir = "c:\\asrs" + "\\D\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";
                else
                    dir = "c:\\asrs" + "\\DH\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";

                workbook.SaveToFile(dir, ExcelVersion.Version2013);
                System.Diagnostics.Process.Start(dir);
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("PrintExcel " + E.Message);
                Close();
            }
        }
        private bool PrintHeader()
        {
            try
            {
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {                 
                    rowHeader = db.ExecuteQuery<tacarp>(tacarSql, bachadate, car_no, seq).SingleOrDefault();                 
                }
            }
            catch (Exception E) {
                return false;
            }

            if (rowHeader == null)
            {
                return false;
            }                
            else return true;

        }
        private void PrintBody(Worksheet sheet)  // sales order 단위
        {
            string errMsg = string.Empty;
            string[] cols = new string[27];
            bool first = true;
            double oqty = 0;
            double ltqty = 0;
            string rmrk = string.Empty;
            string cmt = string.Empty;
            string PreArr = string.Empty;
            string CurArr = string.Empty;
           
            int saverow = 0;
            int lpp = 0;
            string vgbel;

       
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<loadinglist>(taordiSql).ToList();
              
                if (q == null) return;
                if (q.Count == 0) return;

                string pr_rmrk = "", pr_cmmt = "";

                foreach (var r in q)
                {
                    soCurr = r.sdno;
                    vgbel = r.vgbel;                    
                 
                    if (soCurr != soPrev)
                    {
                        if (!first) // total and so comment
                        {                            
                            // Total line 찍기
                            // copy total and set height
                            sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);  // row = 20
                            sheet.SetRowHeight(row, 32);
                            sheet.Range[row, 3].Text = "TOTAL";
                            sheet.Range[row, 5].NumberValue = oqty;
                            sheet.Range[row, 7].NumberValue = ltqty;

                            sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                            row++;
                                                      
                            cmt = pr_cmmt;
                            rmrk = pr_rmrk;                           

                            string[] ls = rmrk.Split('\n');
                            for (int i = 0; i < ls.Length; i++)
                            {
                                if (ls[i].Trim() == "") continue;
                                sheet.Range[row, 1].Text = ls[i].Trim();
                                sheet.Range[row, 1].Style.Font.Size = 12;
                                row++;
                            }
                        
                            row++;  // 여백 so 
                           
                        }  // end first
                       
                        oqty = 0;
                        ltqty = 0;
                        // copy sohdr
                        // header 찍기 -----------------
                        sheet.Range["sohdr"].Copy(sheet.Range[row, 1, row + 2, 9]);                       
                        sheet.SetRowHeight(row, 24);
                        sheet.SetRowHeight(row + 1, 24);
                        sheet.SetRowHeight(row + 2, 24);
                
                        //Thread.Sleep(100);                      

                        sheet.Range[row, 3].Text = vgbel;
                        sheet.Range[row + 1, 3].Text = soCurr;
                        sheet.Range[row + 1, 9].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                   
                        row = row + 3;
                    }

                    // Body 찍기
                    // copy row and set height
                    sheet.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);                    
                    sheet.SetRowHeight(row, 24);

                    CurArr = r.arrival;
                    if (soCurr == soPrev)
                    {
                        if (CurArr != PreArr)
                        {
                            row++;
                            sheet.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);                            
                            sheet.SetRowHeight(row, 24);
                        }
                    }
                
                    soPrev = soCurr;
                    PreArr = CurArr;

                    string str = "";
                    str = db.ExecuteQuery<string>("select mast_desc1 from mimast where mast_cd = '" + r.matnr + "'").SingleOrDefault();                    
                    if (str == "" || str == null)
                    {                      
                        str = r.matnrdesc;
                        string[] strs = str.Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray();
                        if (strs.Length != 0)
                            str = strs[strs.Length - 1].Trim();
                        else
                        {
                            str = r.matnrdesc;
                        }
                    }
                    
                    // move data
                    sheet.Range[row, 1].NumberValue = (double)r.posnr;
                    sheet.Range[row, 2].Text = r.matnr;

                    if (str == null)
                        sheet.Range[row, 3].Text = "";
                    else
                        sheet.Range[row, 3].Text = str;

                    
                    sheet.Range[row, 4].NumberValue = (double)r.ordi_size;
                    sheet.Range[row, 5].NumberValue = (double)r.qty;
                    sheet.Range[row, 7].Text = r.charg;
                    sheet.Range[row, 8].Text = r.lgort;
                    sheet.Range[row, 9].Text = r.cust_name1;
                  
                    CurArr = r.arrival;

                    if (!lt75)
                        oqty = oqty + (double)r.qty;
                    else
                    {
                        if (r.ordi_size >= 7.5m)
                            oqty = oqty + (double)r.qty;
                    }

                    ltqty = ltqty + (double)r.ordi_ltqty;

                    row++;

                    //pr = r; // save lastrow
                    pr_rmrk = r.rmrk;
                    pr_cmmt = r.cmmt;

                    first = false;
                } // end foreach

                // end footer
                // copy total and set height
                sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);
                sheet.SetRowHeight(row, 32);

                sheet.Range[row, 3].Text = "TOTAL";
                sheet.Range[row, 5].NumberValue = oqty;
                sheet.Range[row, 7].NumberValue = ltqty;

                sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                row++;

                cmt = pr_cmmt;
                rmrk = pr_rmrk;             

                string[] lstr = rmrk.Split('\n');
                for (int i = 0; i < lstr.Length; i++)
                {
                    if (lstr[i].Trim() == "") continue;
                    sheet.Range[row, 1].Text = lstr[i].Trim();
                    sheet.Range[row, 1].Style.Font.Size = 12;
                    row++;
                }             

                row++;
            }
        
        }
        private void PrintBody2(Worksheet sheet)
        {
            string errMsg = string.Empty;
            string[] cols = new string[27];
            bool first = true;
            double oqty = 0;
            double ltqty = 0;
            string rmrk = string.Empty;
            string cmt = string.Empty;

            string CurArr = string.Empty;
            string preArr = "xxxxxxxxxxxxxxxxxxxxxx";
            string curSo = string.Empty;
            string preSo = string.Empty;

            string soStr = string.Empty;
          
            string vgbels = string.Empty;
            string pr_rmrk = "";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
             

                var q = db.ExecuteQuery<loadinglist>(taordiSql2).ToList();
                if (q == null) return;
                if (q.Count() == 0) return;

                

                foreach (var r in q)
                {
                    CurArr = r.arrival;
                    soCurr = r.sdno;
                    rmrk = r.rmrk;
                    if (CurArr != preArr)
                    {
                        if (!first) // total and so print
                        {
                            // Total line 찍기
                            // copy total and set height
                            sheet0.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]); // row = 20

                            sheet.SetRowHeight(row, 32);
                            sheet.Range[row, 3].Text = "TOTAL";
                            sheet.Range[row, 5].NumberValue = oqty;
                            sheet.Range[row, 7].NumberValue = ltqty;

                            sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                            row++;
                                                      
                            rmrk = pr_rmrk;
                            string[] lss = rmrk.Split('\n');
                            for (int i = 0; i < lss.Length; i++)
                            {
                                if (lss[i].Trim() == "") continue;
                                sheet.Range[row, 1].Text = lss[i].Trim();
                                sheet.Range[row, 1].Style.Font.Size = 12;
                                row++;
                            }
                           
                            sheet.Range[row, 1].Style.Font.Size = 12;
                            sheet.Range[row, 1].Style.Font.Color = Color.Red;
                            sheet.Range[row, 1].Text = "도착지 : " + preArr;
                            row++;
                            row++;
                        }
                        // header 찍기 -----------------
                        oqty = 0;
                        ltqty = 0;
                        // copy sohdr
                        
                        sheet0.Range["sohdr"].Copy(sheet.Range[row, 1, row + 2, 9]);                        

                        sheet.SetRowHeight(row, 24);
                        sheet.SetRowHeight(row + 1, 24);
                        sheet.SetRowHeight(row + 2, 24);

                        getorders(CurArr, out soStr, out vgbels);

                        sheet.Range[row, 3].Text = vgbels;
                        sheet.Range[row + 1, 3].Text = soStr;
                        sheet.Range[row + 1, 9].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                        row = row + 3;
                    }
                   
                    // Body 찍기------------------------------------
                    // copy row and set height
                    sheet0.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);
                    sheet.SetRowHeight(row, 24);

                    if (CurArr == preArr)
                    {
                        if (soCurr != soPrev)
                        {
                            row++;
                            sheet.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);
                            sheet.SetRowHeight(row, 24);
                        }
                    }
                    //if (soCurr != soPrev)
                    //{
                    //    row++;
                    //    sheet0.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);
                    //    sheet.SetRowHeight(row, 24);
                    //}

                    soPrev = soCurr;
                    preArr = CurArr;
                   
                    string str = "";
                    str = db.ExecuteQuery<string>("select mast_desc1 from mimast where mast_cd = '" + r.matnr + "'").SingleOrDefault();
                    if (str == "" || str == null)
                    {
                        str = r.matnrdesc;
                        string[] strs = str.Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray();
                        if (strs.Length != 0)
                            str = strs[strs.Length - 1].Trim();
                        else
                        {
                            str = r.matnrdesc;
                        }
                    }
                    // move data
                    sheet.Range[row, 1].NumberValue = (double)r.posnr;
                   
                    sheet.Range[row, 2].Text = r.matnr;

                    if (str == null)
                        sheet.Range[row, 3].Text = "";
                    else
                    {
                       
                        sheet.Range[row, 3].Text = str;
                    }                        
                  
                    sheet.Range[row, 4].NumberValue = (double)r.ordi_size;
                    sheet.Range[row, 5].NumberValue = (double)r.qty;
                    sheet.Range[row, 7].Text = r.charg;
                    sheet.Range[row, 8].Text = r.lgort;
                    sheet.Range[row, 9].Text = r.cust_name1;
                                                       
                    if (!lt75)
                        oqty = oqty + (double)r.qty;
                    else
                    {
                        if ((double)r.ordi_size >= 7.5)
                            oqty = oqty + (double)r.qty;
                    }

                    ltqty = ltqty + (double)r.ordi_ltqty;

                    row++;

                    //pr = r; // save lastrow
                    pr_rmrk = r.rmrk;                     
                    first = false;

                }
                /////////////////////////////////////////////////////////////////////////////////////
                // End- footer
                // copy total and set height
                sheet0.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);
                sheet.SetRowHeight(row, 32);
                sheet.Range[row, 3].Text = "TOTAL";
                sheet.Range[row, 5].NumberValue = oqty;
                sheet.Range[row, 7].NumberValue = ltqty;

                sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                row++;

                rmrk = pr_rmrk;
                string[] rs = rmrk.Split('\n');
                for (int i = 0; i < rs.Length; i++)
                {
                    if (rs[i].Trim() == "") continue;
                    sheet.Range[row, 1].Text = rs[i].Trim();
                    sheet.Range[row, 1].Style.Font.Size = 12;
                    row++;
                }
             
                sheet.Range[row, 1].Style.Font.Size = 12;
                sheet.Range[row, 1].Style.Font.Color = Color.Red;
                sheet.Range[row, 1].Text = "도착지 : " + preArr;

                row++;
                row++;
            }
        }

        private void getorders(string arr, out string orders, out string vgbels)
        {
            orders = string.Empty;
            vgbels = string.Empty;

            string rmk = string.Empty;
            string vgbel = string.Empty;

            bool first = true;

            string sql = getSSqlTaordi1(rowHeader.bachadate, rowHeader.car_no, rowHeader.seq, arr);

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(sql).ToList();
                if (q == null) return;
                if (q.Count == 0) return;
                try
                {
                    foreach (var r in q)
                    {
                        if (first)
                        {
                            orders = r.sdno;
                            vgbels = r.vgbel;
                        }
                        else
                        {
                            orders = orders + "," + r.sdno;
                            vgbels = vgbels + "," + r.vgbel;
                        }                    
                        first = false;
                    }
                }
                catch (Exception E) { MessageBox.Show("Getorders " + E.Message); }


            }

        }
        private void MakeSql()
        {

            tacarSql = getSSqlTacar(bachadate, car_no, seq);

            taordiSql = getSSqlTaordi(bachadate, car_no, seq);

            taordiSql2 = getSSqlTaordi2(bachadate, car_no, seq);

            update_taordiSql = getUSqlTaordi(bachadate, car_no, seq);
        }

        private string getSSqlTacar(string date, string carno, int seq)
        {
            string sql;

            if (!hist)
                sql = "SELECT bachadate, car_no, seq, car_desc, car_man, car_dest, max_vol, load_vol, load_qty, step, remark " +
                      "  FROM tacar " +
                      " WHERE bachadate = '" + date + "' AND car_no = '" + carno + "' AND seq = " + seq.ToString();
            else
                sql = "SELECT bachadate, car_no, seq, car_desc, car_man, car_dest, max_vol, load_vol, load_qty, step, remark " +
                      "  FROM hacar " +
                      " WHERE bachadate = '" + date + "' AND car_no = '" + carno + "' AND seq = " + seq.ToString();
          
            return sql;
        }
        private string getSSqlTaordi(string date, string carno, int seq)
        {
            string sql;
            if (!hist)
            {
                if (opt == 1)
                {
                    sql = " SELECT arrival, sdno, posnr, matnr, matnrdesc, charg, lgort,  max(cust_name1) as cust_name1, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, max(vgbel) as vgbel," +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by arrival, sdno, posnr, matnrdesc, matnr, charg, lgort " +
                             " ORDER BY arrival, sdno, ordi_size desc, matnrdesc, charg, lgort ";
                  
                }
                else  //
                {
                    sql = " SELECT sdno, posnr, matnr, matnrdesc, charg, lgort, max(cust_name1) as cust_name1, max(vgbel) as vgbel," +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, posnr, matnr, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, ordi_size desc, matnrdesc, charg, lgort, posnr ";
                 

                }
            }
            else
            {
                if (opt == 1)
                    sql = " SELECT arrival, sdno, posnr, matnr, matnrdesc, charg, lgort,  max(cust_name1) as cust_name1, " +
                          " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, max(vgbel) as vgbel," +
                          " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                          " FROM haordi " +
                          " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " and ordi_check = '' " +
                          "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                          " group by arrival, sdno, posnr, matnrdesc, matnr, charg, lgort " +
                          " ORDER BY arrival, sdno, ordi_size desc, matnrdesc, charg, lgort ";

                else
                    sql = " SELECT sdno, posnr, matnr, matnrdesc, charg, lgort,  max(cust_name1) as cust_name1, max(vgbel) as vgbel," +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM haordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, posnr, matnr, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, ordi_size desc, matnrdesc, charg, lgort, posnr ";

            }


            return sql;
        }
        private string getSSqlTaordi1(string date, string carno, int seq, string arr)
        {
            string sql;
            if (!hist)
            {
                if (opt == 1)
                {
                    sql = " SELECT sdno, vgbel, isnull(max(rmrk),'') as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno, vgbel" +
                    " ORDER BY sdno, vgbel ";

                }
                else
                {
                    sql = " SELECT sdno, vgbel, isnull(max(rmrk),'') as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno, vgbel" +
                    " ORDER BY sdno, vgbel ";
                }
            }
            else
            {
                if (opt == 1)
                {
                    sql = " SELECT sdno, vgbel, isnull(max(rmrk),'') as rmrk " +
                             " FROM haordi" +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno, vgbel" +
                    " ORDER BY sdno, vgbel ";

                }
                else
                {
                    sql = " SELECT sdno, vgbel, isnull(max(rmrk), '') as rmrk " +
                             " FROM haordi  " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno, vgbel" +
                    " ORDER BY sdno, vgbel ";
                }

            }
            return sql;
        }
        private string getSSqlTaordi2(string date, string carno, int seq)
        {
            #region old------
            //string sql;
            //if (!hist)
            //{
            //    if (opt == 1)
            //    {
            //        sql = " SELECT arrival, matnrdesc, charg, lgort, " +
            //                 " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
            //                 " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
            //                 " FROM taordi  " +
            //                 " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
            //                 " group by arrival, matnrdesc, charg, lgort " +
            //        " ORDER BY arrival, ordi_size desc, matnrdesc, charg, lgort ";


            //    }
            //    else
            //    {
            //        sql = " SELECT arrival,  matnrdesc, charg, lgort, " +
            //                 " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
            //                 " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
            //                 " FROM taordi  " +
            //                 " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
            //                 " group by arrival, matnrdesc, charg, lgort " +
            //                 " ORDER BY arrival, ordi_size desc,  matnrdesc, charg, lgort ";
            //    }
            //}
            //else
            //{
            //    if (opt == 1)
            //    {
            //        sql = " SELECT arrival,  matnrdesc, charg, lgort, " +
            //             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
            //             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
            //             " FROM hiordi " +
            //             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
            //             " group by arrival, matnrdesc, charg, lgort " +
            //             " ORDER BY arrival, ordi_size desc,  matnrdesc, charg, lgort ";

            //    }
            //    else
            //    {
            //        sql = " SELECT arrival, matnrdesc, charg, lgort, " +
            //                 " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
            //                 " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
            //                 " FROM hiordi " +
            //                 " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
            //                 " group by arrival, matnrdesc, charg, lgort " +
            //                 " ORDER BY arrival, ordi_size desc, matnrdesc, charg, lgort ";
            //    }

            //}
            //return sql;
            #endregion

            string sql;
            if (!hist)
            {
                if (opt == 1)
                {
                    sql = " SELECT arrival, sdno, posnr, matnr, matnrdesc, charg, lgort,  max(cust_name1) as cust_name1, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, max(vgbel) as vgbel," +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by arrival, sdno, posnr, matnrdesc, matnr, charg, lgort " +
                             " ORDER BY arrival, sdno, ordi_size desc, matnrdesc, charg, lgort ";

                }
                else  //
                {
                    sql = " SELECT sdno, posnr, matnr, matnrdesc, charg, lgort, max(cust_name1) as cust_name1, max(vgbel) as vgbel," +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, posnr, matnr, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, ordi_size desc, matnrdesc, charg, lgort, posnr ";


                }
            }
            else
            {
                if (opt == 1)
                    sql = " SELECT arrival, sdno, posnr, matnr, matnrdesc, charg, lgort,  max(cust_name1) as cust_name1, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, max(vgbel) as vgbel," +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM haordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + "  and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by arrival, sdno, posnr, matnrdesc, matnr, charg, lgort " +
                             " ORDER BY arrival, sdno, ordi_size desc, matnrdesc, charg, lgort ";

                else
                    sql = " SELECT sdno, posnr, matnr, matnrdesc, charg, lgort, max(cust_name1) as cust_name1, max(vgbel) as vgbel," +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM haordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + "  and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, posnr, matnr, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, ordi_size desc, matnrdesc, charg, lgort, posnr ";

            }


            return sql;
        }

        private string getUSqlTaordi(string date, string carno, int seq)
        {
            string sql;

            if (!hist)
                sql = "update taordi " +
                      "   set print_step = '2' " +
                       " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' ";
            else
                sql = "update haordi " +
                      "   set print_step = '2' " +
                      " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND  car_sno = " + seq.ToString() + " and ordi_check = '' ";

            return sql;
        }


    }

    
    public class loadinglist
    {
        public string arrival { get; set; }
        public string sdno { get; set; }
        public int posnr { get; set; }
        public string matnr { get; set; }
        public string matnrdesc { get; set; }
        public string charg { get; set; }
        public string lgort { get; set; }
       
        public string cust_name1 { get; set; }
        public string vgbel { get; set; }
        public string wecust_name1 { get; set; }
        public string remark { get; set; }
        public decimal ordi_size { get; set; }
        public decimal qty { get; set; }
        public decimal ordi_ltqty { get; set; }
        public string cmmt { get; set; }
        public string rmrk { get; set; }

    }
}
