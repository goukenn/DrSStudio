

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSurfaceBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Android.WinUI
{
    public class AndroidSurfaceBase : 
        IGKXWinCoreWorkingSurface,
        IAndroidSolutionSurface,
        ICoreWorkingProjectSolutionSurface 
    {

        private AndroidProject m_Project;

        /// <summary>
        /// represent a android project
        /// </summary>
        public AndroidProject Project
        {
            get
            {
                return this.m_Project;
            }
            set
            {
                if (this.m_Project != value)
                {
                    this.m_Project = value;
                    OnProjectChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ProjectChanged;
        private void OnProjectChanged(EventArgs eventArgs)
        {
            if (this.ProjectChanged != null)
            {
                this.ProjectChanged(this, eventArgs);
            }
        }

        public ICoreWorkingProjectSolution Solution
        {
            get {
                return this.m_Project as ICoreWorkingProjectSolution;
            }
        }
    }
}
