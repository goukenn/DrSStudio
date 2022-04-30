

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ITextSegment.cs
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
file:ITextSegment.cs
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
namespace IGK.DrSStudio.XMLEditorAddIn.Segment
{
    using IGK ;
    public interface ITextSegment
    {
        /// <summary>
        /// get or set the value of this segment
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// get the color attached to this segment
        /// </summary>
        Colorf Color { get; }
        /// <summary>
        /// get the text editor font style
        /// </summary>
        FontStyle FontStyle { get; }
        /// <summary>
        /// get the segment type, ReservedWord, Word, Symbol or other type
        /// </summary>
        string SegmentType { get; set; }
        /// <summary>
        /// raise when segment type changed
        /// </summary>
        event EventHandler SegmentTypeChanged;
        /// <summary>
        /// raised when segment value changed
        /// </summary>
        event EventHandler SegmentValueChanged;
    }
}

