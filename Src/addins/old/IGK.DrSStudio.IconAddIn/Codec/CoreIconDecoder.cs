

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreIconDecoder.cs
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
file:CoreIconDecoder.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.XIcon
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    [CoreCodec("BASIC ICO DECODER", "image/icon", "ico")]
    public class CoreIconDecoder : CoreDecoderBase
    {
        public virtual bool IsValid(string filename)
        {          
            //read the 3 short valie from stream
            if (File.Exists (filename)==false)return false;
            BinaryReader bin = new BinaryReader (File.OpenRead (filename));
            short v1 = bin.ReadInt16 ();
            short v2 = bin.ReadInt16 ();
            short v3 = bin.ReadInt16 ();
            bin.Close();
            return ((v1 == 0)&&(v2 == 1));
        }
        public override bool Open(ICoreWorkbench bench, string filename, bool attach)
        {
            Core2DDrawingDocumentBase[] docs = (Core2DDrawingDocumentBase[])Open(filename);
            if ((docs == null) || (docs.Length == 0)) 
                return false ;
            XIconSurface surface = new XIconSurface(docs);                 
            bench.Surfaces.Add(surface);
            return true;
        }
        public virtual Core2DDrawingDocumentBase[] Open(string filename)
        {
            IconStruct ico = IconStruct.OpenFromFile(filename);
            if (ico.Equals(IconStruct.Empty))
                return null;
            string name = Path.GetFileName(filename);
            CoreIconDocument doc = null;
            CoreIconDocument[] documents = new CoreIconDocument[ico.Count];
            for (int i = 0; i < documents.Length; i++)
            {
                doc = new CoreIconDocument(name, ico[i]);
                documents[i] = doc;
            }
            return documents;
        }
        public ICoreWorkingDocument[] OpenFileDocument(string filename)
        {
            return Open(filename);
        }
        public virtual  System.Drawing.Image GetBitmap(string filename)
        {
            if (!File.Exists(filename))
                return null;
            Icon ico = new Icon(filename);
            Bitmap bmp= ico.ToBitmap();
            ico.Dispose();
            return bmp;
        }
    }
}

