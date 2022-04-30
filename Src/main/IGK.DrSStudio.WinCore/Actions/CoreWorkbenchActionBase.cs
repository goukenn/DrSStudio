

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkbenchActionBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreWorkbenchActionBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Actions
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    public abstract class CoreWorkbenchActionBase : 
        CoreActionBase ,
        ICoreAction 
    {
        private ICoreWorkbench m_WorkBench;
        /// <summary>
        /// get the workbench
        /// </summary>
        public ICoreWorkbench Workbench
        {
            get { return m_WorkBench; }
            internal set
            {
                if (this.m_WorkBench != value)
                {
                    this.m_WorkBench = value;
                    OnWorkbenchChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// raise when workbench changed by the system
        /// </summary>
        public event EventHandler WorkbenchChanged;
        protected virtual void OnWorkbenchChanged(EventArgs eventArgs)
        {
            if (WorkbenchChanged != null)
            {
                this.WorkbenchChanged(this, eventArgs);
            }
        }
        public CoreWorkbenchActionBase()
        {
            this.Id  = "CoreWorkBenchAction." + this.GetType().Name ;
        }
        protected override bool PerformAction()
        {
            return false;
        }
        #region ICoreIdentifier Members
        //public  string Id
        //{
        //    override get { return this.m_id; }
        //    protected set { this.m_id = value; }
        //}
        #endregion
    }
}

