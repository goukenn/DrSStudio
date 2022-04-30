

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreIconEncoder.cs
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
file:CoreIconEncoder.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.XIcon
{
    using IGK.ICore.Codec;
    using IGK.ICore.Tools;
    [CoreCodec ("ImagingIconEncoder","drs/image-ico", "ico",Category=CoreConstant.CAT_PICTURE)]
    class CoreIconEncoder : CoreEncoderBase 
    {
        public bool Save(string filename, params ICore2DDrawingDocument[] documents)
        {
            if (Path.GetExtension(filename).ToLower() != ".ico")
            {
                filename += ".ico";
            }
            FileStream fs = File.Create(filename);
            bool result  = this.Save(fs, documents);            
            fs.Flush();
            fs.Dispose();            
            return result;
        }
        public bool Save(Stream stream, params ICore2DDrawingDocument[] documents)
        {
            if ((stream == null) || (documents == null) || (documents.Length == 0))
                return false;
            XIcon ico = GetXIcon(documents);
            ico.Save(stream);
            GC.Collect();
            return true;
        }
        protected XIcon GetXIcon(ICoreWorkingDocument[] documents)
        {
            XIcon v_ico = null;
            int i = 0;
            Bitmap obmp = null;
            foreach (Core2DDrawingLayerDocument doc in documents)
            {
                if (v_ico == null)
                {
                    if (doc is CoreIconDocument)
                    {
                        v_ico = (doc as CoreIconDocument).IconInfo;
                    }
                    else
                        v_ico = XIcon.CreateIcon((int)doc.Width, (int)doc.Height, enuIconColor.trueColorRGBA);
                    Bitmap bmp = v_ico.GetImage(0);
                    Graphics g = Graphics.FromImage(bmp);
                    g.Clip = new Region(new Rectangle(Point.Empty, bmp.Size));
                    //g.Clear(Color.Red);
                    obmp = WinCoreBitmapOperation.GetBitmap(doc, CoreScreen.DpiX , CoreScreen.DpiY ).ToGdiBitmap();
                    g.DrawImage(obmp, Point.Empty);
                    //doc.Draw(g);
                    g.Flush();
                    g.Dispose();
                    obmp.Dispose();
                }
                else
                {
                    if (doc is CoreIconDocument)
                    {
                        v_ico.AddIcon((doc as CoreIconDocument).IconInfo.Clone() as XIcon);
                    }
                    else
                        v_ico.AddIcon((int)doc.Width, (int)doc.Height, enuIconColor.trueColorRGBA);
                    Bitmap bmp = v_ico.GetImage(i);
                    Graphics g = Graphics.FromImage(bmp);
                    g.Clip = new Region(new Rectangle(Point.Empty, bmp.Size));
                    //   doc.Draw(g);
                    obmp = WinCoreBitmapOperation.GetBitmap(doc, CoreScreen.DpiX, CoreScreen.DpiY).ToGdiBitmap();
                    g.DrawImage(obmp, Point.Empty);
                    g.Flush();
                    g.Dispose();
                }
                ++i;
            }
            return v_ico;
        }
        public  bool Save(ICoreWorkingSurface surface)
        {
            ICore2DDrawingSurface v_surface = (surface as ICore2DDrawingSurface);
            ICoreWorkingFilemanagerSurface v_ed = surface as ICoreWorkingFilemanagerSurface;
            XIcon v_icon = GetXIcon(v_surface.Documents.ToArray());
            if (v_icon != null)
            {
                FileStream stream = null;
                try
                {
                    stream = File.Create(v_ed.FileName);
                    v_icon.Save(stream);
                    stream.Flush();
                }
                catch {
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
                return true;
            }
            return false;
        }
        public  bool SaveToStream(ICoreWorkingSurface surface, Stream stream)
        {
            ICore2DDrawingSurface v_surface = (surface as ICore2DDrawingSurface);
            XIcon v_icon = GetXIcon(v_surface.Documents.ToArray());
            if (v_icon != null)
            {
                v_icon.Save(stream);
                stream.Flush();
                return true;
            }
            return false;
        }
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            try
            {
                XIcon v_icon = GetXIcon(documents);
                if (v_icon != null)
                {
                    FileStream stream = null;
                    try
                    {
                        stream = File.Create(filename );
                        v_icon.Save(stream);
                        stream.Flush();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                    }
                    return true;
                }
            }
            catch {
                return false;
            }
            return false;
        }
    }    
}

