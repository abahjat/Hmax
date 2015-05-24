using System;
using System.Runtime.InteropServices;
using System.Text;
using NDde.Client;

namespace Hmax
{
    internal class ProcessCommUtil
    {
        public static string GetBrowserURL(string browser)
        {
            try
            {
                var dde = new DdeClient(browser, "WWW_GetWindowInfo");
                dde.Connect();
                var url = dde.Request("URL", int.MaxValue);
                var text = url.Split(new[] {"\",\""}, StringSplitOptions.RemoveEmptyEntries);
                dde.Disconnect();
                return text[0].Substring(1);
            }
            catch
            {
                return null;
            }
        }

        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        public static string GetActiveWindow()
        {
            const int nChars = 256;
            var handle = 0;
            var Buff = new StringBuilder(nChars);

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