

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFileUtils.cs
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
file:XFileUtils.cs
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
using System.Drawing ;
namespace IGK.DrSStudio.Drawing3D.FileBrowser
{
    
using IGK.OGLGame.Graphics ;
    using IGK.ICore.Codec ;
    using IGK.ICore.Resources;
    using IGK.ICore;
    using IGK.DrSStudio;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinCore;

    class XFileUtils
    {
        public static void SetEmptyBitmap(Texture2D texture)
        {
            texture.GraphicsDevice.MakeCurrent ();
            Bitmap v_noting = new Bitmap (32,32);
            var b = CoreResources.GetBitmapResources (FBConstant.EMPTY_PICTURE);
            if (b!=null){
                b.Draw(v_noting);
            }
                //CoreResources.Get.GetBitmapResources(.GetDocumentImage(FBConstant .EMPTY_PICTURE, 256, 256) as Bitmap;
            if (v_noting != null)
                texture.ReplaceTexture(v_noting);
        }
        public static void ReplaceTexture(OGLGraphicsDevice device, 
            IGK.OGLGame.Graphics.Texture2D texture, 
            string file,
            int width,
            int height)
        {
            if ((width <= 0) || (height <= 0))
                return;
            float ex = 1.0f;
            float ey = 1.0f;
            float w = 0.0f;
            float h = 0.0f;
            device.MakeCurrent();
            if (string.IsNullOrEmpty(file) || !System.IO.File.Exists(file))
            {
                SetEmptyBitmap(texture);
            }
            else
            {
                try
                {
                    ICoreCodec[] d = CoreSystem.GetDecodersByExtension(System.IO.Path.GetExtension(file));
                    if (d != null)
                    {
                        ICoreBitmapDecoder deco = null;
                        //get the first deco
                        for (int i = 0; i < d.Length; i++)
                        {
                            deco = d[i] as ICoreBitmapDecoder;
                            if (deco != null) break;
                        }
                            if (deco!=null)
                            {
                                ICoreBitmap bmp = deco.GetBitmap(file);
                                //width = bmp.Width;
                                //height = bmp.Height;
                                Bitmap offbmp = new Bitmap(width, height);
                                    
                                    if (bmp != null)
                                    {
                                        ex = offbmp.Width / (float)bmp.Width;
                                        ey =  offbmp.Height / (float)bmp.Height;
                                        ex = ey = Math.Min(ex, ey);
                                        w = bmp.Width * ex;
                                        h = bmp.Height * ey;
                                        Graphics g = Graphics.FromImage(offbmp );
                                        g.Clear(Color.White);
                                        g.Flush();
                                        g.Dispose();
                                        WinCoreExtensions.Draw(bmp, offbmp
                                            ,
                                            Rectanglei.Round(new Rectanglef((width - w) / 2.0f,
                                            (height - h) / 2.0f,
                                            w,
                                            h))
                                            );
                                        
                                        bmp.Dispose();
                                    }
                                    else {
                                        Graphics g = Graphics.FromImage(offbmp);
                                        g.Clear(Color.Black);
                                        g.Flush();
                                        g.Dispose();
                                    }
                                     texture.ReplaceTexture(offbmp );
                                //Bitmap bmp = (deco as ICoreBitmapDecoder).GetBitmap(file, width, height, true );
                                //if (bmp != null)
                                //{
                                //    texture.ReplaceTexture(bmp);
                                //    bmp.Dispose();
                                //}
                                //else
                                //{
                                //    SetEmptyBitmap(texture);
                                //}
                            }
                        }
                        else
                        {
                            SetEmptyBitmap(texture);
                        }
                    
                }
                catch
                {
                    CoreLog.WriteDebug("Error When importing File");
                }
            }
        }
    }
}

