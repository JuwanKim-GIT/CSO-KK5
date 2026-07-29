using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Transactions;
using System.ComponentModel;

namespace RCP
{
    class UpdateProc
    {
        DBDataContext db;
        public BackgroundWorker bw = new BackgroundWorker();
     
        public UpdateProc()
        {
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += Bw_DoWork;
            

        }
        public void Run_upateproc()
        {
            if (bw.IsBusy) return;
            bw.RunWorkerAsync();
        }
        public void stop_upateproc()
        {
            bw.CancelAsync();                
        }
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            db = new DBDataContext(Config.DBCon);
            db.CommandTimeout = 10000; //10초
            db.ExecuteCommand(@"delete from tbevnt where evnt_uflg = 'S'" );
            
            bw.ReportProgress(1, "Update Process now started.");
            while (!bw.CancellationPending)
            {              
                try
                {                   
                    mainproc();
                    bw.ReportProgress(2);
                }
                catch (Exception E)
                {
                    bw.ReportProgress(3, E.Message);
                }
                Thread.Sleep(1000);
            }
            bw.ReportProgress(1, "Update Process stopped.");
        }

        private void mainproc()
        {
          
            var q = db.ExecuteQuery<utbevnt>(@"Select Top 1 * from tbevnt where evnt_uflg = '0' order by evnt_key").SingleOrDefault();
            if (q == null) return;
           
            if (q.evnt_gubn == "R") // RCP 수동처리임
            {
                db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                bw.ReportProgress(1, "RCP 수동처리건=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                return;
            }
         
            int rc = 0;
            int wgb = 0;
            
            #region ////////// start of 입고취소 루틴 ///////////////////
            if (q.evnt_wflg == "C" && q.evnt_xmov == "I")
            {
                if (q.evnt_fstn == "21" || q.evnt_fstn == "24") wgb = 1;
                else if (q.evnt_fstn == "22") wgb = 2;
                           
                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_inpt_cancel(wgb, q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit(); else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[입고취소 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0 )
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "입고취소성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "입고취소실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);

                }
                return;
            }
            #endregion
          
            #region ////////// start of 입고완료 루틴 ///////////////////
            if ((q.evnt_wflg == "F" || q.evnt_wflg == "S") && q.evnt_xmov == "I")
            {
                if (q.evnt_fstn == "21" || q.evnt_fstn == "24") wgb = 1;
                else if (q.evnt_fstn == "22") wgb = 2;

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_inpt_finish(wgb, q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_inpt_finish(wgb, q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[입고완료 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "입고완료성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {                   
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "입고완료실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 출고완료 루틴 ///////////////////
            if ((q.evnt_wflg == "F" || q.evnt_wflg == "S") && q.evnt_xmov == "$")
            {
               
                   //using (TransactionScope scope = new TransactionScope())
                   //{
                   //    rc = db.u_oupt_finish(q.evnt_pltn, q.evnt_lstk);
                   //    if (rc > 0) scope.Complete();
                   //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_oupt_finish(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[출고완료 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "출고완료성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {                   
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "출고완료실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 이동완료 루틴 ///////////////////
            if ((q.evnt_wflg == "F" || q.evnt_wflg == "S") && q.evnt_xmov == "M")
            {
                
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_move_finish(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_move_finish(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[이동완료 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이동완료성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {                   
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이동완료실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 출고취소 루틴 ///////////////////
            if (q.evnt_wflg == "C" && q.evnt_xmov == "$")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_oupt_cancel(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_oupt_cancel(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[출고취소 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "출고취소성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "출고취소실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 이출취소 루틴 ///////////////////
            if (q.evnt_wflg == "C" && q.evnt_xmov == "M")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_move_cancel(q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_move_cancel(q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[이출취소 루틴]"); }
                }
                db.Connection.Close();

                if (rc == 1)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이출취소성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이출취소실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 야적이동취소 루틴 ///////////////////
            if (q.evnt_wflg == "C" && q.evnt_xmov == "N")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_ymove_cancel(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_ymove_cancel(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[야적이동취소 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "야적이동취소성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {                  
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "야적이동취소실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 야적이동완료 루틴 ///////////////////
            if (q.evnt_wflg == "F" && q.evnt_xmov == "N")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_ymove_finish(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}
                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_ymove_finish(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[야적이동완료 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "야적이동완료성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk + " ret=" + rc.ToString());
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "야적이동완료실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk + " ret=" + rc.ToString());
                }
                return;
            }
            #endregion

            #region ////////// start of 공출고 루틴 ///////////////////
            if (q.evnt_wflg == "E" && q.evnt_xmov == "$")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_oupt_finish(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}

                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_oupt_finish(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[공출고 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "공출고처리성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "공출고처리실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion

            #region ////////// start of 이출공출고 루틴 ///////////////////
            if (q.evnt_wflg == "E" && q.evnt_xmov == "M")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_move_finish(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}
                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_move_finish(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[이출공출고 루틴]"); }
                }
                db.Connection.Close();

                if (rc > 0)
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이출공출고성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk + " ret=" + rc.ToString());
                }
                else
                {                  
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이출공출고실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk + " ret=" + rc.ToString());
                }
                return;
            }
            #endregion

            #region ////////// start of 이중입고 루틴 ///////////////////
            if (q.evnt_wflg == "D" && q.evnt_xmov == "I")
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    rc = db.u_inpt_double(q.evnt_pltn, q.evnt_lstk);
                //    if (rc > 0) scope.Complete();
                //}
                db.Connection.open();                
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        rc = db.u_inpt_double(q.evnt_pltn, q.evnt_lstk);
                        if (rc > 0) db.Transaction.Commit();
                        else db.Transaction.Rollback();
                    }
                    catch(Exception E) { db.Transaction.Rollback(); bw.ReportProgress(1, E.Message + "[이중입고 루틴]");  }
                }
                db.Connection.Close();

                if (rc > 0)
                {                    
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'S' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이중입고처리성공=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                else
                {
                    db.ExecuteCommand(@"update tbevnt set evnt_uflg = 'F' where evnt_key = {0}", q.evnt_key);
                    bw.ReportProgress(1, "이중입고처리실패=> 파렛번호 :" + q.evnt_pltn + " 보관위치: " + q.evnt_lstk);
                }
                return;
            }
            #endregion         
           
        }

    }
    public class utbevnt  // for query
    {
        public decimal evnt_key { get; set; }
        public string evnt_gubn { get; set; }
        public string evnt_jio { get; set; }
        public string evnt_hogi { get; set; }
        public string evnt_fstn { get; set; }
        public string evnt_tstn { get; set; }
        public string evnt_pltn { get; set; }
        public string evnt_lstk { get; set; }
        public string evnt_xmov { get; set; }
        public string evnt_sflg { get; set; }
        public string evnt_wflg { get; set; }
        public string evnt_uflg { get; set; }
        public string evnt_wdate { get; set; }  // 14

    }
}