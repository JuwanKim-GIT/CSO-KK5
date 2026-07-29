using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;
using System.Transactions;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace ERP
{
    public partial class FMain : Form
    {

        System.Timers.Timer TT = new System.Timers.Timer(5000);

        #region declare variables ----------------

        bool CloseFlag = false;
        public bool auto = false;

        #endregion

        public FMain()
        {
            InitializeComponent();
            this.FormClosing += FMain_FormClosing;
            this.notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            this.exitToolStripMenuItem1.Click += ExitToolStripMenuItem_Click;

            try
            {
                bool isLocalMode = false;

                // XML 파일 경로 확인
                string xmlFilePath = "c:\\asrs\\DBCon_chilseo.xml";
                if (!System.IO.File.Exists(xmlFilePath) && System.IO.File.Exists("c:\\AWS\\CSO\\DBCon_chilseo.xml"))
                {
                    xmlFilePath = "c:\\AWS\\CSO\\DBCon_chilseo.xml";
                }

                if (!System.IO.File.Exists(xmlFilePath))
                {
                    MessageBox.Show("DB 설정 파일이 존재하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                XDocument xd = XDocument.Load(xmlFilePath);
                var q = (from d in xd.Root.Descendants("DB") select d.Element("Con")).SingleOrDefault();
                var localQ = (from d in xd.Root.Descendants("DB") select d.Element("LocalCon")).SingleOrDefault();

                if (q != null)
                {
                    string primaryConnStr = q.Value.ToString();
                    string localConnStr = (localQ != null) ? localQ.Value.ToString() : "";

                    // 1차 시도: 서버 DB 접속
                    try
                    {
                        using (System.Data.SqlClient.SqlConnection testConn = new System.Data.SqlClient.SqlConnection(primaryConnStr))
                        {
                            testConn.Open();
                        }
                        Config.DBCon = primaryConnStr;
                    }
                    catch (System.Data.SqlClient.SqlException sqlEx)
                    {
                        // 2차 시도: 서버 접속 실패 시 로컬 DB 전환
                        if (!string.IsNullOrEmpty(localConnStr))
                        {
                            Config.DBCon = localConnStr;
                            isLocalMode = true;

                            MessageBox.Show(
                                $"서버 DB에 접속할 수 없어 로컬 DB로 전환합니다.\n\n사유: {sqlEx.Message}\n\n※ 로컬 작업 내용은 서버와 동기화되지 않습니다.",
                                "로컬 DB 모드 안내",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show($"서버 DB 접속 실패 및 로컬 DB 정보가 없습니다.\n\n상세: {sqlEx.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"{xmlFilePath} 파일에 connection 정보가 존재하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                // 로컬 모드일 때 메인 폼 타이틀바 표시
                if (isLocalMode)
                {
                    this.Text += " [로컬 DB 모드]";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"초기화 중 오류가 발생했습니다.\n\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        // 트레이의 종료 메뉴를 눌렀을때
        private void TT_Elased(object sender, EventArgs e)
        {
            string rfiles = Config.RecvDir;

            if (!auto) return;

            TT.Stop();
            try
            {
                string[] flist = Common.GetFiles(rfiles);


                this.Invoke(new MethodInvoker(delegate {
                    try
                    {
                        listBox1.SuspendLayout(); listBox1.Items.Clear(); listBox1.Items.AddRange(flist); listBox1.ResumeLayout();
                    }
                    catch (Exception E) { }
                }));

                for (int j = 0; j < 10; j++)
                {
                    Thread.Sleep(1000);
                    Application.DoEvents();
                }

                int cc = flist.Count();
                if (cc > 0)
                {
                    this.Invoke(new MethodInvoker(delegate { try { DispMsg(listBox2, "File 처리를 시작합니다...!!"); } catch (Exception E) { } }));

                    XmlParse xmlparse = new XmlParse(this);

                    foreach (var f in flist)
                    {

                        string s = xmlparse.GetDocType(f);
                        if (s == "")
                        {
                            Common.MoveFailureFile(f);
                            this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + "= 알수 없는 XML File 입니다.thread"); }));
                        }
                        else if (s.Substring(0, 8) == "WMTOID02")
                        {
                            if (xmlparse.ParseWMTO(f) == true)
                            {
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing success!."); }));
                                Common.MoveSuccessFile(f);
                            }
                            else
                            {
                                Common.MoveExceptionFile(f);
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing failed!."); }));
                            }
                        }
                        else if (s.Substring(0, 4) == "ZMAT")
                        {
                            if (xmlparse.ParseZMATMS(f) == true)
                            {
                                Common.MoveSuccessFile(f);
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing success!."); }));
                            }
                            else
                            {
                                Common.MoveExceptionFile(f);
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing failed!."); }));
                            }
                        }
                        else if (s.Substring(0, 4) == "DELV")
                        {
                            if (xmlparse.ParseDelivery(f) == true)
                            {
                                Common.MoveSuccessFile(f);
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing success!."); }));
                            }
                            else
                            {
                                Common.MoveExceptionFile(f);
                                this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + " Parsing failed!."); }));
                            }
                        }
                        else
                        {
                            Common.MoveFailureFile(f);
                            this.Invoke(new MethodInvoker(delegate { DispMsg(listBox2, f + "= 알수 없는 XML File 입니다.thread"); }));
                        }
                    }


                    this.Invoke(new MethodInvoker(delegate
                    {
                        try
                        {
                            listBox1.SuspendLayout();
                            listBox1.Items.Clear();
                            listBox1.ResumeLayout();
                        }
                        catch (Exception E) { }
                    }));
                }
            }
            catch (Exception E) { }
            finally
            {
                TT.Start();
            }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFlag = true;
            //트레이아이콘 없앰
            notifyIcon1.Visible = false;
            //프로세스 종료
            Application.Exit();
        }

        //트레이 아이콘을 더블클릭 했을시 호출
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true; // 폼의 표시
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal; // 최소화를 멈춘다 
            this.Activate(); // 폼을 활성화 시킨다
        }


        //폼이 종료 되려 할때 호출
        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseFlag)
                e.Cancel = true; // 종료 이벤트를 취소 시킨다
            this.Visible = false; // 폼을 표시하지 않는다;
        }


        public void DisplayFolder()
        {
            string[] ss;
            try
            {
                listBox1.SuspendLayout();
                listBox1.Items.Clear();

                string path = Config.RecvDir;
                ss = Common.GetFiles(path);

                listBox1.Items.AddRange(ss);

            }
            catch (Exception E)
            {
                //EventLog.Event_Log("EventLog", "Exception", E.Message, true);
                DispMsg(listBox2, E.Message);
            }
            finally
            {
                listBox1.ResumeLayout();
            }
        }
            
        public void DispMsg(ListBox lb, string msg)
        {
            string tm = DateTime.Now.ToString("hh:mm:ss.fff");

            lb.SuspendLayout();
            if (lb.Items.Count >= 500)
            {
                lb.Items.Clear();
            }

            lb.Items.Insert(0, tm + ":  " + msg);
            lb.ResumeLayout();
        }

        private void btndir_Click(object sender, EventArgs e)
        {
            DisplayFolder();
        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            auto = false;
            btndir.Enabled = true;
            btnParse.Enabled = true;

            btnAuto.Enabled = true;
            btnHand.Enabled = false;
            label4.Text = "수동 모드";
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            auto = true;
            btndir.Enabled = false;
            btnParse.Enabled = false;

            btnAuto.Enabled = false;
            btnHand.Enabled = true;
            label4.Text = "자동 모드";
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string file = listBox1.SelectedItems[0].ToString();
            if (file == string.Empty) return;

            XmlParse xmlparse = new XmlParse(this);

            string s = xmlparse.GetDocType(file);
            if (s == "" || s.Length < 4)
            {
                Common.MoveFailureFile(file);
                DispMsg(listBox2, file + "= 알수 없는 XML File 입니다.");
            }
            else if (s.Substring(0, 4) == "WMTO")
            {
                if (xmlparse.ParseWMTO(file))
                {
                    Common.MoveSuccessFile(file);
                    DispMsg(listBox2, file + "= OK");
                }
                else
                {
                    Common.MoveExceptionFile(file);
                    DispMsg(listBox2, file + "= Exception");
                }
            }
            else if (s.Substring(0, 4) == "ZMAT")
            {
                if (xmlparse.ParseZMATMS(file))
                {
                    Common.MoveSuccessFile(file);
                    DispMsg(listBox2, file + "= OK");
                }
                else
                {
                    Common.MoveExceptionFile(file);
                    DispMsg(listBox2, file + "= Exception");
                }

            }
            else if (s.Substring(0, 4) == "DELV")
            {
                if (xmlparse.ParseDelivery(file))
                {
                    Common.MoveSuccessFile(file);
                    DispMsg(listBox2, file + "= OK");
                }
                else
                {
                    Common.MoveExceptionFile(file);
                    DispMsg(listBox2, file + "= Exception");
                }
            }
            else
            {
                Common.MoveFailureFile(file);
                DispMsg(listBox2, file + "= 알수 없는 XML File 입니다.");
            }
            DisplayFolder();
        }
     
        private void FMain_Load(object sender, EventArgs e)
        {
          
            DisplayFolder();

            TT.Elapsed += TT_Elased;
            TT.Start();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
