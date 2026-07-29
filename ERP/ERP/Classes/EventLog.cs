using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ERP
{
    class EventLog
    {
        public static void Event_Log(string sFileName, string sEvent)
        {
            string sPath = System.IO.Directory.GetCurrentDirectory().ToString();
            string sFile = sPath + "\\" + DateTime.Now.Day.ToString() + "\\" + sFileName;

            FileInfo File_Info = new FileInfo(sFile);
            FileStream LogFile;

            try
            {
                if (!File_Info.Directory.Exists)       //폴더 체크
                {
                    File_Info.Directory.Create();
                }

                LogFile = File_Info.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                if (LogFile.CanWrite)
                {
                    string sTemp;
                    sTemp = "[" + DateTime.Now.ToString() + "] " + sEvent + "\r\n";
                    byte[] Buff = Encoding.Default.GetBytes(sTemp);
                    LogFile.Write(Buff, 0, Buff.Length);
                }
                LogFile.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static void Event_Log(string sFileName, string sTitle, string sEvent_Arg, Boolean bSave)
        {
            string sPath = System.IO.Directory.GetCurrentDirectory().ToString();
            string drive = sPath.Substring(0, 1);

            //sPath = drive + ":\\ERPIF";
            sPath = Config.ExceptionDir;
            string sFile = sPath + "\\EventLog\\" + DateTime.Now.Day.ToString() + "\\" + sFileName;
            string sNextDay = sPath + "\\EventLog\\" + DateTime.Now.AddDays(1).Day.ToString() + "\\" + sFileName;

            FileInfo File_Info = new FileInfo(sFile);
            FileInfo Next_File_Info = new FileInfo(sNextDay);
            FileStream LogFile;

            if (!bSave) return;

            try
            {
                if (Next_File_Info.Directory.Exists)       //한달 이전데이타 삭제
                {
                    Next_File_Info.Directory.Delete(true);
                }
                if (!File_Info.Directory.Exists)       //폴더 체크
                {
                    File_Info.Directory.Create();
                }
                LogFile = File_Info.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                if (LogFile.CanWrite)
                {
                    string sTemp;
                    sTemp = "[" + DateTime.Now.ToString() + "] [" + sTitle + "]" + sEvent_Arg + "\r\n";
                    byte[] Buff = Encoding.Default.GetBytes(sTemp);
                    LogFile.Write(Buff, 0, Buff.Length);
                }
                LogFile.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
