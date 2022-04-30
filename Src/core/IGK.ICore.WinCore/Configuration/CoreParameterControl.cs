

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterControl.cs
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
using System.Windows.Forms;
namespace IGK.ICore.WinCore.Configuration
{
    public class CoreParameterControl : 
        CoreParameterItemBase, 
        ICoreParameterItem ,
        ICoreParameterControl 
    {
        protected ICoreControl c_ctr;
        /// <summary>
        /// get the control key
        /// </summary>
        public ICoreControl Control
        {
            get{
                return this.c_ctr ;
            }
        }
        object ICoreParameterControl.Control{
            get {
                return this.c_ctr;
            }
        }
        public CoreParameterControl(string name, string captionkey, ICoreControl control):base(name, captionkey)
        {        
            this.c_ctr = control;
        }
    }
}

