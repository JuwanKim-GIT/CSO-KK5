using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KK5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //using (FrmLogin p = new FrmLogin())
            //{
            //    p.ShowDialog();
            //    if (p.DialogResult == DialogResult.Cancel)
            //    {
            //        Application.Exit();
            //        return;
            //    }
            //}       

            Application.Run(new FrmMain());
        }
    }
}
