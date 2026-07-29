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
using System.Xml.Linq;

namespace RCP
{
    public partial class FMain : Form
    {
        bool disp11 = true;
        bool disp12 = true;
        bool disp13 = true;
        bool disp14 = true;
        bool disp15 = true;
        bool disp2 = true;
        bool disp3 = true;
        bool disp4 = true;

        public FMain()
        {
            
            InitializeComponent();

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
        //SCRC scrc = null;
        UpdateProc updt;
        LabelPrinter LP;
        SCRC scrc;
        CVC cvc;
        private void FMain_Load(object sender, EventArgs e)
        {
            // update proc
            updt = new UpdateProc();
            updt.bw.ProgressChanged += Bwupdt_ProgressChanged;

            // label proc
            LP = new LabelPrinter();
            LP.bwprn.ProgressChanged += Bwprn_ProgressChanged;

            //스텍카크레인 
            scrc = new SCRC();
            scrc.bw.ProgressChanged += Bwscrc_ProgressChanged;

            //컨베이어
            cvc = new CVC();
            cvc.bw.ProgressChanged += Bwcvc_ProgressChanged;
        }

        private void Bwscrc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (led1.Active) led1.Active = false; else led1.Active = true;

            int hogi = e.ProgressPercentage;
            if (hogi == 0 & !disp11) return;
            if (hogi == 1 & !disp12) return;
            if (hogi == 2 & !disp13) return;
            if (hogi == 3 & !disp14) return;
            if (hogi == 4 & !disp15) return;

            listBox1.SuspendLayout();
            if (listBox1.Items.Count > 200) listBox1.Items.RemoveAt(listBox1.Items.Count - 1);

            string ls = DateTime.Now.ToString("hh:mm:ss.fff");

            if (hogi != 9)
            {
                listBox1.Items.Insert(0, (hogi + 1).ToString("0") + "호기 [" + ls + "] " + e.UserState.ToString());
            }
            else {
                listBox1.Items.Insert(0, "Conveyor [" + ls + "] " + e.UserState.ToString());
            }

            listBox1.ResumeLayout();
        }

        private void Bwprn_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 2)
            {
                if (led3.Active) led3.Active = false; else led3.Active = true;
                return;
            }
            if (!disp3) return;

            listBox3.SuspendLayout();
            if (listBox3.Items.Count > 200) listBox3.Items.RemoveAt(listBox3.Items.Count - 1);

            string ls = DateTime.Now.ToString("hh:mm:ss.fff");

