

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextureResource.cs
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
file:TextureResource.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    /// <summary>
    /// represent a core texture resources
    /// </summary>
    public class TextureResource : CoreResourceItemBase ,  ICoreTextureResource
    {
        private ICoreBitmap m_Bitmap;
        private string m_Source;
        /// <summary>
        /// get or set the source of the file
        /// </summary>
        public string Source
        {
            get { return m_Source; }
            set
            {
                if (m_Source != value)
                {
                    m_Source = value;
                }
            }
        }
        internal TextureResource()
        {
            this.m_Source = null;            
        }
        //.ctr
        internal TextureResource(string filename, string name, ICoreBitmap image)
        {
            if (!System.IO.File.Exists(filename))
                throw new CoreException(enuExceptionType.FileNotFound, "filename");
            if (string.IsNullOrEmpty(name))
                throw new CoreException(enuExceptionType.ArgumentNotValid, "image");
            if ((image == null) || (image.PixelFormat == enuPixelFormat.Undefined))
                throw new CoreException(enuExceptionType.ArgumentNotValid, "image");
            this.m_Source = filename;
            this.Id = name;
            this.m_Bitmap = image;
        }
        public ICoreBitmap GetBitmap()
        {
            return this.m_Bitmap;
        }
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Texture ; }
        }
        public override object GetData()
        {
            return this.GetBitmap();
        }
        public override string GetDefinition()
        {
            return this.Source;
        }
        internal protected override void SetValue(string value)
        {
            if (System.IO.File.Exists(value))
            {
                var v_res = CoreApplicationManager.Application.ResourcesManager;
                if (v_res != null)
                {
                    ICoreBitmap bmp = v_res.CreateBitmapFromFile(value);
                    if (bmp != null)
                    {
                        this.m_Bitmap = bmp;
                        this.m_Source = value;
                    }
                }
            }
        }
    }
}

