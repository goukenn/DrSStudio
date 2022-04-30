

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlRenderer.cs
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
file:HtmlRenderer.cs
*/
using IGK.DrSStudio.WebProjectAddIn.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.Xml;
using IGK.ICore.WinCore;
namespace IGK.DrSStudio.WebProjectAddIn
{
    /// <summary>
    /// represent a html renderer
    /// </summary>
    public static class HtmlRenderer
    {
        internal static void RenderBackground(WorkingObjects.WebHtmlElementBase htmlElementBase,
            WebRenderingEventArgs e)
        {
            Brush br = WinCoreBrushRegister.GetBrush(htmlElementBase.Style.FillBrush);
            if (br == null) return;
            e.Graphics.FillRectangle(br, e.Rectangle);
        }
        internal static void RenderContent(WorkingObjects.WebHtmlElementBase nodeElement, 
            WebRenderingEventArgs e)
        {
            Rectanglei v_rc = e.Rectangle;
            string s = CoreXmlUtility.GetStringValue((nodeElement.Node as CoreXmlElement ).Content);
            if (string.IsNullOrEmpty (s ) )return ;
           // e.Graphics.DrawString (s, GetFont(nodeElement), GetForeBrush(nodeElement), v_rc, GetStringFormat(nodeElement));
        }
        private static System.Drawing.StringFormat GetStringFormat(WebHtmlElementBase nodeElement)
        {
            return null;
        }
        private static System.Drawing.Brush GetForeBrush(WebHtmlElementBase nodeElement)
        {
            return null;
        }
        private static System.Drawing.Font GetFont(WebHtmlElementBase nodeElement)
        {
            return null;
        }
        internal static void RenderBorder(WebHtmlElementBase item, WebRenderingEventArgs e )
        {
            DrawBorder(e.Graphics, e.Rectangle,
                Colorf.Black, 1, ButtonBorderStyle.Solid,
                Colorf.Black, 1, ButtonBorderStyle.Solid,
                Colorf.Black, 1, ButtonBorderStyle.Solid,
                Colorf.Black, 1, ButtonBorderStyle.Solid);
        }

        private static void DrawBorder(Graphics graphics, 
            Rectanglei rectanglei, 
            Colorf colorf1, int p1, 
            ButtonBorderStyle buttonBorderStyle1, 
            Colorf colorf2, int p2, 
            ButtonBorderStyle buttonBorderStyle2, 
            Colorf colorf3, int p3,
            ButtonBorderStyle buttonBorderStyle3, 
            Colorf colorf4,
            int p4,
            ButtonBorderStyle buttonBorderStyle4)
        {
            
        }
    }
}

