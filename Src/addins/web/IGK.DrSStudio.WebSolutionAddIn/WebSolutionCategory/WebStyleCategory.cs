

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebStyleCategory.cs
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
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web
{
    class WebStyleCategory : WebSolutionCategoryItem
    {
        public override int Index
        {
            get
            {
                return 0x20;
            }
        }
        public WebStyleCategory():base()
        {

        }
        public override ICoreControl GetConfigControl()
        {
            return base.GetConfigControl();
        }
        public override enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
    }
}
