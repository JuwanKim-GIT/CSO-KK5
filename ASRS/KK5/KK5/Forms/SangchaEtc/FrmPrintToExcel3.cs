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
    public partial class FrmPrintToExcel3 : Form
    {
        public FrmPrintToExcel3()
        {
            InitializeComponent();
        }
        #region -- variables definition ---------------
        Workbook workbook = new Workbook();
        Worksheet sheet = null;
      

        int row = 20;

        string tacarSql = string.Empty;
        string tawmtoSql = string.Empty;
        string update_tawmtoSql = string.Empty;

        Boolean lt75 = false;
     
        string soPrev = string.Empty;
        string soCurr = string.Empty;

        tacarp rowHeader = null;
        #endregion

        string bachadate = "";
        string car_no = "";
        int seq = 0;
        bool hist = false;

        public FrmPrintToExcel3(string bacha, string car, int seq, bool hist)
        {
            InitializeComponent();

            this.bachadate = bacha;
            this.car_no = car;
            this.seq = seq;
            this.hist = hist;
        }

        private void FrmPrintToExcel3_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
         
            PrintExcel();
        }
        private void PrintExcel()            ///////////////////
        {          
            if (checkBox1.Checked) lt75 = true;

            MakeSql();
           
            workbook.LoadFromFile("sampleetc.xlsx");
                              
            if (!PrintHeader())
            {
                MessageBox.Show("차량번호가 존재하지 않읍니다...!");
                Close();
                return;
            }
            try
            {
              
                sheet = workbook.Worksheets[0];
                
                PrintBody();
               
                sheet.DeleteRow(7, 13);
                sheet.Name = rowHeader.car_no + "_Doc";
                                
                
                int rc = 0;
                string msg = string.Empty;
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    rc = db.ExecuteCommand(update_tawmtoSql);
                    if (rc == 0) MessageBox.Show("에러", "Update 실패...!");
                }
                DateTime date = DateTime.Now;

                string year = date.Year.ToString("0000") + "년";
                string mon = date.Month.ToString("00") + "월";
                string day = date.Day.ToString("00") + "일";
                string dir;
                bachadate = bachadate.Replace("/", "");

                if (!hist)
                    dir = "c:\\asrs" + "\\G\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";
                else
                    dir = "c:\\asrs" + "\\GH\\" + year + "\\" + mon + "\\" + day + "\\" + bachadate + "_" + car_no + "_" + seq.ToString() + ".xlsx";
              
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
        private void PrintBody()  // sales order / doc 단위
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
            
                var q = db.ExecuteQuery<loadinglistetc>(tawmtoSql).ToList();
              
                if (q == null) return;
                if (q.Count == 0) return;

                string pr_rmrk = "", pr_cmmt = "";
              
                foreach (var r in q)
                {
                    soCurr = r.docnum;
                 
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

                        //sheet.Range[row, 3].Text = vgbel;
                        sheet.Range[row, 3].Text = soCurr;
                        sheet.Range[row, 9].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                   
                        row = row + 2;
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
                      
                        str = r.maktx;
                        string[] strs = str.Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray();
                        if (strs.Length != 0)
                            str = strs[strs.Length - 1].Trim();
                        else
                        {
                            str = r.maktx;
                        }
                    }
                    
                    // move data
                    sheet.Range[row, 1].NumberValue = (double)r.tapos;
                    sheet.Range[row, 2].Text = r.matnr;

                    if (str == null)
                        sheet.Range[row, 3].Text = "";
                    else
                        sheet.Range[row, 3].Text = str;

                    
                    sheet.Range[row, 4].NumberValue = (double)r.pksz;
                    sheet.Range[row, 5].NumberValue = (double)r.vsolm;
                    sheet.Range[row, 7].Text = r.charg;
                    sheet.Range[row, 8].Text = r.lgort;
                    sheet.Range[row, 9].Text = r.bigo;
                  
                    CurArr = r.arrival;

                    if (!lt75)
                        oqty = oqty + (double)r.vsolm;
                    else
                    {
                        if (r.pksz >= 7.5m)
                            oqty = oqty + (double)r.vsolm;
                    }

                    ltqty = ltqty + (double)r.pksz * (double)r.vsolm;

                    row++;

                    //pr = r; // save lastrow
                    pr_rmrk = r.remark;

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

        //private void getorders(string arr, out string orders, out string cmmt)
        //{
        //    orders = string.Empty;
        //    cmmt = string.Empty;

        //    string cmt = string.Empty;
        //    bool first = true;

        //    string sql = getSSqlTaordi1(rowHeader.bachadate, rowHeader.car_no, rowHeader.seq, arr);

        //    using (DBDataContext db = new DBDataContext(Config.DBCon))
        //    {
        //        var q = db.ExecuteQuery(sql).ToList();
        //        if (q == null) return;
        //        if (q.Count == 0) return;
        //        try
        //        {
        //            foreach (var r in q)
        //            {
        //                if (first)
        //                {
        //                    orders = r.sdno;
        //                }
        //                else
        //                {
        //                    orders = orders + "," + r.sdno;
        //                }
        //                cmt = r.cmmt;
        //                if (cmt.Length >= cmmt.Length) cmmt = cmt;
        //                first = false;
        //            }
        //        }
        //        catch (Exception E) { MessageBox.Show("Getorders " + E.Message); }


        //    }

        //}
        private void MakeSql()
        {

            tacarSql = getSSqlTacar(bachadate, car_no, seq);

            tawmtoSql = getSSqlTawmto(bachadate, car_no, seq);


            update_tawmtoSql = getUSqlTawmto(bachadate, car_no, seq);
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
        private string getSSqlTawmto(string date, string carno, int seq)
        {
            string sql;
            if (!hist)
            {
              
                sql = " SELECT docnum, tanum, tapos, matnr, maktx, charg, lgort," +
                            " max(remark) as remark, max(isnull(pksz, 0)) as pksz, " +
                            " sum(vsolm) as vsolm, sum(vsolm * pksz) as ordi_ltqty, max(remark) as remark,  max(bigo) as bigo " +
                            " FROM tawmto " +
                            " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' " +
                            "   and  lgort <> '' and charg <> '0' and vsolm <> 0 " +
                            " group by docnum, tanum, tapos, matnr, maktx, charg, lgort " +
                            " ORDER BY docnum, pksz desc, tapos, maktx, charg, lgort ";
                 

                
            }
            else
            {
                sql = " SELECT docnum, tanum, tapos, matnr, maktx, charg, lgort," +
                     " max(remark) as remark, max(isnull(pksz, 0)) as pksz, " +
                     " sum(vsolm) as vsolm, sum(vsolm * pksz) as ordi_ltqty, max(remark) as remark,  max(bigo) as bigo " +
                     " FROM hawmto " +
                     " WHERE  bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '2' and ordi_check = '' " +
                     "   and  lgort <> '' and charg <> '0' and vsolm <> 0 " +
                     " group by docnum, tanum, tapos, matnr, maktx, charg, lgort " +
                     " ORDER BY docnum, pksz desc, tapos, maktx, charg, lgort ";


            }


            return sql;
        }

        private string getUSqlTawmto(string date, string carno, int seq)
        {
            string sql;

            if (!hist)
                sql = "update tawmto " +
                      "   set print_step = '2' " +
                       " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND car_sno = " + seq.ToString() + " AND print_step = '1' and ordi_check = '' ";
            else
                sql = "update hawmto " +
                      "   set print_step = '2' " +
                      " WHERE bachadate = '" + date + "' AND car_no ='" + carno + "' AND  car_sno = " + seq.ToString() + " and ordi_check = '' ";

            return sql;
        }


    }

    
    public class loadinglistetc
    {
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public int tapos { get; set; }
        public string matnr { get; set; }
        public string maktx { get; set; }
        public string charg { get; set; }
        public string lgort { get; set; }
        public string remark { get; set; }
        public string bigo { get; set; }
        public decimal pksz { get; set; }
        public decimal vsolm { get; set; }
        public string arrival { get; set; }
    }
}
