using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkladPi
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginPage());
            if (LoginPage.IsSeller)
            {
                Application.Run(new SkladPi());
            }
            else if (LoginPage.IsCustomer)
            {
                Application.Run(new CustomerPage());
            }
            else
            {
                return;
            }



        }
    }
}
