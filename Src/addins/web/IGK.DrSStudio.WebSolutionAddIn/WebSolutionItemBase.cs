

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionItemBase.cs
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

namespace IGK.DrSStudio.Web
{
    /// <summary>
    /// represent a web solution item base
    /// </summary>
    public abstract class WebSolutionItemBase : ICoreWorkingProjectSolutionItem
    {
        private string m_Name;
        private WebSolutionSolution m_solution;
        /// <summary>
        /// get or set the name of this item
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public virtual void Open(ICoreSystemWorkbench bench)
        { 

        }
        public abstract string ImageKey
        {
            get;
        }

        /// <summary>
        /// get the solution attached to this
        /// </summary>
        public WebSolutionSolution Solution
        {
            get { return this.m_solution; }
            internal set {
                if (this.m_solution != value)
                {
                    this.m_solution = value;
                    OnSolutionChanged(EventArgs.Empty);
                }
            }
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


        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionItem.Solution
        {
            get { return this.m_solution; }
        }
    }
}
