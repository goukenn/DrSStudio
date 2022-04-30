

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BlendColorMenu.cs
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
file:BlendColorMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Effects.Menu
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
   [IGK.DrSStudio.Menu.CoreMenu("Image.Effects.BlendColorMenu", 3)]
    class BlendColorMenu :  ImageMenuBase
    {
       protected override bool PerformAction()
       {
           CoreBitmapData data = CoreBitmapData.FromBitmap(this.ImageElement.Bitmap);
           UnaryPixelOp op = new  UnaryPixelOps.BlendConstant(Colorf.FromFloat (0.5f, Colorf.Blue));
           op.Apply(data);
           this.ImageElement.SetBitmap(data.ToBitmap(), false);
           if (this.CurrentSurface !=null) 
               this.CurrentSurface.Invalidate();
           return base.PerformAction();
       }
    }
}

