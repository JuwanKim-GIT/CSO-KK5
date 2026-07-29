using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Diagnostics;

namespace RCP
{
    class SCRC
    {
              
        #region --- scrc global variable 선언 -------------------------------------------------
     
        //public string rrbuf = "00000000010100000000000000000000000000000000";
        bool[] sPortOpened = new bool[5] { false, false, false, false, false };

        public bool[] showmsg = new bool[5] { true, true, true, true, true };
        public BackgroundWorker bw = new BackgroundWorker();
        SerialPort[] sPort = new SerialPort[5];

        const char STX = (char)0x2;
        const char ETX = (char)0x3;
        const char EOT = (char)0x4;
        const char ENQ = (char)0x5;
        const char ACK = (char)0x6;
        const char NAK = (char)0x15;
        const char XCR = (char)0x0D;
        const char XLF = (char)0x0A;

        int li_handle = 0;
        string inifile = "";
        bool disp_on = false, disp_clear = false;
        bool prgm_exit = false;

        //***********************************************************/
        // DATA BUFFER AREA                                         */ 
        //***********************************************************/
        string disp_msg = "", prev_ibuf = "";
        string inpt_buff = "", oupt_buff = "";

        int g_scc_comd = 0;
        int[] g_scc_strt = new int[5] { 0, 0, 0, 0, 0 };
        int[] g_comm = new int[5] { 0, 0, 0, 0, 0 };

        string recv_stat1 = ""; //= mid(read_data,1,4)         // D10  현재작업상태
        string recv_ercd = "";  //= mid(read_data,5,4)         // D11  에러코드
        string recv_bylv = "";  //= mid(read_data,9,4)         // D12  현재위치 = bay + levl
        string recv_stat2 = ""; //= mid(read_data,13,4)        // D13  현재상세상태
        string recv_xio = "";   //= mid(read_data,17,4)        // D14  받은지령모드(0001,002,007)
        string recv_xbk1 = "";  //= mid(read_data,21,4)        // D15  받은지령-열1
        string recv_xby1 = "";  //= mid(read_data,25,4)        // D16  받은지령-연1
        string recv_xlv1 = "";  //= mid(read_data,29,4)        // D17  받은지령-단1
        string recv_xbk2 = "";  //= mid(read_data,33,4)        // D18  받은지령-열2
        string recv_xby2 = "";  //= mid(read_data,37,4)        // D19  받은지령-연2
        string recv_xlv2 = "";  //= mid(read_data,41,4)        // D20  받은지령-단2
        string recv_pwron = "";
        string recv_emer = "";
        string recv_scplt = "";
        string recv_remote = "";

        //string prev_ercd[5], prev_posi[5], prev_chdt[5, 11];

        string[] prev_ercd = new string[5] { "", "", "", "", ""};
        string[] prev_posi = new string[5] { "", "", "", "", "" };
        string[][] prev_chdt = new string[5][] {
            new string[11] { "", "", "", "", "", "", "", "", "", "", ""},
            new string[11] { "", "", "", "", "", "", "", "", "", "", ""},
            new string[11] { "", "", "", "", "", "", "", "", "", "" ,""},
            new string[11] { "", "", "", "", "", "", "", "", "", "", ""},
            new string[11] { "", "", "", "", "", "", "", "", "", "", ""}
        };
        string scc_mode = "", scc_gubn = "", scc_io = "", scc_onln = "", scc_pwron = "";
        string scc_stat = "", scc_palt = "", scc_posi = "", scc_eror = "", scc_ecod = "";
        string scc_stop = "", scc_iuse = "", scc_ouse = "", scc_emer = "";
        string scc_lstk = "", scc_pltn = "", scc_jno = "", scc_indx = "", scc_fstn = "", scc_tstn = "", scc_xmov = "";
        string scc_mesg = "", scc_chdt = "", scc_comm = "", scc_rset = "";

        //***************************************************************
        // IO-SEARCH AREA
        //***************************************************************
        string srch_gubn = "", srch_jio = "", srch_lstk = "", srch_pltn = "";
        string srch_jno = "", srch_indx = "", srch_fstn = "", srch_tstn = "", srch_xmov = "";
  
        //***************************************************************
        // CNVC INPT-SIGNAL
        //***************************************************************
        char[] cv_op_onof = new char[8] { '0', '0', '0', '0', '0', '0', '0', '0' };
        char[] cv_op_eror = new char[8] { '0', '0', '0', '0', '0', '0', '0', '0' };
        char[] cv_buf_palt = new char[50] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'   };

        char cv_21_rqst = '0', cv_22_rqst = '0', cv_remote = '0';

        char[] cv_ist_redy = new char[5] { '0', '0', '0', '0', '0' }; // st= 1,3,5,7,9
        char[] cv_ist_palt = new char[5] { '0', '0', '0', '0', '0' }; // st= 1,3,5,7,9

        char[] cv_ost_redy = new char[5] { '0', '0', '0', '0', '0' }; // st= 2,4,6,8,10
        char[] cv_ost_palt = new char[5] { '0', '0', '0', '0', '0' }; // st= 2,4,6,8,10

        string cv_stop = "", cv_comm = "";
        string[] cv_chdt = new string[6] { "", "", "", "", "", "" };
        string[] cv_job_no = new string[15] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };// st= 1 - 10,21,22,43,45,50 

        char[] sc_pwr_onof = new char[8] {  '0', '0', '0', '0', '0', '0', '0', '0' };
        char[] sc_eror_stat = new char[8] { '0', '0', '0', '0', '0', '0', '0', '0' };
        char[] prev_sc_pwr = new char[8] {  '0', '0', '0', '0', '0', '0', '0', '0' };
        char[] prev_sc_eror = new char[8] { '0', '0', '0', '0', '0', '0', '0', '0' };

        int g_sc_init = 0;
        int[] sc_pwr_wait = new int[5] {0, 0, 0, 0, 0 };

        public string debugstep = "0"; 
       
        #endregion


        public SCRC()  //, SerialPort[] sPorts)
        {
           
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += Bw_DoWork;
       
        }
        public void Run_scrcproc()
        {
            if (bw.IsBusy) return;
            bw.RunWorkerAsync();
        }
        public void stop_scrcproc()
        {
            bw.CancelAsync(); ;
        }
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            f_init_setting();

