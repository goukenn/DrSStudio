

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BitmapDecoder.cs
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
file:IGKD2DBitmapDecoder.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    [IGKD2DBitmapDecoderAttribute("BitmapDecoder",
        "Image/bitmap-data", 
        "bmp;dib;gkdata;png;jpeg;jpg")]
    public class BitmapDecoder : 
        CoreDecoderBase ,
        ICoreBitmapDecoder
    {
        public override bool Open(ICoreWorkbench bench, string filenames, bool selected)
        {
            Type t = CoreSystem.GetWorkingObjectType (CoreConstant.DRAWING2D_SURFACE_TYPE );
            if (t==null)
                return false;
            string file = filenames;
                try
                {
                    ImageElement img = ImageElement.CreateFromFile(file);
                    if (img != null)
                    {
                            ICore2DDrawingSurface surface = t.Assembly.CreateInstance (t.FullName )  as ICore2DDrawingSurface;
                            if (surface!=null)
                            {
                                surface.CurrentDocument.SetSize(img.Width, img.Height);
                                surface.CurrentLayer.Elements.Add(img);
                                bench.AddSurface (surface, selected );
                                if (surface is ICoreWorkingFilemanagerSurface)
                                    (surface as ICoreWorkingFilemanagerSurface).FileName = file;
                            }
                    }
                }
                catch {
                    CoreLog.WriteDebug("Error when opening ");
                    return false;
                }
                return true;
            }        
     
        public virtual  ICoreBitmap GetBitmap(string file)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(file);
        }
       
    }
}

