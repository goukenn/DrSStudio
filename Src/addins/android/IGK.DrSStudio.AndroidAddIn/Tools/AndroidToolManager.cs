

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidToolManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    
using IGK.DrSStudio.Android.WinUI;
    using IGK.ICore;
    
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    /*
     * 
     * used to manage environment for maintain android aplication state
     * 
     * */
    [CoreTools("Tool.AndroidToolManager")]
    public sealed class AndroidToolManager : CoreToolBase
    {

        private AndroidProject m_CurrentProject;

        public AndroidProject CurrentProject
        {
            get { return m_CurrentProject; }
            set
            {
                if (m_CurrentProject != value)
                {
                    m_CurrentProject = value;
                    OnCurrentProjectChanged(EventArgs.Empty);
                }
            }
        }

        private void OnCurrentProjectChanged(EventArgs eventArgs)
        {
            if (this.CurrentProjectChanged != null)
                this.CurrentProjectChanged(this, eventArgs);
        }
        public event EventHandler CurrentProjectChanged;
        private static AndroidToolManager sm_instance;
        private AndroidToolManager()
        {
        }

        public static AndroidToolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidToolManager()
        {
            sm_instance = new AndroidToolManager();

        }

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench){

            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.NewElement is IAndroidSolutionSurface)
            {
                this.CurrentProject =( e.NewElement as IAndroidSolutionSurface).Project;
            }
            else {
                this.CurrentProject = null;
            }
        }
        
    }
}
