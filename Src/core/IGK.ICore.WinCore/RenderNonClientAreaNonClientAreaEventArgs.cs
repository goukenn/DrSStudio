

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RenderNonClientAreaNonClientAreaEventArgs.cs
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
file:RenderNonClientAreaNonClientAreaEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    public class RenderNonClientAreaNonClientAreaEventArgs: EventArgs 
    {
        private System.Drawing.Graphics g;
        private System.Drawing.Rectangle rectangle;
        private System.Drawing.Region rg;
        public Region Region { get { return rg; } }
        public Rectangle Rectangle { get { return rectangle; } }
        public Graphics Graphics { get { return g; } }
        public RenderNonClientAreaNonClientAreaEventArgs(System.Drawing.Graphics g, System.Drawing.Rectangle rectangle, System.Drawing.Region rg)
        {
            this.g = g;
            this.rectangle = rectangle;
            this.rg = rg;
        }
    }
}

