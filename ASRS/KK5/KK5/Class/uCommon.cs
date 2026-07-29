using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Drawing;

namespace KK5
{
    class uCommon
    {
        public const char C_SOH = (char)1;
        public const char C_STX = (char)2;
        public const char C_ETX = (char)3;
        public const char C_DEL = (char)4;
        public const char C_DLE = (char)16;
        public const char C_LF  = (char)10;
        public const char C_FF  = (char)13;
        public const char C_CR  = (char)14;
        public const char C_US  = (char)31;
        public const char C_CAN = (char)24;

        public static string PLC_IP = "140.80.0.5";
        public static int PLC_FETCH_PORT = 1024;
        public static int PLC_WRITE_PORT = 1025;
        public static ushort PLC_CONNECT_TIMEOUT = 20000;
              
        public static volatile string LogDir = @"c:\logAsrs";

        public static TIniFile IniFile = new TIniFile();

        public static FrmMain frmMain = null;
        
        //public static void LogMsg(int iLogType, string sMsg)
        //{
        //    if (frmMain == null) return;

        //    string sDevExt = string.Empty;
        //    ListBox ctlList = null; ;

        //    switch (iLogType)
        //    {
        //        case Stor.LOG_SYSTEM:
        //            ctlList = frmMain.lstLogSystem;
        //            sDevExt = "SYS";
        //            break;
        //        case Stor.LOG_INPUT:
        //            ctlList = frmMain.lstLogInput;
        //            sDevExt = "INP";
        //            break;
        //        case Stor.LOG_OUTPUT_LEFT:
        //            ctlList = frmMain.lstLogOutputLeft;
        //            sDevExt = "OUT";
        //            break;
        //        case Stor.LOG_OUTPUT_RIGHT:
        //            ctlList = frmMain.lstLogOutputRight;
        //            sDevExt = "OUT";
        //            break;
        //        case Stor.LOG_ALARM:
        //            ctlList = frmMain.lstLogOutputRight;
        //            sDevExt = "ERR";
        //            break;
        //        default:
        //            break;
        //    }

        //    try
        //    {
        //        ctlList.Invoke(new Action(() =>
        //        {
        //            try
        //            {
        //                string sTime = string.Format("{0:HH:mm:ss}", DateTime.Now);

        //                // log file 저장 skip 쓰잘때없이-----------------------------------------------------
        //                //string sFileName = GetLogFileName();
        //                //sFileName = sFileName + "." + sDevExt;

        //                //if (!Directory.Exists(Path.GetDirectoryName(sFileName)))
        //                //    Directory.CreateDirectory(Path.GetDirectoryName(sFileName));

        //                //File.AppendAllText(sFileName, "<" + sTime + ">" + sMsg + "\r\n");
        //                //------------------------------------------------------------------------

        //                if (iLogType == Stor.LOG_ALARM) return;

        //                ctlList.SuspendLayout();

        //                if (ctlList.Items.Count > 500) ctlList.Items.RemoveAt(0);

        //                ctlList.Items.Add("[" + sTime + "] " + " ⇒ " + sMsg);
                        
        //                ctlList.SetSelected(ctlList.Items.Count - 1, true);

        //                ctlList.ResumeLayout();
        //            }
        //            catch (Exception E)
        //            {}
        //        }
        //       ));

        //    }
        //    catch(Exception E)
        //    {}
            
        //} 

        public static void LogView(string sLogExt, ListBox ctlListBox)
        {
            string sFileName = GetLogFileName();
            sFileName = sFileName + "." + sLogExt;

            lock (ctlListBox)
            {
                ctlListBox.BeginUpdate();

                if (File.Exists(sFileName))
                {
                    string[] buf = File.ReadAllLines(sFileName);

                    for (int i = 0; i < buf.Length; i++)
                        ctlListBox.Items.Add(buf[i]);
                }
                else
                    ctlListBox.Items.Clear();

                ctlListBox.EndUpdate();
            }


        }
        public static string GetLogFileName() // not Check
        {

            string sDate  = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string sYear  = sDate.Substring(0, 4);
            string sMonth = sDate.Substring(5, 2);
            string sDay   = sDate.Substring(8, 2);


//          string sDir = Path.GetDirectoryName(Application.ExecutablePath);

            string sDir = LogDir + "\\Log\\" + sMonth + "\\" + sDay ;

            if (Directory.Exists(sDir)) Directory.CreateDirectory(sDir);    
                 
            return sDir + "\\" + sDate;
        }
        
