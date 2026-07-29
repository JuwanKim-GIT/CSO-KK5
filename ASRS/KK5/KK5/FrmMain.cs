using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KK5
{
    public partial class FrmMain : Form
    {
        public FrmMain()
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

                // USER LEVEL 읽기 (KK5 프로젝트용)
                var p = (from d in xd.Root.Descendants("ACCESS") select d.Element("USER")).SingleOrDefault();
                if (p != null)
                {
                    Config.UserLevel = p.Value.ToString();
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
        private void ShowForm(Form f)
        {
            f.MdiParent = this;
            f.Show();
            f.Activate();
            f.WindowState = FormWindowState.Maximized;
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //if (Config.UserLevel != "1") inToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void milstkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLstkmgr.Instance);
        }

        private void miinpetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMiinpe.Instance);
        }

        private void hiinpeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Frmhiinpe.Instance);
        }

        private void FactoryinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmInptF.Instance);
        }

        private void maininToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmInptY.Instance);
        }

        private void q5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLabelprn.Instance);
        }

        private void q3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmItemSum1.Instance);
        }

        private void q4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmItemSum2.Instance);
        }

        private void m1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMimast.Instance);
        }

        private void m2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMicust.Instance);
        }

        private void m4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMidest.Instance);
        }

        private void m5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMiarea.Instance);
        }

        private void cVCLinkTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLinkTable.Instance);
        }

        private void T3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmStackErorr.Instance);
        }

        private void rCPJOBsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmRcpJob.Instance);
        }

        private void l1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmBankDisp.Instance);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMichng.Instance);
        }

        private void 재고변경이력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Frmhichng.Instance);
        }

        private void o1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMiordi.Instance);
        }

        private void q2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmHiodi.Instance);
        }

        private void o3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMiwmtx.Instance);
        }

        private void o4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmHiwmtx.Instance);
        }

        private void s1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmTaordi.Instance);
        }

        private void s2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmTaordiLoad.Instance);
        }

        private void s3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLoadHistory1.Instance);
        }

        private void q2StripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmItemLstk.Instance);
        }

        private void s4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLoadHistory2.Instance);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMimvht.Instance);
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMiuser.Instance);
        }

        private void m3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmTaCar.Instance);
        }

        private void awToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmMijchg.Instance);
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmAdvMiplti.Instance);
        }

        private void q6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmBarError.Instance);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            ShowForm(FrmLstkCelinfo.Instance);
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmEtcWmto.Instance);
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmTawmtoLoad.Instance);
        }

        private void 기타상차이력1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmEtcLoadHistory1.Instance);
        }

        private void 기타상차이력2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(FrmEtcLoadHistory2.Instance);
        }
    }
}
