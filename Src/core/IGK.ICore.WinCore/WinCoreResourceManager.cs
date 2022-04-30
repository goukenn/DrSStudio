

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreResourceManager.cs
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
file:WinCoreResourceManager.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinCore
{
    using IGK.ICore.IO;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO.Files;
    using IGK.ICore.Drawing2D.Segments;
    using Native;
    using Windows.Native;
    public class WinCoreResourceManager : 
        CoreResourceManagerBase,
        ICoreResourceManager,
        ICoreGraphicsResource
    {

        public override ICoreBitmap CreateBitmap(object obj)
        {
            if (obj is Bitmap)
            {
                WinCoreBitmap bmp = WinCoreBitmap.Create(obj as Bitmap);
                return bmp;
            }
            if (obj is Byte[])
            {
                var doc = CoreResourcesManager.GetAllDocuments(obj as Byte[]);
                if ((doc != null) && (doc.Length > 0))
                {
                    return doc[0].ToBitmap ();
                }
            }
            return null;
        }

        public override  ICoreBitmap CreateBitmap(int width, int height)
        {
            return WinCoreBitmap.Create(width, height);
        }
        public override  ICoreBitmap CreateBitmapFromFile(string value)
        {
            return WinCoreBitmap.CreateFromFile(value);
        }
        public override ICoreGraphics CreateDevice(object obj)
        {
            return MethodInfo.GetCurrentMethod().Visit(this, obj) as ICoreGraphics ;
        }
        public ICoreGraphics CreateDevice(Graphics device)
        {
            return WinCoreBitmapDeviceVisitor.Create(device);
        }
        protected  ICoreGraphics CreateDevice(WinCoreBitmap bitmap)
        {
            Graphics g = Graphics.FromImage (bitmap.Bitmap );
            return WinCoreBitmapDeviceVisitor.Create(g);
        }
        public override ICoreBitmap CreateBitmap(System.IO.Stream stream)
        {
            try
            {
                Bitmap bmp = Bitmap.FromStream(stream) as Bitmap ;
                WinCoreBitmap c = WinCoreBitmap.Create (bmp);
                return c;
            }
            catch { 
            }

            return null;
        }
        public override ICoreFont CreateFont(string fontName, float size, enuFontStyle enuFontStyle, enuGraphicUnit enuGraphicUnit)
        {
            return null;
        }
        public override ICoreCursor GetCusor(ICoreBitmap bmp)
        {
            WinCoreBitmap v_bmp = bmp as WinCoreBitmap;
            if (v_bmp == null)
                return null;
            Bitmap p = v_bmp.Bitmap;
            IntPtr h = p.GetHbitmap();
            Bitmap mask =  p.WinCoreInvert();
            Graphics g = Graphics.FromImage(mask);
            g.Clear(Color.Transparent);
            g.Flush();
            g.Dispose();
            mask.MakeTransparent();
            IntPtr vd = mask.GetHbitmap();
            IconInfo info = new IconInfo();
            info.hbmColor = h;
            byte[] t = WinCoreBitmapOperation.GetMaskData(p, 1);
            IntPtr v = Marshal.AllocCoTaskMem(t.Length);
            Marshal.Copy(t, 0, v, t.Length);
            info.hbmMask = vd;
            Marshal.FreeCoTaskMem(v);
            info.isIcon = 0;
            info.hotspotx = 16;
            info.hotspoty = 16;
            IntPtr cursor = User32.CreateIconIndirect(ref info);
            User32.DeleteObject(h);
            User32.DeleteObject(vd);
            bmp.Dispose();
            Cursor c = new Cursor(cursor);
            return XCursor.CreateFromHWND(c.Handle);
        }
        //[StructLayout(LayoutKind.Sequential)]
        //internal struct IconInfo
        //{
        //    internal byte isIcon;
        //    internal int hotspotx;
        //    internal int hotspoty;
        //    internal IntPtr hbmMask;
        //    internal IntPtr hbmColor;
        //}
        //[DllImport("user32.dll")]
        //internal extern static IntPtr CreateIconIndirect(
        //ref IconInfo piconinfo);
        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //internal static extern bool DeleteObject(IntPtr hObject);



        public override CoreFont CreateFont(string fontName, float size, enuFontStyle style, enuRenderingMode mode)
        {
            CoreFont winFont = CoreFont.CreateFont(fontName, size, style, mode);
            return winFont;
        }
        /// <summary>
        /// used to create a font defition utility used internall
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public override ICoreFontInfo CreateFontInfo(string fontName)
        {
            ICoreFontInfo c = new WinCoreFontInfo(fontName);
            return c;
        }

        public override bool RegisterPrivateFont(string filename)
        {
            WinCoreFontInfo c = WinCoreFontInfo.CreateFontFromFile(filename);
            if (c != null)
            {
                CoreFont.RegisterFonts(c);
                return true;
            }
            return false;
        }
        public override ICorePathStringDefinition CreatePathStringDefinition(string text, Rectanglef bounds, CoreFont coreFont, enuStringAlignment halignment, enuStringAlignment valignment)
        {
            if ((string.IsNullOrWhiteSpace (text) || (coreFont == null)))
                return null;
            GraphicsPath c = new GraphicsPath();
            StringFormat v_format = coreFont.ToStringFormat();
            v_format.Alignment = (StringAlignment)halignment;
            v_format.LineAlignment = (StringAlignment)valignment;
            v_format.FormatFlags = StringFormatFlags.NoWrap;
            v_format.Trimming = StringTrimming.None;
            
            ICorePathStringDefinition v_o = null;
            CoreUnit px = (coreFont.FontSize + "px");

            float emValue = px.GetValue(enuUnitType.em);
            var v_fontfam = coreFont.FontFamily();
            //c.AddRectangle(bounds);
            c.AddString (text, v_fontfam, 
                (int)coreFont.FontStyle , 
                emValue, //new Point((int)bounds.X,(int) bounds.Y ),
                new RectangleF(bounds.X, bounds.Y, bounds.Width, (int)Math.Ceiling(bounds.Height)),
                v_format);
            try
            {
                if (c.PointCount > 0)
                    v_o = new PathStringDefinition(c.PathPoints.CoreConvertFrom<Vector2f[]>(), c.PathTypes);
            }
            catch { 
            }
            c.Dispose ();
            return v_o ;
        }
        public struct  PathStringDefinition : ICorePathStringDefinition
        {
            private Vector2f[] m_points;
            private byte[] m_pointTypes;
            public PathStringDefinition(Vector2f[] pointF, byte[] pointTypes)
            {
                this.m_points = pointF;
                this.m_pointTypes = pointTypes;
            }
            public Vector2f[] Points
            {
                get
                {
                    return this.m_points; 
                }
            }
            public byte[] PointTypes
            {
                get { return this.m_pointTypes; }
            }
        }
        public override  Rectanglef GetStringRangeBounds(string text, ICoreFont font, Rectanglef bounds, int from, int length)
        {
            if (!string.IsNullOrEmpty(text) && (length > 0) && (from >= 0) && ((from + length) <= text.Length))
            {
                using (Graphics p = Graphics.FromHwnd(IntPtr.Zero))
                {
                    using (Font ft = font.ToGdiFont())
                    {
                        StringFormat sf =  font.ToStringFormat();
                        sf.FormatFlags  |= StringFormatFlags.MeasureTrailingSpaces;
                        sf.SetMeasurableCharacterRanges(new CharacterRange[] { 
                        new CharacterRange (from , length )
                    });

                        Region[] r = p.MeasureCharacterRanges(text, ft, new RectangleF(
                            bounds.X,
                            bounds.Y,
                            bounds.Width,
                            bounds.Height
                            ), sf);
                        var rc = r[0].GetBounds(p);
                        sf.Dispose();
                        return new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height);
                   
                    }
                }
            }
            return Rectanglef.Empty;

        }
        public override Size2f MeasureString(string text, ICoreFont font)
        {
            if (string.IsNullOrEmpty (text ))
                return Size2f.Empty;
            Graphics p = Graphics.FromHwnd(IntPtr.Zero);
            Font ft = font.ToGdiFont ();
            StringFormat sf =  font.ToStringFormat();
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            SizeF c = p.MeasureString(text,  ft, new SizeF(short.MaxValue, short.MaxValue),
                sf);
            sf.Dispose();
            p.Dispose();
            ft.Dispose();
            return new Size2f(c.Width, c.Height);
        }
        public override Rectanglef  MeasureString(string text, ICoreFont font, Rectanglef rectangle, int index, int length)
        {
            if (string.IsNullOrEmpty(text) || (index < 0) || (length < 0) ||
            (text.Length < (index + length)))
            {
                return Rectanglef.Empty;
            }
            Font ft = font.ToGdiFont ();
            StringFormat sf = font.ToStringFormat ();
            sf.SetMeasurableCharacterRanges(new CharacterRange[] { 
                new CharacterRange(index, length)
            });

            Rectanglef v_r =  Rectanglef.Empty;
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {

               Region[] rc =  g.MeasureCharacterRanges(text, ft, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width,
                    rectangle.Height),
                    sf);

               var s = rc[0].GetBounds(g);
               v_r = new Rectanglef(
                   s.X, s.Y , s.Width, s.Height);
            }
            sf.Dispose();
            return v_r;


        }
        
        public override ICoreBitmap CreateBitmapFromStringData(string stringData)
        {
            Bitmap bmp = WinCoreBitmapOperation.StringBase64ToBitmap(stringData);
            if (bmp != null)
            {
                ICoreBitmap sc =  WinCoreBitmap.Create(bmp);
                

                return sc;
            }
            return null;
        }

        public override ICoreZipReader GetZipReader()
        {
            Assembly sm = CoreSystem.GetAddIns().GetAssembly(CoreConstant.ZIP_READER_ASM);
            if (sm != null) {
                Type t = sm.GetType(CoreConstant.ZIP_READER_TYPE);
                if (t != null)
                {
                    return sm.CreateInstance(t.FullName) as ICoreZipReader;
                }
            }
            return null; 
        }

        public override XCursor CreateCursor(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            XCursor c = null;
            switch (key.ToLower())
            {
                case "wait":
                    c = XCursor.CreateFromHWND(Cursors.WaitCursor.Handle);
                    break;
                case "default":
                case "normal":
                    c = XCursor.CreateFromHWND(Cursors.Default.Handle);
                    break;
                case "hand":
                    c = XCursor.CreateFromHWND(Cursors.Hand.Handle);
                    break;
                default:
                    break;
            }
            return c;
        }

        public bool GetGraphicsDefinition(Vector2f[] vector2f, bool closefigure, out Vector2f[] pt, out byte[] def)
        {
            if (vector2f.Length == 0)
            {
                pt = null;
                def =null;
                return false ;
            }
            GraphicsPath p = new GraphicsPath();
            PointF[] c = new PointF [vector2f.Length];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new PointF(vector2f[i].X, vector2f[i].Y);
            }
            
            if (closefigure)
                p.AddClosedCurve(c);
            else
                p.AddCurve(c);
            pt = p.PathPoints.CoreConvertFrom<Vector2f[]>();
            def = p.PathTypes;
            p.Dispose();
            return true;
        }
        
        /// <summary>
        /// init win core resources
        /// </summary>
        public override void Init()
        {
        }
    }
}

