

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAniEncoder.cs
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
file:CoreAniEncoder.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
namespace IGK.DrSStudio.XIcon.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [CoreCodec("IconAddInANIEncoder", "drs/ani", "ani", Category=CoreConstant.CAT_PICTURE )]
    class CoreAniEncoder : CoreEncoderBase 
    {
        private byte bitCount = 32;
        private int displayrate = 1 ;
        public byte BitCount { 
            get{
                return this.bitCount;
            }
            set {
                this.bitCount = value;
            }
        }
        public int DisplayRate {
            get {
                return this.displayrate;
            }
            set {
                this.displayrate = value;
            }
        }
        public bool Save(string filename, params ICoreWorkingDocument[] documents)
        {
            if (Path.GetExtension(filename).ToLower() != ".ani")
            {
                filename += ".ani";
            }
            FileStream fs = File.Create(filename);
            bool result = Save(fs, documents);
            fs.Close();
            return result;
        }
        public bool Save(Stream stream, params ICoreWorkingDocument[] documents)
        {
            Core2DDrawingDocumentBase d = documents[0] as Core2DDrawingDocumentBase;
            XAniCursor ctr = XAniCursor.CreateAni((int)d.Width, (int)d.Height, this.BitCount, this.DisplayRate);
            Bitmap bmp = null;
            for (int i = 0; i < documents.Length; i++)
            {
                if (documents[i] is ICore2DDrawingDocument)
                {
                    bmp = WinCoreBitmapOperation.GetGdiBitmap(documents[i] as ICore2DDrawingDocument,
                    CoreScreen.DpiX,
                    CoreScreen.DpiY);
                    ctr.AddFrame(bmp, 2);
                    bmp.Dispose();
                }
            }
            ctr.Save(stream);
            return true;
        }
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            throw new NotImplementedException();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
        public override bool CanConfigure
        {
            get
            {
                return base.CanConfigure;
            }
        }
    }
}

