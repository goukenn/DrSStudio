

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBitmap.cs
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
file:WinCoreBitmap.cs
*/

using IGK.ICore.GraphicModels;
using IGK.ICore.Imaging;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
namespace IGK.ICore.WinCore
{
    /// <summary>
    /// represent a core bitmap. encapsulate bitmap data and store it as resource element
    /// </summary>
    public sealed class WinCoreBitmap  : CoreResourceItemBase, ICoreBitmap, ICoreResourceItem
    {
        private Bitmap m_bitmap;
        List<ICoreBitmap> m_bitmaps;
        private enuPixelFormat m_format;
        private string m_id;

        public static ICoreScreenInfo GetScreenInfo() {
            return CoreApplicationManager.Application.GetScreenInfo();
        }
        public Size2i Size {
            get {
                return new Size2i(m_bitmap.Width, m_bitmap.Height);
            }
        }
        public Bitmap  Bitmap {
            get {
                return this.m_bitmap;
            }
        }
        public int Width
        {
            get { return m_bitmap.Width; }
        }
        public int Height
        {
            get { return m_bitmap.Height; }
        }
        public enuPixelFormat PixelFormat
        {
            get { return this.m_format; }
        }
        public ICoreGraphics CreateDevice()
        {
            Graphics g = Graphics.FromImage(m_bitmap);
            return WinCoreBitmapDeviceVisitor.Create(g, m_bitmap );
        }
        public override void Dispose()
        {
            
            this.m_bitmap.Dispose();
            base.Dispose();
        }
        internal WinCoreBitmap()
        {
            this.m_bitmaps = new List<ICoreBitmap>();
        }
        public static WinCoreBitmap Create(int width, int height)
        {
            Bitmap v = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            WinCoreBitmap bmp = new WinCoreBitmap();
            bmp.m_bitmap = v;
            bmp.m_format = enuPixelFormat.Format32bppArgb;
            return bmp;
        }

        public static WinCoreBitmap Create(int width, int height, enuPixelFormat format)
        {
            Bitmap v = new Bitmap(width, height, GetFormat(format));
            WinCoreBitmap bmp = new WinCoreBitmap();
            bmp.m_bitmap = v;
            bmp.m_format = format;
            return bmp;
        }

        private static System.Drawing.Imaging.PixelFormat GetFormat(enuPixelFormat format)
        {
            switch (format)
            {
                case enuPixelFormat.Undefined:
                    return global::System.Drawing.Imaging.PixelFormat.Undefined;
                   
                case enuPixelFormat.Format32bppArgb:
                    break;
                case enuPixelFormat.Format24bpprgb:
                    return global::System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                default:
                    break;
            }
            return global::System.Drawing.Imaging.PixelFormat.Format32bppArgb;
        }
        public static WinCoreBitmap Create(Bitmap bmp)
        {
            if ((bmp == null)||(bmp .PixelFormat ==  global::System.Drawing.Imaging.PixelFormat.Undefined))
            {
                return null;
            }
            Bitmap c = bmp.Clone() as Bitmap;
            if (c != null)
            {
                bmp = GetCoreBitmap(c);
                if (c != bmp) {
                    c.Dispose();
                    c = null; //free clone copy
                }
                WinCoreBitmap v_obmp = new WinCoreBitmap
                {
                    m_bitmap = bmp,
                    m_format = enuPixelFormat.Format32bppArgb
                };
                return v_obmp;
            }
            return null;
        }
        private static Image[] GetAllPages(string file)
        {
            MemoryStream byteStream = null;
            List<Image> images = new List<Image>();
            Bitmap bitmap = null;
            try
            {
                bitmap = (Bitmap)Image.FromFile(file);
            }
            catch
            {
                return null;
            }
            int count = bitmap.GetFrameCount(FrameDimension.Page);
            for (int idx = 0; idx < count; idx++)
            {
                // save each frame to a bytestream
                bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                byteStream = new MemoryStream();
                bitmap.Save(byteStream, ImageFormat.Tiff);
                // and then create a new Image from it
                images.Add((Bitmap)Image.FromStream(byteStream));
                byteStream.Close();
            }
            bitmap.Dispose();
            return images.ToArray();
        }
        public static WinCoreBitmap CreateFromFile(string value)
        {
            if (!System.IO.File.Exists(value))
                return null;
            try
            {
                Image[] vimg = GetAllPages(value);
                if ((vimg != null) && (vimg.Length >0))
                {
                    WinCoreBitmap bmp = new WinCoreBitmap();
                    bmp.m_bitmap = GetCoreBitmap( vimg[0] as Bitmap) ;
                    bmp.m_format = enuPixelFormat.Format32bppArgb;
                    for (int i = 1; i <vimg .Length  ; i++)
			        {
                        Bitmap c = GetCoreBitmap (vimg[i] as Bitmap );
                        if (c!=null)
                        bmp.AddFrame(CreateWinCore(c));
			        }
                    return bmp;
                }
            }
            catch { 
            }
            return null;
        }
        private static ICoreBitmap CreateWinCore(System.Drawing.Bitmap c)
        {
            WinCoreBitmap bmp = new WinCoreBitmap();
            bmp.m_bitmap = c;
            bmp.m_format = enuPixelFormat.Format32bppArgb;
            return bmp;
        }
        private static System.Drawing.Bitmap GetCoreBitmap(System.Drawing.Bitmap v)
        {
            var screen = GetScreenInfo();
            if ((v == null) || (v.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined))
                return null;
            if ((v.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)               
                && (screen !=null)&& 
                (v.VerticalResolution == screen.DpiY) &&
                    (v.HorizontalResolution == screen.DpiY))
                return v;

                //&& (v.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {//convert to bitmap 

                int w = v.Width;
                int h = v.Height;
                Bitmap v_bmp = null;
                try
                {
                    try
                    {
                        v_bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        v_bmp.SetResolution(screen.DpiX, screen.DpiY);
                        using (Graphics g = Graphics.FromImage(v_bmp))
                        {
                            g.DrawImage(v, new Rectangle(Point.Empty, v_bmp.Size), 0, 0, w, h, GraphicsUnit.Pixel);
                            g.Flush();
                        }
                    }
                    catch {
                        //
                        v_bmp = new Bitmap(v);
                    }
                    //v.Dispose();
                    v = v_bmp;
                }
                catch (OutOfMemoryException oex)
                {
                    CoreLog.WriteLine($"Out of memory : {oex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    CoreLog.WriteLine($"Out of memory : {ex.Message}");
                }
                finally {
                    if (v != v_bmp) {
                        v_bmp?.Dispose();
                    }
                }

            }
            return v;
        } 
        public override string Id
        {
            get {
                m_id = m_id ?? "WinCoreBitmap_"+GetHashCode();
                return this.m_id; 
            }
        }
        public override object Clone()
        {
            Bitmap cbmp = this.m_bitmap.Clone() as Bitmap;
            WinCoreBitmap bmp = new WinCoreBitmap();
            bmp.m_bitmap = cbmp;
            bmp.m_format = this.m_format;
            return bmp;
        }
       
