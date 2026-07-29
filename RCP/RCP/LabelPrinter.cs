using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;

namespace RCP
{
    class LabelPrinter
    {
       
        public BackgroundWorker bwprn = new BackgroundWorker();
        public LabelPrinter()
        {
            bwprn.DoWork += Bwprn_DoWork;
            bwprn.WorkerReportsProgress = true;
            bwprn.WorkerSupportsCancellation = true;
        }
        public void run_labelprint()
        {            
            bwprn.RunWorkerAsync();
        }
        public void stop_labelprint()
        {
            bwprn.CancelAsync();
        }
        private void sendlabel(string label)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("10.63.44.29"), 9100);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.SendTimeout = 1000;
                sender.ReceiveTimeout = 1000;

                try
                {
                    sender.Connect(remoteEP, new TimeSpan(0,0,2));
                    bwprn.ReportProgress(1, "Socket connected to " + sender.RemoteEndPoint.ToString());
                    
                    byte[] msg = Encoding.ASCII.GetBytes(label);
                    int bytesSent = sender.Send(msg);
                    bwprn.ReportProgress(1, "send data = " + label);

                    Thread.Sleep(100);
                    //int bytesRec = sender.Receive(bytes);                    
                    //bwprn.ReportProgress(1, "Response data = " +  Encoding.ASCII.GetString(bytes, 0, bytesRec));
                                        
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (SocketException se)
                {
                    bwprn.ReportProgress(3, "SocketException :" + se.ToString());
                }
                catch (Exception e)
                {
                    bwprn.ReportProgress(3, "Unexpected exception:" + e.ToString());
                }
            }
            catch (Exception e)
            {
                bwprn.ReportProgress(3, e.ToString());
            }


        }
        private void Bwprn_DoWork(object sender, DoWorkEventArgs e)
        {
            bwprn.ReportProgress(1, "라벨프린트 프로그램 succesfully loaded...!");
            while (!bwprn.CancellationPending)
            {
                mainproc();
                Thread.Sleep(1000);
                bwprn.ReportProgress(2, "");
            }
        }
        private void mainproc()
        {
            string ls_sdata = string.Empty;
            string pltno = string.Empty;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string lr = db.ExecuteQuery<string>("select stat_lr from tbstat where stat_key = '1' ").SingleOrDefault();
                if (lr == null) return;

                var q = db.ExecuteQuery<tbbprnq>(@"select top 1 prn_pltno, prn_pdesc, prn_lot, prn_qty, prn_pksz, prn_mixcnt from tbbprn
                                                    where prn_no = '1' order by prn_pltno ").SingleOrDefault();
                if (q == null) return;

                db.ExecuteCommand(@"delete from tbbprn where prn_no = '1' and prn_pltno = {0} ", q.prn_pltno);
                db.ExecuteCommand(@"update miplti set plti_label = '1' where plti_pltno = {0}", q.prn_pltno);

                string str = q.prn_pdesc;

                //string[] strs = str.Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                //if (strs.Length != 0)
                //    str = strs[strs.Length - 1].Trim();
              
                if (q.prn_mixcnt == 1)
                {
                    if (lr == "L") ls_sdata = f_mk_label1(q.prn_pltno, str, q.prn_lot, q.prn_qty, q.prn_pksz);
                    else ls_sdata = f_mk_label1_r(q.prn_pltno, str, q.prn_lot, q.prn_qty, q.prn_pksz);
                }
                else
                {
                    if (lr == "L") ls_sdata = f_mk_label2(q.prn_pltno, q.prn_qty, q.prn_mixcnt);
                    else ls_sdata = f_mk_label2_r(q.prn_pltno, q.prn_qty, q.prn_mixcnt);
                }        
                pltno = q.prn_pltno;               
            }

            sendlabel(ls_sdata);
            bwprn.ReportProgress(1, "파렛번호: " + pltno);

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
    }
    public class tbbprnq 
    {
        public string prn_no { get; set; }
        public string prn_pltno { get; set; }
        public string prn_prod { get; set; }
        public string prn_pdesc { get; set; }
        public string prn_lot { get; set; }
        public decimal prn_pksz { get; set; }
        public int prn_qty { get; set; }
        public int prn_mixcnt { get; set; }
        public DateTime prn_date { get; set; }
        public string prn_flag { get; set; }
    }
}
