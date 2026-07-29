using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Dynamic;
using System.Transactions;
using System.Xml.Linq;

namespace MCP
{
    public partial class MCP : Form
    {
        public MCP()
        {
            InitializeComponent();
            this.Size = new Size(1024, 743);

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

                // USER LEVEL 읽기 (MCP 프로젝트용)
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
        private void MCP_Load(object sender, EventArgs e)
        {
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            retrieve();
            of_refresh();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bw.CancellationPending)
            {
               
                Thread.Sleep(1000);
                bw.ReportProgress(1);

            }
        }
        
        private void Btncvoff_Click(object sender, EventArgs e)
        {
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                string stop = d.ExecuteQuery<string>(@"select cnvc_stop from tbcnvc where  cnvc_mode = '01' ").SingleOrDefault();
               
                if (stop == "0")
                {
                    btncvoff.Text = "C/V OFF";
                    if (MessageBox.Show("정말로 콘베어를 사용중지(OFF) 합니까....?", "C/V OFF처리",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                       
                        return;
                    }
                    else
                    {
                        d.ExecuteCommand(@"update tbcnvc set cnvc_stop = '1' where  cnvc_mode = '01' ");
                        btncvoff.Text = "C/V ON";
                        return;
                    }
                }
                else if (stop == "1")
                {
                    btncvoff.Text = "C/V ON";
                    if (MessageBox.Show("정말로 콘베어를 사용가동(ON) 합니까....?", "C/V ON 처리",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {                       
                        return;
                    }
                    else
                    {
                        d.ExecuteCommand(@"update tbcnvc set cnvc_stop = '0' where  cnvc_mode = '01' ");
                        btncvoff.Text = "C/V OFF";
                        return;
                    }
                }
            }

        }

        private void btnexit_Click(object sender, EventArgs e)
        {


            ////char[] sc_pwr_onof = new char[9] { '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            ////char[] ls_bits = new char[4] { '0', '0', '0', '0' };
            ////string ls_bit2 = "0110";

            ////ls_bits = ls_bit2.ToCharArray();
            ////string recv_pwron = new string(ls_bits[1], 1);
            ////sc_pwr_onof[1] = recv_pwron.ToCharArray()[0];
            ////string recv_emer = new string(ls_bits[2], 1);

            //MessageBox.Show(recv_pwron + "-" + recv_emer + sc_pwr_onof[1]);


            //try
            //{
            //    int rc = 0;
            //    string lsi = "1", jno = "",  indx = "", sql = "";
            //    DBDataContext d = new DBDataContext();
            //    string fstn = "21";
            //    d.Connection.open();
            //    d.Transaction = d.Connection.BeginTransaction();

            //    //using (TransactionScope sc = new TransactionScope())
            //    //{
            //        rc = d.p_get_indx_jno(lsi, ref jno);         //jno  = f_get_indx_jno(lsi);

            //        if (rc == 0)
            //        {                      
            //            return;
            //        }
            //        MessageBox.Show("sss");
            //        indx = jno.Substring(jno.Length - 4, 4);      //indx = right(jno, 4);

            //        sql = @"INSERT INTO tbindx  
            //                   ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
            //                       indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
            //                     indx_edat,     indx_sflg,       indx_uflg )  
            //                 VALUES( {0},           {1},           'A',             'M',           '0',
            //                            {2},          '24',            '',              '',            'N',
            //                   '',            'P',             '0')";

            //        rc = d.ExecuteCommand(sql, jno, indx, fstn);
            //    d.Transaction.Rollback();
            //    d.Connection.Close();

            //    //}
            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show(E.Message);
            //}
            //try
            //{
            //    using (DBDataContext d = new DBDataContext(Config.DBCon))
            //    {
            //        try
            //        {
            //            d.Connection.open();
            //            d.Transaction = d.Connection.BeginTransaction();
            //            d.ExecuteCommand(@"update testbl set a = '2', b = '2' where id = 1");

            //            //using (DBDataContext dd = new DBDataContext())
            //            //{

            //            //    dd.Connection.open();
            //            //    dd.Transaction = dd.Connection.BeginTransaction();
            //            //    dd.ExecuteCommand(@"update testbl set a = '3', b = '3' where id = 2");

            //            //    dd.Transaction.Commit();
            //            //    dd.Connection.Close();
            //            //}
            //            MessageBox.Show("sss");

            //            d.Transaction.Rollback();
            //            d.Connection.Close();
            //        }
            //        catch (Exception E) { }
            //        finally
            //        {
            //            MessageBox.Show("s");
            //            d.Connection.Close();
            //        }


            //    }
            //}
            //catch (Exception E) { }
            //finally { }

            ////DBDataContext d = new DBDataContext();
            ////try
            //{
            //    using (TransactionScope scope = new TransactionScope())
            //    {
            //        d.ExecuteCommand(@"update testbl set a = '2', b = '33' where id = 2");
            //        MessageBox.Show("aaa");


            //    }
            //}
            //catch (Exception E) { MessageBox.Show(E.Message); }


            if (MessageBox.Show("모니토링 프로그램 종료 하겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            bw.CancelAsync();
            Application.DoEvents();

            Application.Exit();

        }
        private void btnlabelopt_Click(object sender, EventArgs e)
        {
            using (FrmLabelOption_p p = new FrmLabelOption_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                string lr = "L";

                if (p.radioButton1.Checked) lr = "L";
                if (p.radioButton2.Checked) lr = "R";

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"update tbstat set stat_lr = {0} where stat_key = '1' ", lr);
                }
            }
        }
        private void btnnormalopt_Click(object sender, EventArgs e)
        {
            using (FrmOutOption_p p = new FrmOutOption_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                string ls = "0";

                if (p.radioButton1.Checked) ls = "0";
                if (p.radioButton2.Checked) ls = "1";

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"update tbstat set stat_out = {0} where stat_key = '1' ", ls);
                }
            }
        }

