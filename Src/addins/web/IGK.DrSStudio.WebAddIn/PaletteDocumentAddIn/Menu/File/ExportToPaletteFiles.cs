

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportToPaletteFiles.cs
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
﻿using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [DrSStudioMenu("File.ExportTo.IGKWebPalette", 0x0300)]
    class ExportToPaletteFiles : PaletteMenuBase
    {
        protected override bool PerformAction()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "IGKWEB Palette |*.gkpal";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    var c = this.CurrentSurface.CurrentLayer.Elements.ToArray();
                    var p = CoreXmlElement.CreateXmlNode("palette");
                    int i = 1;
                    foreach (var item in c)
                    {
                        var g = p.Add("item");
                        g["name"] = "color_"+i;
                        g["color"] =(item as ICoreBrushOwner).GetBrush(enuBrushMode.Fill).Colors[0].ToString(true);
                        i++;
                    }
                    System.IO.File.WriteAllText(sfd.FileName, p.RenderXML(null));
                    
                }
            }


            return false;
        }
    }
}
