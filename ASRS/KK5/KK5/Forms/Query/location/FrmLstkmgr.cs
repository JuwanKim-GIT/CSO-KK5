using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;


namespace KK5
{
    public partial class FrmLstkmgr : Form
    {
        #region --- MDI Child ----------------
        private static FrmLstkmgr _instance;
        public static FrmLstkmgr Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLstkmgr();
                   
                return _instance;
            }
        }
        private void FrmLstkmgr_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
       
        DataGridView dv1, dv2;
        string org_sel = "  SELECT lstk_no, lstk_hogi, lstk_use, lstk_io, lstk_stat, lstk_type, lstk_bk, lstk_by, lstk_lv, lstk_srch FROM milstk where lstk_no is not null ";
        string org_sel2 = @"  SELECT lstk_no, lstk_hogi, lstk_use, lstk_io, lstk_stat, lstk_type, lstk_bk, lstk_by, lstk_lv, lstk_srch 
                              FROM milstk left outer join miplti on milstk.lstk_no = miplti.plti_lstk where plti_pltno is null and lstk_stat = '10' ";

        string org_sel3 = @"  SELECT lstk_no, lstk_hogi, lstk_use, lstk_io, lstk_stat, lstk_type, lstk_bk, lstk_by, lstk_lv, lstk_srch 
                              FROM milstk inner join miplti on milstk.lstk_no = miplti.plti_lstk where plti_pltno is not null and lstk_stat = '00' ";

        public FrmLstkmgr()
        {
            InitializeComponent();

            this.Size = new Size(1270, 900);
            dv1 = dataGridView1;
            dv1.CellFormatting += DataGridView1_CellFormatting;

            dv2 = dataGridView2;
            dataGridView2.CellFormatting += DataGridView2_CellFormatting;
            this.FormClosed += FrmLstkmgr_FormClosed;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.SelectionChanged += dataGridView1_SelectionChanged;

            if(Config.UserLevel != "1")
            {
                btnCreCell.Enabled = false;
                btndblout.Enabled = false;
                btnDelCell.Enabled = false;
                btnlstkemty.Enabled = false;
                btnout.Enabled = false;
                btndblout.Enabled = false;
                btnTypeChg.Enabled = false;
                btnuse.Enabled = false;
            }

        }   
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
                dv2.DataSource = db.ExecuteQuery<mipltiq>(@"select * from miplti where plti_lstk = {0} ", lstk).ToList();
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    e.Value = s.Substring(0, 1) + "-" + s.Substring(1, 2) + "-" + s.Substring(3, 2) + "-" + s.Substring(5, 2);
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    if (s == "0") e.Value = "금지";
                    else e.Value = "";
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    if (s == "0") e.Value = "";
                    if (s == "M") e.Value = "이동";
                    if (s == "I") e.Value = "입고";
                    if (s == "$") e.Value = "출고";
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString();
                    if (s == "00") e.Value = "빈셀";
                    if (s == "10") e.Value = "재고";
                    if (s == "$R") e.Value = "출고예약";
                    if (s == "$X") e.Value = "출고실행";
                    if (s == "$Z") e.Value = "출고완료";
                    if (s == "$E") e.Value = "공출고";
                    if (s == "IR") e.Value = "입고예약";
                    if (s == "IX") e.Value = "입고실행";
                    if (s == "IZ") e.Value = "입고완료";
                    if (s == "ID") e.Value = "이중입고";

                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == 5)
            {
                if (e.Value != null)
                {
                    string s = e.Value.ToString().Trim();
                    if (s == "0") e.Value = "일반";
                    if (s == "") e.Value = "일반";
                    if (s == "1") e.Value = "위험물";
                    if (s == "2") e.Value = "Thinner";
                    if (s == "3") e.Value = "유독물";

                    e.FormattingApplied = true;
                }
            }
        }
        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void FrmLstkmgr_Load(object sender, EventArgs e)
        {
        
            dv1 = dataGridView1; dv1.AutoGenerateColumns = false;
            dv2 = dataGridView2; dv2.AutoGenerateColumns = false;
            dv1.RowPostPaint += Common.RowPostPaint;
            dv2.RowPostPaint += Common.RowPostPaint;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            retrieve();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            if (rbnoitem.Checked)
            {
                retrieve2();
            }
            else if (rbitemerr.Checked)
                    {
                retrieve3();
            }
            else retrieve();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            string uuse = "0";
            string msg = "";
            int lp = 0;

            if (dv1.SelectedRows.Count == 0) return;
            if (comboBox1.Text.Substring(0, 2) != "A:") return;

            using (Frmlstkuse_p p = new Frmlstkuse_p())
            {
                p.ShowDialog();
                if (p.DialogResult != DialogResult.OK) return;

                if (p.radioButton1.Checked) uuse = "0";
                if (p.radioButton2.Checked) uuse = "1";
            }

            Cursor.Current = Cursors.WaitCursor;
            string lstk;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    if (lstk.Substring(0, 1) != "A")
                    {
                        continue;
                    }
                    int rc = d.ExecuteCommand("update milstk set lstk_use = '" + uuse + "' where lstk_no = '" + lstk + "'");
                    lp++;
                    r.Cells["lstk_use"].Value = uuse;
               }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show(lp.ToString() + " 개의 cell입 변경 되었읍니다.");
        }

        private void btnTypeChg_Click(object sender, EventArgs e)
        {
            string uuse = "0";
            string msg = "";
            string type = "0";
            int lp = 0;

            if (dv1.SelectedRows.Count == 0) return;
            if (comboBox1.Text.Substring(0, 2) != "A:")
            {
                MessageBox.Show("자동창고만 변경가능합니다!");
                return;
            }
            using (FrmLstkType_p p = new FrmLstkType_p())
            {
                p.ShowDialog();
                if (p.DialogResult != DialogResult.OK) return;
                type = p.comboBox1.SelectedIndex.ToString("0");
            }

            Cursor.Current = Cursors.WaitCursor;
            string lstk;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                foreach (DataGridViewRow r in dv1.SelectedRows)
                {
                    lstk = r.Cells["lstk_no"].Value.ToString();
                    if (lstk.Substring(0, 1) != "A")
                    {
                        continue;
                    }
                    int rc = d.ExecuteCommand("update milstk set lstk_type = '" + type + "' where lstk_no = '" + lstk + "'");
                    if (rc > 0)
                    {
                        r.Cells["lstk_type"].Value = type;
                        lp++;
                    }
                }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show(lp.ToString() + " 개의 cell입 변경 되었읍니다.");
            
        }

        private void btnCreCell_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 1) return;

            string ls = "";
            using (FrmLstkInsertYard_p p = new FrmLstkInsertYard_p())
            {
                p.ShowDialog();
                if (p.DialogResult != DialogResult.OK) return;
                ls = p.maskedTextBox1.Text;
                ls = ls.Replace("-", "");
            }
                

            if (ls.Length != 6)
            {
                MessageBox.Show("위치가 잘못되었읍니다.");
                return;
            }
            int rt = -1;
            if (!int.TryParse(ls, out rt))
            {
                MessageBox.Show("위치가 잘못되었읍니다.");
                return;
            }
            string lstk_no = "Y" + ls;
            string lstk_bk = lstk_no.Substring(1, 2);
            string lstk_by = lstk_no.Substring(3, 2);
            string lstk_lv = lstk_no.Substring(5, 2);
            string lstk_srch = lstk_lv + lstk_by + lstk_bk;
            string sql = @"insert into milstk ( lstk_no, lstk_bk, lstk_by,  lstk_lv, lstk_hogi,  lstk_srch, lstk_use, lstk_io, lstk_stat, lstk_type, lstk_flag )
                           values ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} ) ";
            try
            {
                using (DBDataContext d = new DBDataContext(Config.DBCon))
                {
                    int ret = d.ExecuteCommand(sql, lstk_no, lstk_bk, lstk_by, lstk_lv, "0", lstk_srch, "1", "0", "00", "0", "1");
                    if (ret > 0)
                    {
                        string[] r = new string[] { lstk_no, lstk_bk, lstk_by, lstk_lv, "0", lstk_srch, "1", "0", "00", "0", "1" };
                        milstkq m = new milstkq();
                        m.lstk_no = lstk_no;
                        m.lstk_bk = lstk_bk;
                        m.lstk_by = lstk_by;
                        m.lstk_lv = lstk_lv;
                        m.lstk_bk = lstk_bk;
                        m.lstk_hogi = "0";
                        m.lstk_srch = lstk_srch;
                        m.lstk_use = "1";
                        m.lstk_io = "0";
                        m.lstk_stat = "00";
                        m.lstk_type = "0";
                        m.lstk_flag = "1";
                        
                        BindingList<milstkq> b = (BindingList<milstkq>)dv1.DataSource;
                        CurrencyManager bc = (CurrencyManager)this.BindingContext[dv1.DataSource];
                        
                        b.Add(m);
                        bc.Position = bc.Count - 1;
                        dv1.FirstDisplayedScrollingRowIndex = dv1.Rows.Count - 1;                      
                        
                    }
                }
                comboBox1.SelectedIndex = 1;

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + Environment.NewLine + Environment.NewLine + "야적등록 실패입니다.");
            }
        }

        private void btnDelCell_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;

            string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            if (lstk.Substring(0, 1) != "Y") return;
            if (lstk == "Y000000") return;

            if (MessageBox.Show("선택된 셀을 삭제하시겠읍니까?", "셀삭제확인", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            int rc = 0;
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                rc = db.ExecuteCommand("delete from milstk where lstk_no = {0} and 0 = (select count(*) from miplti where plti_lstk = {0}) ", lstk);
                if (rc > 0)
                {
                    dv1.Rows.RemoveAt(dv1.SelectedRows[0].Index);
                }
            }
            if (rc == 0) MessageBox.Show("셀삭제 실패...재고 존재!");
            if (rc > 0) MessageBox.Show("셀삭제 성공...!");
            
        }


   
        private void btndblout_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한 행만 선택하세요!");
                return;
            }
            
            string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            if (lstk.Substring(0, 1) != "A") return;

            string stat = dv1.SelectedRows[0].Cells["lstk_stat"].Value.ToString();
            if (stat != "ID") return;

            if (MessageBox.Show("이중입고셀 출고해보겠읍니까?", "출고 확인",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            string pltno = "";
            string bonof = "";
            string beror = "";
            string lshogi = "";
            char[] onln = new char[5] { '0', '0', '0', '0', '0' };
            char[] stop = new char[5] { '0', '0', '0', '0', '0' };
            string hogi = "";
            int rc = 0;
            using (DBDataContext d = new DBDataContext(Config.DBCon))
            {
                var cvc = d.ExecuteQuery("Select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01' ").SingleOrDefault();
                if (cvc == null) return;
                bonof = cvc.cnvc_op_onof;
                beror = cvc.cnvc_op_eror;

                for(int i = 0; i < 5; i++)
                {
                    lshogi = (i+1).ToString("00");
                    var sc = d.ExecuteQuery("Select scrc_onln, scrc_stop from tbscrc where scrc_no = '" + lshogi + "'").SingleOrDefault();
                    if (sc == null) return;
                    onln[i] = sc.scrc_onln;
                    stop[i] = sc.scrc_stop;
                }

                rc = d.p_get_hogi(lstk, ref hogi);
                int lh = Convert.ToInt32(hogi);

                if (onln[lh - 1] != '1')
                {
                    if (MessageBox.Show("크레인 No:" + lh.ToString("0") + " 원격이 아닙니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                }
                if (stop[lh - 1] != '0')
                {
                    if (MessageBox.Show("크레인 No:" + lh.ToString("0") + " 입출금지입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                }
                if (bonof.Substring(lh, 1) != "0")
                {
                    if (MessageBox.Show("OP 판넬 No:" + lh.ToString("0") + " 수동입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                }
                if (beror.Substring(lh, 1) != "0")
                {
                    if (MessageBox.Show("OP 판넬 No:" + lh.ToString("0") + " 에러입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                }
                string indx_hogi = "";
                string indx_fstn = "";
                string indx_jno = "";
                string indx_indx = "";
                
                int st = 0;
                d.Connection.open();
                using (d.Transaction = d.Connection.BeginTransaction())
                {
                    rc = d.ExecuteCommand("update milstk set lstk_io = 'M', lstk_stat = '$X' where lstk_no = '" + lstk + "' and lstk_stat = 'ID' ");
                    if (rc > 0)
                    {
                        rc = d.p_getpltno(ref pltno);
                        if (rc == 1)
                        {
                            rc = d.ExecuteCommand(@"INSERT INTO miplti 
                                                (plti_pltno, plti_lstk,     plti_prod,  plti_loc,   plti_lot,   plti_bestq, 
                                                 plti_pksz,  plti_remark,   plti_icust, plti_stok,  plti_rqty,  plti_cycl_date, 
                                                 plti_idate, plti_itime,    plti_flag,  plti_label, plti_oprod, plti_pdesc )
                                        values ( {0},        {1},           'X',        'SKUD',     '',         '',
                                                 0,          '',            '',         0,          0,          '',
                                                 '',         '',            '1',        '0',        '',         '' ) ", pltno, lstk);

                            rc = d.p_get_hogi(lstk, ref indx_hogi);
                            if (rc == 1)
                            {
                                indx_fstn = (Convert.ToInt32(indx_hogi) * 2).ToString("00");

                                rc = d.p_get_indx_jno("3", ref indx_jno);
                                if (rc == 1)
                                {
                                    indx_indx = indx_jno.Substring(indx_jno.Length - 4, 4);

                                    rc = d.ExecuteCommand(@"INSERT INTO tbindx  
                                                      ( indx_jno,    indx_indx,   indx_gubn,   indx_jio,   
           	                                            indx_hogi,   indx_fstn,   indx_tstn,   indx_pltn,   
        	                                            indx_lstk,   indx_xmov,   indx_edat,   indx_sflg,   indx_uflg ) 
                                                values ( {0},        {1},         'A',         '$',  
                                                         {2},        {3},         '43',        {4},
                                                         {5},        'M',         '',          'W',         '0' ) ",
                                                         indx_jno, indx_indx,
                                                         indx_hogi, indx_fstn, pltno,
                                                         lstk);
                                    d.Transaction.Commit();
                                }
                                else
                                {
                                    d.Transaction.Rollback();
                                    st = 4;
                                }
                            }
                            else
                            {
                                d.Transaction.Rollback();
                                st = 3;
                            }
                        }
                        else
                        {
                            d.Transaction.Rollback();
                            st = 2;
                        }
                    }
                    else
                    {
                        d.Transaction.Rollback();
                        st = 1;
                    }
                } //using end transa
                d.Connection.Close();

                if (rc != 1) MessageBox.Show("상태변함...!" + rc.ToString() + "-" + st.ToString());
                if (rc == 1) MessageBox.Show("성공 ...!");
            } // using db
            retrieve();
        }

        private void btnout_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한 행만 선택하세요!");
                return;
            }
  
            string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            if (lstk.Substring(0, 1) != "A") return;

            if (MessageBox.Show("그냥 출고해보겠읍니까?", "데이타무관 출고확인",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            //string stat = dv1.SelectedRows[0].Cells["lstk_stat"].Value.ToString();
            //if (stat != "10") return;

            string pltno = "";
            string bonof = "";
            string beror = "";
            string lshogi = "";
            string[] onln = new string[5] { "0", "0", "0", "0", "0" };
            string[] stop = new string[5] { "0", "0", "0", "0", "0" };
            string hogi = "";
            int rc = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {               
                var cvc = db.ExecuteQuery("Select cnvc_op_onof, cnvc_op_eror from tbcnvc where cnvc_mode = '01' ").SingleOrDefault();
                if (cvc == null) return;
                bonof = cvc.cnvc_op_onof;
                beror = cvc.cnvc_op_eror;

                for (int i = 0; i < 5; i++)
                {
                    lshogi = (i + 1).ToString("00");
                    var sc = db.ExecuteQuery("Select scrc_onln, scrc_stop from tbscrc where scrc_no = '" + lshogi + "'").SingleOrDefault();
                    if (sc == null) return;
                    onln[i] = sc.scrc_onln;
                    stop[i] = sc.scrc_stop;
                }

                rc = db.p_get_hogi(lstk, ref hogi);
                int lh = Convert.ToInt32(hogi);

                //삭제 요청 황현우 2020 0217
                //if (onln[lh - 1] != "1")
                //{
                //    if (MessageBox.Show("크레인 No:" + lh.ToString("0") + " 원격이 아닙니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                //}
                //if (stop[lh - 1] != "0")
                //{
                //    if (MessageBox.Show("크레인 No:" + lh.ToString("0") + " 입출금지입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                //}
                //if (bonof.Substring(lh - 1, 1) != "0")
                //{
                //    if (MessageBox.Show("OP 판넬 No:" + lh.ToString("0") + " 수동입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                //}
                //if (beror.Substring(lh - 1, 1) != "0")
                //{
                //    if (MessageBox.Show("OP 판넬 No:" + lh.ToString("0") + " 에러입니다" + Environment.NewLine + "계속하시겠읍니까?", "확인",
                //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                //}

                string indx_hogi = "";
                string indx_fstn = "";
                string indx_jno = "";
                string indx_indx = "";
                pltno = "99999999";
                int st = 0;

                db.Connection.open();
                using (db.Transaction = db.Connection.BeginTransaction())
                {
                    rc = db.p_get_hogi(lstk, ref indx_hogi);
                    if (rc == 1)
                    {
                        indx_fstn = (Convert.ToInt32(indx_hogi) * 2).ToString("00");

                        rc = db.p_get_indx_jno("3", ref indx_jno);
                        if (rc == 1)
                        {
                            indx_indx = indx_jno.Substring(indx_jno.Length - 4, 4);

                            rc = db.ExecuteCommand(@"INSERT INTO tbindx  
                                              ( indx_jno,    indx_indx,   indx_gubn,   indx_jio,   
           	                                    indx_hogi,   indx_fstn,   indx_tstn,   indx_pltn,   
        	                                    indx_lstk,   indx_xmov,   indx_edat,   indx_sflg,   indx_uflg ) 
                                        values ( {0},        {1},         'A',         '$',  
                                                 {2},        {3},         '43',        {4},
                                                 {5},        'M',         '',          'W',         '0' ) ",
                                                 indx_jno, indx_indx,
                                                 indx_hogi, indx_fstn, pltno,
                                                 lstk);
                            db.Transaction.Commit();
                        }
                        else
                        {
                            db.Transaction.Rollback();
                            st = 2;
                        }
                    }
                    else
                    {
                        db.Transaction.Rollback();
                        st = 1;
                    }
                } //using end transa
                db.Connection.Close();
                if (rc != 1) MessageBox.Show("상태변함...!" + rc.ToString() + "-" + st.ToString());
                if (rc == 1) MessageBox.Show("성공 ...!");
            } // using db
            retrieve();

        }

        private void btnlstkemty_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한 행만 선택하세요!");
                return;
            }
            int saverow = dv1.FirstDisplayedScrollingRowIndex;
            int rowIndex = dv1.CurrentCell.RowIndex;

            string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            if (lstk.Substring(0, 1) != "A") return;

            string stat = dv1.SelectedRows[0].Cells["lstk_stat"].Value.ToString();
            
            //if (dv2.Rows.Count != 0) return;

            if (MessageBox.Show("셀은 재고인데 아래 실제 재고가 없는 경우나 꼬인 셀을 빈셀로 만드시겠읍니까?", "확인", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return; 

            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int r = db.ExecuteQuery<int>(@"select count(*) from miplti where substring(plti_lstk, 1, 1) = 'A' and plti_lstk = {0} ", lstk).SingleOrDefault();
                if (r > 0)
                {
                    db.ExecuteCommand(@"delete from miplti where substring(plti_lstk, 1, 1) = 'A' and plti_lstk = {0}", lstk);
                }
                db.ExecuteCommand("update milstk set lstk_stat = '00', lstk_io = '0' where substring(lstk_no, 1, 1) = 'A' and lstk_no = {0} ", lstk);

                dv1.SelectedRows[0].Cells["lstk_io"].Value = "0";
                dv1.SelectedRows[0].Cells["lstk_stat"].Value = "0";

            }
            retrieve2();
        
        }


        private void retrieve()
        {         
            
            string modstr = org_sel;
            string wh = "A";
            switch (comboBox1.Text.Substring(0, 2))
            {
                case "A:":
                    wh = "A";
                    modstr = modstr + " and substring(lstk_no,1,1) = 'A' ";
                    break;
                case "Y:":
                    wh = "Y";
                    modstr = modstr + " and substring(lstk_no,1,1) = 'Y' ";
                    break;
                case "F:":
                    wh = "F";
                    modstr = modstr + " and substring(lstk_no,1,1) = 'F' ";
                    break;
                default:
                    break;
            }
           

            if (rbuseno.Checked) modstr = modstr + " and lstk_use = '0' ";
            else if (rbuse.Checked) modstr = modstr + " and lstk_use = '1' ";

            if (rbempty.Checked) modstr = modstr + " and lstk_stat = '00' ";
            else if (rbfill.Checked) modstr = modstr + " and lstk_stat <> '00' ";

            string bk1 = mtbfrombk.Text;
            string bk2 = mtbtobk.Text;
            string by1 = mtbfromby.Text;
            string by2 = mtbtoby.Text;
            string lv1 = mtbfromlv.Text;
            string lv2 = mtbtolv.Text;

            if (bk1 != "") modstr = modstr + " and Lstk_bk >= '" + bk1 + "' ";
            if (bk2 != "") modstr = modstr + " and Lstk_bk <= '" + bk2 + "' ";
            if (by1 != "") modstr = modstr + " and Lstk_by >= '" + by1 + "' ";
            if (by2 != "") modstr = modstr + " and Lstk_by <= '" + by2 + "' ";
            if (lv1 != "") modstr = modstr + " and Lstk_lv >= '" + lv1 + "' ";
            if (lv2 != "") modstr = modstr + " and Lstk_lv <= '" + lv2 + "' ";

            string ls = comboBox2.Text.Substring(0, 1);
            if (ls != "4")
                modstr = modstr + " and lstk_type = '" + ls + "' ";

            modstr = modstr + " order by lstk_no ";         
            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                BindingList<milstkq> b = new BindingList<milstkq>(db.ExecuteQuery<milstkq>(modstr).ToList());
                //var q = db.ExecuteQuery<milstkq>(modstr).ToList();
                dv1.DataSource = b;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void rbuseno_CheckedChanged(object sender, EventArgs e)
        {
            rbuseno.ForeColor = Color.Red;
            rbuse.ForeColor = Color.Black;
            rbuseall.ForeColor = Color.Black;
        }

        private void rbuse_CheckedChanged(object sender, EventArgs e)
        {
            rbuseno.ForeColor = Color.Black;
            rbuse.ForeColor = Color.Red;
            rbuseall.ForeColor = Color.Black;
        }

        private void rbuseall_CheckedChanged(object sender, EventArgs e)
        {
            rbuseno.ForeColor = Color.Black;
            rbuse.ForeColor = Color.Black;
            rbuseall.ForeColor = Color.Red;
        }

        private void rbempty_CheckedChanged(object sender, EventArgs e)
        {
            rbempty.ForeColor = Color.Red;
            rbfill.ForeColor = Color.Black;
            rballcell.ForeColor = Color.Black;
            rbnoitem.ForeColor = Color.Black;
            rbitemerr.ForeColor = Color.Black;
        }

        private void rbfill_CheckedChanged(object sender, EventArgs e)
        {

            rbempty.ForeColor = Color.Black;
            rbfill.ForeColor = Color.Red;
            rballcell.ForeColor = Color.Black;
            rbnoitem.ForeColor = Color.Black;
            rbitemerr.ForeColor = Color.Black;
        }

        private void rballcell_CheckedChanged(object sender, EventArgs e)
        {
            rbempty.ForeColor = Color.Black;
            rbfill.ForeColor = Color.Black;
            rballcell.ForeColor = Color.Red;
            rbnoitem.ForeColor = Color.Black;
            rbitemerr.ForeColor = Color.Black;
        }

        private void rbnoitem_CheckedChanged(object sender, EventArgs e)
        {
            rbempty.ForeColor = Color.Black;
            rbfill.ForeColor = Color.Black;
            rballcell.ForeColor = Color.Black;
            rbnoitem.ForeColor = Color.Red;
            rbitemerr.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrencyManager bc = (CurrencyManager)this.BindingContext[dv1.DataSource];
            
            bc.Position--;
        }

        private void rbitemerr_CheckedChanged(object sender, EventArgs e)
        {
            rbempty.ForeColor = Color.Black;
            rbfill.ForeColor = Color.Black;
            rballcell.ForeColor = Color.Black;
            rbnoitem.ForeColor = Color.Black;
            rbitemerr.ForeColor = Color.Red;
        }

        private void retrieve2()
        {
            string modstr = org_sel2;
            string wh = "A";
            if (comboBox1.Text.Substring(0, 2) != "A:" ) return;
                        
            //string bk1 = mtbfrombk.Text;
            //string bk2 = mtbtobk.Text;
            //string by1 = mtbfromby.Text;
            //string by2 = mtbtoby.Text;
            //string lv1 = mtbfromlv.Text;
            //string lv2 = mtbtolv.Text;

            //if (bk1 != "") modstr = modstr + " and Lstk_bk >= '" + bk1 + "' ";
            //if (bk2 != "") modstr = modstr + " and Lstk_bk <= '" + bk2 + "' ";
            //if (by1 != "") modstr = modstr + " and Lstk_by >= '" + by1 + "' ";
            //if (by2 != "") modstr = modstr + " and Lstk_by <= '" + by2 + "' ";
            //if (lv1 != "") modstr = modstr + " and Lstk_lv >= '" + lv1 + "' ";
            //if (lv2 != "") modstr = modstr + " and Lstk_lv <= '" + lv2 + "' ";

      
            modstr = modstr + " order by lstk_no ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<milstkq>(modstr).ToList();
                dv1.DataSource = q;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv1.SelectedRows.Count > 1)
            {
                MessageBox.Show("한 행만 선택하세요!");
                return;
            }
          
            string lstk = dv1.SelectedRows[0].Cells["lstk_no"].Value.ToString();
            if (lstk.Substring(0, 1) != "A") return;                        

            if (MessageBox.Show("셀은 Clear하겠읍니까?", "확인",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                int r = db.ExecuteQuery<int>(@"select count(*) from miplti where substring(plti_lstk, 1, 1) = 'A' and plti_lstk = {0} ", lstk).SingleOrDefault();
                if (r > 0)
                {
                    db.ExecuteCommand(@"delete from miplti where substring(plti_lstk, 1, 1) = 'A' and plti_lstk = {0}", lstk);
                }
                db.ExecuteCommand("update milstk set lstk_stat = '00', lstk_io = '0' where substring(lstk_no, 1, 1) = 'A' and lstk_no = {0} ", lstk);

                dv1.SelectedRows[0].Cells["lstk_io"].Value = "0";
                dv1.SelectedRows[0].Cells["lstk_stat"].Value = "0";

            }
            retrieve2();
        }

        private void retrieve3()
        {
            string modstr = org_sel3;
            string wh = "A";
            if (comboBox1.Text.Substring(0, 2) != "A:") return;
          
            modstr = modstr + " order by lstk_no ";
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<milstkq>(modstr).ToList();
                dv1.DataSource = q;

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
    }
public class milstkq
    {
        public string lstk_no { get; set; }
        public string lstk_bk { get; set; }
        public string lstk_by { get; set; }
        public string lstk_lv { get; set; }
        public string lstk_hogi { get; set; }
        public string lstk_use { get; set; }
        public string lstk_srch { get; set; }
        public string lstk_flag { get; set; }
        public string lstk_io { get; set; }
        public string lstk_stat { get; set; }
        public string lstk_type { get; set; }
    }
    public class mipltiq
    {
        public string lstk_no { get; set; }
        public string lstk_use { get; set; }
        public string lstk_io { get; set; }
        public string lstk_stat { get; set; }
        public string plti_pltno { get; set; }
        public string plti_prod { get; set; }
        public string plti_oprod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal plti_pksz { get; set; }
        public string plti_remark { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_flag { get; set; }
    }

}
