

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DTiffEncoder.cs
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
file:IGKD2DTiffEncoder.cs
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
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    [IGKD2DBitmapEncoderAttribute("Tiff", "pictures/tiff", "tiff")]
    public sealed class IGKD2DTiffEncoder : IGKD2DPictureEncoderBase
    {
        private enuTiffBitCount m_BitCount;
        public enuTiffBitCount BitCount
        {
            get { return m_BitCount; }
            set
            {
                if (m_BitCount != value)
                {
                    m_BitCount = value;
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
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup v_group = parameters.AddGroup("CodecDefinition", "lb.CodecDefinition.caption");
            v_group.AddItem(this.GetType().GetProperty("BitCount"));
            return parameters;
        }
        
        public IGKD2DTiffEncoder()
        {
            this.m_BitCount = enuTiffBitCount.Bitcount24;
        }
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            string v_dir = PathUtils.GetDirectoryName(filename);
            string v_file = Path.GetFileNameWithoutExtension(filename);
            string v_ext = Path.GetExtension(filename).ToUpper();
            if (documents.Length > 1)
            {
                using (Bitmap bmp = documents[0].ToBitmapDpi(DpiX, DpiY).ToGdiBitmap ())
                {
                    if (bmp == null) return false;
                    SetPropertyItem(bmp,filename );
                    ImageCodecInfo v_info = GetImageEncodersInfo("TIFF");
                    EncoderParameters v_params = new EncoderParameters(2);
                    EncoderParameter v_frame = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag,
                        (long)EncoderValue.MultiFrame);
                    EncoderParameter v_bitcount = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth,
                        (long)this.BitCount);
                    v_params.Param[0] = v_frame;
                    v_params.Param[1] = v_bitcount;
                    System.IO.MemoryStream mem = new System.IO.MemoryStream();
                    bmp.Save(mem, v_info, v_params);
                    Bitmap v_framebmp = null;
                    for (int i = 1; i < documents.Length; i++)
                    {
                        v_framebmp  = documents[i].ToBitmapDpi (DpiX, DpiY).ToGdiBitmap ();
                        if (v_framebmp == null) continue;
                        v_frame = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag,
                            (long)EncoderValue.FrameDimensionPage);
                        v_params.Param[0] = v_frame;
                        bmp.SaveAdd(v_framebmp, v_params);
                        v_framebmp.Dispose();
                    }
                    v_frame = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag,
                  (long)EncoderValue.Flush);
                    v_params.Param[0] = v_frame;
                    bmp.SaveAdd(v_params);
                    mem.Flush();
                    mem.Seek(0, SeekOrigin.Begin);
                    FileStream v_fstream = File.Create(filename);
                    mem.WriteTo(v_fstream);
                    v_fstream.Flush();
                    v_fstream.Close();
                    mem.Close();
                }
            }
            else if (documents.Length == 1)
            {
                if (!this.Extensions.Contains(v_ext))
                {
                    filename = string.Format("{0}{1}{2}.{3}" +
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file,
                        "Tiff");
                }
               return this.SavePicture(filename , documents[0].ToBitmapDpi (DpiX, DpiY));
            }
            return false;
        }
        public override bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            ImageCodecInfo v_info = GetImageEncodersInfo("TIFF");
            EncoderParameters v_params = new EncoderParameters(1);
            v_params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, (long)this.BitCount);
            using (Bitmap v_bmp = bitmap.ToGdiBitmap())
            {
                if (v_bmp !=null)
                SetPropertyItem(v_bmp, filename);
                try
                {
                    v_bmp.Save(filename, v_info, v_params);
                }
                catch (Exception ex)
                {
                    CoreMessageBox.Show(new CoreException(enuExceptionType.ParameterNotValid, "Bitmap : " + ex.Message));
                }
            }
            return true;
        }
    }
}

