using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KK5
{
    public partial class FrmInptY : KK5.FrmPltzYF
    {
        #region --- MDI Child ----------------
        private static FrmInptY _instance;
        public static FrmInptY Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmInptY();

                return _instance;
            }
        }
        private void FrmInptY_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
        public FrmInptY()
        {
            InitializeComponent();
            igb = "Y";
            this.FormClosed += FrmInptY_FormClosed;
            label1.Text = "메인 입고";
            this.Text = "메인 입고";
        }

        private void FrmInptY_Load(object sender, EventArgs e)
        {

        }
    }
}
