

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionSurfaceBase.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Web.WinUI
{
    /// <summary>
    /// represent a base working web solution surface
    /// </summary>
    public class WebSolutionSurfaceBase : IGKXWinCoreWorkingSurface,
        ICoreWorkingProjectSolutionSurface
    {
        private WebSolutionSolution m_Solution;

        /// <summary>
        /// get or set the solution
        /// </summary>
        public WebSolutionSolution Solution
        {
            get { return m_Solution; }
            set
            {
                if (m_Solution != value)
                {
                    m_Solution = value;
                    OnSolutionChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// get the solution
        /// </summary>
        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionSurface.Solution
        {
            get { return this.m_Solution; }
        }

        public event EventHandler SolutionChanged;
        ///<summary>
        ///raise the SolutionChanged 
        ///</summary>
        protected virtual void OnSolutionChanged(EventArgs e)
        {
            if (SolutionChanged != null)
                SolutionChanged(this, e);
        }

    }
}
