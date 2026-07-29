using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data;
using System.Transactions;
using System.Transactions.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;

namespace RCP
{
    public class CVC
    {       
        //DBDataContext db = new DBDataContext(Config.DBCon);
        bool sPortOpened = false;
        public bool showmsg = true; 
        public BackgroundWorker bw = null;
        SerialPort sPort;
        
        #region --- global variable 선언
      
        const char STX = (char)0x2;
        const char ETX = (char)0x3;
        const char EOT = (char)0x4;
        const char ENQ = (char)0x5;
        const char ACK = (char)0x6;
        const char NAK = (char)0x15;
        const char XCR = (char)0x0D;
        const char XLF = (char)0x0A;

        int gi_lan_err = 0;
        int gi_comm_err = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // CVC BUFFER
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        //Send Buffer
        byte[] gb_sData = new byte[2048];      //blob Send Buffer ( Binary )
        public string gs_sData   = "";                //Send Buffer ( Hexa or Ascii string )

        int[] send_iwd = new int[120];                     //Send Buffer For Integer
        string[] send_hwd = new string[120];               //Send Buffer For Hexas
        string[] send_bwd = new string[120];               //Send Buffer For 16Bits

        //Recv Buffer
        //byte[] gb_rData = new byte[2048];                  //Read Buffer ( Binary )
        //string gs_rData = "";                              //Read Buffer ( Hexa or Ascii string )

        ///////////////////////////////////////////////////////////////////////
        long gfail_cnt = 0;

        ////////cnvc ///////////////
        //n_tty u_tty

        bool port_ok = false;
        bool disp_on = false;
        bool disp_clear = false;

        int li_handle = 0;
        string inifile = "";

        //***********************************************************/
        // DATA BUFFER AREA                                         */ 
        //***********************************************************/
        public string prev_ibuf = "";
        public string inpt_buff = "", oupt_buff = "";
        int g_cvc_com = 0;  // Conveyor 통신상태 0 = 정상   0 > 비정상(통신 불능상태)
        int g_srch_no = 0;  // Conveyor Writ버퍼 탐색번호

        //***************************************************************
        // CNVC INPT-SIGNAL
        //***************************************************************
        public char[] cv_op_onof = new char[8];
        public char[] cv_op_eror = new char[8];
        public char[] cv_buf_palt = new char[50];

        public char cv_21_rqst;
        public char cv_22_rqst;
        public char cv_remote;
        public char cv_24_rqst;

        public char[] cv_ist_redy = new char[5], cv_ist_palt = new char[5]; // st= 1,3,5,7,9
        public char[] cv_ost_redy = new char[5], cv_ost_palt = new char[5]; // st= 2,4,6,8,10
        public string cv_stop = "0";
        public string[] cv_chdt = new string[6];
        public string[] cv_job_no = new string[47];                         // st= 1 - 10,21,22,43,45,50 

        //***************************************************************
        // SCC  CONTROL-POWER DATA
        //***************************************************************
        public char[] sc_pwr_onof = new char[8];
        public char[] sc_eror_stat = new char[8];
        public string prev_sc_pwr = "", prev_sc_eror = "";
        int g_sc_pwr = 0;

        string scc_mode, scc_gubn, scc_io, scc_onln, scc_pwron;
        string scc_stat, scc_palt, scc_posi, scc_eror, scc_ecod;
        string scc_stop, scc_iuse, scc_ouse, scc_emer;
        string scc_lstk, scc_pltn, scc_jno, scc_indx, scc_fstn, scc_tstn, scc_xmov;
        string scc_mesg, scc_chdt, scc_comm, scc_rset;

        //***************************************************************
        // 콘베어 쓰기 성공시 
        //***************************************************************
        int g_wflg = 0;                     // 1=쓰기성공 0=쓴것이 없음
        string g_wfstn="", g_wjno="";            // 쓰기성공한 작업정보  

        //***************************************************************
        // 콘베어 입고모드/ 입고selector sw 
        //***************************************************************
        string imode = "0"; // '0' : 입고선택 '1' :바코드입고
        string ipath = "3"; // '0' : 입고중지 '1':창고입고 '2':공장입고 '3':양족입고
        string barm = "0";  // '0' : 중지(수작업) '1':출고대이동 43

        string in_sw2 = "0";   // 0/1 창고/공장
        string in_sw = "0";    // 0/1 창고/공장
        string aux_24_rqst = "";
        string pls_24_rqst = "";
        string barData = "";

        // 유독물
        int ghogi3 = 4;
        string disp_msg = "";

        public string debugstep = "0";
        #endregion

        public CVC()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            
            bw.DoWork += Bw_DoWork;
        }
        public void Run_cvcproc()
        {
            if (bw.IsBusy) return;
            bw.RunWorkerAsync();
        }
        public void stop_cvcproc()
        {
            bw.CancelAsync(); ;
        }
        private void Bw_DoWork(object sender, DoWorkEventArgs e)  // ue_main_proc
        {           
            f_m_init_proc();
           
            while (!bw.CancellationPending)
            {
                try
                {
                    if (!sPortOpened)
                    {
                        f_msg(1, "컨베이 통신 포트를 열수 없읍니다...1");
                        Thread.Sleep(1000);
                    }
                    else f_m_main_proc();
                }
                catch (Exception E)
                {
                    f_msg(1, debugstep + " Bw_DoWork=" + E.Message);
                }        

                Thread.Sleep(400);
                bw.ReportProgress(2, "");
            }
            f_m_end_proc();
        }

