

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreGKLCapDecoder.cs
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
file:CoreGKLCapDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec;
    [CoreCodec("GKCLDecoder", "gkds/line-cap", "gklc")]
    public class CoreGKLCapDecoder : CoreDecoderBase 
    {
        public override bool Open(IGK.DrSStudio.WinUI.ICoreWorkbench bench, string filename)
        {
            IGK.DrSStudio.Codec.IXMLDeserializer deseri = null;
            Type t = null;
            System.Reflection.MethodInfo f = null;
            t = CoreSystem.GetWorkingObjectType(CoreConstant.DRAWING2D_SURFACE);
            if (t!= null)
            f = t.GetMethod("CreateSurface", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public );
            if ((t == null) || (f==null))
                return false ;
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                deseri = CoreXMLDeserializer.Create(fs);
                if (deseri.ReadToDescendant("Path"))
                {
                    PathElement obj = CoreSystem.CreateWorkingObject(deseri.Name) as PathElement;
                    if (obj != null)
                    {
                        obj.Deserialize(deseri);
                        Core2DDrawingDocument doc = new GLCapDocument ("32","32");
                        doc.CurrentLayer.Elements.Add(obj);
                       IGK.DrSStudio.WinUI.ICoreWorkingSurface v_s =  f.Invoke(null, new object[] { null, new Core2DDrawingDocument[] { doc } })
                           as IGK.DrSStudio.WinUI.ICoreWorkingSurface ;
                       if (v_s != null)
                       {
                           bench.Surfaces.Add(v_s);
                       }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

