

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKSurfaceFileManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Tools;
    using System.IO;
    using IGK.ICore.IO;
    [CoreTools("Tool.Drawing2DFileManager")]
    sealed class IGKSurfaceFileManager : CoreToolBase
    {
        private static IGKSurfaceFileManager sm_instance;
        private Dictionary<ICoreWorkingFilemanagerSurface, FileWatcherSurface> m_manager = new Dictionary<ICoreWorkingFilemanagerSurface, FileWatcherSurface>();
        private IGKSurfaceFileManager()
        {
        }

        public static IGKSurfaceFileManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKSurfaceFileManager()
        {
            sm_instance = new IGKSurfaceFileManager();

        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }

        void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            var s = e.NewElement  as ICoreWorkingFilemanagerSurface;
            if ((s!=null) && (!this.m_manager.ContainsKey(s)))
            { 
                
                this.m_manager.Add(s, new FileWatcherSurface(s, this));
            }
        }
      

        class FileWatcherSurface
        {
            private ICoreWorkingFilemanagerSurface m_surface;
            private FileSystemWatcher m_watcher;
            private IGKSurfaceFileManager m_tool;
            private DateTime m_lastTime;
            private object m_Sync = new object ();


            public FileWatcherSurface(ICoreWorkingFilemanagerSurface surface, IGKSurfaceFileManager tool)
            {
                this.m_tool = tool;
                this.m_surface = surface;
                this.m_watcher = new FileSystemWatcher();
                
                this.m_surface.FileNameChanged += m_surface_FileNameChanged;
                this.m_surface.Disposed += m_surface_Disposed;
                this.m_surface.NeedToSaveChanged += m_surface_NeedToSaveChanged;
                var fb = this.m_tool.Workbench as ICoreSurfaceManagerWorkbench;
                
                this.m_surface.Saved += m_surface_Saved;
                if (fb != null)
                {
                    fb.SurfaceAdded += Workbench_SurfaceAdded;
                    fb.SurfaceRemoved += Workbench_SurfaceRemoved;
                }
                this.m_watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                this.m_watcher.Changed += m_watcher_Changed;
                this._updatePath();
                this._updateTime();
            }

            void Workbench_SurfaceRemoved(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                if (e.Item == this.m_surface)
                {
                    this.m_surface_Disposed(this.m_surface, EventArgs.Empty);
                }
            }

            void Workbench_SurfaceAdded(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                
            }

            void m_surface_Saved(object sender, EventArgs e)
            {
                this._updateTime();
            }

            private void _updateTime()
            {
           
                lock (m_Sync)
                {
                    if (File.Exists(this.m_surface.FileName))
                    {
                        if (this.m_surface.NeedToSave == false)
                        {
                            FileInfo finfo = new FileInfo(this.m_surface.FileName);
                            this.m_lastTime = finfo.LastWriteTime;
                        }
                    }
                }
            }

            void m_surface_NeedToSaveChanged(object sender, EventArgs e)
            {           
                   
            }

            private void _updatePath()
            {

                this.m_watcher.EnableRaisingEvents = false;
                if (File.Exists(this.m_surface.FileName))
                {
                    string d = PathUtils.GetDirectoryName(this.m_surface.FileName);
                    if (string.IsNullOrEmpty(d) == false)
                    {
                        this.m_watcher.Path = d;
                        this.m_watcher.EnableRaisingEvents = true;
                    }
                }
            }
           

            void m_watcher_Changed(object sender, FileSystemEventArgs e)
            {
                //TODO: SAVE WATCHER
                //return;


                //lock (m_Sync)
                //{
                //    if (this.m_surface.Saving)
                //        return;
                //    string file = this.m_surface.FileName;
                //    if (this.m_surface.FileName.ToLower() == e.FullPath.ToLower())
                //    {
                //        FileInfo finfo = new FileInfo(file);
                //        if (finfo.LastWriteTime > m_lastTime)
                //        {
                //            switch (e.ChangeType)
                //            {
                //                case WatcherChangeTypes.Changed:
                //                    if (CoreMessageBox.Show("msg.surfaceneedtoreload".R(), "warning".R())
                //                        == enuDialogResult.OK)
                //                    {                                        
                //                        this.m_surface.ReloadFileFromDisk(); 
                //                    }
                //                    break;
                //                default:
                //                    break;
                //            }
                //            //update the last writed time
                //            m_lastTime = finfo.LastWriteTime;
                //        }
                //    }
                //}
            }

            void m_surface_Disposed(object sender, EventArgs e)
            {
                this.m_watcher.Dispose();
                this.m_tool.m_manager.Remove(this.m_surface);
            }

            void m_surface_FileNameChanged(object sender, EventArgs e)
            {
                this._updatePath();
            }
        }
    }
}
