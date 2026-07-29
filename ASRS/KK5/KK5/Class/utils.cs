using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Transactions;

namespace KK5
{
    #region ActionTextWriter usage ----------
    //db.Log = new ActionTextWriter(s => MessageBox.Show(s));

    class ActionTextWriter : TextWriter
    {
        private readonly Action<string> action;

        public ActionTextWriter(Action<string> action)
        {
            this.action = action;
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new string(buffer, index, count));
        }

        public override void Write(string value)
        {
            action.Invoke(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
    #endregion

    #region DebugTextWriter usage
    //1. db.Log = new DebugTextWriter();
    //2. To a file
    //#if DEBUG
    //    db.Log = new System.IO.StreamWriter("linq-to-sql.log") { AutoFlush = true };
    //#endif

    class DebugTextWriter : System.IO.TextWriter
    {
        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debug.Write(new String(buffer, index, count));
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
    #endregion

    class utils
    {
        public static string f_gethogi(string lstk)
        {
            string ret = "0";

            switch (lstk.Substring(1, 2))
            {
                case "01":
                case "02":
                    ret = "1";
                    break;
                case "03":
                case "04":
                    ret = "2";
                    break;
                case "05":
                case "06":
                    ret = "3";
                    break;
                case "07":
                case "08":
                    ret = "4";
                    break;
                case "09":
                case "10":
                    ret = "5";
                    break;
                default:
                    break;
            }
            return ret;
        }

        #region --- 라벨 폼 ----------

        public static string f_mk_label1(string apltno, string desc, string alot, int aqty, decimal acts)
        {

            string ls_ret =
                "^XA~TA000~JSN^LT0^MMT^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4^MD0^JUS^LRN^CI0^XZ" +
                "^XA^LL0543" +
                "^PW543" +
                "^BY5,3,143^FT169,462^BCB,,N,N" +
                "^FD>;" + apltno + "^FS" +
                "^FT206,475^A0N,68,67^FH\\^FD" + apltno + "^FS" +
                "^FT206,123^A0N,39,38^FH\\^FD" + desc.Substring(0, desc.Length) + "^FS" +
                "^FT345,287^A0N,28,28^FH\\^FDEA^FS" +
                "^FT336,348^A0N,39,38^FH\\^FD" + acts.ToString("0.000") + "^FS" +
                "^FT279,290^A0N,39,38^FH\\^FD" + aqty.ToString("0") + "^FS" +
                "^FT206,286^A0N,28,28^FH\\^FDQTY :^FS" +
                "^FT435,344^A0N,23,24^FH\\^FDLT^FS" +
                "^FT207,409^A0N,28,28^FH\\^FDP/L NO^FS" +
                "^FT206,346^A0N,28,28^FH\\^FDContents:^FS" +
                "^FT206,184^A0N,28,28^FH\\^FDBatch No:^FS" +
                "^FT206,227^A0N,39,38^FH\\^FD" + alot + "^FS" +
                "^FT206,82^A0N,28,28^FH\\^FDPRODUCT^FS" +
                "^PQ1,0,1,Y^XZ";

            return ls_ret;
        }

        public static string f_mk_label1_r(string apltno, string desc, string alot, int aqty, decimal acts)
        {
            string ls_ret =
                "^XA~TA000~JSN^LT0^MMT^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4^MD0^JUS^LRN^CI0^XZ" +
                "^XA^LL0543" +
                "^PW543" +
                "^BY5,3,143^FT514,473^BCB,,N,N" +
                "^FD>;" + apltno + "^FS" +
                "^FT17,467^A0N,68,67^FH\\^FD" + apltno + "^FS" +
                "^FT17,114^A0N,39,38^FH\\^FD" + desc.Substring(0, desc.Length) + "^FS" +
                "^FT156,276^A0N,28,28^FH\\^FDEA^FS" +
                "^FT146,339^A0N,39,38^FH\\^FD" + acts.ToString("0.000") + "^FS" +
                "^FT90,281^A0N,39,38^FH\\^FD" + aqty.ToString("0") + "^FS" +
                "^FT17,277^A0N,28,28^FH\\^FDQTY :^FS" +
                "^FT246,336^A0N,23,24^FH\\^FDLT^FS" +
                "^FT17,400^A0N,28,28^FH\\^FDP/L NO^FS" +
                "^FT17,337^A0N,28,28^FH\\^FDContents:^FS" +
                "^FT17,175^A0N,28,28^FH\\^FDBatch No:^FS" +
                "^FT17,219^A0N,39,38^FH\\^FD" + alot + "^FS" +
                "^FT17,73^A0N,28,28^FH\\^FDPRODUCT^FS" +
                "^PQ1,0,1,Y^XZ";

            return ls_ret;
        }

        public static string f_mk_label2(string apltno, int aqty, decimal acnt)
        {
            string ls_ret =
                "^XA~TA000~JSN^LT0^MMT^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4^MD0^JUS^LRN^CI0^XZ" + 
                "^XA^LL0543" + 
                "^PW543" + 
                "^BY5,3,145^FT168,469^BCB,,N,N" + 
                "^FD>;" + apltno + "^FS" + 
                "^FT200,477^A0N,68,67^FH\\^FD" + apltno + "^FS" + 
                "^FT322,286^A0N,28,28^FH\\^FDEA^FS" + 
                "^FT273,291^A0N,39,38^FH\\^FD" + aqty.ToString("0") + "^FS" + 
                "^FT200,287^A0N,28,28^FH\\^FDQTY :^FS" + 
                "^FT428,344^A0N,23,24^FH\\^FDLT^FS" + 
                "^FT201,414^A0N,28,28^FH\\^FDP/L NO^FS" + 
                "^FT200,348^A0N,28,28^FH\\^FDContents:^FS" + 
                "^FT200,185^A0N,28,28^FH\\^FDBatch No:^FS" + 
                "^FT200,84^A0N,28,28^FH\\^FDPRODUCT^FS" + 
                "^FT342,148^A0N,104,124^FH\\^FD" + acnt.ToString("0") + "^FS" + 
                "^PQ1,0,1,Y^XZ";

            return ls_ret;
        }

        public static string f_mk_label2_r(string apltno, int aqty, decimal acnt)
        {
            string ls_ret =
                "^XA~TA000~JSN^LT0^MMT^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4^MD0^JUS^LRN^CI0^XZ" +
                "^XA^LL0543" +
                "^PW543" +
                "^BY5,3,143^FT514,473^BCB,,N,N" +
                "^FD>;" + apltno + "^FS" +
                "^FT17,467^A0N,68,67^FH\\^FD" + apltno + "^FS" +
                "^FT156,276^A0N,28,28^FH\\^FDEA^FS" +
                "^FT90,281^A0N,39,38^FH\\^FD" + aqty.ToString() + "^FS" +
                "^FT17,277^A0N,28,28^FH\\^FDQTY :^FS" +
                "^FT245,333^A0N,23,24^FH\\^FDLT^FS" +
                "^FT17,404^A0N,28,28^FH\\^FDP/L NO^FS" +
                "^FT17,337^A0N,28,28^FH\\^FDContents:^FS" +
                "^FT17,175^A0N,28,28^FH\\^FDBatch No:^FS" +
                "^FT17,73^A0N,28,28^FH\\^FDPRODUCT^FS" +
                "^FT161,135^A0N,68,81^FH\\^FD" + acnt.ToString("0") + "^FS" +
                "^PQ1,0,1,Y^XZ";

            return ls_ret;
        }

     
        #endregion

        public static void importmiplti(DBDataContext ctx)
        {
            string[] lines  = System.IO.File.ReadAllLines(@"c:\kk5d\aws\miplti_data2.txt", Encoding.UTF8);
            int i = 0;
            string[] ss;
            ctx.ExecuteCommand("delete from miplti");
            ctx.SubmitChanges();
            try
            {
                foreach (string str in lines)
                {
                    i++;
                    if (i == 1) continue;
                    char tab = '\t';
                    ss = str.Split(tab).ToArray();

                    miplti p = new miplti();
                    p.plti_pltno = ss[0];
                    p.plti_lstk = ss[1];
                    p.plti_prod = ss[2];
                    p.plti_oprod = ss[2];
                    p.plti_pdesc = ss[2];
                    p.plti_loc = ss[3];
                    p.plti_lot = ss[4];
                    p.plti_bestq = "";
                    p.plti_pksz = Convert.ToDecimal(ss[7]);
                    p.plti_remark = ss[8];
                    p.plti_icust = ss[9];
                    p.plti_stok = Convert.ToDecimal(ss[10]);
                    p.plti_rqty = Convert.ToDecimal(ss[11]);
                    p.plti_cycl_date = ss[12];
                    p.plti_idate = ss[13];
                    p.plti_itime = ss[14];
                    p.plti_flag = ss[15];
                    p.plti_label = ss[16];

                    ctx.mipltis.InsertOnSubmit(p);
                    ctx.SubmitChanges(); 
                   
                }

            }
            catch (Exception E) { MessageBox.Show(E.Message); }
          
        }

        public static string f_curgetdatetime14(DBDataContext ctx)
        {
            string s = ctx.ExecuteQuery<string>("select convert(char(19), getdate(), 121) from tbstat ").SingleOrDefault();

            s = s.Substring(0, 4) + s.Substring(5, 2) + s.Substring(8, 2) + s.Substring(11, 2) + s.Substring(14, 2) + s.Substring(17, 2);
            
            return s;
        }
        public static string f_get_indx_jno(DBDataContext ctx, char ac)
        {
            string ls = f_curgetdatetime14(ctx);
         
            if (ac == '1')
            {
                var q = ctx.tbseqns.Where(m => m.seqn_key == '1').Select(k => k).SingleOrDefault();
                if (q == null) return "";

                if (ls.Substring(0, 8) != q.seqn_date.Trim())
                {
                    q.seqn_date = ls.Substring(0, 8);
                    q.seqn_no = 1;
                }
                else q.seqn_no = q.seqn_no + 1;

                string indx = q.seqn_no.Value.ToString("0000");
                
                return ls + indx;
            }

            if (ac == '2')
            {
                var q = ctx.tbseqns.Where(m => m.seqn_key == '2').Select(k => k).SingleOrDefault();
                if (q == null) return "";

                if (ls.Substring(0, 8) != q.seqn_date.Trim())
                {
                    q.seqn_date = ls.Substring(0, 8);
                    q.seqn_no = 2000;
                }
                else q.seqn_no = q.seqn_no + 1;

                string indx = q.seqn_no.Value.ToString("0000");
               
                return ls + indx;
            }

            if (ac == '3')
            {
                var q = ctx.tbseqns.Where(m => m.seqn_key == '3').Select(k => k).SingleOrDefault();
                if (q == null) return "";

                if (ls.Substring(0, 8) != q.seqn_date.Trim())
                {
                    q.seqn_date = ls.Substring(0, 8);
                    q.seqn_no = 5000;
                }
                else q.seqn_no = q.seqn_no + 1;

                string indx = q.seqn_no.Value.ToString("0000");
                
                return ls + indx;
            }
            if (ac == '4')
            {
                var q = ctx.tbseqns.Where(m => m.seqn_key == '4').Select(k => k).SingleOrDefault();
                if (q == null) return "";

                if (ls.Substring(0, 8) != q.seqn_date.Trim())
                {
                    q.seqn_date = ls.Substring(0, 8);
                    q.seqn_no = 9000;
                }
                else q.seqn_no = q.seqn_no + 1;

                string indx = q.seqn_no.Value.ToString("0000");
                
                return ls + indx;
            }
            return "";
        }

    }
}
