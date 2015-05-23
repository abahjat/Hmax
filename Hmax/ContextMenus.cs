using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using Hmax.Properties;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Text;
using System.Text.RegularExpressions;

namespace Hmax
{
	/// <summary>
	/// 
	/// </summary>
	public class ContextMenus
	{
		/// <summary>
		/// Is the About box displayed?
		/// </summary>
        ContextMenuStrip menu = new ContextMenuStrip();
        ToolStripMenuItem Activate;
        ToolStripMenuItem Deactivate;
        ToolStripMenuItem ProfileSettings;
        ToolStripMenuItem Exit;
        ToolStripSeparator sep;
        Boolean isActive = false;
	    private string _certSubject = "CN=Test Cert2";
	    private EncryptionUtil util;
        String password = null;
	    private UserActivityHook hook = null;

	    public string CertSubject
	    {
	        get { return _certSubject; }
	        set { _certSubject = value; }
	    }

	    public string CharString
	    {
	        get { return charString; }
	        set { charString = value; }
	    }

	    public void ResetCert(string subject)
	    {
	        util = subject == null ? new EncryptionUtil() : new EncryptionUtil(subject);
	        CertSubject = subject;
	    }

	    /// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>ContextMenuStrip</returns>
		public ContextMenuStrip Create()
		{
			// Add the default menu options.

	        try
	        {
	            util = new EncryptionUtil(_certSubject);
	        }
	        catch (Exception)
	        {
                util = new EncryptionUtil();
	        }
	        // Windows Explorer.
            Activate = new ToolStripMenuItem();
            Activate.Text = "Activate";
            Activate.Click += new EventHandler(Activate_Click);
            Activate.ShortcutKeys = Keys.F4;
            Activate.Image = Resources.bullet_green;
            menu.Items.Add(Activate);

            //Profile Settings
            ProfileSettings = new ToolStripMenuItem();
		    ProfileSettings.Text = "Settings";
		    ProfileSettings.Click += new EventHandler(ProfileSettings_Click);
		    menu.Items.Add(ProfileSettings);

			// About.
			Deactivate = new ToolStripMenuItem();
            Deactivate.Text = "Deactivate";
            Deactivate.Click += new EventHandler(Deactivate_Click);
            Deactivate.Image = Resources.bullet_red;
            Deactivate.Visible = false;
            menu.Items.Add(Deactivate);
            
			// Separator.
			sep = new ToolStripSeparator();
			menu.Items.Add(sep);

			// Exit.
			Exit = new ToolStripMenuItem();
			Exit.Text = "Exit";
            Exit.Click += new System.EventHandler(Exit_Click);
            menu.Items.Add(Exit);

            hook = new UserActivityHook();
            hook.Start(false,true);
            hook.KeyPress += hook_KeyPress;
            hook.KeyUp += f4_KeyUp;
            hook.KeyDown += f4_KeyDown;

			return menu;
		}

