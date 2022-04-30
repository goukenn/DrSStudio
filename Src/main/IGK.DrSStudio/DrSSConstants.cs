

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSSConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio
{
    public static class DrSSConstants
    {
        public const string BASEVERSION = "3.0";
        public const string REVISION = "03.19";
        public const string VERSION = BASEVERSION + "." + REVISION;
#if DEBUG 
        public const string BUILD = "DEBUG";
#else
        public const string BUILD = "RELEASE";
#endif
        public const string VERSION_F = VERSION + "-"+ BUILD;

        //for DrSStudio
        public const String APP_TITLE = "IGKDEV DrSStudio";
        public const String APP_TITLE_1 = APP_TITLE + " - v " + BASEVERSION + " {1} ";
        public const String APP_TITLE_SURFACE = APP_TITLE_1 + " [ {0} ]";
        public const String APP_MAINFORM_TITLE_1 = APP_TITLE + " - Desktop - {0}";
        public const String APP_MAINFORM_SURFACE_TITLE_2 = "{1} - " + APP_MAINFORM_TITLE_1;

        internal static string GetVersion()
        {
            return string.Format("{0}.{1}", BASEVERSION, REVISION);
        }
    }
}
