

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreExtension.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WinCoreExtension.cs
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore
{
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Resources;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    
    /// <summary>
    /// Used to resolve link and extension for 8.0 to 9.0
    /// </summary>
    public static class WinCoreExtensions
  {

        
      /// <summary>
      /// 
      /// </summary>
      /// <param name="webControl"></param>
      /// <param name="document"></param>
      public static void IGKApplyBrowserDocumentText(this WebBrowser webControl,
          CoreXmlWebDocument document)
      {
          if (webControl == null)
              return;
          document.ForWebBrowserDocument = true;
          if ((webControl.Document == null) ||
           (webControl.Document.Body == null))
          {
              webControl.DocumentText = document.RenderXML(null);
          }
          else
          {
              webControl.Document.Body.InnerHtml = (document.Body.RenderInnerHTML(null));

          }
      }


      public static CoreFont ToCoreFont(this Font font)
      {
          if (font == null)
              return null;
          CoreFont f = string.Format("FontName:{0}; Size:{1}pt;", font.Name, font.SizeInPoints);
          return f;
      }

        public static StringFormat ToStringFormat(this ICoreFont font)
        {
            if (font == null)
                return new StringFormat(StringFormat.GenericTypographic);

            StringFormat sf = new StringFormat(StringFormat.GenericTypographic)
            {
                Alignment = (StringAlignment)font.HorizontalAlignment,
                LineAlignment = (StringAlignment)font.VerticalAlignment
            };
            sf.FormatFlags |= StringFormatFlags.NoClip| StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox ;
            if (!font.WordWrap)
                sf.FormatFlags |= StringFormatFlags.NoWrap;
            switch (font.HotKeyPrefix)
            {
                case enuHotKeyPrefix.Disabled:
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                    break;
                case enuHotKeyPrefix.Hide:
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
                    break;
                case enuHotKeyPrefix.Show:
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
                    break;
                default:
                    break;
            }
            return sf;
        }
        //public static StringFormat ToStringFormat(this ICoreFont font, StringFormatFlags formatFlag)
        //{
        //    StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
        //    sf.Alignment = (StringAlignment)font.HorizontalAlignment;
        //    sf.LineAlignment = (StringAlignment)font.VerticalAlignment;
        //    sf.FormatFlags = formatFlag;
        //    return sf;
        //}


        #region drawing 2D Extentions

        public static void Draw(this ICoreBitmap bmp, ICoreGraphics device, int x, int y)
        {
            if ((bmp == null) || (device == null))
                return;
            var ob = device.Save();
            device.TranslateTransform(x, y, enuMatrixOrder.Append);
            device.Draw(bmp, new Rectanglei (0,0,bmp.Width, bmp.Height));
                device.Restore (ob);
        }
        /// <summary>
        /// draw data to bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="data"></param>
        public static void Draw(this ICoreBitmap bmp, Bitmap data)
        {
            Draw(bmp, data, false, 
                new Rectanglei(0, 0, bmp.Width, bmp.Height), 
                enuFlipMode.None);
        }
        public static void Draw(this ICoreBitmap bmp, Bitmap data, Rectanglei rc)
        {
            Draw(bmp, data, false,
                rc,
                enuFlipMode.None);
        }
        public static void Draw(this ICoreBitmap bmp, Bitmap data, bool proportional, Rectanglei rec, enuFlipMode flipmode)
        {
            if ((data == null) || (bmp == null))
                return;
            Graphics g = Graphics.FromImage(data);
            var c = CreateGraphics(g);
            c.InterpolationMode = enuInterpolationMode.Hight;
            bmp.Draw(c, rec, proportional,  flipmode );
            g.Dispose();
            c.Dispose();
        }
        public static ICoreGraphics CreateGraphics(Graphics g)
        {
            WinCoreBitmapDeviceVisitor dev = WinCoreBitmapDeviceVisitor.Create(g);
            return dev;
        }

        public static void Draw(this ICore2DDrawingObject obj, Graphics g,
        bool proportional,
        Rectanglei Rectangle,
        enuFlipMode flipMode)
        {
            obj.Draw(CreateGraphics(g),
                Rectangle,
                proportional,
                flipMode);
        }
        public static void Draw(this ICore2DDrawingDocument d, Graphics device, Rectangle rc)
        {
            Draw(d, device, true, enuFlipMode.None, rc.X, rc.Y, rc.Width, rc.Height);
        }
        /// <summary>
        /// Extensions draw a document to a gdi graphics device
        /// </summary>
        /// <param name="d">document to draw</param>
        /// <param name="device">gdi+ device</param>
        public static void Draw(this ICore2DDrawingDocument d, Graphics device)
        {
            Draw(d, device, true, enuFlipMode.None, 0, 0, d.Width, d.Height);
        }
        /// <summary>
        /// Extension draw a document to gdi graphics device
        /// </summary>
        /// <param name="d">document to draw</param>
        /// <param name="device">gdi+ device</param>
        /// <param name="rc">rectangle position</param>
        public static void Draw(this ICore2DDrawingDocument d, Graphics device, Rectanglei rc)
        {
            Draw(d, device, true, enuFlipMode.None, rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static void Draw(this ICore2DDrawingDocument d, Graphics device, bool proportional, Rectangle rc, enuFlipMode mode)
        {
            Draw(d, device, proportional, mode, rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static void Draw(this ICore2DDrawingDocument d, Graphics device, bool proportional, enuFlipMode mode, int x, int y, int width, int height)
        {
            ICoreGraphics g = WinCoreBitmapDeviceVisitor.Create(device);
            if (g != null)
            {
                d.Draw(g, proportional, new Rectanglei(x, y, width, height), mode);
            }
        }
        /// <summary>
        /// draw a layered document to graphics device
        /// </summary>
        /// <param name="d"></param>
        /// <param name="device"></param>
        public static void Draw(this ICore2DDrawingLayeredElement d, Graphics device)
        {
            ICoreGraphics g = WinCoreBitmapDeviceVisitor.Create(device);
            if (g != null)
            {
                d.Draw(g);
                g.Release();
            }
        }

        public static void DrawRectangle(this Graphics d, Pen br, Rectanglef rc)
        {
            d.DrawRectangle(br, rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static void DrawString(this Graphics d, string text, Font ft,
                 Brush br
                 , Rectanglef rc)
        {
            using (StringFormat sf = new StringFormat())
            {
                DrawString(d, text, ft, br, rc.X, rc.Y, rc.Width, rc.Height, sf);
            }
        }
        public static void DrawString(this Graphics d, string text, Font ft,
                    Brush br
                    , Rectanglef rc, StringFormat v_sformat)
        {
            DrawString(d, text, ft, br, rc.X, rc.Y, rc.Width, rc.Height, v_sformat);
        }
        /// <summary>
        /// draw string extension for 
        /// </summary>
        /// <param name="d">gdi graphics device</param>
        /// <param name="text">test to draw</param>
        /// <param name="ft">gdi font </param>
        /// <param name="br">brush</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="sformat">string format</param>
        public static void DrawString(this Graphics d, string text, Font ft, Brush br, float x, float y, float width,
            float height, StringFormat sformat)
        {
            d.DrawString(text, ft, br, new RectangleF(x, y, width, height), sformat);
        }
#endregion



        #region "Control Extension"

        public static void SetStyle(this Control ctrl, ControlStyles styles, bool value)
        {
            if (ctrl == null) return;
            MethodInfo v_minfo = ctrl.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (v_minfo != null)
                v_minfo.Invoke(ctrl, new object[] { styles, value });
        }
        public static bool GetStyle(this Control ctrl, ControlStyles style)
        {
            if (ctrl == null) return false;
            MethodInfo v_minfo = ctrl.GetType().GetMethod("GetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (v_minfo != null)
            {
                object obj = v_minfo.Invoke(ctrl, new object[] { style });
                if (obj is bool)
                    return (bool)obj;
            }
            return false;
        }
        #endregion


        public static Rectanglei ToRectanglei(this Rectangle rc)
        {
            return new Rectanglei(rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static Rectanglei ToRectanglei(this RectangleF rc)
        {
            return new Rectanglei((int)rc.X,
                (int)rc.Y,
                (int)rc.Width,
                (int)rc.Height);
        }
        

        static readonly StringFormat sm_sf;
        public static FontFamily FontFamily(this ICoreFont ft)
        {
            if (ft != null)
            {
                ICoreFontInfo c = CoreFont.GetFonts(ft.FontName);
                if (c != null)
                    return (FontFamily)c.FontFamily;                
            }
            return null;
        }
        static WinCoreExtensions()
        {
            sm_sf = new StringFormat();
        }
        public static global::System.Drawing.Drawing2D.Matrix ToGdiMatrix(this IGK.ICore.Matrix matrix)
        {
            if ((matrix == null)||(matrix.Elements == null))
                return null;
            return new System.Drawing.Drawing2D.Matrix(
                     matrix.Elements[0],
                    matrix.Elements[1],
                    matrix.Elements[4],
                    matrix.Elements[5],
                    matrix.Elements[12],
                    matrix.Elements[13]
                    );
        }
        /// <summary>
        /// get document image extension
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public static Bitmap GetDocumentImage(string documentName)
        {
             ICore2DDrawingDocument v_doc = CoreResources.GetDocument(documentName);
             if (v_doc != null)
             {
                 ICoreBitmap v_p = v_doc.ToBitmap();
                 Bitmap v_bmp = v_p.ToGdiBitmap();
                 v_p.Dispose();
                 return v_bmp;
             }
             return null;
        }
      
        public static void SetCursor(this ICoreControl control, Cursor cursor)
        {
            if (control is System.Windows.Forms.Control)
            {
                (control as System.Windows.Forms.Control).Cursor = cursor;
            }
        }
        public static bool Contains(this Rectangle rc, Rectanglei r)
        {
            return rc.Contains(new Rectangle(r.X, r.Y, rc.Width, r.Height));
        }
        public static bool Contains(this Rectangle rc, Vector2f r)
        {
            return rc.Contains(new Point(
                (int)Math.Ceiling(r.X), 
                (int)Math.Ceiling(r.Y))
                );
        }
        public static bool Contains(this Rectangle rc, Vector2i r)
        {
            return rc.Contains(new Point(r.X, r.Y));
        }
        public static void FillRectangle(this ICoreGraphics d, Brush br, int x, int y, int width, int height)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if ((dev == null)||(br ==null))
                return;
            dev.Device.FillRectangle(br, x, y, width, height);
        }
        public static void FillRectangle(this ICoreGraphics d, Brush br, Rectangle rc)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            dev.Device.FillRectangle(br, rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static void FillPath(this ICoreGraphics d, Brush brush, GraphicsPath path)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            dev.Device.FillPath(brush, path);
        }
        #region "Gdi Graphics Extensions"
        public static void FillRectangle(this Graphics d, Brush br, Rectanglef rc)
        {
            if ((d==null) || (br == null)) return;
            d.FillRectangle (br, rc.X, rc.Y, rc.Width, rc.Height);
        }
       
        #endregion
        public static void FillRectangle(this ICoreGraphics d, Brush br, Rectanglef rc)
         {
             FillRectangle(d, br, rc.X,rc.Y, rc.Width, rc.Height);
         }
        public static void FillRectangle(this ICoreGraphics d, Brush br, float x, float y, float width, float height)
        {
            if ((d == null)||(br==null))
                return;
            if (x < 0.0001)
                x = 0;
            if (y < 0.0001f)
                y = 0;
            if (d is WinCoreBitmapDeviceVisitor dev)
            {
                dev.Device.FillRectangle(br, x, y, width, height);
            }
            else
            {
                Colorf cl = Colorf.Black;
                if (br is SolidBrush)
                    cl = (br as SolidBrush).Color.CoreConvertFrom<Colorf>();
                d.FillRectangle(cl, new Rectanglef(x, y, width, height));
            }
        }
        public static void DrawRectangle(this ICoreGraphics d, Pen br, float x, float y, float width, float height)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            if (x < 0.0001)
                x = 0;
            if (y < 0.0001f)
                y = 0;
            dev.Device.DrawRectangle(br, x, y, width, height);
        }
        public static void DrawLine(this ICoreGraphics d,
                Pen pen, float x1, float y1, float x2, float y2)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            dev.Device.DrawLine(pen, x1, y1, x2, y2);
        }
        public static void DrawString(this ICoreGraphics d, string text, Font ft,
                    Brush br
                    , float x , float y )
        {
            using (StringFormat sf = new StringFormat())
            {
                DrawString(d, text, ft, br, new Rectanglef(x, y, 0, 0), sf);
            }
        }
        /// <summary>
        /// draw on string with the string format
        /// </summary>
        /// <param name="d"></param>
        /// <param name="text"></param>
        /// <param name="ft"></param>
        /// <param name="br"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stringFormat"></param>
        public static void DrawString(this ICoreGraphics d, string text, Font ft,
                  Brush br
                  , float x, float y, StringFormat stringFormat)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            dev.Device.DrawString (text, ft, br, x, y, stringFormat);
        }
        public static void DrawString(this ICoreGraphics d, string text, Font ft,
                 Brush br
                 , Rectanglef rc)
        {
            DrawString(d, text, ft, br, rc, sm_sf);
        }
        public static void DrawString(this ICoreGraphics d, string text, Font ft,
                    Brush br
                    ,Rectanglef rc, StringFormat v_sformat)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return;
            dev.Device.DrawString(text, ft, br, rc.X, rc.Y, rc.Width, rc.Height, v_sformat);
        }
        public static Size2f MeasureString(this ICoreGraphics d,
            string text, 
            Font ft,
            Size2f size, 
            StringFormat format)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return Size2f.Empty;
            SizeF s = dev.Device.MeasureString(text, ft, new SizeF(size.Width , size.Height ), format);
            return new Size2f(s.Width, s.Height);
        }
        public static SizeF MeasureString(this ICoreGraphics d, string text, Font ft, SizeF size, StringFormat format)
        {
            WinCoreBitmapDeviceVisitor dev = d as WinCoreBitmapDeviceVisitor;
            if (dev == null)
                return SizeF.Empty ;
            return dev.Device.MeasureString(text, ft, size, format);
        }
        public static void Draw(this ICore2DDrawingDocument d, ICoreGraphics device, int x, int y, int width, int height)
        {
            device.Draw(d, true, new  Rectanglei(x, y, width, height), enuFlipMode.None);
        }
     
     
        public static void RenderTexture(this ICoreBitmap bmp,ICoreGraphics device, float x, float y, float width, float height) {
            TextureBrush cb = CoreBrushRegisterManager.GetBrush<TextureBrush>(bmp);
            global::System.Drawing.Drawing2D.Matrix m = new global::System.Drawing.Drawing2D.Matrix ();
            m.Translate(x, y, MatrixOrder.Prepend );
            cb.Transform = m;
            device.FillRectangle(cb, x, y, width, height);
            m.Reset();
            cb.Transform = m;
            m.Dispose();
        }
        public static byte TrimByte(this int i)
        {
            if (i < 0)
                return 0;
            if (i > 255)
                return (byte)i;
            return (byte)i;
        }
        public static T[] WinCoreArraySub<T>(this T[] t, int index)
        { 
            if (t==null)
                return null;
            if ((t.Length > index)&& (index >=0))
            {
                T[] r = new T[t.Length - index];
                for (int i = 0; i < r.Length; i++)
                {
                    r[i] = t[index + i];
                }
                return r;
            }
            return null;
        }
        /// <summary>
        /// get the enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int WinCoreGetValue<T>(this T e) where T : struct , IConvertible 
        {
            if (typeof(T).IsEnum)
            {
                return (int)e.ToInt32(null);
            }
            return 0;
        }
        /// <summary>
        /// get get the core bitmap from bitmap. get the reference to the gdi bitmap embed in this.
        /// check if this coreBitmap is a WinCoreBitmap
        /// </summary>
        /// <param name="coreBitmap"></param>
        /// <returns></returns>
        public static Bitmap GetGdiBitmap(this ICoreBitmap coreBitmap)
        {
            if (coreBitmap is WinCoreBitmap)
            {
                return (coreBitmap as WinCoreBitmap).Bitmap;
            }
            return null;
        }
        /// <summary>
        /// create a new gdi bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ToGdiBitmap(this ICoreBitmap coreBitmap)
        {
            return ToGdiBitmap(coreBitmap, false);
        }
        /// <summary>
        /// create a new  Gdi bitmap data from the ICoreBitmap
        /// </summary>
        /// <param name="coreBitmap"></param>
        /// <param name="dispose">true to dispose the coreBitmap</param>
        /// <returns></returns>
        public static Bitmap ToGdiBitmap(this ICoreBitmap coreBitmap, bool dispose)
        {
            if (coreBitmap == null)
                return null;

            try
            {

                Bitmap v_bmp = new Bitmap(coreBitmap.Width, coreBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(v_bmp))
                {
                    ICoreGraphics device = WinCoreBitmapDeviceVisitor.Create(g);
                    coreBitmap.Draw(device);
                    device.Flush();
                    g.Flush();
                }
                if (dispose)
                {
                    coreBitmap.Dispose();
                }
                return v_bmp;
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message 
                    );
            }
            return null;
        }
        /// <summary>
        /// draw a working object if this can be visited
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="device"></param>
        public static void Draw(this ICoreWorkingObject obj, ICoreGraphics device)
        {
            if ((obj == null) || (device == null)) return;
            if (device.Accept(obj))
            {
                device.Visit(obj);
            }
        }
      
        public static void Draw(this ICoreWorkingObject obj, ICoreGraphics device, Rectanglei size, bool proportional, enuFlipMode mode)
        {
            if ((obj == null) || (device == null)) return;
            if (device.Accept(obj))
            {
                device.Draw(obj,proportional, size, mode);
            }
        }
        

        public static void  WinCoreToGdiGraphicsPath(this GraphicsPath cpath,  CoreGraphicsPath path)
        {
            cpath.Reset();
            using (GraphicsPath c = path.WinCoreToGdiGraphicsPath())
            {
                if (c != null)
                    cpath.AddPath(c, false);
            }
        }
        public static GraphicsPath WinCoreToGdiGraphicsPath(this ICoreGraphicsPath path)
        {
            if (path == null)
                return null;
            path.GetAllDefinition(out Vector2f[] t, out byte[] b);
            if ((t != null) && (t.Length > 0) && (t.Length == b.Length ))
            {
                PointF[] v_rt = t.CoreConvertTo<System.Drawing.PointF[]>();
                if (v_rt != null)
                {
                    GraphicsPath cp = new GraphicsPath(v_rt, b)
                    {
                        FillMode = (FillMode)path.FillMode
                    };
                    return cp;
                }
            }
            return null;
        }
        private static FillMode WinCoreGetFillMode(this enuFillMode enuFillMode)
        {
            switch (enuFillMode)
            {
                case enuFillMode.Alternate:
                    return FillMode.Alternate;
            }
            return FillMode.Winding;
        }
        public static ICoreBitmap ToCoreBitmap(this Bitmap bitmap)
        {
            return WinCoreBitmap.Create(bitmap);
        }
        public static Color ToGdiColor(this Colorf cl)
        {
            return cl.CoreConvertTo<Color>();
        }
        public static Colorf ToIGKColor(this Color cl)
        {
            return cl.CoreConvertFrom<Colorf>();
        }
        public static Font ToGdiFont(this ICoreFont ft)     
        {
            if (ft == null)
                return null;
            //protect font size
            float v_fonts = ft.FontSize > 0 ? ft.FontSize : 1.0f;
            CoreUnit runit = string.Format("{0}px", v_fonts);
            var  family = ft.FontFamily();
            if (family == null) {
                return null;
            }
            Font v_ft = new Font(family,runit.GetPoint(),
                (FontStyle)ft.FontStyle, GraphicsUnit.Point);
            return v_ft;
    }
        public static Font ToGdiFont(this ICoreFont ft, float size, enuFontStyle style)
        {
            if (ft == null)
                return null;
            Font v_ft = new Font(ft.FontFamily(), size,(FontStyle ) style );
            return v_ft;
        }
        ///graphics path extensions
        public static void AddRectangle(this GraphicsPath path, Rectanglef rc)
        {
            path.AddRectangle(new RectangleF(rc.X, rc.Y, rc.Width, rc.Height));
        }
        public static void AddEllipse(this GraphicsPath path, Rectanglef rc)
        {
            path.AddEllipse(rc.X, rc.Y, rc.Width, rc.Height);
        }
        public static Vector2i  GetCenter(this Rectangle rc)
        { 
            Vector2f c = CoreMathOperation.GetCenter (new Rectanglef (rc.X , rc.Y, rc.Width, rc.Height ));
            return new Vector2i(
                (int)c.X,
                (int)c.Y);
        }
        public static Vector2f GetCenter(this Rectanglef rc)
        {
            Vector2f c = CoreMathOperation.GetCenter(new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height));
            return new Vector2f(
                c.X,
                c.Y);
        }
        #region ICoreGraphicsPathGraph Layere Element
        public static GraphicsPath GetPath(this ICore2DDrawingLayeredElement l)
        {
            if (l == null) return null;
            if (l.GetPath() is CoreGraphicsPath c)
            {
                return c.WinCoreToGdiGraphicsPath();
            }
            return null;
        }
        #endregion
        
    
        
        #region Control
        public static void SetBackgroundColor(this Control ctrl, Colorf cl)
        {
            ctrl.BackColor = cl.CoreConvertTo<Color>();
        }
        public static void SetBackColor(this Control ctrl, Colorf cl)
        {
            ctrl.BackColor = cl.CoreConvertTo<Color>();
        }
        #endregion
    }
}

