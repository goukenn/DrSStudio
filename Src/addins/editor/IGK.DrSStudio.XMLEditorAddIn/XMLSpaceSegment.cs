

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLSpaceSegment.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XMLSpaceSegment.cs
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
namespace IGK.DrSStudio.XMLEditorAddIn
{
    /// <summary>
    /// represent a default space segment;
    /// </summary>
    public class XMLSpaceSegment : XMLSegment 
    {
        const char INT_VIEW = '_';
        public XMLSpaceSegment():base(" ")
        {
        }
        public override void Draw(IGK.DrSStudio.XMLEditorAddIn.Segment.TextEditorRenderingEventArgs e)
        {
            e.OffsetX += GetSpaceWidth(e.Font);
        }
        private int GetSpaceWidth(System.Drawing.Font font)
        {
            return TextRenderer.MeasureText(INT_VIEW.ToString(), font, new System.Drawing.Size(
                short.MaxValue, short.MinValue)).Width ;
        }
    }
}

