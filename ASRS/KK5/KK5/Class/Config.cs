using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;

namespace KK5
{
    public static class Config
    {
        public static string Ko_KR = "ks_c_5601-1987";

        private static DataSet dsConfig = new DataSet();

        public static void LoadXML(string filePath)
        {
            try
            {
                dsConfig.ReadXml(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveXML(string filePath)
        {
            try
            {
                dsConfig.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string DBCONNECTION
        {
            get { return dsConfig.Tables[0].Rows[0]["DBCONNECTION"].ToString(); }
            set { dsConfig.Tables[0].Rows[0]["DBCONNECTION"] = value; }
        }
    
        public static string MainDBConnectionString
        {
            get { return dsConfig.Tables[0].Rows[0]["MAINDBCONNECTIONSTRING"].ToString(); }
            set { dsConfig.Tables[0].Rows[0]["MAINDBCONNECTIONSTRING"] = value; }
        }
        public static string BackupDBConnectionString  {
            get { return dsConfig.Tables[0].Rows[0]["BACKUPDBCONNECTIONSTRING"].ToString(); }
            set { dsConfig.Tables[0].Rows[0]["BACKUPDBCONNECTIONSTRING"] = value; }
        }
        public static string UserLevel = "1"; // 1:power 2:상차  3:조회

        public static string DBCon = @"Data Source=CSON01;Initial Catalog=IPK_RCP;Persist Security Info=True;User ID=AW_SA;Password=moNey=918+;TrustServerCertificate=True";
        public static string ConnectionString
        {
            get
            {
                if (DBCONNECTION == "MAIN")
                {
                    return MainDBConnectionString;
                }
                else
                {
                    return BackupDBConnectionString;
                }
            }
        }
         
        //static Config()
        //{
        //    LoadXML(Common.ProjectDir + "//" + "XMLConfig.xml");
        //}
    }

}