        private void delay()
        {
            Thread.Sleep(20);
        }
        private void f_msg(int num, string msg)
        {
            if (!showmsg) return;
            bw.ReportProgress(num, msg);
        }
        private int f_barcode_err(string apltno, string amsg)
        {
            debugstep = "1";
            //**********************************************
            // barcode error reporting
            //**********************************************
            // 별도 db connection
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    string dtstr = "";
                    d.p_curgetdatetime19(ref dtstr);

                    string ls_date = dtstr.Substring(0, 10);
                    string ls_time = dtstr.Substring(11, 8);

                    d.ExecuteCommand(@"insert into tbberr ( err_date, err_time, err_pltno, err_msg ) values ({0}, {1}, {2}, {3} )", ls_date, ls_time, apltno, amsg);
                }
            }
            catch (Exception E) { }
            return 1;
        }
        private void f_m_init_proc()
        {
            debugstep = "2";
            // barcode 초기화
            try
            {
                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand(@"update tibarc set barc_pltno = '', barc_msg = '', cvc_msg = '', barc_flag = '0' where barc_key = '1'");
                }

                sPort = new SerialPort("COM11", 9600, Parity.Even, 7, StopBits.One);

                sPort.Encoding = Encoding.ASCII;  
                sPort.Open();
                sPortOpened = true;
                bw.ReportProgress(1, "port open success");
            }
            catch(Exception E) { bw.ReportProgress(1, E.Message); }

            f_m_init_setting();
        }
        private void f_m_init_setting()
        {
            debugstep = "3";
            //Initialize all global_variables
            prev_ibuf = "";
            inpt_buff = "";
            oupt_buff = "";
            disp_msg = "";

            cv_op_onof = Fill<char>('0', 8);
            cv_op_eror = Fill<char>('0', 8);
            cv_buf_palt = Fill<char>('0', 50);
            cv_ist_redy = Fill<char>('0', 5);
            cv_ist_palt = Fill<char>('0', 5);
            cv_ost_redy = Fill<char>('0', 5);
            cv_ost_palt = Fill<char>('0', 5);

            g_cvc_com = 0;
            g_srch_no = 0;

            sc_pwr_onof = Fill<char>('0', 8);
            sc_eror_stat = Fill<char>('0', 8);
            prev_sc_pwr = "00000000";
            prev_sc_eror = "00000000";
            g_sc_pwr = 0;

            for (int j = 0; j < 47; j++) cv_job_no[j] = "0000";
            for (int j = 0; j < 6; j++) cv_chdt[j] = "0000000000000000";
            cv_21_rqst = '0';
            cv_22_rqst = '0';
            cv_remote = '1';

            g_wfstn = "";
            g_wjno = "";
            g_wflg = 0;
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"update tbcnvc set cnvc_comm = '0' where cnvc_mode = '01' ");
            }


        }
        private void f_m_end_proc()
        {
            debugstep = "4";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"update tbcnvc set cnvc_comm = '0' where cnvc_mode = '01'");
            }
                
            if (sPortOpened)
            {
                sPort.Close(); sPort.Dispose();
            }
        } 
        private int f_bf2122_proc()
        {
            debugstep = "5";
            //
            //cv_op_onof[5] = '1';
            //cv_op_eror[5] = '0';
            //cv_job_no[26] = "0000";
            //cv_job_no[25] = "0000";
            //cv_buf_palt[22] = '0';
            //cv_buf_palt[23] = '0';
            //cv_job_no[10] = "0000";
            //cv_job_no[11] = "0000";
            //cv_21_rqst = '1';

            // ST-JOBNO ==> 1 - 10,21,22,43,45,50, 11,12,13,14,15,16,17,18,19,20,23,24,25,26,27,28,31,32,33,34,35,36,37,38,39,40,41,42  
            // araay count=        11 12 13 14 15  16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43  

            // 입고 모드 check
            if (imode == "0") return -1;    // 입고선택 모드

            // 바코드 입고모드 
            if (ipath == "0") return -1;    // 입고예약중지

            f_msg(1, "f_bf2122_proc !!");

            // CVC 상태 check
            if (cv_op_onof[5] != '1' || cv_op_eror[5] != '0')
            {
                return -1;
            }
            //f_msg(1, cv_job_no[26] + "-" + cv_job_no[25]);

            if (cv_job_no[26] != "0000") return -1;       // buf #24 job_no
            if (cv_job_no[25] != "0000") return -1;       // buf #23 job_no
            if (cv_buf_palt[22] == '1') return -1;
            if (cv_buf_palt[23] == '1') return -1;
            f_msg(1, "21,22 plt ok.......!!"); 

            if (cv_job_no[10] != "0000" && cv_job_no[11] != "0000") return -1; // buf #21/22 job_no
           
            if (cv_21_rqst == '0' && cv_22_rqst == '0') return -1; // 투입요청 둘다 없으면
           
            string ls_21ok = "0";
            string ls_22ok = "0";
            int lc = 0;

            string lsi = "2", fstn = "", jno = "", indx = "";
            string sql = "";
            int rc = 0;

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                if (cv_21_rqst == '1' && cv_job_no[10] == "0000")
                {
                    lc = d.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '21' and indx_sflg = 'P' ").SingleOrDefault();
                    if (lc == 0) ls_21ok = "1";
                }

                if (cv_22_rqst == '1' && cv_job_no[11] == "0000")
                {
                    lc = d.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '22' and indx_sflg = 'P' ").SingleOrDefault();
                    if (lc == 0) ls_22ok = "1";
                }
            }

            if (ls_21ok == "1" && ls_22ok == "1")
            {
                if (in_sw2 == "0")
                    ls_22ok = "0";
                else
                    ls_21ok = "0";
            }


            if (ls_21ok == "1")
            {
                in_sw2 = "1";
                lsi = "2";
                fstn = "21";
                f_msg(1, "21 ok.......!!");

                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.Connection.open();
                    using (d.Transaction = d.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = d.p_get_indx_jno(lsi, ref jno);    //jno  = f_get_indx_jno(lsi);
                            if (rc == 0)
                            {
                                d.Transaction.Rollback();
                                f_msg(1, "f_bf2122_proc p_get_indx_jno 2 실패...!");
                                return -1;
                            }
                            indx = jno.Substring(jno.Length - 4, 4); //indx = right(jno, 4);

                            sql = @"INSERT INTO tbindx  
 		                              ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
   		                                indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
	     	                            indx_edat,     indx_sflg,       indx_uflg )  
	                            VALUES( {0},           {1},            'A',             'M',            '0',
   	                                    {2},          '24',            '',              '',            'N',
			                            '',            'P',            '0')";

                            rc = d.ExecuteCommand(sql, jno, indx, fstn);
                            if (rc == 0)
                            {
                                d.Transaction.Rollback();
                                f_msg(1, "f_bf2122_proc INSERT INTO tbindx 실패...!");
                            }
                            else
                            {
                                d.Transaction.Commit();
                                f_msg(1, "f_bf2122_proc Commit ok...!");
                            }
                        }
                        catch (Exception E)
                        {
                            d.Transaction.Rollback();
                            f_msg(1, "f_bf2122_proc INSERT INTO tbindx 실패...!");
                        }
                        finally
                        {
                            d.Connection.Close();
                        }
                    }
                }
            }
            else if (ls_22ok == "1")
            {
                in_sw2 = "0";
                lsi = "1";
                fstn = "22";
                f_msg(1, "22 ok.......!!");
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.Connection.open();
                    using (d.Transaction = d.Connection.BeginTransaction())
                    {
                        try
                        {
                            rc = d.p_get_indx_jno(lsi, ref jno);         //jno  = f_get_indx_jno(lsi);
                            if (rc == 0)
                            {
                                f_msg(1, "f_bf2122_proc p_get_indx_jno 1 실패...!");
                                return -1;
                            }
                            indx = jno.Substring(jno.Length - 4, 4);      //indx = right(jno, 4);

                            sql = @"INSERT INTO tbindx  
 		                            ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
   		                                indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
	     	                            indx_edat,     indx_sflg,       indx_uflg )  
	                            VALUES( {0},           {1},           'A',             'M',           '0',
   	                                    {2},           '24',           '',              '',            'N',
			                            '',            'P',            '0')";
                            rc = d.ExecuteCommand(sql, jno, indx, fstn);
                            if (rc == 0)
                            {
                                d.Transaction.Rollback();
                                f_msg(1, "f_bf2122_proc INSERT INTO tbindx 실패...!");
                            }
                            else { d.Transaction.Commit(); }
                        }
                        catch (Exception E)
                        {
                            d.Transaction.Rollback();
                            f_msg(1, "f_bf2122_proc INSERT INTO tbindx 실패...!" + E.Message);
                        }
                        finally
                        {
                            d.Connection.Close();
                        }
                    }
                }
            }
            
            return 1;
        }
        private int f_bf24_proc()
        {
            debugstep = "6";

            // ST-JOBNO ==> 1 - 10,21,22,43,45,50, 11,12,13,14,15,16,17,18,19,20,23,24,25,26,27,28,31,32,33,34,35,36,37,38,39,40,41,42
            // araay count= 0         11 12 13 14 15  16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43

            int lc = 0;
            int rc = 0;
            string sql;
            string pltno = "";
            string indx = "", jno = "";
            string bflag = "";

            ////////////////////////////
            //cv_24_rqst = '1';
            //cv_op_onof[5] = '1';
            //cv_op_eror[5] = '0';
            //cv_job_no[26] = "2001";
            //cv_buf_palt[23] = '1';

            /////////////////////////////

            f_msg(1, "f_bf24_proc");
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                // CVC 상태 check
                bflag = d.ExecuteQuery<string>("select barc_flag from tibarc where barc_key = '1'").SingleOrDefault();
                if (cv_24_rqst == '0')
                {
                    if (bflag == "2")
                    {
                        d.ExecuteCommand("update tibarc set barc_flag = '0', barc_msg = '', cvc_msg = '' where barc_key = '1'");
                    }
                    return -1;
                }

                if (cv_op_onof[5] != '1' || cv_op_eror[5] != '0')
                {
                    f_msg(1, "buf#24 op onof + error = " + cv_op_onof[5] + cv_op_eror[5]);
                    return -1;
                }
                if (cv_job_no[26] == "0000")
                {
                    f_msg(1, "buf#24에 데이타 없음");
                    return -1; // buf #24 job_no
                }
                if (cv_buf_palt[23] == '0')
                {
                    f_msg(1, "buf#24에 PLT 없음");
                    return -1;
                }
                
                //20200607 추가
                if (cv_buf_palt[24] == '1')
                {
                    f_msg(1, "buf#25에 PLT 있음");
                    return -1;
                }
                ///////////////////

                lc = d.ExecuteQuery<int>("select count(*) from tbindx where indx_fstn = '24' and indx_sflg = 'P'").SingleOrDefault();
                if (lc > 0)
                {
                    f_msg(1, "RCP 이미 지시나감P...!");
                    return -1;
                }

                lc = d.ExecuteQuery<int>("select count(*) from tbindx where indx_fstn = '24' and indx_indx = {0}", cv_job_no[26]).SingleOrDefault();
                if (lc > 0)
                {
                    // 이럴수 없는데...
                    //update tibarc set barc_flag = '0', barc_msg = '', cvc_msg = '' where barc_key = '1'; 
                    //commit;	
                    //f_disp_msg(1, "컨베어 지시받음...!")
                    f_msg(1, "이럴수 없는데..!");
                    return -1;
                }
            }

            /////// scaning 작업///////////////
            if (bflag != "1")
            {
                f_scan_proc();
            }
            ///////////////////////////////////

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                var q = d.ExecuteQuery(@"select barc_pltno, barc_flag from tibarc where barc_key = '1'").SingleOrDefault();
                if (q == null) return -1;

                pltno = q.barc_pltno;
                bflag = q.barc_flag;                          
            }

            try
            {
                ///////////////////////////////////////////////////////
                //int rc = f_tilock()           
                ///////////////////////////////////////////////////////

                if (!Microsoft.VisualBasic.Information.IsNumeric(pltno) || pltno.Trim() == "" || pltno.Trim() == "00000000") //NOREAD
                {
                    gfail_cnt++;
                    if (gfail_cnt > 5)
                    {
                        gfail_cnt = 0;
                        if (barm == "1")
                        {
                            f_barcode_err("", "바코드 읽기 실패");
                            goto nn;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    f_msg(1, gfail_cnt.ToString());
                    return -1;
                }

                if (pltno != "00000000" && pltno != "" && bflag == "1") { }  // ok
                else
                {
                    gfail_cnt++;
                    if (gfail_cnt > 5)
                    {
                        if (barm == "1")
                        {
                            f_barcode_err("", "바코드 읽기 실패");
                            goto nn;
                        }
                        else return -1;
                    }
                    return -1;
                }
                gfail_cnt = 0;

                string ls = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); // hh:mm:ss --> HH 24시간 20200626
                string plti_idate = ls.Substring(0, 10).Replace("-", "/");
                string plti_itime = ls.Substring(11, 8);
                string plti_cycl_date = plti_idate;

                //  입고 예약 --------------------------------------------------------
                string lsm = "";
                string loca = "";
                string prod = "";
                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    loca = d.mipltis.Where(w => w.plti_pltno == pltno).Max(m => m.plti_lstk);
                    prod = d.mipltis.Where(w => w.plti_pltno == pltno).Max(m => m.plti_prod);
                   
                }

                f_msg(1, "f_bf24_proc6");
                if (loca == null)
                {
                    lsm = "파렛트번호가 존재하지 않읍니다!(출고처리)";
                    if (barm == "1") // 자동
                    {
                        f_barcode_err(pltno, lsm);
                        goto nn;
                    }
                    else
                    {
                        using (DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                        }
                        return -1;
                    }
                }
                f_msg(1, "f_bf24_proc7");
                if (loca.Substring(0, 1) == "A")
                {
                    lsm = "파렛트번호가 랙에 존재(바코드잘못붙임)";
                    if (barm == "1") // 자동
                    {
                        f_barcode_err(pltno, lsm);
                        goto nn;
                    }
                    else
                    {
                        using (DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                        }
                        return -1;
                    }
                }
                f_msg(1, "f_bf24_proc8");
                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    lc = d.mipltis.Where(x => x.plti_pltno == pltno && x.plti_rqty > 0).Count();
                }
                if (lc > 0)
                {
                    lsm = "출고예약이 되어 있읍니다!";
                    if (barm == "1") // 자동
                    {
                        f_barcode_err(pltno, lsm);
                        goto nn;
                    }
                    else
                    {
                        using (DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                        }
                        return -1;
                    }
                }
                f_msg(1, "f_bf24_proc9" + prod);
                string ls_type = "";
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    ls_type = d.ExecuteQuery<string>(@"select mast_flag from mimast where mast_cd = {0}", prod).SingleOrDefault();
                }                    
                if (ls_type == null)
                {
                    lsm = "제품코드가 존재하지 않읍니다!";
                    if (barm == "1") // 자동
                    {
                        f_barcode_err(pltno, lsm);
                        goto nn;
                    }
                    else
                    {
                        using (DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                        }
                        return -1;
                    }
                }
                f_msg(1, "f_bf24_proc10");
                string ls_lstk = "";
                ls_type = ls_type.Trim();
                if (ls_type != "0" && ls_type != "1" && ls_type != "2" && ls_type != "3") return -1;
                f_msg(1, "f_bf24_proc11");
                ///////////////////////////////             

            
                f_msg(1, "ls_type = " + ls_type);
                switch (ls_type)
                {
                    case "0": ls_lstk = f_get_rsrv_hogi(ls_type); break;
                    case "1": ls_lstk = f_get_rsrv_hogi1(ls_type); break;
                    case "2": ls_lstk = f_get_rsrv_hogi2(ls_type); break;
                    case "3": ls_lstk = f_get_rsrv_hogi3(ls_type); break;
                }

                f_msg(1, "f_bf24_proc99999999");
                if (ls_lstk == "")
                { 
                    lsm = "빈셀이 없읍니다!";
                    if (barm == "1") // 자동
                    {
                        f_barcode_err(pltno, lsm);
                        goto nn;
                    }
                    else
                    {
                        f_msg(1, "빈셀이 없읍니다!");
                        using (DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                        }
                        return -1;
                    }
                }
                f_msg(1, "f_bf24_proc12");

                /////////////////////////////////////////////////////////////////////////////////////
              

                string alstk = "A" + ls_lstk;
                using(DBDataContext dx = new DBDataContext(Config.DBCon))
                {
                    dx.Connection.open();
                    using(dx.Transaction = dx.Connection.BeginTransaction())
                    {
                        rc = dx.ExecuteCommand(@"update milstk set lstk_io = 'I', lstk_stat = 'IX' where lstk_no = {0} and lstk_io  = '0'", alstk);
                        if (rc == 0)
                        {
                            dx.Transaction.Rollback();
                            dx.Connection.Close();

                            lsm = "랙위치 상태가 변했읍니다(랙정보)";
                            if (barm == "1") // 자동
                            {
                                f_barcode_err(pltno, lsm);
                                goto nn;
                            }
                            else
                            {
                                using (DBDataContext d = new DBDataContext(Config.DBCon))
                                {
                                    d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                                }

                                return -1;
                            }
                        }
                        f_msg(1, "f_bf24_proc13");
                        rc = dx.ExecuteCommand(@"Update miplti 
                                            set plti_lstk = {0}, 
                                            plti_cycl_date = {1},  
                                            plti_idate = {2}, 
                                            plti_itime = {3} 
                                         where plti_pltno = {4} and plti_rqty = 0 ",
                                                    alstk, plti_cycl_date, plti_idate, plti_itime, pltno);
                        if (rc == 0)
                        {
                            dx.Transaction.Rollback();
                            dx.Connection.Close();

                            lsm = "재고상태가 변했읍니다(재고정보)";
                            if (barm == "1") // 자동
                            {
                                f_barcode_err(pltno, lsm);
                                goto nn;
                            }
                            else
                            {
                                using (DBDataContext d = new DBDataContext(Config.DBCon))
                                {
                                    d.ExecuteCommand(@"update tibarc set cvc_msg = {0} where barc_key = '1' ", lsm);
                                }
                                return -1;
                            }
                        }
                        string tstn = "";
                        switch (ls_lstk.Substring(0, 2))
                        {
                            case "01":
                            case "02": { tstn = "01"; break; }
                            case "03":
                            case "04": { tstn = "03"; break; }
                            case "05":
                            case "06": { tstn = "05"; break; }
                            case "07":
                            case "08": { tstn = "07"; break; }
                            case "09":
                            case "10": { tstn = "09"; break; }
                            default:
                                {
                                    break;
                                }
                        }

                        // 신작업번호 생성
                        int lhno = (Convert.ToInt32(tstn) + 1) / 2;
                        string ls_hogi = lhno.ToString("0");

                        //// 기존 작업번호 삭제
                        ////Delete from tbindx where indx_indx = :cv_job_no[27] using sqlca;
                        //if (cv_job_no[26] != "0000" && cv_job_no[26] != "")
                        //    dx.ExecuteCommand(@"delete from tbindx where indx_indx = {0}", cv_job_no[26]);
                        

                        string lsii = "";
                        if (Convert.ToInt32(cv_job_no[26]) < 2000) lsii = "2";
                        else lsii = "1";
                                             
                        dx.p_get_indx_jno(lsii, ref jno);
                     
                        indx = jno.Substring(jno.Length - 4, 4);

                        f_msg(1, "indx = " + indx);
                        f_msg(1, "ls_hogi = " + ls_hogi);
                        f_msg(1, "tstn = " + tstn);
                        f_msg(1, "pltno = " + pltno);
                        f_msg(1, "alstk = " + alstk);

                        rc = dx.ExecuteCommand(@"INSERT INTO tbindx  
                                ( indx_jno,      indx_indx,       indx_gubn,     indx_jio,      indx_hogi,   
                                  indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
                                  indx_edat,     indx_sflg,       indx_uflg )  
                        VALUES ( {0},          {1},             'A',             'I',           {2},
                                '24',          {3},             {4},             {5},           'I',
			                    '',            'P',             '0') ", jno, indx, ls_hogi, tstn, pltno, alstk);
                        if (rc <= 0)
                        {
                            dx.Transaction.Rollback();
                            dx.Connection.Close();
                            return -1;
                        }
                        dx.ExecuteCommand(@"update tbhogi set hogi_no = {0}", lhno);
                        dx.ExecuteCommand(@"update tibarc set cvc_msg = ' ' where barc_key = '1' ");
                        dx.Transaction.Commit();  // mistake 누락
                        dx.Connection.Close();
                    }

                }

                return 1;


                nn:;

                gfail_cnt = 0;
                string lsi = "";
                if (Convert.ToInt32(cv_job_no[26]) < 2000) lsi = "2";
                else lsi = "1";
              
                using (DBDataContext dx = new DBDataContext(Config.DBCon))
                {
                    dx.Connection.open();
                    using (dx.Transaction = dx.Connection.BeginTransaction())
                    {
                        try
                        {
                            ////// 기존 작업번호 삭제
                            //if (cv_job_no[26] != "0000" && cv_job_no[26] != "")
                            //    dx.ExecuteCommand(@"delete from tbindx where indx_indx = {0}", cv_job_no[26]);

                            dx.p_get_indx_jno(lsi, ref jno);
                            indx = jno.Substring(jno.Length - 4, 4);

                            dx.ExecuteCommand(@"INSERT INTO tbindx  
                                                  ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
                                                    indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
                                                    indx_edat,     indx_sflg,       indx_uflg )  
                                            VALUES ({0},           {1},             'A',             'M',           '0',
                                                    '24',          '43',            {2},           'Y000000',       'N',
			                                        '',            'P',             '0') ", jno, indx, pltno);

                            dx.ExecuteCommand(@"update tibarc set cvc_msg = ' ' where barc_key = '1' ");

                            gfail_cnt = 0;

                            dx.Transaction.Commit();  // mistake 누락
                        }
                        catch (Exception E)
                        {
                            dx.Transaction.Rollback();
                            if (dx.Connection.State == ConnectionState.Open) dx.Connection.Close();
                        }
                    }
                }

            }
            catch (Exception E) { f_msg(1, "f_bf24_proc = " + E.Message); }
            finally
            {
            }
            

            return 1;
        }
        private void f_cvc_comm_eror()
        {
            debugstep = "7";
            //********************************************************************************************//
            //* CVC와 통신이 않될때
            //********************************************************************************************//
            // 별도 db connection
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand(@"update tbcnvc set cnvc_comm = '0' where cnvc_mode = '01' ");
                }
            }
            catch (Exception E) { }
        }
        private void f_iwait_proc()
        {
            debugstep = "8";
            //**********************************************************************************************************//
            //* 입고 STAN 도착 처리
            //**********************************************************************************************************//
            string ls_tkno = "";
            string ls_indx = "";
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                for (int j = 0; j < 9; j++)
                {
                    ls_indx = cv_job_no[j];
                    if (ls_indx == "0000") continue;

                    if (j == 0) ls_tkno = "01";  // S/C#01 입고대
                    else if (j == 2) ls_tkno = "03";  // S/C#02 입고대
                    else if (j == 4) ls_tkno = "05";  // S/C#03 입고대
                    else if (j == 6) ls_tkno = "07";  // S/C#04 입고대
                    else if (j == 8) ls_tkno = "09";  // S/C#05 입고대
                    else continue;

                    //*******************************************************************************************************//
                    //* 해당 입고대 도착 => S/C 입고 대기중( 'M' -> 'W' )
                    //*******************************************************************************************************//

                    try
                    {
                        string ls_jno = d.ExecuteQuery<string>
                             (@" select min(indx_jno) from tbindx where indx_jio = 'I' and indx_indx = {0} and indx_tstn = {1} and  indx_sflg = 'M'",
                                 ls_indx, ls_tkno).SingleOrDefault();
                        if (ls_jno == null) continue;

                        string dts = "";
                        d.p_curgetdatetime14(ref dts);

                        d.ExecuteCommand(@"update tbindx set indx_edat = {0}, indx_sflg = 'W'
                                        where  indx_jno  = {1}
                                          and  indx_jio  = 'I'
                                          and  indx_indx = {2}
                                          and  indx_tstn = {3}
                                          and  indx_sflg = 'M' ", dts, ls_jno, ls_indx, ls_tkno);
                    }
                    catch (Exception E) { f_msg(1, E.Message + Environment.NewLine + "f_iwait_proc Exception"); }
                }
            }
        }
        private int f_cvc_icvrt2(string as_ibuf)
        {
            debugstep = "9";
            string[] lsp_chdt = new string[6];
            string[] lsp_jno = new string[47];

            string ls_rbuf = as_ibuf;  

            // 전 상태 버퍼 
            for(int j = 0; j < 6; j++) lsp_chdt[j] = cv_chdt[j];
           
            // cv_job_no = 1 - 10, 21, 22, 43, 45, 50
            for (int j = 0; j < 47; j++) lsp_jno[j] = cv_job_no[j];
          
            f_iwait_proc();      // 입고대 대기중 처리 (M -> W)
      
            //**************************************************************************************************************
            // 입력 신호 변환
            //**************************************************************************************************************
            // 신호부분만(6 CHANNEL) 
            string lstr = "";

            // 신호부분만(6 CHANNEL) 
            lstr = "";
            for (int j = 0; j < 24; j++)
            {
                switch (ls_rbuf.Substring(j, 1))
                {
                    case "0": { lstr = lstr + "0000"; break; }
                    case "1": { lstr = lstr + "1000"; break; }
                    case "2": { lstr = lstr + "0100"; break; }
                    case "3": { lstr = lstr + "1100"; break; }
                    case "4": { lstr = lstr + "0010"; break; }
                    case "5": { lstr = lstr + "1010"; break; }
                    case "6": { lstr = lstr + "0110"; break; }
                    case "7": { lstr = lstr + "1110"; break; }
                    case "8": { lstr = lstr + "0001"; break; }
                    case "9": { lstr = lstr + "1001"; break; }
                    case "A": { lstr = lstr + "0101"; break; }
                    case "B": { lstr = lstr + "1101"; break; }
                    case "C": { lstr = lstr + "0011"; break; }
                    case "D": { lstr = lstr + "1011"; break; }
                    case "E": { lstr = lstr + "0111"; break; }
                    case "F": { lstr = lstr + "1111"; break; }
                    default:
                        {
                            return -1;
                            break;
                        }
                }
            }
            string ls = lstr;
       
            string[] ls_rbits = new string[6];
            for(int j = 0; j < 6; j++)
            {
                ls_rbits[j] = ls.Substring(j * 16, 16);
                cv_chdt[j] = ls_rbits[j];
            }

            // CHANNEL #01
            string ls_temp = ls_rbits[0];   

            cv_op_onof[4] = ls_temp[0];             // C/V 자동,수동
            cv_op_onof[5] = ls_temp[1];
            cv_op_onof[6] = ls_temp[2];
            cv_op_onof[7] = ls_temp[3];
            cv_op_onof[0] = ls_temp[4];
            cv_op_onof[1] = ls_temp[5];
            cv_op_onof[2] = ls_temp[6];
            cv_op_onof[3] = ls_temp[7];

            cv_op_eror[4] = ls_temp[8];             // C/V 에러
            cv_op_eror[5] = ls_temp[9];
            cv_op_eror[6] = ls_temp[10];
            cv_op_eror[7] = ls_temp[11];
            cv_op_eror[0] = ls_temp[12];
            cv_op_eror[1] = ls_temp[13];
            cv_op_eror[2] = ls_temp[14];
            cv_op_eror[3] = ls_temp[15];

            // CHANNEL #02
            ls_temp = ls_rbits[1];  

            cv_buf_palt[11] = ls_temp[0];
            cv_buf_palt[1] = ls_temp[1];

            cv_buf_palt[0] = ls_temp[4];
            cv_buf_palt[10] = ls_temp[5];
            cv_buf_palt[30] = ls_temp[6];
            cv_buf_palt[31] = ls_temp[7];

            cv_buf_palt[13] = ls_temp[8];
            cv_buf_palt[3] = ls_temp[9];

            cv_buf_palt[2] = ls_temp[12];
            cv_buf_palt[12] = ls_temp[13];
            cv_buf_palt[32] = ls_temp[14];
            cv_buf_palt[33] = ls_temp[15];

            // CHANNEL #03
            ls_temp = ls_rbits[2]; 

            cv_buf_palt[15] = ls_temp[0];
            cv_buf_palt[5] = ls_temp[1];

            cv_buf_palt[4] = ls_temp[4];
            cv_buf_palt[14] = ls_temp[5];
            cv_buf_palt[34] = ls_temp[6];
            cv_buf_palt[35] = ls_temp[7];

            cv_buf_palt[17] = ls_temp[8];
            cv_buf_palt[7] = ls_temp[9];

            cv_buf_palt[6] = ls_temp[12];
            cv_buf_palt[16] = ls_temp[13];
            cv_buf_palt[36] = ls_temp[14];
            cv_buf_palt[37] = ls_temp[15];
     
            // CHANNEL #04
            ls_temp = ls_rbits[3];  

            cv_buf_palt[19] = ls_temp[0];
            cv_buf_palt[9] = ls_temp[1];
            cv_buf_palt[44] = ls_temp[2];

            cv_buf_palt[8] = ls_temp[4];
            cv_buf_palt[18] = ls_temp[5];
            cv_buf_palt[38] = ls_temp[6];
            cv_buf_palt[39] = ls_temp[7];

            cv_buf_palt[25] = ls_temp[8];
            cv_buf_palt[24] = ls_temp[9];

            cv_buf_palt[20] = ls_temp[12];
            cv_buf_palt[21] = ls_temp[13];
            cv_buf_palt[22] = ls_temp[14];
            cv_buf_palt[23] = ls_temp[15];

            // CHANNEL #05
            ls_temp = ls_rbits[4];

            cv_buf_palt[26] = ls_temp[4];
            cv_buf_palt[27] = ls_temp[5];
            cv_buf_palt[49] = ls_temp[6];
            cv_buf_palt[48] = ls_temp[7];

            cv_buf_palt[40] = ls_temp[12];
            cv_buf_palt[41] = ls_temp[13];
            cv_buf_palt[42] = ls_temp[14];

            cv_buf_palt[45] = ls_temp[10];
            cv_buf_palt[46] = ls_temp[11];
            cv_buf_palt[43] = ls_temp[15];

            // CHANNEL #06
            ls_temp = ls_rbits[5];  

            cv_ost_redy[4] = ls_temp[0];

            cv_24_rqst = ls_temp[1];

            cv_ost_redy[0] = ls_temp[4];
            cv_ost_redy[1] = ls_temp[5];
            cv_ost_redy[2] = ls_temp[6];
            cv_ost_redy[3] = ls_temp[7];

            cv_ist_redy[4] = ls_temp[8];
            cv_21_rqst = ls_temp[9];
            cv_22_rqst = ls_temp[10];
            cv_remote = ls_temp[11];   //항상 1

            cv_ist_redy[0] = ls_temp[12];
            cv_ist_redy[1] = ls_temp[13];
            cv_ist_redy[2] = ls_temp[14];
            cv_ist_redy[3] = ls_temp[15];
            

            // ist,ost palt
            cv_ist_palt[0] = cv_buf_palt[0];
            cv_ist_palt[1] = cv_buf_palt[2];
            cv_ist_palt[2] = cv_buf_palt[4];
            cv_ist_palt[3] = cv_buf_palt[6];
            cv_ist_palt[4] = cv_buf_palt[8];

            cv_ost_palt[0] = cv_buf_palt[1];
            cv_ost_palt[1] = cv_buf_palt[3];
            cv_ost_palt[2] = cv_buf_palt[5];
            cv_ost_palt[3] = cv_buf_palt[7];
            cv_ost_palt[4] = cv_buf_palt[9];
        
            // ST-JOBNO ==> 1 - 10,21,22,43,45,50, 11,12,13,14,15,16,17,18,19,20,23,24,25,26,27,28,31,32,33,34,35,36,37,38,39,40,41,42,44,49.46,47
            // araay count=        11 12 13 14 15  16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43,44,45,46,47

            ls = ls_rbuf;
            string ls_job_str = ls.Substring(24, 60);      // 15 * 4 = 60
            string ls_jobstr = ls.Substring(24, 188);      // 47 * 4 = 188
            for (int j = 0; j < 47; j++)
                cv_job_no[j] = ls_jobstr.Substring(j * 4, 4);

            //********************************************************************************************************//
            //* CVC 정보를 저장한다.
            //********************************************************************************************************//
            int li_update = 0;
            int li_update2 = 0;
            for (int j = 0; j < 6; j++)
            {
                if (lsp_chdt[j] != ls_rbits[j])
                {
                    li_update = 1;
                    break;
                }
            }

            for (int j = 0; j < 47; j++)
            {
                if (lsp_jno[j] != cv_job_no[j])
                {
                    li_update2 = 1;
                    break;
                }
            }
            f_msg(1, "TBCNVC SAVE전..!!");
            if (li_update == 0 && li_update2 == 0)
            {
                return 0;
            }

            f_msg(1, "TBCNVC SAVE..!!");

            string ls_op_onof = new string(cv_op_onof).Substring(0, 8);
            string ls_op_eror = new string(cv_op_eror).Substring(0, 8); 
            string ls_plt_str = new string(cv_buf_palt).Substring(0, 50); 

            string ls_ist_redy = new string(cv_ist_redy).Substring(0, 5); 
            string ls_ist_palt = new string(cv_ist_palt).Substring(0, 5);
            string ls_ost_redy = new string(cv_ost_redy).Substring(0, 5);
            string ls_ost_palt = new string(cv_ost_palt).Substring(0, 5);
            string ls_21_rqst = cv_21_rqst.ToString().Trim();
            string ls_22_rqst = cv_22_rqst.ToString().Trim();
            string ls_remote = cv_remote.ToString().Trim();
            string ls_24_rqst = cv_24_rqst.ToString().Trim();

            ls_job_str = ls_job_str.Substring(0, 60);
            ls_jobstr  = ls_jobstr.Substring(0, 188);

            string sql = @"update tbcnvc set cnvc_ch01 = {0},      cnvc_ch02 = {1}, 
                                             cnvc_ch03 = {2},      cnvc_ch04 = {3},
                                             cnvc_ch05 = {4},      cnvc_ch06 = {5}, 
                                             cnvc_op_onof = {6},   cnvc_op_eror = {7},
                                             cnvc_job_no = {8},    cnvc_buf_palt = {9},
                                             cnvc_ist_redy = {10}, cnvc_ist_palt = {11},
                                             cnvc_ost_redy = {12}, cnvc_ost_palt = {13},
                                             cnvc_21_rqst = {14},  cnvc_22_rqst  = {15},  
						                     cnvc_remote = {16},   cnvc_24_rqst  = {17},
						                     cnvc_jobno = {18},    
                                             cnvc_comm = '1'
                                     where  cnvc_mode = '01'";
            int rc = 0;
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                rc = d.ExecuteCommand(sql, ls_rbits[0], ls_rbits[1],
                           ls_rbits[2], ls_rbits[3],
                           ls_rbits[3], ls_rbits[5],
                           ls_op_onof, ls_op_eror,
                           ls_job_str, ls_plt_str,
                           ls_ist_redy, ls_ist_palt,
                           ls_ost_redy, ls_ost_palt,
                           ls_21_rqst, ls_22_rqst,
                           ls_remote, ls_24_rqst,
                           ls_jobstr);
                if (rc == 0) f_msg(1, "update tbcnvc Error");

                rc = d.ExecuteCommand(@"update tbcnvc set cnvc_jobno = {0} where cnvc_mode = '01' ", ls_jobstr);
            }

            return 0;
        }
        private int f_cvc_info()
        {
            debugstep = "10";
            string ls_stop = "0";
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                try
                {
                    //char ls_s = d.ExecuteQuery<char>(@"select cnvc_stop from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                    ls_stop = d.ExecuteQuery<string>(@"select cnvc_stop from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                    if (ls_stop == null)
                    {
                        ls_stop = "0";
                        return -1;
                    }
                    cv_stop = ls_stop;
                    f_msg(1, "cv_stop=<" + cv_stop + ">");

                    var q = d.ExecuteQuery(@"select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '02'").SingleOrDefault();
                    string ls_sc_pwr_onof = q.cnvc_op_onof;
                    string ls_sc_eror_stat = q.cnvc_op_eror;

                    sc_pwr_onof = ls_sc_pwr_onof.ToCharArray();
                    sc_eror_stat = ls_sc_eror_stat.ToCharArray();

                    if (prev_sc_pwr != ls_sc_pwr_onof || prev_sc_eror != ls_sc_eror_stat)
                    {
                        prev_sc_pwr = ls_sc_pwr_onof;
                        prev_sc_eror = ls_sc_eror_stat;
                        g_sc_pwr = 0;
                    }
                    else
                    {
                        g_sc_pwr = g_sc_pwr + 1;
                        if (g_sc_pwr > 100)
                        {
                            g_sc_pwr = 0;
                            prev_sc_pwr = ls_sc_pwr_onof;
                            prev_sc_eror = ls_sc_eror_stat;
                        }
                    }
                    return 0;
                }
                catch (Exception E) { f_msg(1, "f_cvc_info Error"); }
            }
            return 0;
        }
        private int f_cvc_writ_srch()
        {
            debugstep = "11";
            //****************************************************************************************//
            // CONVEYOR CONTROL PROC - ONLY WRITE PROC
            // cv_job_no = 1 - 10, 21, 22, 43, 45, 50
            //****************************************************************************************//

            string ls_21 = "0";
            string ls_22 = "0";

            string ls_fbuf = "0204060810242150**";  
            g_srch_no = g_srch_no + 1;
            if (g_srch_no > 8) g_srch_no = 1;

            string ls_tkno = ls_fbuf.Substring((g_srch_no - 1) * 2, 2);
            //*******************************************************************************************************//
            //* 콘베어 버퍼 쓰기 조건 체크(fstn-search) 
            //* 만일 출고대 쓰기 실패가 많을 경우 //막아놓은것을 사용한다.
            //*******************************************************************************************************//
          
            switch (ls_tkno)
            {
                case "02":   // S/C#01 출고대
                    if (cv_op_onof[0] != '1' || cv_op_eror[0] != '0' || cv_ost_palt[0] != '1') return -1;
                    if (cv_job_no[1] != "0000") return -1;
                    break;
                case "04":   // S/C#02 출고대
                    if (cv_op_onof[1] != '1' || cv_op_eror[1] != '0' || cv_ost_palt[1] != '1') return -1;
                    if (cv_job_no[3] != "0000") return -1;
                    break;
                case "06":   // S/C#03 출고대
                    if (cv_op_onof[2] != '1' || cv_op_eror[2] != '0' || cv_ost_palt[2] != '1') return -1;
                    if (cv_job_no[5] != "0000") return -1;
                    break;
                case "08":   // S/C#04 출고대
                    if (cv_op_onof[3] != '1' || cv_op_eror[3] != '0' || cv_ost_palt[3] != '1') return -1;
                    if (cv_job_no[7] != "0000") return -1;
                    break;
                case "10":   // S/C#05 출고대
                    if (cv_op_onof[4] != '1' || cv_op_eror[4] != '0' || cv_ost_palt[4] != '1') return -1;
                    if (cv_job_no[9] != "0000") return -1;
                    break;
                case "21":
                case "22":   // ST21 MAIN 입고GATE,   ST22 : 공장동
                    if (ipath == "0") return -1;
                    f_msg(1, "f_cvc_writ_srch() " + ls_tkno);

                    if (cv_op_onof[5] != '1' || cv_op_eror[5] != '0') return -1;
                    if (cv_buf_palt[22] == '1' || cv_buf_palt[23] == '1') return -1;
                    if (cv_job_no[25] != "0000" || cv_job_no[26] != "0000") return -1;  // bf#23,24 Data무
                    if (cv_24_rqst == '1') return -1;

                    f_msg(1, "f_cvc_writ_srch() 2" + ls_tkno);
                    int lc1 = 0;
                    int lc2 = 0;

                    using(DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        lc1 = d.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '21' and indx_sflg = 'P' ").SingleOrDefault();
                        lc2 = d.ExecuteQuery<int>(@"select count(*) from tbindx where indx_fstn = '22' and indx_sflg = 'P' ").SingleOrDefault();
                    }

                    if (lc1 == 0 && lc2 == 0) return -1;

                    if (cv_job_no[10] != "0000" || cv_job_no[11] != "0000") return -1;

                    if (cv_job_no[10] == "0000" && cv_buf_palt[20] == '1' && cv_21_rqst == '1' && lc1 > 0 && (ipath == "1" || ipath == "3"))
                    {
                        ls_21 = "1";
                    }
                    if (cv_job_no[11] == "0000" && cv_buf_palt[21] == '1' && cv_22_rqst == '1' && lc2 > 0 && (ipath == "2" || ipath == "3"))
                    {
                        ls_22 = "1";
                    }
                    f_msg(1, "f_cvc_writ_srch() 3" + ls_tkno);
                    string lcase = ls_21 + ls_22;
                    switch (lcase)
                    {
                        case "10":
                            ls_tkno = "21";
                            in_sw = "1";
                            break;
                        case "01":
                            ls_tkno = "22";
                            in_sw = "0";
                            break;
                        case "11":
                            if(in_sw == "0")
                                  { ls_tkno = "21";  in_sw = "1";  }
                            else  { ls_tkno = "22";  in_sw = "0";  }
                            break;
                        default:
                            return -1;
                            break;
                    }
                    break;
                case "50":// ST50 재입고대
                    if (cv_op_onof[6] != '1' || cv_op_eror[6] != '0' || cv_buf_palt[22] != '1') return -1;
                    if (cv_job_no[14] != "0000") return -1;
                    if (!(cv_buf_palt[22] == '0' && cv_21_rqst == '0' && cv_22_rqst == '1')) return -1;
                    break;

                case "24": // ST24 입고GATE
                    if (cv_op_onof[5] != '1' || cv_op_eror[5] != '0') return -1;
                    if (cv_job_no[26] == "0000") return -1;  // buf #24
                    if (!(cv_buf_palt[23] == '1' && cv_buf_palt[24] == '0' && cv_24_rqst == '1')) return -1;
                    break;
                default:
                    return -2;
                    break;
            }// end of ls_tkno
             //*******************************************************************************************************//
             //* 해당 콘베어 버퍼 쓸데이타 유무 체크 - only 'P'
             //*******************************************************************************************************//
            string ls_jno = "";
            string ls_edat = "";
            tbindx q = null;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                ls_jno = d.ExecuteQuery<string>(@"select min(indx_jno) from tbindx where indx_fstn = {0} and indx_sflg = 'P'", ls_tkno).SingleOrDefault();
                if (ls_jno == null) return -4;
                if (ls_jno == "") return -4;

                f_msg(1, "f_cvc_writ_srch 4");         
        
                q = d.tbindxes.Where(w => w.indx_jno == ls_jno && w.indx_fstn == ls_tkno && w.indx_sflg == "P").SingleOrDefault();
                if (q == null) return -5;
               
                d.p_curgetdatetime14(ref ls_edat);
            }
            //****************************************************************************************************//
            // 해당 입고 호기가 수동이나, 사용금지이면, 통신에러시 출발을 하지 못하게 한다
            //****************************************************************************************************//
            int li_hogi = 0;
            string in_bank = "00";
            if (q.indx_jio == "I")
            {
                if (ls_tkno == "21" || ls_tkno == "22" || ls_tkno == "50" || ls_tkno == "24")
                {
                    switch (q.indx_tstn)
                    {
                        case "01":
                            li_hogi = 0;
                            in_bank = q.indx_lstk.Substring(1, 2);
                            if (!(in_bank == "01" || in_bank == "02")) return -9;
                            break;
                        case "03":
                            li_hogi = 1;
                            in_bank = q.indx_lstk.Substring(1, 2);
                            if (!(in_bank == "03" || in_bank == "04")) return -9;
                            break;
                        case "05":
                            li_hogi = 2;
                            in_bank = q.indx_lstk.Substring(1, 2);
                            if (!(in_bank == "05" || in_bank == "06")) return -9;
                            break;
                        case "07":
                            li_hogi = 3;
                            in_bank = q.indx_lstk.Substring(1, 2);
                            if (!(in_bank == "07" || in_bank == "08")) return -9;
                            break;
                        case "09":
                            li_hogi = 4;
                            in_bank = q.indx_lstk.Substring(1, 2);
                            if (!(in_bank == "09" || in_bank == "10")) return -9;
                            break;
                        default:
                            return -9;
                            break;
                    }
                    if (f_scc_info(li_hogi) != 0) return -9;
                    if (scc_stop == "1") return -9;
                    if (scc_onln == "0") return -9;
                    if (scc_comm == "0") return -9;
                } // end of ls_tkno
            } // end of (q.indx_jio == "I")


            //****************************************************************************************************//
            // SFLG = 'P'일경우,   CVC에 지령-데이타 만들기
            //****************************************************************************************************//
            // JOB-NO + FROM-ADDR + TO-ADDR + JOB-IO
            //****************************************************************************************************//
            // job-no    =  작업번호
            // from-addr =  시작지 ST번호, 입고시는 21,22,50,     출고시는 2,4,6,8,10
            // to-addr   =  목적지 ST번호, 입고시는 1,3,5,7,9,    출고시는 43,45
            // job-io    =  입출모드(0001=입고,0002=출고,0007=이동동작)
            //****************************************************************************************************//
            string ls_comd = "0000";
            switch (q.indx_jio)
            {
                case "I": // 입고 Data =                     fstn = 21,22,50  tstn = 0001,0003,0005,0007,0009, 0024
                    ls_comd = "0001";
                    break;
                case "$": // 출고 Data =                     fstn = 0002,0004,0006,0008,0010    tstn = 43,45
                    ls_comd = "0002";
                    break;
                case "M":  // 이동 Data =                     fstn = 21,22,50,24  tstn = 43, 45
                    ls_comd = "0003";
                    break;
                default:
                    return -6;
                    break;
            }

            string writ_data = q.indx_indx + "00" + q.indx_fstn + "00" + q.indx_tstn + ls_comd;
            string ls_obuf = writ_data;
            //*******************************************************************************************************//
            //* 해당 콘베어 버퍼 쓰기( 'P' -> 'M' )
            //*******************************************************************************************************//
            string ls_ldate = ls_edat.Substring(0, 4) + "/" + ls_edat.Substring(4, 2) + "/" + ls_edat.Substring(6, 2);
            string ls_ltime = ls_edat.Substring(8, 2) + ":" + ls_edat.Substring(10, 2) + ":" + ls_edat.Substring(12, 2);

            if (f_writ_proc(ls_obuf) != 0) return -9; //해당 버퍼 PLC 쓰기 FAIL

            string ls_flag = q.indx_gubn + q.indx_jio;
            //*******************************************************************************************************//
            //* 해당 콘베어 버퍼 쓰기 성공시 ( 'P' -> 'M' )
            //*******************************************************************************************************//
            int rt = 0;
            switch (ls_flag)
            {
                case "AI": //정상입고건 => 입고대 PLC쓰기 성공시 이동중 처리한다.   
                    using(DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.Connection.open();
                        using(d.Transaction = d.Connection.BeginTransaction())
                        {
                            try
                            {
                                rt = d.ExecuteCommand(@"update tbindx set indx_edat = {0}, indx_sflg = 'M'
                                                         where indx_jno = {1} and indx_fstn = {2} and indx_sflg = 'P'", ls_edat, ls_jno, ls_tkno);
                                if (rt == 0)
                                {
                                    d.Transaction.Rollback();
                                    return -5;
                                }
                                if (q.indx_fstn == "24")
                                {
                                    rt = d.ExecuteCommand(@"update tibarc set barc_pltno = '', barc_flag = '2', barc_msg = '', cvc_msg = '' where barc_flag = '1' ");
                                    if (rt == 0)
                                    {
                                        d.Transaction.Rollback();
                                        return -5;
                                    }
                                }
                                rt = d.ExecuteCommand(@"update miplti set plti_idate = {0}, plti_itime = {1} where plti_pltno = {2}", ls_ldate, ls_ltime, q.indx_pltn);
                                if (rt == 0)
                                {
                                    d.Transaction.Rollback();
                                    f_msg(1, "f_writ_srch fail AI Fail");
                                    return -5;
                                }
                                else
                                {
                                    d.Transaction.Commit();
                                }
                            }
                            catch (Exception E)
                            {                               
                                d.Transaction.Rollback();
                                f_msg(1, "f_writ_srch fail AI Fail " + E.Message);
                            }
                            finally
                            {
                                d.Connection.Close();
                            }
                        }
                    }
                    break;

                case "A$": //정상출고건 => 출고대 PLC쓰기 성공시 삭제   처리한다.
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.ExecuteCommand(@"delete from  tbindx where indx_jno = {0} and indx_fstn = {1} and indx_sflg = 'P'", ls_jno, ls_tkno);
                    }
                    break;

                case "AM": //정상이동건 => PLC쓰기 성공시 삭제   처리한다. tbevnt insert
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {                      
                           
                        d.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_fstn = {1} and indx_sflg = 'P'", ls_jno, ls_tkno);

                        if (q.indx_tstn == "24") { }
                        else if ((q.indx_fstn == "21" || q.indx_fstn == "22") || q.indx_tstn == "43")
                        {
                            d.ExecuteCommand(@"insert into tbevnt 
                                                ( evnt_gubn,    evnt_jio,  evnt_hogi, evnt_fstn,   evnt_tstn,
  			                                      evnt_pltn,    evnt_lstk, evnt_xmov, evnt_sflg,   evnt_wflg,    evnt_uflg, evnt_wdate )
     	   	                            values  ( {0},          {1},        '0',       {2},         {3}, 
                                                  {4},          {5},        'N',       'M',         'F',          '0',     {6} ) ",
                                                            q.indx_gubn, q.indx_jio, q.indx_fstn, q.indx_tstn,
                                                            q.indx_pltn, q.indx_lstk, ls_edat);
                        }
                        else if (q.indx_fstn == "24" && q.indx_tstn == "43")
                        {
                            d.ExecuteCommand(@"update tibarc set barc_flag = '2', barc_msg = '', cvc_msg = '' where barc_key = '1'");
                            // pltno, loca를 모름 이동불가
                        }
                        else if (q.indx_hogi != "0" && q.indx_tstn == "43")
                        {
                            d.ExecuteCommand(@"insert into tbevnt 
                                                ( evnt_gubn,    evnt_jio,  evnt_hogi, evnt_fstn,   evnt_tstn,
  			                                      evnt_pltn,    evnt_lstk, evnt_xmov, evnt_sflg,   evnt_wflg,    evnt_uflg, evnt_wdate )
     	   	                            values  ( {0},          {1},        {2},         {3},      {4}, 
                                                  {5},          {6},        {7},        'M',         'F',          '0',     {8} ) ",
                                                            q.indx_gubn, q.indx_jio, q.indx_hogi, q.indx_fstn, q.indx_tstn,
                                                            q.indx_pltn, q.indx_lstk, q.indx_xmov, ls_edat);
                        }
                    
                    }
                    
                    break;
                case "RI": //비정상 입고시
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.ExecuteCommand(@"update tbindx set indx_edat = {0}, indx_sflg = 'M' 
                                         where indx_jno = {1} and indx_fstn = {2} and indx_sflg = 'P'",
                                         ls_edat, ls_jno, ls_tkno);
                    }
                    break;
                case "R$":  //비정상 출고대 -> 출고존  이동건 PLC쓰기 성공시 삭제   처리한다.
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_fstn = {1} and indx_sflg = 'P'", ls_jno, ls_tkno);
                    }
                    break;

                case "RM":   //비정상 입고GATE -> 출고존 이동건 PLC쓰기 성공시 삭제   처리한다. 
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.ExecuteCommand(@"delete from tbindx where indx_jno = {0} and indx_fstn = {1} and indx_sflg = 'P'", ls_jno, ls_tkno);
                    }
                    break;
                default:
                    f_msg(1, "CVC_WRIT_SRCH = NO MODE = [" + ls_flag + "]");
                    return -1;
                    break;
            }
            f_msg(1, "PLC WRIT OK !! TKNO=[" + ls_tkno + "]=[" + ls_flag + "]");

            return 0;
        }
        private void f_get_inpt_mode()
        {
            debugstep = "12";
            try
            {
                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    var q = d.ExecuteQuery(@"select stat_imode, stat_ipath, stat_barm from tbstat where stat_key = '1'").SingleOrDefault();
                    if (q == null)
                    {
                        imode = "0";
                        ipath = "0";
                        f_msg(1, "select tbstat 에러");
                        return;
                    }
                    imode = q.stat_imode;
                    ipath = q.stat_ipath;
                    barm = q.stat_barm;
                }
            }
            catch (Exception E)
            {
                imode = "0";
                ipath = "0";
                f_msg(1, "DB 접속에러");
            }
        }
        private int f_gets_bcc(string as_ibuf, ref string as_obuf)
        {
            debugstep = "13";
            //***********************************************************************************************//
            // 송,수신 데이타의 BCC 값 얻기
            //***********************************************************************************************//
            string ls_data = as_ibuf.Trim();
            as_obuf = "";

            int li_dlen = ls_data.Length;
            short li_dsum = 0;
            short li_dsum1 = 0;
            int v = 0;

            for (int j = 0; j < li_dlen; j++)
            {
                byte a = (byte)(ls_data[j]);
                //  f_gets_hexcharton(ls_data[j], ref v);

                li_dsum1 = a;
                li_dsum += li_dsum1;
                if (li_dsum >= 256) li_dsum -= 256;
            }
            short li_val1 = (short)(li_dsum / 16);
            short li_val2 = (short)(li_dsum % 16);

            char[] ls_ch = new char[2] { '0', '0' };
            if (f_gets_ntohexchar(li_val1, ref ls_ch[0]) != 0) return -1;
            if (f_gets_ntohexchar(li_val2, ref ls_ch[1]) != 0) return -2;

            as_obuf = new string(ls_ch); //두자리HEX String으로 돌려줌
            return 0;
        }
        private int f_gets_hexcharton(char ac_char, ref int ai_nval)
        {
            debugstep = "14";
            //********************************************************************************
            //* Hex Character(1자리)를 숫자N으로 변환 (HexChar TO Numeric )
            //********************************************************************************
            ai_nval = 0;

            int li_ret = 0;
            switch (ac_char)
            {
                case '0': li_ret = 0; break;
                case '1': li_ret = 1; break;
                case '2': li_ret = 2; break;
                case '3': li_ret = 3; break;
                case '4': li_ret = 4; break;
                case '5': li_ret = 5; break;
                case '6': li_ret = 6; break;
                case '7': li_ret = 7; break;
                case '8': li_ret = 8; break;
                case '9': li_ret = 9; break;
                case 'A': li_ret = 10; break;
                case 'B': li_ret = 11; break;
                case 'C': li_ret = 12; break;
                case 'D': li_ret = 13; break;
                case 'E': li_ret = 14; break;
                case 'F': li_ret = 15; break;
                default:
                    return -1;
                    break;
            }
            ai_nval = li_ret;
            return 0;
        }
        private int f_gets_ntohexchar(int ai_nval, ref char as_char)
        {
            debugstep = "15";
            //********************************************************************************
            //* 숫자Numeric를 Hex Character(1자리)로 변환 (N To HexChar)
            //********************************************************************************
            char lc_ret = '0';
            as_char = '0';

           
            switch (ai_nval)
            {
                case 0: lc_ret = '0'; break;
                case 1: lc_ret = '1'; break;
                case 2: lc_ret = '2'; break;
                case 3: lc_ret = '3'; break;
                case 4: lc_ret = '4'; break;
                case 5: lc_ret = '5'; break;
                case 6: lc_ret = '6'; break;
                case 7: lc_ret = '7'; break;
                case 8: lc_ret = '8'; break;
                case 9: lc_ret = '9'; break;
                case 10: lc_ret = 'A'; break;
                case 11: lc_ret = 'B'; break;
                case 12: lc_ret = 'C'; break;
                case 13: lc_ret = 'D'; break;
                case 14: lc_ret = 'E'; break;
                case 15: lc_ret = 'F'; break;
                default:
                    return -1;
                    break;
            }
            as_char = lc_ret;
            return 0;
        }
        private string f_get_rsrv_hogi(string atype)
        {
            debugstep = "16";
            string srch = "";
            
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                char[] bonof = new char[5];
                char[] beror = new char[5];

                int lhno = d.ExecuteQuery<int>(@"select hogi_no from tbhogi").SingleOrDefault();
                string hogi = lhno.ToString("0");

                var c = d.ExecuteQuery(@"select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (c == null) return "";

                string tmp = c.cnvc_op_onof;
                bonof = tmp.Substring(0, 5).ToCharArray();
                f_msg(1, tmp);
                tmp = c.cnvc_op_eror;
                beror = tmp.Substring(0, 5).ToCharArray();

                
                f_msg(1, tmp);
                for (int i = 0; i < 5; i++)
                {
                    lhno++;
                    if (lhno > 5) lhno = 1;

                    hogi = lhno.ToString("0");

                    //if (bonof[lhno - 1] == '0' || beror[lhno - 1] == '1') continue;
                    
                    string ls_hogi = "0" + hogi;

                    var s = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).Select(x => x).SingleOrDefault();
                    if (s == null) return "";
                    if (s.scrc_onln != "1") continue;
                    if (s.scrc_stop != "0") continue;
                    if (s.scrc_comm != "1") continue;
                    if (s.scrc_emer != "0") continue;
                    if (s.scrc_iuse != "1") continue;
                    if (s.scrc_eror != "0") continue;

                    //f_msg(1, "s.scrc_onln" + s.scrc_onln);
                    //f_msg(1, "s.scrc_stop" + s.scrc_stop);
                    //f_msg(1, "s.scrc_comm" + s.scrc_comm);
                    //f_msg(1, "s.scrc_emer" + s.scrc_emer);
                    //f_msg(1, "s.scrc_iuse" + s.scrc_onln);
                    //f_msg(1, "s.scrc_eror" + s.scrc_eror);

                    srch = d.ExecuteQuery<string>(@"select top 1 lstk_srch from milstk 
                                                 where lstk_hogi = {0}
   	                                             and lstk_io   = '0'
	                                             and lstk_use  = '1'
   	                                             and lstk_stat = '00'
	                                             and lstk_type in ('0', '1')
	                                             and lstk_no like 'A%' order by lstk_type, lstk_srch ", hogi).SingleOrDefault();
                    if (srch == null) continue;
                    if (srch != "") break;  // found

                }
                if (srch == null) return "";
                if (srch == "") return "";
                d.ExecuteCommand(@"update tbhogi set hogi_no = {0} where hogi_key = '1'", lhno);

                return srch.Substring(4, 2) + srch.Substring(2, 2) + srch.Substring(0, 2);
            }                      
            
        }
        private string f_get_rsrv_hogi1(string atype)
        {
            debugstep = "17";
            string srch = "";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                char[] bonof = new char[5];
                char[] beror = new char[5];

                int lhno = d.ExecuteQuery<int>(@"select hogi_no from tbhogi").SingleOrDefault();
                string hogi = lhno.ToString("0");

                var c = d.ExecuteQuery(@"select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (c == null) return "";

                string tmp = c.cnvc_op_onof;
                bonof = tmp.Substring(0, 5).ToCharArray();

                tmp = c.cnvc_op_eror;
                beror = tmp.Substring(0, 5).ToCharArray();

                for (int i = 0; i < 5; i++)
                {
                    lhno++;
                    if (lhno > 5) lhno = 1;

                    hogi = lhno.ToString("0");
                    if (bonof[lhno - 1] == '0' || beror[lhno - 1] == '1') continue;

                    string ls_hogi = "0" + hogi;

                    var s = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).Select(x => x).SingleOrDefault();
                    if (s == null) return "";
                    if (s.scrc_onln != "1") continue;
                    if (s.scrc_stop != "0") continue;
                    if (s.scrc_comm != "1") continue;
                    if (s.scrc_emer != "0") continue;
                    if (s.scrc_iuse != "1") continue;
                    if (s.scrc_eror != "0") continue;

                    srch = d.ExecuteQuery<string>(@"select top 1 lstk_srch from milstk 
                                                 where lstk_hogi = {0}
   	                                             and lstk_io   = '0'
	                                             and lstk_use  = '1'
   	                                             and lstk_stat = '00'
	                                             and lstk_type = '1'
                                                 and lstk_lv = '01'
	                                             and lstk_no like 'A%' order by lstk_srch ", hogi).SingleOrDefault();
                    if (srch == null) continue;
                    if (srch != "") break;  // found

                }
                if (srch == null) return "";
                if (srch == "") return "";

                d.ExecuteCommand(@"update tbhogi set hogi_no = {0} where hogi_key = '1'", lhno);

                return srch.Substring(4, 2) + srch.Substring(2, 2) + srch.Substring(0, 2);
            }
          
        }
        private string f_get_rsrv_hogi2(string atype)
        {
            debugstep = "18";
            string srch = "";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                char[] bonof = new char[5];
                char[] beror = new char[5];

                int lhno = d.ExecuteQuery<int>(@"select hogi_no2 from tbhogi").SingleOrDefault();

                var c = d.ExecuteQuery(@"select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (c == null) return "";

                string tmp = c.cnvc_op_onof;
                bonof = tmp.Substring(0, 5).ToCharArray();

                tmp = c.cnvc_op_eror;
                beror = tmp.Substring(0, 5).ToCharArray();

                //-      Thinner 인 경우 02 - 05열 17번지까지 1 - 2단에 적재함. 
                //-      Thinner 외 위험물은 Thinner 공간을 제외한 01 ? 10열 36번지까지 1단에 적재함.

                string ls_bk = "";

                string ls_hogi = "00";

                for (int i = 0; i < 5; i++)
                {
                    lhno++;
                    if (lhno > 5 || lhno < 2) lhno = 2;

                    ls_bk = '0' + lhno.ToString("0");

                    switch (ls_bk)
                    {
                        case "02":
                            ls_hogi = "01"; break;
                        case "03":
                        case "04":
                            ls_hogi = "02"; break;
                        case "05":
                            ls_hogi = "03"; break;
                        default:
                            ls_hogi = "02"; break;
                    }

                    if (bonof[lhno - 1] == '0' || beror[lhno - 1] == '1') continue;


                    var s = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).Select(x => x).SingleOrDefault();
                    if (s == null) return "";
                    if (s.scrc_onln != "1") continue;
                    if (s.scrc_stop != "0") continue;
                    if (s.scrc_comm != "1") continue;
                    if (s.scrc_emer != "0") continue;
                    if (s.scrc_iuse != "1") continue;
                    if (s.scrc_eror != "0") continue;

                    srch = d.ExecuteQuery<string>(@"select top 1 lstk_srch from milstk 
                                                 where lstk_bk = {0}
   	                                             and lstk_io   = '0'
	                                             and lstk_use  = '1'
   	                                             and lstk_stat = '00'
	                                             and lstk_type = {1}
                                                 and lstk_lv in ('01', '02')
	                                             and lstk_no like 'A%' order by lstk_srch ", ls_bk, atype).SingleOrDefault();
                    if (srch == null) continue;
                    if (srch != "") break;  // found

                }
                if (srch == null) return "";
                if (srch == "") return "";
                d.ExecuteCommand(@"update tbhogi set hogi_no2 = {0} where hogi_key = '1'", lhno);

                return srch.Substring(4, 2) + srch.Substring(2, 2) + srch.Substring(0, 2);
            }
          
        }
        private string f_get_rsrv_hogi3(string atype)
        {
            debugstep = "19";
            string srch = "";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                char[] bonof = new char[5];
                char[] beror = new char[5];

                int lhno = ghogi3;

                string hogi = lhno.ToString("0");

                var c = d.ExecuteQuery(@"select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01'").SingleOrDefault();
                if (c == null) return "";

                string tmp = c.cnvc_op_onof;
                bonof = tmp.Substring(0, 5).ToCharArray();

                tmp = c.cnvc_op_eror;
                beror = tmp.Substring(0, 5).ToCharArray();


                for (int i = 0; i < 3; i++)
                {
                    lhno++;
                    if (lhno > 5) lhno = 4;

                    hogi = lhno.ToString("0");
                    if (bonof[lhno - 1] == '0' || beror[lhno - 1] == '1') continue;

                    string ls_hogi = "0" + hogi;

                    var s = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).Select(x => x).SingleOrDefault();
                    if (s == null) return "";

                    if (s.scrc_onln != "1") continue;
                    if (s.scrc_stop != "0") continue;
                    if (s.scrc_comm != "1") continue;
                    if (s.scrc_emer != "0") continue;
                    if (s.scrc_iuse != "1") continue;
                    if (s.scrc_eror != "0") continue;

                    srch = d.ExecuteQuery<string>(@"select top 1 lstk_srch from milstk 
                                                 where lstk_hogi = {0}
   	                                             and lstk_io   = '0'
	                                             and lstk_use  = '1'
   	                                             and lstk_stat = '00'
	                                             and lstk_type = '3'
                                                 and lstk_lv in ('01', '02')
	                                             and lstk_no like 'A%' order by lstk_srch ", hogi).SingleOrDefault();
                    if (srch == null) continue;

                    if (srch != "") break;  // found

                }
                if (srch == null) return ""; // not found
                if (srch == "") return ""; // not found

                d.ExecuteCommand(@"update tbhogi set hogi_no = {0} where hogi_key = '1'", lhno);

                ghogi3 = lhno;

                return srch.Substring(4, 2) + srch.Substring(2, 2) + srch.Substring(0, 2);
            }
  
        }
        private int f_scc_info(int ai_hogi)
        {
            debugstep = "20";
            string ls_hogi = (ai_hogi + 1).ToString("00");
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                var q = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).Select(x => x).SingleOrDefault();
                if (q == null) return -1;

                scc_mode = q.scrc_mode;
                scc_gubn = q.scrc_gubn;
                scc_io = q.scrc_io;
                scc_onln = q.scrc_onln;
                scc_pwron = q.scrc_pwron;
                scc_emer = q.scrc_emer;

                scc_stat = q.scrc_stat;
                scc_palt = q.scrc_palt;
                scc_posi = q.scrc_posi;
                scc_eror = q.scrc_eror;
                scc_ecod = q.scrc_ecod;

                scc_stop = q.scrc_stop;
                scc_iuse = q.scrc_iuse;
                scc_ouse = q.scrc_ouse;

                scc_lstk = q.scrc_lstk;
                scc_pltn = q.scrc_pltn;
                scc_jno = q.scrc_jno;
                scc_indx = q.scrc_indx;
                scc_fstn = q.scrc_fstn;
                scc_tstn = q.scrc_tstn;
                scc_xmov = q.scrc_xmov;
                scc_comm = q.scrc_comm;
                scc_rset = q.scrc_rset;
            }
            return 0;
        }
        private int f_sc_pwr_writ()
        {
            debugstep = "21";
            //****************************************************************************************************//
            // CNVC에 S/C 콘트롤 전원 상태 송신처리
            //****************************************************************************************************//
            // ENQ+06+FF+WW5+D0210+05+[WRIT-DATA=16]+BCC1,2+CRLF  -->
            //                                               <-- ACK+06+FF+CRLF or NAK+06+FF+CRLF
            //****************************************************************************************************//
            // WRIT-DATA = S/C#1(0000 or 1000) + S/C#2(0000 or 1000) + S/C#3(0000 or 1000) + S/C#4(0000 or 1000) + S/C#5(0000 or 1000)
            //****************************************************************************************************//

            if (!sPortOpened)
            {
                f_msg(1, "CVC Port Not Opend!!");
                return -1;
            }
            string writ_data = "";
            string ls_chek = "";
            string ls = "";
            for (int j = 0; j < 5; j++)
            {
                ls_chek = sc_pwr_onof[j].ToString() + sc_eror_stat[j].ToString();
                switch (ls_chek)
                {
                    case "00":
                        ls = ls + "0000";
                        break;
                    case "01":
                        ls = ls + "0002";
                        break;
                    case "10":
                        ls = ls + "0001";
                        break;
                    case "11":
                        ls = ls + "0003";
                        break;                
                }
            }
            f_msg(1, "f_sc_pwr_writ" + ls);
            //****************************************************************************************************//
            // STEP1 = sending writ_data to CNVC-plc
            //****************************************************************************************************//
            writ_data = ls;
            string ls_wdata = "06FFWW5D021005" + writ_data;
            string ls_bcc = "";
            if(f_gets_bcc(ls_wdata, ref ls_bcc) != 0)
            {
                f_msg(1, "CVC Writ_Comd Bcc Eror...!!");
                return -1;
            }

            oupt_buff = ENQ + "06FFWW5D021005" + writ_data + ls_bcc + XCR + XLF;
            try
            {                
                sPort.Write(oupt_buff);
                f_msg(1, "CVC W_DATA=[" + oupt_buff + "]");

            } catch (Exception E) { f_msg(1, "f_sc_pwr_writ" ); return -1; }

            Thread.Sleep(300);
            //****************************************************************************************************//
            // STEP2 = receive writ_ack from CNVC-plc
            //****************************************************************************************************//
            inpt_buff = "";
            int ll_ack = -1;
            try
            {
                inpt_buff = sPort.ReadExisting();
                f_msg(1, "CVC W_ACK=[" + inpt_buff + "]");
                if (inpt_buff.Length > 0)
                {
                    ll_ack = inpt_buff.IndexOf(ACK);
                    if (ll_ack < 0)
                    {
                        f_msg(1, "No ACK");
                        return -5;
                    }
                    else
                    { 
                        f_msg(1, "ACK OK");                      
                    }
                }
                else { return -6; }              
            }
            catch (InvalidOperationException E)
            {
                f_msg(1, E.Message);
                return -1;
            }
            inpt_buff = "";
            g_sc_pwr = 1;
            return 0;
        }  
        private int f_read_proc(ref string as_ibuf)
        {
            debugstep = "22";
            //***********************************************************************************************//
            // CONVEYOR로부터 STATUS-데이타 수신처리
            //***********************************************************************************************//
            // ENQ+06+FFWR5+D1001+15+BCC1,2+CRLF  -->
            //                                    <--    STX+06+FF+[READ-DATA=84]+ETX+BCC1,2+CRLF
            // ACK+06+FF+CRLF or NAK+06+FF+CRLF   -->
            //***********************************************************************************************//
            as_ibuf = "";
         
            //if (!sim)
            //{
                if (!sPortOpened)
                {
                    disp_msg = "CNVC Port Not Opend...!!";
                    f_msg(1, disp_msg);
                    return -2;
                }
            //}
            //*****************************************************************************************
            //STEP1= send read-command to CVC#1-plc
            //*****************************************************************************************
            //ls_wdata = "06FFWR5D100115"
            string ls_wdata = "06FFWR5D100135";  // 6 + 47 = 53
            string ls_bcc = "";
            if(f_gets_bcc(ls_wdata, ref ls_bcc) != 0)
            {
                disp_msg = "CNVC Read_Comd Bcc Eror...!!";
                f_msg(1, disp_msg);
                return -1;
            }
            oupt_buff = ENQ + ls_wdata + ls_bcc + XCR + XLF;
            string ls = oupt_buff.Trim();

            try {
               
                sPort.Write(ls);

                disp_msg = "R_Comd=[" + oupt_buff + "]";
                f_msg(1, disp_msg);
            }
            catch (Exception E) { f_msg(1, "Writer error!");  return -1; }

            //*****************************************************************************************
            //STEP2= 상태 데이타 수신
            //*****************************************************************************************
            inpt_buff = "";
            string read_data = "";
            string read_buff = "";
            int dlen = 0;
         
            for(int j = 0; j < 20; j++)
            {
                read_buff = "";
                Thread.Sleep(100);

                read_buff = sPort.ReadExisting();                
                if (read_buff.Length > 0)
                {
                    inpt_buff += read_buff;
                }
                else continue;

                dlen = inpt_buff.Length;
                if (dlen >= 220) break;
            }

            disp_msg = "R_Data Len=[" + inpt_buff.Length.ToString() + "]";
            f_msg(1, disp_msg);

            //*****************************************************************************************
            //STEP3= 수신데이타 체크, ACK SENDING
            //*****************************************************************************************
            string ls_rchek = "";
            string ls_rbcc = "";
            char ls_ch = ' ';
            if (dlen > 1)
            {
              
                int ll_stx = inpt_buff.IndexOf(STX);
                if (ll_stx < 0) return -1;
             
                int ll_etx = inpt_buff.IndexOf(ETX, ll_stx + 1);
                if (ll_etx < 0) return -2;               
                
                if ((ll_stx + 4 + 212 + 1) != ll_etx) return -5;
                f_msg(1, "6666666");

                ls = inpt_buff.Substring(ll_stx + 4 + 1, 212);
                //S12345E
                ls_rchek = inpt_buff.Substring(ll_stx + 1, ll_etx - ll_stx); // Bcc Cheking Data
                ls_rbcc = inpt_buff.Substring(ll_etx + 1, 2);                // read Bcc-values
                for (int i = 0; i < 212; i++)
                {
                    ls_ch = ls[i];
                    switch (ls_ch)
                    {
                        case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9': break;
                        case 'A': case 'B': case 'C': case 'D': case 'E': case 'F': break;
                        default:
                            ls = ""; read_data = "";
                            return -3;
                            break;
                    }
                }
            }
            else
            {
                disp_msg = "CNVC No Read_Data...!!";
                f_msg(1, disp_msg);
                return -8;
            }
            //*****************************************************************************************
            // 수신된 상태 데이타 저장.  
            //*****************************************************************************************
            read_data = ls;
            if (f_gets_bcc(ls_rchek, ref ls_bcc) == 0)
            {
                if(ls_rbcc != ls_bcc)
                {
                    disp_msg = "CVC Read_Data Bcc Eror...!!=PLC_BCC=[" + ls_rbcc + "], My_BCC=[" + ls_bcc + "]";
                    f_msg(1, disp_msg);
                    return -1;
                }
            }else
            {
                disp_msg = "CVC Read_Data Bcc 계산 불능 !!";
                f_msg(1, disp_msg);
                return -1;
            }
            as_ibuf = read_data;
            disp_msg = "CVC R_DATA=[" + read_data + "]";
            f_msg(1, disp_msg);

            return 0;
        }
        private int f_writ_proc(string as_obuf)
        {
            debugstep = "23";
            //****************************************************************************************************//
            // CNVC에 지령-데이타 송신처리
            //****************************************************************************************************//
            // ENQ+06+FF+WW5+D0000+04+[WRIT-DATA=16]+BCC1,2+CRLF  -->
            //                                               <-- ACK+06+FF+CRLF or NAK+06+FF+CRLF
            //****************************************************************************************************//
            // WRIT-DATA = JOBNO(0000) + FROM-ADDR(00 + STNO) + TO-ADDR(00 + STNO) + JOB-IO(000 + 0[1=입고,2=출고,3=이동..)
            //****************************************************************************************************//
            if (!sPortOpened)
            {
                disp_msg = "CNVC Port Not Opend...!!";
                f_msg(1, disp_msg);
                return -2;
            }
            //****************************************************************************************************//
            // STEP1 = sending writ_data to CNVC-plc
            //****************************************************************************************************//
            string ls_bcc = string.Empty;
            string writ_data = as_obuf;
            string ls_wdata = "06FFWW5D000004" + writ_data;

            if(f_gets_bcc(ls_wdata, ref ls_bcc) != 0)
            {
                disp_msg = "CVC Writ_Comd Bcc Eror...!!";
                f_msg(1, disp_msg);
                return -1;
            }
            oupt_buff = ENQ + "06FFWW5D000004" + writ_data + ls_bcc + XCR + XLF;
            try
            {
                sPort.Write(oupt_buff);
                disp_msg = "CVC W_DATA=[" + oupt_buff + "]";
                f_msg(1, disp_msg);
            }
            catch (Exception E) { return -1; }

            Thread.Sleep(300);
            //****************************************************************************************************//
            // STEP2 = receive writ_ack from CNVC-plc
            //****************************************************************************************************//
            inpt_buff = "";
            int ll_ack = -1;

            inpt_buff = sPort.ReadExisting();
            disp_msg = "CVC W_ACK =[" + inpt_buff + "]";
            f_msg(1, disp_msg);
            if (inpt_buff.Length > 0)
            {
                ll_ack = inpt_buff.IndexOf(ACK);
                if (ll_ack < 0)
                {
                    disp_msg = "NO ACK";
                    f_msg(1, disp_msg);
                    return -5;
                }
                else f_msg(1, "OK ACK");
            }
            else return -6;

            inpt_buff = "";            
            return 0;
        }
        private int f_scan_proc()
        {
            debugstep = "24";
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];
            string ls_rdata = "";
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("10.63.44.28"), 1537);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.SendTimeout = 2100;  // 1초에서 2초 
                sender.ReceiveTimeout = 1000;
                try
                {
                    sender.Connect(remoteEP, new TimeSpan(0,0,2));
                    f_msg(1, "Socket connected to " + sender.RemoteEndPoint.ToString());

                    string ls_sdata = "<R>" + XCR + XLF;

                    byte[] msg = Encoding.ASCII.GetBytes(ls_sdata);
                    int bytesSent = sender.Send(msg);
                    f_msg(1, "send data to scanner = " + "<R>");

                    Thread.Sleep(100);
                    int bytesRec = sender.Receive(bytes);
                    if (bytesRec > 0)
                    {                       
                        ls_rdata = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                     
                        f_msg(1, "바코드 파렛트번호=" + ls_rdata);
                        int lp1 = ls_rdata.IndexOf('<');
                        if (lp1 < 0)
                        {
                            return -1;
                        }
                     
                        int lp2 = ls_rdata.IndexOf('>');
                        if (lp2 < 0)
                        {
                            return -1;
                        }
                        string ls_rstr = ls_rdata.Substring(lp1 + 1, lp2 - lp1 - 1);


                        
                        using(DBDataContext d = new DBDataContext(Config.DBCon))
                        {
                            if (ls_rstr.Length != 8)
                            {
                                ls_rstr = ls_rstr.Substring(0, 8);
                                //d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '0', barc_msg = 'Read Not OK(' + {0} + ')', cvc_msg = '' where barc_flag <> '1' ", ls_rstr);
                                d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '0', barc_msg = 'Read Not OK(' + {0} + ')' where barc_flag <> '1' ", ls_rstr); //20200523
                                f_msg(1, "파렛트번호가 8자리입니다..." + ls_rstr); return -1;
                            }
                            int ival = 0;
                            if (!int.TryParse(ls_rstr, out ival))
                            {
                                //d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '0', barc_msg = 'Read Not OK(' + {0} + ')',  cvc_msg = '' where barc_flag <> '1' ", ls_rstr);
                                d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '0', barc_msg = 'Read Not OK(' + {0} + ')' where barc_flag <> '1' ", ls_rstr); //20200523
                                f_msg(1, "파렛트번호가 숫자가 아닙니다..." + ls_rstr); return -1;
                            }
                            // read ok
                            //d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '1', barc_msg = 'Read OK', cvc_msg = '' where barc_flag <> '1' ", ls_rstr);
                            d.ExecuteCommand(@"update tibarc set barc_pltno = {0}, barc_flag = '1', barc_msg = 'Read OK' where barc_flag <> '1' ", ls_rstr); //20200523

                        }

                    }
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    sender.Dispose();
                    
                    return 0;
                }
                catch (SocketException se)
                {
                    f_msg(1, "SocketException :" + se.ToString()); return -1;
                }
                catch (Exception e)
                {
                    f_msg(1, "Unexpected exception:" + e.ToString()); return -1;
                }
            }
            catch (Exception e)
            {
                f_msg(1, e.ToString());
            }
            return 0;
        }
        private void f_m_main_proc()
        {
            debugstep = "25";
            //f_get_inpt_mode();
            //f_bf24_proc();
            //return ;
            //------------------------------------------------------------//
            // CONVEYOR MAIN PROC
            //------------------------------------------------------------//
            f_msg(1, "f_m_main_proc");
            string ls_ibuf = "";
          
            if (f_read_proc(ref ls_ibuf) == 0)// READ 성공시
            {
                f_msg(1, "f_read_proc ok.....!!");
                g_cvc_com = 0;
                if (f_cvc_icvrt2(ls_ibuf) == 0)  // read ok
                {
                    f_cvc_info();
                    if (cv_stop == "0")  // CV 정지가 아닐때만 쓰기 가능 
                    {
                        f_get_inpt_mode();
                        f_bf24_proc();
                        f_bf2122_proc();
                        for(int jj = 0; jj < 8; jj++)
                        {
                            if (f_cvc_writ_srch() == 0) break;  // 성공
                        }
                    }
                    else { f_msg(1, "CNVC STOPPED.......!!"); Thread.Sleep(100); }
                }else
                {
                    // READ Fail시
                    f_msg(1, "READ Fail시");
                    g_cvc_com++;
                    if (g_cvc_com > 10)
                    {
                        f_cvc_comm_eror();
                        g_cvc_com = 1;
                    }
                }
            }

            if (g_sc_pwr == 0)
                f_sc_pwr_writ(); // CNVC에 S/ C 콘트롤 전원 상태 송신처리
        }

        private DbTransaction transBegin(DataContext ctx)
        {
            DbTransaction tr = null;
            try
            {
                if (ctx.Connection.State == ConnectionState.Closed)
                {
                    ctx.Connection.open();
                }
                else if (ctx.Connection.State == ConnectionState.Broken)
                {
                    ctx.Connection.Close();
                    ctx.Connection.open();
                }
                else
                {
                    ctx.Connection.open();
                }

            }
            catch (Exception E)
            {
                return tr;
            }
            if (ctx.Connection.State == ConnectionState.Open)
            {
                ctx.Transaction = ctx.Connection.BeginTransaction();
                tr = ctx.Transaction;
            }

            return tr;
        }
         
        public T[] Fill<T>(T initialValue, int length)
        {
            T[] result = new T[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = initialValue;
            }
            return result;
        }
    }
}
