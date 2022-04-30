

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAMLDecoder.cs
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
file:XAMLDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.IO;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec ;
    [CoreCodec("XMLDecoder", "text/xaml", "xaml")]
    public sealed class XAMLDecoder : CoreDecoderBase
    {
        public override bool Open(DrSStudio.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            using (FileStream fs = File.Open(filename , FileMode.Open ))
            {
              object obj =    XamlReader.Load(fs);
              if (obj != null)
              {
                  WinUI.WPFHostSurface surface = new IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFHostSurface();
                  surface.LoadObject(obj);
                  bench.Surfaces.Add(surface);
                  if (selectCreatedSurface)
                      bench.CurrentSurface = surface;
                  return true;
              }
            }
            return false ;
        }

      
    }
}

