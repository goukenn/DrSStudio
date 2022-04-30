

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathBrushLineStyle.cs
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
file:PathBrushLineStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.PathBrushEditorAddIn
{
    public class PathBrushLineStyle : IGK.DrSStudio.Drawing2D.CorePathBrushStyleBase  
    {
        public override string Id
        {
            get { return "LineBrush"; }
        }
        public PathBrushLineStyle()
        {
        }
        public override void Generate(System.Drawing.Drawing2D.GraphicsPath v_path)
        {
            base.Generate(v_path);
        }
    }
}

