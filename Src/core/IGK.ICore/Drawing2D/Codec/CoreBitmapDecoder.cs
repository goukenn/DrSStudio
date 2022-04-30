

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBitmapDecoder.cs
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
file:CoreBitmapDecoder.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Codec
{
    /// <summary>
    /// represent the default bitmap decoder
    /// </summary>
    public class CoreBitmapDecoder : 
        CoreDecoderBase,
        ICoreBitmapDecoder 
    {
        public override bool Open(ICore.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            ICoreBitmap v_bmpFile = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(filename);
            if (v_bmpFile != null)
            {
                ImageElement img = ImageElement.CreateFromBitmap(v_bmpFile);
                ICore2DDrawingSurface v = CoreSystem.CreateWorkingObject(CoreConstant.DRAWING2D_SURFACE_TYPE) as ICore2DDrawingSurface;
                if (v != null)
                {
                    v.CurrentDocument.SetSize(img.Width, img.Height);
                    v.CurrentDocument.BackgroundTransparent = true;
                    v.CurrentLayer.Elements.Add(img);
                    bench.AddSurface(v, true);
                    if (v is ICoreWorkingFilemanagerSurface)
                        (v as ICoreWorkingFilemanagerSurface).FileName = filename;
                    return true;
                }
            }
            return false;
        }
        public virtual ICoreBitmap GetBitmap(string file)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(file);
        }
    }
}

