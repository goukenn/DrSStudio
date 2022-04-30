

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryRenderer.cs
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
file:HistoryRenderer.cs
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
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    static class HistoryRenderer
    {
        public static Colorf HistoryBackgroundColor { get { return CoreRenderer.GetColor("HistoryBackgroundColor", Colorf.FromFloat (0.4f)); } }
        public static Colorf HistoryForeColor { get { return CoreRenderer.GetColor("HistoryForeColor", Colorf.White ); } }
        public static Colorf HistoryBorderColor { get { return CoreRenderer.GetColor("HistoryBorderColor", Colorf.Transparent ); } }
        public static Colorf HistoryBackgroundStartColor { get { return CoreRenderer.GetColor("HistoryBackgroundStartColor", Colorf.FromFloat (0.4f)); } }
        public static Colorf HistoryBackgroundEndColor { get { return CoreRenderer.GetColor("HistoryBackgroundEndColor", Colorf.FromFloat (0.6f)); } }
        public static Colorf HistoryOverForeColor { get { return CoreRenderer.GetColor("HistoryOverForeColor", Colorf.Black); } }
        public static Colorf HistoryOverStartColor { get { return CoreRenderer.GetColor("HistoryOverStartColor", Colorf.Aquamarine); } }
        public static Colorf HistoryOverEndColor { get { return CoreRenderer.GetColor("HistoryOverEndColor", Colorf.LightBlue); } }
        public static Colorf HistorySelectedStartColor { get { return CoreRenderer.GetColor("HistorySelectedStartColor", Colorf.Orange ); } }
        public static Colorf HistorySelectedEndColor { get { return CoreRenderer.GetColor("HistorySelectedEndColor", Colorf.Yellow); } }
        public static Colorf HistorySelectedForeColor { get { return CoreRenderer.GetColor("HistorySelectedForeColor", Colorf.Black); } }
    }
}

