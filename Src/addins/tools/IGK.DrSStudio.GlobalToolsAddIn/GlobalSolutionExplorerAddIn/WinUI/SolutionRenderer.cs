

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SolutionRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    public static class SolutionRenderer 
    {
        static SolutionRenderer() {
            CoreRendererBase.InitRenderer(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);
        }
        public static Colorf SolutionTreeViewBackgroundColor { get { return CoreRenderer.GetColor("SolutionTreeViewBackgroundColor", Colorf.FromFloat (0.6f)); } }
        public static Colorf SolutionTreeViewForeColor { get { return CoreRenderer.GetColor("SolutionTreeViewForeColor", Colorf.Black); } }
        public static Colorf SolutionTreeViewDarkRowColor { get { return CoreRenderer.GetColor("SolutionTreeViewDarkRowColor", Colorf.FromFloat (0.75f)); } }
        public static Colorf SolutionTreeViewLightRowColor { get { return CoreRenderer.GetColor("SolutionTreeViewLightRowColor", Colorf.FromFloat (0.85f)); } }

    }
}
