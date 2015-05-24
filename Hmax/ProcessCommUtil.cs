using System;
using System.Runtime.InteropServices;
using System.Text;
using NDde.Client;

namespace Hmax
{
    class ProcessCommUtil
    {
        
        public static string GetBrowserURL(string browser)
        {
            try
            {
                DdeClient dde = new DdeClient(browser, "WWW_GetWindowInfo");
                dde.Connect();
                string url = dde.Request("URL", int.MaxValue);
                string[] text = url.Split(new string[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
                dde.Disconnect();
                return text[0].Substring(1);
            }
            catch
            {
                return null;
            }
        }

        [DllImport("user32.dll")]

        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]

        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        public static string GetActiveWindow()
        {

            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);

            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "";

        }

    }
    /*
    class Profile
    {
        private String name;
        private String color;
        private String iconPath;
        private String key;
    }
     */
}
