

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCursorEncoder.cs
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
file:CoreCursorEncoder.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace IGK.DrSStudio.XIcon
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.WinUI.Configuration;
    [CoreCodec("IconAddInCURSOREncoder", "drs/cursor", "cur", Category=CoreConstant.CAT_PICTURE )]
    class CoreCursorEncoder : CoreIconEncoder 
    {
        private short m_hotspotx;
        private short m_hotspoty;
        public short HotSpotX { get { return this.m_hotspotx; } set { this.m_hotspotx = value; } }
        public short HotSpotY { get { return this.m_hotspoty; } set { this.m_hotspoty = value; } }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections v_d = parameters;
            ICoreParameterGroup v_g = v_d.AddGroup("HotSpot", "lb.HotSpot.caption");
            v_g.AddItem(GetType().GetProperty("HotSpotX"));
            v_g.AddItem(GetType().GetProperty("HotSpoty"));
            return v_d;
        }
        public bool Save(string filename, params ICoreWorkingDocument[] documents)
        {
            if (Path.GetExtension(filename).ToLower() != ".cur")
            {
                filename += ".cur";
            }
            FileStream fs = File.Create(filename);
            bool result = this.Save(fs, documents);
            fs.Flush();
            fs.Dispose();
            return result;
        }
        public  bool Save(Stream stream, params ICoreWorkingDocument[] documents)
        {
            if ((stream == null) || (documents == null) || (documents.Length == 0))
                return false;
            XIcon v_ico = GetXIcon(documents);
            XCursor v_cur = XCursor.FromXIcon(v_ico, m_hotspotx, m_hotspoty);
            v_cur.Save(stream);
            v_ico.Dispose();
            GC.Collect();
            return true;
        }
    }
}