            listBox3.Items.Insert(0, "[" + ls + "] " + e.UserState.ToString());
            listBox3.ResumeLayout();
        }

        private void Bwupdt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 2)
            {
                if (ledu.Active) ledu.Active = false; else ledu.Active = true;
                return;
            }
            if (!disp4) return;

            listBox4.SuspendLayout();
            if (listBox4.Items.Count > 200) listBox4.Items.RemoveAt(listBox4.Items.Count - 1);

            string ls = DateTime.Now.ToString("hh:mm:ss.fff");

            listBox4.Items.Insert(0, "[" + ls + "] " + e.UserState.ToString());
            listBox4.ResumeLayout();
        }

        private void Bwcvc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 2)
            {
                if (led2.Active) led2.Active = false; else led2.Active = true;
                return;
            }
            if (!disp2) return;

            listBox2.SuspendLayout();
            if (listBox2.Items.Count > 200) listBox2.Items.RemoveAt(listBox2.Items.Count - 1);

            string ls = DateTime.Now.ToString("hh:mm:ss.fff");


            listBox2.Items.Insert(0, "[" + ls + "] " + e.UserState.ToString());
            listBox2.ResumeLayout();
        }

        private void Bwcvc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dispMsg(listBox2, (string)e.Result);
        }

        private void Bwsrc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dispMsg(listBox1, (string)e.UserState);
        }

        private void Bwsrc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBox1.Items.Insert(0, "SC 프로그램 종료...!");

        }

        #region Setup Bacgroudworker 


        #endregion

        #region Comport Setup
        private void SetupSCRCComport()
        {
            //sPort1.PortName = "COM5";
            //sPort1.BaudRate = 9600;
            //sPort1.DataBits = 7;
            //sPort1.Parity = System.IO.Ports.Parity.Even;
            //sPort1.StopBits = System.IO.Ports.StopBits.One;
            //sPort1.Handshake = System.IO.Ports.Handshake.None;

            //sPort2.PortName = "COM6";
            //sPort2.BaudRate = 9600;
            //sPort2.DataBits = 7;
            //sPort2.Parity = System.IO.Ports.Parity.Even;
            //sPort2.StopBits = System.IO.Ports.StopBits.One;
            //sPort2.Handshake = System.IO.Ports.Handshake.None;

            //sPort3.PortName = "COM7";
            //sPort3.BaudRate = 9600;
            //sPort3.DataBits = 7;
            //sPort3.Parity = System.IO.Ports.Parity.Even;
            //sPort3.StopBits = System.IO.Ports.StopBits.One;
            //sPort3.Handshake = System.IO.Ports.Handshake.None;

            //sPort4.PortName = "COM8";
            //sPort4.BaudRate = 9600;
            //sPort4.DataBits = 7;
            //sPort4.Parity = System.IO.Ports.Parity.Even;
            //sPort4.StopBits = System.IO.Ports.StopBits.One;
            //sPort4.Handshake = System.IO.Ports.Handshake.None;

            //sPort5.PortName = @"\\.\COM9";
            //sPort5.BaudRate = 9600;
            //sPort5.DataBits = 7;
            //sPort5.Parity = System.IO.Ports.Parity.Even;
            //sPort5.StopBits = System.IO.Ports.StopBits.One;
            //sPort5.Handshake = System.IO.Ports.Handshake.None;
        }
        private void SetupCVCComport()
        {
            //cPort.PortName = @"\\.\COM11";
            //cPort.BaudRate = 9600;
            //cPort.DataBits = 7;
            //cPort.Parity = System.IO.Ports.Parity.Even;
            //cPort.StopBits = System.IO.Ports.StopBits.One;
            //cPort.Handshake = System.IO.Ports.Handshake.None;
        }
        #endregion

        private void dispMsg(ListBox lb, string msg)
        {
            lb.SuspendLayout();
            int c = lb.Items.Count;
            if (c > 200)
            {
                lb.Items.RemoveAt(200);
            }
            string ls_time = DateTime.Now.ToShortTimeString();
            lb.Items.Insert(0, ls_time + " > " + msg);
            lb.ResumeLayout();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            scrc.stop_scrcproc();
            led1.Active = false;
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FrmExit_p p = new FrmExit_p())
            {
                p.Show();
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                scrc.bw.CancelAsync();
                Thread.Sleep(1000);
                cvc.bw.CancelAsync();
                Thread.Sleep(1000);             

                updt.bw.CancelAsync();
                Thread.Sleep(1000);
                LP.bwprn.CancelAsync();
                Thread.Sleep(1000);
                Cursor = Cursors.Default;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (updt.bw.IsBusy) return;
            updt.Run_upateproc();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            updt.stop_upateproc();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
          

        private void button8_Click(object sender, EventArgs e)
        {
            if (LP.bwprn.IsBusy) return;
            LP.bwprn.RunWorkerAsync();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LP.bwprn.CancelAsync();
        }

      
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnScstart_Click(object sender, EventArgs e)
        {
            if (scrc.bw.IsBusy) return;
            scrc.Run_scrcproc();
        }

        private void chkcvc_CheckedChanged(object sender, EventArgs e)
        {
            disp2 = chkcvc.Checked;
        }

        private void chkupdt_CheckedChanged(object sender, EventArgs e)
        {
            disp4 = chkupdt.Checked;
        }

        private void chklabl_CheckedChanged(object sender, EventArgs e)
        {
            disp3 = chklabl.Checked;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (cvc.bw.IsBusy) return;
            cvc.Run_cvcproc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cvc.stop_cvcproc();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            disp11 = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            disp12 = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            disp13 = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            disp14 = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {            
           disp15= checkBox5.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
         

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
            checkBox2.Checked = !checkBox2.Checked;
            checkBox3.Checked = !checkBox3.Checked;
            checkBox4.Checked = !checkBox4.Checked;
            checkBox5.Checked = !checkBox5.Checked;
        }
    }
 }
