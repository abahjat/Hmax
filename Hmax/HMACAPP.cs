using Hmax.Properties;
using System;
using System.Windows.Forms;

namespace Hmax
{
    class HMACAPP:IDisposable
    {
        private NotifyIcon applicationIcon;
        private static HMACAPP app = new HMACAPP();

        private HMACAPP()
        {
            applicationIcon = new NotifyIcon();
        }

        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            applicationIcon.Dispose();
        }

        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.			
            //deactivatedIcon.MouseClick += new MouseEventHandler(ni_MouseClick);
            applicationIcon.Icon = Resources.bullet_red1;
            applicationIcon.Text = "Password Converter Deactivated";
            applicationIcon.Visible = true;

            // Attach a context menu.
            ContextMenus contextMenu = new ContextMenus();
            applicationIcon.ContextMenuStrip =  contextMenu.Create();

            Form profileForm = new Form();
            
                
        }


        public void setApplicationIconToActivated()
        {

                applicationIcon.Icon = Resources.bullet_green1;
                applicationIcon.Text = "Password Converter Activated";
         
        }

        public void setApplicationIconToDeactivated()
        {
            applicationIcon.Icon = Resources.bullet_red1;
            applicationIcon.Text = "Password Converter Deactivated";
        }

        public void setBubbleText(String text)
        {
            applicationIcon.BalloonTipText = text;
            applicationIcon.ShowBalloonTip(200);
        }

        public static HMACAPP getHMACAPP()
        {
            return app;
        }
    }
}
