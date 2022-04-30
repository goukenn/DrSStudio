

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreIconDocument.cs
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
file:CoreIconDocument.cs
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
namespace IGK.DrSStudio.XIcon
{
    using IGK.ICore.Codec;
    using IGK.ICore.Tools;
    /// <summary>
    /// represent the core icon document    
    /// </summary>    
    [CoreWorkingObject("IconDocument")]
    public class CoreIconDocument : Core2DDrawingLayerDocument
    {
        //private IconStruct info;
        private XIcon icons;
        private enuCoreDocumentPixelFormat m_pixelFormat;
        public XIcon IconInfo {
            get {
                return icons;
            }
        }
        public enuCoreDocumentPixelFormat PixelFormat
        {
            get
            {
                return this.m_pixelFormat;
            }
            internal set {
                this.m_pixelFormat = value;
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            if (icons != null) {
                icons.Dispose();
                icons = null;
            }
        }
        internal CoreIconDocument():base()
        {
        }
        internal CoreIconDocument(string name, IconStruct info)  :base("1","1")          
        {
            this.BackgroundTransparent = true;
            icons = XIcon.FromIconStruct(info);
            ImageElement imgElement = ImageElement.CreateFromBitmap(icons.GetClonedImage(0).ToCoreBitmap());
            this.SetSize(imgElement.Width, imgElement.Height);                        
            this.CurrentLayer.Elements.Add(imgElement);
            this.PixelFormat = (enuCoreDocumentPixelFormat)imgElement.Bitmap.PixelFormat;
            //this.info = info;
            this.Id = name + "_" + info.Width + "x" + info.Height+"_"+info.Bitcount;
            this.PixelFormat = (enuCoreDocumentPixelFormat)info.Bitcount;
            Dictionary<string, object> v_info = new Dictionary<string, object>();
            v_info.Add("FileName", name);
            v_info.Add("Type", GetType().FullName);
            v_info.Add("Width", info.Width==0? imgElement.Width : info.Width );
            v_info.Add("Height", info.Height==0? imgElement.Height : info.Height );
            v_info.Add("Format", this.PixelFormat.ToString());
            IGK.ICore.Dependency.CoreDependencyExtension.SetParam(imgElement, "Image:Info", v_info);
          //  IGK.ICore.Dependency.CoreDependencyExtension.SetParam(imgElement, "FileName", name);
        }
    }
}

