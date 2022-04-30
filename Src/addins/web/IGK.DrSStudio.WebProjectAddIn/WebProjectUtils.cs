

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebUtils.cs
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
file:WebUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore;
namespace IGK.DrSStudio.WebProjectAddIn
{
    /// <summary>
    /// represent a web utils class
    /// </summary>
    static class WebProjectUtils
    {
        public static string GetCss(Rectanglef rc)
        {
            return GetCss(Rectanglei.Round(rc));
        }
        public static string GetCss(Rectanglei rc)
        {
            return string.Format("left:{0}px; top:{1}px; width:{2}px; height:{3}px;",
                rc.X,
                rc.Y,
                rc.Width,
                rc.Height);
        }
        public static string GetBrushStyle(Core2DDrawingLayeredElement element, Brush br, string folder)
        {
            if ((element == null) || (br == null))
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool v_saveigm = true;
            if (br is SolidBrush)
            {
                SolidBrush sbr = br as SolidBrush;
                if (sbr.Color.A == 255)
                {
                    sb.Append("background-color: " + (sbr.Color.CoreConvertTo<Colorf>()).ToString(true));
                    v_saveigm = false;
                }
            }
            if (v_saveigm && (br != null))
            {
                string str = SaveAsBitmap(element,sb, folder);
                if (!string.IsNullOrEmpty(str))
                {
                          sb.Append("background-image:url('Res/"+Path.GetFileName (str)+"'); ");
                }
            }
            if (sb.Length > 0)
                sb.Append(";");
            return sb.ToString();
        }
        public static  string GetPenStyle(Pen pen)
        {
            if ((pen == null)||(pen.Color.A != 255))
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            switch (pen.PenType)
            {
                case PenType.HatchFill:
                    break;
                case PenType.LinearGradient:
                    break;
                case PenType.PathGradient:
                    break;
                case PenType.SolidColor:
                    sb.Append(Int32.Parse(pen.Width.ToString()) + "px " + (pen.Color.CoreConvertTo<Colorf>()).ToString(true)+" ");
                    switch (pen.DashStyle)
	                    {
		                    case DashStyle.Custom:
                             break;
                            case DashStyle.Dash:
                             sb.Append("dashed");
                             break;
                            case DashStyle.DashDot:
                             break;
                            case DashStyle.DashDotDot:
                             break;
                            case DashStyle.Dot:
                             sb.Append("dotted");
                             break;
                            case DashStyle.Solid:
                             sb.Append("solid");
                             break;
                        default:
                             break;
	                    }
                        break ;
                case PenType.TextureFill:
                    break;
                default:
                    break;
            }
            if (sb.Length > 0)
                sb.Append(";");
            return sb.ToString();
        }
        public static string GetBorderStyle(Core2DDrawingLayeredElement element)
        {
            return string.Empty;
        }
        public static string SaveAsBitmap(Core2DDrawingLayeredElement element, StringBuilder sb , string tempFolder)
        {
            string uri = string.Empty;
            Rectanglei v_rc = Rectanglei.Ceiling(element.GetBound());
            float v_size = 1.0f;
            ICorePen v_br = (element.GetBrush(enuBrushMode.Stroke) as ICorePen );
            if (v_br != null)
                v_size = v_br.Width /2.0f;
            float dx = v_rc.X;
            float dy = v_rc.Y;
            int w = v_rc.Width;
            int h = v_rc.Height;
            v_rc.Inflate((int)v_size, (int)v_size);
            using (Bitmap bmp = new Bitmap(v_rc.Width + 1, v_rc.Height + 1))
            {
                float ex = (w - 1) / (float)w;
                float ey = (h - 1) / (float)h;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    var v_g=WinCoreBitmapDeviceVisitor.Create(g);
                    v_g.ScaleTransform(ex, ey, enuMatrixOrder.Append);
                    v_g.TranslateTransform(-v_rc.X, -v_rc.Y, enuMatrixOrder.Append);
                    element.Draw(v_g);
                }
                string d = Path.Combine(tempFolder, "Res");
                if (!Directory.Exists(d))
                    Directory.CreateDirectory(d);
                uri = d + "/" + element.Id + ".png";
                bmp.Save(uri, ImageFormat.Png);
            }
            sb.Append ( WebProjectUtils.GetCss (v_rc ) +"");
            return uri;
        }
        internal static string GetElementStyle(Core2DDrawingLayeredElement element, string TemporyFolder)
        {
            if (element == null) return string.Empty;
            Rectanglei v_rc =Rectanglei.Round ( element.GetBound());
            //gen resource
            string v_ts = string.Empty ;
            ICoreBrush v_br = null;
           StringBuilder sb = new StringBuilder ();
            sb.Append (WebProjectUtils.GetBorderStyle(element));
            sb.Append (WebProjectUtils.GetBrushStyle(element, ((v_br= element.GetBrush (enuBrushMode.Fill)) !=null? WinCoreBrushRegister.GetBrush ( v_br) :null), TemporyFolder));
            v_br = (element.GetBrush(enuBrushMode.Stroke));
            if (v_br is ICorePen)
            {
                Pen p = WinCoreBrushRegister.GetPen(v_br as ICorePen ) ;
                v_ts = GetPenStyle(p);
                sb.Append(string.IsNullOrEmpty(v_ts) ? null : "border:" + v_ts);
            }
            //no layout engine
            sb.Append("position:absolute; ");
            sb.Append ( WebProjectUtils.GetCss(v_rc));
            return sb.ToString();
        }
    }
}

