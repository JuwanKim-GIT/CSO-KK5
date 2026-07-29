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
    public partial class FrmMichng : Form
    {
        #region --- MDI Child ----------------
        private static FrmMichng _instance;
        public static FrmMichng Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmMichng();
                else _instance.WindowState = FormWindowState.Normal;

                return _instance;
            }
        }
        private void FrmMichng_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion

        #region - Sql statement 
        string sqlm = @"  SELECT docnum,   
                                 credat,   
                                 cretim, 
                                (substring(credat,1,4) + '-' +  substring(credat,5,2) + '-' +  substring(credat,7,2) + ' ' +
                                 substring(cretim,1,2) + ':' +  substring(cretim,3,2) + ':' +  substring(cretim,5,2) + ' ') as credt,  
                                 lgnum,   
                                 tanum,   
                                 bwlvs,   
                                 trart,   
                                 bname,   
                                 tapos,   
                                 matnr,   
                                 plant,   
                                 charg,   
                                 bestq,   
                                 sobkz,   
                                 lsonr,   
                                 meins,   
                                 wdatu,   
                                 wenum,   
                                 vltyp,   
                                 vsolm,   
                                 nltyp,   
                                 maktx,   
                                 vfdat,   
                                 lgort,   
                                 rqty,   
                                 fqty,   
                                 flag,   
                                 bname,
                                 io  
                        FROM miwmto  
                       WHERE miwmto.docnum is not null ";

        string sqls = @" SELECT lstk_no,   
                                lstk_io,   
                                lstk_stat,   
                                plti_pltno,   
                                plti_prod,
                                plti_loc,      
                                plti_lot,      
                                plti_bestq,      
                                plti_pdesc,       
                                plti_pksz,     
                                plti_remark,   
                                plti_stok,     
                                plti_rqty,     
                                plti_cycl_date
                         FROM milstk, miplti   
                        Where (lstk_no = plti_lstk)
                          and (lstk_io in ( '0', '' ))
                          and (lstk_stat = '10' )
                          and (plti_stok > 0)
                          and (plti_rqty = 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot = {2})
                          and (plti_bestq = {3})  
                        UNION 
                         SELECT plti_lstk as lstk_no,   
                                '0' as lstk_io,   
                                '10' as lstk_stat,   
                                plti_pltno,   
                                plti_prod,
                                plti_loc,      
                                plti_lot,      
                                plti_bestq,      
                                plti_pdesc,       
                                plti_pksz,     
                                plti_remark,   
                                plti_stok,     
                                plti_rqty,     
                                plti_cycl_date
                         FROM miplti   
                        Where (plti_stok > 0)
                          and (plti_rqty = 0)
                          and (plti_prod = {0})
                          and (plti_loc = {1})  
                          and (plti_lot =  {2})
                          and (plti_bestq = {3})
                          and (plti_pltno = '00000000')
                          and ((plti_lstk like 'Y%') or (plti_lstk like 'F%')  )
                          and (plti_flag = '1')";
        #endregion

        DataGridView dv1, dv2;
        public FrmMichng()
        {
            InitializeComponent();
            FormClosed += FrmMichng_FormClosed;

            dv1 = dataGridView1;
            dv2 = dataGridView2;

            dv1.AutoGenerateColumns = false;
            dv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv1.MultiSelect = false;

            dv2.AutoGenerateColumns = false;
            dv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dv2.MultiSelect = true;

            dv1.ReadOnly = true;
            dv2.ReadOnly = true;

            dv1.CellFormatting += Dv1_CellFormatting;
            dv2.CellFormatting += Dv2_CellFormatting;

            dv1.SelectionChanged += Dv1_SelectionChanged;

            comboBox1.SelectedIndex = 0;

            if(Config.UserLevel != "1") btnspec.Enabled = false;
        }

        private void Dv1_SelectionChanged(object sender, EventArgs e)
        {
            retrieve2();
        }

        private void Dv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (e.Value != null)
                    {
                        string ls = e.Value.ToString();
                        e.Value = ls.Substring(0, 1) + "-" + ls.Substring(1, 2) + "-" + ls.Substring(3, 2) + "-" + ls.Substring(5, 2);
                        e.FormattingApplied = true;
                    }
                }           

            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
            }
        }

        private void Dv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmPltiChg_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void btnqury_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        private void retrieve()
        {
            string modstr = sqlm;

            string date1 = dtDatefrom.Text;
            string date2 = dtDateTo.Text;

            date1 = date1.Replace("-", "");
            date2 = date2.Replace("-", "");

            if (!chkdt.Checked)
            {
                if (date1 != "") modstr = modstr + " and credat >= '" + date1 + "'";
            }
            else
            {
                if (date1 != "") modstr = modstr + " and credat >= '" + date1 + "'";
                if (date2 != "") modstr = modstr + " and credat <= '" + date2 + "'";
            }
            string bwlvs = "";

            if (checkBox1.Checked)
            {
                modstr = modstr + " and bwlvs = '999' ";
                btnspec.Enabled = false;
            }
            else
            {
                btnspec.Enabled = true;
                modstr = modstr + " and bwlvs in ( '309', '321' ) ";

                bwlvs = comboBox1.SelectedItem.ToString();
                if (bwlvs != "ALL") modstr = modstr + " and bwlvs = '" + bwlvs.Substring(0, 3) + "'";
            }

            string docnum = tbDoc.Text.Trim();
            if (docnum != "") modstr = modstr + " and docnum like '" + docnum + "%'";

            string prod = tbmaterial.Text.Trim();
            if (prod != "") modstr = modstr + " and matnr like '" + prod + "%'";

            string charg = tbbatch.Text.Trim();
            if (charg != "") modstr = modstr + " and charg like '" + charg + "%'";

            string loc = tbloc.Text.Trim();
            if (loc != "") modstr = modstr + " and lgort like '" + loc + "%'";

            string pdesc = tbpdesc.Text.Trim();
            if (pdesc != "") modstr = modstr + " and maktx like '%" + pdesc + "%'";

            string user = txtuser.Text;
            if (user != "") modstr = modstr + " and bname like '%" + user + "%'";

            modstr = modstr + " order by credat, cretim, docnum, tanum, tapos ";

            using(DBDataContext db = new DBDataContext(Config.DBCon))
            {
                dv1.DataSource = new SortableBindingList<michnge>(db.ExecuteQuery<michnge>(modstr).ToList());
                //dv1.DataSource = db.ExecuteQuery<michnge>(modstr).ToList();

                dv1.TopLeftHeaderCell.Value = dv1.RowCount.ToString();
                dv1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void retrieve2()
        {
            if (dv1.SelectedRows.Count <= 0)
            {
                dv2.DataSource = null;
                return;
            }
            string prod = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string loc = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string lot = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["bestq"].Value.ToString();

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                var q = db.ExecuteQuery<mipltiChange>(sqls, prod, loc, lot, bestq).ToList();
                dv2.DataSource = q;

                dv2.TopLeftHeaderCell.Value = dv2.RowCount.ToString();
                dv2.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count == 0) return;
            if (dv2.SelectedRows.Count == 0) return;

            if (MessageBox.Show("재고변경하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            if (tapos != 1)
            {
                MessageBox.Show("홀수행을 선택하세요");
                return;
            }
            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string bwlvs = dv1.SelectedRows[0].Cells["bwlvs"].Value.ToString();
            if (bwlvs != "309" && bwlvs != "321")
            {

                return;
            }
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["bestq"].Value.ToString();
            decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            decimal fqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["fqty"].Value.ToString());
            if (fqty >= vsolm) return;

            int indx = dv1.CurrentRow.Index + 1;           

            string lgort2 = dv1.Rows[indx].Cells["lgort"].Value.ToString();
            string charg2 = dv1.Rows[indx].Cells["charg"].Value.ToString();
            string bestq2 = dv1.Rows[indx].Cells["bestq"].Value.ToString();

            int ret = 0;
            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.Connection.open();
                using(db.Transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        if (lgort != lgort2)
                        {
                            ret = db.p_pltichng_lgort(docnum, tanum, tapos, matnr, lgort, charg, bestq, lgort2, vsolm - fqty);
                            if (ret == 1) db.Transaction.Commit();
                            else db.Transaction.Rollback();
                        }

                        if (charg != charg2)
                        {
                            ret = db.p_pltichng_charg(docnum, tanum, tapos, matnr, lgort, charg, bestq, charg2, vsolm - fqty);
                        }
                        if (ret == 1) db.Transaction.Commit();
                        else db.Transaction.Rollback();

                        if (bestq != bestq2)
                        {
                            ret = db.p_pltichng_bestq(docnum, tanum, tapos, matnr, lgort, charg, bestq, bestq2, vsolm - fqty, bwlvs);
                            if (ret == 1) db.Transaction.Commit();
                            else db.Transaction.Rollback();
                        }
                    }
                    catch (Exception E) { db.Transaction.Rollback(); MessageBox.Show(E.Message); }
                }
            }              
            if (ret == -1) MessageBox.Show("재고상태 변함...!");
            if (ret == -2) MessageBox.Show("재고가 없읍니다...!");
            if (ret == -3) MessageBox.Show("오더상태 변함...!");
            if (ret == -100) MessageBox.Show("기록실패...!");
            if (ret == 0) MessageBox.Show("DB Error...!");
            if (ret == 1) MessageBox.Show("제품구분 변경 ok...!");
            retrieve();

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  
        private void btnspec_Click(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            if (dv2.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("파렛트지정 재고 변경하시겠읍니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());
            if (tapos != 1)
            {
                MessageBox.Show("홀수행을 선택하세요");
                return;
            }

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            string matnr = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();
            string bwlvs = dv1.SelectedRows[0].Cells["bwlvs"].Value.ToString();
            if (bwlvs == "999")
            {
                MessageBox.Show("알수없는 유형입니다");
                return;
            }
            string lgort = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
            string charg = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
            string bestq = dv1.SelectedRows[0].Cells["bestq"].Value.ToString();
            decimal vsolm = Convert.ToDecimal(dv1.SelectedRows[0].Cells["vsolm"].Value.ToString());
            decimal fqty = Convert.ToDecimal(dv1.SelectedRows[0].Cells["fqty"].Value.ToString());

            if (fqty >= vsolm) return;

            decimal cqty = vsolm - fqty;
            decimal? uqty = 0;

            int indx = dv1.CurrentRow.Index + 1;

            int tapos2 = Convert.ToInt32(dv1.Rows[indx].Cells["tapos"].Value.ToString());
            if (tapos2 != 2)
            {
                MessageBox.Show("두번째는 짝수행이어야 합니다");
                return;
            }
            string docnum2 = dv1.Rows[indx].Cells["docnum"].Value.ToString();
            decimal tanum2 = Convert.ToDecimal(dv1.Rows[indx].Cells["tanum"].Value.ToString());
            string matnr2 = dv1.Rows[indx].Cells["matnr"].Value.ToString();

            if (docnum != docnum2)
            {
                MessageBox.Show("아래 위 docnum 가 틀립니다" );
                return;
            }
            if (tanum != tanum2)
            {
                MessageBox.Show("아래 위 오더번호가 틀립니다");
                return;
            }
            if (matnr != matnr2)
            {
                MessageBox.Show("아래 위 제품이 틀립니다");
                return;
            }

            string lgort2 = dv1.Rows[indx].Cells["lgort"].Value.ToString();
            string charg2 = dv1.Rows[indx].Cells["charg"].Value.ToString();
            string bestq2 = dv1.Rows[indx].Cells["bestq"].Value.ToString();

            int ret = 0;
            int lp = 0;
            string pltno = "";

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                List<DataGridViewRow> rr = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dv2.SelectedRows)
                {
                    rr.Insert(0, r);
                }

                db.Connection.open();                            
                foreach (DataGridViewRow r in rr)
                {
                    pltno = r.Cells["plti_pltno"].Value.ToString();
                    uqty = 0;

                    if (lgort != lgort2)
                    {
                        using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                ret = db.p_pltichng_lgort_spec2(docnum, tanum, tapos, bwlvs, matnr, lgort, charg, bestq, lgort2, cqty, pltno, ref uqty);
                                if (ret == 1)
                                {
                                    db.Transaction.Commit(); lp++;
                                    cqty = cqty - (uqty.HasValue ? (decimal)uqty : 0);
                                    if (cqty <= 0) break; 
                                }
                                else { db.Transaction.Rollback(); break; }
                            }
                            catch (Exception E)
                            {
                                ret = -101;
                                db.Transaction.Rollback();
                                break;
                            }
                        }
                    }
                    if (charg != charg2)
                    {
                        using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                ret = db.p_pltichng_charg_spec2(docnum, tanum, tapos, bwlvs, matnr, lgort, charg, bestq, charg2, cqty, pltno, ref uqty);
                                if (ret == 1)
                                {
                                    db.Transaction.Commit(); lp++;
                                    cqty = cqty - (uqty.HasValue ? (decimal)uqty : 0);
                                    if (cqty <= 0) break;
                                }
                                else { db.Transaction.Rollback(); break; }
                            } catch (Exception E)
                            {
                                ret = -101;
                                db.Transaction.Rollback(); break;
                            }
                        }
                    }
                    if (bestq != bestq2)
                    {
                        using (db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                ret = db.p_pltichng_bestq_spec2(docnum, tanum, tapos, bwlvs, matnr, lgort, charg, bestq, bestq2, cqty, pltno, ref uqty);
                                if (ret == 1)
                                {
                                    db.Transaction.Commit(); lp++;
                                    cqty = cqty - (uqty.HasValue ? (decimal)uqty : 0);
                                    if (cqty <= 0) break;
                                }
                                else { db.Transaction.Rollback(); break; }
                            }
                            catch (Exception E)
                            {
                                ret = -101;
                                db.Transaction.Rollback(); break;
                            }
                        }
                    }
                }
                db.Connection.Close();
            }           
            if (ret == -1) MessageBox.Show("상위 레코드 상태 변함...!");
            if (ret == -2) MessageBox.Show("Source 재고상태 변함...!");
            if (ret == -3) MessageBox.Show("Source 재고상태 변함2...!");
            if (ret == -4) MessageBox.Show("Target 재고상태 아님...!  입출 예약확인");
            if (ret == -100) MessageBox.Show("이력 기록 실패 Dup");
            if (ret == -101) MessageBox.Show("target 재고 삽입 Error...!");
            if (ret == 0) MessageBox.Show("변경된게 없음...!");
            MessageBox.Show(lp.ToString() + " 개의 행이 변경되었읍니다");
            retrieve();
            
        }

        private void tbDoc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbDoc.Text = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
        }

        private void tbmaterial_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbmaterial.Text = dv1.SelectedRows[0].Cells["matnr"].Value.ToString();

        }

        private void tbbatch_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbbatch.Text = dv1.SelectedRows[0].Cells["charg"].Value.ToString();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.RowPostPaint(sender, e);
        }

        private void tbpdesc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbpdesc.Text = dv1.SelectedRows[0].Cells["maktx"].Value.ToString();
        }

        private void tbloc_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            tbloc.Text = dv1.SelectedRows[0].Cells["lgort"].Value.ToString();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {            
            if (dv1.SelectedRows.Count <= 0) return;

            if (MessageBox.Show("삭제하겠읍니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            string docnum = dv1.SelectedRows[0].Cells["docnum"].Value.ToString();
            decimal tanum = Convert.ToDecimal(dv1.SelectedRows[0].Cells["tanum"].Value.ToString());
            int tapos = Convert.ToInt32(dv1.SelectedRows[0].Cells["tapos"].Value.ToString());

            using (DBDataContext db = new DBDataContext(Config.DBCon))
            {
                db.ExecuteCommand(@"delete from miwmto where docnum = {0} and tanum = {1} and tapos = {2} ", docnum, tanum, tapos);
            }
            retrieve();            
        }

        private void txtuser_DoubleClick(object sender, EventArgs e)
        {
            if (dv1.SelectedRows.Count <= 0) return;
            txtuser.Text = dv1.SelectedRows[0].Cells["bname"].Value.ToString();
        }

        private void chkdt_CheckedChanged(object sender, EventArgs e)
        {
            dtDateTo.Enabled = chkdt.Checked;
        }             
    }
    public class michnge
    {
        public string credt { get; set; }
        public string docnum { get; set; }
        public decimal tanum { get; set; }
        public int tapos { get; set; }
        public string bwlvs { get; set; }
        public string matnr { get; set; }
        public string maktx { get; set; }
        public string lgort { get; set; }
        public string charg { get; set; }
        public string bestq { get; set; }
        public string vltyp { get; set; }
        public string nltyp { get; set; }
        public string trart { get; set; }
        public decimal vsolm { get; set; }
        public string sobkz { get; set; }
        public string lsonr { get; set; }
        public string wdatu { get; set; }
        public string wenum { get; set; }
        public string vfdat { get; set; }
        public string credat { get; set; }
        public string cretim { get; set; }
        public decimal rqty { get; set; }
        public decimal fqty { get; set; }
        public string flag { get; set; }
        public string hdate { get; set; }
        public string htime { get; set; }
        public string bname { get; set; }

        public string hist_dt { get; set; }
    }

    public class mipltiChange
    {
        public string lstk_no { get; set; }
        public string plti_pltno { get; set; }
        public string plti_prod { get; set; }
        public string plti_oprod { get; set; }
        public string plti_pdesc { get; set; }
        public string plti_loc { get; set; }
        public string plti_lot { get; set; }
        public string plti_bestq { get; set; }
        public decimal? plti_pksz { get; set; }
        public string plti_remark { get; set; }
        public decimal plti_stok { get; set; }
        public decimal plti_rqty { get; set; }
        public decimal plti_sqty { get; set; }

        public string plti_cycl_date { get; set; }
        public string plti_idate { get; set; }
        public string plti_itime { get; set; }
        public string plti_flag { get; set; }
        public string plti_icust { get; set; }
        public string plti_label { get; set; }
    }
}
