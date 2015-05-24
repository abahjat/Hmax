using System;
using System.Windows.Forms;

namespace Hmax
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var ai = HMACAPP.getHMACAPP())
            {
                ai.Display();
                Application.Run();
            }
        }
    }
}