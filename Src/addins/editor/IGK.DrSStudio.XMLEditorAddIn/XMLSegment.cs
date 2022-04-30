

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLSegment.cs
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
file:XMLSegment.cs
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
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.XMLEditorAddIn
{
    using IGK ;
    using IGK.ICore.WinCore;
    /// <summary>
    /// represent the base class of all segment
    /// </summary>
    public class XMLSegment : Segment.TextEditorSegmentBase ,
        IXMLDocumentSegment 
    {
        public XMLSegment(string value)
            : base(value)
        { 
        }
        public override void Draw(IGK.DrSStudio.XMLEditorAddIn.Segment.TextEditorRenderingEventArgs e)
        {
            switch (this.SegmentType)
            {
                //case "Symbol":
                //    break;
                //case "Space":
                //    break;
                //case "Tab":
                //    break;
                //case "Word":
                default :
                    Font ft =new Font (e.Font, this.FontStyle );
                    e.Graphics.DrawString(this.Value,
                        ft,
                        WinCoreBrushRegister.GetBrush(this.Color),
                        e.OffsetX, e.OffsetY );
                    e.OffsetX += Utils.StringMeasurement.MeasureString(this.Value, ft).Width;
                    ft.Dispose ();
                    break;
            }
        }
        public override string ToString()
        {
            return this.Value ;
        }
        #region IXMLDocumentSegment Members
        public int Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region IXMLDocumentSegment Members
        public void Draw(XMLRendereringEventArgs g)
        {
            this.Draw(g as IGK.DrSStudio.XMLEditorAddIn.Segment.TextEditorRenderingEventArgs);
        }
        #endregion
    }
}

