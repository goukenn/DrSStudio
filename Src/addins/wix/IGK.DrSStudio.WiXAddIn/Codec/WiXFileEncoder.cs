

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXEncoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
file:WiXEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.WiXAddIn.Codec
{
    [FileCodec("WiX", "application/igk-wix-file", WiXConstant.WIXPROJECTFILE_EXTENSION)]
    class WiXFileEncoder  : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            WiXDocument[] v_document = documents.ConvertTo<WiXDocument>();
            if ((v_document ==null) || (v_document.Length == 0))
            return false;
            StringBuilder v_sb = new StringBuilder ();
            XmlWriterSettings v_setting = new XmlWriterSettings ();
            v_setting.Indent = true ;
            v_setting.OmitXmlDeclaration = false ;
            WiXWriter v_writer =  WiXWriter.Create(v_sb,v_setting);
            v_writer.Visit(v_document );
            v_writer.Flush();
            File.WriteAllText (filename, v_sb .ToString());
            return true ;
        }
    }
}
