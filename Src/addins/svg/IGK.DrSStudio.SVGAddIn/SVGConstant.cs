

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:SVGConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// constante svg
    /// </summary>
    public class SVGConstant
    {
        public const string NAMESPACE = "http://www.w3.org/2000/svg";
        public const string VERSION = "1.1";

        public const string URL_ATTRIB_REGEX = @"url\((?<value>(.)+)\)";
        public const string HASH_ATTRIB_REGEX = @"#(?<value>(.)+)";

        public const string SERVICE_MANAGER_DESC = "provide a collection to convert to gkds constant";
    }
}

