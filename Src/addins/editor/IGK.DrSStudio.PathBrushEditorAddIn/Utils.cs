

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Utils.cs
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
file:Utils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.PathBrushEditorAddIn
{
    internal class Utils
    {
        public static IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle[] LoadItems()
        {
            List<IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle> v_im  = new List<IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle>();
            v_im.Add(new SinPathBrushStyle());
            v_im.Add(new CosPathBrushStyle());
            v_im.Add(new SinPPathBrushStyle());
            v_im.Add(new PathBrushLineStyle ());
            return v_im.ToArray();
        }
    }
}

