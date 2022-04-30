

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICorePenStruct.cs
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
file:ICorePenStruct.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent the pen core struct
    /// </summary>
    public interface ICorePenStruct
    {
                // Summary:
        //     Gets or sets the alignment for this System.Drawing.Pen.
        //
        // Returns:
        //     A System.Drawing.Drawing2D.PenAlignment that represents the alignment for
        //     this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not a member of System.Drawing.Drawing2D.PenAlignment.
        //
        //   System.ArgumentException:
        //     The System.Drawing.Pen.Alignment property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         enuPenAlignment Alignment { get; set; }
        // Summary:
        //     Gets or sets the cap style used at the end of the dashes that make up dashed
        //     lines drawn with this System.Drawing.Pen.
        //
        // Returns:
        //     One of the System.Drawing.Drawing2D.DashCap values that represents the cap
        //     style used at the beginning and end of the dashes that make up dashed lines
        //     drawn with this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not a member of System.Drawing.Drawing2D.DashCap.
        //
        //   System.ArgumentException:
        //     The System.Drawing.Pen.DashCap property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         enuDashCap DashCap { get; set; }
        //
        //
        // Summary:
        //     Gets or sets the style used for dashed lines drawn with this System.Drawing.Pen.
        //
        // Returns:
        //     A System.Drawing.Drawing2D.DashStyle that represents the style used for dashed
        //     lines drawn with this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The System.Drawing.Pen.DashStyle property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
        enuDashStyle DashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the cap style used at the end of lines drawn with this System.Drawing.Pen.
        //
        // Returns:
        //     One of the System.Drawing.Drawing2D.LineCap values that represents the cap
        //     style used at the end of lines drawn with this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not a member of System.Drawing.Drawing2D.LineCap.
        //
        //   System.ArgumentException:
        //     The System.Drawing.Pen.EndCap property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         enuLineCap EndCap { get; set; }
        //
        // Summary:
        //     Gets or sets the join style for the ends of two consecutive lines drawn with
        //     this System.Drawing.Pen.
        //
        // Returns:
        //     A System.Drawing.Drawing2D.LineJoin that represents the join style for the
        //     ends of two consecutive lines drawn with this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The System.Drawing.Pen.LineJoin property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         enuLineJoin LineJoin { get; set; }
        //
        // Summary:
        //     Gets or sets the limit of the thickness of the join on a mitered corner.
        //
        // Returns:
        //     The limit of the thickness of the join on a mitered corner.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The System.Drawing.Pen.MiterLimit property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         float MiterLimit { get; set; }
        //
        // Summary:
        //     Gets or sets the cap style used at the beginning of lines drawn with this
        //     System.Drawing.Pen.
        //
        // Returns:
        //     One of the System.Drawing.Drawing2D.LineCap values that represents the cap
        //     style used at the beginning of lines drawn with this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not a member of System.Drawing.Drawing2D.LineCap.
        //
        //   System.ArgumentException:
        //     The System.Drawing.Pen.StartCap property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         enuLineCap StartCap { get; set; }
        //
        // Summary:
        //     Gets or sets the width of this System.Drawing.Pen, in units of the System.Drawing.Graphics
        //     object used for drawing.
        //
        // Returns:
        //     The width of this System.Drawing.Pen.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The System.Drawing.Pen.Width property is set on an immutable System.Drawing.Pen,
        //     such as those returned by the System.Drawing.Pens class.
         float Width { get; set; }
    }
}