            //화면 UI에서 종료시 빠져나간다
            while (!bw.CancellationPending)
            {
                Thread.Sleep(50);
                try
                {
                    f_m_main_proc();
                }
                catch (Exception E)
                {
                    f_msg(9, "f_m_main_proc error=" + debugstep + " " + E.Message);
                    Thread.Sleep(1000);
                }                         
            }
            f_m_end_proc();            
        }
        #region  ----------------------------------------------------------------------------------

        private int f_char_to_bits(string ac_char, ref string as_bit1, ref string as_bit2)
        {
            debugstep = "1";

            string lc_ch;
            string ls_bit1, ls_bit2;

            ls_bit1 = "";                // 1248 bits Conversion
            ls_bit2 = "";                // 8421 bits Conversion

            as_bit1 = "";
            as_bit2 = "";

            lc_ch = ac_char.Trim();

            switch (lc_ch)
            {
                case "0":
                    {
                        ls_bit1 = "0000";
                        ls_bit2 = "0000";
                        break;
                    }
                case "1":
                    {
                        ls_bit1 = "0001";
                        ls_bit2 = "1000";
                        break;
                    }

                case "2":
                    {
                        ls_bit1 = "0010";
                        ls_bit2 = "0100";
                        break;
                    }

                case "3":
                    {
                        ls_bit1 = "0011";
                        ls_bit2 = "1100";
                        break;
                    }
                case "4":
                    {
                        ls_bit1 = "0100";
                        ls_bit2 = "0010";
                        break;
                    }
                case "5":
                    {
                        ls_bit1 = "0101";
                        ls_bit2 = "1010";
                        break;
                    }
                case "6":
                    {
                        ls_bit1 = "0110";
                        ls_bit2 = "0110";
                        break;
                    }
                case "7":
                    {
                        ls_bit1 = "0111";
                        ls_bit2 = "1110";
                        break;
                    }
                case "8":
                    {
                        ls_bit1 = "1000";
                        ls_bit2 = "0001";
                        break;
                    }
                case "9":
                    {
                        ls_bit1 = "1001";
                        ls_bit2 = "1001";
                        break;
                    }
                case "A":
                    {
                        ls_bit1 = "1010";
                        ls_bit2 = "0101";
                        break;
                    }
                case "B":
                    {
                        ls_bit1 = "1011";
                        ls_bit2 = "1101";
                        break;
                    }
                case "C":
                    {
                        ls_bit1 = "1100";
                        ls_bit2 = "0011";
                        break;
                    }
                case "D":
                    {
                        ls_bit1 = "1101";
                        ls_bit2 = "1011";
                        break;
                    }
                case "E":
                    {
                        ls_bit1 = "1110";
                        ls_bit2 = "0111";
                        break;
                    }
                case "F":
                    {
                        ls_bit1 = "1111";
                        ls_bit2 = "1111";
                        break;
                    }
                default:
                    {
                        return -1;
                    }
            }

            as_bit1 = ls_bit1.Trim();
            as_bit2 = ls_bit2.Trim();

            return 0;
        }

        private int f_cvc_info()
        {
            debugstep = "2";
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    var c = d.tbcnvcs.Where(x => x.cnvc_mode == "01").SingleOrDefault();
                    if (c == null) return -1;

                    cv_op_onof = c.cnvc_op_onof.Trim().ToCharArray();
                    cv_op_eror = c.cnvc_op_eror.Trim().ToCharArray();
                    cv_buf_palt = c.cnvc_buf_palt.Trim().ToCharArray();
                    cv_ist_redy = c.cnvc_ist_redy.Trim().ToCharArray();
                    cv_ist_palt = c.cnvc_ist_palt.Trim().ToCharArray();
                    cv_ost_redy = c.cnvc_ost_redy.Trim().ToCharArray();
                    cv_ost_palt = c.cnvc_ost_palt.Trim().ToCharArray();
                    cv_21_rqst = Convert.ToChar(c.cnvc_21_rqst);
                    cv_remote = Convert.ToChar(c.cnvc_remote);
                    cv_stop = c.cnvc_stop;
                    cv_comm = c.cnvc_comm;

                    string ls = c.cnvc_job_no.Trim();

                    for (int i = 0; i < 15; i++)
                    {
                        cv_job_no[i] = ls.Substring(i * 4, 4);
                    }
                }
            }
            catch (Exception E)
            {
                f_msg(9, "f_cvc_info Err=" + E.Message);
            }
            return 0;
        }

        private int f_eror_log(int ai_hogi, string as_ercd)
        {
            //****************************************************************************************************//
            // Stacker-Crane이 error시 err-log기록
            //****************************************************************************************************//
            debugstep = "3";

            string ls_hogi, ls_ercd, ls_date, ls_time;
            string ls_mesg = "", ls_emsg = "", ls_lstk, ls_pltn, ls_gubn;
            int li_hogi;

            li_hogi = ai_hogi;
            ls_hogi = li_hogi.ToString("00");

            ls_ercd = as_ercd.ToString();

            ls_date = DateTime.Now.ToString("yyyyMMdd");
            ls_time = DateTime.Now.ToString("HHmmss");  // hhmmss => HHmmss 20200626

            ls_emsg = "";
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                ls_mesg = d.ExecuteQuery<string>("select scer_mesg from tbscer where scer_ercd = '" + ls_ercd + "'").SingleOrDefault();
                if (ls_mesg == null || ls_mesg == "") ls_emsg = "*** 에러코드 등록바람 ***!!";
            }
            ls_mesg = ls_mesg.Trim();

            f_scc_info(li_hogi);
            if (scc_lstk.Trim() != "")
            {
                ls_lstk = "A" + scc_lstk.Trim();
                ls_pltn = scc_pltn.Trim();
            }
            else
            {
                ls_lstk = "";
                ls_pltn = "";
            }

            ls_gubn = scc_gubn.Trim();

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                string sql = @"insert into tberht 
                                     (erht_date, erht_time, erht_hogi, erht_ercd, erht_mesg, erht_gubn, erht_pltn, erht_lstk, erht_pos, erht_xmov) 
                               values( {0},      {1},       {2},       {3},       {4},       {5},       {6},       {7},       {8},      {9} ) ";
                int rc = d.ExecuteCommand(sql,
                                       ls_date, ls_time, ls_hogi, ls_ercd, ls_mesg, ls_gubn, ls_pltn, ls_lstk, recv_bylv, scc_xmov);

            }
            return 0;
        }

        private int f_gets_bcc(string as_ibuf, ref string as_obuf)
        {
            //***********************************************************************************************//
            // 송,수신 데이타의 BCC 값 얻기
            //***********************************************************************************************//
            debugstep = "4";
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
            debugstep = "5";
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
            debugstep = "6";
            //********************************************************************************
            //* 숫자Numeric를 Hex Character(1자리)로 변환 (N To HexChar)
            //********************************************************************************
            switch (ai_nval)
            {
                case 0:
                    {
                        as_char = '0';
                        break;
                    }
                case 1:
                    {
                        as_char = '1';
                        break;
                    }
                case 2:
                    {
                        as_char = '2';
                        break;
                    }
                case 3:
                    {
                        as_char = '3';
                        break;
                    }
                case 4:
                    {
                        as_char = '4';
                        break;
                    }
                case 5:
                    {
                        as_char = '5';
                        break;
                    }
                case 6:
                    {
                        as_char = '6';
                        break;
                    }
                case 7:
                    {
                        as_char = '7';
                        break;
                    }
                case 8:
                    {
                        as_char = '8';
                        break;
                    }
                case 9:
                    {
                        as_char = '9';
                        break;
                    }
                case 10:
                    {
                        as_char = 'A';
                        break;
                    }
                case 11:
                    {
                        as_char = 'B';
                        break;
                    }
                case 12:
                    {
                        as_char = 'C';
                        break;
                    }
                case 13:
                    {
                        as_char = 'D';
                        break;
                    }
                case 14:
                    {
                        as_char = 'E';
                        break;
                    }
                case 15:
                    {
                        as_char = 'F';
                        break;
                    }
                default:
                    {
                        as_char = '0';
                        break;

                    }
            }
            return 0;
        }

        private void f_init_setting()
        {
            debugstep = "7";
            // initialize all variable and set intial values to global variables
            f_msg(9, "크레인 프로그램 가동되었읍니다.");

            prev_ibuf = "";
            inpt_buff = "";
            oupt_buff = "";
            disp_msg = "";
            g_scc_comd = 0;

            for (int j = 0; j < 5; j++)
            {
                g_scc_strt[j] = 0;
                sc_pwr_wait[j] = 0;                      // SC Control-Power 지령대기 0 = 지령없음, >0 = 지령응답중 
            }
            sc_pwr_onof = "00000000".ToCharArray();
            sc_pwr_onof = "00000000".ToCharArray();
            sc_eror_stat = "00000000".ToCharArray();
            prev_sc_pwr = "00000000".ToCharArray();
            prev_sc_eror = "00000000".ToCharArray();
            g_sc_init = 1;

            recv_stat1 = ""; //= mid(read_data,1,4)         // D10  현재작업상태
            recv_ercd = "";  //= mid(read_data,5,4)         // D11  에러코드
            recv_bylv = "";  //= mid(read_data,9,4)         // D12  현재위치 = bay + levl
            recv_stat2 = ""; //= mid(read_data,13,4)        // D13  현재상세상태
            recv_xio = "";   //= mid(read_data,17,4)        // D14  받은지령모드(0001,002,007)
            recv_xbk1 = "";  //= mid(read_data,21,4)        // D15  받은지령-열1
            recv_xby1 = "";  //= mid(read_data,25,4)        // D16  받은지령-연1
            recv_xlv1 = "";  //= mid(read_data,29,4)        // D17  받은지령-단1
            recv_xbk2 = "";  //= mid(read_data,33,4)        // D18  받은지령-열2
            recv_xby2 = "";  //= mid(read_data,37,4)        // D19  받은지령-연2
            recv_xlv2 = "";  //= mid(read_data,41,4)        // D20  받은지령-단2
            recv_pwron = "";
            recv_emer = "";
            recv_scplt = "";
            recv_remote = "";

            prev_ercd[0] = "";
            prev_ercd[1] = "";
            prev_ercd[2] = "";
            prev_ercd[3] = "";
            prev_ercd[4] = "";

            prev_posi[0] = "";
            prev_posi[1] = "";
            prev_posi[2] = "";
            prev_posi[3] = "";
            prev_posi[4] = "";

            scc_mode = "";
            scc_io = "";
            scc_onln = "";
            scc_pwron = "";
            scc_emer = "";

            scc_stat = "";
            scc_palt = "";
            scc_posi = "";
            scc_eror = "";
            scc_ecod = "";

            scc_stop = "";
            scc_iuse = "";
            scc_ouse = "";

            scc_lstk = "";
            scc_pltn = "";
            scc_jno = "";
            scc_indx = "";
            scc_fstn = "";
            scc_tstn = "";
            scc_xmov = "";
            scc_rset = "0";

            srch_gubn = "";
            srch_jio = "";
            srch_lstk = "";
            srch_pltn = "";
            srch_jno = "";
            srch_indx = "";
            srch_fstn = "";
            srch_tstn = "";
            srch_xmov = "";

            cv_op_onof = "00000000".ToCharArray();
            cv_op_eror = "00000000".ToCharArray();
            cv_buf_palt = "00000000000000000000000000000000000000000000000000".ToCharArray();
            cv_ist_redy = "00000".ToCharArray();
            cv_ist_palt = "00000".ToCharArray();
            cv_ost_redy = "00000".ToCharArray();
            cv_ost_palt = "00000".ToCharArray();

            for (int j = 0; j < 15; j++)
            {
                cv_job_no[j] = "0000";
            }
            for (int j = 0; j < 6; j++)
            {
                cv_chdt[j] = "0000000000000000";
            }

            cv_21_rqst = '0';
            cv_22_rqst = '0';
            cv_remote = '0';

            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                string sql = @"update tbscrc set scrc_mode = 'I3', scrc_comm = '0', scrc_rset = '0' where  scrc_mode in ( 'H0', 'H1' ) ";
                d.ExecuteCommand(sql);

                sql = @"update tbscrc set scrc_rset = '0' where scrc_rset <> '0' ";
                d.ExecuteCommand(sql);
            }

            // port settings
            sPort[0] = new SerialPort("COM5", 9600, Parity.Even, 7, StopBits.One);
            sPort[1] = new SerialPort("COM6", 9600, Parity.Even, 7, StopBits.One);
            sPort[2] = new SerialPort("COM7", 9600, Parity.Even, 7, StopBits.One);
            sPort[3] = new SerialPort("COM8", 9600, Parity.Even, 7, StopBits.One);
            sPort[4] = new SerialPort("COM9", 9600, Parity.Even, 7, StopBits.One);

            try
            {
                sPort[0].Encoding = Encoding.ASCII;
                sPort[0].Open();
                sPortOpened[0] = true;
            }
            catch (Exception E) { f_msg(1, "Com5 port error"); }

            try
            {
                sPort[1].Encoding = Encoding.ASCII;
                sPort[1].Open();
                sPortOpened[1] = true;
            }
            catch (Exception E) { f_msg(2, "Com6 port error"); }

            try
            {
                sPort[2].Encoding = Encoding.ASCII;
                sPort[2].Open();
                sPortOpened[2] = true;
            }
            catch (Exception E) { f_msg(3, "Com7 port error"); }

            try
            {
                sPort[3].Encoding = Encoding.ASCII;
                sPort[3].Open();
                sPortOpened[3] = true;
            }
            catch (Exception E) { f_msg(4, "Com8 port error"); }

            try
            {
                sPort[4].Encoding = Encoding.ASCII;
                sPort[4].Open();
                sPortOpened[4] = true;
            }
            catch (Exception E) { f_msg(5, "Com9 port error"); }
        }
            
        private void f_m_end_proc()
        {
            debugstep = "8";
            //********************************************************************************
            // exit scrc 프로그램
            //********************************************************************************
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand(@"update tbscrc set scrc_comm = '0', scrc_rset = '0' ");
                d.ExecuteCommand(@"update tbscrc set scrc_mode = 'I3', scrc_comm = '0', scrc_rset = '0' where scrc_mode in ( 'H0', 'H1' )");
            }

            for (int i = 0; i < 5; i++) if (sPortOpened[i]) { sPort[i].Close(); sPort[i].Dispose(); }

            f_msg(9, "크레인 프로그램 정지되었읍니다.");
        }
  
        private void f_m_main_proc()
        {

            //********************************************************************************
            // STACKER-CRANE MAIN PROC
            //********************************************************************************
        
            f_cvc_info();              //CVC 정보 체크  f_scc_cntl에서 뺏음 한번만 읽어어도 되니까...       
           
            string ls_ibuf = "";
            int li_hogi = 0;
            for (int j = 0; j < 5 ; j++)
            {               
                if (!sPortOpened[j]) continue;

                li_hogi = j;
                ls_ibuf = "";
               
                if (f_read_proc(li_hogi, ref ls_ibuf) == 0)
                {                   
                    g_comm[j] = 0;
                    f_scc_icvrt(j, ls_ibuf);                  
                    f_scc_cntl(j);
                   
                    if (g_sc_init == 0)
                    {                       
                        if ((sc_pwr_onof[j] != prev_sc_pwr[j]) || (sc_eror_stat[j] != prev_sc_eror[j]))
                        {
                            prev_sc_pwr[j] = sc_pwr_onof[j];
                            prev_sc_eror[j] = sc_eror_stat[j];
                            f_scpwr_cvwrit();
                        }
                    }
                }
                else
                {
                    g_comm[j] = g_comm[j] + 1;
                    if (g_comm[j] > 4)
                    {
                        g_comm[j] = 1;
                    }
                }
            }
          
            if (g_sc_init == 1)
            {
                f_scpwr_cvwrit();
            }
            g_sc_init = 0;

            Thread.Sleep(200);
        }

        private int f_make_writ_data(int ai_hogi, int as_comd, ref string as_obuf)
        {
            debugstep = "9";
            //****************************************************************************************************//
            // S/C#1 에 지령-데이타 만들기
            //****************************************************************************************************//
            // D0-          입출모드(0001=입고,0002=출고,0007=이동동작)
            // D1-          지령값(from)   input = 0001,3,5,7,9       output = bank
            // D2-                                 0001                        bay
            // D3-                                 0001                        level
            // D4-          지령값(to)     input = bank               output = 0002,4,6,8,10
            // D5-                                 bay                         0001
            // D6-                                 level                       0001
            //****************************************************************************************************//
            string writ_data = "", comd_mode = "", from_data = "", to_data = "";
            string ls_bank = "", ls_bay = "", ls_levl = "";
            string ls_inst = "", ls_oust = "", ls_hogi = "";
            int li_hogi = 0, li_comd = 0, li_mstn = 0; ;

            li_hogi = ai_hogi;
            ls_hogi = (li_hogi + 1).ToString("00"); // for display log

            writ_data = "";
            disp_msg = "";

            li_comd = as_comd;
            as_obuf = "";

            switch (li_comd)
            {
                case 1:  // 입고 Data = fstn = 21,23  tstn = 01,03,05,07,09
                    {
                        ls_bank = "00" + srch_lstk.Substring(0, 2);
                        ls_bay = "00" + srch_lstk.Substring(2, 2);
                        ls_levl = "00" + srch_lstk.Substring(4, 2);
                        ls_inst = "00" + srch_tstn.Trim();
                        comd_mode = "0001";                        //D0
                        from_data = ls_inst + "0001" + "0001";     //D1,D2,D3
                        to_data = ls_bank + ls_bay + ls_levl;
                        disp_msg = "S/C#" + ls_hogi + " 입고지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 2:  // 출고 Data = fstn = 02,04,06,08,10  tstn = 43,45
                    {
                        ls_bank = "00" + srch_lstk.Substring(0, 2);
                        ls_bay = "00" + srch_lstk.Substring(2, 2);
                        ls_levl = "00" + srch_lstk.Substring(4, 2);
                        ls_oust = "00" + srch_fstn.Trim();
                        comd_mode = "0002";                        //D0
                        from_data = ls_bank + ls_bay + ls_levl;    //D1,D2,D3
                        to_data = ls_oust + "0001" + "0001";       //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 출고지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 3: // HOME 이동 Data (HOME)           fstn = 01,03,05,07,09  tstn = 43,45
                    {
                        li_mstn = li_hogi * 2 + 1;
                        ls_inst = "00" + li_mstn.ToString("00");
                        comd_mode = "0007";                        //D0
                        from_data = ls_inst + "0001" + "0001";     //D1,D2,D3
                        to_data = "FFFF" + "FFFF" + "FFFF";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 이동지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 5:// 이중입고 RESET(작업완료 0008일때만 가능)
                    {
                        comd_mode = "000E";                        //D0
                        from_data = "000A" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";        //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 이중입고RESET지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 6: // 공출고   RESET(작업완료 0008일때만 가능)
                    {
                        comd_mode = "000E";                        //D0
                        from_data = "000B" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 공출고 RESET지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 7: // DATA이상 RESET(작업완료 0008일때만 가능)
                    {
                        comd_mode = "000E";                        //D0
                        from_data = "000D" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " DATA 이상 RESET지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 8: // 원격모드 RESET(POWER SOURCE 차단지령)   - 원격이고, power on시
                    {
                        comd_mode = "000C";                       //D0
                        from_data = "0000" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 원격외 모드 전환지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 9:  // 원격모드 SET  (POWER SOURCE 공급지령)   - 원격이고, power off시 
                    {
                        comd_mode = "000C";                        //D0
                        from_data = "0009" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 원격모드 전환 SET지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                case 11: // 작업완료 RESET
                    {
                        comd_mode = "000D";                        //D0
                        from_data = "000E" + "0000" + "0000";      //D1,D2,D3
                        to_data = "0000" + "0000" + "0000";      //D4,D5,D6
                        disp_msg = "S/C#" + ls_hogi + " 작업완료 RESET 지령";
                        f_msg(li_hogi, disp_msg);
                        break;
                    }
                default:
                    {
                        g_scc_comd = 0;
                        return -1;
                    }
            }

            //               D0     D1 - D3     D4 - D6  
            writ_data = comd_mode + from_data + to_data;
            as_obuf = writ_data;

            return 0;
        }

        private void f_msg(int ai_hogi, string as_msg)
        {
           
            //****************************************************************************************************//
            // 화면에 debug 보여줌
            //****************************************************************************************************//

            bw.ReportProgress(ai_hogi, debugstep + " " +  as_msg);         
        
        }

        private int f_read_proc(int ai_hogi, ref string as_ibuf)
        {
            debugstep = "11";
            //***********************************************************************************************//
            // ENQ+01+FFWR5+D0010+0B+BCC1,2+CRLF  -->
            //                                    <--    STX+01+FF+READ-DATA+ETX+BCC1,2+CRLF
            // ACK+01+FF+CRLF or NAK+01+FF+CRLF   -->
            //***********************************************************************************************//
            int li_hogi = 0, rc, dlen, li_step;
            int ll_stx, ll_etx, ll_xcr, ll_xlf;
            string ls, read_buff, read_data, ls_hogi;
            string ls_wdata, ls_bcc = "", ls_rdata, ls_rchek, ls_rbcc;
            char ls_ch;
            bool success = false;

            as_ibuf = "";
            li_hogi = ai_hogi;
            ls_hogi = (li_hogi + 1).ToString("00");

            if (!sPortOpened[li_hogi])
            {
                f_msg(li_hogi, "Port Not Opend...!!");
                return -2;
            }
            //*****************************************************************************************
            //STEP1= send read-command to S/C#1-plc
            //*****************************************************************************************
            ls_wdata = ls_hogi + "FFWR5D00100B";
            if (f_gets_bcc(ls_wdata, ref ls_bcc) != 0)
            {
                f_msg(li_hogi, "R_Comd Bcc Eror...!!");
                return -1;
            }

            oupt_buff = "";
            oupt_buff = ENQ + ls_hogi + "FFWR5D00100B" + ls_bcc + XCR + XLF;
            sPort[li_hogi].Write(oupt_buff);

            disp_msg = "R_Comd=[" + oupt_buff + "]";
            f_msg(li_hogi, disp_msg);
            Thread.Sleep(200);

            //*****************************************************************************************
            //STEP2= 상태 데이타 수신
            //*****************************************************************************************
            inpt_buff = "";
            read_data = "";
            dlen = 0;
            li_step = 0;
            for (int j = 0; j < 15; j++)
            {
                read_buff = "";
                Thread.Sleep(100);
               
                read_buff = sPort[li_hogi].ReadExisting();
                if (read_buff.Length > 0)
                {
                    inpt_buff = inpt_buff + read_buff;
                }
                else continue;

                dlen = inpt_buff.Length;
                if (dlen > 53) break;

            }
            disp_msg = "R_Data=[" + inpt_buff + "]";
            f_msg(li_hogi, disp_msg);
            Thread.Sleep(100);

            //*****************************************************************************************
            //STEP3= 수신데이타 체크, ACK SENDING
            //*****************************************************************************************
            if (dlen > 1)
            {   
                ll_stx = inpt_buff.IndexOf(STX, 0);
                if (ll_stx < 0) return -1;

                ll_etx = inpt_buff.IndexOf(ETX, ll_stx + 1);
                if (ll_etx < 0) return -2;
                          
                // checking read data
                if ((ll_stx + 4 + 44 + 1) != ll_etx) return -1;
                ls = inpt_buff.Substring(ll_stx + 4 + 1, 44);
                ls_rchek = inpt_buff.Substring(ll_stx + 1, ll_etx - ll_stx); // Bcc Cheking Data
                ls_rbcc = inpt_buff.Substring(ll_etx + 1, 2);                 // read Bcc-values
                for (int i = 0; i < 44; i++)
                {
                    ls_ch = ls.Substring(i, 1).ToCharArray()[0];

                    switch (ls_ch)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case 'A':
                        case 'B':
                        case 'C':
                        case 'D':
                        case 'E':
                        case 'F': break;
                        default:
                            {
                                ls = ""; read_data = "";
                                return -3;
                            }
                    }
                }
            }
            else
            {
                disp_msg = "No Read_Data...!!";
                f_msg(li_hogi, disp_msg);
                return -8;
            }
            //*****************************************************************************************
            // 수신된 상태 데이타 저장.  
            //*****************************************************************************************

            read_data = ls;
            if (f_gets_bcc(ls_rchek, ref ls_bcc) == 0)
            {
                if (ls_rbcc.Trim() != ls_bcc.Trim())
                {
                    disp_msg = "Read_Data Bcc Eror...!!=PLC_BCC=[" + ls_rbcc + "], My_BCC=[" + ls_bcc + "]";
                    f_msg(li_hogi, disp_msg);
                    return -1;
                }
            }
            else
            {
                disp_msg = "Read_Data Bcc 계산 불능 !!";
                f_msg(li_hogi, disp_msg);
                return -1;
            }

            as_ibuf = read_data.Trim();
            disp_msg = "READ_DATA=[" + read_data + "]";
            f_msg(li_hogi, disp_msg);

            return 0;
        }

        private int f_scc_cntl(int ai_hogi)
        {
            debugstep = "12";
            //*****************************************************************************************************//
            //* SCC 호기별 제어처리
            //*****************************************************************************************************//
            string ls_hogi = "", ls_obuf = "";
            int li_hogi = 0;

            li_hogi = ai_hogi;
            ls_hogi = (li_hogi + 1).ToString("00");

            //*********************************************************************************************//
            // SC가 비상시나, OFF-LINE MODE시는 제어처리를 하지 않는다
            //*********************************************************************************************//

            if (recv_emer != "0") return -1;
            if (recv_remote != "1") return -1;
            //*********************************************************************************************//
            // SC와 CV정보를 얻는다.
            //*********************************************************************************************//
            if (f_scc_info(li_hogi) != 0) return -1;         //SCC 정보 체크
            f_msg(li_hogi, "f_scc_info");

            //*********************************************************************************************//
            // SC 제어처리 = 상태정보를 분석하여, 완료처리나, 입출지시를 탐색 한다.
            //*********************************************************************************************//
            g_scc_comd = 0;
            f_scc_proc(li_hogi);
            f_msg(li_hogi, "f_scc_proc(li_hogi) 밑에g_scc_comd = " + g_scc_comd.ToString());
            // 탐색된 입출지시나 리셋을 처리 한다.
            switch (g_scc_comd)
            {
                case 0:
                    {
                        if (f_scc_rest_chek(li_hogi) == 0)    // g_scc_comd = 5,6,7 ..
                        {
                            f_make_writ_data(li_hogi, g_scc_comd, ref ls_obuf);

                            if (f_writ_proc(li_hogi, ls_obuf) == 0)
                            {
                                if (g_scc_comd == 5)
                                {
                                    disp_msg = "***** 이중입고 RSET 지령입니다.*****";          //DATA유 시
                                    f_msg(li_hogi, disp_msg);
                                }
                                if (g_scc_comd == 6)
                                {
                                    disp_msg = "***** 공출고   RSET 지령입니다.*****";         //DATA유 시
                                    f_msg(li_hogi, disp_msg);
                                }
                                if (g_scc_comd == 7)
                                {
                                    disp_msg = "***** Data이상 RSET 지령입니다.*****";          //DATA유 시
                                    f_msg(li_hogi, disp_msg);
                                }
                            }
                        }
                        break;
                    }
                default:
                    {
                        ls_obuf = "";
                        f_make_writ_data(li_hogi, g_scc_comd, ref ls_obuf);
                        if (f_writ_proc(li_hogi, ls_obuf) == 0)
                        {
                            if (g_scc_comd == 1)
                            {
                                disp_msg = "***** 입고지령입니다.*****";          //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                f_scc_i0_proc(li_hogi);
                            }
                            else if (g_scc_comd == 2)
                            {
                                disp_msg = "***** 출고지령입니다.*****";         //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                f_scc_o0_proc(li_hogi);
                            }
                            else if (g_scc_comd == 3)
                            {
                                disp_msg = "***** 이동지령입니다.*****";        //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                f_scc_h0_proc(li_hogi);
                            }                            
                            else if (g_scc_comd == 8)
                            {
                                disp_msg = "***** CONTROL 전원 차단 지령입니다.*****";        //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                sc_pwr_wait[li_hogi] = 1;
                            }
                            else if (g_scc_comd == 9)
                            {
                                disp_msg = "***** CONTROL 전원 공급 지령입니다.*****";        //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                sc_pwr_wait[li_hogi] = 1;
                            }
                            else if (g_scc_comd == 11)
                            {
                                disp_msg = "***** 완료지령입니다.*****";        //DATA유 시
                                f_msg(li_hogi, disp_msg);
                                sc_pwr_wait[li_hogi] = 1;
                                f_scc_ee_proc(li_hogi);
                            }
                            else { }
                        }
                        break;
                    }
            }
            return 0;

        }
        private int f_scc_icvrt(int ai_hogi, string as_rdata)
        {
            debugstep = "13";
            //***********************************************************************************************//
            // SRC로부터 수신된 상태 데이타 저장.
            //***********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            char[] ls_bits = new char[4] { '0', '0', '0', '0' };
            string ls_bit1 = "", ls_bit2 = "", ls_desc = "", ls_eror = "", lls = "";

            string ls_sdata = as_rdata.Trim();            // 현재 S/C 상태 DATA
            scc_comm = "1";

            recv_stat1 = ls_sdata.Substring(0, 4);         // D10  현재작업상태
            recv_ercd = ls_sdata.Substring(4, 4);          // D11  에러코드
            recv_bylv = ls_sdata.Substring(8, 4);          // D12  현재위치 = bay + levl
            recv_stat2 = ls_sdata.Substring(12, 4);        // D13  현재상세상태
            recv_xio = ls_sdata.Substring(16, 4);          // D14  받은지령모드(0001,0002,0007)
            recv_xbk1 = ls_sdata.Substring(20, 4);         // D15  받은지령-열1
            recv_xby1 = ls_sdata.Substring(24, 4);         // D16  받은지령-연1
            recv_xlv1 = ls_sdata.Substring(28, 4);         // D17  받은지령-단1
            recv_xbk2 = ls_sdata.Substring(32, 4);         // D18  받은지령-열2
            recv_xby2 = ls_sdata.Substring(36, 4);         // D19  받은지령-연2
            recv_xlv2 = ls_sdata.Substring(40, 4);         // D20  받은지령-단2

            string ls_stat = recv_stat2.Substring(0, 1);   // D13  현재상세상태
            if (f_char_to_bits(ls_stat, ref ls_bit1, ref ls_bit2) == 0)
            {
                ls_bits = ls_bit2.ToCharArray();
                recv_pwron = new string(ls_bits[1], 1);
                sc_pwr_onof[li_hogi] = recv_pwron.ToCharArray()[0];
                recv_emer = new string(ls_bits[2], 1);
            }

            // power command 지시중이면 
            if (sc_pwr_wait[li_hogi] != 0)
            {
                sc_pwr_wait[li_hogi] = sc_pwr_wait[li_hogi] + 1;
                if (sc_pwr_wait[li_hogi] >= 3)
                {
                    sc_pwr_wait[li_hogi] = 0;
                }
            }
            ls_stat = recv_stat2.Substring(1, 1);              // D13  현재상세상태
            if (f_char_to_bits(ls_stat, ref ls_bit1, ref ls_bit2) == 0)
            {
                ls_bits = ls_bit2.ToCharArray();
                recv_scplt = new string(ls_bits[0], 1);
                recv_remote = new string(ls_bits[3], 1);
            }
            ls_stat = recv_stat2.Substring(3, 1);                // D13  현재상세상태
            if (f_char_to_bits(ls_stat, ref ls_bit1, ref ls_bit2) == 0)
            {
                ls_desc = ls_bit2.Trim();
            }

            sc_eror_stat[li_hogi] = '0';
            if (recv_stat1 == "0008")               // S/C ERROR시 
            {
                sc_eror_stat[li_hogi] = '1';
                switch (recv_ercd.Trim())
                {
                    case "0000":   //에러 무
                        {
                            ls_eror = "0";
                            break;
                        }
                    case "0091":    // 이중입고
                        {
                            ls_eror = "D";
                            break;
                        }
                    case "0092":    // 공출고
                        {
                            ls_eror = "E";
                            break;
                        }
                    case "0093":    //  DATA이상
                        {
                            ls_eror = "Q";
                            break;
                        }
                    default: // 기타 에러 
                        {
                            ls_eror = "G";
                            break;
                        }
                }
                if (recv_ercd.Trim() != "0000")
                {
                    if (prev_ercd[li_hogi] != recv_ercd)         // Eror Log-Proc
                    {
                        f_eror_log(li_hogi, recv_ercd);
                    }
                }
                prev_ercd[li_hogi] = recv_ercd;
            }
            else
            {
                ls_eror = "0";
                prev_ercd[li_hogi] = recv_ercd.Trim();
            }

            if (recv_remote == "1") lls = "자동모드 =";
            else lls = "수동모드 =";

            if (recv_pwron == "0")
            {
                disp_msg = lls + ">파워 OFF 상태입니다...!!=[" + recv_stat1 + "]";
                f_msg(li_hogi, disp_msg);
            }
            if (recv_emer == "1")
            {
                disp_msg = lls + ">비상정지 상태입니다...!!=[" + recv_stat1 + "]";
                f_msg(li_hogi, disp_msg);
            }

            disp_msg = "";
            switch (recv_stat1)
            {
                case "0001": { disp_msg = lls + "> 하무대기 상태입니다...!!"; break; }
                case "0002": { disp_msg = lls + "> 하유대기 상태입니다...!!"; break; }
                case "0007": { disp_msg = lls + "> 작업중상태입니다...!!"; break; }
                case "0008":               // SC 에러상태
                    {
                        disp_msg = lls + "> 에러상태입니다...!!=[" + recv_ercd + "]";
                        switch (recv_ercd)
                        {
                            case "0091": { disp_msg = "이중입고" + disp_msg; break; }
                            case "0092": { disp_msg = "공출고" + disp_msg; break; }
                            case "0093": { disp_msg = "DATA이상" + disp_msg; break; }
                            default: { disp_msg = "기타" + disp_msg; break; }
                        }
                        break;
                    }
                case "0009": { disp_msg = lls + "> 작업완료상태입니다...!! "; break; }
                default: { disp_msg = lls + ">기타상태입니다...!!=[" + recv_stat1 + "]"; break; }
            }
            f_msg(li_hogi, disp_msg);

            //***********************************************************************************************

            int li_uflg = 0;
            string[] ls_chdt = new string[11] { "", "", "", "", "", "", "", "", "", "", "" };
            try
            {
                for (int i = 0; i < 11; i++)
                {
                    ls_chdt[i] = ls_sdata.Substring(i * 4, 4);  //0,4,8에서 4개씩
                    if (prev_chdt[li_hogi][i] != ls_chdt[i])
                    {
                        li_uflg = 1;
                    }
                    prev_chdt[li_hogi][i] = ls_chdt[i];
                }

            }
            catch (Exception E)
            {
                f_msg(li_hogi, "channel convert error" + E.Message);
            }
            string sql;
            if (li_uflg == 1)
            {
                if (recv_ercd != "0000")
                {

                    sql = @"update tbscrc 
                            set scrc_onln = {0}, scrc_pwron = {1}, scrc_emer = {2},
                                scrc_stat = {3}, scrc_palt  = {4}, scrc_posi = {5},
                                scrc_eror = {6}, scrc_ecod  = {7}, scrc_chdt = {8},  scrc_comm = '1' 
                            where scrc_no   = {9} ";
                }
                else
                {
                    sql = @"update tbscrc 
                            set scrc_onln = {0}, scrc_pwron = {1}, scrc_emer = {2},
                                scrc_stat = {3}, scrc_palt  = {4}, scrc_posi = {5},
                                scrc_eror = {6}, scrc_ecod  = {7}, scrc_chdt = {8},  scrc_comm = '1', scrc_rset = '0'
                            where scrc_no   = {9} ";
                }
                try
                {
                    using (DBDataContext d = new DBDataContext(Config.DBCon))
                    {
                        d.ExecuteCommand(sql,
                            recv_remote, recv_pwron, recv_emer, recv_stat1, recv_scplt, recv_bylv, ls_eror, recv_ercd, ls_sdata, ls_hogi);
                    }
                }
                catch (Exception E)
                {
                    f_msg(li_hogi, E.Message + Environment.NewLine + "update tbscrc f_scc_icvrt Error");
                    f_msg(li_hogi, recv_remote + "-" + recv_pwron + "-" + recv_emer + "-" + recv_stat1 + "-" + recv_scplt + "-" + recv_bylv + "-" + ls_eror + "-" + recv_ercd + "-" + ls_sdata + "-" + ls_hogi);

                }
            }

            return 0;
            //*******************************************************************************************//
            //     [D13=SC상태정보]
            //*******************************************************************************************//
            // 0 :  
            // 1 : crane power on
            // 2 : emergency stop
            // 3 :  
            //-------------------------------------------------------------------------------------------//
            // 4 : 1=PLT유,0=PLT무  
            // 5 : 
            // 6 : 
            // 7 : 1=원격, 0=원격외 모드  
            //-------------------------------------------------------------------------------------------//
            // 8 : 1=승강하위치  
            // 9 : 1=포크센타
            // A : 1=포크좌측끝
            // B : 1=포크우측끝
            //-------------------------------------------------------------------------------------------//
            // C : 1=주행원점  
            // D : 1=주행정위치
            // E : 1=승강원점
            // F : 1=승강상위치
            //-------------------------------------------------------------------------------------------//

        }
        private int f_scc_comm_eror(int ai_hogi)
        {
            debugstep = "14";
            //********************************************************************************************//
            //* SC와 통신이 않될때
            //********************************************************************************************//

            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.ExecuteCommand("update tbscrc set scrc_comm = '0', scrc_rset = '0', scrc_mesg = '' where scrc_no = '" + ls_hogi + "'");
            }

            return 0;
        }

        private int f_scc_ee_proc(int ai_hogi)
        {
            debugstep = "15";
            //********************************************************************************************//
            //* 기타 RESET 동작 지령후  ACK시 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand("update tbscrc set scrc_rset = '0', scrc_mesg = '' where scrc_no ='" + ls_hogi + "'");
                }
            }
            catch (Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_ee_proc ERROR"); }

            return 0;
        }
 
        private int f_scc_h0_proc(int ai_hogi)
        {
            debugstep = "16";
            //********************************************************************************************//
            //* HOME 이동지령 송신 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand("update tbscrc set scrc_mode = 'H1', scrc_mesg = 'HOME 이동지령 송신', scrc_comm = '1', scrc_rset = '0' " +
                                     "  where scrc_no = '" + ls_hogi + "' and scrc_mode = 'H0' ");
                }
            }
            catch (Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_h0_proc ERROR"); }

            return 0;
        }
        private int f_scc_h1_proc(int ai_hogi)
        {
            debugstep = "17";
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand("update tbscrc set scrc_mode = 'I3', scrc_mesg = '',scrc_comm = '1', scrc_rset = '0' " +
                                      " where scrc_no = '" + ls_hogi + "' and scrc_mode in ('H0','H1')");
                }
            }
            catch (Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_h1_proc ERROR"); }

            return 0;
        }
        private int f_scc_i0_proc(int ai_hogi)
        {
            debugstep = "18";
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            string ls_tkno = scc_fstn.Trim();
            string ls_jno = scc_jno.Trim();
            string ls_lstk = scc_lstk.Trim();
            string ls_pltn = scc_pltn.Trim();

            //* 해당 입고ST 지령나감( 'W' -> 'X' )
            string ls_edat = DateTime.Now.ToString("yyyyMMddHHmmss");  // hh=>HH 20200626
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    int rc = d.ExecuteCommand("update tbscrc set scrc_mode = 'I1', scrc_mesg = '입고지령 송신 ',scrc_comm = '1', scrc_rset = '0' " +
                                              " where scrc_no = '" + ls_hogi + "' and scrc_mode = 'I0' ");
                    if (rc > 0)
                    {
                        d.ExecuteCommand("update tbindx set indx_edat = '" + ls_edat + "', indx_sflg = 'X' " +
                                           " where indx_fstn = '" + ls_tkno + "'" +
                                           " and indx_jno    = '" + ls_jno + "'" +
                                           " and indx_pltn   = '" + ls_pltn + "'" +
                                           " and indx_lstk   = '" + ls_lstk + "'");

                    }

                }
            }
            catch (Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_i0_proc ERROR"); }
            return 0;
        }
 
        private int f_scc_i1_proc(int ai_hogi)
        {
            debugstep = "19";
            //********************************************************************************************//
            //* 입고대 LOAD 완료시 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    d.ExecuteCommand("update tbscrc set scrc_mode = 'I2', scrc_mesg = '입고대 LOAD 완료 ', scrc_comm = '1', scrc_rset = '0' " +
                                     " where scrc_no = '" + ls_hogi + "' and scrc_mode = 'I1' ");

                }
            }
            catch (Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_i1_proc ERROR"); }
            return 0;
        }

        private int f_scc_i2_proc(int ai_hogi)
        {
            debugstep = "20";
            //********************************************************************************************//
            //* Rack 입고완료시 ( 'X' -> 'Z' ) 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            string ls_jno = scc_jno.Trim();
            string ls_jio = scc_io.Trim();
            string ls_indx = scc_jno.Substring(14, 4);
            string ls_lstk = scc_lstk.Trim();
            string ls_pltn = scc_pltn.Trim();
            string ls_fstn = scc_fstn.Trim();
            string ls_tstn = scc_tstn.Trim();
            string ls_gubn = scc_gubn.Trim();
            string ls_xmov = scc_xmov.Trim();

            string sql = @" update tbscrc
                          set scrc_mode = 'I3', 
                              scrc_gubn = '', 
                              scrc_io = '', 
                              scrc_lstk = '', 
                              scrc_pltn = '', 
                              scrc_jno = '', 
                              scrc_indx = '', 
                              scrc_fstn = '', 
                              scrc_tstn = '', 
                              scrc_xmov = '', 
                              scrc_mesg = 'RACK 입고 완료 ', 
                              scrc_comm = '1', 
                              scrc_rset = '0' 
                          where scrc_no = {0} and scrc_mode = 'I2' ";


            string sql2 = @"insert into tbevnt
                                ( evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn,
                                  evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg, evnt_wdate )
                         values (  {0},      'I',        {1},        {2},      {3}, 
                                   {4},      {5},        {6},        'X',      'F',       '0',      {7} )";
            int rc = 0;
         
            string ls_date = DateTime.Now.ToString("yyyyMMdd");
            string ls_time = DateTime.Now.ToString("HHmmss");
            string ls_edat = ls_date + ls_time;
            string ls_loca = 'A' + ls_lstk;
            string ls_scno = ls_hogi.Substring(1, 1);

         
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                d.Connection.open();
                using(d.Transaction = d.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = d.ExecuteCommand(sql, ls_hogi);
                        if (rc > 0)
                        {
                            if (ls_gubn != "R") // 입고대 강제 데이타 만든 경우가 아니면 기록
                            {      
                                rc = d.ExecuteCommand(sql2, ls_gubn, ls_scno, ls_fstn, ls_tstn, ls_pltn, ls_loca, ls_xmov, ls_edat);
                                if (rc <= 0)
                                {
                                    d.Transaction.Rollback();
                                    return 0;
                                }                              
                            }
                            rc = d.ExecuteCommand("delete from tbindx where indx_jno  = '" + ls_jno + "' and indx_pltn = '" + ls_pltn + "' ");
                            d.Transaction.Commit();
                            
                        }
                    }
                    catch (Exception E)
                    {
                        d.Transaction.Rollback();
                        f_msg(li_hogi, "f_scc_i2_proc Error");
                    }
                    finally
                    {
                        d.Connection.Close();
                    }
                }                
            }           
         
            return 0;
        }
          
        private int f_scc_i3_proc(int ai_hogi)
        {
            debugstep = "21";
            //********************************************************************************************//
            //* I3 상태 : 출고 / 입고 탐색
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            int srch_ok = 0;
            srch_ok = f_scc_oupt_srch(li_hogi);

            if (srch_ok != 1)
            {
                srch_ok = f_scc_inpt_srch(li_hogi);
            }

            return srch_ok;
        } 
      
        private int f_scc_info(int ai_hogi)
        {
            debugstep = "22";
            //*******************************************************************************************//
            //     stacker 정보 얻어오기
            //*******************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    var s = d.tbscrcs.Where(x => x.scrc_no == ls_hogi).SingleOrDefault();
                    if (s == null)
                    {
                        return -1;
                    }
                    scc_mode = s.scrc_mode.Trim();
                    scc_gubn = s.scrc_gubn.Trim();
                    scc_io = s.scrc_io.Trim();
                    scc_onln = s.scrc_onln.Trim();
                    scc_pwron = s.scrc_pwron.Trim();
                    scc_emer = s.scrc_emer.Trim();
                    scc_stat = s.scrc_stat.Trim();
                    scc_palt = s.scrc_palt.Trim();
                    scc_posi = s.scrc_posi.Trim();
                    scc_eror = s.scrc_eror.Trim();
                    scc_ecod = s.scrc_ecod.Trim();

                    scc_stop = s.scrc_stop.Trim();
                    scc_iuse = s.scrc_iuse.Trim();
                    scc_ouse = s.scrc_ouse.Trim();

                    scc_lstk = s.scrc_lstk.Trim();
                    scc_pltn = s.scrc_pltn.Trim();
                    scc_jno = s.scrc_jno.Trim();
                    scc_indx = s.scrc_indx.Trim();
                    scc_fstn = s.scrc_fstn.Trim();
                    scc_tstn = s.scrc_tstn.Trim();
                    scc_xmov = s.scrc_xmov.Trim();
                    scc_comm = s.scrc_comm.Trim();
                    scc_rset = s.scrc_rset.Trim();
                }
            }
            catch (Exception E)
            {
                f_msg(li_hogi, "f_scc_info Error=" + E.Message);
            }
            return 0;
        }

        private int f_scc_inpt_srch(int ai_hogi)
        {
            debugstep = "23";
            //**********************************************************************************************************//
            //* 입고 데이타 탐색 처리
            //**********************************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            string ls_tkno = "", ls_cv_indx = "", ls_lstk = "", in_bank = "";

            g_scc_comd = 0;
            f_msg(li_hogi, "f_scc_inpt_srch");
            srch_jio = "I";
            srch_lstk = "";
            srch_pltn = "";
            srch_indx = "";
            srch_fstn = "";          // ST21,ST22,ST50
            srch_tstn = "";          // SC#1 = ST01, SC#2 = ST03, SC#3 = ST05, SC#4 = ST07, SC#5 = ST09
            srch_xmov = "I";
            if (cv_comm == "0")      // CVC 통신불능이면 SC 입출지령 처리않됨
            {
                return -1;
            }

            //* 입고대 버퍼에 INDX데이타가 있는지, READY인지 조건 체크 
            // S/C#01 입고대
            if (ls_hogi == "01")
            {
                if (cv_op_onof[0] != '1' || cv_op_eror[0] != '0') return -1;
                if (cv_ist_redy[0] != '1' || cv_ist_palt[0] != '1') return -1;
                if (cv_job_no[0] == "0000") return -1;
                ls_tkno = "01";
                ls_cv_indx = cv_job_no[0];
            }
            // S/C#02 입고대
            if (ls_hogi == "02")
            {
                if (cv_op_onof[1] != '1' || cv_op_eror[1] != '0') return -1;
                if (cv_ist_redy[1] != '1' || cv_ist_palt[1] != '1') return -1;
                if (cv_job_no[2] == "0000") return -1;
                ls_tkno = "03";
                ls_cv_indx = cv_job_no[2];
            }

            // S/C#03 입고대
            if (ls_hogi == "03")
            {
                if (cv_op_onof[2] != '1' || cv_op_eror[2] != '0') return -1;
                if (cv_ist_redy[2] != '1' || cv_ist_palt[2] != '1') return -1;
                if (cv_job_no[4] == "0000") return -1;
                ls_tkno = "05";
                ls_cv_indx = cv_job_no[4];
            }

            // S/C#04 입고대
            if (ls_hogi == "04")
            {
                if (cv_op_onof[3] != '1' || cv_op_eror[3] != '0') return -1;
                if (cv_ist_redy[3] != '1' || cv_ist_palt[3] != '1') return -1;
                if (cv_job_no[6] == "0000") return -1;
                ls_tkno = "07";
                ls_cv_indx = cv_job_no[6];
            }

            // S/C#05 입고대
            if (ls_hogi == "05")
            {
                if (cv_op_onof[4] != '1' || cv_op_eror[4] != '0') return -1;
                if (cv_ist_redy[4] != '1' || cv_ist_palt[4] != '1') return -1;
                if (cv_job_no[8] == "0000") return -1;
                ls_tkno = "09";
                ls_cv_indx = cv_job_no[8];
            }
            if (scc_iuse != "1") return -1;         //입고 금지 이면 

            //*******************************************************************************************************//
            //* READY이고, INDX가 있으면, 실제 입고 데이타 탐색 처리(W)
            //*******************************************************************************************************//
            string ls_jno = string.Empty;

            string ls_indx = "", ls_gubn = "", ls_jio = "", ls_fstn = "", ls_tstn = "";
            string ls_pltn = "", ls_loca = "", ls_xmov = "", ls_edat = "", ls_sflg = "", ls_uflg = "";

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {

                ls_jno = d.tbindxes.Where(x => x.indx_jio == "I"
                                              && x.indx_indx == ls_cv_indx
                                              && x.indx_tstn == ls_tkno
                                              && x.indx_sflg == "W"
                                          ).Min(m => m.indx_jno);

                if (ls_jno == null) return -4;
                if (ls_jno == "") return -4;            

                var q = d.tbindxes.Where(x => x.indx_jno == ls_jno 
                                              && x.indx_jio == "I"
                                              && x.indx_tstn == ls_tkno
                                              && x.indx_sflg == "W").SingleOrDefault();

                if (q == null) return -5;

                ls_indx = q.indx_indx.Trim();
                ls_gubn = q.indx_gubn.ToString().Trim();
                ls_jio = q.indx_jio.ToString().Trim();
                ls_fstn = q.indx_fstn.Trim();
                ls_tstn = q.indx_tstn.Trim();
                ls_pltn = q.indx_pltn.Trim();
                ls_loca = q.indx_lstk.Trim();
                ls_xmov = q.indx_xmov.ToString().Trim();

                ls_sflg = q.indx_sflg.ToString().Trim();
                ls_uflg = q.indx_uflg.ToString().Trim();

                if (ls_pltn == "") return -4;
                if (ls_loca == "") return -4;
                if (ls_sflg != "W") return -1;

                //************************************************************************************************// 
                // 자기 입고위치가 아니면 불가
                //************************************************************************************************// 
                ls_lstk = ls_loca.Substring(1, 6);
                in_bank = ls_lstk.Substring(0, 2);

                if (ls_hogi == "01")
                {
                    if (!(in_bank == "01" || in_bank == "02")) return -1;
                }
                if (ls_hogi == "02")
                {
                    if (!(in_bank == "03" || in_bank == "04")) return -1;
                }
                if (ls_hogi == "03")
                {
                    if (!(in_bank == "05" || in_bank == "06")) return -1;
                }
                if (ls_hogi == "04")
                {
                    if (!(in_bank == "07" || in_bank == "08")) return -1;
                }
                if (ls_hogi == "05")
                {
                    if (!(in_bank == "09" || in_bank == "10")) return -1;
                }

                srch_pltn = ls_pltn;
                srch_lstk = ls_lstk;
                srch_jno = ls_jno;
                srch_indx = ls_indx;
                srch_fstn = ls_fstn;
                srch_tstn = ls_tstn;
                srch_xmov = ls_xmov;

                string sql1 = @"update  tbscrc
                                 set scrc_mode = 'I0',
                                     scrc_gubn = {0},
                                     scrc_io = 'I', 
                                     scrc_eror = '0', 
                                     scrc_ecod = '', 
                                     scrc_lstk = {1},  
                                     scrc_pltn = {2}, 
                                     scrc_jno =  {3},
                                     scrc_indx = {4}, 
                                     scrc_fstn = {5},  
                                     scrc_tstn = {6}, 
                                     scrc_xmov = {7}, 
                                     scrc_mesg = '입고대기',  
                                     scrc_comm = '1', 
                                     scrc_rset = '0' 
                                 where scrc_no = {8}
                                  and  scrc_onln = '1' 
                                  and  scrc_pwron = '1' 
                                  and  scrc_emer = '0' 
                                  and  scrc_eror = '0' 
                                  and  scrc_iuse = '1' 
                                  and  scrc_stop = '0' ";

                //*******************************************************************************************************//
                //* 해당 입고 콘베어 SC 작업중( 'W' -> 'X' )
                //*******************************************************************************************************//
                string sql2 = @"update tbindx set indx_edat = {0}, indx_sflg = 'X' 
                            where indx_jno = {1}
                            and  indx_jio  = 'I' 
                            and  indx_tstn = {2}
                            and  indx_gubn = {3} 
                            and  indx_sflg = 'W' ";

                d.Connection.open();
                using (d.Transaction = d.Connection.BeginTransaction())
                {
                    try
                    {
                        int r = d.ExecuteCommand(sql1, ls_gubn, srch_lstk, srch_pltn, srch_jno, srch_indx, srch_fstn, srch_tstn, srch_xmov, ls_hogi);
                        if (r > 0)
                        {
                            ls_edat = DateTime.Now.ToString("yyyyMMddHHmmss");
                            r = d.ExecuteCommand(sql2, ls_edat, ls_jno, ls_tkno, ls_gubn);

                            if (r > 0)
                            {
                                d.Transaction.Commit();
                                g_scc_comd = 1;
                            }
                            else
                            {
                                d.Transaction.Rollback();
                                return -1;
                            }
                        }
                        else
                        {
                            d.Transaction.Rollback();
                            return -1;
                        }
                    }
                    catch(Exception E)
                    {
                        d.Transaction.Rollback();
                        return -1;
                    }
                    finally
                    {
                        d.Connection.Close();
                    }                   
                }
            }            
            return 1;
        }
             
        private int f_scc_o0_proc(int ai_hogi)
        {
            debugstep = "24";
            //********************************************************************************************//
            //* 출고 지령 송신 완료시 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            string ls_tkno = scc_fstn;
            string ls_jno = scc_jno;
            string ls_lstk = "A" + scc_lstk;
            string ls_pltn = scc_pltn;
            string sql = string.Empty;
            int rc = 0;
           
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                try
                {
                    d.Connection.open();
                    using (d.Transaction = d.Connection.BeginTransaction())
                    {
                        f_msg(li_hogi, "출고 지령 송신");
                        sql = @"update tbscrc  
                                  set scrc_mode = 'O1', 
                                      scrc_mesg = '출고 지령 송신', 
                                      scrc_comm = '1', 
                                      scrc_rset = '0'
                                where scrc_no = {0} and scrc_mode = 'O0' ";
                        rc = d.ExecuteCommand(sql, ls_hogi);

                        d.Transaction.Commit();                       
                    }
                }
                catch (Exception E)
                {
                    f_msg(li_hogi, "f_scc_o0_proc Error " + E.Message);
                }
                finally
                {
                    d.Connection.Close();
                }
            }
            return 0;
        }
          
        private int f_scc_o1_proc(int ai_hogi)
        {
            debugstep = "25";
            //********************************************************************************************//
            //* Rack 출고완료시 
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            using(DBDataContext d = new DBDataContext(Config.DBCon))
            {
                string sql = @" update tbscrc  
                                   set scrc_mode = 'O2', scrc_mesg = 'RACK 출고 LOAD 완료', scrc_comm = '1', scrc_rset = '0'
                                 where scrc_no = {0}
                                   and scrc_mode = 'O1' ";

                d.ExecuteCommand(sql, ls_hogi);
            }
            return 0;
        }
 
        private int f_scc_o2_proc(int ai_hogi)
        {
            debugstep = "26";
            //********************************************************************************************//
            //* 출고대 UNLOAD 완료시( 'X' -> 'P' )  
            //********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            f_msg(li_hogi, "f_scc_o2_proc");

            string ls_jno = scc_jno.Trim();
            string ls_jio = scc_io.Trim();
            string ls_indx = scc_jno.Trim().Substring(14, 4);
            string ls_lstk = scc_lstk.Trim();
            string ls_pltn = scc_pltn.Trim();
            string ls_fstn = scc_fstn.Trim();
            string ls_tstn = scc_tstn.Trim();
            string ls_gubn = scc_gubn.Trim();
            string ls_xmov = scc_xmov.Trim();

            string ls_edat = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ls_loca = "A" + ls_lstk.Trim();
            string ls_scno = ls_hogi.Substring(1, 1);
            string sql = "";
            int rc = 0;

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                try
                {
                    d.Connection.open();
                    using (d.Transaction = d.Connection.BeginTransaction())
                    {
                        sql = @"update tbscrc  
                                    set scrc_mode = 'O3', 
                                        scrc_gubn = '',  
                                        scrc_io   = '',  
                                        scrc_lstk = '', 
                                        scrc_pltn = '', 
                                        scrc_jno  = '',   
                                        scrc_indx = '',  
                                        scrc_fstn = '',  
                                        scrc_tstn = '',
                                        scrc_xmov = '',   
                                        scrc_mesg = '출고대 UNLOAD 완료', 
                                        scrc_comm = '1', 
                                        scrc_rset = '0'
                                    where scrc_no = {0} and scrc_mode = 'O2' ";
                        rc = d.ExecuteCommand(sql, ls_hogi);
                        f_msg(li_hogi, ls_hogi + " tbscrc update");
                        if (rc > 0)
                        {
                            if (ls_pltn != "99999999")// 그냥출고 2014/08/01 
                            {
                                sql = @"insert into tbevnt
                                            ( evnt_gubn, evnt_jio,  evnt_hogi, evnt_fstn, evnt_tstn,
                                              evnt_pltn, evnt_lstk, evnt_xmov, evnt_sflg, evnt_wflg, evnt_uflg, evnt_wdate )
                                      values (  {0},      '$',        {1},        {2},      {3}, 
                                                {4},      {5},        {6},        'X',      'F',       '0',      {7} )";
                                rc = d.ExecuteCommand(sql, 
                                               ls_gubn,              ls_scno,    ls_fstn,   ls_tstn, 
                                               ls_pltn,   ls_loca,   ls_xmov,    ls_edat);
                                f_msg(li_hogi, ls_hogi + " tbevnt insert");

                                if (rc <= 0)
                                {
                                    d.Transaction.Rollback();
                                    f_msg(li_hogi, "insert into tbevnt at f_scc_o2_proc Error");
                                    return -1;
                                }
                            }

                            // 출고대 도착(CV쓰기 지시: sflg = X -> P)
                            sql = @"update tbindx set indx_edat = {0}, indx_sflg = 'P' where indx_jno = {1} and indx_pltn = {2} ";
                            f_msg(li_hogi, ls_hogi + " tbindx update");
                            rc = d.ExecuteCommand(sql, ls_edat, ls_jno, ls_pltn);
                            if (rc == 0)
                            {
                                d.Transaction.Rollback();
                                f_msg(li_hogi, "update tbindx set at f_scc_o2_proc Error");
                                return -1;
                            }
                            f_msg(li_hogi, ls_hogi + " tbindx commit");
                            d.Transaction.Commit();
                        }
                        else
                        {
                            d.Transaction.Rollback();
                            f_msg(li_hogi, "update tbscrc set at f_scc_o2_proc Error");
                            return -1;
                        } 
                    } // end of using trans
                }
                catch(Exception E) { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_o2_proc Error"); }
                finally
                {
                    d.Connection.Close();
                }                   
            }                      
            return 0;
        }

        private int f_scc_o3_proc(int ai_hogi)
        {
            debugstep = "27";
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            int srch_ok = 0;

            srch_ok = f_scc_inpt_srch(li_hogi);
            if (srch_ok != 1)
            {
                srch_ok = f_scc_oupt_srch(li_hogi);
            }
            return srch_ok;
        }

        private int f_scc_oupt_srch(int ai_hogi)
        {
            debugstep = "28";
            //**********************************************************************************************************//
            //* 출고 데이타 탐색 처리
            //**********************************************************************************************************//

            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            string ls_tkno = "";

            srch_jio = "$";
            srch_lstk = "";
            srch_pltn = "";
            srch_indx = "";
            srch_fstn = "";           // SC#1= ST02, SC#2= ST04, SC#3= ST06, SC#4= ST08, SC#5= ST10
            srch_tstn = "";           // ST43,ST45
            srch_xmov = "";

            if (cv_comm == "0") return -1;    // CVC 통신불능이면 SC 입출지령 처리않됨

            //* 출고대 READY 조건 체크 

            if (ls_hogi == "01")   // S/C#01 출고대
            {
                if (cv_op_onof[0] != '1' || cv_op_eror[0] != '0') return -1;
                if (cv_ost_redy[0] != '1' || cv_ost_palt[0] != '0') return -1;
                if (cv_job_no[1] != "0000") return -1;
                ls_tkno = "02";
            }
            else if (ls_hogi == "02")   // S/C#02 출고대
            {
                if (cv_op_onof[1] != '1' || cv_op_eror[1] != '0') return -1;
                if (cv_ost_redy[1] != '1' || cv_ost_palt[1] != '0') return -1;
                if (cv_job_no[3] != "0000") return -1;
                ls_tkno = "04";
            }
            else if (ls_hogi == "03")   // S/C#03 출고대
            {
                if (cv_op_onof[2] != '1' || cv_op_eror[2] != '0') return -1;
                if (cv_ost_redy[2] != '1' || cv_ost_palt[2] != '0') return -1;
                if (cv_job_no[5] != "0000") return -1;
                ls_tkno = "06";
            }
            else if (ls_hogi == "04")   // S/C#04 출고대
            {
                if (cv_op_onof[3] != '1' || cv_op_eror[3] != '0') return -1;
                if (cv_ost_redy[3] != '1' || cv_ost_palt[3] != '0') return -1;
                if (cv_job_no[7] != "0000") return -1;
                ls_tkno = "08";
            }
            else if (ls_hogi == "05")   // S/C#05 출고대
            {
                if (cv_op_onof[4] != '1' || cv_op_eror[4] != '0') return -1;
                if (cv_ost_redy[4] != '1' || cv_ost_palt[4] != '0') return -1;
                if (cv_job_no[9] != "0000") return -1;
                ls_tkno = "10";
            }
            else
            {
                return -2;
            }
          
            //* 출고대 READY 조건일때, 출고할 데이타 탐색 처리
            if (scc_ouse != "1") return -1;         //출고 금지 이면 

            string ls_jno = string.Empty;
            string sql =string.Empty;

            sql = @" select min(indx_jno) from tbindx where indx_fstn = {0} and indx_jio  = '$' and indx_sflg in ('W', 'X', 'P') ";
            try
            {
                using(DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    ls_jno = d.ExecuteQuery<string>(sql, ls_tkno).SingleOrDefault();
                }
            }
            catch (Exception E)
            {
                f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_oupt_srch Error=kkkk" + ls_tkno + " " + Environment.NewLine + sql);
                return -3;
            }

            if (ls_jno == null) return -4;
            if (ls_jno.Trim() == "") return -4;

            tbindx q;
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {  
                    q = d.tbindxes.Where(x => x.indx_jno == ls_jno && x.indx_jio == "$" && x.indx_fstn == ls_tkno).SingleOrDefault();
                }
            }
            catch (Exception E)
            { f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_oupt_srch Error2=" + ls_tkno + " " + ls_jno + Environment.NewLine + sql); return -3; }

            if (q == null) return -4;

            string ls_indx = q.indx_indx.ToString().Trim();
            string ls_pltn = q.indx_pltn.ToString().Trim();
            string ls_loca = q.indx_lstk.ToString().Trim();
            string ls_sflg = q.indx_sflg.ToString().Trim();
            string ls_fstn = q.indx_fstn.ToString().Trim();
            string ls_tstn = q.indx_tstn.ToString().Trim();
            string ls_xmov = q.indx_xmov.ToString().Trim();
            string ls_gubn = q.indx_gubn.ToString().Trim();


         
            if (ls_pltn == "") return -6;
            if (ls_loca == "" || ls_loca.Length != 7) return -7;
            if (ls_sflg != "W") return -7;
          
            string ls_lstk = ls_loca.Substring(1, 6);
            
            //************************************************************************************************// 
            // 자기 출고위치가 아니면 불가
            //************************************************************************************************// 
            string ou_bank = ls_lstk.Substring(0, 2);
            if (ls_hogi == "01")
            {
                if (!(ou_bank == "01" || ou_bank == "02")) return -1;
            }
            if (ls_hogi == "02")
            {
                if (!(ou_bank == "03" || ou_bank == "04")) return -1;
            }
            if (ls_hogi == "03")
            {
                if (!(ou_bank == "05" || ou_bank == "06")) return -1;
            }
            if (ls_hogi == "04")
            {
                if (!(ou_bank == "07" || ou_bank == "08")) return -1;
            }
            if (ls_hogi == "05")
            {
                if (!(ou_bank == "09" || ou_bank == "10")) return -1;
            }

            srch_pltn = ls_pltn.Trim();
            srch_lstk = ls_lstk.Trim();
            srch_jno = ls_jno.Trim();
            srch_indx = ls_indx.Trim();
            srch_fstn = ls_fstn.Trim();
            srch_tstn = ls_tstn.Trim();
            srch_xmov = ls_xmov.Trim();

            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {

                d.Connection.open();
                using (d.Transaction = d.Connection.BeginTransaction())
                {
                    try
                    {
                        sql = @"update  tbscrc 
                               set  scrc_mode = 'O0',   
                                    scrc_gubn  = {0}, 
                                    scrc_io    = '$',    
                                    scrc_eror  = '0',
                                    scrc_ecod  = '',     
                                    scrc_lstk  = {1}, 
                                    scrc_pltn  = {2},    
                                    scrc_jno   = {3}, 
                                    scrc_indx  = {4},   
                                    scrc_fstn  = {5}, 
                                    scrc_tstn  = {6}, 
                                    scrc_xmov  = {7},   
                                    scrc_mesg  = '출고대기', 
                                    scrc_comm  = '1',    
                                    scrc_rset  = '0'
                            where scrc_no    = {8}
                             and  scrc_onln  = '1'  
                             and  scrc_pwron = '1'  
                             and  scrc_emer  = '0'  
                             and  scrc_eror  = '0'
                             and  scrc_ouse  = '1'
                             and  scrc_stop  = '0' ";

                        int r = d.ExecuteCommand(sql, ls_gubn, srch_lstk, srch_pltn, srch_jno, srch_indx, srch_fstn, srch_tstn, srch_xmov, ls_hogi);                     
                        if (r == 0)
                        {
                            d.Transaction.Rollback();
                            f_msg(li_hogi, "update tbscrc f_scc_oupt_srch Error");
                            return -1;
                        }
                      

                        //* 해당 출고건 SC 작업중( 'W' -> 'X' )
                        string ls_edat = DateTime.Now.ToString("yyyyMMddHHmmss");
                        sql = @"update tbindx 
                                       set indx_edat = {0}, 
                                           indx_sflg = 'X' 
                                    where indx_jno = {1} 
                                      and indx_jio  = '$'  
                                      and indx_fstn = {2}  
                                      and indx_gubn = {3}  
                                      and indx_sflg = 'W' ";
                        r = d.ExecuteCommand(sql, ls_edat, ls_jno, ls_tkno, ls_gubn);
                        if (r == 0)
                        {
                            d.Transaction.Rollback();
                            f_msg(li_hogi, "update tbindx f_scc_oupt_srch Error");
                            return -1;
                        }

                        d.Transaction.Commit();
                        g_scc_comd = 2;
                    }
                    catch (Exception E)
                    {
                        d.Transaction.Rollback();
                        f_msg(li_hogi, E.Message + Environment.NewLine + "f_scc_oupt_srch " + sql);
                        return -1;
                    }
                    finally { d.Connection.Close(); }
                }     
            }

            return 1;
        }
        private int f_scc_proc(int ai_hogi)
        {
            debugstep = "29";
            //*********************************************************************************************//
            // SC 제어처리 = 상태정보를 분석하여, 완료처리를 하고, 대기시 입출지시를 탐색 한다.
            //*********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            g_scc_comd = 0;

            srch_jio = "";
            srch_lstk = "";
            srch_pltn = "";
            srch_indx = "";
            srch_fstn = "";
            srch_tstn = "";
            srch_xmov = "";

            //* 작업완료 CLEAR 체크         
            string ls_inst = recv_xbk1.Substring(2, 2);
            string ls_iloc = recv_xbk2.Substring(2, 2) + recv_xby2.Substring(2, 2) + recv_xlv2.Substring(2, 2);

            string ls_oloc = recv_xbk1.Substring(2, 2) + recv_xby1.Substring(2, 2) + recv_xlv1.Substring(2, 2);
            string ls_oust = recv_xbk2.Substring(2, 2);
            f_msg(li_hogi, "f_scc_proc 111");
            f_msg(li_hogi, "recv_stat1= " + recv_stat1);
            f_msg(li_hogi, "recv_pwron= " + recv_pwron);
            f_msg(li_hogi, "scc_rset= " + scc_rset);
            f_msg(li_hogi, "scc_mode= " + scc_mode);
            f_msg(li_hogi, "recv_scplt= " + recv_scplt);
            f_msg(li_hogi, "scc_rset= " + scc_rset);

            switch (recv_stat1.Trim())
            {
                case "0001": // SC 하무대기 상태
                    {
                        if (scc_stop != "0") return -1; // SC 사용 중지 요청 이면, SC지시탐색처리는 하지 않는다.
                        if (cv_comm == "0") return -1; // CVC 통신불능이면 SC 입출지령 처리않됨
                        if (f_scc_pwr_chek(li_hogi) == 0) return 0;  // 이미 파워지령이 있거나, 내릴경우
                        f_msg(li_hogi, "scc_rset= " + scc_rset);
                        if (recv_pwron.Trim() != "1") return -1;
                        if (scc_rset.Trim() != "0") return -1;

                        switch (scc_mode.Trim())
                        {
                            case "I0":
                                {
                                    if (recv_scplt == "0")
                                    {
                                        srch_pltn = scc_pltn.Trim();
                                        srch_lstk = scc_lstk.Trim();
                                        srch_jno = scc_jno.Trim();
                                        srch_indx = scc_indx.Trim();
                                        srch_fstn = scc_fstn.Trim();
                                        srch_tstn = scc_tstn.Trim();
                                        srch_xmov = scc_xmov.Trim();
                                        g_scc_comd = 1;
                                    }
                                    break;
                                }
                            case "O0":
                                {
                                    if (recv_scplt == "0")
                                    {
                                        srch_pltn = scc_pltn.Trim();
                                        srch_lstk = scc_lstk.Trim();
                                        srch_jno = scc_jno.Trim();
                                        srch_indx = scc_indx.Trim();
                                        srch_fstn = scc_fstn.Trim();
                                        srch_tstn = scc_tstn.Trim();
                                        srch_xmov = scc_xmov.Trim();
                                        g_scc_comd = 2;
                                    }
                                    break;
                                }
                            case "H0":
                                {
                                    srch_pltn = scc_pltn.Trim();
                                    srch_lstk = scc_lstk.Trim();
                                    srch_jno = scc_jno.Trim();
                                    srch_indx = scc_indx.Trim();
                                    srch_fstn = scc_fstn.Trim();
                                    srch_tstn = scc_tstn.Trim();
                                    srch_xmov = scc_xmov.Trim();
                                    g_scc_comd = 3;
                                    break;
                                }
                            case "I1":
                            case "O1":
                            case "I2":
                            case "O2": break;

                            case "I3": { f_scc_i3_proc(li_hogi); break; }
                            case "O3": { f_scc_o3_proc(li_hogi); break; }
                            default: { break; }
                        }
                        break;
                    }
                case "0002": { g_scc_comd = 0; break; }  // SC 하유대기 상태
                case "0008": { g_scc_comd = 0; break; }  // SC 에러상태
                case "0007":                             // SC 작업중 상태
                    {
                        if (recv_pwron != "1") return -1;
                        switch (scc_mode.Trim())
                        {
                            case "I1": { if (recv_scplt == "1") f_scc_i1_proc(li_hogi); break; }
                            case "O1": { if (recv_scplt == "1") f_scc_o1_proc(li_hogi); break; }
                            case "H1": { f_scc_h1_proc(li_hogi); break; } // H1 -> H3
                            default: { break; }
                        }
                        break;
                    }
                case "0009":  // SC 작업완료 상태
                    {
                        f_msg(li_hogi, " SC 작업완료 상태 "  );
                        if (recv_pwron != "1") return -1;
                        switch (scc_mode.Trim())
                        {
                            case "I2":
                                {
                                    if (recv_scplt.Trim() == "0" )
                                    {
                                        if (scc_tstn.Trim() == ls_inst.Trim() && recv_xby1.Trim() == "0001" && recv_xlv1.Trim() == "0001" )                                            
                                        {
                                            if (scc_lstk.Trim() == ls_iloc.Trim())
                                            {
                                                if (f_scc_i2_proc(li_hogi) == 0)
                                                    g_scc_comd = 11;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "O2":
                                {
                                    f_msg(li_hogi, recv_scplt + "-" + scc_fstn + "-" + ls_oust + "-" + recv_xby2 + "-" + recv_xlv2 + "-");
                                    f_msg(li_hogi, scc_lstk + "-" + ls_oloc);

                                    if (recv_scplt.Trim() == "0" )                                        
                                    {
                                        if (scc_fstn.Trim() == ls_oust.Trim() && recv_xby2.Trim() == "0001" && recv_xlv2.Trim() == "0001")
                                        {
                                            if (scc_lstk.Trim() == ls_oloc.Trim())
                                            {
                                                if (f_scc_o2_proc(li_hogi) == 0)
                                                    g_scc_comd = 11;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "I3":
                            case "O3":
                                {
                                    if (recv_scplt.Trim() == "0")
                                    {
                                        if (recv_xbk1.Trim() != "0000" || recv_xby1.Trim() != "0000" || recv_xlv1.Trim() != "0000")
                                        {
                                            g_scc_comd = 11;
                                        }

                                        if (recv_xbk2.Trim() != "0000" || recv_xby2.Trim() != "0000" || recv_xlv2.Trim() != "0000")
                                        {
                                            g_scc_comd = 11;
                                        }
                                    }
                                    break;
                                }
                            case "I1":
                            case "O1":
                                {
                                    g_scc_comd = 0; //return 0;
                                    break;
                                }
                            case "H0":
                            case "H1":  // H0, H1 -> I3
                                {
                                    f_msg(li_hogi, "f_scc_h1_proc 222");
                                    f_scc_h1_proc(li_hogi);
                                    g_scc_comd = 11;
                                    break;
                                }
                            default:
                                {
                                    if (recv_xbk1.Trim() != "0000" || recv_xby1.Trim() != "0000" || recv_xlv1.Trim() != "0000") g_scc_comd = 11;
                                    if (recv_xbk2.Trim() != "0000" || recv_xby2.Trim() != "0000" || recv_xlv2.Trim() != "0000") g_scc_comd = 11;
                                    break;
                                }
                        }
                        break;
                    }
                default: { break; }
            }

            return 0;
        }

        private int f_scc_pwr_chek(int ai_hogi)
        {
            debugstep = "30";
            //*********************************************************************************************//
            // SCC 호시별 Power Check
            //*********************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            if (sc_pwr_wait[li_hogi] != 0) return 0;  // POWER ON-OFF지령이 이미 있을 경우
            if (cv_comm == "0") return -1;            // CVC 통신불능이면 SC 입출지령 처리않됨

            // 만일 CV를 수동전환시 SC-Control POWER ON => OFF
            if (cv_op_onof[li_hogi] == '0' && recv_stat1 == "0001" && recv_pwron == "1")
            {
                g_scc_comd = 8;
                return 0;
            }

            // 만일 CV를 자동전환시 SC-Control POWER OFF => ON
            if (cv_op_onof[li_hogi] == '1' && recv_stat1 == "0001" && recv_pwron == "0")
            {
                g_scc_comd = 9;
                return 0;
            }
            return 1;
        }

        private int f_scc_rest_chek(int ai_hogi)
        {
            debugstep = "31";
            int ret = 0;

            //*****************************************************************************************************//
            //* SCC 기타 지령 처리
            //*****************************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");
            string ls_by = scc_posi.Substring(0, 2);

            if (ls_by.Trim() != "01") return -1;
            if (sc_pwr_wait[li_hogi] != 0) return -1;       // POWER ON-OFF지령이 이미 있을 경우

            string ls_stat = recv_stat2.Substring(3, 1);
            string ls_bits = "";
            string ls_bit1 = "";
            string ls_bit2 = "";
            if (f_char_to_bits(ls_stat, ref ls_bit1, ref ls_bit2) == 0)
                ls_bits = ls_bit2.Trim();

            switch (scc_rset)
            {
                case "1":        //이중입고리셋
                    {
                        f_scc_ee_proc(li_hogi);                             // 화면에서 지시된 rset-flag 바로 clear  
                        if (!(recv_stat1 == "0008" && recv_ercd == "0091")) return -1;
                        g_scc_comd = 5;
                        break;
                    }
                case "2":       //공출고 리셋
                    {
                        f_scc_ee_proc(li_hogi);                           // 화면에서 지시된 rset-flag 바로 clear  
                        if (!(recv_stat1 == "0008" && recv_ercd == "0092")) return -1;
                        g_scc_comd = 6;
                        break;
                    }
                case "3":       //Data이상 리셋
                    {
                        f_scc_ee_proc(li_hogi);                           // 화면에서 지시된 rset-flag 바로 clear  
                        if (!(recv_stat1 == "0008" && recv_ercd == "0093")) return -1;
                        g_scc_comd = 7;
                        break;
                    }
                default: { ret = -1; break; }
            }
            return ret;
            //*******************************************************************************************//
            //     [D13=SC상태정보]
            //*******************************************************************************************//
            // 0 :  
            // 1 : crane power on
            // 2 : emergency stop
            // 3 :  
            //-------------------------------------------------------------------------------------------//
            // 4 : 1=PLT유,0=PLT무  
            // 5 : 
            // 6 : 
            // 7 : 1=원격, 0=원격외 모드  
            //-------------------------------------------------------------------------------------------//
            // 8 : 1=승강하위치  
            // 9 : 1=포크센타
            // A : 1=포크좌측끝
            // B : 1=포크우측끝
            //-------------------------------------------------------------------------------------------//
            // C : 1=주행원점  
            // D : 1=주행정위치
            // E : 1=승강원점
            // F : 1=승강상위치
            //-------------------------------------------------------------------------------------------//

        }
  
        private void f_scpwr_cvwrit()
        {
            debugstep = "32";
            //------------------------------------------------------------//
            // STACKER-CRANE control power info
            //------------------------------------------------------------//
            int rc = 0;

            string ls_sc_pwr = new string(prev_sc_pwr).Substring(0,8);
            string ls_sc_eror = new string(prev_sc_eror).Substring(0,8);
            f_msg(9, "sc_pwr=" + ls_sc_pwr + "  sc_eror=" + ls_sc_eror + " SC 파워및 에러정보를 CNVC에 전송");
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    rc = d.ExecuteCommand(@"update tbcnvc set cnvc_op_onof = {0}, cnvc_op_eror = {1} where cnvc_mode = '02' ", ls_sc_pwr, ls_sc_eror);
                    if (rc == 0) f_msg(9, "update eror: f_scpwr_cvwrit");
                }
            }
            catch (Exception E)
            { f_msg(9, "f_scpwr_cvwrit " + E.Message); }
        }
 
        private int f_writ_proc(int ai_hogi, string as_obuf)
        {
            debugstep = "33";
            //****************************************************************************************************//
            // SRC에 지령-데이타 송신처리
            //****************************************************************************************************//
            // ENQ+01+FF+WWA+D0000+07+WRIT-DATA+BCC1,2+CRLF  -->
            //                                               <-- ACK+01+FF+CRLF or NAK+01+FF+CRLF
            //****************************************************************************************************//
            int li_hogi = ai_hogi;
            string ls_hogi = (li_hogi + 1).ToString("00");

            if (!sPortOpened[li_hogi])
            {
                disp_msg = "Port Not Opend!!";
                f_msg(li_hogi, disp_msg);

                return -2;
            }            

            //*******************************************
            // STEP1 = sending writ_data to src#1-plc
            //*******************************************
            string writ_data = as_obuf.Trim();
            string ls_wdata = ls_hogi + "FFWWAD000007" + writ_data;
            string ls_bcc = "";

            if (f_gets_bcc(ls_wdata, ref ls_bcc) != 0)
            {
                disp_msg = "Writ_Comd Bcc Eror...!!";
                f_msg(li_hogi, disp_msg);
                return -1;
            }

            oupt_buff = ENQ + ls_hogi + "FFWWAD000007" + writ_data + ls_bcc + XCR + XLF;

            byte[] b = Encoding.ASCII.GetBytes(oupt_buff);
            sPort[li_hogi].Write(b, 0, b.Length);

            //sPort[li_hogi].Write(oupt_buff);

            disp_msg = "W_DATA=[" + oupt_buff + "]";
            f_msg(li_hogi, disp_msg);
        
            Thread.Sleep(200);

            //*******************************************
            // STEP2 = receive writ_ack from src#1-plc
            //*******************************************
            
            string inpt_buff = "";
            int ll_ack = 0;
           
            inpt_buff = sPort[li_hogi].ReadExisting();
            disp_msg = "W_ACK =[" + inpt_buff + "]";
            f_msg(li_hogi, disp_msg);

            int rc = inpt_buff.Length;
            if (rc > 0)
            {
                ll_ack = inpt_buff.IndexOf(ACK, 0);
                if (ll_ack < 0)
                {
                    disp_msg = "NO ACK";
                    f_msg(li_hogi, disp_msg);
                    return -5;
                }
                else
                {
                    disp_msg = "OK ACK";
                    f_msg(li_hogi, disp_msg);
                }
            }
            else
            {
                return -6;
            }

            inpt_buff = "";  // 성공시 입력버퍼 초기화

            return 0;
        }
        #endregion     
           
    }

}

