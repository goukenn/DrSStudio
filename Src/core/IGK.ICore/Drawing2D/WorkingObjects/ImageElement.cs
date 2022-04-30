

/*
IGKDEV @ 2008-2016
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
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ImageElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement ("Image", null, IsVisible=false )]
    public class ImageElement : Core2DDrawingLayeredElement, ICore2DDrawingVisitable
    {
        ICoreBitmap m_Bitmap;
        public int Width { get { return this.m_Bitmap?.Width ?? 0;
        } }
        public int Height { get {
            return this.m_Bitmap?.Height ?? 0; 
        } }
        public ICoreBitmap Bitmap { get { return this.m_Bitmap; } }
        private enuInterpolationMode m_InterpolationMode;
        
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuInterpolationMode.Pixel) ]        
        [CoreConfigurablePropertyAttribute(Group = "GraphicProperties")]
        /// <summary>
        /// get or set the interpolation mode
        /// </summary>
        public enuInterpolationMode InterpolationMode
        {
            get { return m_InterpolationMode; }
            set
            {
                if (m_InterpolationMode != value)
                {
                    m_InterpolationMode = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        /// <summary>
        /// .ctr internally used
        /// </summary>
        protected  ImageElement()
        {
         
        }
        public override bool IsValid
        {
            get
            {
                return base.IsValid && (this.m_Bitmap != null);
            }
            protected set
            {
                base.IsValid = value;
            }
        }
        protected override void InitializeElement()
        {
            this.m_InterpolationMode = enuInterpolationMode.Pixel; 
            base.InitializeElement();            
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.CDATA:
                        this.m_Bitmap =  CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromStringData(xreader.Value);
                        if (this.m_Bitmap == null)
                            this.IsValid = false;
                        break;
                    case System.Xml.XmlNodeType.Element :
                        CoreXMLSerializerUtility.ReadNode(this, xreader);
                        break;
                }
            }
            
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
            if (!xwriter.EmbedBitmap)
            {
                xwriter.WriteAttributeString("Width", this.Width.ToString());
                xwriter.WriteAttributeString("Height", this.Height.ToString());
                xwriter.Resources.Add(this, this.Bitmap);
            }
        }
       
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            if (this.Bitmap == null)
                return;
            base.WriteElements(xwriter);
            
            if (xwriter.EmbedBitmap ) 
            {
                var data = this.Bitmap.ToStringData();
                if (data != null)
                {
                    xwriter.WriteCData(data);
                }
                else
                    xwriter.Error = true;
            }
        }
        public static ImageElement CreateFromFile(string filename)
        {
            ICoreBitmap bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(filename);
            if (bmp == null) return null;
            ImageElement img = new ImageElement();
            img.m_Bitmap = bmp;
            img.InitElement();
            return img;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.DisposeBitmap();           
           
        }

        protected void DisposeBitmap()
        {
            if (m_Bitmap != null)
            {
                m_Bitmap.Dispose();
                m_Bitmap = null;
            }
        }

        public override enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public static ImageElement CreateFromBitmap(ICoreBitmap bmp)
        {
            if (bmp == null) 
                return null;
            ICoreBitmap v_bmp = bmp.Clone() as ICoreBitmap;
            ImageElement img = new ImageElement();
            img.m_Bitmap = v_bmp;
            img.InitElement();
            return img;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path )
        {
            path.Reset();
            if ((this.Bitmap != null)&&(this.Bitmap .PixelFormat != enuPixelFormat.Undefined ))
            {
                path.AddRectangle(
                    0, 0,
                    this.Bitmap.Width,
                    this.Bitmap.Height);
            }
        }

        public virtual bool Accept(ICore2DDrawingVisitor visitor)
        {
            if (visitor != null)
                return true;
            return false;
        }

        public virtual  void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
            {
                return;
            }
            object obj = visitor.Save();
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend );            
            visitor.SetupGraphicsDevice(this);
            visitor.InterpolationMode = this.InterpolationMode;            
            visitor.Visit(this.Bitmap);
            visitor.Restore (obj);

        }

        /// <summary>
        /// used to invert color and raise event of color changed bitmap
        /// </summary>
        public void InvertColor()
        { 
            this.m_Bitmap.InvertColor();
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged,
                 this.m_Bitmap));
        }

        public void RotateBitmap(int i)
        {
            this.m_Bitmap.Rotate(i);
            this.InitElement();
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged,
             this.m_Bitmap));
        }


        /// <summary>
        /// change the bitmap with another one
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="temp"></param>
        public void SetBitmap(ICoreBitmap bitmap, bool temp)
        {


            if ((bitmap == null) || (bitmap.PixelFormat == enuPixelFormat.Undefined))
            {
                if (temp) {
                    OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged,
                    bitmap));
                }
                return;
            }

            this.DisposeBitmap();
            this.m_Bitmap = bitmap;
            this.InitElement();
            if (!temp)
            {
                OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BitmapChanged,
                    bitmap));
            }
        }
    }
}

