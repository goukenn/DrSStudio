

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingCopyLayerMenuAttribute.cs
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
file:IGKD2DDrawingCopyLayerMenuAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    public class IGKD2DDrawingCopyLayerMenuAttribute : IGKD2DDrawingLayerMenuAttribute
    {
        public IGKD2DDrawingCopyLayerMenuAttribute():base("CopyLayer",0x500)
        {
        }
        public IGKD2DDrawingCopyLayerMenuAttribute(string name, int index)
            : base("CopyLayer." + name, index)
        {
        }
    }
}

