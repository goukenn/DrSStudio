using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Win10.Tools.Menu.File
{
    using IGK.ICore.WinUI;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    [CoreMenu("File.OpenWith", 4)]
    class OpenWithWinMenu : CoreApplicationMenu  { 
    }
    [CoreMenu("File.OpenWith.WindowPhotoExplorer", 10)]
    class OpenWithWinPhotoViewerMenu : CoreApplicationMenu
    {
        protected override bool IsVisible()
        {
            return true;
        }
        protected override bool PerformAction()
        {
            var f = this.CurrentSurface as ICoreWorkingFilemanagerSurface;
            if (string.IsNullOrEmpty (f?.FileName)) {
                return false;
            }
            string s = f.FileName;
            string c = Environment.ExpandEnvironmentVariables(string.Format("\"%SystemRoot%\\System32\\Rundll32.exe\" \"%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll\", ImageView_Fullscreen {0}",
                s));
            try
            {
               ProcessStartInfo ff = new ProcessStartInfo();
               ff.UseShellExecute = true ;
               ff.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%\\System32\\Rundll32.exe");
               ff.Arguments = Environment.ExpandEnvironmentVariables(string.Format("\"%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll\",ImageView_Fullscreen {0}", s));
                Process.Start(s); 
            }
            catch (Exception ex)
            {
                IGK.ICore.CoreLog.WriteDebug(ex.Message);
            }
            finally {
                
            }
            return base.PerformAction();
        }
    }
}
