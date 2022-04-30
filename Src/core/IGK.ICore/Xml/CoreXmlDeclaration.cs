

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlDeclaration.cs
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
file:XmlDeclaration.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Xml
{
    public sealed class CoreXmlDeclaration  : CoreXmlPreprocessor 
    {
        public CoreXmlDeclaration()
        {
            this.setTagName( "?xml");
        }
        public override bool CanAddChild
        {
            get
            {
                return false;
            }
        }
        public override string RenderXML(IXmlOptions option)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml ");
            sb.Append(this.RenderAttributes(option));
            sb.Append("?>");
            if (option.Indent)
                sb.AppendLine();
            return sb.ToString();
        }
    }
}

