

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionItemOpener.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android
{
    class AndroidSolutionItemOpener
    {
        private AndroidProject m_androidProject;

        public AndroidSolutionItemOpener(AndroidProject androidProject)
        {
            this.m_androidProject = androidProject;
        }

        internal void Open(ICoreSystemWorkbench coreWorkbench, ICoreWorkingProjectSolutionItem item)
        {

            if (coreWorkbench == null)
            {
                if (item is AndroidSolutionJScriptFile)
                {
                    Open(null, item as AndroidSolutionJScriptFile);
                }
                else
                {
                    ParameterInfo[] inf = MethodInfo.GetCurrentMethod().GetParameters();
                    MethodInfo.GetCurrentMethod().Visit(this, inf, coreWorkbench, item);
                }
            }
            else
            {
                MethodInfo.GetCurrentMethod().Visit(this, coreWorkbench, item);
            }
        }

        public void Open(ICoreWorkbench coreWorkbench, AndroidSolutionFolder item)
        {
            item.Open(null);
        }
        public void Open(ICoreWorkbench bench, AndroidSolutionManifest item)
        {
            AndroidManifestEditorSurface v_manifest = new AndroidManifestEditorSurface();
            v_manifest.Project = this.m_androidProject;
            v_manifest.FileName = item.FileName;
            v_manifest.Manifest.Open(item.FileName);
            bench.AddSurface(v_manifest, true );
        }
        public void Open(ICoreWorkbench bench, AndroidSolutionFile item)
        {
            ICoreFileManagerWorkbench fb = bench  as ICoreFileManagerWorkbench;
                                
            if (fb != null)
            {
                AndroidCodeFileBuilder cb = null;
                if (!fb.IsFileOpened(item.FileName))
                {
                    //check for surface opened
                    cb = new AndroidCodeFileBuilder();
                    cb.LoadFile(item.FileName);
                    cb.Project = this.m_androidProject;
                    fb.AddSurface(cb, true);
                }
                else
                {
                    if (bench is ICoreSurfaceManagerWorkbench)
                    fb.CurrentSurface = (bench as ICoreSurfaceManagerWorkbench).Surfaces[item.FileName];
                }
            }
            else
            {
                Process.Start(item.FileName);
            }
        }
        public void Open(ICoreWorkbench bench, AndroidSolutionJScriptFile item)
        {
            ICoreFileManagerWorkbench fb = CoreSystem.Instance.Workbench as ICoreFileManagerWorkbench;
                            
            if (File.Exists(item.FileName))
            {
                if ((fb != null))
                {


                    AndroidJavaFileCodeBuilder cb = null;
                    if (!fb.IsFileOpened(item.FileName))
                    {
                        //check for surface opened
                        cb = new AndroidJavaFileCodeBuilder();
                        cb.LoadFile(item.FileName);
                        cb.Project = this.m_androidProject;
                        fb.AddSurface(cb,true );
                    }
                    else
                    {
                        if (bench is ICoreSurfaceManagerWorkbench)
                        {
                            bench.CurrentSurface = (bench as ICoreSurfaceManagerWorkbench).Surfaces[item.FileName];
                        }
                    }
                }
                else
                {


                    Thread th = new Thread(() =>
                    {
                        var cb = new AndroidJavaFileCodeBuilder();
                        cb.LoadFile(item.FileName);
                        cb.Project = this.m_androidProject ;
                        using (Form frm = new Form())
                        {
                            frm.Text = item.FileName;
                            frm.Size = new System.Drawing.Size(600, 400);
                            cb.Dock = DockStyle.Fill;
                            frm.Controls.Add(cb);
                            try
                            {
                                Application.EnableVisualStyles();
                                Application.Run(frm);
                            }
                            catch (Exception ex)
                            {
                                CoreLog.WriteLine(ex.Message);
                            }
                        }
                    }
                        );
                    th.SetApartmentState(ApartmentState.STA);
                    th.IsBackground = false;
                    th.Start();
                }
            }
        }

        public void Open(ICoreWorkbench bench, AndroidLayoutDocument item)
        {
            AndroidLayoutDesignSurface v_surface = new AndroidLayoutDesignSurface();
            v_surface.Project = this.m_androidProject;
            //v_surface.FileName = item.FileName;
            //v_surface.Open(item.FileName);
            bench.AddSurface(v_surface, true );
        }
    }
}
