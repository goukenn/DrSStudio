

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebBrowserFileChange.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebBrowserFileChange.cs
*/
using IGK.ICore.Tools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms ;
using IGK.DrSStudio.WebBrowserAddIn.WinUI;
using IGK.ICore.IO;
namespace IGK.DrSStudio.WebBrowserAddIn.Tools
{
    [CoreTools("Tool.WebBrowserFileChange")]
    internal class WebBrowserFileChangeTool : CoreToolBase
    {
        private static WebBrowserFileChangeTool sm_instance;
        private WebBrowserFileChangement m_WebBrowserFileChangement;
        private WebBrowserFileChangeTool()
        {
        }
        public static WebBrowserFileChangeTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WebBrowserFileChangeTool()
        {
            sm_instance = new WebBrowserFileChangeTool();
        }
        public void RegisterChanged(WebKitBrowserSurface surface, string filename)
        {
            if (this.m_WebBrowserFileChangement!=null)
            {
                this.m_WebBrowserFileChangement.Dispose();
            }
            string dir = PathUtils.GetDirectoryName(filename);
            if (Directory.Exists(dir))
            {
                this.m_WebBrowserFileChangement = new WebBrowserFileChangement(filename, surface);
            }
        }
        /// <summary>
        /// used to monitor a file changed
        /// </summary>
        class WebBrowserFileChangement : IDisposable 
        {
            FileSystemWatcher m_watcher;
            WebKitBrowserSurface m_surface;
            string m_filename; 
            public WebBrowserFileChangement(string filename, WebKitBrowserSurface surface)
            {
                this.m_surface = surface;
                this.m_filename = Path.GetFullPath(filename);
                FileSystemWatcher watcher = new FileSystemWatcher(PathUtils.GetDirectoryName(this.m_filename));
                watcher.Changed += watcher_Changed;
                watcher.Renamed += watcher_Renamed;
                watcher.Created += watcher_Created;
                watcher.EnableRaisingEvents = true;
                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
                this.m_watcher = watcher;
            }
            void watcher_Created(object sender, FileSystemEventArgs e)
            {
                CoreLog.WriteDebug("Filename Created" + e.Name);
            }
            internal void Dispose()
            {
                this.m_watcher.Created -= watcher_Created;
                this.m_watcher.Changed -= watcher_Changed;
                this.m_watcher.Renamed -= watcher_Renamed;
                this.m_watcher.Dispose();
            }
            void watcher_Renamed(object sender, RenamedEventArgs e)
            {
                //this.m_surface.Reload();
                this.m_surface.OpenFile(e.FullPath);
            }
            void watcher_Changed(object sender, FileSystemEventArgs e)
            {
                if (e.FullPath == this.m_filename)
                {
                    this.m_surface.Reload();
                    this.m_surface.Workbench.GetLayoutManager()?.Refresh();
                    Application.DoEvents();
                }
            }
            void IDisposable.Dispose()
            {
                this.Dispose();
            }
        }
        internal void Unregister()
        {
            if (this.m_WebBrowserFileChangement != null)
                this.m_WebBrowserFileChangement.Dispose();
        }
    }
}

