

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifDecoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
file:GifDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.GifAddIn.Codec
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Codec;
    
    using IGK.DrSStudio.GifAddIn;
    using IGK.DrSStudio.Drawing2D.Codec;
    using IGK.DrSStudio.GifAddIn.Gif;
    class  GifCodecAttribute  : CoreCodecAttribute
{
      public GifCodecAttribute(string name, string mimetype, string ext):base(name,mimetype,ext)
      {
      }
  }
  [GifCodecAttribute("gifdecoder", 
      "image/gif", GifConstant.FILE_EXTENSION, Category = CoreConstant.CAT_PICTURE)]
    class GifDecoder : CoreBitmapDecoder
    {
      /// <summary>
      /// open a gif decoder
      /// </summary>
      /// <param name="bench"></param>
      /// <param name="filename"></param>
      /// <param name="selectSurface"></param>
      /// <returns></returns>
      public override bool Open(ICoreWorkbench bench, string filename, bool selectSurface)
      {          
            ICoreBitmap bmp = null;
            try
            {
                Type t = CoreSystem.GetWorkingObjectType (CoreConstant.DRAWING2D_SURFACE_TYPE);
                Bitmap _cbmp = (Bitmap)Bitmap.FromFile(filename);
                bmp = (_cbmp).ToCoreBitmap ();
                if (bmp != null)
                {
                    ImageElement img = ImageElement.CreateFromBitmap(bmp);
                    Dictionary<string, object> _dic = new Dictionary<string, object>();
                    _dic.Add("FileName", filename);
                    _dic.Add("PixelFormat", _cbmp.PixelFormat);
                    _dic.Add("Width", _cbmp.Width);
                    _dic.Add("Height", _cbmp.Height);
                    IGK.ICore.Dependency.CoreDependencyExtension.SetParam(img, "Image:Info", _dic);
                    ICore2DDrawingSurface surface = t.Assembly.CreateInstance(t.FullName) as ICore2DDrawingSurface;
                    if (surface != null)
                    {
                        surface.CurrentDocument.SetSize(img.Width, img.Height);
                        surface.CurrentLayer.Elements.Add(img);
                        bench.AddSurface(surface, selectSurface );
                        if (surface is ICoreWorkingFilemanagerSurface)
                            (surface as ICoreWorkingFilemanagerSurface).FileName = filename;
                        
                    }
                    return true;
                }
            }
            catch
            {
                CoreLog.WriteError("Exception when opening the gif file");
            }
            return base.Open(bench, filename, selectSurface);
        }
        public override ICoreBitmap GetBitmap(string file)
        {
            Bitmap bmp = null;
            try
            {
                bmp = (Bitmap)Bitmap.FromFile(file);
                var c = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
                if (c > 1)
                { 
                }
                return bmp.ToCoreBitmap();
            }
            catch {
                CoreLog.WriteError("Exception when opening the gif file");
                
            }
            return base.GetBitmap(file);
        }
    }
}

