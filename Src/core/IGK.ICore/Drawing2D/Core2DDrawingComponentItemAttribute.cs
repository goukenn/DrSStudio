

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingComponentItemAttribute.cs
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
file:Core2DDrawingComponentItemAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [AttributeUsage(AttributeTargets.Class,
     AllowMultiple = false,
     Inherited = false)]
    public class Core2DDrawingComponentItemAttribute : Core2DDrawingStandardElementAttribute
    {
        public Core2DDrawingComponentItemAttribute(string name, Type type)
            : base(name, type)
        {
        }
        public override string GroupName
        {
            get
            {                
                return "Component";
            }
        }
        public override string GroupImageKey
        {
            get
            {
                return "Group_Component";
            }
        }
    }
}

