

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLBitmapEditorSurface.cs
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
file:OGLBitmapEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace IGK.DrSStudio.OGLBitmapEditor.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [IGK.DrSStudio.CoreSurface (OGLBitmapConstant.SURFACE_NAME )]
    public class OGLBitmapEditorSurface : XDrawing2DSurface 
    {
        public override IGK.DrSStudio.ICoreWorkingMecanism IsToolValid(System.Type t)
        {
            if (t.IsAssignableFrom(typeof(WorkingElements.PixelMarquer)))
            {
                return base.IsToolValid(t);
            }
            return null;
        }
        public override void Save()
        {
            base.Save();
        }
        public override void SaveAs(string filename)
        {
            base.SaveAs(filename);
        }
    }
}