        public static void KeyNumericCheck(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
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
                case '.':
                case (char)13:
                case (char)8:
                    e.Handled = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        public static bool IsNumeric( string sNumData)
        {
            char c;
            bool ok = false;

            for (int i= 1; i < sNumData.Length; i++)
            {
                c = sNumData[i];
                switch (c)
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
                    case '.':
                    case (char)8:
                        ok = true;
                        break;
                    default:
                        break;
                }
            }
            return ok;
        }

        public static bool IsCharNumeric(char cData)
        {
            bool ok = false;

            switch (cData)
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
                    ok = true;
                    break;
                default:
                    break;
            }
            return ok;
        }

        public static string Word2HexStr(ushort wValue)
        {
            //return string.Format("{0:X4}", wValue).Replace("-", ""); 
            return wValue.ToString("X4").Replace("-", "");
        }

        public static int StrHextoInt( string sVal)
        {
            //return Convert.ToInt32(sVal, 16);
            return int.Parse(sVal, System.Globalization.NumberStyles.HexNumber);
        }
        public static ushort StrHextoWord(string sHexStr)
        {
            return ushort.Parse(sHexStr, System.Globalization.NumberStyles.HexNumber);
        }
        public static int HexVal(string sData)
        {
            int ret = 0;

            sData.ToUpper();
            switch (sData)
            {
               
                case "0":
                    ret = 0;
                    break;
                case "1":
                    ret = 1;
                    break;
                case "2":
                    ret = 2;
                    break;
                case "3":
                    ret = 3;
                    break;
                case "4":
                    ret = 4;
                    break;
                case "5":
                    ret = 5;
                    break;
                case "6":
                    ret = 6;
                    break;
                case "7":
                    ret = 7;
                    break;
                case "8":
                    ret = 8;
                    break;
                case "9":
                    ret = 9;
                    break;
                case "A":
                    ret = 10;
                    break;
                case "B":
                    ret = 11;
                    break;
                case "C":
                    ret = 12;
                    break;
                case "D":
                    ret = 13;
                    break;
                case "E":
                    ret = 14;
                    break;
                case "F":
                    ret = 15;
                    break;

            }
            return ret;
        }

        public static ushort Rev2Words(ushort iValue)
        {
            int i = iValue;
            return (ushort)(i >> 24 | i << 24);
        }

        public static string Sec2HHMMSS(int iCalSec)
        {
            TimeSpan t = TimeSpan.FromSeconds(iCalSec);

            return  string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

        }

        public static string Str2DateTime(string sDateTime)
        {
            if (sDateTime.Trim().Length != 14) return "";
            else
                return sDateTime.Substring(0, 4) + "-" + sDateTime.Substring(4, 2) + "-" + sDateTime.Substring(6, 2) + " " +
                        sDateTime.Substring(8, 2) + ":" + sDateTime.Substring(10, 2) + ":" + sDateTime.Substring(12, 2);
        }

        //public static uMsg frmMsg = new uMsg();
        //public static void CustomMessage(string sMsg, bool bBtnYes, bool bBtnNo, bool bBtnOk, Color objColor)
        //{
        //    //if (frmMsg != null) return;
            
        //    Stor.TPrgm.objMsgColor = objColor;
        //    uMsg frmMsg = new uMsg();
        //    frmMsg.pnlMsg.Text = sMsg;
        //    frmMsg.btnYes.Visible = bBtnYes;
        //    frmMsg.btnNo.Visible = bBtnNo;
        //    frmMsg.btnOK.Visible = bBtnOk;

        //    frmMsg.ShowDialog();
        //    frmMsg.Dispose();
                    
        //}
    }



    class TIniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public TIniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }

}
