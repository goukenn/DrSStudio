

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PaletteFileDecoder.cs
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
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    [CoreCodec("IGKWEBPAL", "gkds/gkpal", "gkpal", Description="Open Palette file used in the iGKWeb Framework")]
    class PaletteFileDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            if (!File.Exists(filename))
                return false;
            IGK.ICore.Xml.CoreXmlElement c = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("dummy");
            System.Xml.XmlReader xr  =null;
            try
            {
                xr = System.Xml.XmlReader.Create(filename);
                while (xr.Read())
                {
                    if (xr.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        c.LoadString(xr.ReadOuterXml());
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            finally
            {
                xr.Close();
            }
            
            PaletteDrawingSurface v_surface = new PaletteDrawingSurface();
            var t = v_surface.CurrentDocument.CurrentLayer.Elements.ToArray();
            int i = 0;
            foreach (var g in c.getElementsByTagName("item"))
            {
                if (i >= t.Length)
                    break;
                var s = g["color"];
                if (s != null)
                {
                    (t[i] as ICoreBrushOwner).GetBrush(enuBrushMode.Fill).SetSolidColor(
                        Colorf.FromString(s));
                    (t[i] as Core2DDrawingLayeredElement ).Id = g["name"];
                    i++;
                }
            }
            v_surface.FileName = filename;
            v_surface.NeedToSave = false;
            bench.AddSurface(v_surface, selectCreatedSurface );
            return true;
        }
    }
}
