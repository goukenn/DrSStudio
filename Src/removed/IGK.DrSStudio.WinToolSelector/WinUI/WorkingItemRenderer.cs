

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WorkingItemRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WorkingItemRenderer.cs
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
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK ;
    public static class WorkingItemRenderer
    {
        public static Colorf WinToolSelectorBackgroundColor { get { return CoreRenderer.GetColor("WinToolSelectorBackgroundColor", Colorf.FromFloat (0.2f)); } }
        public static Colorf WinToolSelectorForeColor { get { return CoreRenderer.GetColor("WinToolSelectorForeColor", Colorf.White); } }
        public static Colorf WinToolSelectorGroupStartColor { get { return CoreRenderer.GetColor ("WinToolSelectorGroupStartColor", Colorf.FromFloat (.5f)) ;} }
        public static Colorf WinToolSelectorGroupEndColor { get { return CoreRenderer.GetColor("WinToolSelectorGroupEndColor", Colorf.FromFloat(.45f)); } }
        public static Colorf WinToolSelectorGroupBorderColor { get { return CoreRenderer.GetColor("WinToolSelectorGroupBorderColor", Colorf.FromFloat(.25f)); } }
        public static Colorf WinToolSelectorGroupForeColor { get { return CoreRenderer.GetColor("WinToolSelectorGroupForeColor", Colorf.FromFloat(.95f)); } }
        public static Colorf WinToolSelectorStartColor { get { return CoreRenderer.GetColor("WinToolSelectorStartColor", Colorf.FromFloat(.35f)); } }
        public static Colorf WinToolSelectorEndColor { get { return CoreRenderer.GetColor("WinToolSelectorEndColor", Colorf.FromFloat (.3f)); } }
        public static Colorf WinToolSelectorSelectedForeColor { get { return CoreRenderer.GetColor("WinToolSelectorSelectedForeColor", Colorf.SkyBlue ); } }
        public static Colorf WinToolSelectorOverStartColor { get { return CoreRenderer.GetColor("WinToolSelectorOverStartColor", Colorf.FromFloat(.7f)); } }
        public static Colorf WinToolSelectorOverEndColor { get { return CoreRenderer.GetColor("WinToolSelectorOverEndColor", Colorf.FromFloat(.7f)); } }
        public static Colorf WinToolSelectorOverForeColor { get { return CoreRenderer.GetColor("WinToolSelectorOverForeColor", Colorf.Black ); } }
        public static Colorf WinToolSelectorSelectedStartColor { get { return CoreRenderer.GetColor("WinToolSelectorSelectedStartColor", Colorf.FromFloat(.6f)); } }
        public static Colorf WinToolSelectorSelectedEndColor { get { return CoreRenderer.GetColor("WinToolSelectorSelectedEndColor", Colorf.FromFloat(.6f)); } }
        public static Colorf WinToolSelectorBorderColor { get { return CoreRenderer.GetColor("WinToolSelectorBorderColor", Colorf.FromFloat (0.2f, .9f)); } }    }
}

