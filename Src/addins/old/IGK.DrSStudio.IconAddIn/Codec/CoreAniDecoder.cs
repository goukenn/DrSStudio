

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAniDecoder.cs
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
file:CoreAniDecoder.cs
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
    [CoreCodec("Ani", "image/ani", "ani")]
    class CoreAniDecoder : CoreDecoderBase
    {
        public bool IsValid(string filename)
        {
            BinaryReader binR = new BinaryReader(File.OpenRead(filename));
            bool v = false;
            if (binR.ReadInt32() == XAniCursor.RIFF)
            {
                v = true;
            }
            binR.Close ();
            return v;
        }
        public  System.Drawing.Image GetBitmap(string filename)
        {
            if (!File.Exists(filename))
                return null;
            Icon ico = new Icon(filename);
            Bitmap bmp = ico.ToBitmap();
            ico.Dispose();
            return bmp;
        }
        public override bool Open(ICoreWorkbench bench, string filename, bool attachToBench)
        {
            if (filename == null)
                return false ;
                if (IsValid(filename) == false)
                {
                    CoreLog.WriteDebug(string.Format ("Not a valid file : {0}",filename));
                    return false;
                }
                XAniCursor ctr = XAniCursor.OpenFile(filename);
                XAniSurface surface = new XAniSurface(ctr);
                bench.Surfaces.Add(surface);
                if (attachToBench)
                    bench.CurrentSurface = surface;
                return true ;
        }
    }
}

