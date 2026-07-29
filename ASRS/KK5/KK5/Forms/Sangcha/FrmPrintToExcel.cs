using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Xls;

namespace KK5
{
    public partial class FrmPrintToExcel : Form
    {
        #region -- variables definition ---------------
        Workbook workbook = new Workbook();
        Worksheet sheet = null;
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

        public FrmPrintToExcel(string bacha, string car, int seq, bool hist)
        {
            InitializeComponent();

            this.bachadate = bacha;
            this.car_no = car;
            this.seq = seq;
            this.hist = false;
        }

        private void FrmPrintToExcel_Load(object sender, EventArgs e)
        {
            string ls_opt = string.Empty;
            if (System.IO.File.Exists("printopt.ini"))
            {
                ls_opt = System.IO.File.ReadAllText("printopt.ini").Trim();
                if (ls_opt == "0") radioButton1.Checked = true;
                if (ls_opt == "1") radioButton2.Checked = true;
                if (ls_opt == "2") radioButton3.Checked = true;
                if (ls_opt == "3") radioButton4.Checked = true;
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
            if (radioButton4.Checked)
                System.IO.File.WriteAllText("printopt.ini", "3");

            PrintExcel();
        }
        private void PrintExcel()
        {
            if (radioButton2.Checked) opt = 1;
            if (radioButton3.Checked) opt = 2;
            if (radioButton4.Checked) opt = 3;
            if (checkBox1.Checked) lt75 = true;

            MakeSql();

            workbook.LoadFromFile("sample.xlsx");
            sheet = workbook.Worksheets[0];

            if (!PrintHeader())
            {
                MessageBox.Show("차량번호가 존재하지 않읍니다...!");
                Close();
                return;
            }
            try
            {
                sheet = workbook.Worksheets[0];
                if (opt == 0)
                {
                    PrintBody();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_오더별";
                }
                else if (opt == 1)
                {

                    PrintBody2();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_도착지별";
                }
                else if (opt == 2)
                {

                    PrintBody();
                    PrintBody2();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_DO는 오더별_SO는 도착지별";
                }
                else
                {

                    sheet1 = workbook.Worksheets.AddCopy(sheet);
                    sheet2 = workbook.Worksheets.AddCopy(sheet);

                    row = 20;
                    opt = 0;
                    MakeSql();
                    PrintBody();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_오더별";

                    row = 20;
                    opt = 1;
                    MakeSql();
                    sheet = sheet1;
                    PrintBody2();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_도착지별";

                    row = 20;
                    opt = 2;
                    MakeSql();
                    sheet = sheet2;
                    PrintBody();
                    PrintBody2();
                    sheet.DeleteRow(7, 13);
                    sheet.Name = rowHeader.car_no + "_DO는 오더별_SO는 도착지별";

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
                bachadate = bachadate.Substring(0, 4) + bachadate.Substring(4, 2) + bachadate.Substring(6, 2);

                if (hist)
                    dir = "c:\\asrs" + "\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";
                else
                    dir = "c:\\asrs" + "\\H\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";

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
            catch(Exception E) {  return false; }
          
            if (rowHeader == null) return false;
            else return true;

        }
        private void PrintBody()  // sales order 단위
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

            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
               
                var q = db.ExecuteQuery(taordiSql).ToList();
                if (q == null) return;
                if (q.Count == 0) return;

                string pr_rmrk = "", pr_cmmt = "";

                foreach (var r in q)
                {
                    soCurr = r.sdno;
                    if (soCurr != soPrev)
                    {
                        if (!first) // total and so comment
                        {
                            // copy total and set height
                            sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);

                            sheet.SetRowHeight(row, 32);
                            sheet.Range[row, 2].Text = "TOTAL";
                            sheet.Range[row, 4].NumberValue = oqty;
                            sheet.Range[row, 6].NumberValue = ltqty;

                            sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                            row++;
                            sheet.Range[row, 9].Text = pr_rmrk;

                            // so comment 
                            cmt = pr_cmmt;
                            string[] ls = cmt.Split('\n');

                            for (int i = 0; i < ls.Length; i++)
                            {
                                if (ls[i].Trim() == "") continue;
                                sheet.Range[row, 2].Text = ls[i].Trim();
                                row++;
                            }
                            row++;  // 여백 so 
                        }
                        oqty = 0;
                        ltqty = 0;
                        // copy sohdr
                        sheet.Range["sohdr"].Copy(sheet.Range[row, 1, row + 1, 9]);

                        sheet.SetRowHeight(row, 24);
                        sheet.SetRowHeight(row + 1, 24);

                        // write so
                        sheet.Range[row, 2].Text = soCurr;

                        //soPrev = soCurr;
                        // datetime 
                        sheet.Range[row, 9].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                        row = row + 2;
                    }

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

                    // move data
                    sheet.Range[row, 2].Text = r.matnrdesc;
                    sheet.Range[row, 3].NumberValue = (double)r.ordi_size;
                    sheet.Range[row, 4].NumberValue = (double)r.qty;
                    sheet.Range[row, 6].Text = r.charg;
                    sheet.Range[row, 8].Text = r.lgort;
                    sheet.Range[row, 7].Text = r.remark;
                    sheet.Range[row, 9].Text = r.arrival;
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
                }
                // end footer
                // copy total and set height
                sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);
                sheet.SetRowHeight(row, 32);
                sheet.Range[row, 2].Text = "TOTAL";
                sheet.Range[row, 4].NumberValue = oqty;
                sheet.Range[row, 6].NumberValue = ltqty;

                sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                row++;
                sheet.Range[row, 9].Text = pr_rmrk;

                // so comment 
                cmt = pr_cmmt;
                string[] lstr = cmt.Split('\n');

                // MessageBox.Show(lstr.Length.ToString());
                for (int i = 0; i < lstr.Length; i++)
                {
                    if (lstr[i].Trim() == "") continue;
                    sheet.Range[row, 2].Text = lstr[i].Trim();
                    row++;
                }
                row++;
            }       
      
        }
        private void PrintBody2()
        {
            string errMsg = string.Empty;
            string[] cols = new string[27];
            bool first = true;
            double oqty = 0;
            double ltqty = 0;
            string rmrk = string.Empty;
            string cmt = string.Empty;

            string arrivalCur = string.Empty;
            string arrivalPre = string.Empty;
            string curSo = string.Empty;
            string preSo = string.Empty;
            string soStr = string.Empty;
            int saverow = 0;

            string orders = string.Empty;
            string cmmt = string.Empty;

            string pr_cmmt = "";
            string pr_rmrk = "";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery(taordiSql2).ToList();
                if (q == null) return;
                if (q.Count() == 0) return;
                foreach (var r in q)
                {
                    arrivalCur = r.arrival;
                    if (arrivalCur != arrivalPre)
                    {
                        if (!first) // total and so print
                        {
                            // Total line 찍기
                            // copy total and set height
                            sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);
                            sheet.SetRowHeight(row, 32);
                            sheet.Range[row, 2].Text = "TOTAL";
                            sheet.Range[row, 4].NumberValue = oqty;
                            sheet.Range[row, 6].NumberValue = ltqty;

                            sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                            row++;
                            sheet.Range[row, 9].Text = pr_rmrk;

                            getorders(arrivalPre, out soStr, out cmmt);
                            sheet.Range[saverow, 2].Text = soStr;
                            saverow = 0;
                            string[] lst = cmmt.Split('\n');

                            for (int i = 0; i < lst.Length; i++)
                            {
                                if (lst[i].Trim() == "") continue;
                                sheet.Range[row, 2].Text = lst[i].Trim();
                                row++;
                            }
                            row++;  // 여백 so 

                        }
                        // header 찍기 -----------------
                        oqty = 0;
                        ltqty = 0;
                        // copy sohdr
                        sheet.Range["sohdr"].Copy(sheet.Range[row, 1, row + 1, 9]);
                        sheet.SetRowHeight(row, 24);
                        sheet.SetRowHeight(row + 1, 24);

                        if (saverow == 0) saverow = row;
                        sheet.Range[row, 9].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  // datetime 

                        row = row + 2;
                    }
                    // Body 찍기
                    // copy row and set height
                    sheet.Range["soBody"].Copy(sheet.Range[row, 1, row, 9]);
                    sheet.SetRowHeight(row, 24);

                    // move data
                    sheet.Range[row, 2].Text = r.matnrdesc;
                    sheet.Range[row, 3].NumberValue = (double)r.ordi_size;
                    sheet.Range[row, 4].NumberValue = (double)r.qty;
                    sheet.Range[row, 6].Text = r.charg;
                    sheet.Range[row, 8].Text = r.lgort;
                    sheet.Range[row, 7].Text = r.remark;
                    sheet.Range[row, 9].Text = r.arrival;

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
                    pr_cmmt = r.cmmt;

                    arrivalPre = arrivalCur;
                    first = false;

                }
                /////////////////////////////////////////////////////////////////////////////////////
                // End- footer
                // copy total and set height
                sheet.Range["soFtr"].Copy(sheet.Range[row, 1, row, 9]);
                sheet.SetRowHeight(row, 32);
                sheet.Range[row, 2].Text = "TOTAL";
                sheet.Range[row, 4].NumberValue = oqty;
                sheet.Range[row, 6].NumberValue = ltqty;

