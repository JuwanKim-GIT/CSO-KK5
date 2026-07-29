using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.IO;

namespace ERP
{
    class Config
    {
        #region [ 전역 변수 ]

        public static string RecvDir = @"C:\SFTP\Incoming\";
        public static string BackupDir = @"C:\SFTP\Backup\";
        public static string FailDir = @"C:\SFTP\Backup\Failure\";
        public static string ExceptionDir = @"C:\SFTP\Backup\Exception\";

        #endregion

        static Config()
        {
            if (!Directory.Exists(RecvDir))
            {
                Directory.CreateDirectory(RecvDir);
            }
            if (!Directory.Exists(BackupDir))
            {
                Directory.CreateDirectory(BackupDir);
            }
            if (!Directory.Exists(FailDir))
            {
                Directory.CreateDirectory(FailDir);
            }
            if (!Directory.Exists(ExceptionDir))
            {
                Directory.CreateDirectory(ExceptionDir);
            }
        }
        public static string DBCon = @"Data Source=CSON01;Initial Catalog=IPK_RCP;Persist Security Info=True;User ID=AW_SA;Password=moNey=918+;TrustServerCertificate=True";

        #region [ Mac Address 얻기 ]
        public string GetMacAddress()
        {
            string qry = "select * FROM Win32_NetworkAdapter";

            ObjectQuery objectQuery = new ObjectQuery(qry);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);

            foreach (ManagementObject nicObj in searcher.Get())
            {
                return nicObj["MACAddress"].ToString();
            }

            return "";
        }

        #endregion
    }


}
