using System;
using System.Windows.Forms;

namespace Hmax
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
            
            using(HMACAPP ai = HMACAPP.getHMACAPP())
            {
                ai.Display();
                Application.Run();
            }
        }
    }
}