        private void btnmoveopt_Click(object sender, EventArgs e)
        {
            using (FrmMoveOption_p p = new FrmMoveOption_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                string dplt = "0";

                if (p.radioButton1.Checked) dplt = "0";
                if (p.radioButton2.Checked) dplt = "1";

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"update tbstat set stat_dplt = {0} where stat_key = '1' ", dplt);
                }
            }
        }

        private void btninptopt_Click(object sender, EventArgs e)
        {
            using (FrmEproc_p p = new FrmEproc_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                string barm = "0";

                if (p.radioButton1.Checked) barm = "0";
                if (p.radioButton2.Checked) barm = "1";

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"update tbstat set stat_barm = {0} where stat_key = '1' ", barm);
                }
            }
        }

        private void btnirsrv_Click(object sender, EventArgs e)
        {
            using (FrmPath_p p = new FrmPath_p())
            {
                p.ShowDialog();
                if (p.DialogResult == DialogResult.Cancel) return;
                string path = "0";

                if (p.radioButton1.Checked) path = "0";
                if (p.radioButton2.Checked) path = "1";
                if (p.radioButton3.Checked) path = "2";
                if (p.radioButton4.Checked) path = "3";

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.ExecuteCommand(@"update tbstat set stat_ipath = {0} where stat_key = '1' ", path);
                }
            }
        }
        private void btnread_Click(object sender, EventArgs e)
        {
            using (FrmBarcodeInit_p p = new FrmBarcodeInit_p())
            {
                p.ShowDialog();
            }
        }

        private void btnBarInput_Click(object sender, EventArgs e)
        {
            using (FrmInptBarcode_p p = new FrmInptBarcode_p())
            {
                p.ShowDialog();
            }
        }
        private void btnymove_Click(object sender, EventArgs e)
        {
            using (FrmYmove_p p = new FrmYmove_p())
            {
                p.ShowDialog();
            } 
        }
  
        private void of_refresh()
        {
            Label lb;
            Color colr;
            for (int i = 0; i < 50; i++)
            {
                string ls_chek = cv_palt[i].ToString() + cv_data[i].ToString();
                if (i == 28 || i == 29 || i == 47) continue;

                lb = this.Controls["b" + (i + 1).ToString("00")] as Label;

                colr = Color.FromKnownColor(KnownColor.DarkSeaGreen);
                //colr = Color.FromArgb(220, 192, 220);
                if (ls_chek == "11")
                {
                    colr = Color.Green;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ls_chek == "10")
                {
                    colr = Color.Cyan;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ls_chek == "01")
                {
                    colr = Color.Lime;
                    lb.BorderStyle = BorderStyle.None;
                }
                else
                {
                    lb.BorderStyle = BorderStyle.None;
                }
                lb.BackColor = colr;

                if (i == 0)
                {
                    if (cv_irdy[0] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r1.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r1.Visible = false; }
                }
                if (i == 1)
                {
                    if (cv_ordy[0] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r2.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r2.Visible = false;  }
                }
                if (i == 2)
                {
                    if (cv_irdy[1] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r3.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r3.Visible = false; }
                }
                if (i == 3)
                {
                    if (cv_ordy[1] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r4.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r4.Visible = false; }
                }
                if (i == 4)
                {
                    if (cv_irdy[2] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r5.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r5.Visible = false; }
                }
                if (i == 5)
                {
                    if (cv_ordy[2] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r6.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r6.Visible = false; }
                }
                if (i == 6)
                {
                    if (cv_irdy[3] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r7.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r7.Visible = false; }
                }
                if (i == 7)
                {
                    if (cv_ordy[3] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r8.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r8.Visible = false; }
                }
                if (i == 8)
                {
                    if (cv_irdy[4] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r9.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r9.Visible = false; }
                }
                if (i == 9)
                {
                    if (cv_ordy[4] == '1') { lb.BorderStyle = BorderStyle.FixedSingle; r10.Visible = true; } else { lb.BorderStyle = BorderStyle.None; r10.Visible = false; }
                }
            
            }

            if (cv_21_rqst != "0") x_21.BackColor = Color.Green; else x_21.BackColor = Color.Ivory;
            if (cv_22_rqst != "0") x_22.BackColor = Color.Green; else x_22.BackColor = Color.Ivory;
            if (cv_24_rqst != "0") x_24.BackColor = Color.Green; else x_24.BackColor = Color.Ivory;


            for (int j = 0; j < 8; j++)
            {
                Color color = Color.FromKnownColor(KnownColor.DarkSeaGreen);
                //Color color = Color.FromArgb(220, 192, 220);
                if (cv_onln[j] == '0') color = Color.Yellow;
                if (cv_eror[j] == '1') color = Color.Red;
                if (cv_stop == "1") color = Color.Black;
                if (cv_comm == "0") color = Color.Magenta;

                if (j == 0) //1호기
                {
                    c01.BackColor = color; c02.BackColor = color; c11.BackColor = color;
                }
                if (j == 1)//2호기
                {
                    c03.BackColor = color; c04.BackColor = color; c21.BackColor = color;
                }
                if (j == 2)//3호기
                {
                    c05.BackColor = color; c06.BackColor = color; c31.BackColor = color;
                }
                if (j == 3)//4호기
                {
                    c07.BackColor = color; c08.BackColor = color; c41.BackColor = color;
                }
                if (j == 4)//5호기
                {
                    c09.BackColor = color; c10.BackColor = color; c51.BackColor = color;
                }
                if (j == 5) //입고 scan 주위 컨베이어 버퍼
                {
                    c61.BackColor = color; c62.BackColor = color; c63.BackColor = color;
                    c611.BackColor = color;
                    c612.BackColor = color;
                    c613.BackColor = color;
                    c614.BackColor = color;
                    c615.BackColor = color;
                    c616.BackColor = color;
                    c617.BackColor = color;
                    c618.BackColor = color;
                    c619.BackColor = color;
                }
                if (j == 6) //입고 컨베이어 버퍼
                {
                    c721.BackColor = color; c722.BackColor = color; c723.BackColor = color;
                    c711.BackColor = color;
                    c712.BackColor = color;
                    c713.BackColor = color;
                    c714.BackColor = color;
                    c715.BackColor = color;
                    c716.BackColor = color;
                    c717.BackColor = color;
                    c718.BackColor = color;
                    c719.BackColor = color;
                }

                if (j == 7) //출고대
                {
                    c81.BackColor = color;
                    c821.BackColor = color;
                    c822.BackColor = color;
                    c823.BackColor = color;
                    c824.BackColor = color;
                    c825.BackColor = color;
                }
            } 

            // sc
            int li_bay = 0, ll_x = 0;
            string ls_bay = "";
            for (int k = 0; k < 5; k++)
            {
                ls_bay = sc_posi[k].Substring(0, 2);
                li_bay = Convert.ToInt32(ls_bay);
                if (li_bay > 36) li_bay = 36;
                if (li_bay == 1) { ll_x = 575; }
                else
                {
                    ll_x = (int)(575 - (li_bay * 15.52777));
                    //ll_x = (int)(575 - ((li_bay - 1) * 15.52777));
                    
                    //if (li_bay > 5) ll_x = ll_x - 6;
                    //if (li_bay > 10) ll_x = ll_x - 6;
                    //if (li_bay > 15) ll_x = ll_x - 6;
                    //if (li_bay > 20) ll_x = ll_x - 6;
                    //if (li_bay > 25) ll_x = ll_x - 7;
                    //if (li_bay > 30) ll_x = ll_x - 7;
                    //if (li_bay > 35) ll_x = ll_x - 7;

                }

                Label sc = this.Controls["s" + (k + 1).ToString("00")] as Label;
                sc.Left = ll_x;

                string plt_data = sc_palt[k].ToString() + sc_data[k].ToString();
                if (plt_data == "11")
                {
                    sc.BorderStyle = BorderStyle.Fixed3D;
                    sc.BackColor = Color.Green;
                }
                else if (plt_data == "10")
                {
                    sc.BorderStyle = BorderStyle.Fixed3D;
                    sc.BackColor = Color.Cyan;
                }
                else if (plt_data == "01")
                {
                    sc.BorderStyle = BorderStyle.None;
                    sc.BackColor = Color.Lime;
                }
                else
                {
                    sc.BorderStyle = BorderStyle.None;
                    sc.BackColor = Color.DarkSeaGreen;
                }
                               
                if (sc_eror[k] != '0') sc.BackColor = Color.Red;
                if (sc_onln[k] == '0') sc.BackColor = Color.Yellow;
                if (sc_stop[k] == '1') sc.BackColor = Color.Black;
                if (sc_comm[k] == '0') sc.BackColor = Color.Magenta;

            }// end for k

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var t = db.ExecuteQuery(@"select stat_imode, stat_ipath from tbstat where stat_key = '1'").SingleOrDefault();

                if (t != null)
                {
                    if (t.stat_imode == "1") bmode.Text = "바코드입고"; else bmode.Text = "선택입고";
                    if (t.stat_ipath == "0") { p_up.Visible = false; p_left.Visible = false; } // 양쪽진입금지
                    if (t.stat_ipath == "1") { p_up.Visible = true; p_left.Visible = false; } // 21 입고 메인
                    if (t.stat_ipath == "2") { p_up.Visible = false; p_left.Visible = true; }  // 22 입고 공장동
                    if (t.stat_ipath == "3") { p_up.Visible = true; p_left.Visible = true; }  // 양쪽                   
                }

                var b = db.ExecuteQuery(@"select barc_pltno, cvc_msg,   barc_msg, barc_flag from tibarc where barc_key = '1' ").SingleOrDefault();
                if (b != null)
                {
                    if (is_pltno != b.barc_pltno)
                    {
                        st_pltno.Text = b.barc_pltno;
                        is_pltno = b.barc_pltno;
                    }


                    if (is_cvmsg != b.cvc_msg)
                    {
                        st_msg.Text = is_cvmsg = b.cvc_msg;
                    }
                    if (is_flag != b.barc_flag)
                    {
                        switch ((string)b.barc_flag)
                        {
                            case "0":
                                st_flag.Text = "";
                                break;
                            case "1":
                                st_flag.Text = "읽음";
                                break;
                            case "2":
                                st_flag.Text = "처리완료";
                                break;
                            case "3":
                                st_flag.Text = "다음대기";
                                break;
                            default:
                                break;
                        }
                        is_flag = b.barc_flag;
                    }
                }
            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using(FrmIMode_p p = new FrmIMode_p())
            {
                p.ShowDialog();
            }
        }

        private void s01_Click(object sender, EventArgs e)
        {
            using (FrmSCRC_p p = new FrmSCRC_p("01"))
            {
                p.ShowDialog();
            }                
        }
        private void s02_Click(object sender, EventArgs e)
        {
            using (FrmSCRC_p p = new FrmSCRC_p("02"))
            {
                p.ShowDialog();
            }
        }
        private void s03_Click(object sender, EventArgs e)
        {
            using (FrmSCRC_p p = new FrmSCRC_p("03"))
            {
                p.ShowDialog();
            }
        }
        private void s04_Click(object sender, EventArgs e)
        {
            using (FrmSCRC_p p = new FrmSCRC_p("04"))
            {
                p.ShowDialog();
            }
        }
        private void s05_Click(object sender, EventArgs e)
        {
            using (FrmSCRC_p p = new FrmSCRC_p("05"))
            {
                p.ShowDialog();
            }
        }
        private void MCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.CancelAsync();      
        }
        private void btnSCControl_Click(object sender, EventArgs e)
        {
            using (FrmSCContol_p p = new FrmSCContol_p())
            {
                p.ShowDialog();
            }
        }

        private void retrieve()
        {
            cv_onln = Fill<char>('0', 8);
            cv_eror = Fill<char>('0', 8);
            cv_palt = Fill<char>('0', 50);
            cv_data = Fill<char>('0', 50);
            cv_irdy = Fill<char>('0', 5);
            cv_ordy = Fill<char>('0', 5);
            cv_21_rqst = "0";
            cv_22_rqst = "0";
            cv_24_rqst = "0";
            cv_stop = "0";
            cv_comm = "0";

            for (int i = 0; i < 47; i++)
            {
                //cv_jno[i] = new string(Fill<char>('0', 4));
                cv_jno[i] = "0000";
            }

            sc_onln = Fill<char>('1', 5);
            sc_data = Fill<char>('0', 5);
            sc_palt = Fill<char>('0', 5);
            sc_eror = Fill<char>('0', 5);
            sc_stat = Fill<char>('0', 5);
            sc_redy = Fill<char>('0', 5);
            sc_stop = Fill<char>('0', 5);
            sc_comm = Fill<char>('0', 5);
            for (int i = 0; i < 5; i++) sc_posi[i] = "0000";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var r = db.ExecuteQuery<tbcnvc>("Select * from tbcnvc where cnvc_mode = '01'").Single();

                cv_onln = r.cnvc_op_onof.ToCharArray();
                cv_eror = r.cnvc_op_eror.ToCharArray();
                cv_palt = r.cnvc_buf_palt.ToCharArray();
                cv_irdy = r.cnvc_ist_redy.ToCharArray();
                cv_ordy = r.cnvc_ost_redy.ToCharArray();
                cv_21_rqst = r.cnvc_21_rqst;
                cv_22_rqst = r.cnvc_22_rqst;
                cv_24_rqst = r.cnvc_24_rqst;
                cv_stop = r.cnvc_stop;
                cv_comm = r.cnvc_comm;

                for (int i = 0; i < 47; i++)
                {
                    cv_jno[i] = r.cnvc_jobno.Substring(i * 4, 4);
                }

                for (int i = 0; i < 10; i++)
                {
                    if (r.cnvc_jobno.Substring(i * 4, 4) != "0000")
                    {
                        cv_data[i] = '1';
                    }
                }
                if (r.cnvc_jobno.Substring(4 * 10, 4) != "0000") cv_data[20] = '1';
                if (r.cnvc_jobno.Substring(4 * 11, 4) != "0000") cv_data[21] = '1';
                if (r.cnvc_jobno.Substring(4 * 12, 4) != "0000") cv_data[42] = '1';
                if (r.cnvc_jobno.Substring(4 * 13, 4) != "0000") cv_data[44] = '1';
                if (r.cnvc_jobno.Substring(4 * 14, 4) != "0000") cv_data[49] = '1';

                if (r.cnvc_jobno.Substring(4 * 15, 4) != "0000") cv_data[10] = '1';
                if (r.cnvc_jobno.Substring(4 * 16, 4) != "0000") cv_data[11] = '1';
                if (r.cnvc_jobno.Substring(4 * 17, 4) != "0000") cv_data[12] = '1';
                if (r.cnvc_jobno.Substring(4 * 18, 4) != "0000") cv_data[13] = '1';
                if (r.cnvc_jobno.Substring(4 * 19, 4) != "0000") cv_data[14] = '1';
                if (r.cnvc_jobno.Substring(4 * 20, 4) != "0000") cv_data[15] = '1';
                if (r.cnvc_jobno.Substring(4 * 21, 4) != "0000") cv_data[16] = '1';
                if (r.cnvc_jobno.Substring(4 * 22, 4) != "0000") cv_data[17] = '1';
                if (r.cnvc_jobno.Substring(4 * 23, 4) != "0000") cv_data[18] = '1';
                if (r.cnvc_jobno.Substring(4 * 24, 4) != "0000") cv_data[19] = '1';


                if (r.cnvc_jobno.Substring(4 * 25, 4) != "0000") cv_data[22] = '1';
                if (r.cnvc_jobno.Substring(4 * 26, 4) != "0000") cv_data[23] = '1';
                if (r.cnvc_jobno.Substring(4 * 27, 4) != "0000") cv_data[24] = '1';
                if (r.cnvc_jobno.Substring(4 * 28, 4) != "0000") cv_data[25] = '1';
                if (r.cnvc_jobno.Substring(4 * 29, 4) != "0000") cv_data[26] = '1';
                if (r.cnvc_jobno.Substring(4 * 30, 4) != "0000") cv_data[27] = '1';


                if (r.cnvc_jobno.Substring(4 * 31, 4) != "0000") cv_data[30] = '1';
                if (r.cnvc_jobno.Substring(4 * 32, 4) != "0000") cv_data[31] = '1';
                if (r.cnvc_jobno.Substring(4 * 33, 4) != "0000") cv_data[32] = '1';
                if (r.cnvc_jobno.Substring(4 * 34, 4) != "0000") cv_data[33] = '1';
                if (r.cnvc_jobno.Substring(4 * 35, 4) != "0000") cv_data[34] = '1';
                if (r.cnvc_jobno.Substring(4 * 36, 4) != "0000") cv_data[35] = '1';
                if (r.cnvc_jobno.Substring(4 * 37, 4) != "0000") cv_data[36] = '1';
                if (r.cnvc_jobno.Substring(4 * 38, 4) != "0000") cv_data[37] = '1';
                if (r.cnvc_jobno.Substring(4 * 39, 4) != "0000") cv_data[38] = '1';
                if (r.cnvc_jobno.Substring(4 * 40, 4) != "0000") cv_data[39] = '1';
                if (r.cnvc_jobno.Substring(4 * 41, 4) != "0000") cv_data[40] = '1';
                if (r.cnvc_jobno.Substring(4 * 42, 4) != "0000") cv_data[41] = '1';

                if (r.cnvc_jobno.Substring(4 * 43, 4) != "0000") cv_data[43] = '1';
                if (r.cnvc_jobno.Substring(4 * 44, 4) != "0000") cv_data[48] = '1';
                if (r.cnvc_jobno.Substring(4 * 45, 4) != "0000") cv_data[45] = '1';
                if (r.cnvc_jobno.Substring(4 * 46, 4) != "0000") cv_data[46] = '1';

                for (int i = 0; i < 5; i++)
                {
                    string ls_hogi = (i + 1).ToString("00");
                    var s = db.ExecuteQuery<tbscrc>("Select * from tbscrc where scrc_no ={0}", ls_hogi).SingleOrDefault();

                    sc_comm[i] = Convert.ToChar(s.scrc_comm);
                    if (s.scrc_onln != "1") sc_onln[i] = '0';
                    if (s.scrc_posi != "") sc_posi[i] = s.scrc_posi;
                    if (s.scrc_jno != "") sc_data[i] = sc_data[i] = '1';
                    if (s.scrc_palt == "1") sc_palt[i] = '1';
                    if (s.scrc_eror != "0") sc_eror[i] = '1';
                    if (s.scrc_stat == "0001") sc_redy[i] = '1';
                    if (s.scrc_stop == "1") sc_stop[i] = '1';
                }
            }
        }
        public  T[] Fill<T>(T initialValue, int length)
        {
            T[] result = new T[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = initialValue;
            }
            return result;
        }

        #region ---- variavle definition ------------------

        string is_pltno = "";
        string is_cvmsg = "";
        string is_flag = "";


        //***************************************************************
        // SCC INPT-SIGNAL
        //***************************************************************
        string scc_mode, scc_io, scc_onln, scc_pwron;
        string scc_stat, scc_palt, scc_posi, scc_eror, scc_ecod;
        string scc_stop, scc_iuse, scc_ouse;
        string scc_lstk, scc_pltn, scc_jno, scc_indx, scc_fstn, scc_tstn, scc_xmov;
        string scc_mesg, scc_chdt, scc_comm;

        char[] sc_onln = new char[5];
        char[] sc_palt = new char[5];
        char[] sc_data = new char[5];
        char[] sc_eror = new char[5];
        char[] sc_stat = new char[5];
        char[] sc_redy = new char[5];
        char[] sc_stop = new char[5];
        string[] sc_posi = new string[5];
        char[] sc_comm = new char[5];
        int li_posi = 0;

        //***************************************************************
        // CNVC INPT-SIGNAL
        //***************************************************************
        char[] cv_onln = new char[8];
     
        private void b31_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[31]);
        }
       
        private void b21_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[10]);
        }

        private void b22_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[11]);
        }

        private void b23_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[25]);
        }

        private void b24_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[26]);
        }

        private void b25_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[27]);
        }

        private void b26_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[28]);
        }
        

        private void b27_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[29]);
        }

        private void b28_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[30]);
        }

        private void b11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[15]);
        }

        private void b32_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[32]);
        }
        // ST-JOBNO ==> 1 - 10,21,22,43,45,50, 11,12,13,14,15,16,17,18,19,20,23,24,25,26,27,28,31,32,33,34,35,36,37,38,39,40,41,42  
        // araay count=        11 12 13 14 15  16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43  
        private void b33_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[33]);
        }

        private void b34_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[34]);
        }

        private void b35_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[35]);
        }

        private void b36_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[36]);
        }

        private void b37_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[37]);
        }

        private void b38_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[38]);
        }

        private void b39_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[39]);
        }

        private void b40_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[40]);
        }

        private void b41_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[41]);
        }

        private void b42_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[42]);
        }

        private void b43_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[12]);
        }

        private void b46_Click(object sender, EventArgs e)
        {

        }

        private void b44_Click(object sender, EventArgs e)
        {

        }

        private void b45_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[13]);
        }

        private void b49_Click(object sender, EventArgs e)
        {

        }

        private void b50_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[14]);
        }

        private void b01_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[0]);
        }

        private void b02_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[1]);
        }

        private void b03_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[2]);
        }

        private void b04_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[3]);
        }

        private void b05_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[4]);
        }

        private void b06_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[5]);
        }

        private void b07_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[6]);
        }

        private void b08_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[7]);
        }

        private void b09_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[8]);
        }

        private void b10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[9]);
        }

        private void b12_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[16]);
        }

        private void b13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[17]);
        }

        private void b14_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[18]);
        }

        private void b15_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[19]);
        }

        private void b16_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[20]);
        }

        private void b17_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[21]);
        }

        private void b18_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[22]);
        }

        private void b19_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[23]);
        }

        private void b20_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cv_jno[24]);
        }

        // ST-JOBNO ==> 1 - 10,21,22,43,45,50, 11,12,13,14,15,16,17,18,19,20,23,24,25,26,27,28,31,32,33,34,35,36,37,38,39,40,41,42  
        // araay count=        11 12 13 14 15  16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43  

        char[] cv_eror = new char[8];
        char[] cv_palt = new char[50];
        char[] cv_irdy = new char[5];
        char[] cv_ordy = new char[5];
        char[] cv_data = new char[50];
        string[] cv_jno = new string[47];

        char[] cv_op_onof = new char[8];
        char[] cv_op_eror = new char[8];
        char[] cv_buf_palt = new char[50];
        string cv_21_rqst, cv_22_rqst, cv_remote, cv_24_rqst;
        char[] cv_ist_redy = new char[5];     // st= 1,3,5,7,9
        char[] cv_ist_palt = new char[5];     // st= 1,3,5,7,9
        char[] cv_ost_redy = new char[5];     // st= 2,4,6,8,10
        char[] cv_ost_palt = new char[5];     // st= 2,4,6,8,10
        string[] cv_chdt = new string[6];      
        string[] cv_job_no = new string[47];  // st= 1 - 10,21,22,43,45,50 
        string cv_stop, cv_comm;

        #endregion
    }

    public class tbcnvcq
    {
        public string cnvc_mode { get; set; }
        public string cnvc_ch01 { get; set; }

        public string cnvc_ch02 { get; set; }
        public string cnvc_ch03 { get; set; }
        public string cnvc_ch04 { get; set; }
        public string cnvc_ch05 { get; set; }
        public string cnvc_ch06 { get; set; }
        public string cnvc_op_onof { get; set; }
        public string cnvc_op_eror { get; set; }
        public string cnvc_job_no { get; set; }
        public string cnvc_jobno { get; set; }
        public string cnvc_buf_palt { get; set; }
        public string cnvc_ist_redy { get; set; }
        public string cnvc_ist_palt { get; set; }
        public string cnvc_ost_redy { get; set; }
        public string cnvc_ost_palt { get; set; }
        public string cnvc_21_rqst { get; set; }
        public string cnvc_22_rqst { get; set; }
        public string cnvc_remote { get; set; }
        public string cnvc_stop { get; set; }
        public string cnvc_comm { get; set; }
        public string cnvc_24_rqst { get; set; }
    }


}
