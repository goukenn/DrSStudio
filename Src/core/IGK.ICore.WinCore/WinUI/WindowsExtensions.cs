
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI
{
    public static class WindowsExtensions
    {
        /// <summary>
        /// make this windows as client fullscreen
        /// </summary>
        /// <param name="form"></param>
        public static void ClientFullScreen(this Form form) {
            if (form.WindowState != FormWindowState.Normal)
                form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;

        }
    }
}
