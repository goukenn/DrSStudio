

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerBlockRenderer.cs
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
file:LayerBlockRenderer.cs
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
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    static class LayerBlockRenderer
    {
        public static Colorf XLayerBlockCheckedBackgroundColor
        {
            get {
                return CoreRenderer.GetColor("XLayerBlockCheckedBackgroundColor", Colorf.FromFloat (0.7f));
            }
        }
        public static Colorf XLayerBlockCheckedBorderColor
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockCheckedBorderColor", Colorf.FromFloat (0.6f));
            }
        }
        public static Colorf XLayerBlockBorderColor
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockBorderColor", Color.DarkBlue);
            }
        }
        public static Colorf XLayerBlockSelectedBackgroundBegin
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockSelectedBackgroundBegin", Colorf.FromFloat (0.4f) );
            }
        }
        public static Colorf XLayerBlockSelectedBackgroundEnd
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockSelectedBackgroundEnd", Colorf.FromFloat (0.5f) );
            }
        }
           public static Colorf XLayerBlockSelectedOverBackgroundBegin
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockSelectedOverBackgroundBegin", Colorf.FromFloat (0.8f) );
            }
        }
        public static Colorf XLayerBlockSelectedOverBackgroundEnd
        {
            get
            {
                return CoreRenderer.GetColor("XLayerBlockSelectedOverBackgroundEnd", Colorf.FromFloat (0.5f));
            }
        }
        public static Colorf XLayerBlockBackground {
            get {
                return CoreRenderer.GetColor("XLayerBlockBackground", Colorf.FromFloat(0.3f));
            }
        }
        public static Colorf XLayerBlockOverBackground{get{return CoreRenderer.GetColor("XLayerBlockOverBackground", Colorf.FromFloat (0.8f));            }        }
        public static Colorf XLayerBlockForeColor{ get { return CoreRenderer.GetColor("XLayerBlockForeColor", Colorf.Black); } }
        public static Colorf XLayerBlockSelectedForeColor { get { return CoreRenderer.GetColor("XLayerBlockSelectedForeColor", Colorf.White); } }
    }
}

