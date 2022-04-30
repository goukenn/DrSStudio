

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DJPEGEncoder.cs
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
file:IGKD2DJPEGEncoder.cs
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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration ;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.IO;
    [IGKD2DBitmapEncoderAttribute("JPEG", "image/jpeg", "jpeg;jpg")]
    class IGKD2DJPEGEncoder : IGKD2DPictureEncoderBase 
    {
        private ICore2DDrawingDocument[] param_document;
        private int m_Quality;     
        /// <summary>
        /// get or set the quality of the bitmap
        /// </summary>
        public int Quality
        {
            get { return m_Quality; }
            set
            {
                if ((m_Quality != value)&& (value >0) && (value <= 100.0f))
                {
                    m_Quality = value;
                }
            }
        }
        public override bool CanConfigure
        {
            get
            {
                return true;
            }
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override void SetConfigParameter(object t)
        {
            if (t is ICore2DDrawingDocument[])
            {
                param_document = t as ICore2DDrawingDocument[];
            }
            else
                param_document = null;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterItem v_sizePreview = null;
            ICoreParameterGroup v_group = parameters.AddGroup("CodecDefinition", "lb.CodecDefinition.caption");
            v_group.AddTrackbar(this.GetType().GetProperty("Quality"), 0, 100, 100, (CoreParameterChangedEventHandler)delegate(object sender, CoreParameterChangedEventArgs e) {
                this.m_Quality = Convert.ToInt32 (e.Value);
                parameters.GetItem("SizePreview");
                UpdateInfo(parameters);
            }
                );
            v_group.AddItem(this.GetType().GetProperty("AdditionalInfo"));

            var picBox = CoreControlFactory.CreateControl("PictureBox") as ICorePictureBox;
            picBox.ZoomMode = enuZoomMode.Stretch;
            info = GetSaveInfo(param_document);
            if (picBox != null)
            { 
                picBox.Bitmap = info.Bitmap;
                v_group.AddItem("PictureBox", "lb.PictureBox", picBox);
            }
            v_sizePreview =  v_group.AddLabel("SizePreview");
            initParameterSetting(parameters);
            parameters.PreferedSize = new Size2i(450, 520);


            return parameters;
        }

     
        IGKD2DJPEGEncoderSaveInfo info;
        private void UpdateInfo(ICoreParameterConfigCollections parameters)
        {
            var h = parameters.GetItem("PictureBox") as ICoreParameterControl ;
            ICorePictureBox p = h.Control  as ICorePictureBox;
            if (info!=null)
                info.Dispose();
            info = GetSaveInfo(param_document);
            if (p != null) {
                p.Bitmap = info.Bitmap;
            }

            var s = parameters.GetItem("SizePreview");
            s.Value = string.Format("Quality:{0}  - Size: {1} ", this.Quality, info.Size.GetFileSize () );
        }

        private string GetSize()
        {
            return "100Ko";
        }
       

        public IGKD2DJPEGEncoder()
        {
            this.m_Quality = 100;
        }
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            string v_dir = PathUtils.GetDirectoryName(filename);
            string v_file = Path.GetFileNameWithoutExtension(filename);
            string v_ext = Path.GetExtension(filename).ToUpper();
            if (documents.Length > 1)
            {
                for (int i = 0; i < documents.Length; i++ )
                {
                    if (!SaveDocument(string.Format("{0}{1}{2}.{3}",
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file + "_" + i,
                        "Jpeg"),
                        new ICore2DDrawingDocument[] { documents[i] }))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (documents.Length == 1)
            {
                if (!this.Extensions.Contains(v_ext))
                {
                    filename = string.Format("{0}{1}{2}.{3}" +
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file,
                        "Jpeg");
                }
                return SavePicture(filename,documents[0].ToBitmapDpi(DpiX, DpiY));
            }
            return false;
        }
        public override bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            if ((bitmap == null) || (bitmap.PixelFormat == enuPixelFormat.Undefined ))
                return false ;
            Image v_img = bitmap.ToGdiBitmap();
            //ImageFormat img = ImageFormat.Bmp;
            ImageCodecInfo v_info = GetImageEncodersInfo("JPEG");
            EncoderParameters v_p = GetParameter();
                if (this.AdditionalInfo)
                    SetPropertyItem(v_img, filename);
                try
                {
                v_img.Save(filename, v_info, v_p);
                }
                catch
                {
                    throw new CoreException(enuExceptionType.ParameterNotValid, "Bitmap");
                }
                return true;
        }
        private IGKD2DJPEGEncoderSaveInfo GetSaveInfo(params ICore2DDrawingDocument[] document) {
            IGKD2DJPEGEncoderSaveInfo g = new IGKD2DJPEGEncoderSaveInfo();
            if ((document != null) && (document.Length > 0))
            {
                Image img = document[0].ToBitmapDpi(DpiX, DpiY).ToGdiBitmap();
                
                MemoryStream mem = new MemoryStream ();
                ImageCodecInfo v_info = GetImageEncodersInfo("JPEG");
                EncoderParameters v_p = GetParameter();
                img.Save(mem, v_info, v_p);
                mem.Seek (0, SeekOrigin.Begin );

                g.Src = img;
                var r = new Bitmap(mem);
                g.Bitmap = WinCoreBitmap.Create ( r);
                g.Size = mem.Length;
                r.Dispose();
                mem.Dispose();
            }
            return g;
        }
        
        private EncoderParameters GetParameter()
        {
            EncoderParameters v_params = new EncoderParameters(1);
            EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Quality);
            v_params.Param[0] = p;
            return v_params;
        }


        class IGKD2DJPEGEncoderSaveInfo : IDisposable 
        {
            public long Size;
            public Image Src;
            public ICoreBitmap Bitmap;

            public void Dispose()
            {
                if (Src != null)
                    Src.Dispose();
                if (Bitmap != null)
                    Bitmap.Dispose();

            }
        }
    }
}

