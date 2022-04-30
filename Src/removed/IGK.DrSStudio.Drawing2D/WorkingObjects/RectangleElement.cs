

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RectangleElement.cs
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
file:RectangleElement.cs
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
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    [Serializable ()]
    [Core2DDrawingStandardItem("Rectangle", 
        typeof (Mecanism),
        Keys = Keys.R)]
    public class RectangleElement :
        Core2DDrawingDualBrushBoundElement ,
        ICore2DRectangleElement
    {  
        protected override void GeneratePath()
        {           
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            v_path.AddRectangle(this.Bound);
            this.SetPath(v_path); 
        }
        //for gdi purpose
        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }
        public new class Mecanism : Core2DDrawingDualBrushBoundElement.Mecanism<RectangleElement >
        {
        }
    }
}

