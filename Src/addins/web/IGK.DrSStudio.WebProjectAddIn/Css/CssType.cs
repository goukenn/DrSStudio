

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CssType.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
file:CssType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WebProjectAddIn.Css
{
    /// <summary>
    /// represent a css type
    /// </summary>
    public sealed class CssType
    {
        public readonly static CssType Mixed;
        public readonly static CssType Clip;
        public readonly static CssType Enum;
        public readonly static CssType Length;
        public readonly static CssType Url;
        public readonly static CssType WebColor;
        public readonly static CssType Angle;
        public readonly static CssType Rotate;
        public readonly static CssType Percent;
        private string m_value;
        static CssType() {
        Mixed = new CssType("mixed");
        Clip = new CssType("clip");
        Enum = new CssType("enum");
        Length = new CssType("length");
        Url = new CssType("url");
        WebColor = new CssType("web");
        Angle = new CssType("angle");
        Rotate = new CssType("rotate");
        Percent = new CssType("percent");
        }
        private CssType(string v)
        {
            this.m_value = v;
        }
        public override string ToString()
        {
            return this.m_value;
        }
    }
}

