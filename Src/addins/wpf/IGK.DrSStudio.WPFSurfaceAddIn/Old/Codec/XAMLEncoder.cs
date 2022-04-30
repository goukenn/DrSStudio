

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAMLEncoder.cs
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
file:XAMLEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Markup;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec ;
    [CoreCodec("XMLDecoder", "text/xaml", "xaml")]
    public sealed class XAMLEncoder : CoreEncoderBase 
    {
        public override bool Save(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            if (surface is WinUI.WPFHostSurface)
            {
                WinUI.WPFHostSurface d = surface as WinUI.WPFHostSurface;
                System.Xml.XmlWriterSettings setting = new System.Xml.XmlWriterSettings();
                setting.Indent = true;
                setting.CloseOutput = true;
                System.Xml.XmlWriter xwriter = System.Xml.XmlWriter.Create(filename,
                    setting);
                XamlWriter.Save(d.RootElement , xwriter );
                xwriter.Close();
                return true;
            }
            return false;
        }
    }
}

