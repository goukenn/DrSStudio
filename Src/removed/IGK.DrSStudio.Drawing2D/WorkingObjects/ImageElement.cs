

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageElement.cs
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
file:ImageElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging ;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Codec ;
    [Core2DDrawingStandardItem("Image",
        typeof(Mecanism),        
        IsVisible=false)]
    public class ImageElement  :         
        Core2DDrawingLayeredElement
    {
        private Bitmap m_Bitmap;
        private CompositingQuality m_ComposingQuality;
        private InterpolationMode  m_InterpolationMode;
        private ICoreTextureResource m_texturRes;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor)]
        public InterpolationMode  InterpolationMode
        {
            get { return m_InterpolationMode; }
            set
            {
                if ((m_InterpolationMode != value) && (value != System.Drawing.Drawing2D.InterpolationMode.Invalid ))
                {
                    m_InterpolationMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(CompositingQuality.Default)]
        /// <summary>
        /// get or set the compositing quality
        /// </summary>
        public CompositingQuality ComposingQuality
        {
            get { return m_ComposingQuality; }
            set
            {
                if ((m_ComposingQuality != value) && (value != CompositingQuality.Invalid ))
                {
                    m_ComposingQuality = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private ImageElement()
        {
            this.m_ComposingQuality = CompositingQuality.Default;
            this.m_InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        }
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("GraphicsProperty");
            g.AddItem(GetType().GetProperty("ComposingQuality"));
            g.AddItem(GetType().GetProperty("InterpolationMode"));
            return parameters;
        }
        /// <summary>
        /// edit with mecanism disabled
        /// </summary>
        public override bool CanEdit
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// no shawdow filter is supported
        /// </summary>
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        protected override void SetGraphicsProperty(Graphics g)
        {
            base.SetGraphicsProperty(g);
            g.CompositingQuality = this.ComposingQuality;
            g.InterpolationMode = this.InterpolationMode ;
        }
        public override object Clone()
        {
            return FromImage(this.m_Bitmap);
            //return base.Clone();
        }
        /// <summary>
        /// get the bitmap attached to the image element
        /// </summary>
        public Bitmap Bitmap
        {
            get {
                if (this.m_Bitmap == null)
                    throw new CoreException(enuExceptionType.ArgumentIsNull, "Bitmap Not define");
                return m_Bitmap;
            }            
        }
        [CoreXMLAttribute ()]
        public int Width
        {
            get {
                if ((this.m_Bitmap == null)||(this.Bitmap .PixelFormat == PixelFormat.Undefined ))
                    return 0;
                return this.Bitmap.Width ; }
        }
        [CoreXMLAttribute()]
        public int Height
        {
            get {
                if ((this.m_Bitmap == null) || (this.Bitmap.PixelFormat == PixelFormat.Undefined))
                    return 0;
                return this.Bitmap.Height ; }
        }
        protected override void GeneratePath()
        {
            if ((this.Bitmap == null) || (this.Bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            v_path.AddRectangle( new Rectangle(0,0,
                this.Bitmap.Width,
                this.Bitmap.Height));
            this.SetPath(v_path);
        }
        /// <summary>
        /// Create a new Image Element from Image Object
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static ImageElement FromImage(Image image)
        {
            if ((image == null) || (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined))
                throw new CoreException(enuExceptionType.ArgumentNotValid , "bmp");
            Bitmap cbmp = null;
            if ((image.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                && (image is Bitmap))
            {
                Bitmap vbmp = CoreBitmapOperation.ConvertToCoreBitmap(image as Bitmap);
                cbmp = vbmp;
            }
            else
            {
                //clone the image
                cbmp = new Bitmap(image);
            }
            ImageElement img = new ImageElement();
            img.m_Bitmap = cbmp;
            img.InitElement();
            return img;
        }
        public static ImageElement FromTexture(ICoreTextureResource textureResource)
        {
            ImageElement v_m = FromImage(textureResource.GetBitmap());
            if (v_m != null)
            {
                v_m.m_texturRes = textureResource;
            }
            return v_m;
        }
        protected override void BuildBeforeResetTransform()
        {           
        }
        public override void Dispose()
        {
            this.DisposeBitmap();
            base.Dispose();
        }
        private void DisposeBitmap()
        {
            if (this.m_Bitmap != null)
            {
                this.m_Bitmap.Dispose();
                this.m_Bitmap = null;
            }
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
         protected override void  ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            xreader.MoveToElement();
            Byte[] t = null;
            if (!xreader.IsEmptyElement)
            {
                while (xreader.Read())
                {
                    switch (xreader.NodeType)
                    {
                        case System.Xml.XmlNodeType.CDATA :
                            string[] v = xreader.Value.Split('|');
                            try
                            {
                                t = Convert.FromBase64String(v[v.Length >= 3 ? 2 : 0]);
                                //String sr = System.Text.ASCIIEncoding.ASCII.GetString(t, 0, t.Length);
                                MemoryStream mem = new MemoryStream();
                                //StreamWriter sw = new StreamWriter(mem);
                                //sw.Write(sr);
                                mem.Write(t, 0, t.Length);
                                mem.Seek(0, SeekOrigin.Begin);
                                Bitmap v_bmp = null;
                                v_bmp = Bitmap.FromStream(mem) as Bitmap ;
                                this.m_Bitmap = v_bmp;
                            }
                            catch {
                                if (v.Length >= 3)
                                {
                                    int w = Convert.ToInt32(v[0]);
                                    int h = Convert.ToInt32(v[1]);
                                    Bitmap bmp = new Bitmap(w, h);
                                    BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h),
                                         ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                                    System.Runtime.InteropServices.Marshal.Copy(
                                        t, 0,
                                        data.Scan0,
                                        t.Length);
                                    bmp.UnlockBits(data);
                                    this.m_Bitmap = bmp;
                                }
                            }
                            if (this.m_Bitmap != null)
                            {
                                CoreGraphicsPath v_path = new CoreGraphicsPath();
                                v_path.AddRectangle(new Rectangle(0, 0,
                                    this.Bitmap.Width,
                                    this.Bitmap.Height));
                                this.SetPath(v_path);
                            }
                            break;
                        case System.Xml.XmlNodeType .Element :
                            System.Reflection.PropertyInfo pr = GetType ().GetProperty (xreader.Name );
                            if ((pr !=null)&& (IGK.DrSStudio.Codec.CoreXMLElementAttribute .IsCoreXMLElementAttribute (pr)))
                            {
                                IGK.DrSStudio.Codec.CoreXMLSerializerUtility.SetElementProperty (xreader, pr, this, null);
                            }
                            break;
                        default :
                            break;
                    }
                }
            }
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            //save embed bitmap data
            if (xwriter.EmbedResource)
            {
                MemoryStream mem = new MemoryStream();
                this.m_Bitmap.Save(mem, ImageFormat.Png);
                mem.Seek(0, SeekOrigin.Begin);
                byte[] c_data = new byte[mem.Length];
                mem.Read(c_data, 0, c_data.Length);
                mem.Dispose();
                //BitmapData data = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height),
                //     ImageLockMode.ReadOnly, Bitmap.PixelFormat);
                //byte[] c_data = new byte[data.Height * data.Stride];
                //System.Runtime.InteropServices.Marshal.Copy(data.Scan0,
                //    c_data, 0, c_data.Length);
                //Bitmap.UnlockBits (data);
                try
                {
                    string v_d = Convert.ToBase64String(c_data, Base64FormattingOptions.None);
                    xwriter.WriteCData(v_d);
                   // xwriter.WriteValue(sr);
                    //xwriter.WriteElementString("Data", sr);
                    //xwriter.WriteCData(string.Format("{0}|{1}|{2}",
                    //this.Width,
                    //this.Height, v_d));
                }
                catch (Exception ex)
                {
                    CoreLog.WriteErrorLine("Error When writing Image Elements :" + ex.Message);
                }
            }
            else {
            }
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
            if (!xwriter.EmbedResource)
            {
                string id = xwriter.Resources.Add(this, this.Bitmap);
                xwriter.WriteAttributeString("Src",
                    string.Format("Rex/#",id)
                    );
            }
        }
        public override void Draw(Graphics g)
        {
            if ((this.m_Bitmap == null) || (this.m_Bitmap .PixelFormat == PixelFormat.Undefined ))
                return;
            try
            {
                lock (this.m_Bitmap)
                {
                    //this.ParentLayer.Elements.Remove(this);
                    RectangleF vrect = new RectangleF(Point.Empty, this.Bitmap.Size);
                    Vector2f[] points = CoreMathOperation.GetPoints(vrect);
                    Matrix mat = this.GetMatrix();
                    points = CoreMathOperation.TransformVector2fPoint(mat, points);
                    //mat.TransformPoints(points);
                    PointF[] dpoint = new PointF[3];
                    dpoint[0] = points[0];
                    dpoint[1] = points[1];
                    dpoint[2] = points[3];
                    GraphicsState s = g.Save();
                    SetGraphicsProperty(g);
                    g.DrawImage(this.Bitmap, dpoint, vrect, GraphicsUnit.Pixel);
                    g.Restore(s);
                }
            }
            catch(Exception ex) 
            {
                CoreLog.WriteDebug("Exception for "+ ex.Message);
            }
        }
        public void SetBitmap(Bitmap bmp, bool temp)
        {
            if ((bmp == null) || (bmp.PixelFormat == PixelFormat.Undefined))
                return;
            this.DisposeBitmap ();
            this.m_Bitmap = bmp;
            this.InitElement();
            if (!temp)
            {
                OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged));
            }
            else {
                this.Filters.FilterChanged = true;
            }
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.None;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        sealed class Mecanism : Core2DDrawingMecanismBase
        { 
        }
    }
}

