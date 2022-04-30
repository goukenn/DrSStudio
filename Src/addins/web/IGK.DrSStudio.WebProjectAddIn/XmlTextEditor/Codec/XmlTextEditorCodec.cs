

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlTextEditorCodec.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XmlTextEditorCodec.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.XmlTextEditor.Codec
{
    using IGK.DrSStudio.XmlTextEditor.WinUI;
    [CoreCodec ("XmlEditor", "text/xml", "xml", Category= CoreConstant.CAT_TEXT )]
    class XmlTextEditorCodec : CoreDecoderBase 
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool attachToWorkbench)
        {
            XmlEditorSurface v_surface = XmlEditorManager.Instance.GetSurface(bench);
            if (v_surface != null)
            {
                if (attachToWorkbench && ( v_surface != bench.CurrentSurface))
                {
                    bench.CurrentSurface = v_surface;
                }
                return v_surface.Open(filename);
            }
            return false;
        }
    }
}

