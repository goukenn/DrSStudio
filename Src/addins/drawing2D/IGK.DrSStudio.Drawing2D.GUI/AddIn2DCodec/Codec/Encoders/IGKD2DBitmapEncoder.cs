

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DBitmapEncoder.cs
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
file:IGKD2DBitmapEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    using ICore.WinUI.Configuration;

    [IGKD2DBitmapEncoder("BitmapEncoder", "image/bitmap", "bmp")]
    public class IGKD2DBitmapEncoder : IGKD2DPictureEncoderBase
    {
        private int m_BitCount;
        public int BitCount
        {
            get { return m_BitCount; }
            set
            {
                if ((m_BitCount != value) && 
                        ((value ==4)||
                        (value == 8) ||
                        (value ==32) ||
                        (value == 24) 
                        ))
                {
                    m_BitCount = value;
                }
            }
        }
        public IGKD2DBitmapEncoder()
        {
            //default bitmap encoder value
            this.BitCount = 32;
        }
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            string v_dir = PathUtils.GetDirectoryName(filename);
            string v_file = Path.GetFileNameWithoutExtension(filename);
            string v_ext = Path.GetExtension(filename).ToUpper();
            if (documents.Length > 1)
            {
                for (int i = 0; i < documents.Length; i++)
                {
                    if (!SaveDocument(string.Format("{0}{1}{2}.{3}",
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file + "_" + i,
                        "Bmp"),
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
                        "Bmp");
                }
                using (ICoreBitmap vmp = documents[0].ToBitmap())
                {
                    return this.SavePicture(filename, vmp);
                    //vmp.SaveToFileName(filename, "BMP", this.BitCount);
                }
            }            
            return false;
        }
        public override bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            Image v_img  = bitmap.ToGdiBitmap ();
            if (v_img ==null)
                return false ;
            PixelFormat frm = PixelFormat.Format32bppRgb;
            switch (this.BitCount) {
                case 4:
                    frm = PixelFormat.Format4bppIndexed;
                    break;
                case 8:
                    frm = PixelFormat.Format8bppIndexed;break;
                case 24:
                    frm = PixelFormat.Format24bppRgb; break;
            }

            using (var g = (v_img as Bitmap).Clone(new Rectangle(Point.Empty, v_img.Size),
                    frm))
            {
                g.Save(filename, ImageFormat.Bmp);
                return true;
            }


            //ImageFormat img = ImageFormat.Bmp;
            //ImageCodecInfo v_info = GetImageEncodersInfo("BMP");
            //EncoderParameters v_p = new EncoderParameters(1);
            //v_p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth,
            //    (long)this.BitCount);
            //SetPropertyItem(v_img, filename);
            //bool v = false;
            //try
            //{
            //    v_img.Save(filename, v_info, v_p);
            //    v = true;
            //}
            //catch
            //{
            //    throw new CoreException(enuExceptionType.ParameterNotValid, "Bitmap");
            //}
            //return v;
        }



        public override bool CanConfigure =>  true;
        public override enuParamConfigType GetConfigType() => enuParamConfigType.ParameterConfig;

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var p= base.GetParameters(parameters);
            ICoreParameterGroup v_group = parameters.AddGroup("CodecDefinition", "lb.CodecDefinition.caption");
            v_group.AddItem(this.GetType().GetProperty("BitCount"));
            return p;
        }

    }
}

