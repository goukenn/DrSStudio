

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileBrowserActions.cs
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
file:FileBrowserActions.cs
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.Actions
{
    
using IGK.ICore;using IGK.DrSStudio.Actions;
    
    
    using IGK.ICore.MecanismActions;
    using IGK.DrSStudio.Drawing3D.FileBrowser.WinUI;

    /// <summary>
    /// represent the base file browser action
    /// </summary>
    public abstract class FBAction : CoreMecanismActionBase, IFBAction 
    {        
        public FBControlSurface FileBrowser
        {
            get {
                return this.Mecanism.CurrentSurface as FBControlSurface; 
            }
        }   
        public FBAction()
        {
        }
        #region IFBAction Members
        IFBSurface  IFBAction.FileBrowser
        {
            get { return this.FileBrowser as IFBSurface; }
        }
        #endregion
        public override string Id
        {
            get { return string.Format ("FileBrowserAction.{0}",this.GetType ().Name ); }
        }
    }
}