        public int FrameCount
        {
            get { return this.m_bitmaps.Count + 1; }
        }
        public ICoreBitmap GetFrame(int i)
        {
            if (i == 0)
            {
                return this;
            }
            else {
                int vi = i - 1;
                if (this.m_bitmaps.IndexExists(vi))
                    return m_bitmaps[vi];
            }
            return null;
        }
        public void AddFrame(ICoreBitmap frame)
        {
            if (m_bitmaps.Contains(frame))
            { 
            }
            this.m_bitmaps.Add(frame);
        }
        string ICoreIdentifier.Id
        {
            get { return this.Id; }
        }
        public bool Save(string filename, object ImageCodecInfo, object EncoderParameters)
        {
            ImageFormat img = ImageFormat.Bmp;
            ImageCodecInfo v_info = ImageCodecInfo as ImageCodecInfo;
            EncoderParameters v_p = EncoderParameters as EncoderParameters;
            try
            {
                this.Bitmap.Save(filename, v_info, v_p);
                return true;
            }
            catch { 
            }
            return false;
        }


        public bool Save(string filename, CoreBitmapFormat format)
        {
            ImageFormat frm = GetFormat(format.Name);
            bool v_r = false;

            Bitmap bmp = new Bitmap(this.m_bitmap.Width, this.m_bitmap.Height);


            if (frm != null)
            {
                // using (Bitmap bmp = new Bitmap(this.m_bitmap.Width, this.m_bitmap.Height))
                try
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {

                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        g.DrawImage(this.m_bitmap, 0, 0);
                        bmp.Save(filename, frm);
                        v_r = true;
                    }

                }
                catch (Exception ex) {
                    CoreLog.WriteLine(ex.Message);
                }
                finally
                {
                    bmp.Dispose();
                }
                return true;

                //try
                //{
                //    this.m_bitmap.Save(filename, frm);
                //    return true;
                //}
                //catch(Exception ex) {
                //    CoreLog.WriteLine(ex.Message);
                //}
                
            }
            return v_r;
        }

        private ImageFormat GetFormat(string name)
        {
            FieldInfo finfo = typeof(ImageFormat).GetField(name, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic |
                 System.Reflection.BindingFlags.Public| BindingFlags.IgnoreCase);
            if (finfo != null)
            {
                return finfo.GetValue(null) as ImageFormat;
            }
            return null;
        }

        public void SetResolution(float dpix, float dpiy)
        {
            this.m_bitmap.SetResolution(dpix, dpiy);
        }



        public void InvertColor()
        {
            Bitmap c = WinCoreBitmapOperation.WinCoreInvert(this.m_bitmap);
            this.m_bitmap.Dispose();
            this.m_bitmap = c;
            
        }
        public void Rotate(int rotationFlag)
        {
            this.m_bitmap.RotateFlip((RotateFlipType)rotationFlag);
        }

        /// <summary>
        /// get bitmap string data
        /// </summary>
        /// <returns></returns>
        public string ToStringData()
        {
            return WinCoreBitmapOperation.BitmapToBase64String(this.m_bitmap, true);
        }
        public string ToStringData(bool withSizeInfo)
        {
            return WinCoreBitmapOperation.BitmapToBase64String(this.m_bitmap, withSizeInfo);
        }
        /// <summary>
        /// save to memory stream
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="data"></param>
        public void Save(Stream mem, CoreBitmapFormat data)
        {
            if (CoreBitmapFormat.Png == data)
            {
                this.m_bitmap.Save(mem, ImageFormat.Png);
                return;
            }

            if (CoreBitmapFormat.JPEG == data)
            {
                this.m_bitmap.Save(mem, ImageFormat.Jpeg);
                return;
            }
        }

        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Bitmap; }
        }

        public override object GetData()
        {
            return Bitmap;
        }

        public override string GetDefinition()
        {
            return ToStringData();
        }

        protected override void SetValue(string value)
        {
            this.m_bitmap = WinCoreBitmapOperation.StringBase64ToBitmap(value);
        }


        public byte[] ToData()
        {
            var data = WinCoreBitmapData.FromBitmap(this).Data;
            return data;
        }
    }
}

