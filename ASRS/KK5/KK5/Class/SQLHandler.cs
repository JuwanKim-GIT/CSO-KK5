using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Linq;
using System.Data.SqlTypes;


namespace KK5
{
    public class SQLHandler
    {

        public SqlConnection conn = null;
        public SqlCommand cmd = null;
        public SqlTransaction tr = null;

        private int connetTimeOut = 5000;
        private int cmdTimeOut = 30000;

        public int ConnetTimeOut
        {
            get { return connetTimeOut; }
            set { connetTimeOut = value; }
        }
        public int CmdTimeOut
        {
            get { return cmdTimeOut; }
            set { cmdTimeOut = value; }
        }
        public SQLHandler()
        {
         //   conn = new SqlConnection(Properties.Settings.Default.KK5DConnection);
            cmd = new SqlCommand();
        }
        public static SQLHandler GetSQLHander()
        {
            SQLHandler SqlHandler = new SQLHandler();

           // SqlHandler.conn = new SqlConnection(Properties.Settings.Default.KK5DConnection);
            SqlHandler.cmd = new SqlCommand();
            try
            {
                SqlHandler.conn.Open();
            }catch(Exception E) { }

            return SqlHandler;
        }

        public bool Connect()
        {
            try
            {
                switch (this.conn.State)
                {
                    case ConnectionState.Open:
                        break;
                    case ConnectionState.Closed:
                        this.conn.Open();
                        break;
                    case ConnectionState.Connecting:
                        this.conn.Close();
                        this.conn.Open();
                        break;
                    default:
                        this.conn.Close();
                        this.conn.Open();
                        break;
                }
                return true;

                if (conn.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
               // uCommon.LogMsg(Stor.LOG_SYSTEM, "DB접속에러\r\n" + E.Message);
                return false;
            }

        }
        public void DisConnect()
        {
            if (conn == null) return;
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public void Close()
        {
            if (conn == null) return;
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public bool qrySql(string sql, DataTable dt)
        {
            if (!Connect()) return false;

            cmd.CommandText = sql;
            cmd.Connection = conn;
            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) dt.Load(rdr);
                rdr.Close();
            }
            catch (Exception E)
            {
                //uCommon.LogMsg(Stor.LOG_SYSTEM, E.Message);
                return false;
            }

            return true;
        }
        public bool qrySql(string sql, DataTable dt, out string ErrMsg)
        {
            ErrMsg = string.Empty;

            if (!Connect()) return false;
            cmd.CommandText = sql;
            cmd.Connection = conn;
            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) dt.Load(rdr);
                rdr.Close();
            }
            catch (Exception E)
            {
                ErrMsg = E.Message;
                //uCommon.LogMsg(Stor.LOG_SYSTEM, E.Message);
                return false;
            }

            return true;
        }

        public bool exeSql(string sql, bool transact = false)
        {
            if (!Connect()) return false;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            if (transact)
            {
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
            }
            try
            {
                cmd.ExecuteNonQuery();
                if (transact) tr.Commit();
            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
                if (transact) tr.Rollback();
                //uCommon.LogMsg(Stor.LOG_SYSTEM, E.Message);
                return false;
            }

            return true;
        }
        public bool exeSql(string sql, bool transact, out string eMsg)
        {
            eMsg = "";

            if (!Connect()) return false;
            cmd.CommandText = sql;
            cmd.Connection = conn;

            if (transact)
            {
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
            }
            try
            {
                cmd.ExecuteNonQuery();
                if (transact) tr.Commit();
            }
            catch (Exception E)
            {
                eMsg = E.Message;
                if (transact) tr.Rollback();
                //uCommon.LogMsg(Stor.LOG_SYSTEM, E.Message);
                return false;
            }

            return true;
        }
    }
}
