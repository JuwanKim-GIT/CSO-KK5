using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK5
{
    public partial class FrmLstkCelinfo : Form
    {
        #region --- MDI Child ----------------
        private static FrmLstkCelinfo _instance;
        public static FrmLstkCelinfo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmLstkCelinfo();

                return _instance;
            }
        }
        private void FrmLstkCelinfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
        public FrmLstkCelinfo()
        {
            InitializeComponent();
            FormClosed += FrmLstkCelinfo_FormClosed;
        }

        private void FrmLstkCelinfo_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

        }  

        private void btnqry_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string uuse, stat, pltno, bk, bay, lv;
            decimal c11 = 0, c12 = 0, c13 = 0, c14 = 0, c15 = 0, c16 = 0, c17 = 0, c18 = 0, c19 = 0, c20 = 0;
            decimal c21 = 0, c22 = 0, c23 = 0, c24 = 0, c25 = 0, c26 = 0, c27 = 0, c28 = 0, c29 = 0, c30 = 0;
            decimal c31 = 0, c32 = 0, c33 = 0, c34 = 0, c35 = 0, c36 = 0, c37 = 0, c38 = 0, c39 = 0, c40 = 0;
            decimal c41 = 0, c42 = 0, c43 = 0, c44 = 0, c45 = 0, c46 = 0, c47 = 0, c48 = 0, c49 = 0, c50 = 0;
            decimal c51 = 0, c52 = 0, c53 = 0, c54 = 0, c55 = 0, c56 = 0, c57 = 0, c58 = 0, c59 = 0, c60 = 0;
            decimal c61 = 0, c62 = 0, c63 = 0, c64 = 0, c65 = 0, c66 = 0, c67 = 0, c68 = 0, c69 = 0, c70 = 0;
            decimal c71 = 0, c72 = 0, c73 = 0, c74 = 0, c75 = 0, c76 = 0, c77 = 0, c78 = 0, c79 = 0, c80 = 0;
            decimal c81 = 0, c82 = 0, c83 = 0, c84 = 0, c85 = 0, c86 = 0, c87 = 0, c88 = 0, c89 = 0, c90 = 0;
            decimal c91 = 0, c92 = 0, c93 = 0, c94 = 0, c95 = 0, c96 = 0, c97 = 0, c98 = 0;
            

            DBDataContext db = new DBDataContext(Config.DBCon);

            //cx1 : 셀수
            //cx2 : 사용셀수
            //cx3 : 재고셀수
            //cx4 : 금지셀수
            //cx5 : 공출셀수
            //cx6 : 이중셀수
            //cx7 : 입고가능셀수

            var q = db.ExecuteQuery("Select lstk_use as uuse, lstk_stat as stat, lstk_bk as bk from milstk where lstk_no like 'A%' ").ToList();
            foreach (var s in q)
            {
                bk = s.bk;
                switch (bk)
                {
                    case "01":
                        {
                            c11++;
                            if (s.uuse == "1") c21++; else c41++;
                            if (s.stat == "10") c31++;
                            if (s.stat == "$E") c51++;
                            if (s.stat == "ID") c61++;
                            if (s.stat == "00" && s.uuse == "1") c71++;
                            break;
                        }
                    case "02":
                        {
                            c12++;
                            if (s.uuse == "1") c22++; else c42++;
                            if (s.stat == "10") c32++;
                            if (s.stat == "$E") c52++;
                            if (s.stat == "ID") c62++;
                            if (s.stat == "00" && s.uuse == "1") c72++;
                            break;
                        }
                    case "03":
                        {
                            c13++;
                            if (s.uuse == "1") c23++; else c43++;
                            if (s.stat == "10") c33++;
                            if (s.stat == "$E") c53++;
                            if (s.stat == "ID") c63++;
                            if (s.stat == "00" && s.uuse == "1") c73++;
                            break;
                        }
                    case "04":
                        {
                            c14++;
                            if (s.uuse == "1") c24++; else c44++;
                            if (s.stat == "10") c34++;
                            if (s.stat == "$E") c54++;
                            if (s.stat == "ID") c64++;
                            if (s.stat == "00" && s.uuse == "1") c74++;
                            break;
                        }
                    case "05":
                        {
                            c15++;
                            if (s.uuse == "1") c25++; else c45++;
                            if (s.stat == "10") c35++;
                            if (s.stat == "$E") c55++;
                            if (s.stat == "ID") c65++;
                            if (s.stat == "00" && s.uuse == "1") c75++;
                            break;
                        }
                    case "06":
                        {
                            c16++;
                            if (s.uuse == "1") c26++; else c46++;
                            if (s.stat == "10") c36++;
                            if (s.stat == "$E") c56++;
                            if (s.stat == "ID") c66++;
                            if (s.stat == "00" && s.uuse == "1") c76++;
                            break;
                        }
                    case "07":
                        {
                            c17++;
                            if (s.uuse == "1") c27++; else c47++;
                            if (s.stat == "10") c37++;
                            if (s.stat == "$E") c57++;
                            if (s.stat == "ID") c67++;
                            if (s.stat == "00" && s.uuse == "1") c77++;
                            break;
                        }
                    case "08":
                        {
                            c18++;
                            if (s.uuse == "1") c28++; else c48++;
                            if (s.stat == "10") c38++;
                            if (s.stat == "$E") c58++;
                            if (s.stat == "ID") c68++;
                            if (s.stat == "00" && s.uuse == "1") c78++;
                            break;
                        }
                    case "09":
                        {
                            c19++;
                            if (s.uuse == "1") c29++; else c49++;
                            if (s.stat == "10") c39++;
                            if (s.stat == "$E") c59++;
                            if (s.stat == "ID") c69++;
                            if (s.stat == "00" && s.uuse == "1") c79++;
                            break;
                        }
                    case "10":
                        {
                            c20++;
                            if (s.uuse == "1") c30++; else c50++;
                            if (s.stat == "10") c40++;
                            if (s.stat == "$E") c60++;
                            if (s.stat == "ID") c70++;
                            if (s.stat == "00" && s.uuse == "1") c80++;
                            break;
                        }
                    default:
                        break;

                }
            }

            //합계
            c91 = c11 + c12 + c13 + c14 + c15 + c16 + c17 + c18 + c19 + c20;
            c92 = c21 + c22 + c23 + c24 + c25 + c26 + c27 + c28 + c29 + c30;
            c93 = c31 + c32 + c33 + c34 + c35 + c36 + c37 + c38 + c39 + c40;
            c94 = c41 + c42 + c43 + c44 + c45 + c46 + c47 + c48 + c49 + c50;
            c95 = c51 + c52 + c53 + c54 + c55 + c56 + c57 + c58 + c59 + c60;
            c96 = c61 + c62 + c63 + c64 + c65 + c66 + c67 + c68 + c69 + c70;
            c97 = c71 + c72 + c73 + c74 + c75 + c76 + c77 + c78 + c79 + c80;

            //try
            //{
            //적재율(재고셀수/셀수) 
            /*    c81 = c31 / c11 * 100;
                c82 = c32 / c12 * 100;
                c83 = c33 / c13 * 100;
                c84 = c34 / c14 * 100;
                c85 = c35 / c15 * 100;
                c86 = c36 / c16 * 100;
                c87 = c37 / c17 * 100;
                c88 = c38 / c18 * 100;
                c89 = c39 / c19 * 100;
                c90 = c40 / c20 * 100;
                c98 = c93 / c91 * 100;  */

            //적재율(재고셀수/사용셀수)로 변경 요청(완료일:210406)
            c81 = c31 / c21 * 100;
            c82 = c32 / c22 * 100;
            c83 = c33 / c23 * 100;
            c84 = c34 / c24 * 100;
            c85 = c35 / c25 * 100;
            c86 = c36 / c26 * 100;
            c87 = c37 / c27 * 100;
            c88 = c38 / c28 * 100;
            c89 = c39 / c29 * 100;
            c90 = c40 / c30 * 100;
            c98 = c93 / c92 * 100;
            //}
            //catch (Exception E)
            //{ }


            btnqry.Text = c11.ToString();
            DataGridView dv = dataGridView1;
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();
            dv.Rows.Add();

            dv[0, 0].Value = "셀수";
            dv[1, 0].Value = c11.ToString();
            dv[2, 0].Value = c12.ToString();
            dv[3, 0].Value = c13.ToString();
            dv[4, 0].Value = c14.ToString();
            dv[5, 0].Value = c15.ToString();
            dv[6, 0].Value = c16.ToString();
            dv[7, 0].Value = c17.ToString();
            dv[8, 0].Value = c18.ToString();
            dv[9, 0].Value = c19.ToString();
            dv[10, 0].Value = c20.ToString();

            dv[0, 1].Value = "사용셀수";
            dv[1, 1].Value = c21.ToString();
            dv[2, 1].Value = c22.ToString();
            dv[3, 1].Value = c23.ToString();
            dv[4, 1].Value = c24.ToString();
            dv[5, 1].Value = c25.ToString();
            dv[6, 1].Value = c26.ToString();
            dv[7, 1].Value = c27.ToString();
            dv[8, 1].Value = c28.ToString();
            dv[9, 1].Value = c29.ToString();
            dv[10, 1].Value = c30.ToString();

            dv[0, 2].Value = "재고셀수";
            dv[1, 2].Value = c31.ToString();
            dv[2, 2].Value = c32.ToString();
            dv[3, 2].Value = c33.ToString();
            dv[4, 2].Value = c34.ToString();
            dv[5, 2].Value = c35.ToString();
            dv[6, 2].Value = c36.ToString();
            dv[7, 2].Value = c37.ToString();
            dv[8, 2].Value = c38.ToString();
            dv[9, 2].Value = c39.ToString();
            dv[10, 2].Value = c40.ToString();

            dv[0, 3].Value = "금지셀수";
            dv[1, 3].Value = c41.ToString();
            dv[2, 3].Value = c42.ToString();
            dv[3, 3].Value = c43.ToString();
            dv[4, 3].Value = c44.ToString();
            dv[5, 3].Value = c45.ToString();
            dv[6, 3].Value = c46.ToString();
            dv[7, 3].Value = c47.ToString();
            dv[8, 3].Value = c48.ToString();
            dv[9, 3].Value = c49.ToString();
            dv[10,3].Value = c50.ToString();

            dv[0, 4].Value = "공출셀수";
            dv[1, 4].Value = c51.ToString();
            dv[2, 4].Value = c52.ToString();
            dv[3, 4].Value = c53.ToString();
            dv[4, 4].Value = c54.ToString();
            dv[5, 4].Value = c55.ToString();
            dv[6, 4].Value = c56.ToString();
            dv[7, 4].Value = c57.ToString();
            dv[8, 4].Value = c58.ToString();
            dv[9, 4].Value = c59.ToString();
            dv[10, 4].Value = c60.ToString();

            dv[0, 5].Value = "이중셀수";
            dv[1, 5].Value = c61.ToString();
            dv[2, 5].Value = c62.ToString();
            dv[3, 5].Value = c63.ToString();
            dv[4, 5].Value = c64.ToString();
            dv[5, 5].Value = c65.ToString();
            dv[6, 5].Value = c66.ToString();
            dv[7, 5].Value = c67.ToString();
            dv[8, 5].Value = c68.ToString();
            dv[9, 5].Value = c69.ToString();
            dv[10, 5].Value = c70.ToString();

            dv[0, 6].Value = "입고가능셀수";
            dv[1, 6].Value = c71.ToString();
            dv[2, 6].Value = c72.ToString();
            dv[3, 6].Value = c73.ToString();
            dv[4, 6].Value = c74.ToString();
            dv[5, 6].Value = c75.ToString();
            dv[6, 6].Value = c76.ToString();
            dv[7, 6].Value = c77.ToString();
            dv[8, 6].Value = c78.ToString();
            dv[9, 6].Value = c79.ToString();
            dv[10, 6].Value = c80.ToString();

            dv[0, 7].Value = "적재율";
            dv[1, 7].Value = c81.ToString("0.00");
            dv[2, 7].Value = c82.ToString("0.00");
            dv[3, 7].Value = c83.ToString("0.00");
            dv[4, 7].Value = c84.ToString("0.00");
            dv[5, 7].Value = c85.ToString("0.00");
            dv[6, 7].Value = c86.ToString("0.00");
            dv[7, 7].Value = c87.ToString("0.00");
            dv[8, 7].Value = c88.ToString("0.00");
            dv[9, 7].Value = c89.ToString("0.00");
            dv[10, 7].Value = c90.ToString("0.00");
            //합계
            dv[11, 0].Value = c91.ToString();
            dv[11, 1].Value = c92.ToString();
            dv[11, 2].Value = c93.ToString();
            dv[11, 3].Value = c94.ToString();
            dv[11, 4].Value = c95.ToString();
            dv[11, 5].Value = c96.ToString();
            dv[11, 6].Value = c97.ToString();
            dv[11, 7].Value = c98.ToString("0.00");

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Common.ExtractDataToCSV(dataGridView1);
        }
    }

    public class milstkCellinfo
    {
        public string uuse { get; set; }
        public string stat { get; set; }
        public string bk { get; set; }
     
    }
}
