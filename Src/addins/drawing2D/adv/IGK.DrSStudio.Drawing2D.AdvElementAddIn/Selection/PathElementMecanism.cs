

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathElementMecanism.cs
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
file:PathElementMecanism.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IGK.DrSStudio.Drawing2D.Selection
{
    using IGK.ICore.Drawing2D.Mecanism;

    public class PathElementMecanism : Core2DDrawingSurfaceMecanismBase<PathElement >
    {
      
        protected bool SelectElement(Vector2f location)
        {
            foreach (ICore2DDrawingLayeredElement  l in this.CurrentLayer.Elements)
            {
                if (l.Contains(location) && l is PathElement)
                {
                    this.CurrentLayer.Select(l);
                    base.Element = l as PathElement ;
                    return true;
                }
            }
            this.CurrentLayer.Select(null);
            this.DisposeSnippet();
            return false;
        }
    }
}