        private void f4_KeyDown(object sender, KeyEventArgs e){
            if (e.KeyData == Keys.F4)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
                
        }
        private void f4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F4)
            {
                if (isActive)
                {
                    deactivate();
                }
                else
                {
                    activate();
                }
            }
        }
        
        string url = "http://default.com/%2E%2E/%2E%2E";
	    private Uri uriAddress = null;


        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);

	    private string stronPss = "";
        private void hook_KeyPress(object sender, KeyPressEventArgs e)
        {

            
            //Regex regex = new Regex("^(?>https?://|)([-A-Z0-9+&@#%?=~_|!,.;]+)", RegexOptions.IgnoreCase);
            //Match match = regex.Match(url);

            //Console.WriteLine(uriAddress.DnsSafeHost);

            int lenMentalPwd = 0;
            
                if (isActive)
                {
                   // Console.WriteLine("test: " + e.KeyChar);
                    

                    if (Convert.ToInt32(e.KeyChar) == 8 && stronPss.Length > 0)
                    {
                       stronPss = stronPss.Substring(0, stronPss.Length - 1);
                        return;
                    }
                    else if (Convert.ToInt32(e.KeyChar) == 8)
                    {
                        return;
                    }

                    stronPss += "" + e.KeyChar;
                    /* stronPss = getHMACPart(stronPss+e.KeyChar,util);
                     foreach (var x in stronPss)
                     {
                         sendKeyStrokeToActiveWindow(""+x);
                     }*/
                    string domain ="",activeWindow;
                    if (Convert.ToInt32(e.KeyChar) == 13 || e.KeyChar == '\t')
                    {
#if DEBUG
                        Stopwatch stopwatch = Stopwatch.StartNew();
#endif
                        lenMentalPwd = stronPss.Length;
                        //Console.WriteLine(ProcessCommUtil.GetActiveWindow());
                        
                        activeWindow = ProcessCommUtil.GetActiveWindow();
                        if (activeWindow.Contains("Firefox"))
                        {
                            try
                            {
                                url = ProcessCommUtil.GetBrowserURL("firefox");
                                if (url != null)
                                {
                                    uriAddress = new Uri(url);
                                    domain = uriAddress.DnsSafeHost;
                                    domain = domain.Substring(domain.IndexOf("."));
                                }
                                    
                            }
                            catch (Exception)
                            {
#if DEBUG
                                Console.WriteLine("No URI updated"); 
#endif
                            }
                            
                        }
                        else if (activeWindow.Contains("PuTTY"))
                        {
                            domain = activeWindow;
                        }
                            
                        stronPss = getHMACPwd(stronPss.Trim() ,domain, util);
                        if (stronPss.Contains("{"))
                        {
                            stronPss = stronPss.Replace("{", "{{}");
                        }
                        if (stronPss.Contains("~"))
                        {
                            stronPss = stronPss.Replace("~", "{~}");
                        }
                        if (stronPss.Contains("+"))
                        {
                            stronPss = stronPss.Replace("+", "{+}");
                        }
                        if (stronPss.Contains("^"))
                        {
                            stronPss = stronPss.Replace("^", "{^}");
                        }
                        if (stronPss.Contains("%"))
                        {
                            stronPss = stronPss.Replace("%", "{%}");
                        }
                        if (stronPss.Contains("("))
                        {
                            stronPss = stronPss.Replace("(", "{(}");
                        }
                        if (stronPss.Contains(")"))
                        {
                            stronPss = stronPss.Replace(")", "{)}");
                        }

                        string backspaces = getBackSpaces(lenMentalPwd);
                        
                        e.Handled = true;
                        deactivate();
                        if (Convert.ToInt32(e.KeyChar) == 13)
                        {
                            sendKeyStrokeToActiveWindow(backspaces + stronPss + "{ENTER}");
                        }
                        else
                        {
                            sendKeyStrokeToActiveWindow(backspaces + stronPss);
                        }
                        stronPss = "";
#if DEBUG
                        stopwatch.Stop();
                        Console.WriteLine("time = "+stopwatch.ElapsedMilliseconds);
#endif

                    }

                }
            
        }

	    private string getBackSpaces(int len)
	    {
	        string result = "";
            for (int i = 0; i < len-1; i++)
            {
                result += "{BS}";
            }

	        return result;
	    }

        private void sendKeyStrokeToActiveWindow(string text)
        {
            int iHandle = NativeWin32.FindWindow(null, text);

            NativeWin32.SetForegroundWindow(iHandle);

            Console.WriteLine("Text: "+text);

            System.Windows.Forms.SendKeys.Send(text);

        }
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] 
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
            }
            return ch;
        }

        private string getHMACPwd(string password, string url, EncryptionUtil utilParam)
        {
            if (password == null || utilParam == null)
                return null;
            if (password.Equals(""))
                return "";

            string hmacpwd = "";
            char newChar, spare;
            string EncryptAndEncode = "";
            char[] buffer;
            char[] passArray = password.ToCharArray();
            int i;


            for (i = 0; i < passArray.Length - 1; i++)
            {
                EncryptAndEncode = util.EncryptAndEncode(hmacpwd + password +url);
                //if (i == 0) Console.WriteLine("\"" + hmacpwd + password.Trim() + url + "\"");
                EncryptAndEncode = util.getHMAC5(EncryptAndEncode);
                buffer = EncryptAndEncode.ToCharArray();
                newChar = getTranslatedChar(buffer, 0, buffer.Length / 2);
                hmacpwd += newChar;
                newChar = getTranslatedChar(buffer, buffer.Length / 2, buffer.Length);
                hmacpwd += newChar;
            }

            if (hmacpwd.Length < 12)
            {
                spare = passArray[i];
                for (int j = 0; j < 14 - i * 2; j += 2)
                {
                    EncryptAndEncode = util.EncryptAndEncode(hmacpwd + spare);
                    EncryptAndEncode = util.getHMAC5(EncryptAndEncode);
                    buffer = EncryptAndEncode.ToCharArray();
                    newChar = getTranslatedChar(buffer, 0, buffer.Length / 2);
                    hmacpwd += newChar;
                    newChar = getTranslatedChar(buffer, buffer.Length / 2, buffer.Length);
                    hmacpwd += newChar;
                    spare = newChar;
                }

            }
            else
            {
                EncryptAndEncode = util.EncryptAndEncode(hmacpwd + passArray[i]);
                EncryptAndEncode = util.getHMAC5(EncryptAndEncode);
                buffer = EncryptAndEncode.ToCharArray();
                newChar = getTranslatedChar(buffer, 0, buffer.Length / 2);
                hmacpwd += newChar;
                newChar = getTranslatedChar(buffer, buffer.Length / 2, buffer.Length);
                hmacpwd += newChar;
            }

            //Console.WriteLine(hmacpwd.Length);
            return hmacpwd;
        }

	    private string charString =
	        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_-+=<>;:[]?.,|/`";
        private char getTranslatedChar(char[] buffer, int start, int end)
        {
            long number = 0;
            char[] chars = charString.ToCharArray();

            for (int i = start; i < end; i++)
                if (long.MaxValue - number > buffer[i])
                    number = number + buffer[i];
                else
                    Console.WriteLine("Overflow");
            return chars[(number % chars.Length)];
        }
        /// <summary>
        /// Handles the Click event of the Settings.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ProfileSettings_Click(object sender, EventArgs e)
        {
            settings();
        }

		/// <summary>
		/// Handles the Click event of the Explorer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Activate_Click(object sender, EventArgs e)
		{
            activate();
		}

		/// <summary>
		/// Handles the Click event of the About control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Deactivate_Click(object sender, EventArgs e)
		{
            deactivate();
		}

		/// <summary>
		/// Processes a menu item.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
            hook.Stop();
			Application.Exit();
		}

        void activate()
        {
            Activate.Visible = false;
            Deactivate.Visible = true;
            isActive = true;
            
            HMACAPP.getHMACAPP().setApplicationIconToActivated();
            HMACAPP.getHMACAPP().setBubbleText("Hmax Scheme is activated");
        }

        void settings()
        {
            SettingsForm set = new SettingsForm(this);
            set.ShowDialog();
            set.Dispose();
        }

        void deactivate()
        {
            Deactivate.Visible = false;
            Activate.Visible = true;
            isActive = false;
            HMACAPP.getHMACAPP().setApplicationIconToDeactivated();
            HMACAPP.getHMACAPP().setBubbleText("Hmax Scheme is deactivated");
        }
	}
}