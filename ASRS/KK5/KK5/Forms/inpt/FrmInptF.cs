using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KK5
{
    public partial class FrmInptF : KK5.FrmPltzYF
    {
        #region --- MDI Child ----------------
        private static FrmInptF _instance;
        public static FrmInptF Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FrmInptF();

                return _instance;
            }
        }
        private void FrmInptF_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        #endregion
        public FrmInptF()
        {
            InitializeComponent();

            igb = "F";
            this.FormClosed += FrmInptF_FormClosed;
        }

        private void FrmInptF_Load(object sender, EventArgs e)
        {          
           

        }
    }
}
