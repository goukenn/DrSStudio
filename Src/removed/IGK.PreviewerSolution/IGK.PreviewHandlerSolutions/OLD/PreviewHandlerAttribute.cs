

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerAttribute.cs
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
file:PreviewHandlerAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.PreviewHandlerLib
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PreviewHandlerAttribute : Attribute
    {
        private string _name, _extension, _appId;
        public PreviewHandlerAttribute(string name, string extension, string appId)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (extension == null) throw new ArgumentNullException("extension");
            if (appId == null) throw new ArgumentNullException("appId");
            _name = name;
            _extension = extension;
            _appId = appId;
        }
        public string Name { get { return _name; } }
        public string Extension { get { return _extension; } }
        public string AppId { get { return _appId; } }
    }
}