                sheet.Range[row, 9].Text = "차량번호 : " + rowHeader.car_no;
                row++;
                sheet.Range[row, 9].Text = pr_rmrk;

                //----------------------------------------------
                getorders(arrivalCur, out soStr, out cmmt);
                sheet.Range[saverow, 2].Text = soStr;

                string[] ls = cmmt.Split('\n');

                for (int i = 0; i < ls.Length; i++)
                {
                    if (ls[i].Trim() == "") continue;
                    sheet.Range[row, 2].Text = ls[i].Trim();
                    row++;
                }

            }
        }

        private void getorders(string arr, out string orders, out string cmmt)
        {
            orders = string.Empty;
            cmmt = string.Empty;

            string cmt = string.Empty;
            bool first = true;

            string sql = getSSqlTaordi1(rowHeader.bachadate, rowHeader.car_no, rowHeader.seq, arr);

            using(DBDataContext db = new DBDataContext(Config.DBCon))
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
                        }
                        else
                        {
                            orders = orders + "," + r.sdno;
                        }
                        cmt = r.cmmt;
                        if (cmt.Length >= cmmt.Length) cmmt = cmt;
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
                sql = "SELECT bachadate, car_no, sno, car_desc, car_man, car_dest, max_vol, load_vol, load_qty, step, remark " +
                      "  FROM hicar " +
                      " WHERE bachadate = '" + date + "' AND car_no = '" + carno + "' AND sno = " + seq.ToString();

            return sql;
        }
        private string getSSqlTaordi(string date, string carno, int seq)
        {
            string sql;
            if (!hist)
            {
                if (opt == 2)
                {
                    sql = " SELECT arrival, sdno, matnrdesc, charg, lgort, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, arrival, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, arrival, ordi_size desc, matnrdesc, charg, lgort ";
                }
                else
                {
                    sql = " SELECT arrival, sdno, matnrdesc, charg, lgort, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                             " group by sdno, arrival, matnrdesc, charg, lgort " +
                             " ORDER BY sdno, arrival, ordi_size desc, matnrdesc, charg, lgort ";

                }
            }
            else
            {
                if (opt == 2)
                    sql = " SELECT arrival, sdno, matnrdesc, charg, lgort, " +
                          " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                          " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                          " FROM hiordi " +
                          " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                          "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                          " group by sdno, arrival, matnrdesc, charg, lgort " +
                          " ORDER BY sdno, arrival, ordi_size desc, matnrdesc, charg, lgort ";
                else
                    sql = " SELECT arrival, sdno, matnrdesc, charg, lgort, " +
                          " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                          " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, min(cmmt) as cmmt,  max(rmrk) as rmrk " +
                          " FROM hiordi " +
                          " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                          "   and  lgort <> '' and charg <> '0' and qty <> 0 " +
                          " group by sdno, arrival, matnrdesc, charg, lgort " +
                          " ORDER BY sdno, arrival, ordi_size desc, matnrdesc, charg, lgort ";

            }


            return sql;
        }
        private string getSSqlTaordi1(string date, string carno, int seq, string arr)
        {
            string sql;
            if (!hist)
            {
                if (opt == 2)
                {
                    sql = " SELECT sdno, isnull(max(cmmt),'') as cmmt " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno" +
                    " ORDER BY sdno ";

                }
                else
                {
                    sql = " SELECT sdno, isnull(max(cmmt),'') as cmmt " +
                             " FROM taordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno" +
                    " ORDER BY sdno ";
                }
            }
            else
            {
                if (opt == 2)
                {
                    sql = " SELECT sdno, isnull(max(cmmt),'') as cmmt " +
                             " FROM hiordi" +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno" +
                    " ORDER BY sdno ";

                }
                else
                {
                    sql = " SELECT sdno, isnull(max(cmmt),'') as cmmt " +
                             " FROM hiordi  " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' and arrival = '" + arr + "'" +
                             " group by sdno" +
                    " ORDER BY sdno ";
                }

            }
            return sql;
        }
        private string getSSqlTaordi2(string date, string carno, int seq)
        {
            string sql;
            if (!hist)
            {
                if (opt == 2)
                {
                    sql = " SELECT arrival, matnrdesc, charg, lgort, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi  " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             " group by arrival, matnrdesc, charg, lgort " +
                    " ORDER BY arrival, ordi_size desc, matnrdesc, charg, lgort ";


                }
                else
                {
                    sql = " SELECT arrival,  matnrdesc, charg, lgort, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM taordi  " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             " group by arrival, matnrdesc, charg, lgort " +
                             " ORDER BY arrival, ordi_size desc,  matnrdesc, charg, lgort ";
                }
            }
            else
            {
                if (opt == 2)
                {
                    sql = " SELECT arrival,  matnrdesc, charg, lgort, " +
                         " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                         " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                         " FROM hiordi " +
                         " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                         " group by arrival, matnrdesc, charg, lgort " +
                         " ORDER BY arrival, ordi_size desc,  matnrdesc, charg, lgort ";

                }
                else
                {
                    sql = " SELECT arrival, matnrdesc, charg, lgort, " +
                             " max(wecust_name1) as wecust_name1, max(remark) as remark, max(ordi_size) as ordi_size, " +
                             " sum(qty) as qty, sum(ordi_ltqty) as ordi_ltqty, max(cmmt) as cmmt,  max(rmrk) as rmrk " +
                             " FROM hiordi " +
                             " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                             " group by arrival, matnrdesc, charg, lgort " +
                             " ORDER BY arrival, ordi_size desc, matnrdesc, charg, lgort ";
                }

            }
            return sql;
        }

        private string getUSqlTaordi(string date, string carno, int seq)
        {
            string sql;

            if (!hist)
                sql = "update taordi " +
                      "   set print_step = '2' " +
                      "  FROM taordi " +
                       " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' ";
            else
                sql = "update haordi " +
                      "   set print_step = '2' " +
                      "  FROM hiordi " +
                       " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND  car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' ";

            return sql;
        }
    }
    public class tacarp
    {
        public string bachadate { get; set; }
        public string car_no { get; set; }
        public int seq { get; set; }
        public string car_desc { get; set; }
        public string car_man { get; set; }
        public string car_dest { get; set; }
        public decimal max_vol { get; set; }
        public decimal load_vol { get; set; }
        public decimal load_qty { get; set; }
        public string step { get; set; }
        public string remark { get; set; }
    }
}
