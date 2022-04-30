

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGDefNode.cs
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
file:SVGDefNode.cs
*/

using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.SVGAddIn
{
    class SVGDefNode : CoreXmlElement
    {
        public override bool Visible
        {
            get
            {
                return this.HasChild;
            }
        }
        public SVGDefNode()
        {
            this.Tag = "defs";
            this.setTagName ("defs");
        }
        public override string Render()
        {
            return base.Render();
        }
        public override string RenderXML(IXmlOptions option)
        {
            return base.RenderXML(option);
        }
    }
}

